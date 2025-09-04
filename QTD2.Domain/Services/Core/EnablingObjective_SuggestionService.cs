using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class EnablingObjective_SuggestionService : Common.Service<EnablingObjective_Suggestion>, IEnablingObjective_SuggestionService
    {
        public EnablingObjective_SuggestionService(IEnablingObjective_SuggestionRepository repository, IEnablingObjective_SuggestionValidation validation)
            : base(repository, validation)
        {
        }
    }
}
