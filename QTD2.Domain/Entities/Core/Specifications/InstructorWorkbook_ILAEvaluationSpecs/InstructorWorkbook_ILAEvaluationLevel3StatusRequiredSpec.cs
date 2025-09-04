
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILAEvaluationSpecs
{
    public class InstructorWorkbook_ILAEvaluationLevel3StatusRequiredSpec : ISpecification<InstructorWorkbook_ILAEvaluation>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ILAEvaluation entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Level3Status);
        }
    }
}
