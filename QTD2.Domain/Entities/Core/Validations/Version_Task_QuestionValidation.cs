using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_Task_QuestionSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_Task_QuestionValidation : Validation<Version_Task_Question>, IVersion_Task_QuestionValidation
    {
        public Version_Task_QuestionValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_Task_Question>(new VTQ_VersionTaskIdRequiredSpec(), _validationStringLocalizer["VTQ_VersionTaskIdRequired"]));
            AddRule(new ValidationRule<Version_Task_Question>(new VTQ_TaskQuestionIdRequiredSpec(), _validationStringLocalizer["VTQ_TaskQuestionIdRequired"]));
            AddRule(new ValidationRule<Version_Task_Question>(new VTQ_QuestionRequiredSpec(), _validationStringLocalizer["VTQ_QuestionRequired"]));
            AddRule(new ValidationRule<Version_Task_Question>(new VTQ_AnswerRequiredSpec(), _validationStringLocalizer["VTQ_AnswerRequired"]));
        }
    }
}
