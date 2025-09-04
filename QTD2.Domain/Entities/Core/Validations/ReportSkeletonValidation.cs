using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications;
using QTD2.Domain.Entities.Core.Specifications.ReportSkeleton;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ReportSkeletonValidation : Validation<ReportSkeleton>, IReportSkeletonValidation
    {
        public ReportSkeletonValidation(IStringLocalizerFactory stringLocalizerFactory) : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ReportSkeleton>(new ReportSkeleton_AtLeastOneColumnRequiredSpec(), _validationStringLocalizer["ReportSkeleton_AtLeastOneColumnRequired"]));
            AddRule(new ValidationRule<ReportSkeleton>(new ReportSkeleton_DefaultTitleRequiredSpec(), _validationStringLocalizer["ReportSkeleton_DefaultTitleRequired"]));
        }
    }
}
