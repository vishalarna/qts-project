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
    public class EnablingObjective_StepService : Common.Service<EnablingObjective_Step>, IEnablingObjective_StepService
    {
        public EnablingObjective_StepService(IEnablingObjective_StepRepository repository, IEnablingObjective_StepValidation validation)
            : base(repository, validation)
        {
        }
    }
}
