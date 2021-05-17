using Domain.Impl.Models.IisApi;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Impl
{
    public class BsuirIisApiService : IBsuirIisApiService
    {
        private readonly RestClient _client;
        private readonly string _baseUrl = "https://journal.bsuir.by/api/v1";

        public BsuirIisApiService()
        {
            _client = new RestClient(_baseUrl);
        }

        public async Task<ScheduleModel> GetScheduleByGroupNumber(string groupName)
        {
            var request = new RestRequest("studentGroup/schedule").AddQueryParameter("studentGroup", groupName);
            return await _client.GetAsync<ScheduleModel>(request);
        }

        public async Task<int> GetCurrentWeek()
        {
            var request = new RestRequest("week");
            return await _client.GetAsync<int>(request);
        }

        public async Task<DateTime> GetLastScheduleUpdateDate(string groupName)
        {
            var request = new RestRequest("studentGroup/lastUpdateDate").AddQueryParameter("studentGroup", groupName);
            return await _client.GetAsync<DateTime>(request);
        }

        public async Task<IList<FacultyModel>> GetFaculties()
        {
            var request = new RestRequest("faculties", DataFormat.Json);
            return await _client.GetAsync<IList<FacultyModel>>(request);
        }

        public async Task<IList<SpecialityModel>> GetSpecialities()
        {
            var request = new RestRequest("specialities", DataFormat.Json);
            return await _client.GetAsync<IList<SpecialityModel>>(request);
        }

        public async Task<IList<GroupModel>> GetGroups()
        {
            var request = new RestRequest("groups", DataFormat.Json);
            return await _client.GetAsync<IList<GroupModel>>(request);
        }

    }
}
