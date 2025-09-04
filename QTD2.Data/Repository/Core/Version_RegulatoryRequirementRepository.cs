using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Services.Core;

namespace QTD2.Data.Repository.Core
{
    public class Version_RegulatoryRequirementRepository : Common.Repository<Version_RegulatoryRequirement>, IVersion_RegulatoryRequirementRepository
    {
        public Version_RegulatoryRequirementRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
