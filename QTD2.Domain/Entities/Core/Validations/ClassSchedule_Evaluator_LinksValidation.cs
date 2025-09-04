using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.ClassSchedule_Evaluator_LinksSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
   public class ClassSchedule_Evaluator_LinksValidation : Validation<ClassSchedule_Evaluator_Link>, IClassSchedule_Evaluator_LinksValidation
    {
        public ClassSchedule_Evaluator_LinksValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ClassSchedule_Evaluator_Link>(new ClassSchedule_Evaluator_LinksClassScheduleIdRequiredSpec(), _validationStringLocalizer["ClassSchedule_Evaluator_LinksClassScheduleIdRequiredSpec"]));
            AddRule(new ValidationRule<ClassSchedule_Evaluator_Link>(new ClassSchedule_Evaluator_LinksEvaluatorIdRequiredSpec(), _validationStringLocalizer["ClassSchedule_Evaluator_LinksEvaluatorIdRequiredSpec"]));
        }
    }
}