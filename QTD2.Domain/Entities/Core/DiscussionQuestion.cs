using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class DiscussionQuestion : Entity
    {
        public int ILATraineeEvaluationId { get; set; }

        public string QuestionText { get; set; }

        public string QuestionFileUpload { get; set; }

        public string QuestionImageUpload { get; set; }

        public string QuestionLinksUpload { get; set; }

        public string AnswerKeywords { get; set; }

        public string AnswerImageUpload { get; set; }

        public string AnswerFileUpload { get; set; }

        public virtual ILATraineeEvaluation ILATraineeEvaluation { get; set; }

        public DiscussionQuestion(int ilaTraineeEvaluationId, string quetsionText, string questionFileUpload, string questionImageUpload, string questionLinksUpload, string answerKeywords, string answerImageUpload, string answerFileUpload)
        {
            ILATraineeEvaluationId = ilaTraineeEvaluationId;
            QuestionText = quetsionText;
            QuestionFileUpload = questionFileUpload;
            QuestionImageUpload = questionImageUpload;
            QuestionLinksUpload = questionLinksUpload;
            AnswerKeywords = answerKeywords;
            AnswerImageUpload = answerImageUpload;
            AnswerFileUpload = answerFileUpload;
        }

        public DiscussionQuestion()
        {
        }
    }
}
