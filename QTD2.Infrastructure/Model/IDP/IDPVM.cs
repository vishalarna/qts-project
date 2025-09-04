using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.IDP
{
    public class IDPVM
    {
        public int Id { get; set; }

        public int EmpId { get; set; }

        public int ILAId { get; set; }
        public int? ClassScheduleId { get; set; }

        public string StudentNumber { get; set; }

        public bool HasEnrolledInAnyClass { get; set; }

        public string ILATitle { get; set; }
        public bool Active { get; set; }

        public string ILANumber { get; set; }

        public string DeliveryMethod { get; set; }

        public DateTime? PlannedDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set;}

        public string Grade { get; set; }

        public int Score { get; set; }

        public bool TestLinked { get; set; }

        public bool CBTLinked { get; set; }

        public bool SelfRegister { get; set; }
        public string? CompletionDate { get; set; }
        public bool? TaskQualificationCompleted { get; set; }
        public string? GradeUpdateReason { get; set; }

        public bool ILAActive { get; set; }

        public int? IDPYear { get; set; }

        public IDPVM(int id, string ilaTitle, string ilaNumber, string deliveryMethod,DateTime? plannedDate, bool testLinked, bool cbtLinked, bool selfRegister, bool hasEnrolledInAnyClass)
        {
            Id = id;
            ILATitle = ilaTitle;
            ILANumber = ilaNumber;
            DeliveryMethod = deliveryMethod;
            TestLinked = testLinked;
            PlannedDate = plannedDate;
            CBTLinked = cbtLinked;
            SelfRegister = selfRegister;
            HasEnrolledInAnyClass = hasEnrolledInAnyClass;
        }
    }
}
