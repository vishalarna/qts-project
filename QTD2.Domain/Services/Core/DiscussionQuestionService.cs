using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class DiscussionQuestionService : Common.Service<DiscussionQuestion>, IDiscussionQuestionService
    {
        public DiscussionQuestionService(IDiscussionQuestionRepository repository, IDiscussionQuestionValidation validation)
            : base(repository, validation)
        {
        }
    }
}