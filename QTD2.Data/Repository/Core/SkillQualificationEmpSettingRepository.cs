using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class SkillQualificationEmpSettingRepository : Common.Repository<SkillQualificationEmpSetting>, ISkillQualificationEmpSettingRepository
    {
        public SkillQualificationEmpSettingRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
