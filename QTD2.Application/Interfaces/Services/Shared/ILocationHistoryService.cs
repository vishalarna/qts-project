using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Locations;
using QTD2.Infrastructure.Model.Location_History;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ILocationHistoryService
    {
        public Task<Location_History> CreateAsync(Location_HistoryCreateOptions options);

        public Task<Location_History> UpdateAsync(int id, Location_HistoryCreateOptions options);
        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);

        public Task<List<Location_History>> GetAllLocCatHistories();
        public Task<Location_History> GetLocCatHistory(int id);

        public Task<List<LocationLatestActivityVM>> GetHistoryAsync();

    }
}
