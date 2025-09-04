using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
namespace QTD2.Data.Repository.Core
{
    public class QuestionBankRepository : Common.Repository<QuestionBank>, IQuestionBankRepository
    {
        public QuestionBankRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
