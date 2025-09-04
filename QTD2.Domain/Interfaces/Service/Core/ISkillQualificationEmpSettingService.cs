using QTD2.Domain.Entities.Core;
using System.Linq;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ISkillQualificationEmpSettingService : Common.IService<SkillQualificationEmpSetting>
    {
        public  System.Threading.Tasks.Task<SkillQualificationEmpSetting> GetSQSettingBySkillQualificationIdAsync(int skillQualificationId);
       
    }
}
