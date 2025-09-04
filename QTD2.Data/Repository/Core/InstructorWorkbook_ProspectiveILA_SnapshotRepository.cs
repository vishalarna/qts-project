using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
  public  class InstructorWorkbook_ProspectiveILA_SnapshotRepository : Common.Repository<InstructorWorkbook_ProspectiveILA_Snapshot>, IInstructorWorkbook_ProspectiveILA_SnapshotRepository
    {
        public InstructorWorkbook_ProspectiveILA_SnapshotRepository(QTDContext context)
            : base(context)
        {

        }
    }
}
