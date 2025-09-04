using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_EnablingObjective_StepSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_EnablingObjective_StepValidation : Validation<Version_EnablingObjective_Step>, IVersion_EnablingObjective_StepValidation
    {
        public Version_EnablingObjective_StepValidation(IStringLocalizerFactory stringLocalizerFactory) : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_EnablingObjective_Step>(new VEO_S_DescriptionRequiredSpec(), _validationStringLocalizer["VEO_S_DescriptionRequiredSpec"]));
            AddRule(new ValidationRule<Version_EnablingObjective_Step>(new VEO_S_NumberRequiredSpec(), _validationStringLocalizer["VEO_S_NumberRequiredSpec"]));
            AddRule(new ValidationRule<Version_EnablingObjective_Step>(new VEO_S_EOStepIdRequiredSpec(), _validationStringLocalizer["VEO_S_EOStepIdRequiredSpec"]));
        }
    }
}
