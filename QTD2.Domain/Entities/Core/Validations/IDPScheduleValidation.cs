using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.IDPSchedulesSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class IDPScheduleValidation : Validation<IDPSchedule>, IIDPScheduleValidation
    {
        public IDPScheduleValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            //AddRule(new ValidationRule<DutyArea>(new DutyAreaDescriptionRequiredSpec(), _validationStringLocalizer["DutyAreaDescRequired"]));
            AddRule(new ValidationRule<IDPSchedule>(new IDPSchedulesIDPRequiredSpec(), _validationStringLocalizer["IDPRequired"]));
            AddRule(new ValidationRule<IDPSchedule>(new IDPSchedulesSCRequiredSpec(), _validationStringLocalizer["ScheduleClassRequired"]));
        }
    }
}
