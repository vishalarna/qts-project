using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IIDP_ReviewService : Common.IService<IDP_Review>
    {
        public System.Threading.Tasks.Task<IEnumerable<IDP_Review>> GetIDPReviewListAsync(bool includeInactiveEmployees);
        System.Threading.Tasks.Task<IDP_Review> GetIdpReviewForNotificationAsync(int employeeId, int idpId);
    }
}
