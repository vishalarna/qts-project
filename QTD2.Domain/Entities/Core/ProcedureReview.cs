using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ProcedureReview : Common.Entity
    {
       
        public int ProcedureId { get; set; }
        public string ProcedureReviewTitle { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public string ProcedureReviewInstructions { get; set; }
        
        public string ProcedureReviewAcknowledgement { get; set; }

        public bool IsEmployeeShowResponses { get; set; }

        public bool IsPublished { get; set; }
        public virtual Procedure Procedure { get; set; }

        public virtual ICollection<ProcedureReview_Employee> ProcedureReview_Employee { get; set; } = new List<ProcedureReview_Employee>();

        public ProcedureReview()
        {

        }
        public ProcedureReview(int procedureId, string procedureReviewTitle, DateTime startDateTime, DateTime endDateTime, string procedureReviewInstructions, bool isEmployeeShowResponses, bool isPublished,string procedureReviewAcknowledgement)
        {
            ProcedureId = procedureId;
            ProcedureReviewTitle = procedureReviewTitle;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            ProcedureReviewInstructions = procedureReviewInstructions;
            IsEmployeeShowResponses = isEmployeeShowResponses;
            IsPublished = isPublished;
            ProcedureReviewAcknowledgement = procedureReviewAcknowledgement;
        }

        public ProcedureReview_Employee LinkEmployee(Employee employee)
        {
            ProcedureReview_Employee procedureReciew_employee_link = ProcedureReview_Employee.FirstOrDefault(x => x.EmployeeId == employee.Id && x.ProcedureReviewId == this.Id);

            if (procedureReciew_employee_link != null)
            {
                AddDomainEvent(new Domain.Events.Core.OnProcedureReview_EmployeeCreated(procedureReciew_employee_link));
                return procedureReciew_employee_link;
            }

            procedureReciew_employee_link = new ProcedureReview_Employee(this, employee);
            AddEntityToNavigationProperty<ProcedureReview_Employee>(procedureReciew_employee_link);

            AddDomainEvent(new Domain.Events.Core.OnProcedureReview_EmployeeCreated(procedureReciew_employee_link));
            return procedureReciew_employee_link;
        }
        public void UnlinkEmployee(Employee employee)
        {
            ProcedureReview_Employee procedureReciew_employee_link = ProcedureReview_Employee.FirstOrDefault(x => x.EmployeeId == employee.Id && x.ProcedureReviewId == this.Id);
            if (procedureReciew_employee_link != null)
            {
                procedureReciew_employee_link.Delete();
            }
        }

        public void Publish()
        {
            IsPublished = true;

            AddDomainEvent(new Domain.Events.Core.OnProcedureReviewPublished(this));
        }
    }
}
