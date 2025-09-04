using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
   public class PublicClassScheduleRequestService : Common.Service<PublicClassScheduleRequest>,
                  IPublicClassScheduleRequestService
    {
        public PublicClassScheduleRequestService(IPublicClassScheduleRequestRepository repository, IPublicClassScheduleRequestValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<PublicClassScheduleRequest> GetLastRequestByIpAsync(string ipAddress)
        {
            var requests = await FindAsync(r => r.IpAddress == ipAddress);

            return requests.OrderByDescending(r => r.CreatedDate).FirstOrDefault();
        }

        public async Task<List<PublicClassScheduleRequest>> GetAllPublicRequestsAsync()
        {
            var currentDate = DateTime.UtcNow;
            var publicRequests = await FindWithIncludeAsync(x => x.Status == PublicClassScheduleRequestStatus.Requested && x.ClassSchedule.StartDateTime >= currentDate && x.Active && !x.Deleted,new string[] { "ClassSchedule.Instructor", "ClassSchedule.Location", "ClassSchedule.ILA"});
            return publicRequests.ToList();
        }

        public async Task<int> GetPublicRequestStatsAsync()
        {
            var currentDate = DateTime.UtcNow;
            var publicRequests = (await FindWithIncludeAsync(x => x.Active && !x.Deleted && x.Status == PublicClassScheduleRequestStatus.Requested && x.ClassSchedule.StartDateTime >= currentDate, new string[] { "ClassSchedule" })).Count();
            return publicRequests;
        }
        
        public async Task<PublicClassScheduleRequest> GetPublicRequestById(int id)
        {
            var publicRequest = await GetAsync(id);
            return publicRequest;
        }

        public async Task<PublicClassScheduleRequest> GetExistingRequestAsync(int classScheduleId, PublicClassScheduleRequest request)
        {
            var result = (await FindAsync(x => x.Active && x.EmailAddress == request.EmailAddress && x.LastName == request.LastName && (request.NercCertNumber!=null ? x.NercCertNumber == request.NercCertNumber : x.NercCertNumber == null))).FirstOrDefault();
            return result;
        }

        public async Task<PublicClassScheduleRequest> GetRequestsByClassScheduleEmployeeId(int classScheduleEmployeeId)
        {
            var result = (await FindAsync(x => x.Active && x.ClassScheduleEmployeeId == classScheduleEmployeeId)).FirstOrDefault();
            return result;
        }
    }
}