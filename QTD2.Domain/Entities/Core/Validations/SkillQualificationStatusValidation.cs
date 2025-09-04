using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SaftyHazard_RR_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SkillQualificationStatusValidation : Validation<SkillQualificationStatus>, ISkillQualificationStatusValidation
    {
        public SkillQualificationStatusValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
        }
    }
}
