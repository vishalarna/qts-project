using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_TrainingProgramSpecs;
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
    public class Version_TrainingProgramValidation : Validation<Version_TrainingProgram>, IVersion_TrainingProgram
    {
        public Version_TrainingProgramValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_TrainingProgram>(new Version_TrainingProgram_VersionNumberRequiredSpec(), _validationStringLocalizer["VersionNumverRequired"]));
            AddRule(new ValidationRule<Version_TrainingProgram>(new Version_TrainingProgram_TrainingProgramIdRequiredSpec(), _validationStringLocalizer["VersionTrainingProgramIdRequired"]));
        }
    }
}