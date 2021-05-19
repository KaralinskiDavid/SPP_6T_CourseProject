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
    public class SpecialityFileSectionDao : BaseDao<SpecialityFileSection>, ISpecialityFileSectionDao<SpecialityFileSection>
    {
        public SpecialityFileSectionDao(DaoContext context) : base(context) { }

        public override async Task<SpecialityFileSection> GetItemByIdAsync(int id) => await _context.SpecialityFileSections.Include(sfc=>sfc.SpecialityFiles).FirstOrDefaultAsync(sfc=>sfc.Id==id);
    }
}
