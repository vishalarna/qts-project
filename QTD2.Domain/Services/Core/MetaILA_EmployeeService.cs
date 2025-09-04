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
    public class MetaILA_EmployeeService : Common.Service<MetaILA_Employee>, IMetaILA_EmployeeService
    {
        public MetaILA_EmployeeService(IMetaILA_EmployeeRepository repository, IMetaILA_EmployeeValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<MetaILA_Employee>> GetByIlaIdAndEmployeeId(int iLAID, int employeeId)
        {
            var metaIla_Employees = await FindWithIncludeAsync(r => r.MetaILA.Meta_ILAMembers_Links.Where(r => r.ILAID == iLAID).Any() && r.EmployeeId == employeeId,
                new string[] { "MetaILA_Employee_MemberLinkFufillments", "MetaILA.Meta_ILAMembers_Links.ILA.ILA_SelfRegistrationOption", "MetaILA.Meta_ILAMembers_Links.MetaILAConfigurationPublishOption" } );
            return metaIla_Employees.ToList();
        }

        public async Task<List<MetaILA_Employee>> GetByEmployeeId(int employeeId)
        {
            return (await FindWithIncludeAsync(e => e.EmployeeId == employeeId, new[] { "MetaILA" })).ToList();
        }
    }
}
