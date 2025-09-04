using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.EnablingObjective_CategoryHistory;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IEnablingObjective_CategoryHistoryService
    {
        public Task<List<EnablingObjective_CategoryHistory>> GetAllEOCatHistories();

        public Task<EnablingObjective_CategoryHistory> GetEOCatHistory(int id);

        public Task<EnablingObjective_CategoryHistory> CreateEOCatHistory(EnablingObjective_CategoryHistoryCreateOptions options);

        public Task<EnablingObjective_CategoryHistory> UpdateEOCatHistory(int id, EnablingObjective_CategoryHistoryCreateOptions options);

        public System.Threading.Tasks.Task DeleteEOCatHistory(int id);

        public System.Threading.Tasks.Task ActiveEOCatHistory(int id);

        public System.Threading.Tasks.Task InActiveEOCatHistory(int id);
    }
}
