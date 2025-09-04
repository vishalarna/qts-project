using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class Version_Task_SuggestionService : Common.Service<Version_Task_Suggestion>, IVersion_Task_SuggestionService
    {
        public Version_Task_SuggestionService(IVersion_Task_SuggestionRepository repository, IVersion_Task_SuggestionValidation validation)
            : base(repository, validation)
        {
        }
    }
}
