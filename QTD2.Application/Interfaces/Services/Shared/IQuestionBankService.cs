using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.QuestionBank;


namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IQuestionBankService
    {
        public Task<List<QuestionBankCustomModel>> GetAsync();

        public Task<QuestionBank> GetAsync(int id);

        public Task<QuestionBank> CreateAync(QuestionBankCreateOptions options);

        public Task<QuestionBank> UpdateAsync(int id, QuestionBankCreateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task DeactivateAsync(int id);

        public System.Threading.Tasks.Task ActivateAsync(int id);
        public Task<List<int>> CreateEvalutaionAndLinkQUestionAync(QuestionBankCreateOptions options);
    }
}
