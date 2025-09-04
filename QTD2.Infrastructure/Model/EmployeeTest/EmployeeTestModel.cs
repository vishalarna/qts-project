using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.EmployeeTest
{
    public class EmployeeTestModel
    {
        public int TestId { get; set; }

        public int TestTypeId { get; set; }
        public string TestType { get; set; }

        public int EmpId { get; set; }

        public string Grade { get; set; }

        public DateTime? DueDate { get; set; }

        public int? Score { get; set; }

        public string Status { get; set; }

        public string ILA { get; set; }

        public string Instructpr { get; set; }

        public string Location { get; set; }

        public DateTime ClassDate { get; set; }
        public int ClassScheduleId { get; set; }

        public string ILANumber { get; set; }

        public string Provider { get; set; }

        public int TestHours { get; set; }

        public int TestMinutes { get; set; }

        public string ProviderName { get; set; }


        public DateTime? CompletedDate { get; set; }

        public string Instructions { get; set; }

        public int RosterId { get; set; }

        public bool TestSubmittedAnswers { get; set; }

        public bool TestCorrectIncorrectAnswers { get; set; }

        public bool PretestSubmittedAnswers { get; set; }

        public bool PretestCorrectIncorrectAnswers { get; set; }

        public bool TestRetakeSubmittedAnswers { get; set; }

        public bool TestRetakeCorrectIncorrectAnswers { get; set; }

        public int? ILAId { get; set; }

    }
}
