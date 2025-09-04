using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ScormUpload;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
   public class ScormUploadValidation : Validation<CBT_ScormUpload>, IScormUploadValidation
    {
        public ScormUploadValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
           // AddRule(new ValidationRule<ScormUpload>(new ScormUpload_ILARequired(), _validationStringLocalizer["ScormUpload_ILARequired"]));
        }
    }
}
