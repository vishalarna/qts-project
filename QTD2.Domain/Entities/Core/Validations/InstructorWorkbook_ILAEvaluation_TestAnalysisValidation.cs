using Microsoft.Extensions.Localization;

using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILAEvaluation_TestAnalysisSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
   public class InstructorWorkbook_ILAEvaluation_TestAnalysisValidation : Validation<InstructorWorkbook_ILAEvaluation_TestAnalysis>, IInstructorWorkbook_ILAEvaluation_TestAnalysisValidation
    {
        public InstructorWorkbook_ILAEvaluation_TestAnalysisValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_ILAEvaluation_TestAnalysis>(new InstructorWorkbook_ILAEvaluation_TestAnalysisnotesRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILAEvaluation_TestAnalysisnotesRequired"]));

        }
    }
}
