using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ReportSkeletonSpecs.ReportSkeleton_CategorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ReportSkeletonCategoryValidation : Validation<ReportSkeleton_Category>, IReportSkeletonCategoryValidation
    {
        public ReportSkeletonCategoryValidation(IStringLocalizerFactory stringLocalizerFactory) : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ReportSkeleton_Category>(new ReportSkeleton_Category_NameRequiredSpec(), _validationStringLocalizer["ReportSkeleton_Category_NameRequired"]));
        }
    }
}
