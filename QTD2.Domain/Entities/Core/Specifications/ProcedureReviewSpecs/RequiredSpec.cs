using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Authentication.Specifications.ProcedureReviewSpecs
{
    public class RequiredSpec : ISpecification<ProcedureReview>
    {
        public bool IsSatisfiedBy(ProcedureReview entity, params object[] args)
        {
            return true;
        }
    }
}
