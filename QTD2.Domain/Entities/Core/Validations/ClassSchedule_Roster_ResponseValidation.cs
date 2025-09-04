using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ClassSchedule_RosterSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ClassSchedule_Roster_ResponseValidation : Validation<ClassSchedule_Roster_Response>, IClassSchedule_Roster_ResponseValidation
    {
        public ClassSchedule_Roster_ResponseValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
        }
    }
}
