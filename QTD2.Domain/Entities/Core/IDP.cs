using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class IDP : Common.Entity
    {
        public int EmployeeId { get; set; }

        public int ILAId { get; set; }

        public DateTime? IDPYear { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ILA ILA { get; set; }
        public virtual ICollection<IDPSchedule> IDPSchedules { get; set; }

        public IDP()
        {
        }

        public IDP(int employeeId, int iLAId, DateTime? idpYear)
        {
            EmployeeId = employeeId;
            ILAId = iLAId;
            IDPYear = idpYear;
        }

        public override void Create(string username)
        {
            AddDomainEvent(new Domain.Events.Core.OnIDPAdded(this));
            base.Create(username);
        }

        public void AddIdpSchedule(IDPSchedule idpSchedule)
        {
            if (IDPSchedules == null) IDPSchedules = new List<IDPSchedule>();

            if (IDPSchedules.Where(r => r.ClassScheduleId == idpSchedule.ClassScheduleId).Count() > 0) return;

            IDPSchedules.Add(idpSchedule);
        }
    }
}
