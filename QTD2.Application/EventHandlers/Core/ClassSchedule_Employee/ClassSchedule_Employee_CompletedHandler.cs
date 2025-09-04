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
using Microsoft.Extensions.Logging;

namespace QTD2.Application.EventHandlers.Core
{
    public class ClassSchedule_Employee_CompletedHandler : INotificationHandler<OnClassSchedule_Employee_Completed>
    {
        private readonly ILogger<ClassSchedule_Employee_CompletedHandler> _logger;

        IMetaILA_EmployeeService _metaIlaEmployeeService;
        IClassScheduleService _classScheduleService;
        IClassSchedule_Evaluation_RosterService _classScheduleEvaluationRosterService;
        INotificationService _notificationService;
        IClientSettings_NotificationService _clientSettingsNotificationService;
        IQTDUserService _qtdUserService;
        IEmployeeService _employeeService;
        IClassSchedule_RosterService _classScheduleRosterService;
        IIDPScheduleService _iDPScheduleService;

        Interfaces.Services.Shared.IClassScheduleService _applicationClassScheduleService;

        public ClassSchedule_Employee_CompletedHandler(
            ILogger<ClassSchedule_Employee_CompletedHandler> logger,
            IMetaILA_EmployeeService metaIlaEmployeeService,
            IClassScheduleService classScheduleService,
            INotificationService notificationService,
            IClientSettings_NotificationService clientSettingsNotificationService,
            IQTDUserService qtdUserService,
            IEmployeeService employeeService,
            IClassSchedule_RosterService classScheduleRosterService,
            IClassSchedule_Evaluation_RosterService classScheduleEvaluationRosterService,
            Interfaces.Services.Shared.IClassScheduleService applicationClassScheduleService,
            IIDPScheduleService iDPScheduleService
            )
        {
            _logger = logger;
            _metaIlaEmployeeService = metaIlaEmployeeService;
            _classScheduleService = classScheduleService;
            _notificationService = notificationService;
            _clientSettingsNotificationService = clientSettingsNotificationService;
            _qtdUserService = qtdUserService;
            _employeeService = employeeService;
            _classScheduleRosterService = classScheduleRosterService;
            _classScheduleEvaluationRosterService = classScheduleEvaluationRosterService;

            _applicationClassScheduleService = applicationClassScheduleService;
            _iDPScheduleService = iDPScheduleService;
        }

        public async System.Threading.Tasks.Task Handle(OnClassSchedule_Employee_Completed payload, CancellationToken cancellationToken)
        {
            try
            {
                await _handleMetaIla(payload);
                await UpdateIDPScheduleAsync(payload);
            }
            catch (Exception e)
            {
                _logger.LogError("ClassSchedule_Employee_CompletedHandler._handleMetaILas", e);
            }
        }

        private async System.Threading.Tasks.Task _handleMetaIla(OnClassSchedule_Employee_Completed payload)
        {
            var classSchedule = payload.ClassSchedule_Employee.ClassSchedule;

            if (classSchedule == null)
                classSchedule = await _classScheduleService.GetWithDetailsAsync(payload.ClassSchedule_Employee.ClassScheduleId);

            var metaEmployees = await _metaIlaEmployeeService.GetByIlaIdAndEmployeeId(classSchedule.ILAID.Value, payload.ClassSchedule_Employee.EmployeeId);

            foreach (var metaEmployee in metaEmployees)
            {
                var links = metaEmployee.MetaILA.Meta_ILAMembers_Links.OrderBy(r => r.SequenceNumber).ToList();
                var currentLink = links.Where(r => r.ILAID == classSchedule.ILAID).First();
                var currentIdx = links.IndexOf(currentLink);

                metaEmployee.Fulfill(currentLink, payload.ClassSchedule_Employee);
                await _metaIlaEmployeeService.UpdateAsync(metaEmployee);

                if (currentIdx + 1 == links.Count())
                {
                    if(metaEmployee.MetaILA.MetaILA_SummaryTest_FinalTestId != null)
                    {
                        await releaseTest(metaEmployee);
                    }
                    if(metaEmployee.MetaILA.StudentEvaluationId != null)
                    {
                        await releaseEvaluation(metaEmployee);
                    }
                    return;
                }

                var nextLink = links[currentIdx + 1];

                //on demand
                if (nextLink.MetaILAConfigPublishOptionID == 1)
                    continue;

                //Passing
                if (nextLink.MetaILAConfigPublishOptionID == 2)
                {
                    if (payload.ClassSchedule_Employee.IsFinalGradePassing)
                    {
                        await releaseNextClassToEmployee(metaEmployee, nextLink);
                    }
                }

                //Complete
                if (nextLink.MetaILAConfigPublishOptionID == 3)
                {
                    if (payload.ClassSchedule_Employee.IsComplete)
                    {
                        await releaseNextClassToEmployee(metaEmployee, nextLink);
                    }
                }
            }
        }

        private async System.Threading.Tasks.Task releaseEvaluation(MetaILA_Employee metaEmployee)
        {
            var roster = new ClassSchedule_Evaluation_Roster(metaEmployee.MetaILAId, metaEmployee.MetaILA.StudentEvaluationId.Value, metaEmployee.EmployeeId);
            roster.Release(DateTime.Now.ToUniversalTime());
            await _classScheduleEvaluationRosterService.AddAsync(roster);
        }

        private async System.Threading.Tasks.Task releaseTest(MetaILA_Employee metaEmployee)
        {
            var roster = new ClassSchedule_Roster(metaEmployee.Id, metaEmployee.MetaILA.MetaILA_SummaryTest_FinalTestId.Value, metaEmployee.EmployeeId);
            roster.Release(DateTime.Now.ToUniversalTime());
            await _classScheduleRosterService.AddAsync(roster);
        }

        private async System.Threading.Tasks.Task releaseNextClassToEmployee(MetaILA_Employee metaEmployee, Meta_ILAMembers_Link nextLink)
        {
            var employee = metaEmployee.Employee;

            if (employee == null)
                employee = await _employeeService.GetAsync(metaEmployee.EmployeeId);

            if (nextLink.ILA.IsSelfPaced)
            {
                var cs = await _applicationClassScheduleService.CreateAsync(new Infrastructure.Model.ClassSchedule.ClassScheduleCreateOptions()
                {
                    ILAID = nextLink.ILAID,
                    IsStartAndEndTimeEmpty = false,
                    EndDateTime = DateTime.Now.ToUniversalTime().AddDays(7),
                    StartDateTime = DateTime.Now.ToUniversalTime(),
                    ProviderID = nextLink.ILA.ProviderId,
                    ClassSize =1,
                });

                await _applicationClassScheduleService.LinkEmployee(cs.Id, new Infrastructure.Model.ClassSchedule_Employee.ClassSchedule_EmployeeCreateOptions()
                {
                    classScheduleId = cs.Id,
                    employeeIds = new int[] { metaEmployee.EmployeeId }
                });

                var clientSettingsNotification = await _clientSettingsNotificationService.GetClientSettingNotificationByName("Meta ILA - Self Paced Released");
                await _notificationService.AddAsync(new Notification(DateTime.Now.ToUniversalTime(), employee.PersonId, clientSettingsNotification.Steps.First().Id));
            }
            else if (nextLink.ILA.ILA_SelfRegistrationOption.MakeAvailableForSelfReg)
            {
                var employeeNotificationSetting = await _clientSettingsNotificationService.GetClientSettingNotificationByName("Meta ILA - Employee - Self Registration Needed");

                if (employeeNotificationSetting.Enabled)
                {
                    await _notificationService.AddAsync(new Notification(DateTime.Now.ToUniversalTime(), employee.PersonId, employeeNotificationSetting.Steps.First().Id));
                }

                var adminNotificationSetting = await _clientSettingsNotificationService.GetClientSettingNotificationByName("Meta ILA - Admin - Self Registration Needed");
                if (adminNotificationSetting.Enabled)
                {
                    var adminUsers = await _qtdUserService.GetAllActive();
                    foreach (var adminUser in adminUsers)
                    {
                        await _notificationService.AddAsync(new Notification(DateTime.Now.ToUniversalTime(), adminUser.PersonId, adminNotificationSetting.Steps.First().Id));
                    }
                }
            }
            else
            {
                var employeeNotificationSetting = await _clientSettingsNotificationService.GetClientSettingNotificationByName("Meta ILA - Employee - Registration Needed");
                if (employeeNotificationSetting.Enabled)
                {
                    await _notificationService.AddAsync(new Notification(DateTime.Now.ToUniversalTime(), employee.PersonId, employeeNotificationSetting.Steps.First().Id));
                }

                var adminNotificationSetting = await _clientSettingsNotificationService.GetClientSettingNotificationByName("Meta ILA - Admin - Registration Needed");
                if (adminNotificationSetting.Enabled)
                {
                    var adminUsers = await _qtdUserService.GetAllActive();
                    foreach (var adminUser in adminUsers)
                    {
                        await _notificationService.AddAsync(new Notification(DateTime.Now.ToUniversalTime(), employee.PersonId, adminNotificationSetting.Steps.First().Id));
                    }
                }
            }
        }

        public async System.Threading.Tasks.Task UpdateIDPScheduleAsync(OnClassSchedule_Employee_Completed payload)
        {
            var classEmployee = payload.ClassSchedule_Employee;
            var idpSchedule = (await _iDPScheduleService.GetIDPSchedulesByClassIdAndEmpIdAsync(classEmployee.ClassScheduleId, classEmployee.EmployeeId));
            idpSchedule.UpdateGradeRelatedData(classEmployee.FinalGrade,classEmployee.GradeNotes,classEmployee.FinalScore?.ToString(),classEmployee.PopulateOJTRecord,classEmployee.CompletionDate);
            await _iDPScheduleService.UpdateAsync(idpSchedule);
        }
    }
}
