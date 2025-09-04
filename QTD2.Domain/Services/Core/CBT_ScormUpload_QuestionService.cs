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
    public class CBT_ScormUpload_QuestionService : Common.Service<CBT_ScormUpload_Question>, ICBT_ScormUpload_QuestionService
    {
        public CBT_ScormUpload_QuestionService(ICBT_ScormUpload_QuestionRepository repository, ICBT_ScormUpload_QuestionValidation validation)
            : base(repository, validation)
        {
        }
    }
}