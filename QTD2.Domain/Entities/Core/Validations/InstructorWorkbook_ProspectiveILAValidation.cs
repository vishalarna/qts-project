using Microsoft.Extensions.Localization;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ProspectiveILASpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
   public class InstructorWorkbook_ProspectiveILAValidation : Validation<InstructorWorkbook_ProspectiveILA>, IInstructorWorkbook_ProspectiveILAValidation
    {
        public InstructorWorkbook_ProspectiveILAValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA>(new InstructorWorkbook_ProspectiveILAProviderIdRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILAProviderIdRequired"]));
           
            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA>(new InstructorWorkbook_ProspectiveILAILANameRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILAILANameRequired"]));
            
            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA>(new InstructorWorkbook_ProspectiveILAILANumberRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILAILANumberRequired"]));
          
            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA>(new InstructorWorkbook_ProspectiveILAInstructorIdRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILAInstructorIdRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA>(new InstructorWorkbook_ProspectiveILAReviewerNotesRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILAReviewerNotesRequired"]));
          
            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA>(new InstructorWorkbook_ProspectiveILAResultRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILAResultRequired"]));
           
            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA>(new InstructorWorkbook_ProspectiveILAProblemStatementResponseRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILAProblemStatementResponseRequired"]));
           
            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA>(new InstructorWorkbook_ProspectiveILAGoalsResponseRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILAGoalsResponseRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA>(new InstructorWorkbook_ProspectiveILAPerformanceObjectivesResponseRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILAPerformanceObjectivesResponseRequired"]));
            
            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA>(new InstructorWorkbook_ProspectiveILAResultsResponseRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILAResultsResponseRequired"]));
            
            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA>(new InstructorWorkbook_ProspectiveILAPerformanceMetricResponseRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILAPerformanceMetricResponseRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA>(new InstructorWorkbook_ProspectiveILAKnowledgeResponseRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILAKnowledgeResponseRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA>(new InstructorWorkbook_ProspectiveILALearningMetricResponseRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILALearningMetricResponseRequired"]));
         
            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA>(new InstructorWorkbook_ProspectiveILAPerceptionResponseRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILAPerceptionResponseRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA>(new InstructorWorkbook_ProspectiveILAMotivationResponseRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILAMotivationResponseRequired"]));
           
            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA>(new InstructorWorkbook_ProspectiveILANtrCommentsRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILANtrCommentsRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA>(new InstructorWorkbook_ProspectiveILACreatorCommentsRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILACreatorCommentsRequired"]));
          
            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA>(new InstructorWorkbook_ProspectiveILAAcceptNotesRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILAAcceptNotesRequired"]));
        }
    }
}
