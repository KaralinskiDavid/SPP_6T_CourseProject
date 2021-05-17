using Dao.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dao
{
    public interface IStudentDao<T> : IBaseDao<T> where T: class
    {
        public Task<T> GetStudentByUserId(string userId);
    }
}
