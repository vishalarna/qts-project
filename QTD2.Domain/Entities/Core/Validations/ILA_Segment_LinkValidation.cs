using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ILA_Segment_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ILA_Segment_LinkValidation : Validation<ILA_Segment_Link>, IILA_Segment_LinkValidation
    {
        public ILA_Segment_LinkValidation(IStringLocalizerFactory localizer)
            : base(localizer)
        {
            AddRule(new ValidationRule<ILA_Segment_Link>(new ILA_Segment_LinkILAIdRequiredSpec(), _validationStringLocalizer["ILA_Segment_LinkILAIdRequired"]));
            AddRule(new ValidationRule<ILA_Segment_Link>(new ILA_Segment_LinkSegmentIdRequiredSpec(), _validationStringLocalizer["ILA_Segment_LinkSegmentIdRequired"]));
        }
    }
}
