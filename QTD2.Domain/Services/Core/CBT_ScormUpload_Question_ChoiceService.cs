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
    public class CBT_ScormUpload_Question_ChoiceService : Common.Service<CBT_ScormUpload_Question_Choice>, ICBT_ScormUpload_Question_ChoiceService
    {
        public CBT_ScormUpload_Question_ChoiceService(ICBT_ScormUpload_Question_ChoiceRepository repository, ICBT_ScormUpload_Question_ChoiceValidation validation)
            : base(repository, validation)
        {
        }
    }
}