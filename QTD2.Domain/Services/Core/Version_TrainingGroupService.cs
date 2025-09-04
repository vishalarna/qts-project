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
    public class Version_TrainingGroupService : Common.Service<Version_TrainingGroup>, IVersion_TrainingGroupService
    {
        public Version_TrainingGroupService(IVersion_TrainingGroupRepository repository, IVersion_TrainingGroupValidation validation)
            : base(repository, validation)
        {
        }
    }
}
