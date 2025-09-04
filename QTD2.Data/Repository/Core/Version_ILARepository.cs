using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class Version_ILARepository : Common.Repository<Version_ILA>, IVersion_ILARepository
    {
        public Version_ILARepository(QTDContext context)
            : base(context)
        {
        }
    }
}
