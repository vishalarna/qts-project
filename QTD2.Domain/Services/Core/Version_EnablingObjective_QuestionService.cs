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
    public class Version_EnablingObjective_QuestionService : Common.Service<Version_EnablingObjective_Question>, IVersion_EnablingObjective_QuestionService
    {
        public Version_EnablingObjective_QuestionService(IVersion_EnablingObjective_QuestionRepository repository, IVersion_EnablingObjective_QuestionValidation validation)
            : base(repository, validation)
        {
        }
    }
}
