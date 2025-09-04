
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ProspectiveILASpecs
{
    class InstructorWorkbook_ProspectiveILAAcceptNotesRequiredSpec : ISpecification<InstructorWorkbook_ProspectiveILA>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ProspectiveILA entity, params object[] args)
        {
            return true;
        }
    }
}
