using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class SkillReQualificationEmp_QuestionAnswerRepository : Common.Repository<SkillReQualificationEmp_QuestionAnswer>, ISkillReQualificationEmp_QuestionAnswerRepository
    {
        public SkillReQualificationEmp_QuestionAnswerRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
