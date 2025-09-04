using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using QTD2.Domain.Events.Core;
using QTD2.Infrastructure.Notification.Interfaces;
using QTD2.Infrastructure.Notification.Notifications;
using QTD2.Domain.Interfaces.Service.Core;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace QTD2.Application.EventHandlers.Core
{
    public class MetaIla_Employee_EnrolledEventHandler : INotificationHandler<MetaIla_Employee_Enrolled>
    {
        private readonly ILogger<ClassSchedule_Employee_CompletedHandler> _logger;

        IMetaILAService _metaIlaService;
        IClassScheduleService _classScheduleService;
        INotificationService _notificationService;
        IClientSettings_NotificationService _clientSettingsNotificationService;
        IQTDUserService _qtdUserService;
        IEmployeeService _employeeService;
        IClassSchedule_RosterService _classScheduleRosterService;

        Interfaces.Services.Shared.IClassScheduleService _applicationClassScheduleService;

        public MetaIla_Employee_EnrolledEventHandler(
            ILogger<ClassSchedule_Employee_CompletedHandler> logger,
            IMetaILAService metaIlaService,
            IClassScheduleService classScheduleService,
            INotificationService notificationService,
            IClientSettings_NotificationService clientSettingsNotificationService,
            IQTDUserService qtdUserService,
            IEmployeeService employeeService,
            IClassSchedule_RosterService classScheduleRosterService,
            Interfaces.Services.Shared.IClassScheduleService applicationClassScheduleService
            )
        {
            _logger = logger;
            _metaIlaService = metaIlaService;
            _classScheduleService = classScheduleService;
            _notificationService = notificationService;
            _clientSettingsNotificationService = clientSettingsNotificationService;
            _qtdUserService = qtdUserService;
            _employeeService = employeeService;
            _classScheduleRosterService = classScheduleRosterService;

            _applicationClassScheduleService = applicationClassScheduleService;
        }

        public async Task Handle(MetaIla_Employee_Enrolled notification, CancellationToken cancellationToken)
        {
            try
            {
                await _pushOnDemand(notification.MetaILA_Employee, notification.UseCurrentDate);
            }
            catch (Exception e)
            {
                _logger.LogError("MetaIla_Employee_EnrolledEventHandler._pushOnDemand", e);
                return;
            }
        }

        private async Task _pushOnDemand(Domain.Entities.Core.MetaILA_Employee metaILA_Employee, bool useCurrentDate)
        {
            var metaIla = await _metaIlaService.GetWithMembersAsync(metaILA_Employee.MetaILAId);

            foreach (var metaIlaMember in metaIla.Meta_ILAMembers_Links)
            {
                if (metaIlaMember.MetaILAConfigPublishOptionID == 1)
                {
                    await releaseNextClassToEmployee(metaILA_Employee, metaIlaMember, useCurrentDate);
                }
            }
        }

        private async System.Threading.Tasks.Task releaseNextClassToEmployee(Domain.Entities.Core.MetaILA_Employee metaEmployee, Domain.Entities.Core.Meta_ILAMembers_Link nextLink, bool useCurrentDate)
        {
            var employee = metaEmployee.Employee;

            if (employee == null)
                employee = await _employeeService.GetAsync(metaEmployee.EmployeeId);

            if (nextLink.ILA.IsSelfPaced)
            {
                var startDate = useCurrentDate ? DateTime.UtcNow : (nextLink.StartDate ?? DateTime.UtcNow);

                var cs = await _applicationClassScheduleService.CreateAsync(new Infrastructure.Model.ClassSchedule.ClassScheduleCreateOptions()
                {
                    ILAID = nextLink.ILAID,
                    IsStartAndEndTimeEmpty = false,
                    EndDateTime = startDate.AddDays(7),
                    StartDateTime = startDate,
                    ProviderID = nextLink.ILA.ProviderId,
                    ClassSize = nextLink.ILA.ClassSize ?? 30
                });

                await _applicationClassScheduleService.LinkEmployee(cs.Id, new Infrastructure.Model.ClassSchedule_Employee.ClassSchedule_EmployeeCreateOptions()
                {
                    classScheduleId = cs.Id,          
                    employeeIds = new int[] { metaEmployee.EmployeeId }
                });

                var clientSettingsNotification = await _clientSettingsNotificationService.GetClientSettingNotificationByName("Meta ILA - Self Paced Released");
                await _notificationService.AddAsync(new Domain.Entities.Core.MetaIlaSelfPacedReleasedNotification(DateTime.Now.ToUniversalTime(), null, nextLink.Id, employee.Id, employee.PersonId, clientSettingsNotification.Steps.First().Id));
            }
            else if (nextLink.ILA.ILA_SelfRegistrationOption != null && nextLink.ILA.ILA_SelfRegistrationOption.MakeAvailableForSelfReg)
            {
                var employeeNotificationSetting = await _clientSettingsNotificationService.GetClientSettingNotificationByName("Meta ILA - Employee - Self Registration Needed");

                if (employeeNotificationSetting.Enabled)
                {
                    await _notificationService.AddAsync(new Domain.Entities.Core.MetaIla_Employee_SelfRegistrationRequiredNotification(DateTime.Now.ToUniversalTime(), null, nextLink.Id, employee.Id, employee.PersonId, employeeNotificationSetting.Steps.First().Id));
                }

                var adminNotificationSetting = await _clientSettingsNotificationService.GetClientSettingNotificationByName("Meta ILA - Admin - Self Registration Needed");
                if (adminNotificationSetting.Enabled)
                {
                    var adminUsers = await _qtdUserService.GetAllActive();
                    foreach (var adminUser in adminUsers)
                    {
                        await _notificationService.AddAsync(new Domain.Entities.Core.MetaIla_Admin_SelfRegistrationRequiredNotification(DateTime.Now.ToUniversalTime(), null, nextLink.Id, employee.Id, adminUser.PersonId, adminNotificationSetting.Steps.First().Id));
                    }
                }
            }
            else
            {
                var employeeNotificationSetting = await _clientSettingsNotificationService.GetClientSettingNotificationByName("Meta ILA - Employee - Registration Needed");
                if (employeeNotificationSetting.Enabled)
                {
                    await _notificationService.AddAsync(new Domain.Entities.Core.MetaIla_Employee_RegistrationRequiredNotification(DateTime.Now.ToUniversalTime(), null, nextLink.Id, employee.Id, employee.PersonId, employeeNotificationSetting.Steps.First().Id));
                }

                var adminNotificationSetting = await _clientSettingsNotificationService.GetClientSettingNotificationByName("Meta ILA - Admin - Registration Needed");
                if (adminNotificationSetting.Enabled)
                {
                    var adminUsers = await _qtdUserService.GetAllActive();
                    foreach (var adminUser in adminUsers)
                    {
                        await _notificationService.AddAsync(new Domain.Entities.Core.MetaIla_Employee_RegistrationRequiredNotification(DateTime.Now.ToUniversalTime(), null, nextLink.Id, employee.Id, employee.PersonId, adminNotificationSetting.Steps.First().Id));
                    }
                }
            }
        }
    }
}
