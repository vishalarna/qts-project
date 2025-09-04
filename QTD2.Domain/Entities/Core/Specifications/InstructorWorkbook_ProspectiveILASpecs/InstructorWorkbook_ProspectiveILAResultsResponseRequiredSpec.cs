
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ProspectiveILASpecs
{
    public class InstructorWorkbook_ProspectiveILAResultsResponseRequiredSpec : ISpecification<InstructorWorkbook_ProspectiveILA>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ProspectiveILA entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.ResultsResponse);
        }
    }
}
