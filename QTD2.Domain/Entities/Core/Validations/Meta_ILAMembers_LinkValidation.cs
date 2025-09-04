using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Meta_ILAMembers_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Meta_ILAMembers_LinkValidation : Validation<Meta_ILAMembers_Link>, IMeta_ILAMembers_LinkValidation
    {
        public Meta_ILAMembers_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Meta_ILAMembers_Link>(new Meta_ILAMemebers_LinkILAIDRequiredSpec(), _validationStringLocalizer["Meta_ILAMemebers_LinkILAIDRequiredSpec"]));
            AddRule(new ValidationRule<Meta_ILAMembers_Link>(new Meta_ILAMemebers_LinkMetaILAIDRequiredSpec(), _validationStringLocalizer["Meta_ILAMemebers_LinkMetaILAIDRequiredSpec"]));
            AddRule(new ValidationRule<Meta_ILAMembers_Link>(new Meta_ILA_Memebers_LinkConfigPublishOptionIDRequiredSpec(), _validationStringLocalizer["Meta_ILA_Memebers_LinkConfigPublishOptionIDRequiredSpec"]));
            AddRule(new ValidationRule<Meta_ILAMembers_Link>(new Meta_ILAMemebers_LinkSequenceNumberRequiredSpec(), _validationStringLocalizer["Meta_ILAMemebers_LinkSequenceNumberRequiredSpec"]));
        }
    }
}
