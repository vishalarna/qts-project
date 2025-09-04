using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Version_EnablingObjective_Question : Entity
    {
        public int EOQuestionId { get; set; }

        //public int Version_EnablingObjectiveId { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        //public virtual Version_EnablingObjective Version_EnablingObjective { get; set; }

        public virtual EnablingObjective_Question EnablingObjective_Question { get; set; }

        public Version_EnablingObjective_Question()
        {
        }

        public Version_EnablingObjective_Question(int eOQuestionId, int version_EnablingObjectiveId, string question, string answer)
        {
            EOQuestionId = eOQuestionId;
            //Version_EnablingObjectiveId = version_EnablingObjectiveId;
            Question = question;
            Answer = answer;
        }

        public Version_EnablingObjective_Question(EnablingObjective_Question question)
        {
            EOQuestionId = question.Id;
            //Version_EnablingObjectiveId = question.EnablingObjectiveId;
            Question = question.Question;
            Answer = question.Answer;
        }

        public Version_EnablingObjective_Question(Version_EnablingObjective eo, EnablingObjective_Question ques)
        {
            EOQuestionId = ques.Id;
            Question = ques.Question;
            Answer = ques.Answer;
        }
    }
}
