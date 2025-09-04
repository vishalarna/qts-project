using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.EnablingObjective_QuestionSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class EnablingObjective_QuestionValidation : Validation<EnablingObjective_Question>, IEnablingObjective_QuestionValidation
    {
        public EnablingObjective_QuestionValidation(IStringLocalizerFactory stringLocalizerFactory) : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<EnablingObjective_Question>(new EnablingObjective_Question_QuestionRequiredSpec(), _validationStringLocalizer["EnablingObjective_Question_QuestionRequiredSpec"]));
            AddRule(new ValidationRule<EnablingObjective_Question>(new EnablingObjective_Question_AnswerRequiredSpec(), _validationStringLocalizer["EnablingObjective_Question_AnswerRequiredSpec"]));
            AddRule(new ValidationRule<EnablingObjective_Question>(new EnablingObjective_Question_EOIdRequiredSpec(), _validationStringLocalizer["EnablingObjective_Question_EOIdRequiredSpec"]));
        }
    }
}
