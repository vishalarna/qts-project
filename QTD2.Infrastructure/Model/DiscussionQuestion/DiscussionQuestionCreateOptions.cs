using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.DiscussionQuestion
{
    public class DiscussionQuestionCreateOptions
    {
        public int ILATraineeEvaluationId { get; set; }

        public string QuestionText { get; set; }

        public string QuestionFileUpload { get; set; }

        public string QuestionImageUpload { get; set; }

        public string QuestionLinksUpload { get; set; }

        public string AnswerKeywords { get; set; }

        public string AnswerImageUpload { get; set; }

        public string AnswerFileUpload { get; set; }
    }
}
