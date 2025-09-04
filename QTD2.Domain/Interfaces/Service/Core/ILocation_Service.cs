using System.Collections.Generic;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface  ILocation_Service : Common.IService<Entities.Core.Location>
    {
        public System.Threading.Tasks.Task<Entities.Core.Location> GetLocationByIdAsync(int? locationId);
        public System.Threading.Tasks.Task<List<Entities.Core.Location>> GetAllLocationAsync();
    }
}
