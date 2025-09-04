using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.SafetyHazard_History;
using QTD2.Infrastructure.Model.SaftyHazard;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ISafetyHazard_HistoryService
    {
        public Task<List<SaftyHazardLatestActivityVM>> GetAllSHHistories(bool getLatest);

        public Task<SafetyHazard_History> GetSHHistory(int id);

        public Task<SafetyHazard_History> CreateSHHistory(SaftyHazardOptions options);

        public Task<SafetyHazard_History> UpdateSHHistory(int id, SafetyHazard_HistoryCreateOptions options);

        public System.Threading.Tasks.Task DeleteSHHistory(int id);

        public System.Threading.Tasks.Task ActiveSHHistory(int id);

        public System.Threading.Tasks.Task InActiveSHHistory(int id);
    }
}
