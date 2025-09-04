using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.DifSurvey;
using QTD2.Domain.Entities.Core.Specifications.DifSurveySpecs.DifSurveySpec;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public  class DIFSurveyValidation : Validation<DIFSurvey>, IDIFSurveyValidation
    {
        public DIFSurveyValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<DIFSurvey>(new DIFSurveyTitleRequiredSpec(), _validationStringLocalizer["DIFSurveyTitleRequiredSpec"]));
            AddRule(new ValidationRule<DIFSurvey>(new DIFSurveyPositionIdRequiredSpec(), _validationStringLocalizer["DIFSurveyPositionIdRequiredSpec"]));
            AddRule(new ValidationRule<DIFSurvey>(new DIFSurveyStartDateRequiredSpec(), _validationStringLocalizer["DIFSurveyStartDateRequiredSpec"]));
            AddRule(new ValidationRule<DIFSurvey>(new DIFSurveyDueDateRequiredSpec(), _validationStringLocalizer["DIFSurveyDueDateRequiredSpec"]));
        }
    }
}
