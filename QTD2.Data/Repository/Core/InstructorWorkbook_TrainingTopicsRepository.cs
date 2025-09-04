using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Repository.Core
{
   public class InstructorWorkbook_TrainingTopicsRepository : Common.Repository<InstructorWorkbook_TrainingTopics>, IInstructorWorkbook_TrainingTopicsRepository
    {
        public InstructorWorkbook_TrainingTopicsRepository(QTDContext context)
            : base(context)
        {

        }
    }
}
