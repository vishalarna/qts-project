using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class QuestionBankHistoryRepository : Common.Repository<QuestionBankHistory>, IQuestionBankHistoryRepository
    {
        public QuestionBankHistoryRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
