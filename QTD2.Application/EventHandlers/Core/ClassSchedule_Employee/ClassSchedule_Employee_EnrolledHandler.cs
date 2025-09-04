using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Events.Core;
using QTD2.Domain.Interfaces.Service.Core;

namespace QTD2.Application.EventHandlers.Core
{
    public class ClassSchedule_Employee_EnrolledHandler : INotificationHandler<OnClassSchedule_Employee_Enrolled>
    {
        private readonly Domain.Interfaces.Service.Core.IIDPService _iDPService;
        private readonly Domain.Interfaces.Service.Core.IIDPScheduleService _iDPScheduleService;
        private readonly Domain.Interfaces.Service.Core.ITQILAEmpSettingService _tqIlaEmpSettingService;
        private readonly Domain.Interfaces.Service.Core.IILA_TaskObjective_LinkService _ilaTaskObjectiveLinkService;

        private readonly INotificationService _notificationService;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;
        private readonly Domain.Interfaces.Service.Core.IILA_TaskObjective_LinkService _iLA_TaskObjective_LinkService;
        private readonly QTD2.Domain.Interfaces.Service.Core.ITaskQualificationService _taskQualificationService;
        private readonly QTD2.Domain.Interfaces.Service.Core.ITQEmpSettingService _tQEmpSettingService;
        private readonly QTD2.Domain.Interfaces.Service.Core.IILAService _iLAService;
        private readonly QTD2.Domain.Interfaces.Service.Core.ICBTService _cbtService;
        private readonly Domain.Interfaces.Service.Core.IILA_Evaluator_LinkService _iLA_Evaluator_LinkService;
        private readonly Domain.Interfaces.Service.Core.ICBT_ScormRegistrationService _cBT_ScormRegistrationService;
        private readonly Domain.Interfaces.Service.Core.IScormUploadService _scormUploadService;
        private readonly Domain.Interfaces.Service.Core.IClassScheduleService _classScheduleService;
        private readonly QTD2.Application.Interfaces.Services.Shared.IClassSchedule_TQEMPSettingsService _classSchedule_TQEMPSettingsService;
        private readonly QTD2.Application.Interfaces.Services.Shared.IClassSchedule_Evaluator_LinksService _classSchedule_Evaluator_LinksService;
        private readonly QTD2.Application.Interfaces.Services.Shared.IClassSchedule_TestReleaseEMPSettingsService _classSchedule_TestReleaseEMPSettingsService;

        public ClassSchedule_Employee_EnrolledHandler(
            IIDPService iDPService,
            IIDPScheduleService iDPScheduleService,
            IILA_TaskObjective_LinkService iLA_TaskObjective_LinkService,
            QTD2.Domain.Interfaces.Service.Core.ITaskQualificationService taskQualificationService,
            QTD2.Domain.Interfaces.Service.Core.ITQEmpSettingService tQEmpSettingService,
            Domain.Interfaces.Service.Core.ITQILAEmpSettingService tqIlaEmpSettingService,
            Domain.Interfaces.Service.Core.IILAService iLAService,
            Domain.Interfaces.Service.Core.IILA_Evaluator_LinkService iLA_Evaluator_LinkService,
            QTD2.Domain.Interfaces.Service.Core.ICBTService cbtService,
            IClientSettings_NotificationService clientSettings_NotificationService,
            INotificationService notificationService,
            Domain.Interfaces.Service.Core.ICBT_ScormRegistrationService cBT_ScormRegistrationService,
             Domain.Interfaces.Service.Core.IScormUploadService scormUploadService,
             Domain.Interfaces.Service.Core.IClassScheduleService classScheduleService,
             QTD2.Application.Interfaces.Services.Shared.IClassSchedule_TQEMPSettingsService classSchedule_TQEMPSettingsService,
             QTD2.Application.Interfaces.Services.Shared.IClassSchedule_Evaluator_LinksService classSchedule_Evaluator_LinksService,
             QTD2.Application.Interfaces.Services.Shared.IClassSchedule_TestReleaseEMPSettingsService classSchedule_TestReleaseEMPSettingsService
            )
        {
            _iDPService = iDPService;
            _iDPScheduleService = iDPScheduleService;
            _iLA_TaskObjective_LinkService = iLA_TaskObjective_LinkService;
            _taskQualificationService = taskQualificationService;
            _tQEmpSettingService = tQEmpSettingService;
            _tqIlaEmpSettingService = tqIlaEmpSettingService;
            _iLAService = iLAService;
            _iLA_Evaluator_LinkService = iLA_Evaluator_LinkService;
            _cbtService = cbtService;
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _notificationService = notificationService;
            _cBT_ScormRegistrationService = cBT_ScormRegistrationService;
            _scormUploadService = scormUploadService;
            _classScheduleService = classScheduleService;
            _classSchedule_TQEMPSettingsService = classSchedule_TQEMPSettingsService;
            _classSchedule_Evaluator_LinksService = classSchedule_Evaluator_LinksService;
            _classSchedule_TestReleaseEMPSettingsService = classSchedule_TestReleaseEMPSettingsService;
        }

        public async System.Threading.Tasks.Task Handle(OnClassSchedule_Employee_Enrolled payload, CancellationToken cancellationToken)
        {
            await addIdpSchedulesAsync(payload);
            await addTQILAEmpSettingAsync(payload);
            await addCbtNotifications(payload);
            await AddCbtScormRegistrationAsync(payload);
            await AddTestEmpSettingAsync(payload);
        }

        protected async System.Threading.Tasks.Task addIdpSchedulesAsync(OnClassSchedule_Employee_Enrolled payload)
        {
            var startDateYear = new System.DateTime(payload.ClassSchedule_Employee.ClassSchedule.StartDateTime.Year, 1, 1);
            var endDateYear = new System.DateTime(payload.ClassSchedule_Employee.ClassSchedule.EndDateTime.Year, 1, 1);

            var iDPs = await _iDPService.FindQueryWithIncludeAsync(x => (x.IDPYear == endDateYear || x.IDPYear == startDateYear) && x.ILAId == payload.ClassSchedule_Employee.ClassSchedule.ILAID && x.EmployeeId == payload.ClassSchedule_Employee.EmployeeId, new string[] { "ILA.ClassSchedules" }).ToListAsync();
            if (iDPs.Count == 0)
            {
                IDP idp = new IDP(payload.ClassSchedule_Employee.Employee.Id, payload.ClassSchedule_Employee.ClassSchedule.ILAID.Value, startDateYear);
                await _iDPService.AddAsync(idp);

                iDPs.Add(idp);
            }
            foreach (var idp in iDPs)
            {
                var idpSchedule = (await _iDPScheduleService.FindAsync(r => r.Active && !r.Deleted && r.IDPId == idp.Id && r.ClassScheduleId == payload.ClassSchedule_Employee.ClassScheduleId)).FirstOrDefault();

                if (idpSchedule == null)
                {
                    idpSchedule = new Domain.Entities.Core.IDPSchedule(
                           idp.Id,
                           payload.ClassSchedule_Employee.ClassScheduleId,
                           payload.ClassSchedule_Employee.ClassSchedule.StartDateTime,
                           payload.ClassSchedule_Employee.ClassSchedule.EndDateTime,
                           payload.ClassSchedule_Employee.PlannedDate);

                    await _iDPScheduleService.AddAsync(idpSchedule);
                }
                else
                {
                    idpSchedule.plannedDate = payload.ClassSchedule_Employee.PlannedDate;

                    await _iDPScheduleService.UpdateAsync(idpSchedule);
                }
            }
        }

        protected async System.Threading.Tasks.Task addTQILAEmpSettingAsync(OnClassSchedule_Employee_Enrolled payload)
        {
            var ila = (await _iLAService.GetWithIncludeAsync(payload.ClassSchedule_Employee.ClassSchedule.ILAID.Value, new string[] { "TQILAEmpSettings", "ILA_TaskObjective_Links" }));
            var classSchedule = (await _classScheduleService.GetWithIncludeAsync(payload.ClassSchedule_Employee.ClassScheduleId, new string[] { "ClassSchedule_TQEMPSettings", "ClassSchedule_Evaluator_Links.Evaluator" }));

            if (ila == null || classSchedule == null) return;

            var tqILAEmpSetting = ila.TQILAEmpSettings.Where(r => r.Active).FirstOrDefault();
            var tqClassEmpSetting = await _classSchedule_TQEMPSettingsService.CreateAsync(classSchedule.Id);

            List<TaskQualification> taskQualifications = new List<TaskQualification>();

            foreach (var taskObj in ila.ILA_TaskObjective_Links.Where(x => x.UseForTQ).OrderBy(x => x.SequenceNumber))
            {
                var taskQual = new TaskQualification(taskObj.TaskId, payload.ClassSchedule_Employee.EmployeeId, false, false, payload.ClassSchedule_Employee.ClassScheduleId, 3);

                if (tqILAEmpSetting != null && tqClassEmpSetting != null && tqClassEmpSetting.Active && !tqClassEmpSetting.TQRequired)
                {
                    if (tqILAEmpSetting.ReleaseOneAtTime)
                    {
                        taskQual.Sequence = taskObj.SequenceNumber;
                    }
                    var classEvaluators = await _classSchedule_Evaluator_LinksService.LinkEvaluatorsFromILA(classSchedule.Id);

                    foreach (var evaluator in classEvaluators)
                    {
                        taskQual.LinkEvaluator(evaluator);
                    }

                    var tqEmpSetting = new TQEmpSetting(taskQual.Id, (tqILAEmpSetting.OneSignOffRequired ? null : tqILAEmpSetting.MultipleSignOffRequired ?? 1), tqILAEmpSetting.OneSignOffRequired, tqILAEmpSetting.ReleaseAtOnce, tqILAEmpSetting.ReleaseOneAtTime, tqClassEmpSetting.ShowTaskSuggestions,tqClassEmpSetting.ShowTaskQuestions);
                    if (tqClassEmpSetting.ReleaseOnClassStart)
                    {
                        tqEmpSetting.ReleaseDate = payload.ClassSchedule_Employee.ClassSchedule.StartDateTime;
                    }
                    else if (tqClassEmpSetting.ReleaseOnClassEnd)
                    {
                        tqEmpSetting.ReleaseDate = payload.ClassSchedule_Employee.ClassSchedule.EndDateTime;
                    }
                    else if (tqClassEmpSetting.PriorToSpecificTime)
                    {
                        tqEmpSetting.ReleaseDate = payload.ClassSchedule_Employee.ClassSchedule.EndDateTime.Subtract(TimeSpan.FromMinutes(Convert.ToDouble(tqILAEmpSetting.SpecificTime)));
                    }
                    else if (!tqClassEmpSetting.PriorToSpecificTime)
                    {
                        tqEmpSetting.ReleaseDate = payload.ClassSchedule_Employee.ClassSchedule.EndDateTime.Add(TimeSpan.FromMinutes(Convert.ToDouble(tqILAEmpSetting.SpecificTime)));
                    }

                    taskQual.TQEmpSetting = tqEmpSetting;
                    taskQual.DueDate = tqILAEmpSetting.GetDueDate(payload.ClassSchedule_Employee.ClassSchedule.EndDateTime);

                    if ((!tqILAEmpSetting.ReleaseOneAtTime || taskQual.Sequence == 1))
                    {
                        taskQual.Release();
                    }
                }
                taskQualifications.Add(taskQual);
            }
            await _taskQualificationService.AddRangeAsync(taskQualifications);
        }

        protected async System.Threading.Tasks.Task addCbtNotifications(OnClassSchedule_Employee_Enrolled payload)
        {
            var clientSettings_Notification = await _clientSettings_NotificationService.GetClientSettingNotificationByName("EMP Online Course");
            var cbt = await _cbtService.GetActiveByIlaAsync(payload.ClassSchedule_Employee.ClassSchedule.ILAID.Value);

            if (cbt == null) return;

            var classSchedule = payload.ClassSchedule_Employee.ClassSchedule;

            if (!classSchedule.ILA.CBTRequiredForCourse) return;

            if (!clientSettings_Notification.Enabled) return;

            if (cbt.Availablity == Domain.Entities.Core.CBTAvailablity.AfterPretestComplete)
                return;

            DateTime sendDate = DateTime.Now.ToUniversalTime();

            if (cbt.Availablity == Domain.Entities.Core.CBTAvailablity.OnClassEndDateTime)
                sendDate = classSchedule.EndDateTime;

            if (cbt.Availablity == Domain.Entities.Core.CBTAvailablity.OnClassStartDateTime)
                sendDate = classSchedule.StartDateTime;

            Domain.Entities.Core.EMPOnlineCourseNotification empOnlineCourseNotification = new Domain.Entities.Core.EMPOnlineCourseNotification(sendDate, payload.ClassSchedule_Employee.Id, cbt.Id, payload.ClassSchedule_Employee.Employee.PersonId, clientSettings_Notification.Steps.First().Id);
            await _notificationService.AddAsync(empOnlineCourseNotification);
        }

        protected async System.Threading.Tasks.Task AddCbtScormRegistrationAsync(OnClassSchedule_Employee_Enrolled payload)
        {
            var classSchedule = payload.ClassSchedule_Employee.ClassSchedule;

            if (!classSchedule.ILAID.HasValue) return;

            var activeCbt = await _cbtService.GetActiveByIlaAsync(classSchedule.ILAID.Value);
            if (activeCbt == null) return;

            var activeScormUploads = await _scormUploadService.FindAsync(x => x.CBT.ILAId == classSchedule.ILAID.Value && x.ScormStatus == "Uploaded" && x.Active);
            if (!activeScormUploads.Any()) return;

            var latestScormUpload = activeScormUploads.OrderByDescending(x => x.ConnectedDate).FirstOrDefault();
            if (latestScormUpload == null) return;

            var cbtScormRegistration = new CBT_ScormRegistration(latestScormUpload.Id, payload.ClassSchedule_Employee.Id);
            await _cBT_ScormRegistrationService.AddAsync(cbtScormRegistration);
        }

        protected async System.Threading.Tasks.Task AddTestEmpSettingAsync(OnClassSchedule_Employee_Enrolled payload)
        {
            var ila = (await _iLAService.GetWithIncludeAsync(payload.ClassSchedule_Employee.ClassSchedule.ILAID.Value, new string[] { "TestReleaseEMPSettings" }));
            var classSchedule = (await _classScheduleService.GetAsync(payload.ClassSchedule_Employee.ClassScheduleId));

            if (ila == null || classSchedule == null) return;
            var ilaTestReleaseEmpSetting = ila.TestReleaseEMPSettings;
            if (ilaTestReleaseEmpSetting == null || !ilaTestReleaseEmpSetting.Active) return;

            await _classSchedule_TestReleaseEMPSettingsService.CreateAsync(classSchedule.Id);

        }
    }
}
