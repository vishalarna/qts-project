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
    public class Version_Task_TrainingGroupValidation : Validation<Version_Task_TrainingGroup>, IVersion_Task_TrainingGroupValidation
    {
        public Version_Task_TrainingGroupValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
        }
    }
}
