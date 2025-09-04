using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.EmployeeTest
{
    public class EmpTestCreateOptions
    {
        public int TestId { get; set; }
        public int QuestionId { get; set; }
        public int TestTypeId { get; set; }

        public int RosterId { get; set; }

        public int EmployeeId { get; set; }
        public int TestItemTypeId { get; set; }
        public int ClassScheduleId { get; set; }
        public List<string> UserAnswer { get; set; }

        public string MatchValue { get; set; }

        public int? CorrectIndex { get; set; }

        public string ShortAnswerDescription { get; set; }

        public List<FillintheBlankAnswers> BlankIndexWithAnwer { get; set; }

        public List<MatchColumnsAnswers> MatchValueWithCorrectValue { get; set; }

        public EmpTestCreateOptions()
        {
            UserAnswer = new List<string>();
            BlankIndexWithAnwer = new List<FillintheBlankAnswers>();
            MatchValueWithCorrectValue = new List<MatchColumnsAnswers>();
        }

    }
    public class MatchColumnsAnswers
    {
        public string MatchValue { get; set; }

        public string UserValue { get; set; }
        public int CorrectIndex { get; set; }
    }
    public class FillintheBlankAnswers
    {
        public int CorrectIndex { get; set; }

        public string UserValue { get; set; }
    }
}
