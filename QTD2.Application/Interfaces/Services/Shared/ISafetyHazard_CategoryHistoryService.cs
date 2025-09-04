using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.SafetyHazard_Category;
using QTD2.Infrastructure.Model.SafetyHazard_CategoryHistory;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ISafetyHazard_CategoryHistoryService
    {
        public Task<List<SafetyHazard_CategoryHistory>> GetAllSHCatHistories();

        public Task<SafetyHazard_CategoryHistory> GetSHCatHistory(int id);

        public Task<SafetyHazard_CategoryHistory> CreateSHCatHistory(SafetyHazard_CategoryHistoryCreateOptions options);

        public Task<SafetyHazard_CategoryHistory> UpdateSHCatHistory(int id, SafetyHazard_CategoryHistoryCreateOptions options);

        public System.Threading.Tasks.Task DeleteSHCatHistory(int id);

        public System.Threading.Tasks.Task ActiveSHCatHistory(int id);

        public System.Threading.Tasks.Task InActiveSHCatHistory(int id);

        public System.Threading.Tasks.Task CreateDeleteHistAsync(SaftyHazardCategoryOptions options);
    }
}
