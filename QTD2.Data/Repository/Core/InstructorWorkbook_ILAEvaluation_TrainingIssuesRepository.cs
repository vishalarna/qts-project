using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
  public  class InstructorWorkbook_ILAEvaluation_TrainingIssuesRepository : Common.Repository<InstructorWorkbook_ILAEvaluation_TrainingIssues>, IInstructorWorkbook_ILAEvaluation_TrainingIssuesRepository
    {
        public InstructorWorkbook_ILAEvaluation_TrainingIssuesRepository(QTDContext context)
            : base(context)
        {

        }
    }
}
