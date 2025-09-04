using QTD2.Infrastructure.Model.Meta_ILAMembers_Link;
using QTD2.Infrastructure.Model.MetaILA_Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.MetaILA
{
    public class MetaILAVM
    {
        public int Id { get; set; }
        public int? MetaILA_SummaryTest_FinalTestId { get; set; }
        public int? MetaILA_SummaryTest_RetakeTestId { get; set; }
        public int? StudentEvaluationId { get; set; }
        public int? MetaILAStatusId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }
        public bool Active { get; set; }
        public bool IsDeleteAllowed { get; set; }
        public bool IsReleasedToEmployees { get; set; }
        public int? MetaIlaCount { get; set; }
        public int? ProviderId { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public List<MetaILA_EmployeeVM> MetaILA_EmployeeVM = new List<MetaILA_EmployeeVM>();
        public List<MetaILA_ILAMemberVM> MetaILA_ILAMemberVM = new List<MetaILA_ILAMemberVM>();

        public MetaILAVM(){}
        public MetaILAVM(int id,int? metaILA_SummaryTest_FinalTestId,int? metaILA_SummaryTest_RetakeTestId,int? studentEvaluationId,int? metaILAStatusId,string name,string description,string reason,bool active,bool isDeleteAllowed,bool isReleasedToEmployees,DateTime? effectiveDate,List<MetaILA_EmployeeVM> metaILA_EmployeeVM,List<MetaILA_ILAMemberVM> metaILA_ILAMemberVM,int? providerId)
        {
            Id = id;
            MetaILA_SummaryTest_FinalTestId = metaILA_SummaryTest_FinalTestId;
            MetaILA_SummaryTest_RetakeTestId = metaILA_SummaryTest_RetakeTestId;
            StudentEvaluationId = studentEvaluationId;
            MetaILAStatusId = metaILAStatusId;
            Name = name ?? string.Empty;
            Description = description ?? string.Empty;
            Reason = reason ?? string.Empty;
            Active = active;
            IsDeleteAllowed = isDeleteAllowed;
            IsReleasedToEmployees = isReleasedToEmployees;
            EffectiveDate = effectiveDate;
            MetaILA_EmployeeVM = metaILA_EmployeeVM ?? new List<MetaILA_EmployeeVM>();
            MetaILA_ILAMemberVM = metaILA_ILAMemberVM ?? new List<MetaILA_ILAMemberVM>();
            ProviderId = providerId;
        }

        public MetaILAVM(int id,string name,bool active, bool isDeleteAllowed, bool isReleasedToEmployees, int? metaIlaCount) 
        {
            Id = id;
            Name = name;
            Active = active;
            IsDeleteAllowed = isDeleteAllowed;
            IsReleasedToEmployees = isReleasedToEmployees;
            MetaIlaCount = metaIlaCount;
        }
    }

}
