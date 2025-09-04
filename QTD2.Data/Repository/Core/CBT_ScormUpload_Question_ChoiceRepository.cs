using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class CBT_ScormUpload_Question_ChoiceRepository : Common.Repository<CBT_ScormUpload_Question_Choice>, ICBT_ScormUpload_Question_ChoiceRepository
    {
        public CBT_ScormUpload_Question_ChoiceRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
