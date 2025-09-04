using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class InstructorWorkbook_Segments_LinkObjectivesRepository : Common.Repository<InstructorWorkbook_Segments_LinkObjectives>, IInstructorWorkbook_Segments_LinkObjectivesRepository
    {
        public InstructorWorkbook_Segments_LinkObjectivesRepository(QTDContext context)
            : base(context)
        {

        }
    }
}
