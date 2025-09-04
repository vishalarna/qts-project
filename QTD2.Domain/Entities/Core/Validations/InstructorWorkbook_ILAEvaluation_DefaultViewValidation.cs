using Microsoft.Extensions.Localization;

using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILAEvaluation_DefaultViewSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class InstructorWorkbook_ILAEvaluation_DefaultViewValidation : Validation<InstructorWorkbook_ILAEvaluation_DefaultView>, IInstructorWorkbook_ILAEvaluation_DefaultViewValidation
    {
        public InstructorWorkbook_ILAEvaluation_DefaultViewValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_ILAEvaluation_DefaultView>(new InstructorWorkbook_ILAEvaluation_DefaultViewDefaultViewRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILAEvaluationStudentEvaluationResultsRequired"]));

        }
    }
}
