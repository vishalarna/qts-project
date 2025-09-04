using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;


namespace QTD2.Data.Repository.Core
{
   public class InstructorWorkbook_ProspectiveILA_ArchivesRepository : Common.Repository<InstructorWorkbook_ProspectiveILA_Archives>, IInstructorWorkbook_ProspectiveILA_ArchivesRepository
    {
        public InstructorWorkbook_ProspectiveILA_ArchivesRepository(QTDContext context)
            : base(context)
        {

        }
    }
 }
