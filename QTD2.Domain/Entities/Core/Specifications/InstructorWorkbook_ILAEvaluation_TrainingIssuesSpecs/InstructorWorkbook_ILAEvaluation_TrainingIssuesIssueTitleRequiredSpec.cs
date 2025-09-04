
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILAEvaluation_TrainingIssuesSpecs
{
   public class InstructorWorkbook_ILAEvaluation_TrainingIssuesIssueTitleRequiredSpec : ISpecification<InstructorWorkbook_ILAEvaluation_TrainingIssues>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ILAEvaluation_TrainingIssues entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.IssueTitle);
        }
    }
}
