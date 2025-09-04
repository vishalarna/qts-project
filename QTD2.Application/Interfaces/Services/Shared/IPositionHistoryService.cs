using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Position;
using QTD2.Infrastructure.Model.PositionHistory;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IPositionHistoryService
    {
        public Task<Position_History> CreateAsync(Position_HistoryCreateOptions options);

        public Task<Position_History> UpdateAsync(int id, Position_HistoryCreateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);

        public Task<List<Position_History>> GetAllPositionHistories();

        public Task<Position_History> GetPositionHistory(int id);

        public Task<List<PositionLatestActivityVM>> GetHistoryAsync(bool getLatest);
    }
}
