using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SegmentObjective_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SegmentObjective_LinkValidation : Validation<SegmentObjective_Link>, ISegmentObjective_LinkValidation
    {
        public SegmentObjective_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SegmentObjective_Link>(new SegmentObjective_Link_SegIdRequiredSpec(), _validationStringLocalizer["SegmentObjective_Link_SegIdRequired"]));
        }
    }
}
