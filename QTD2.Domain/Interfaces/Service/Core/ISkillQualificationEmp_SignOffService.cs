using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ISkillQualificationEmp_SignOffService : Common.IService<SkillQualificationEmp_SignOff>
    {
        public Task<List<SkillQualificationEmp_SignOff>> GetSkillReQualificationEmp_SignOffByTQId(int skillQualificationId);
        public Task<List<SkillQualificationEmp_SignOff>> GetSkillReQualificationsEmp_SignOffBySQId(int skillQualificationId, int evaluatorId);
    }
}
