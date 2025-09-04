using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class SkillQualificationEmpSettingMap : Common.CommonMap<SkillQualificationEmpSetting>
    {
        public override void Configure(EntityTypeBuilder<SkillQualificationEmpSetting> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.SkillQualification).WithOne(o => o.SkillQualificationEmpSetting).HasForeignKey<SkillQualificationEmpSetting>(f => f.SkillQualificationId).IsRequired();
            builder.Ignore("MultipleSignOffDisplay");
        }
    }
}
