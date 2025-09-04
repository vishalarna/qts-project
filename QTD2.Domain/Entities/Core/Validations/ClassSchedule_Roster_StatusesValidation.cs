using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ClassSchedule_Roster_StatusesValidation : Validation<ClassSchedule_Roster_Statuses>, IClassSchedule_Roster_StatusesValidation
    {
        public ClassSchedule_Roster_StatusesValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
        }
    }
}
