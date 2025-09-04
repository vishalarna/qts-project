using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IMetaILA_Employee_MemberLinkFufillmentService : Common.IService<MetaILA_Employee_MemberLinkFufillment>
    {
        System.Threading.Tasks.Task<MetaILA_Employee_MemberLinkFufillment> GetForNotificationAsync(int metaILA_Employee_MemberLinkFufillmentId);
        System.Threading.Tasks.Task<List<MetaILA_Employee_MemberLinkFufillment>> GetWithClassScheduleEmployeeByMemberAndEmpIdAsync(int memberLinkId,int empId);
    }
}
