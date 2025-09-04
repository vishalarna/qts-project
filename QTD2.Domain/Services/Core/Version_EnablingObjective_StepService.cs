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
    public class Version_EnablingObjective_StepService : Common.Service<Version_EnablingObjective_Step>, IVersion_EnablingObjective_StepService
    {
        public Version_EnablingObjective_StepService(IVersion_EnablingObjective_StepRepository repository, IVersion_EnablingObjective_StepValidation validation)
            : base(repository, validation)
        {
        }
    }
}
