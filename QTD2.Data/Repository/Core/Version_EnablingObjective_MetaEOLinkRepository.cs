using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Version_EnablingObjective_MetaEOLinkRepository : Common.Repository<Version_EnablingObjective_MetaEOLink>, IVersion_EnablingObjective_MetaEOLinkRepository
    {
        public Version_EnablingObjective_MetaEOLinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
