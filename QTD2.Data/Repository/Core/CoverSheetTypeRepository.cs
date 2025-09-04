using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class CoverSheetTypeRepository : Common.Repository<CoverSheetType>, ICoverSheetTypeRepository
    {
        public CoverSheetTypeRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
