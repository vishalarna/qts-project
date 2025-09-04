using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.DutyArea_History;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IDutyArea_HistoryService
    {
        public Task<List<DutyArea_History>> GetAsync();

        public Task<DutyArea_History> GetAsync(int id);

        public Task<DutyArea_History> CreateAsync(DutyArea_HistoryCreateOptions options);

        public Task<DutyArea_History> UpdateAsync(int id,DutyArea_HistoryCreateOptions options);

        public Task<DutyArea_History> DeleteAsync(int id);

        public Task<DutyArea_History> ActiveAsync(int id);

        public Task<DutyArea_History> DeactivateAsync(int id);
    }
}
