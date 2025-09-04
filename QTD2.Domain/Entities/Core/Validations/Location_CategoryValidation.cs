using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Location_CategorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Location_CategoryValidation : Validation<Location_Category>, ILocation_CategoryValidation
    {
        public Location_CategoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Location_Category>(new Location_CategoryTitleRequiredSpec(), _validationStringLocalizer["Location_CategoryTitleRequired"]));
           // AddRule(new ValidationRule<Location_Category>(new Location_CategoryDescRequiredSpec(), _validationStringLocalizer["Location_CategoryDescRequired"]));
        }
    }
}
