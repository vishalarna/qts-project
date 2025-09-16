using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

using System.Collections.Generic;
using System.Linq;

namespace QTD2.Domain.Services.Core
{
    public class QTDUserService : Common.Service<QTDUser>, IQTDUserService
    {
        public QTDUserService(IQTDUserRepository repository, IQTDUserValidation validation)
            : base(repository, validation)
        {
        }

        public async System.Threading.Tasks.Task<List<QTDUser>> GetAllActive()
        {
            var users = await FindWithIncludeAsync(r => r.Active, new string[] { "Person" });
            return users.ToList();
        }

        public async System.Threading.Tasks.Task<QTDUser> GetQTDUserByUsername(string username)
        {
            return (await FindAsync(r => r.Person.Username == username && r.Active)).FirstOrDefault();
        }

        public async System.Threading.Tasks.Task<List<QTDUser>> GetAllActiveWithEmployeeData()
        {
            var users = await FindWithIncludeAsync(r => r.Active, new string[] { "Person.Employee.EmployeePositions", "Person.Employee.EmployeeOrganizations" });
            return users.ToList();
        }
    }
}
