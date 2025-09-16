using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ISkillReQualificationEmp_SuggestionService : Common.IService<SkillReQualificationEmp_Suggestion>
    {
        public System.Threading.Tasks.Task<List<SkillReQualificationEmp_Suggestion>> GetBySkillQualificationId(int skillQualificationId,int  employeeId,int evaluatorId);
    }
}
