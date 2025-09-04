
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILADevelopReviewersSpecs
{
    public class InstructorWorkbook_ILADevelopReviewersReviewerIdsRequiredSpec : ISpecification<InstructorWorkbook_ILADevelopReviewers>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ILADevelopReviewers entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.ReviewerIds);
        }
    }
}
