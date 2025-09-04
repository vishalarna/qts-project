using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class EnablingObjective_QuestionService : Common.Service<EnablingObjective_Question>, IEnablingObjective_QuestionService
    {
        public EnablingObjective_QuestionService(IEnablingObjective_QuestionRepository repository, IEnablingObjective_QuestionValidation validation)
            : base(repository, validation)
        {
        }
    }
}
