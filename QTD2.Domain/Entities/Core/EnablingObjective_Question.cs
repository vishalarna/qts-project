using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class EnablingObjective_Question : Entity
    {
        public int EnablingObjectiveId { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public int QuestionNumber { get; set; }

        public virtual EnablingObjective EnablingObjective { get; set; }

        public virtual ICollection<Version_EnablingObjective_Question> Version_EnablingObjective_Questions { get; set; } = new List<Version_EnablingObjective_Question>();
        public virtual ICollection<SkillReQualificationEmp_QuestionAnswer> SkillReQualificationEmp_QuestionAnswers { get; set; } = new List<SkillReQualificationEmp_QuestionAnswer>();

        public EnablingObjective_Question()
        {
        }

        public EnablingObjective_Question(int enablingObjectiveId, string question, string answer, int questionNumber)
        {
            EnablingObjectiveId = enablingObjectiveId;
            Question = question;
            Answer = answer;
            QuestionNumber = questionNumber;
        }

        public Version_EnablingObjective_Question CreateSnapshot()
        {
            return new Version_EnablingObjective_Question(this);
        }
    }
}
