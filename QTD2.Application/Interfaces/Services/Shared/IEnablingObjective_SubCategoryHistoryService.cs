using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.EnablingObjective_SubCategoryHistory;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IEnablingObjective_SubCategoryHistoryService
    {
        public Task<List<EnablingObjective_SubCategoryHistory>> GetAllEOSubCatHistories();

        public Task<EnablingObjective_SubCategoryHistory> GetEOSubCatHistory(int id);

        public Task<EnablingObjective_SubCategoryHistory> CreateEOSubCatHistory(EnablingObjective_SubCategoryHistoryCreateOptions options);

        public Task<EnablingObjective_SubCategoryHistory> UpdateEOSubCatHistory(int id, EnablingObjective_SubCategoryHistoryCreateOptions options);

        public System.Threading.Tasks.Task DeleteEOSubCatHistory(int id);

        public System.Threading.Tasks.Task ActiveEOSubCatHistory(int id);

        public System.Threading.Tasks.Task InActiveEOSubCatHistory(int id);
    }
}
