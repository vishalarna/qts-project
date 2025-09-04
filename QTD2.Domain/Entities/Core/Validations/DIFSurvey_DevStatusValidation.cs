using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.DifSurveySpecs.DIFSurvery_DevStatusSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class DIFSurvery_DevStatusValidation : Validation<DIFSurvey_DevStatus>, IDIFSurvey_DevStatusValidation
    {
        public DIFSurvery_DevStatusValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<DIFSurvey_DevStatus>(new DIFSurvey_DevStatusRequiredSpec(), _validationStringLocalizer["DIFSurvery_DevStatusRequiredSpec"]));
           
        }
    }
}
