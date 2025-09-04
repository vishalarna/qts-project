using Microsoft.Extensions.Localization;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ProspectiveILA_SnapshotSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class InstructorWorkbook_ProspectiveILA_SnapshotValidation : Validation<InstructorWorkbook_ProspectiveILA_Snapshot>, IInstructorWorkbook_ProspectiveILA_SnapshotValidation
    {
        public InstructorWorkbook_ProspectiveILA_SnapshotValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA_Snapshot>(new InstructorWorkbook_ProspectiveILA_SnapshotProspectiveILAIDRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILA_SnapshotProspectiveILAIDRequired"]));
           
            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA_Snapshot>(new InstructorWorkbook_ProspectiveILA_SnapshotVersionRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILA_SnapshotVersionRequired"]));
         
            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA_Snapshot>(new InstructorWorkbook_ProspectiveILA_SnapshotChangedByRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILA_SnapshotChangedByRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA_Snapshot>(new InstructorWorkbook_ProspectiveILA_SnapshotProblemStatementResponseRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILA_SnapshotProblemStatementResponseRequired"]));
          
            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA_Snapshot>(new InstructorWorkbook_ProspectiveILA_SnapshotGoalsResponseRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILA_SnapshotGoalsResponseRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA_Snapshot>(new InstructorWorkbook_ProspectiveILA_SnapshotResultsResponseRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILA_SnapshotResultsResponseRequiredSpec"]));
   
            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA_Snapshot>(new InstructorWorkbook_ProspectiveILA_SnapshotPerformanceObjectivesResponseRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILA_SnapshotPerformanceObjectivesResponseRequired"]));
            
            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA_Snapshot>(new InstructorWorkbook_ProspectiveILA_SnapshotPerformanceMetricResponseRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILA_SnapshotPerformanceMetricResponseRequired"]));
         
            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA_Snapshot>(new InstructorWorkbook_ProspectiveILA_SnapshotKnowledgeResponseRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILA_SnapshotKnowledgeResponseRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA_Snapshot>(new InstructorWorkbook_ProspectiveILA_SnapshotLearningMetricResponseRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILA_SnapshotLearningMetricResponseRequired"]));
           
            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA_Snapshot>(new InstructorWorkbook_ProspectiveILA_SnapshotPerceptionResponseRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILA_SnapshotPerceptionResponseRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA_Snapshot>(new InstructorWorkbook_ProspectiveILA_SnapshotMotivationResponseRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILA_SnapshotMotivationResponseRequired"]));

        }
    }
}
