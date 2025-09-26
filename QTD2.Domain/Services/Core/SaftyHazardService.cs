using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class SaftyHazardService : Common.Service<Entities.Core.SaftyHazard>, Interfaces.Service.Core.ISaftyHazardService
    {
        public SaftyHazardService(ISaftyHazardRepository saftyHazardRepository, ISaftyHazardValidation saftyHazardValidation)
            : base(saftyHazardRepository, saftyHazardValidation)
        {
        }

        public async Task<List<SaftyHazard>> GetForSafetyHazardsByCategory(List<int> safetyHazardCategoryIds, bool includeInactiveSafetyHazards)
        {
            List<Expression<Func<SaftyHazard, bool>>> predicates = new List<Expression<Func<SaftyHazard, bool>>>();
            predicates.Add(r => safetyHazardCategoryIds.Contains(r.SaftyHazardCategoryId));

            if (!includeInactiveSafetyHazards)
            {
                predicates.Add(r => r.Active);
            }

            var saftyHazards = await FindWithIncludeAsync(predicates, new[] { "SaftyHazard_Category", "SaftyHazard_Abatements", "SaftyHazard_Controls", "SafetyHazard_Tool_Links.Tool", "SafetyHazard_Set_Links.SafetyHazardSet" });

            return saftyHazards.ToList();
        }

        public async Task<List<SaftyHazard>> GetForSafetyHazardsByPositionMatrix(List<int> safetyHazardIds, bool includeInactiveSafetyHazards)
        {
            List<Expression<Func<SaftyHazard, bool>>> predicates = new List<Expression<Func<SaftyHazard, bool>>>();
            predicates.Add(r => safetyHazardIds.Contains(r.Id));

            if (!includeInactiveSafetyHazards)
            {
                predicates.Add(r => r.Active);
            }

            var saftyHazards = await FindWithIncludeAsync(predicates, new[] { "SaftyHazard_Category", "SaftyHazard_Abatements", "SaftyHazard_Controls", "SafetyHazard_Tool_Links.Tool", "SafetyHazard_Set_Links.SafetyHazardSet" },true);

            return saftyHazards.ToList();
        }
        public async Task<List<SaftyHazard>> GetForSafetyHazardsAsync()
        {
            var saftyHazards = await AllWithIncludeAsync(new string[] { "SaftyHazard_Category" });
            return saftyHazards.ToList();
        }
        public async Task<List<SaftyHazard>> GetSafetyHazardsByIdAsync(List<int> safetyHazardIds)
        {
            List<Expression<Func<SaftyHazard, bool>>> predicates = new List<Expression<Func<SaftyHazard, bool>>>();
            predicates.Add(sh => safetyHazardIds.Contains(sh.Id));
            var saftyHazards = (await FindWithIncludeAsync(predicates, new[] { "SaftyHazard_Category", "SafetyHazard_EO_Links.EnablingObjective", "SaftyHazard_Abatements", "SaftyHazard_Controls", "SafetyHazard_Tool_Links.Tool", "SafetyHazard_Set_Links.SafetyHazardSet" }, true)).ToList();
            return saftyHazards;
        }

        public async Task<List<SaftyHazard>> GetSafetyHazardsForILAsync(List<int> safetyHazardIds, bool includeInActiveILA)
        {
            List<Expression<Func<SaftyHazard, bool>>> predicates = new List<Expression<Func<SaftyHazard, bool>>>();
            predicates.Add(sh => safetyHazardIds.Contains(sh.Id));
            var saftyHazards = (await FindWithIncludeAsync(predicates, new[] { "SaftyHazard_Category", "SaftyHazard_Abatements", "SaftyHazard_Controls", "SafetyHazard_Tool_Links.Tool", "SafetyHazard_Set_Links.SafetyHazardSet", "ILA_SafetyHazard_Links.ILA.Meta_ILAMembers_Links.MetaILA" }, true)).ToList();
            if (!includeInActiveILA)
            {
                foreach (var hazard in saftyHazards)
                {
                    hazard.ILA_SafetyHazard_Links = hazard.ILA_SafetyHazard_Links.Where(link => link.ILA != null && link.ILA.Active).ToList();
                }
            }

            return saftyHazards;
        }
    }
}
