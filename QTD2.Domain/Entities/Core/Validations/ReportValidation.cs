using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Report;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
  public  class ReportValidation : Validation<Report>, IReportValidation
    {
        public ReportValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            //AddRule(new ValidationRule<Report>(new ReportSpecs_AtLeastOneDisplayColumnRequiredSpec(), _validationStringLocalizer["ReportSpecs_AtLeastOneDisplayColumnRequired"]));
            AddRule(new ValidationRule<Report>(new ReportSpecs_ClientUserIdRequiredSpec(), _validationStringLocalizer["ReportSpecs_ClientUserIdRequired"]));
            AddRule(new ValidationRule<Report>(new ReportSpecs_NameRequiredSpec(), _validationStringLocalizer["ReportSpecs_NameRequired"]));
            AddRule(new ValidationRule<Report>(new ReportSpecs_ReportSkeletonRequiredSpec(), _validationStringLocalizer["ReportSpecs_ReportSkeletonRequired"]));
            AddRule(new ValidationRule<Report>(new ReportSpecs_TitleRequiredSpec(), _validationStringLocalizer["ReportSpecs_TitleRequired"]));

        }
    }
}
