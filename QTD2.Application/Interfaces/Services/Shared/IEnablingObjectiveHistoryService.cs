using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.EnablingObjectiveHistory;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IEnablingObjectiveHistoryService
    {
        public Task<List<EnablingObjectiveHistory>> GetAllEOHistories();

        public Task<EnablingObjectiveHistory> GetEOHistory(int id);

        public Task<EnablingObjectiveHistory> CreateEOHistory(EnablingObjectiveHistoryCreateOptions options);

        public Task<EnablingObjectiveHistory> UpdateEOHistory(int id, EnablingObjectiveHistoryCreateOptions options);

        public System.Threading.Tasks.Task DeleteEOHistory(int id);

        public System.Threading.Tasks.Task ActiveEOHistory(int id);

        public System.Threading.Tasks.Task InActiveEOHistory(int id);

        public Task<List<EOLatestActivityVM>> GetLatestActivity(bool getTrimmed);

        public Task<List<EOLatestActivityVM>> GetLatestActivity(int id);
    }
}
