using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QTD2.Application.Interfaces.Services.QTD;
using QTD2.Data;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Services.Core;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.Hashing.Interfaces;
using QTD2.Infrastructure.HttpClients;
using QTD2.Infrastructure.Notification.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Services.QTD
{
    public class JobNotificationService : IJobNotificationService
    {
        private readonly INotificationFactory _notificationFactory;
        private readonly INotifierFactory _notifierFactory;
        private INotifier _notifier;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHasher _hasher;
        private readonly QtdAuthenticationService _qtdAuthenticationService;
        private readonly HashSet<int> _othersNotified = new HashSet<int>();

        public JobNotificationService(
            INotificationFactory notificationFactory,
            INotifierFactory notifierFactory,
            UserManager<AppUser> userManager,
            IHasher hasher,
            QtdAuthenticationService qtdAuthenticationService
        )
        {
            _notificationFactory = notificationFactory;
            _notifierFactory = notifierFactory;
            _userManager = userManager;
            _hasher = hasher;
            _qtdAuthenticationService = qtdAuthenticationService;
        }


        public async Task<bool> SendCertificationExpirationNotification(int employeeCertificationId, int order, QTDContext qtdContext)
        {
            var defaultTimeZone = (await qtdContext.ClientSettings_GeneralSettings.ToListAsync()).LastOrDefault()?.DefaultTimeZone;

            var setting = await GetClientSettingNotificationByName("Certification Expiration", qtdContext);
            var employeeCertification = await qtdContext.EmployeeCertifications
                .Include("Employee.Person")
                .Include("Employee.EmployeeOrganizations")
                .Include("Certification")
                .FirstOrDefaultAsync(r => r.Id == employeeCertificationId);

            if (setting == null || employeeCertification == null)
                return false;

            List<string> destination = new List<string>();
            destination.Add(employeeCertification.Employee.Person.Username);
            var otherEmails = setting.Steps.Where(s=>s.Order == order).SelectMany(m => m.CustomSettings).Where(x => x.Key == "Send To Others" && !x.Value.IsNullOrEmpty()).FirstOrDefault();
            if (otherEmails != null)
            {
                var emails = otherEmails.Value.Split(",");
                destination.AddRange(emails);
            }
            var toManager = setting.Steps.Where(s => s.Order == order).SelectMany(m => m.CustomSettings).Where(x => x.Key == "Send To Managers" && !x.Value.IsNullOrEmpty()).FirstOrDefault();
            if(toManager != null && toManager.Value == "true")
            {
                var employeeOrgs = employeeCertification.Employee.EmployeeOrganizations.Select(o => o.OrganizationId).Distinct().ToList();
                var managers = await qtdContext.EmployeeOrganizations.Where(eo => employeeOrgs.Contains(eo.OrganizationId) && eo.IsManager && eo.EmployeeId != employeeCertification.EmployeeId)
                .Include(eo => eo.Employee.Person).Select(eo => eo.Employee.Person.Username).ToListAsync();

                destination.AddRange(managers);

            }
            destination = destination.Where(email => !string.IsNullOrWhiteSpace(email)).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
            var notification = _notificationFactory.CreateCertificationExpirationNotification(destination, order, setting, employeeCertification, defaultTimeZone);

            return await sendNotificationAsync(notification);
        }

        public async Task<bool> SendClassScheduleNotification(int classSchedule_EmployeeId, int order, QTDContext qtdContext)
        {
            var setting = await GetClientSettingNotificationByName("Class Schedule", qtdContext);
            
            var classSchedule_Employee = await qtdContext.ClassScheduleEmployees
                .Include("ClassSchedule")
                .Include("ClassSchedule.Instructor")
                .Include("ClassSchedule.Location")
                .Include("ClassSchedule.ILA.Provider")
                .Include("ClassSchedule.ClassSchedule_TestReleaseEMPSettings.PreTest")
                .Include("ClassSchedule.ClassSchedule_TestReleaseEMPSettings.FinalTest")
                .Include("Employee.Person")
                .FirstOrDefaultAsync(r => r.Id == classSchedule_EmployeeId);

            if (setting == null || classSchedule_Employee == null)
                return false;

            List<string> destination = new List<string>();
            destination.Add(classSchedule_Employee.Employee.Person.Username);

            var notification = _notificationFactory.CreateClassScheduleNotification(destination, order, setting, classSchedule_Employee);
            return await sendNotificationAsync(notification);
        }

        public async Task<bool> SendEmpDifSurveyNotification(int employeeId, int surveyId, int order, QTDContext qtdContext)
        {
            var defaultTimeZone = (await qtdContext.ClientSettings_GeneralSettings.ToListAsync()).LastOrDefault()?.DefaultTimeZone;
            var setting = await GetClientSettingNotificationByName("EMP DIF Survey", qtdContext);
            var employee = await qtdContext.Employees
                .Include("Person")
                .FirstOrDefaultAsync(r => r.Id == employeeId);

            if (setting == null || employee == null)
                return false;

            var survey = new { };

            List<string> destination = new List<string>();
            destination.Add(employee.Person.Username);

            var notification = _notificationFactory.CreateDifSurveyNotification(destination, order, setting, employee, survey, defaultTimeZone);

            return await sendNotificationAsync(notification);
        }

        public async Task<bool> SendEmpCourseNotification(int employeeId, int cbtId, int order, QTDContext qtdContext)
        {
            var defaultTimeZone = (await qtdContext.ClientSettings_GeneralSettings.ToListAsync()).LastOrDefault()?.DefaultTimeZone;
            var setting = await GetClientSettingNotificationByName("EMP Online Course", qtdContext);
            var employee = await qtdContext.Employees
                .Include("Person")
                .FirstOrDefaultAsync(r => r.Id == employeeId);
            var cbt = await qtdContext.CBTs
                .Include("ILA")
                .FirstOrDefaultAsync(r => r.Id == cbtId);

            if (setting == null || employee == null || cbt == null)
                return false;

            List<string> destination = new List<string>();
            destination.Add(employee.Person.Username);

            var notification = _notificationFactory.CreateEmpCourseNotification(destination, order, setting, employee, cbt, defaultTimeZone);

            return await sendNotificationAsync(notification);
        }

        public async Task<bool> SendEmpLoginNotification(int employeeId, int order, string url, QTDContext qtdContext)
        {
            var setting = await GetClientSettingNotificationByName("EMP Login", qtdContext);
            var employee = await qtdContext.Employees
                .Include("Person")
                .FirstOrDefaultAsync(r => r.Id == employeeId);

            if (setting == null || employee == null || setting.Enabled == false)
                return false;
            url = url + "/auth/login";
            //string url = "";

            List<string> destination = new List<string>();
            destination.Add(employee.Person.Username);

            var notification = _notificationFactory.CreateEmpLoginNotification(destination, order, setting, employee, url);

            return await sendNotificationAsync(notification);
        }

        public async Task<bool> SendEmpPretestNotification(int classScheduleRosterId, int order, QTDContext qtdContext)
        {
            var defaultTimeZone = (await qtdContext.ClientSettings_GeneralSettings.ToListAsync()).LastOrDefault()?.DefaultTimeZone;
            var setting = await GetClientSettingNotificationByName("EMP Pretest", qtdContext);
            var classScheduleRoster = await qtdContext.ClassSchedule_Roster
                .Include("ClassSchedule")
                .Include("ClassSchedule.Instructor")
                .Include("ClassSchedule.Location")
                .Include("ClassSchedule.ILA.Provider")
                .Include("ClassSchedule.ClassSchedule_TestReleaseEMPSettings.PreTest")
                .Include("ClassSchedule.ClassSchedule_TestReleaseEMPSettings.FinalTest")
                .Include("Employee.Person")
                .FirstOrDefaultAsync(r => r.Id == classScheduleRosterId);

            if (setting == null || classScheduleRoster == null)
                return false;

            List<string> destination = new List<string>();
            destination.Add(classScheduleRoster.Employee.Person.Username);

            var notification = _notificationFactory.CreateEmpPretestNotification(destination, order, setting, classScheduleRoster, defaultTimeZone);

            return await sendNotificationAsync(notification);
        }

        public async Task<bool> SendEmpProcedureReviewNotification(int procedureReviewEmployeeId, int order, QTDContext qtdContext)
        {
            var defaultTimeZone = (await qtdContext.ClientSettings_GeneralSettings.ToListAsync()).LastOrDefault()?.DefaultTimeZone;
            var setting = await GetClientSettingNotificationByName("EMP Procedure Review", qtdContext);

            var procedureReviewEmployee = await qtdContext.ProcedureReview_Employees
                .Include("ProcedureReview.Procedure")
                .Include("Employee.Person")
                .FirstOrDefaultAsync(r => r.Id == procedureReviewEmployeeId);

            if (setting == null || procedureReviewEmployee == null)
                return false;

            List<string> destination = new List<string>();
            destination.Add(procedureReviewEmployee.Employee.Person.Username);

            var notification = _notificationFactory.CreateEmpProcedureReviewNotification(destination, order, setting, procedureReviewEmployee, defaultTimeZone);

            return await sendNotificationAsync(notification);
        }

        public async Task<bool> SendEmpStudentEvaluationNotification(int classScheduleEvaluationRosterId, int order, QTDContext qtdContext)
        {
            var defaultTimeZone = (await qtdContext.ClientSettings_GeneralSettings.ToListAsync()).LastOrDefault()?.DefaultTimeZone;
            var setting = await GetClientSettingNotificationByName("EMP Student Evaluation", qtdContext);
            
            var classScheduleEvaluationRoster = await qtdContext.ClassSchedule_Evaluation_Roster
                .Include("ClassScheduleInfo")
                .Include("ClassScheduleInfo.Instructor")
                .Include("ClassScheduleInfo.Location")
                .Include("ClassScheduleInfo.ILA.Provider")
                .Include("Employee.Person")
                .FirstOrDefaultAsync(r => r.Id == classScheduleEvaluationRosterId);

            if (setting == null || classScheduleEvaluationRoster == null)
                return false;


            List<string> destination = new List<string>();
            destination.Add(classScheduleEvaluationRoster.Employee.Person.Username);

            var notification = _notificationFactory.CreateEmpStudentEvaluationNotification(destination, order, setting, classScheduleEvaluationRoster, defaultTimeZone);

            return await sendNotificationAsync(notification);
        }

        public async Task<bool> SendEmpTestNotification(int classScheduleRosterId, int order, QTDContext qtdContext)
        {
            var defaultTimeZone = (await qtdContext.ClientSettings_GeneralSettings.ToListAsync()).LastOrDefault()?.DefaultTimeZone;
            var setting = await GetClientSettingNotificationByName("EMP Test", qtdContext);
            var classScheduleRoster = await qtdContext.ClassSchedule_Roster
                .Include("ClassSchedule")
                .Include("ClassSchedule.Instructor")
                .Include("ClassSchedule.Location")
                .Include("ClassSchedule.ILA.Provider")
                .Include("ClassSchedule.ClassSchedule_TestReleaseEMPSettings.PreTest")
                .Include("ClassSchedule.ClassSchedule_TestReleaseEMPSettings.FinalTest")
                .Include("Employee.Person")
                .FirstOrDefaultAsync(r => r.Id == classScheduleRosterId);

            if (setting == null || classScheduleRoster == null)
                return false;

            List<string> destination = new List<string>();
            destination.Add(classScheduleRoster.Employee.Person.Username);

            var notification = _notificationFactory.CreateEmpTestNotification(destination, order, setting, classScheduleRoster, defaultTimeZone);

            return await sendNotificationAsync(notification);
        }

        public async Task<bool> SendEmpGapSurveyNotification(int employeeId, int surveyId, int order, QTDContext qtdContext)
        {
            var defaultTimeZone = (await qtdContext.ClientSettings_GeneralSettings.ToListAsync()).LastOrDefault()?.DefaultTimeZone;
            var setting = await GetClientSettingNotificationByName("EMP GAP Survey", qtdContext);
            var employee = await qtdContext.Employees
                .Include("Person")
                .FirstOrDefaultAsync(r => r.Id == employeeId);

            if (setting == null || employee == null)
                return false;

            var survey = new { };

            List<string> destination = new List<string>();
            destination.Add(employee.Person.Username);

            var notification = _notificationFactory.CreateGapSurveyNotification(destination, order, setting, employee, survey, defaultTimeZone);

            return await sendNotificationAsync(notification);
        }

        public async Task<bool> SendEmpIDPReviewNotification(int employeeId, int idpId, int order, QTDContext qtdContext)
        {
            var defaultTimeZone = (await qtdContext.ClientSettings_GeneralSettings.ToListAsync()).LastOrDefault()?.DefaultTimeZone;
            var setting = await GetClientSettingNotificationByName("EMP IDP Review", qtdContext);



            //NOTE: The following isn't setup yet as IDP_Review was not added in the QTDContext as a DBSet for some reason
            throw new NotImplementedException();
            //var idpReview = await qtdContext.IDP_Reviews.Include("Employee.Person").Include("IDP_ReviewStatus").LastOrDefaultAsync(r => r.EmployeeId == employeeId); <-- Can't do this



            //if (setting == null || idpReview == null)
            //    return false;

            //List<string> destination = new List<string>();
            //destination.Add(idpReview.Employee.Person.Username);

            //var notification = _notificationFactory.CreateIDPReviewNotification(destination, order, setting, idpReview, defaultTimeZone);

            //return await sendNotificationAsync(notification);
        }

        public async Task<bool> SendEmpSelfRegistrationApprovalNotification(int classScheduleEmployeeId, int order, QTDContext qtdContext)
        {
            var defaultTimeZone = (await qtdContext.ClientSettings_GeneralSettings.ToListAsync()).LastOrDefault()?.DefaultTimeZone;
            var setting = await GetClientSettingNotificationByName("EMP Self-Registration Approval", qtdContext);

            //todo Check Includes
            var classSchedule_Employee = await qtdContext.ClassScheduleEmployees
                .Include("ClassSchedule")
                .Include("ClassSchedule.Instructor")
                .Include("ClassSchedule.Location")
                .Include("ClassSchedule.ILA.Provider")
                .Include("ClassSchedule.ClassSchedule_TestReleaseEMPSettings.PreTest")
                .Include("ClassSchedule.ClassSchedule_TestReleaseEMPSettings.FinalTest")
                .Include("Employee.Person")
                .FirstOrDefaultAsync(r => r.Id == classScheduleEmployeeId);

            if (setting == null || classSchedule_Employee == null)
                return false;

            List<string> destination = new List<string>();
            destination.Add(classSchedule_Employee.Employee.Person.Username);

            var notification = _notificationFactory.CreateSelfRegistrationApprovalNotification(destination, order, setting, classSchedule_Employee, defaultTimeZone);

            return await sendNotificationAsync(notification);
        }

        public async Task<bool> SendEmpSelfRegistrationDenialNotification(int classScheduleEmployeeId, int order, QTDContext qtdContext)
        {
            var defaultTimeZone = (await qtdContext.ClientSettings_GeneralSettings.ToListAsync()).LastOrDefault()?.DefaultTimeZone;
            var setting = await GetClientSettingNotificationByName("EMP Self-Registration Denial", qtdContext);

            //todo Check Includes
            var classSchedule_Employee = await qtdContext.ClassScheduleEmployees
                .Include("ClassSchedule")
                .Include("ClassSchedule.Instructor")
                .Include("ClassSchedule.Location")
                .Include("ClassSchedule.ILA.Provider")
                .Include("ClassSchedule.ClassSchedule_TestReleaseEMPSettings.PreTest")
                .Include("ClassSchedule.ClassSchedule_TestReleaseEMPSettings.FinalTest")
                .Include("Employee.Person")
                .FirstOrDefaultAsync(r => r.Id == classScheduleEmployeeId);

            if (setting == null || classSchedule_Employee == null)
                return false;

            var course = new { };

            List<string> destination = new List<string>();
            destination.Add(classSchedule_Employee.Employee.Person.Username);

            var notification = _notificationFactory.CreateSelfRegistrationDenialNotification(destination, order, setting, classSchedule_Employee, defaultTimeZone);

            return await sendNotificationAsync(notification);
        }

        public async Task<bool> SendEmpTaskQualitificationEvaluatorNotification(int taskQualification_Evaluator_LinkId, int order, QTDContext qtdContext)
        {
            var setting = await GetClientSettingNotificationByName("EMP Task Qualification - Evaluator", qtdContext);
            var taskQualificiation_Evaluatorn_Link = await qtdContext.TaskQualification_Evaluator_Links
                .Include("Evaluator.Person")
                .Include("TaskQualification.Task.SubdutyArea.DutyArea")
                .Include("TaskQualification.Employee.Person")
                .FirstOrDefaultAsync(r => r.Id == taskQualification_Evaluator_LinkId);

            if (setting == null || taskQualificiation_Evaluatorn_Link == null)
                return false;

            List<string> destination = new List<string>();
            destination.Add(taskQualificiation_Evaluatorn_Link.Evaluator.Person.Username);

            var notification = _notificationFactory.CreateTaskQualitificationEvaluatorNotification(destination, order, setting, taskQualificiation_Evaluatorn_Link);

            return await sendNotificationAsync(notification);
        }

        public async Task<bool> SendEmpTaskQualitificationTraineeNotification(int taskQualificationId, int order, QTDContext qtdContext)
        {
            var setting = await GetClientSettingNotificationByName("EMP Task Qualification - Trainee", qtdContext);
            var taskQualification = await qtdContext.TaskQualifications
                .Include("Employee.Person")
                .Include("EvaluationMethod")
                .Include("TaskQualification_Evaluator_Links.Evaluator.Person")
                .Include("Task.SubdutyArea.DutyArea")
                .FirstOrDefaultAsync(r => r.Id == taskQualificationId);

            if (setting == null || taskQualification == null)
                return false;

            List<string> destination = new List<string>();
            destination.Add(taskQualification.Employee.Person.Username);

            var notification = _notificationFactory.CreateTaskQualitificationTraineeNotification(destination, order, setting, taskQualification);

            return await sendNotificationAsync(notification);
        }

        public async Task<bool> SendMetaILASelfPacedReleasedNotification(int employeeId, int nextMeta_ILAMembers_LinkId, int? metaILA_Employee_MemberLinkFufillmentId, int order, QTDContext qtdContext)
        {
            var setting = await GetClientSettingNotificationByName("Meta ILA - Self Paced Released", qtdContext);

            var nextIla = await qtdContext.Meta_ILAMembers_Links
                .Include("ILA")
                .FirstOrDefaultAsync(r => r.Id == nextMeta_ILAMembers_LinkId);
            var fulfillment = metaILA_Employee_MemberLinkFufillmentId.HasValue ? 
                await qtdContext.MetaILA_Employee_MemberLinkFufillments
                    .Include("MetaILA_Employee.Employee.Person")
                    .Include("FufilledBy_ClassScheduleEmployee.ClassSchedule.ILA")
                    .FirstOrDefaultAsync(r => r.Id == metaILA_Employee_MemberLinkFufillmentId)
                : null;
            var employee = await qtdContext.Employees
                .Include("Person")
                .FirstOrDefaultAsync(r => r.Id == employeeId);

            if (setting == null || nextIla == null)
                return false;

            if (!setting.Enabled)
                return false;

            List<string> destination = new List<string>();
            destination.Add(employee.Person.Username);

            var notification = _notificationFactory.CreateMetaILASelfPacedReleasedNotification(destination, order, setting, employee, nextIla, fulfillment);

            return await sendNotificationAsync(notification);
        }

        public async Task<bool> SendMetaILA_Employee_SelfRegistrationRequiredNotification(int employeeId, int nextMeta_ILAMembers_LinkId, int? metaILA_Employee_MemberLinkFufillmentId, int order, QTDContext qtdContext)
        {
            var setting = await GetClientSettingNotificationByName("Meta ILA - Employee - Self Registration Needed", qtdContext);

            var nextIla = await qtdContext.Meta_ILAMembers_Links
                .Include("ILA")
                .FirstOrDefaultAsync(r => r.Id == nextMeta_ILAMembers_LinkId);
            var fulfillment = metaILA_Employee_MemberLinkFufillmentId.HasValue ?
                await qtdContext.MetaILA_Employee_MemberLinkFufillments
                    .Include("MetaILA_Employee.Employee.Person")
                    .Include("FufilledBy_ClassScheduleEmployee.ClassSchedule.ILA")
                    .FirstOrDefaultAsync(r => r.Id == metaILA_Employee_MemberLinkFufillmentId)
                : null;
            var employee = await qtdContext.Employees
                .Include("Person")
                .FirstOrDefaultAsync(r => r.Id == employeeId);
            //The following wasn't originally using ILAId, as the DomainService it originally came from had it as a parameter yet didn't filter by it. It has been added seemingly for correctness
            var classSchedules = await qtdContext.ClassSchedules
                .Include("ILA.Provider")
                .Where(r => r.ILA.ILA_SelfRegistrationOption.MakeAvailableForSelfReg && r.StartDateTime > DateTime.Now.ToUniversalTime() && r.ILAID == nextIla.ILAID)
                .ToListAsync();

            if (setting == null || nextIla == null)
                return false;

            if (!setting.Enabled)
                return false;

            List<string> destination = new List<string>();
            destination.Add(employee.Person.Username);

            var notification = _notificationFactory.CreateMetaILA_Employee_SelfRegistrationRequiredNotification(destination, order, setting, employee, nextIla, fulfillment, classSchedules);

            return await sendNotificationAsync(notification);
        }

        public async Task<bool> SendMetaILA_Admin_SelfRegistrationRequiredNotification(int? toPersonId, int employeeId, int nextMeta_ILAMembers_LinkId, int? metaILA_Employee_MemberLinkFufillmentId, int order, QTDContext qtdContext)
        {
            var setting = await GetClientSettingNotificationByName("Meta ILA - Admin - Self Registration Needed", qtdContext);

            var nextIla = await qtdContext.Meta_ILAMembers_Links
                .Include("ILA")
                .FirstOrDefaultAsync(r => r.Id == nextMeta_ILAMembers_LinkId);
            var fulfillment = metaILA_Employee_MemberLinkFufillmentId.HasValue ?
                await qtdContext.MetaILA_Employee_MemberLinkFufillments
                    .Include("MetaILA_Employee.Employee.Person")
                    .Include("FufilledBy_ClassScheduleEmployee.ClassSchedule.ILA")
                    .FirstOrDefaultAsync(r => r.Id == metaILA_Employee_MemberLinkFufillmentId)
                : null;
            var employee = await qtdContext.Employees
                .Include("Person")
                .FirstOrDefaultAsync(r => r.Id == employeeId);
            //The following wasn't originally using ILAId, as the DomainService it originally came from had it as a parameter yet didn't filter by it. It has been added seemingly for correctness
            var classSchedules = await qtdContext.ClassSchedules
                .Include("ILA.Provider")
                .Where(r => r.ILA.ILA_SelfRegistrationOption.MakeAvailableForSelfReg && r.StartDateTime > DateTime.Now.ToUniversalTime() && r.ILAID == nextIla.ILAID)
                .ToListAsync();
            var person = await qtdContext.Persons.FirstOrDefaultAsync(r => r.Id == toPersonId);

            if (setting == null || nextIla == null)
                return false;

            if (!setting.Enabled)
                return false;

            List<string> destination = new List<string>();
            if (person != null) {
                destination.Add(person.Username);
            }

            var notification = _notificationFactory.CreateMetaILA_Admin_SelfRegistrationRequiredNotification(destination, order, setting, employee, nextIla, fulfillment, classSchedules);

            return await sendNotificationAsync(notification);
        }

        public async Task<bool> SendMetaILA_Employee_RegistrationRequiredNotification(int employeeId, int nextMeta_ILAMembers_LinkId, int? metaILA_Employee_MemberLinkFufillmentId, int order, QTDContext qtdContext)
        {
            var setting = await GetClientSettingNotificationByName("Meta ILA - Employee - Registration Needed", qtdContext);

            var nextIla = await qtdContext.Meta_ILAMembers_Links
                .Include("ILA")
                .FirstOrDefaultAsync(r => r.Id == nextMeta_ILAMembers_LinkId);
            var fulfillment = metaILA_Employee_MemberLinkFufillmentId.HasValue ?
                await qtdContext.MetaILA_Employee_MemberLinkFufillments
                    .Include("MetaILA_Employee.Employee.Person")
                    .Include("FufilledBy_ClassScheduleEmployee.ClassSchedule.ILA")
                    .FirstOrDefaultAsync(r => r.Id == metaILA_Employee_MemberLinkFufillmentId) 
                : null;
            var employee = await qtdContext.Employees
                .Include("Person")
                .FirstOrDefaultAsync(r => r.Id == employeeId);

            if (setting == null || nextIla == null)
                return false;

            if (!setting.Enabled)
                return false;

            List<string> destination = new List<string>();
            destination.Add(employee.Person.Username);

            var notification = _notificationFactory.CreateMetaILA_Employee_RegistrationRequiredNotification(destination, order, setting, employee, nextIla, fulfillment);

            return await sendNotificationAsync(notification);
        }

        public async Task<bool> SendMetaILA_Admin_RegistrationRequiredNotification(int? toPersonId, int employeeId, int nextMeta_ILAMembers_LinkId, int? metaILA_Employee_MemberLinkFufillmentId, int order, QTDContext qtdContext)
        {
            var setting = await GetClientSettingNotificationByName("Meta ILA - Admin - Registration Needed", qtdContext);

            var nextIla = await qtdContext.Meta_ILAMembers_Links
                .Include("ILA")
                .FirstOrDefaultAsync(r => r.Id == nextMeta_ILAMembers_LinkId);
            var fulfillment = metaILA_Employee_MemberLinkFufillmentId.HasValue ?
                await qtdContext.MetaILA_Employee_MemberLinkFufillments
                    .Include("MetaILA_Employee.Employee.Person")
                    .Include("FufilledBy_ClassScheduleEmployee.ClassSchedule.ILA")
                    .FirstOrDefaultAsync(r => r.Id == metaILA_Employee_MemberLinkFufillmentId) 
                : null;
            var employee = await qtdContext.Employees
                .Include("Person")
                .FirstOrDefaultAsync(r => r.Id == employeeId);
            var person = await qtdContext.Persons.FirstOrDefaultAsync(r => r.Id == toPersonId);

            if (setting == null || nextIla == null)
                return false;

            if (!setting.Enabled)
                return false;

            List<string> destination = new List<string>();
            if (person != null) {
                destination.Add(person.Username);
            }

            var notification = _notificationFactory.CreateMetaILA_Admin_RegistrationRequiredNotification(destination, order, setting, employee, nextIla, fulfillment);

            return await sendNotificationAsync(notification);
        }

        public async Task<bool> SendMetaILA_CourseworkCompletedNotification(int employeeId, int metaILA_Employee_MemberLinkFufillmentId, int? classSchedule_RosterId, int? classSchedule_Evaluation_RosterId, int order, QTDContext qtdContext)
        {
            var setting = await GetClientSettingNotificationByName("Meta ILA - Admin - Registration Needed", qtdContext);
            var classScheduleRoster = classSchedule_RosterId.HasValue ? 
                await qtdContext.ClassSchedule_Roster
                    .Include("ClassSchedule")
                    .Include("ClassSchedule.Instructor")
                    .Include("ClassSchedule.Location")
                    .Include("ClassSchedule.ILA.Provider")
                    .Include("ClassSchedule.ClassSchedule_TestReleaseEMPSettings.PreTest")
                    .Include("ClassSchedule.ClassSchedule_TestReleaseEMPSettings.FinalTest")
                    .Include("Employee.Person")
                    .FirstOrDefaultAsync(r => r.Id == classSchedule_RosterId) 
                : null;
            var fulfillment = await qtdContext.MetaILA_Employee_MemberLinkFufillments
                .Include("MetaILA_Employee.Employee.Person")
                .Include("FufilledBy_ClassScheduleEmployee.ClassSchedule.ILA")
                .FirstOrDefaultAsync(r => r.Id == metaILA_Employee_MemberLinkFufillmentId);
            var classScheduleEvaluationRoster = classSchedule_Evaluation_RosterId.HasValue ? 
                await qtdContext.ClassSchedule_Evaluation_Roster
                    .Include("ClassScheduleInfo")
                    .Include("ClassScheduleInfo.Instructor")
                    .Include("ClassScheduleInfo.Location")
                    .Include("ClassScheduleInfo.ILA.Provider")
                    .Include("Employee.Person")
                    .FirstOrDefaultAsync(r => r.Id == classSchedule_Evaluation_RosterId)
                : null;
            var employee = await qtdContext.Employees
                .Include("Person")
                .FirstOrDefaultAsync(r => r.Id == employeeId);

            if (setting == null)
                return false;

            if (!setting.Enabled)
                return false;

            List<string> destination = new List<string>();
            destination.Add(employee.Person.Username);

            var notification = _notificationFactory.CreateMetaILA_CourseworkCompletedNotification(destination, order, setting, employee, fulfillment, classScheduleRoster, classScheduleEvaluationRoster);

            return await sendNotificationAsync(notification);
        }

        public async Task<bool> SendEMPOnlineCourseNotification(int classScheduleEmployeeId, int cbtId, int order, QTDContext qtdContext)
        {
            var defaultTimeZone = (await qtdContext.ClientSettings_GeneralSettings.ToListAsync()).LastOrDefault()?.DefaultTimeZone;
            var setting = await GetClientSettingNotificationByName("EMP Online Course", qtdContext);
            var classScheduleEmployee = await qtdContext.ClassScheduleEmployees
                .Include("ClassSchedule")
                .Include("ClassSchedule.Instructor")
                .Include("ClassSchedule.Location")
                .Include("ClassSchedule.ILA.Provider")
                .Include("ClassSchedule.ClassSchedule_TestReleaseEMPSettings.PreTest")
                .Include("ClassSchedule.ClassSchedule_TestReleaseEMPSettings.FinalTest")
                .Include("Employee.Person")
                .FirstOrDefaultAsync(r => r.Id == classScheduleEmployeeId);
            var cbt = await qtdContext.CBTs
                 .Include("ILA")
                 .FirstOrDefaultAsync(r => r.Id == cbtId);

            if (setting == null || classScheduleEmployee == null || cbt == null)
                return false;

            List<string> destination = new List<string>();
            destination.Add(classScheduleEmployee.Employee.Person.Username);

            var notification = _notificationFactory.CreateEMPOnlineCourseNotification(destination, order, setting, classScheduleEmployee, cbt, defaultTimeZone);

            return await sendNotificationAsync(notification);
        }
        public async Task<bool> SendEmpPortalCompletionNotification(AdminEMPCompletionNotification notification, int order, QTDContext qtdContext)
        {
            var defaultTimeZone = (await qtdContext.ClientSettings_GeneralSettings.ToListAsync()).LastOrDefault()?.DefaultTimeZone;
            var setting = await GetClientSettingNotificationByName("Admin - Employee Portal Completions", qtdContext);
            var person = await qtdContext.Persons.FirstOrDefaultAsync(r => r.Id == notification.ToPersonId);
            if (setting == null || person == null) { return false; }
            List<string> destination = new List<string>();
            if (person != null)
            {
                destination.Add(person.Username);
            }

            var notificationWithContent = _notificationFactory.CreateEmployeePortalCompletionNotification(destination, order, setting, notification, defaultTimeZone);

            var result = await sendNotificationAsync(notificationWithContent);
            return result;
        }
        public async Task<bool> SendSimulatorScenarioCollaborationNotification(SimulatorScenarioCollaborationNotification notification, int order, string url, QTDContext qtdContext)
        {
            var setting = await GetClientSettingNotificationByName("Simulator Scenario Collaboration", qtdContext);
            var simScenarioCollaborator = await qtdContext.SimulatorScenario_Collaborators
                .Include("User.Person")
                .Include("Permission")
                .Include("SimulatorScenario")
                .FirstOrDefaultAsync(r => r.Id == notification.SimulatorScenarioCollaboratorId);
            var person = await qtdContext.Persons.FirstOrDefaultAsync(r => r.Id == notification.ToPersonId);
            if (setting == null || person == null || simScenarioCollaborator == null) { return false; }
            List<string> destination = new List<string>();
            if (person != null)
            {
                destination.Add(person.Username);
            }

            if (simScenarioCollaborator.Permission.Permission == "Viewer")
            {
                url = $"{url}/dnd/simulatorscenarios/view/{_hasher.Encode(simScenarioCollaborator.SimulatorScenarioId.ToString())}";
            }
            else
            {
                url = $"{url}/dnd/simulatorscenarios/edit/{_hasher.Encode(simScenarioCollaborator.SimulatorScenarioId.ToString())}";
            }

            var notificationWithContent = _notificationFactory.CreateSimulatorScenarioCollaborationNotification(destination, order, setting, simScenarioCollaborator, url);

            return await sendNotificationAsync(notificationWithContent);
        }

        protected async Task<bool> sendNotificationAsync(INotification notification)
        {
            try
            {
                _notifier = _notifierFactory.GetNotifier(notification);
                return await _notifier.NotifyAsync(notification);
            }
            catch (Exception e)
            {
                // TODO Log
                var ex = e;
                return false;
            }
        }

        protected async Task<ClientSettings_Notification> GetClientSettingNotificationByName(string name, QTDContext qtdContext)
        {
            var notifications = await qtdContext.ClientSettings_Notifications
                .Include("CustomSettings")
                .Include("AvailableCustomSettings")
                .Where(r => r.Name == name)
                .ToListAsync();

            var notification = notifications.First();

            var stepsData = await qtdContext.ClientSettings_Notifications
                .Include("Steps")
                .Include("Steps.CustomSettings")
                .Include("Steps.AvailableCustomSettings")
                .Where(r => r.Name == name)
                .ToListAsync();
            var recepients = await qtdContext.ClientSettings_Notifications
                .Include("Steps")
                .Include("Steps.Recipients")
                .Where(r => r.Name == name)
                .ToListAsync();

            var steps = stepsData.Where(r => r.Id == notification.Id).First().Steps.ToList();

            foreach (var step in steps)
            {
                step.Recipients = recepients.SelectMany(r => r.Steps).Where(r => r.Id == step.Id).First()?.Recipients ?? new List<ClientSettings_Notification_Step_Recipient>();
            }
            notification.Steps = steps;
            return notification;
        }

        public async Task<bool> SendPublicClassScheduleRequestNotification(int id, PublicClassScheduleRequestNotification publicClassScheduleRequestNotification, int order, int? toPersonId, QTDContext qtdContext)
        {
            var defaultTimeZone = (await qtdContext.ClientSettings_GeneralSettings.ToListAsync()).LastOrDefault()?.DefaultTimeZone;

            var setting = await GetClientSettingNotificationByName("Public Class Schedule Request", qtdContext);
            var publicClassScheduleRequest = await qtdContext.PublicClassScheduleRequests.Include("ClassSchedule")
                .FirstOrDefaultAsync(r => r.Id == publicClassScheduleRequestNotification.PublicClassScheduleRequestNotification_PublicClassScheduleRequestId);

            if (setting == null || publicClassScheduleRequest == null)
                return false;
            List<string> destination = new List<string>();

            var qTDUser = await qtdContext.QTDUsers.Where(x => x.Active && x.PersonId == toPersonId).Include("Person").FirstOrDefaultAsync();

            if (qTDUser != null)
            {
                destination.Add(qTDUser.Person.Username);
            }

            if (toPersonId == null) 
            {
                destination.Add(publicClassScheduleRequestNotification.OthersEmailAddresses);
            }

            destination = destination.Where(email => !string.IsNullOrWhiteSpace(email)).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
            var notification = _notificationFactory.CreatePublicClassScheduleRequestNotification(destination, order, setting, publicClassScheduleRequest, defaultTimeZone);

            return await sendNotificationAsync(notification);
        }

        public async Task<bool> SendPublicClassScheduleRequestAcceptedNotification(int publicClassScheduleRequestId, int order, int? toPersonId, QTDContext qtdContext,  string url)
        {
            var defaultTimeZone = (await qtdContext.ClientSettings_GeneralSettings.ToListAsync()).LastOrDefault()?.DefaultTimeZone;

            var setting = await GetClientSettingNotificationByName("Public Class Schedule Request Accepted", qtdContext);
            var publicClassScheduleRequest = await qtdContext.PublicClassScheduleRequests
                .Include("ClassSchedule")
                .Include("ClassSchedule.ILA")
                .FirstOrDefaultAsync(r => r.Id == publicClassScheduleRequestId);

            url = url + "/auth/login";

            if (setting == null || publicClassScheduleRequest == null)
                return false;
            var person = await qtdContext.Persons.Where(x => x.Id == toPersonId).FirstOrDefaultAsync();
            List<string> destination = new List<string>();
            if (person != null)
            {
                destination.Add(person.Username);
            }
            
            var notification = _notificationFactory.CreatePublicClassScheduleRequestAcceptedNotification(destination, order, setting, publicClassScheduleRequest, defaultTimeZone, url, publicClassScheduleRequest.ClassSchedule.ILA.NickName);

            return await sendNotificationAsync(notification);
        }
    }
}
