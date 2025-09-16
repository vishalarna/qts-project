using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class SkillReQualificationEmp_SuggestionRepository : Common.Repository<SkillReQualificationEmp_Suggestion>, ISkillReQualificationEmp_SuggestionRepository
    {
        public SkillReQualificationEmp_SuggestionRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
