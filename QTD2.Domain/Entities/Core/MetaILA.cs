using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;
using QTD2.Domain.Events.Core;
using QTD2.Domain.Exceptions;

namespace QTD2.Domain.Entities.Core
{
    public class MetaILA : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int MetaILAStatusId { get; set; }

        public string Reason { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public int? StudentEvaluationId { get; set; }

        public StudentEvaluation StudentEvaluation { get; set; }

        public int? MetaILA_SummaryTest_FinalTestId { get; set; }
        public int? MetaILA_SummaryTest_RetakeTestId { get; set; }
        public int? ProviderId { get; set; }

        public bool IsReleasedToEmployees
        {
            get
            {
                return Meta_ILAMembers_Links != null && Meta_ILAMembers_Links.Any(links => links.ILA != null && links.ILA.ClassSchedules != null && links.ILA.ClassSchedules.Any());
            }
        }

        public bool IsDeleteAllowed { get; set; }
        public virtual Provider Provider { get; set; }
        public virtual MetaILA_SummaryTest MetaILA_SummaryTest_FinalTest { get; set; }
        public virtual MetaILA_SummaryTest MetaILA_SummaryTest_RetakeTest { get; set; }

        public virtual ICollection<MetaILA_Employee> MetaILA_Employees { get; set; } = new List<MetaILA_Employee>();

        public virtual MetaILA_Status MetaILAStatus { get; set; }

        public virtual ICollection<Meta_ILAMembers_Link> Meta_ILAMembers_Links { get; set; } = new List<Meta_ILAMembers_Link>();

        public virtual ICollection<ClassSchedule_Evaluation_Roster> ClassSchedule_Evaluation_Rosters { get; set; } = new List<ClassSchedule_Evaluation_Roster>();

        public virtual ICollection<Version_MetaILA> Version_MetaILAs { get; set; } = new List<Version_MetaILA>();

        public MetaILA(string name, string description, int status, DateTime effectiveDate, int metaILAAssessmentID, string reason)
        {
            Name = name;
            Description = description;
            MetaILAStatusId = status;
            EffectiveDate = effectiveDate;
            Reason = reason;
        }

        public MetaILA(string name, string description, int status, DateTime? effectiveDate, int? metaILA_SummaryFinalTestId, int? metaILA_SummaryRetakeTestId, int? studentEvaluaionId,string reason,int? providerId)
        {
            Name = name;
            Description = description;
            MetaILAStatusId = status;
            EffectiveDate = effectiveDate;
            Reason = reason;
            MetaILA_SummaryTest_FinalTestId = metaILA_SummaryFinalTestId;
            MetaILA_SummaryTest_RetakeTestId = metaILA_SummaryRetakeTestId;
            StudentEvaluationId = studentEvaluaionId;
            ProviderId = providerId;
        }

        public MetaILA()
        {
        }

        /*link and unlink functions for META_ILAMemebers_Link starts*/
        public Meta_ILAMembers_Link LinkILAMemebers(ILA ila,MetaILAConfigurationPublishOption metaILAConfigurationPublishOption, int sequenceNumber, DateTime? startDateUtc)
        {
            Meta_ILAMembers_Link meta_ILAMembers_Link = Meta_ILAMembers_Links.FirstOrDefault(x => x.ILAID == ila.Id && x.MetaILAID == this.Id);
            if (meta_ILAMembers_Link != null)
            {
                return meta_ILAMembers_Link;
            }

            meta_ILAMembers_Link = new Meta_ILAMembers_Link(ila.Id, this.Id, metaILAConfigurationPublishOption != null ? metaILAConfigurationPublishOption.Id : null, sequenceNumber, startDateUtc);
            AddEntityToNavigationProperty<Meta_ILAMembers_Link>(meta_ILAMembers_Link);
            return meta_ILAMembers_Link;
        }

        public void UnlinkILAMemeber(ILA ila)
        {
            Meta_ILAMembers_Link meta_ILAMembers_Link = Meta_ILAMembers_Links.FirstOrDefault(x => x.ILAID == ila.Id && x.MetaILAID == this.Id );
            meta_ILAMembers_Link.Delete();
        }

        /*link and unlink functions for META_ILAMemebers_Link end*/

        public MetaILA_Employee AddMetaILA_Employee(int employeeID, int metaILAId)
        {
            MetaILA_Employee metaILA_Employee = MetaILA_Employees.FirstOrDefault(x => x.EmployeeId == employeeID && x.MetaILAId == metaILAId);
            if (metaILA_Employee == null)
            {
                metaILA_Employee = new MetaILA_Employee(employeeID,metaILAId);
                AddEntityToNavigationProperty<MetaILA_Employee>(metaILA_Employee);

            }
            return metaILA_Employee;
        }

        public void RemoveMetaILA_Employee(int employeeID, int metaILAId)
        {
             MetaILA_Employee metaILA_Employee = MetaILA_Employees.FirstOrDefault(x => x.EmployeeId == employeeID && x.MetaILAId == metaILAId);
            if (metaILA_Employee != null)
            {
                RemoveEntityFromNavigationProperty<MetaILA_Employee>(metaILA_Employee);
            }
        }

        public void EnrollEmployee(int employeeId, bool useCurrentDate)
        {
            var employeeLink = MetaILA_Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employeeLink == null)
            {
                throw new QTDServerException("Employee not found in MetaILA.",false,HttpStatusCode.NotFound);
            }
            employeeLink.Enrolled = true;
            AddDomainEvent(new MetaIla_Employee_Enrolled(employeeLink, useCurrentDate));
        }

    }
}
