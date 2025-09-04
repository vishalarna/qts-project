using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.DiscussionQuestion;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IDiscussionQuestionService
    {
        public Task<DiscussionQuestion> CreateAsync(DiscussionQuestionCreateOptions options);

        public Task<List<DiscussionQuestion>> GetDiscussionQuestionsAsync(int id);

        public System.Threading.Tasks.Task RemoveAllQuestions(int id);

        //public List<DiscussionQuestion> GetAsync(int id);
    }
}
