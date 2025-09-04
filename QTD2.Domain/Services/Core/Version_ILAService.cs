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
    public class Version_ILAService : Common.Service<Version_ILA>, IVersion_ILAService
    {
        public Version_ILAService(IVersion_ILARepository repository, IVersion_ILAValidation validation)
            : base(repository, validation)
        {
        }
    }
}
