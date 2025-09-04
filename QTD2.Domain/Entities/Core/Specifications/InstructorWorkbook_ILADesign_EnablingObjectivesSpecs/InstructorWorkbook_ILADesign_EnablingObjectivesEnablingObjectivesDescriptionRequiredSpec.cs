
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILADesign_EnablingObjectivesSpecs
{
   public class InstructorWorkbook_ILADesign_EnablingObjectivesEnablingObjectivesDescriptionRequiredSpec : ISpecification<InstructorWorkbook_ILADesign_EnablingObjectives>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ILADesign_EnablingObjectives entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.EnablingObjectivesDescription);
        }
    }
}
