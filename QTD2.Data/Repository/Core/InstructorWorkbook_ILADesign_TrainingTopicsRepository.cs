using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class InstructorWorkbook_ILADesign_TrainingTopicsRepository : Common.Repository<InstructorWorkbook_ILADesign_TrainingTopics>, IInstructorWorkbook_ILADesign_TrainingTopicsRepository
    {
        public InstructorWorkbook_ILADesign_TrainingTopicsRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
