using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class NotificationMap : Common.CommonMap<Notification>
    {
        public override void Configure(EntityTypeBuilder<Notification> builder)
        {
            base.Configure(builder);

            builder.Property(o => o.ClientSettingsNotificationStepId).IsRequired();
            builder.Property(o => o.DueDate).IsRequired();
            builder.Property(o => o.SentDate);
            builder.Property(o => o.Status);
            builder.Property(o => o.ToPersonId);
            builder.Property(o => o.RejectionReason);
            builder.HasOne(o => o.ClientSettings_Notification_Step).WithMany().HasForeignKey(k => k.ClientSettingsNotificationStepId);
            builder.HasOne(O => O.ToPerson).WithMany().HasForeignKey(k => k.ToPersonId).OnDelete(DeleteBehavior.NoAction);
            builder.Property(o => o.OthersEmailAddresses);

            builder.HasDiscriminator<string>("NotificationType")
               .HasValue<Notification>("Notification")
               .HasValue<ClassScheduleNotification>("ClassScheduleNotification")
               .HasValue<CertificationExpiringNotification>("CertificationExpiringNotification")
               .HasValue<EmpCourseNotification>("EmpCourseNotification")
               .HasValue<EMPOnlineCourseNotification>("EMPOnlineCourseNotification")
               .HasValue<EMPDifSurveyNotification>("EMPDifSurveyNotification")
               .HasValue<EMPGAPSurveyNotification>("EMPGAPSurveyNotification")
               .HasValue<EMPIdpReviewNotification>("EMPIdpReviewNotification")
               .HasValue<EMPLoginNotification>("EMPLoginNotification")
               .HasValue<EMPPretestNotificiation>("EMPPretestNotificiation")
               .HasValue<EMPLoginNotification>("EMPLoginNotification")
               .HasValue<EMPProcedureReviewNotification>("EMPProcedureReviewNotification")
               .HasValue<EMPSelfRegistrationApprovalNotification>("EMPSelfRegistrationApprovalNotification")
               .HasValue<EMPSelfRegistrationDenialNotification>("EMPSelfRegistrationDenialNotification")
               .HasValue<EMPStudentEvaluationNotication>("EMPStudentEvaluationNotication")
               .HasValue<EMPTaskQualificationTraineeNotification>("EMPTaskQualificationTraineeNotification")
               .HasValue<EMPTaskQualitificationEvaluatorNotification>("EMPTaskQualitificationEvaluatorNotification")
               .HasValue<EMPTestNotification>("EMPTestNotification")
                .HasValue<MetaIla_Admin_RegistrationRequiredNotification>("MetaIla_Admin_RegistrationRequiredNotification")
                .HasValue<MetaIla_Admin_SelfRegistrationRequiredNotification>("MetaIla_Admin_SelfRegistrationRequiredNotification")
                .HasValue<MetaIla_Employee_RegistrationRequiredNotification>("MetaIla_Employee_RegistrationRequiredNotification")
                .HasValue<MetaIla_Employee_SelfRegistrationRequiredNotification>("MetaIla_Employee_SelfRegistrationRequiredNotification")
                .HasValue<MetaIlaSelfPacedReleasedNotification>("MetaIlaSelfPacedReleasedNotification")
                .HasValue<MetaIla_CourseworkCompleteNotification>("MetaIlaCompleteNotification")
               .HasValue<AdminEMPCompletionNotification>("AdminEMPCompletionNotification")
               .HasValue<SimulatorScenarioCollaborationNotification>("SimulatorScenarioCollaborationNotification")
               .HasValue<PublicClassScheduleRequestNotification>("PublicClassScheduleRequestNotification")
               .HasValue<PublicClassScheduleRequestAcceptedNotification>("PublicClassScheduleRequestAcceptedNotification")
               .HasValue<EMPSkillQualificationTraineeNotification>("EMPSkillQualificationTraineeNotification")
               .HasValue<EMPSkillQualitificationEvaluatorNotification>("EMPSkillQualitificationEvaluatorNotification");

        }
    }
}
