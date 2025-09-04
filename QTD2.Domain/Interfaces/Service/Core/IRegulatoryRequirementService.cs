using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IRegulatoryRequirementService : Common.IService<RegulatoryRequirement>
    {
        public System.Threading.Tasks.Task<List<RegulatoryRequirement>> GetRegulatoryRequirementsForEmployeeTrainingTowardProceduresAndRegulatoryRequirements(List<int> regulatoryRequirementIds);
        System.Threading.Tasks.Task<List<RegulatoryRequirement>> GetRegulatoryRequirementsWithIssuingAuthorityAsync();

    }
}
