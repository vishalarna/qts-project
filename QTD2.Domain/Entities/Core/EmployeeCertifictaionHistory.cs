using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class EmployeeCertifictaionHistory : Entity
    {
        public int? EmployeeCertificationId { get; set; }

        public DateOnly ChangeEffectiveDate { get; set; }

        public string ChangeNotes { get; set; }

        public DateOnly IssueDate { get; set; }

        public DateOnly ExpirationDate { get; set; }

        //unclear on waht this is but brining it over anyway
        [Obsolete("We don't know what this value is, you should question if you should be using this.")]
        public DateOnly DRADate { get; set; }

        public string? CertificationNumber { get; set; }

        public virtual EmployeeCertification EmployeeCertification { get; set; }

        public override void Create(string username)
        {
            base.Create(username);
            AddDomainEvent(new Domain.Events.Core.OnEmployeeCertificationHistoryCreated(this));
        }
        public EmployeeCertifictaionHistory()
        {
        }

        public EmployeeCertifictaionHistory(int employeeCertificationId, DateOnly changeEffectiveDate, string changeNotes)
        {
            EmployeeCertificationId = employeeCertificationId;
            ChangeEffectiveDate = changeEffectiveDate;
            ChangeNotes = changeNotes;
        }
    }
}
