using Dao.Impl.Base;
using Dao.Impl.DaoModels;
using Dao.Impl.DaoModels.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Dao.Impl
{
    public class SpecialityFileDao : BaseDao<SpecialityFile>, ISpecialityFileDao<SpecialityFile>
    {
        public SpecialityFileDao(DaoContext context) : base(context) { }

        public override async Task<SpecialityFile> GetItemByIdAsync(int id) => await _context.SpecialityFiles.FirstOrDefaultAsync(sf=>sf.Id==id);
    }
}
