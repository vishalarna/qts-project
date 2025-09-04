
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILADesign_DelieveryMethodsSpecs
{
   public class InstructorWorkbook_ILADesign_DelieveryMethodsInstructorEmailRequiredSpecs : ISpecification<InstructorWorkbook_ILADesign_DelieveryMethods>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ILADesign_DelieveryMethods entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.InstructorEmail);
        }
    }
}
