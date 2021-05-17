using Dao.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dao
{
    public interface ISpecialityDao<T> : IBaseDao<T> where T:class
    {
    }
}
