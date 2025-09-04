
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILAImplementReviewersSpecs
{
    public class InstructorWorkbook_ILAImplementReviewersReviewerIdsRequiredSpec : ISpecification<InstructorWorkbook_ILAImplementReviewers>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ILAImplementReviewers entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.ReviewerIds);
        }
    }
}
