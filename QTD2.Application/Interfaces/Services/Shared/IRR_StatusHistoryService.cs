using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.RR_StatusHistory;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IRR_StatusHistoryService
    {
        public List<RR_StatusHistory> GetAsync();

        public Task<RR_StatusHistory> GetAsync(int id);

        public Task<RR_StatusHistory> CreateAsync(RR_StatusHistoryCreateOptions options);

        public Task<RR_StatusHistory> UpdateAsync(int id, RR_StatusHistoryCreateOptions options);

        public Task<RR_StatusHistory> DeleteAsync(int id);

        public Task<RR_StatusHistory> ActiveAsync(int id);

        public Task<RR_StatusHistory> DeactivateAsync(int id);
    }
}
