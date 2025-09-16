using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class EnablingObjective_MetaEO_LinkService : Common.Service<EnablingObjective_MetaEO_Link>, IEnablingObjective_MetaEO_LinkService
    {
        public EnablingObjective_MetaEO_LinkService(IEnablingObjective_MetaEO_LinkRepository repository, IEnablingObjective_MetaEO_LinkValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<EnablingObjective_MetaEO_Link>> GetMetaEnablingObjectivesByEoIdAsync(List<int> eoIds, bool includeInactiveEnablingObjectives)
        {
            List<Expression<Func<EnablingObjective_MetaEO_Link, bool>>> predicates = new List<Expression<Func<EnablingObjective_MetaEO_Link, bool>>>();
            predicates.Add(r => eoIds.Contains(r.EOID));

            if (!includeInactiveEnablingObjectives)
            {
                predicates.Add(r => r.MetaEO.Active);
            }
            var metaEnablingObjectives = await FindWithIncludeAsync(predicates, new[] { "MetaEO.EnablingObjective_Topic", "MetaEO.EnablingObjective_SubCategory", "MetaEO.EnablingObjective_Category"});
            return metaEnablingObjectives.ToList();
        }
    }
}
