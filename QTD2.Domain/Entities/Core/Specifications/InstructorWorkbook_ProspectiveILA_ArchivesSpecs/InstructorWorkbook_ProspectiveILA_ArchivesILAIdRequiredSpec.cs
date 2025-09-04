
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ProspectiveILA_ArchivesSpecs
{
    public class InstructorWorkbook_ProspectiveILA_ArchivesILAIdRequiredSpec : ISpecification<InstructorWorkbook_ProspectiveILA_Archives>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ProspectiveILA_Archives entity, params object[] args)
        {
            return entity.ILAId > 0;
        }
    }
}
