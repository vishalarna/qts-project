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
    public class Version_EnablingObjective_Position_LinkValidation : Validation<Version_EnablingObjective_Position_Link>, IVersion_EnablingObjective_Position_LinkValidation
    {
        public Version_EnablingObjective_Position_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
        }
    }
}
