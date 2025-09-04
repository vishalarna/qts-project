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
    public class Version_EnablingObjective_ILALinkRepository : Common.Repository<Version_EnablingObjective_ILALink>, IVersion_EnablingObjective_ILALinkRepository
    {
        public Version_EnablingObjective_ILALinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
