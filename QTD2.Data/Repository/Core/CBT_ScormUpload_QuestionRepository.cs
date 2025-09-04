using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class CBT_ScormUpload_QuestionRepository : Common.Repository<CBT_ScormUpload_Question>, ICBT_ScormUpload_QuestionRepository
    {
        public CBT_ScormUpload_QuestionRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
