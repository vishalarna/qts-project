using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class Version_RegulatoryRequirementService : Common.Service<Version_RegulatoryRequirement>, IVersion_RegulatoryRequirementService
    {
        public Version_RegulatoryRequirementService(IVersion_RegulatoryRequirementRepository repository, IVersion_RegulatoryRequirementValidation validation)
            : base(repository, validation)
        {
        }
    }
}
