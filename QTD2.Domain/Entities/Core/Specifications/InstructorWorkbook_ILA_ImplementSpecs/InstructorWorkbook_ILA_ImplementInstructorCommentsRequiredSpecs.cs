
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILA_ImplementSpecs
{
    class InstructorWorkbook_ILA_ImplementInstructorCommentsRequiredSpecs : ISpecification<InstructorWorkbook_ILA_Implement>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ILA_Implement entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.InstructorComments);
        }
    }
}
