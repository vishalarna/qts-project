using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Threading.Tasks;

using System.Linq;
using System.Collections.Generic;

namespace QTD2.Domain.Services.Core
{
    public class MetaILA_Employee_MemberLinkFufillmentService : Common.Service<MetaILA_Employee_MemberLinkFufillment>, IMetaILA_Employee_MemberLinkFufillmentService
    {
        public MetaILA_Employee_MemberLinkFufillmentService(IMetaILA_Employee_MemberLinkFufillmentRepository repository, IMetaILA_Employee_MemberLinkFufillmentValidation validation)
                    : base(repository, validation)
        {

        }

        public async Task<MetaILA_Employee_MemberLinkFufillment> GetForNotificationAsync(int metaILA_Employee_MemberLinkFufillmentId)
        {
            return (await FindWithIncludeAsync(r => r.Id == metaILA_Employee_MemberLinkFufillmentId, new string[] { "MetaILA_Employee.Employee.Person", "FufilledBy_ClassScheduleEmployee.ClassSchedule.ILA" })).FirstOrDefault();
        }

        public async Task<List<MetaILA_Employee_MemberLinkFufillment>> GetWithClassScheduleEmployeeByMemberAndEmpIdAsync(int memberLinkId, int empId)
        {
            return (await FindWithIncludeAsync(r => r.Meta_ILAMembers_LinkId == memberLinkId && r.MetaILA_Employee.EmployeeId == empId, new[] { "FufilledBy_ClassScheduleEmployee" } )).ToList();
        }
    }
}
