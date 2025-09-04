
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Database.Interfaces;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using QTD2.Infrastructure.Notification.Interfaces.Validators;
using QTD2.Infrastructure.Notification.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using QTD2.Domain.Exceptions;
using QTD2.Application.Services.QTD;
using QTD2.Application.Interfaces.Services.QTD;
using QTD2.Data;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Application.Jobs.Notifications
{
    public class NotificationJob : IJob
    {
        public bool RunAtStartup => false;

        private readonly IInstanceFetcher _instanceFetcher;
        private IDbContextBuilder _dbContextBuilder;
        private readonly IJobNotificationService _jobNotificationService;
        private ILogger<NotificationJob> _logger;
        private readonly Settings.DomainSettings _domainSettings;

        public NotificationJob(
            IInstanceFetcher instanceFetcher,
            IDbContextBuilder dbContextBuilder,
            IJobNotificationService jobNotificationService,
            ILogger<NotificationJob> logger,
            IOptions<Settings.DomainSettings> domainSettingOptions)
        {
            _instanceFetcher = instanceFetcher;
            _dbContextBuilder = dbContextBuilder;
            _jobNotificationService = jobNotificationService;
            _logger = logger;
            _domainSettings = domainSettingOptions.Value;
        }

        public async System.Threading.Tasks.Task Execute(IJobExecutionContext context)
        {
            var instances = await _instanceFetcher.GetAllActiveInstancesAsync();

            foreach (var instance in instances)
            {
                try
                {
                    var qtdContext = _dbContextBuilder.BuildQtdContext(instance.DatabaseName);

                    var pendingNotifications = await qtdContext.Notifications
                        .Include("ClientSettings_Notification_Step.ClientSettings_Notification")
                        .Include("EmployeeCertification")
                        .Include("CBT")
                        .Include("Employee")
                        .Include("IDP")
                        .Include("ProcedureReview_Employee")
                        .Include("ClassScheduleEmployee")
                        .Include("ClassSchedule_Evaluation_Roster")
                        .Include("TaskQualification")
                        .Include("TaskQualification_Evaluator_Link")
                        .Include("Items")
                        .Where(r => r.Status == NotificationSendStatus.Pending && r.DueDate < System.DateTime.UtcNow)
                        .ToListAsync();

                    pendingNotifications.ForEach(r => r.Status = NotificationSendStatus.Sending);

                    qtdContext.Notifications.UpdateRange(pendingNotifications);
                    await qtdContext.SaveChangesAsync();

                    foreach (var notification in pendingNotifications)
                    {
                        if (!validate(notification))
                        {
                            notification.Reject("The notification is no longer active");
                        }
                        else
                        {
                            var notificationsInLast24Hours = await qtdContext.NotificationRecipiets
                                .Where(r => 
                                    r.Notification.ClientSettingsNotificationStepId == notification.ClientSettingsNotificationStepId
                                    && r.ToPersonId == notification.ToPersonId
                                    && r.Notification.SentDate > DateTime.Now.AddHours(-24))
                                .ToListAsync();

                            if (notificationsInLast24Hours.Count > 0)
                            {
                                continue;
                            }
                            else
                            {
                                bool success = await trySendNotificationAsync(notification, qtdContext);

                                if (success)
                                {
                                    notification.Send();
                                }
                                else
                                {
                                    notification.Error();
                                }
                            }
                        }

                        try
                        {
                            qtdContext.Notifications.Update(notification);
                            await qtdContext.SaveChangesAsync();
                        }
                        catch (Exception e)
                        {
                            _logger.LogError($"Failed to update notification {e}", e);
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"Failed to run job for {instance.DatabaseName} {e}", e);
                }
            }
        }

        private bool validate(Notification notification)
        {
            return notification.Active && notification.ClientSettings_Notification_Step.Active && notification.ClientSettings_Notification_Step.ClientSettings_Notification.Active && notification.ClientSettings_Notification_Step.ClientSettings_Notification.Enabled;
        }

        private async Task<bool> trySendNotificationAsync(Notification notification, QTDContext qtdContext)
        {
            bool success = true;

            try
            {
                if (notification is Domain.Entities.Core.CertificationExpiringNotification)
                {
                    success = await _jobNotificationService.SendCertificationExpirationNotification((notification as Domain.Entities.Core.CertificationExpiringNotification).EmployeeCertificationId, notification.ClientSettings_Notification_Step.Order, qtdContext);
                }
                else if (notification is Domain.Entities.Core.ClassScheduleNotification)
                {
                    success = await _jobNotificationService.SendClassScheduleNotification((notification as Domain.Entities.Core.ClassScheduleNotification).ClassScheduleEmployeeId, notification.ClientSettings_Notification_Step.Order, qtdContext);
                }
                else if (notification is Domain.Entities.Core.EmpCourseNotification)
                {
                    success = await _jobNotificationService.SendEmpCourseNotification((notification as Domain.Entities.Core.EmpCourseNotification).EmployeeId, (notification as Domain.Entities.Core.EmpCourseNotification).CBTId, notification.ClientSettings_Notification_Step.Order, qtdContext);
                }
                else if (notification is Domain.Entities.Core.EMPOnlineCourseNotification)
                {
                    success = await _jobNotificationService.SendEMPOnlineCourseNotification((notification as Domain.Entities.Core.EMPOnlineCourseNotification).ClassScheduleEmployeeId, (notification as Domain.Entities.Core.EMPOnlineCourseNotification).CBTId, notification.ClientSettings_Notification_Step.Order, qtdContext);
                }
                else if (notification is Domain.Entities.Core.EMPDifSurveyNotification)
                {
                    // await _notificationApplicationService.SendEmpDifSurveyNotification((notification as Domain.Entities.Core.EMPDifSurveyNotification).EmployeeId, (notification as Domain.Entities.Core.EmpCourseNotification).CBTId, notification.ClientSettings_Notification_Step.Order);
                }
                else if (notification is Domain.Entities.Core.EMPGAPSurveyNotification)
                {
                    // await _notificationApplicationService.SendEmpGapSurveyNotification((notification as Domain.Entities.Core.EMPDifSurveyNotification).EmployeeId, (notification as Domain.Entities.Core.EmpCourseNotification).CBTId, notification.ClientSettings_Notification_Step.Order);
                }
                else if (notification is Domain.Entities.Core.EMPIdpReviewNotification)
                {
                    success = await _jobNotificationService.SendEmpIDPReviewNotification((notification as Domain.Entities.Core.EMPIdpReviewNotification).EmployeeId, (notification as Domain.Entities.Core.EMPIdpReviewNotification).IDPId, notification.ClientSettings_Notification_Step.Order, qtdContext);
                }
                else if (notification is Domain.Entities.Core.EMPLoginNotification)
                {
                    success = await _jobNotificationService.SendEmpLoginNotification((notification as Domain.Entities.Core.EMPLoginNotification).EmployeeId, notification.ClientSettings_Notification_Step.Order, _domainSettings.SPA, qtdContext);
                }
                else if (notification is Domain.Entities.Core.EMPTestNotification)
                {
                    success = await _jobNotificationService.SendEmpTestNotification((notification as Domain.Entities.Core.EMPTestNotification).ClassScheduleRosterId, notification.ClientSettings_Notification_Step.Order, qtdContext);
                }
                else if (notification is Domain.Entities.Core.EMPPretestNotificiation)
                {
                    success = await _jobNotificationService.SendEmpPretestNotification((notification as Domain.Entities.Core.EMPPretestNotificiation).ClassScheduleRosterId, notification.ClientSettings_Notification_Step.Order, qtdContext);
                }
                else if (notification is Domain.Entities.Core.EMPProcedureReviewNotification)
                {
                    success = await _jobNotificationService.SendEmpProcedureReviewNotification((notification as Domain.Entities.Core.EMPProcedureReviewNotification).ProcedureReview_EmployeeId, notification.ClientSettings_Notification_Step.Order, qtdContext);
                }
                else if (notification is Domain.Entities.Core.EMPSelfRegistrationApprovalNotification)
                {
                    success = await _jobNotificationService.SendEmpSelfRegistrationApprovalNotification((notification as Domain.Entities.Core.EMPSelfRegistrationApprovalNotification).ClassScheduleEmployeeId, notification.ClientSettings_Notification_Step.Order, qtdContext);
                }
                else if (notification is Domain.Entities.Core.EMPSelfRegistrationDenialNotification)
                {
                    success = await _jobNotificationService.SendEmpSelfRegistrationDenialNotification((notification as Domain.Entities.Core.EMPSelfRegistrationDenialNotification).ClassScheduleEmployeeId, notification.ClientSettings_Notification_Step.Order, qtdContext);
                }
                else if (notification is Domain.Entities.Core.EMPStudentEvaluationNotication)
                {
                    success = await _jobNotificationService.SendEmpStudentEvaluationNotification((notification as Domain.Entities.Core.EMPStudentEvaluationNotication).ClassSchedule_Evaluation_RosterId, notification.ClientSettings_Notification_Step.Order, qtdContext);
                }
                else if (notification is Domain.Entities.Core.EMPTaskQualificationTraineeNotification)
                {
                    success = await _jobNotificationService.SendEmpTaskQualitificationTraineeNotification((notification as Domain.Entities.Core.EMPTaskQualificationTraineeNotification).TaskQualificationId, notification.ClientSettings_Notification_Step.Order, qtdContext);
                }
                else if (notification is Domain.Entities.Core.EMPTaskQualitificationEvaluatorNotification)
                {
                    success = await _jobNotificationService.SendEmpTaskQualitificationEvaluatorNotification((notification as Domain.Entities.Core.EMPTaskQualitificationEvaluatorNotification).TaskQualification_Evaluator_LinkId, notification.ClientSettings_Notification_Step.Order, qtdContext);
                }
                else if (notification is Domain.Entities.Core.MetaIlaSelfPacedReleasedNotification)
                {
                    var _notification = (notification as Domain.Entities.Core.MetaIlaSelfPacedReleasedNotification);
                    success = await _jobNotificationService.SendMetaILASelfPacedReleasedNotification(_notification.EmployeeId, _notification.NextMeta_ILAMembers_LinkId, _notification.MetaILA_Employee_MemberLinkFufillmentId, _notification.ClientSettings_Notification_Step.Order, qtdContext);
                }
                else if (notification is Domain.Entities.Core.MetaIla_Admin_RegistrationRequiredNotification)
                {
                    var _notification = (notification as Domain.Entities.Core.MetaIla_Admin_RegistrationRequiredNotification);
                    success = await _jobNotificationService.SendMetaILA_Admin_RegistrationRequiredNotification(_notification.ToPersonId, _notification.EmployeeId, _notification.NextMeta_ILAMembers_LinkId, _notification.MetaILA_Employee_MemberLinkFufillmentId, _notification.ClientSettings_Notification_Step.Order, qtdContext);
                }
                else if (notification is Domain.Entities.Core.MetaIla_Admin_SelfRegistrationRequiredNotification)
                {
                    var _notification = (notification as Domain.Entities.Core.MetaIla_Admin_SelfRegistrationRequiredNotification);
                    success = await _jobNotificationService.SendMetaILA_Admin_SelfRegistrationRequiredNotification(_notification.ToPersonId, _notification.EmployeeId, _notification.NextMeta_ILAMembers_LinkId, _notification.MetaILA_Employee_MemberLinkFufillmentId, _notification.ClientSettings_Notification_Step.Order, qtdContext);
                }
                else if (notification is Domain.Entities.Core.MetaIla_Employee_RegistrationRequiredNotification)
                {
                    var _notification = (notification as Domain.Entities.Core.MetaIla_Employee_RegistrationRequiredNotification);
                    success = await _jobNotificationService.SendMetaILA_Employee_RegistrationRequiredNotification(_notification.EmployeeId, _notification.NextMeta_ILAMembers_LinkId, _notification.MetaILA_Employee_MemberLinkFufillmentId, _notification.ClientSettings_Notification_Step.Order, qtdContext);
                }
                else if (notification is Domain.Entities.Core.MetaIla_Employee_SelfRegistrationRequiredNotification)
                {
                    var _notification = (notification as Domain.Entities.Core.MetaIla_Employee_SelfRegistrationRequiredNotification);
                    success = await _jobNotificationService.SendMetaILA_Employee_SelfRegistrationRequiredNotification(_notification.EmployeeId, _notification.NextMeta_ILAMembers_LinkId, _notification.MetaILA_Employee_MemberLinkFufillmentId, _notification.ClientSettings_Notification_Step.Order, qtdContext);
                }
                else if (notification is Domain.Entities.Core.MetaIla_CourseworkCompleteNotification)
                {
                    var _notification = (notification as Domain.Entities.Core.MetaIla_CourseworkCompleteNotification);
                    success = await _jobNotificationService.SendMetaILA_CourseworkCompletedNotification(_notification.EmployeeId, _notification.MetaILA_Employee_MemberLinkFufillmentId, _notification.ClassScheduleRosterId, _notification.ClassSchedule_Evaluation_RosterId, _notification.ClientSettings_Notification_Step.Order, qtdContext);
                }
                else if (notification is Domain.Entities.Core.AdminEMPCompletionNotification)
                {
                    success = await _jobNotificationService.SendEmpPortalCompletionNotification(notification as AdminEMPCompletionNotification,notification.ClientSettings_Notification_Step.Order, qtdContext);
                }
                else if (notification is Domain.Entities.Core.SimulatorScenarioCollaborationNotification)
                {
                    success = await _jobNotificationService.SendSimulatorScenarioCollaborationNotification(notification as SimulatorScenarioCollaborationNotification, notification.ClientSettings_Notification_Step.Order, _domainSettings.SPA, qtdContext);
                }
                else if (notification is Domain.Entities.Core.PublicClassScheduleRequestNotification)
                {
                    success = await _jobNotificationService.SendPublicClassScheduleRequestNotification(notification.Id, notification as Domain.Entities.Core.PublicClassScheduleRequestNotification, notification.ClientSettings_Notification_Step.Order, notification.ToPersonId, qtdContext);
                }
                else if (notification is Domain.Entities.Core.PublicClassScheduleRequestAcceptedNotification)
                {
                    success = await _jobNotificationService.SendPublicClassScheduleRequestAcceptedNotification((notification as Domain.Entities.Core.PublicClassScheduleRequestAcceptedNotification).PublicClassScheduleRequestAcceptedNotification_PublicClassScheduleRequestId, notification.ClientSettings_Notification_Step.Order, notification.ToPersonId, qtdContext, _domainSettings.SPA);
                }
                else
                {
                    throw new QTDServerException("Notification type unknown" + notification.GetType());
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Notification failure {e}", e);
                success = false;
            }

            return success;
        }
    }
}