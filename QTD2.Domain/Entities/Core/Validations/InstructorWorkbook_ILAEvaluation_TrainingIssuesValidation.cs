using Microsoft.Extensions.Localization;

using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILAEvaluation_TrainingIssuesSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
   public class InstructorWorkbook_ILAEvaluation_TrainingIssuesValidation : Validation<InstructorWorkbook_ILAEvaluation_TrainingIssues>, IInstructorWorkbook_ILAEvaluation_TrainingIssuesValidation
    {
        public InstructorWorkbook_ILAEvaluation_TrainingIssuesValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_ILAEvaluation_TrainingIssues>(new InstructorWorkbook_ILAEvaluation_TrainingIssuesIssueTitleRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILAEvaluation_TrainingIssuesIssueTitleRequired"]));
            
            AddRule(new ValidationRule<InstructorWorkbook_ILAEvaluation_TrainingIssues>(new InstructorWorkbook_ILAEvaluation_TrainingIssuesIssueDescriptionRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILAEvaluation_TrainingIssuesIssueDescriptionRequired"]));
          
            AddRule(new ValidationRule<InstructorWorkbook_ILAEvaluation_TrainingIssues>(new InstructorWorkbook_ILAEvaluation_TrainingIssuesFeedbackTypeRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILAEvaluation_TrainingIssuesFeedbackTypeRequired"]));
            
            AddRule(new ValidationRule<InstructorWorkbook_ILAEvaluation_TrainingIssues>(new InstructorWorkbook_ILAEvaluation_TrainingIssuesSeverityRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILAEvaluation_TrainingIssuesSeverityRequired"]));

        }
    }
}
