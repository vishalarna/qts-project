using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class CBT_ScormRegistration_ResponseService : Common.Service<CBT_ScormRegistration_Response>, ICBT_ScormRegistration_ResponseService
    {
        public CBT_ScormRegistration_ResponseService(ICBT_ScormRegistration_ResponseRepository repository, ICBT_ScormRegistration_ResponseValidation validation)
            : base(repository, validation)
        {
        }
    }
}