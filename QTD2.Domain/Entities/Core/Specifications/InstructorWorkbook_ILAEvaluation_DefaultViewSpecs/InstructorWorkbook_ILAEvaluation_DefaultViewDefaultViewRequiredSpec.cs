
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILAEvaluation_DefaultViewSpecs
{
   public class InstructorWorkbook_ILAEvaluation_DefaultViewDefaultViewRequiredSpec : ISpecification<InstructorWorkbook_ILAEvaluation_DefaultView>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ILAEvaluation_DefaultView entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.DefaultView);
        }
    }
}
