using AutoMapper;
using Dao;
using Dao.Impl.DaoModels;
using Domain.Impl.Models.Response;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore;

namespace Service.Impl
{
    public class ScheduleService : IScheduleService
    {
        private readonly IBsuirIisApiService _bsuirIisApiService;
        private readonly IMapper _mapper;
        private readonly IScheduleDao<Schedule> _scheduleDao;
        private readonly IGroupDao<Group> _groupDao;

        public ScheduleService(IBsuirIisApiService bsuirIisApiService, IMapper mapper, IScheduleDao<Schedule> scheduleDao,
            IGroupDao<Group> groupDao)
        {
            _bsuirIisApiService = bsuirIisApiService;
            _mapper = mapper;
            _scheduleDao = scheduleDao;
            _groupDao = groupDao;
        }

        public async Task<bool> RefreshGroupSchedule(string groupNumber)
        {
            try
            {
                var group = await _groupDao.GetGroupByNumber(groupNumber);
                if (group == null)
                    return false;

                var schedule = await _bsuirIisApiService.GetScheduleByGroupNumber(groupNumber);
                List<DaySchedule> daySchedules = new List<DaySchedule>();
                foreach (var daySchedule in schedule.Schedules)
                {
                    daySchedules.Add(new DaySchedule()
                    {
                        Lessons = _mapper.Map<List<Lesson>>(daySchedule.Schedule),
                        DayNumber = GetDayNumber(daySchedule.WeekDay)
                    });
                }
                var model = _mapper.Map<Schedule>(schedule);
                model.DaySchedules = daySchedules;
                model.Group = group;

                if (group.ScheduleId != null)
                {
                    model.Id = group.ScheduleId.Value;
                    await _scheduleDao.UpdateAsync(model);
                }
                else
                {
                    await _scheduleDao.CreateAsync(model);
                    group.ScheduleId = (await _scheduleDao.GetScheduleByGroupId(group.Id)).Id;
                    await _groupDao.UpdateAsync(group);
                    
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<GetScheduleResponseModel> GetScheduleByGroupNumber(string groupNumber)
        {
            try
            {
                var group = await _groupDao.GetGroupByNumber(groupNumber);
                if (group == null)
                    return null;
                if (group.ScheduleId == null)
                {
                    await RefreshGroupSchedule(groupNumber);
                    return _mapper.Map<GetScheduleResponseModel>(await _scheduleDao.GetScheduleByGroupId(group.Id));
                }
                else
                    return _mapper.Map<GetScheduleResponseModel>(await _scheduleDao.GetScheduleByGroupId(group.Id));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private int GetDayNumber(string dayName)
        {
            switch (dayName)
            {
                case "Понедельник":
                    return 1;
                case "Вторник":
                    return 2;
                case "Среда":
                    return 3;
                case "Четверг":
                    return 4;
                case "Пятница":
                    return 5;
                case "Суббота":
                    return 6;
                default:
                    return 0;
            }
        }

        public async Task<int> GetCurrentWeek() => await _bsuirIisApiService.GetCurrentWeek();
    }
}
