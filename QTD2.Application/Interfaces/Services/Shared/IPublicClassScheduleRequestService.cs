using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ClientUser;
using QTD2.Infrastructure.Model.PublicClassScheduleRequest;
using QTD2.Infrastructure.Model.PublicILA;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IPublicClassScheduleRequestService
    {
        public Task<PublicClassScheduleRequest> ApproveRequestAsync(int id);
        public Task<PublicClassScheduleRequest> DenyRequestAsync(int id);
        public System.Threading.Tasks.Task CreatePublicClassScheduleRequestAsync(PublicClassScheduleRequest request);
        public Task<List<PublicClassScheduleRequestsVM>> GetAllActiveRequestsAsync();
        public Task<int> GetPublicRequestStatsAsync();
        public Task<PublicClassScheduleRequest> UpdatePublicClassScheduleRequestAsync(int id, ModifyPublicClassScheduleRequestModel options);
        public Task<ILACompletionRequirementVM> GetILACompletionRequirementAsync(int id);
    }
}
