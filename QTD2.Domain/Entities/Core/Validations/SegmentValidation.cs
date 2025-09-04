using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SegmentSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SegmentValidation : Validation<Segment>, ISegmentValidation
    {
        public SegmentValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Segment>(new SegmentTitleRequiredSpec(), _validationStringLocalizer["SegmentTitleRequiredSpec"]));
            //AddRule(new ValidationRule<Segment>(new SegmentDurationRequiredSpec(), _validationStringLocalizer["SegmentDurationRequiredSpec"]));
            AddRule(new ValidationRule<Segment>(new SegmentContentRequiredSpec(), _validationStringLocalizer["SegmentContentRequiredSpec"]));
        }
    }
}
