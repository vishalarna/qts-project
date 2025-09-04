using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.SafetyHazard_Set;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ISafetyHazard_SetService
    {
        public Task<List<SafetyHazard_Set>> GetSafetyHazardSets();

        public Task<SafetyHazard_Set> GetSafetyHazardSets(int id);

        public Task<List<SafetyHazard_Set>> GetSetForSH(int id);

        public Task<SafetyHazard_Set> UpdateSafetyHazardSet(int id, SafetyHazard_SetCreateOptions options);

        public System.Threading.Tasks.Task DeleteSafetyHazardSet(int id);

        public Task<SafetyHazard_Set> CreateSafetyHazardSet(SafetyHazard_SetCreateOptions options);

        public Task<SafetyHazard_Set> CreateAndLinkWithSH(int id, SafetyHazard_SetCreateOptions options);
    }
}
