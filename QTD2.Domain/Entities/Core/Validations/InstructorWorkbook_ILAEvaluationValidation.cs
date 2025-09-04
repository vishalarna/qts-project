using Microsoft.Extensions.Localization;

using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILAEvaluationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class InstructorWorkbook_ILAEvaluationValidation : Validation<InstructorWorkbook_ILAEvaluation>, IInstructorWorkbook_ILAEvaluationValidation
    {
        public InstructorWorkbook_ILAEvaluationValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_ILAEvaluation>(new InstructorWorkbook_ILAEvaluationStudentEvaluationResultsRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILAEvaluationStudentEvaluationResultsRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ILAEvaluation>(new InstructorWorkbook_ILAEvaluationInstructorFeedbackRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILAEvaluationInstructorFeedbackRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ILAEvaluation>(new InstructorWorkbook_ILAEvaluationLevel1StatusRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILAEvaluationLevel1StatusRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ILAEvaluation>(new InstructorWorkbook_ILAEvaluationNotesRequiredSpecs(), _validationStringLocalizer["InstructorWorkbook_ILAEvaluationNotesRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ILAEvaluation>(new InstructorWorkbook_ILAEvaluationLevel2StatusRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILAEvaluationLevel2StatusRequired"]));
            
            AddRule(new ValidationRule<InstructorWorkbook_ILAEvaluation>(new InstructorWorkbook_ILAEvaluationOpenTextFieldrequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILAEvaluationOpenTextFieldrequired"]));
  
            AddRule(new ValidationRule<InstructorWorkbook_ILAEvaluation>(new InstructorWorkbook_ILAEvaluationLevel3StatusRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILAEvaluationLevel3StatusRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ILAEvaluation>(new InstructorWorkbook_ILAEvaluationLevel4TextFieldRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILAEvaluationLevel4TextFieldRequired"]));
           
            AddRule(new ValidationRule<InstructorWorkbook_ILAEvaluation>(new InstructorWorkbook_ILAEvaluationLevel4TextStatusRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILAEvaluationLevel4TextStatusRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ILAEvaluation>(new InstructorWorkbook_ILAEvaluationEvaluationResultRequiredspec(), _validationStringLocalizer["InstructorWorkbook_ILAEvaluationEvaluationResultRequired"]));

        }
    }
}
