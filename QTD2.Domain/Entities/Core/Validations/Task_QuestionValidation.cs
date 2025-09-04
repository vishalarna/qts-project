using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Task_QuestionSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Task_QuestionValidation : Validation<Task_Question>, ITask_QuestionValidation
    {
        public Task_QuestionValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Task_Question>(new Task_Question_TaskIdRequiredSpec(), _validationStringLocalizer["Task_Question_TaskIdRequired"]));
            AddRule(new ValidationRule<Task_Question>(new Task_Question_QuestionRequriedSpec(), _validationStringLocalizer["Task_Question_QuestionRequried"]));
            AddRule(new ValidationRule<Task_Question>(new Task_Question_AnswerRequiredSpec(), _validationStringLocalizer["Task_Question_AnswerRequired"]));
        }
    }
}
