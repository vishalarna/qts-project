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
    public class Version_PositionService : Common.Service<Version_Position>, IVersion_PositionService
    {
        public Version_PositionService(IVersion_PositionRepository repository, IVersion_PositionValidation validation)
            : base(repository, validation)
        {
        }
    }
}
