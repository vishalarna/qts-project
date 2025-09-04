using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ILATraineeEvaluationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ILATraineeEvaluationValidation : Validation<ILATraineeEvaluation>, IILATraineeEvaluationValidation
    {
        public ILATraineeEvaluationValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ILATraineeEvaluation>(new ILATraineeEvaluationILAIdRequiredSpec(), _validationStringLocalizer["ILATraineeEvaluationILAIdRequiredSpec"]));
            AddRule(new ValidationRule<ILATraineeEvaluation>(new ILATraineeEvaluationTypeIdRequiredSpec(), _validationStringLocalizer["ILATraineeEvaluationTypeIdRequiredSpec"]));
        }
    }
}
