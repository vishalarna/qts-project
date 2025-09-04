using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Locations;
using QTD2.Infrastructure.Model.Location_History;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ILocationService
    {
        public Task<List<Location>> GetAsync();

        public Task<Location> GetAsync(int Locid);

        public Task<Location> CreateAsync(Location_CreateOptions options);

        public Task<Location> UpdateAsync(int Locid, Location_CreateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(Location_HistoryCreateOptions options);
        public System.Threading.Tasks.Task ActiveAsync(Location_HistoryCreateOptions options);

        public Task<int> getCount();

        public Task<LocationStatsVM> GetStatsCount();

        public System.Threading.Tasks.Task InActiveAsync(Location_HistoryCreateOptions options);

        public System.Threading.Tasks.Task LocationActivateAsync(int id, LocationOptions options);

        public System.Threading.Tasks.Task LocationDeactivateAsync(int id, LocationOptions options);

        public Task<List<Location_Category>> GetCatActiveInactive(string option);

        public Task<List<Location>> GetLocActiveInactive(string option);



    }
}
