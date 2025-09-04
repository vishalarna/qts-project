using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class Version_Task_QuestionService : Common.Service<Version_Task_Question>, IVersion_Task_QuestionService
    {
        public Version_Task_QuestionService(IVersion_Task_QuestionRepository repository, IVersion_Task_QuestionValidation validation)
            : base(repository, validation)
        {
        }
    }
}
