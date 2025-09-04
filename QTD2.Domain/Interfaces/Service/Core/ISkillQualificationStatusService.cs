using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ISkillQualificationStatusService : Common.IService<SkillQualificationStatus>
    {
        public  System.Threading.Tasks.Task<SkillQualificationStatus> GetSkillQualificationStatusByIdAsync(int? id);
    }
}
