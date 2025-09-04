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
    public class Version_EnablingObjective_SuggestionsService : Common.Service<Version_EnablingObjective_Suggestions>, IVersion_EnablingObjective_SuggestionsService
    {
        public Version_EnablingObjective_SuggestionsService(IVersion_EnablingObjective_SuggestionsRepository repository, IVersion_EnablingObjective_SuggestionsValidation validation)
            : base(repository, validation)
        {
        }
    }
}
