using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class CBT_ScormUpload_Question_Choice : Common.Entity
    {
        public CBT_ScormUpload_Question_Choice()
        {
        }

        public CBT_ScormUpload_Question_Choice(string choice,  bool isCorrect, CBT_ScormUpload_Question question)
        {
            Choice = choice;
            CorrectChoice = isCorrect;
            CBT_ScormUpload_Question = question;
        }

        public int CBTScormUploadQuestionId { get; set; }
        public virtual CBT_ScormUpload_Question CBT_ScormUpload_Question { get; set; }
        public string Choice { get; set; }
        public bool CorrectChoice { get; set; }
        public virtual ICollection<CBT_ScormRegistration_Response> CBT_ScormRegistration_Responses { get; set; } = new List<CBT_ScormRegistration_Response>();

        public void UpdateChoice(string choice, bool isCorrect)
        {
            this.CorrectChoice = isCorrect;
        }
    }
}
