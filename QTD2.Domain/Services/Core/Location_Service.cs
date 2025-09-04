using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Collections.Generic;
using System.Linq;

namespace QTD2.Domain.Services.Core
{
    public class Location_Service : Common.Service<Entities.Core.Location>, Interfaces.Service.Core.ILocation_Service
    {
        public Location_Service (ILocation_Repository location_Repository, ILocation_Validation location_Validation)
            :base (location_Repository, location_Validation)
        {

        }

        public async System.Threading.Tasks.Task<Entities.Core.Location> GetLocationByIdAsync(int? locationId)
        {
            var location = await FindAsync(x => x.Id == locationId);
            return location.FirstOrDefault();
        }

        public async System.Threading.Tasks.Task<List<Entities.Core.Location>> GetAllLocationAsync()
        {
            var locations = (await FindAsync(x => x.Active)).ToList(); ;
            return locations;
        }
    }
}
