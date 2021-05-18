using Domain.Impl.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IQueueService
    {
        public Task<bool> CreateQueue(PostQueueRequestModel request);
        public Task<bool> DeleteQueue(int queueId);
    }
}
