
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILADesign_ResourcesSpecs
{
   public class InstructorWorkbook_ILADesign_ResourcesResourcePathRequiredSpec : ISpecification<InstructorWorkbook_ILADesign_Resources>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ILADesign_Resources entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.ResourcePath);
        }
    }
}
