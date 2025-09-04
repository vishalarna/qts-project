using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IPublicClassScheduleRequestService : IService<PublicClassScheduleRequest>
    {
        Task<PublicClassScheduleRequest> GetLastRequestByIpAsync(string ipAddress);
        Task<List<PublicClassScheduleRequest>> GetAllPublicRequestsAsync();
        Task<PublicClassScheduleRequest> GetPublicRequestById(int id);
        Task<int> GetPublicRequestStatsAsync();
        Task<PublicClassScheduleRequest> GetExistingRequestAsync(int classScheduleId, PublicClassScheduleRequest request);
        Task<PublicClassScheduleRequest> GetRequestsByClassScheduleEmployeeId(int classScheduleEmployeeId); 
    }
}
