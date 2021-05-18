using Dao;
using System;
using System.Collections.Generic;
using System.Text;
using Dao.Impl.DaoModels;
using AutoMapper;
using System.Threading.Tasks;
using Domain.Impl.Models.Request;

namespace Service.Impl
{
    public class QueueService : IQueueService
    {
        private readonly IQueueDao<Queue> _queueDao;
        private readonly IMapper _mapper;

        public QueueService(IQueueDao<Queue> queueDao, IMapper mapper)
        {
            _queueDao = queueDao;
            _mapper = mapper;
        }

        public async Task<bool> CreateQueue(PostQueueRequestModel request)
        {
            try
            {
                await _queueDao.CreateAsync(_mapper.Map<Queue>(request));
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteQueue(int queueId)
        {
            try
            {
                var queue = await _queueDao.GetItemByIdAsync(queueId);
                if (queue == null)
                    return false;
                await _queueDao.RemoveAsync(queue);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
