
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILADesign_TargetAudienceSpecs
{
   public class InstructorWorkbook_ILADesign_TargetAudienceInstructorEmailRequiredSpec : ISpecification<InstructorWorkbook_ILADesign_TargetAudience>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ILADesign_TargetAudience entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.InstructorEmail);
        }
    }
}
