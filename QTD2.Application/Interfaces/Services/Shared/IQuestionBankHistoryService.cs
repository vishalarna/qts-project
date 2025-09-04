using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.QuestionBankHistory;
namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IQuestionBankHistoryService
    {
        public List<QuestionBankHistory> GetAsync();

        public Task<QuestionBankHistory> GetAsync(int id);

        public Task<QuestionBankHistory> CreateAsync(QuestionBankHistoryCreateOptions options);

        public Task<QuestionBankHistory> UpdateAsync(int id, QuestionBankHistoryCreateOptions options);

        public Task<QuestionBankHistory> DeleteAsync(int id);

        public Task<QuestionBankHistory> ActiveAsync(int id);

        public Task<QuestionBankHistory> DeactivateAsync(int id);
    }
}
