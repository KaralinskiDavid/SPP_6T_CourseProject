using AutoMapper;
using Dao.Impl.DaoModels;
using Domain.Impl.Models.IisApi;
using Domain.Impl.Models.Request;
using Domain.Impl.Models.Response;
using Dto.Identity;

namespace Service.Impl.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<PostRegisterRequestModel, LearningAssistantUser>();
            CreateMap<LearningAssistantUser, PostUserResponseModel>();

            CreateMap<FacultyModel, Faculty>()
                .ForMember(f => f.Abbreviation, opt => opt.MapFrom(fm => fm.Abbrev));
            CreateMap<Faculty, GetFacultyResponseModel>();

            CreateMap<SpecialityModel, Speciality>()
                .ForMember(s => s.Abbreviature, opt => opt.MapFrom(sm => sm.Abbrev));
            CreateMap<Speciality, GetSpecialityResponseModel>();
            CreateMap<Speciality, Domain.Impl.Models.SpecialityModel>()
                .ForMember(s => s.Faculty, opt => opt.Ignore())
                .ForMember(s => s.Groups, opt => opt.Ignore())
                .ForMember(s => s.HeadStudent, opt => opt.Ignore());

            CreateMap<GroupModel, Group>()
                .ForMember(g => g.SpecialityId, opt => opt.MapFrom(gm => gm.SpecialityDepartmentEducationFormId))
                .ForMember(g=>g.Number, opt=>opt.MapFrom(gm=>gm.Name));
            CreateMap<Student, Domain.Impl.Models.StudentModel>();
            CreateMap<Group, GetGroupResponseModel>();
            CreateMap<Group, Domain.Impl.Models.GroupModel>()
                .ForMember(s => s.Speciality, opt => opt.Ignore())
                .ForMember(s => s.Students, opt => opt.Ignore())
                .ForMember(s => s.Student, opt => opt.Ignore());

            CreateMap<Student, GetStudentResponseModel>().ForMember(s=>s.Group, opt=>opt.Ignore()).ForMember(s=>s.Speciality, opt=>opt.Ignore());

            CreateMap<LessonType, Domain.Impl.Models.LessonTypeModel>();
            CreateMap<Lesson, Domain.Impl.Models.LessonModel>();
            CreateMap<DaySchedule, Domain.Impl.Models.DayScheduleModel>();
            
            
            CreateMap<Schedule, GetScheduleResponseModel>();
            CreateMap<DaySchedule, DayScheduleModel>();

            CreateMap<ScheduleModel, Schedule>()
                .ForMember(s => s.Group, opt => opt.MapFrom(sm => sm.StudentGroup));
            CreateMap<LessonModel, Lesson>()
                .ForMember(l => l.Auditory, opt => opt.MapFrom(lm => (lm.Auditory==null||lm.Auditory.Count==0) ? null : lm.Auditory[0]))
                .ForMember(l => l.SubjectName, opt => opt.MapFrom(lm => lm.Subject))
                .ForMember(l => l.SubGroup, opt => opt.MapFrom(lm => lm.NumSubgroup))
                .ForMember(l => l.LessonType, opt => opt.Ignore())
                .ForMember(l=>l.WeekNumber, opt=>opt.MapFrom(lm=>string.Join(',', lm.WeekNumber)))
                .ForMember(l=>l.LessonTypeId, opt=>opt.MapFrom(lm=> lm.LessonType=="ЛК" ? 1 : lm.LessonType=="ЛР" ? 2 : 3));

        }

    }
}
