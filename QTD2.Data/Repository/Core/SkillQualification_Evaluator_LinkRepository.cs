using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class SkillQualification_Evaluator_LinkRepository : Common.Repository<SkillQualification_Evaluator_Link>, ISkillQualification_Evaluator_LinkRepository
    {
        public SkillQualification_Evaluator_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
