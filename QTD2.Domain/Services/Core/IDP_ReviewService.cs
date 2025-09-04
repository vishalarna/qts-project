using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class IDP_ReviewService : Common.Service<IDP_Review>, IIDP_ReviewService
    {
        public IDP_ReviewService(IIDP_ReviewRepository repository, IIDP_ReviewValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<IDP_Review> GetIdpReviewForNotificationAsync(int employeeId, int idpId)
        {
            var idpReviews = await FindWithIncludeAsync(r => r.EmployeeId == employeeId, new[] { "Employee.Person", "IDP_ReviewStatus" });
            return idpReviews.Last();
        }

        public async System.Threading.Tasks.Task<IEnumerable<IDP_Review>> GetIDPReviewListAsync(bool includeInactiveEmployees)
        {
            return await FindWithIncludeAsync(r => (!includeInactiveEmployees && r.Employee.Active) || includeInactiveEmployees, new[] { "Employee", "Employee.Person", "Employee.EmployeePositions.Position", "IDP_ReviewStatus" });
        }
    }
}
