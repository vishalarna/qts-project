using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILADesignReviewersSpecs
{
  public  class InstructorWorkbook_ILADesignReviewersReviewerIdsRequiredSpec :  ISpecification<InstructorWorkbook_ILADesignReviewers>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ILADesignReviewers entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.ReviewerIds);
        }
}
}
