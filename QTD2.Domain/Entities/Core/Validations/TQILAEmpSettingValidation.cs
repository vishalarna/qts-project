using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TQILAEmpSettingSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TQILAEmpSettingValidation : Validation<TQILAEmpSetting>, ITQILAEmpSettingValidation
    {
        public TQILAEmpSettingValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TQILAEmpSetting>(new TQILAEmpSettingILAIdRequiredSpec(), _validationStringLocalizer["TQILAEmpSettingILAIdRequiredSpec"]));
        }
    }
}
