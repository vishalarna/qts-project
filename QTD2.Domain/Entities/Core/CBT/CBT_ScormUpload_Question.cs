using QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.ActionItem_QuestionAndAnswer_OperationSpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class CBT_ScormUpload_Question : Common.Entity
    {
        public CBT_ScormUpload_Question()
        {
        }
        public CBT_ScormUpload_Question(CBT_ScormUpload cBT_ScormUpload, CBT_ScormUpload_Question_Type type, string questionId, string description, List<string> choices, List<string> correctChoices)
        {
            CBT_ScormUpload = cBT_ScormUpload;
            CBT_ScormUpload_Question_Type = type;
            Description = description ?? "";
            CBT_ScormUpload_Question_Choices = new List<CBT_ScormUpload_Question_Choice>();
            QuestionId = questionId;

            foreach (var choice in choices)
            {
                this.AddChoice(choice, correctChoices.Contains(choice));
            }
        }

        public int CbtScormUploadId { get; set; }

        public virtual CBT_ScormUpload CBT_ScormUpload { get; set; }

        public string QuestionId { get; set; }

        public string Description { get; set; }

        public CBT_ScormUpload_Question_Type CBT_ScormUpload_Question_Type { get; set; }

        public virtual ICollection<CBT_ScormUpload_Question_Choice> CBT_ScormUpload_Question_Choices { get; set; } = new List<CBT_ScormUpload_Question_Choice>();

        public CBT_ScormUpload_Question_Choice AddChoice(string choice, bool isCorrect)
        {
            if (CBT_ScormUpload_Question_Choices == null) CBT_ScormUpload_Question_Choices = new List<CBT_ScormUpload_Question_Choice>();

            var entity = CBT_ScormUpload_Question_Choices?.Where(r => r.Choice == choice?.Trim()).FirstOrDefault();

            if (entity == null)
            {
                var _choice = new CBT_ScormUpload_Question_Choice(choice?.Trim(), isCorrect, this);
                CBT_ScormUpload_Question_Choices.Add(_choice);
                return _choice;
            }
            else
            {
                entity.UpdateChoice(choice, isCorrect);
            }               

            return entity;
        }

        public CBT_ScormUpload_Question_Choice GetChoice(string learnerResponse)
        {
            if (CBT_ScormUpload_Question_Choices == null) return null;

            return CBT_ScormUpload_Question_Choices.Where(r => r.Choice == learnerResponse.Trim()).FirstOrDefault();
        }
    }
}
