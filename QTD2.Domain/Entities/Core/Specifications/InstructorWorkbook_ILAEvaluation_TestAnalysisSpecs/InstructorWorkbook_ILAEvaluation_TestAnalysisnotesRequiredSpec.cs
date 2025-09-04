
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILAEvaluation_TestAnalysisSpecs
{
   public class InstructorWorkbook_ILAEvaluation_TestAnalysisnotesRequiredSpec : ISpecification<InstructorWorkbook_ILAEvaluation_TestAnalysis>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ILAEvaluation_TestAnalysis entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Notes);
        }
    }
}
