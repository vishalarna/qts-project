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
            if (!includeInActiveILA)
            {
                predicates.Add(r => r.SafetyHazard_ILA_Links.Any(link => link.ILA.Active));
            }
            var saftyHazards = (await FindWithIncludeAsync(predicates, new[] { "SaftyHazard_Category", "SaftyHazard_Abatements", "SaftyHazard_Controls", "SafetyHazard_Tool_Links.Tool", "SafetyHazard_Set_Links.SafetyHazardSet", "SafetyHazard_ILA_Links.ILA.Meta_ILAMembers_Links.MetaILA" }, true)).ToList();
            foreach (var hazard in saftyHazards)
            {
                Console.WriteLine($"Hazard: {hazard.Title} ({hazard.Id})");

                foreach (var link in hazard.SafetyHazard_ILA_Links)
                {
                    var ila = link.ILA;
                    if (ila == null) continue;

                    Console.WriteLine($"  ILA: {ila.Name} ({ila.Id})");

                    if (ila.Meta_ILAMembers_Links != null && ila.Meta_ILAMembers_Links.Any())
                    {
                        foreach (var metaLink in ila.Meta_ILAMembers_Links)
                        {
                            Console.WriteLine($"    MetaILA Link → MetaILAId: {metaLink.MetaILA?.Id}, Title: {metaLink.MetaILA?.Name}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("    No MetaILA member links");
                    }
                }
            }
            return saftyHazards;
        }
    }
}
