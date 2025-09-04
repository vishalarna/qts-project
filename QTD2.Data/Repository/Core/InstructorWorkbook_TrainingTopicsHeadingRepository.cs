using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Repository.Core
{
   public class InstructorWorkbook_TrainingTopicsHeadingRepository : Common.Repository<InstructorWorkbook_TrainingTopicsHeading>, IInstructorWorkbook_TrainingTopicsHeadingRepository
    {
        public InstructorWorkbook_TrainingTopicsHeadingRepository(QTDContext context)
            : base(context)
        {

        }
    }
}
