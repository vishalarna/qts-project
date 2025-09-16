using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class SkillQualificationEmp_SignOffService : Common.Service<Entities.Core.SkillQualificationEmp_SignOff>, Interfaces.Service.Core.ISkillQualificationEmp_SignOffService
    {
        public SkillQualificationEmp_SignOffService(ISkillQualificationEmp_SignOffRepository skillQualificationEmp_SignOffRepository, ISkillQualificationEmp_SignOffValidation skillQualificationEmp_SignOffValidation )
            : base(skillQualificationEmp_SignOffRepository, skillQualificationEmp_SignOffValidation)
        {
        }

        public async Task<List<SkillQualificationEmp_SignOff>> GetSkillReQualificationEmp_SignOffByTQId(int skillQualificationId)
        {
            var skillQualificationEmp = (await FindWithIncludeAsync(x => x.SkillQualificationId == skillQualificationId && x.IsCompleted == true, new string[] { "Evaluator.Person" })).ToList();
            return skillQualificationEmp;
        }

        public async Task<List<SkillQualificationEmp_SignOff>> GetSkillReQualificationsEmp_SignOffBySQId(int skillQualificationId, int evaluatorId)
        {
            return(await FindAsync(x => x.SkillQualificationId == skillQualificationId && x.EvaluatorId == evaluatorId)).ToList();
        }
    }
}
