using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Tool_StatusHistory;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITool_StatusHistoryService
    {
        public Task<List<Tool_StatusHistory>> GetAsync();

        public Task<Tool_StatusHistory> GetAsync(int id);

        public System.Threading.Tasks.Task CreateAsync(Tool_StatusHistoryCreateOptions options);

        public Task<Tool_StatusHistory> UpdateAsync(int id, Tool_StatusHistoryUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);

        public Task<List<ToolLatestActivityVM>> GetAllToolHistories();

    }
}
