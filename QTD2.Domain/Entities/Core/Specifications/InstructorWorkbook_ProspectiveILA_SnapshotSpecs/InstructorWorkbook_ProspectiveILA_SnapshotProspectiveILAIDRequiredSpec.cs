
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ProspectiveILA_SnapshotSpecs
{
    public class InstructorWorkbook_ProspectiveILA_SnapshotProspectiveILAIDRequiredSpec : ISpecification<InstructorWorkbook_ProspectiveILA_Snapshot>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ProspectiveILA_Snapshot entity, params object[] args)
        {
            return entity.ProspectiveILAID > 0;
        }
    }
}
