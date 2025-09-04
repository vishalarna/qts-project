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
    public class ILA_UploadService : Common.Service<ILA_Upload>, IILA_UploadService
    {
        public ILA_UploadService(IILA_UploadRepository repository, IILA_UploadValidation validation)
            : base(repository, validation)
        {
        }
    }
}
