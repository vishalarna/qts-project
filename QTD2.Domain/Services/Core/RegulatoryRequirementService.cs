using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class RegulatoryRequirementService : Common.Service<RegulatoryRequirement>, IRegulatoryRequirementService
    {
        public RegulatoryRequirementService(IRegulatoryRequirementRepository repository, IRegulatoryRequirementValidation validation)
            : base(repository, validation)
        {
        }

		public async Task<List<RegulatoryRequirement>> GetRegulatoryRequirementsForEmployeeTrainingTowardProceduresAndRegulatoryRequirements(List<int> regulatoryRequirementIds)
		{
            List<Expression<Func<RegulatoryRequirement, bool>>> predicates = new List<Expression<Func<RegulatoryRequirement, bool>>>();

            predicates.Add(p => regulatoryRequirementIds.Contains(p.Id));

            var regulatoryRequirements = (await FindWithIncludeAsync(predicates, new string[] {
                "ILA_RegRequirement_Links"
            })).ToList();

            return regulatoryRequirements;
        }

        public async System.Threading.Tasks.Task<List<RegulatoryRequirement>> GetRegulatoryRequirementsWithIssuingAuthorityAsync()
        {
            return (await AllWithIncludeAsync(new string[] { "RR_IssuingAuthority" })).ToList();
        }
    }
}
