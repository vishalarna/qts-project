using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_TrainingProgram_ILASpecs;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_TrainingProgram_ILA_LinkValidation : Validation<Version_TrainingProgram_ILA_Link>, IVersion_TrainingProgram_ILA_LinkValidation
    {
        public Version_TrainingProgram_ILA_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_TrainingProgram_ILA_Link>(new Version_TrainingProgram_ILA_Link_VILAIdRequiredSpec(), _validationStringLocalizer["VersionILAIdRequired"]));
            AddRule(new ValidationRule<Version_TrainingProgram_ILA_Link>(new Version_TrainingProgram_ILA_Link_VTrainingProgramIdRequiredSpec(), _validationStringLocalizer["VersionILA_TrainingProgramIdRequired"]));
        }
    }
}
