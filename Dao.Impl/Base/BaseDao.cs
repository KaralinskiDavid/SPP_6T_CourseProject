using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dao.Base;
using Dao.Impl.DaoModels.Context;
using Microsoft.EntityFrameworkCore;

namespace Dao.Impl.Base
{
    public abstract class BaseDao<T> : IBaseDao<T> where T:class
    {
        protected readonly DaoContext _context;

        public BaseDao(DaoContext context) => _context = context;
        public async Task<int> CreateAsync(T item)
        {
            await _context.Set<T>().AddAsync(item);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> CreateRangeAsync(IEnumerable<T> items)
        {
            await _context.Set<T>().AddRangeAsync(items);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveAsync(T item)
        {
            _context.Set<T>().Remove(item);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveRangeAsync(IEnumerable<T> items)
        {
            _context.Set<T>().RemoveRange(items);
            return await _context.SaveChangesAsync();
        }

        public abstract Task<T> GetItemByIdAsync(int id);

        public async Task<IQueryable<T>> GetItemsAsync() => await Task.Run(()=>_context.Set<T>());

        public async Task<int> UpdateAsync(T item)
        {
            await Task.Run(() => _context.Set<T>().Update(item));
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateRangeAsync(IEnumerable<T> items)
        {
            await Task.Run(() => _context.UpdateRange(items));
            return await _context.SaveChangesAsync();
        }
    }
}
