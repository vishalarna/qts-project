using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_EnablingObjective_QuestionSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_EnablingObjective_QuestionValidation : Validation<Version_EnablingObjective_Question>, IVersion_EnablingObjective_QuestionValidation
    {
        public Version_EnablingObjective_QuestionValidation(IStringLocalizerFactory stringLocalizerFactory) : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_EnablingObjective_Question>(new VEO_Q_EOQuestionIdRequiredSpec(), _validationStringLocalizer["VEO_Q_EOQuestionIdRequiredSpec"]));
            AddRule(new ValidationRule<Version_EnablingObjective_Question>(new VEO_Q_QuestionRequiredSpec(), _validationStringLocalizer["VEO_Q_QuestionRequiredSpec"]));
            AddRule(new ValidationRule<Version_EnablingObjective_Question>(new VEO_Q_AnswerRequiredSpec(), _validationStringLocalizer["VEO_Q_AnswerRequiredSpec"]));
        }
    }
}
