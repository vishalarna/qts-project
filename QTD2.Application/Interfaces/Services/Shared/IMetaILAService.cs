using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Meta_ILAMembers_Link;
using QTD2.Infrastructure.Model.MetaILA_Employee;
using QTD2.Infrastructure.Model.MetaILA;
using QTD2.Infrastructure.Model.Provider;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IMetaILAService
    {
        public Task<List<MetaILAVM>> GetAsync();

        public Task<MetaILA> GetAsync(int id);

        public Task<MetaILA> CreateAsync(MetaILACreateOptions options);

        public Task<MetaILA> UpdateAsync(int id, MetaILAUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);

        /* Meta_ILAMemebers_Link starts*/
        public System.Threading.Tasks.Task<List<MetaILA_ILAMemberVM>> LinkILAMemeberAsync(Meta_ILAMembers_ListOptions options);

        public Task<MetaILA_ILAMemberVM> UpdateILAMembersLinkAsync(Meta_ILAMembers_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkILAMemeberAsync(int id, int ilaMemeberID);
        public System.Threading.Tasks.Task RemoveILAMemeberAsync(int id, int linkedId);

        public Task<List<ILA>> GetLinkedILAMemebersAsync(int id);
        /* Meta_ILAMemebers_Link ends*/

        public Task<List<ILA_ProviderVM>> GetLinkedILAAsync();

        public Task<MetaILAILARequirements_VM> GetMetaILAILARequirements(int iLAId);

        public Task<List<MetaILA_EmployeeVM>> GetLinkedMetaILAEmployeesAsync(int metaILAId);
        public Task<List<MetaILA_EmployeeVM>> LinkMetaILAEMployeesAsync(MetaILA_EmployeeOptions options);
        public Task<List<MetaILA_EmployeeVM>> UnlinkLinkMetaILAEMployeesAsync(MetaILA_EmployeeOptions options);
        public Task<List<EnablingObjective>> GetEnablingObjectivesLinkedToMetaILAAsync(int metaILAId);
        public MetaILAVM MapMetaILAToMetaILAVM(MetaILA metaILA);
        public Task<List<MetaILANercCertificationDetailVM>> GetMetaILANERCCertificationDetailsAsync(int metaILAId);
        public Task<List<MetaILA_IDPVM>> GetLinkedMetaILAsByEmployeeIdForIDPAsync(int employeeId);
        public Task<List<MetaILA_MemberIDPVM>> GetLinkedMetaILAsMembersByMetaILAIdForIDPAsync(int metaILAId,int empId);

    }
}
