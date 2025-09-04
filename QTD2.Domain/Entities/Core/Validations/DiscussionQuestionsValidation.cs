using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.DiscussionQuestionsSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class DiscussionQuestionsValidation : Validation<DiscussionQuestion>, IDiscussionQuestionValidation
    {
        public DiscussionQuestionsValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<DiscussionQuestion>(new DiscussionQuestionsILATraineeEvaluationIdRequiredSpecs(), _validationStringLocalizer["DiscussionQuestionsILATraineeEvaluationIdRequiredSpecs"]));
            AddRule(new ValidationRule<DiscussionQuestion>(new DiscussionQuestionsQuestionTextRequiredSpecs(), _validationStringLocalizer["DiscussionQuestionsQuestionTextRequiredSpecs"]));
        }
    }
}
