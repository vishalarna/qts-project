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
    public class Version_TestStausValidation : Validation<Version_TestStaus>, IVersion_TestStausValidation
    {
        public Version_TestStausValidation(IStringLocalizerFactory stringLocalizerFactory) : base(stringLocalizerFactory)
        {
        }
    }
}
