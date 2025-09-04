using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.EnablingObjective_SubCategoryHistorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class EnablingObjective_SubCategoryHistoryValidation : Validation<EnablingObjective_SubCategoryHistory>, IEnablingObjective_SubCategoryHistoryValidation
    {
        public EnablingObjective_SubCategoryHistoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<EnablingObjective_SubCategoryHistory>(new EO_SubCatHisSubCategoryIdRequiredSpec(), _validationStringLocalizer["EO_SubCatHisSubCategoryIdRequiredSpec"]));
        }
    }
}
