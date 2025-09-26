using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ClassSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Authorization.Operations.Core;
using IClassScheduleDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleService;
using QTD2.Infrastructure.ExtensionMethods;
using QTD2.Infrastructure.Model.ClassSchedule_Employee;
using IClassScheduleEmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleEmployeeService;
using Microsoft.EntityFrameworkCore;
using IILADomainService = QTD2.Domain.Interfaces.Service.Core.IILAService;
using QTD2.Infrastructure.Model.ClassSchedule_Roster;
using QTD2.Infrastructure.Model.ClassSchedule_StudentEvaluation_Link;
using ITestTypeService = QTD2.Domain.Interfaces.Service.Core.ITestTypeService;
using IClassSchedule_RosterDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_RosterService;
using IILA_StudentEvalDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_StudentEvaluation_LinkService;
using IStudentEvalWithoutEmpDomainService = QTD2.Domain.Interfaces.Service.Core.IStudentEvaluationWithoutEmpService;
using IEmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeService;
using IClassSchedule_Evaluation_RosterDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_Evaluation_RosterService;
using IClassSchedule_Eval_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_StudentEvaluations_LinkService;
using IPersonDomainService = QTD2.Domain.Interfaces.Service.Core.IPersonService;
using IInstructorDomainService = QTD2.Domain.Interfaces.Service.Core.IInstructor_Service;
using ILocationDomainService = QTD2.Domain.Interfaces.Service.Core.ILocation_Service;
using IProviderDomainService = QTD2.Domain.Interfaces.Service.Core.IProviderService;
using QTD2.Infrastructure.Model.EmpSelfRegistration;
using IClassSchedule_SelfRegService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_SelfRegistrationOptionsService;
using IEvaluationReleaseEMPSettingDomainService = QTD2.Domain.Interfaces.Service.Core.IEvaluationReleaseEMPSettingsService;

using ISelfRegOptionsDomainService = QTD2.Domain.Interfaces.Service.Core.ISelfRegistrationOptionsService;
using IEmpPositionDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeePositionService;
using IPositionDomainService = QTD2.Domain.Interfaces.Service.Core.IPositionService;
using ICS_RosterStatusDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_Roster_StatusesService;
using ITestDomainService = QTD2.Domain.Interfaces.Service.Core.ITestService;
using IClassTestReleaseEmpSettingDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_TestReleaseEMPSettingsService;
using ITestReleaseEMPSetting_Retake_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITestReleaseEMPSetting_Retake_LinkService;
using IEmployeeOrganizationDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeOrganizationService;
using IOrganizationService = QTD2.Domain.Interfaces.Service.Core.IOrganizationService;
using Position = QTD2.Domain.Entities.Core.Position;
using ITQReleaseEmpSettingsDomainService = QTD2.Domain.Interfaces.Service.Core.ITQILAEmpSettingService;
using IEvaluationReleaseEmpSettingsDomainService = QTD2.Domain.Interfaces.Service.Core.IEvaluationReleaseEMPSettingsService;
using IClientSettings_NotificationServiceDomainService = QTD2.Domain.Interfaces.Service.Core.IClientSettings_NotificationService;
using IEnablingObjectiveDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjectiveService;
using ITaskDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskService;
using IILACertificationLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILACertificationLinkService;
using IMetaILADomainService = QTD2.Domain.Interfaces.Service.Core.IMetaILAService;
using ITaskQualificationDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskQualificationService;
using System.Dynamic;
using DocumentFormat.OpenXml.Office2010.Excel;
using QTD2.Infrastructure.Rustici.EngineApi;
using DocumentFormat.OpenXml.VariantTypes;

using Microsoft.Extensions.Options;
using QTD2.Infrastructure.Scorm.Settings;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Domain.Validation;
using QTD2.Domain;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.Procedure;
using QTD2.Infrastructure.Model.Task;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.PublicILA;
using QTD2.Infrastructure.Model.ILA_Certification_Link;
using QTD2.Infrastructure.Model.Certification;
using System.Net;

namespace QTD2.Application.Services.Shared
{
    public class ClassScheduleService : IClassScheduleService
    {

        private readonly IEvaluationReleaseEMPSettingDomainService _eval_EmpSettingsService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<ClassScheduleService> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly ClassSchedule _classSchedule;
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeDomainService _emp_domainService;
        private readonly IClassScheduleEmployeeDomainService _classScheduleEmployeeLinkService;
        private readonly ClassSchedule_Employee _classScheduleEmployee;
        private readonly IILADomainService _ilaService;
        private readonly ITestTypeService _testtypeService;
        private readonly IClassSchedule_RosterDomainService _classRosterService;
        private readonly IILA_StudentEvalDomainService _ila_eval_linkService;
        private readonly IStudentEvalWithoutEmpDomainService _eval_withoutEmpService;
        private readonly IClassSchedule_Evaluation_RosterDomainService _cs_eval_rosterService;
        private readonly IClassSchedule_Eval_LinkDomainService _cs_eval_linkService;
        private readonly IPersonDomainService _personService;
        private readonly IInstructorDomainService _instructorDomainService;
        private readonly ILocationDomainService _locationDomainService;
        private readonly IProviderDomainService _providerDomainService;
        //private readonly ISelfRegOptionsDomainService _selfRegOptionService;
        private readonly IEmpPositionDomainService _emp_posService;
        private readonly IPositionDomainService _positionService;
        private readonly ICS_RosterStatusDomainService _rosterStatusService;
        private readonly ITestDomainService _testService;
        private readonly ITestReleaseEMPSetting_Retake_LinkDomainService _testReleaseSetting_retake_linkService;
        private readonly IClassScheduleDomainService _classScheduleService;
        private readonly QTD2.Infrastructure.HttpClients.ScormEngineService _scormEngineService;
        private readonly QTD2.Domain.Interfaces.Service.Core.IScormUploadService _scormUploadService;
        private readonly QTD2.Infrastructure.Database.Interfaces.IInstanceFetcher _instanceFetcher;
        private readonly QTD2.Domain.Interfaces.Service.Core.ICBT_ScormRegistrationService _cbt_ScormRegistrationService;
        private readonly ScormServerSettings _scormServerSettings;
        private readonly IClassSchedule_SelfRegService _classSchedule_SelfRegService;

        private readonly IEmployeeOrganizationDomainService _emp_orgService;
        private readonly IOrganizationService _orgService;
        private readonly ITQReleaseEmpSettingsDomainService _tqreleaseEmpSettings;
        private readonly IEvaluationReleaseEmpSettingsDomainService _evaluationReleaseSettings;
        private readonly IClientSettings_NotificationServiceDomainService _clientNotificationService;
        private readonly IClassTestReleaseEmpSettingDomainService _classTestReleaseEmpSettingDomainService;
        private readonly IEnablingObjectiveDomainService _enablingObjectiveDomainService;
        private readonly ITaskDomainService _taskDomainService;
        private readonly IILACertificationLinkDomainService _ilaCertificationLinkService;
        private readonly IMetaILADomainService _metaILADomainService;
        private readonly ITaskQualificationDomainService _taskQualificationDomainService;


        public ClassScheduleService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<ClassScheduleService> localizer, IClassScheduleDomainService classScheduleService, UserManager<AppUser> userManager, IEmployeeService employeeService, IClassScheduleEmployeeDomainService classScheduleEmployeeLinkService, IILADomainService ilaService, ITestTypeService testtypeService, IClassSchedule_RosterDomainService classRosterService, IILA_StudentEvalDomainService ila_eval_linkService, IStudentEvalWithoutEmpDomainService eval_withoutEmpService, IEmployeeDomainService emp_domainService, IClassSchedule_Evaluation_RosterDomainService cs_eval_rosterService, IClassSchedule_Eval_LinkDomainService cs_eval_linkService, IPersonDomainService personService, IInstructorDomainService instructorDomainService, IProviderDomainService providerDomainService, ILocationDomainService locationDomainService, ISelfRegOptionsDomainService selfRegOptionService, IEmpPositionDomainService emp_posService,
            IPositionDomainService positionService, ICS_RosterStatusDomainService rosterStatusService, ITestDomainService testService, ITestReleaseEMPSetting_Retake_LinkDomainService testReleaseSetting_retake_linkService,
            IEmployeeOrganizationDomainService emp_orgService, IOrganizationService orgService, ITQReleaseEmpSettingsDomainService tqreleaseEmpSettings,
            IEvaluationReleaseEmpSettingsDomainService evaluationReleaseSettings, IClientSettings_NotificationServiceDomainService clientNotificationService,
            QTD2.Infrastructure.HttpClients.ScormEngineService scormEngineService, QTD2.Domain.Interfaces.Service.Core.IScormUploadService scormUploadService,
            QTD2.Infrastructure.Database.Interfaces.IInstanceFetcher instanceFetcher, QTD2.Domain.Interfaces.Service.Core.ICBT_ScormRegistrationService cbt_ScormRegistrationService,
            IOptions<ScormServerSettings> scormServerSettingsOptions, IClassSchedule_SelfRegService classSchedule_SelfRegService,
            IClassTestReleaseEmpSettingDomainService classTestReleaseEmpSettingDomainService, 
            IEnablingObjectiveDomainService enablingObjectiveDomainService, 
            ITaskDomainService taskDomainService, 
            IMetaILADomainService metaILADomainService, 
            IILACertificationLinkDomainService iLACertificationLinkDomainService, ITaskQualificationDomainService taskQualificationDomainService)

        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _classScheduleService = classScheduleService;
            _userManager = userManager;
            _classSchedule = new ClassSchedule();
            _employeeService = employeeService;
            _classScheduleEmployeeLinkService = classScheduleEmployeeLinkService;
            _classScheduleEmployee = new ClassSchedule_Employee();
            _ilaService = ilaService;
            _classSchedule_SelfRegService = classSchedule_SelfRegService;
            _testtypeService = testtypeService;
            _classRosterService = classRosterService;
            _ila_eval_linkService = ila_eval_linkService;
            _eval_withoutEmpService = eval_withoutEmpService;
            _emp_domainService = emp_domainService;
            _cs_eval_rosterService = cs_eval_rosterService;
            _cs_eval_linkService = cs_eval_linkService;
            _personService = personService;
            _providerDomainService = providerDomainService;
            _instructorDomainService = instructorDomainService;
            _locationDomainService = locationDomainService;
            // _selfRegOptionService = selfRegOptionService;
            _emp_posService = emp_posService;
            _positionService = positionService;
            _rosterStatusService = rosterStatusService;
            _testService = testService;
            _testReleaseSetting_retake_linkService = testReleaseSetting_retake_linkService;
            _scormEngineService = scormEngineService;
            _scormUploadService = scormUploadService;
            _instanceFetcher = instanceFetcher;
            _cbt_ScormRegistrationService = cbt_ScormRegistrationService;
            _scormServerSettings = scormServerSettingsOptions.Value;
            _emp_orgService = emp_orgService;
            _orgService = orgService;
            _tqreleaseEmpSettings = tqreleaseEmpSettings;
            _evaluationReleaseSettings = evaluationReleaseSettings;
            _clientNotificationService = clientNotificationService;
            _classTestReleaseEmpSettingDomainService = classTestReleaseEmpSettingDomainService;
            _enablingObjectiveDomainService = enablingObjectiveDomainService;
            _taskDomainService = taskDomainService;
            _ilaCertificationLinkService = iLACertificationLinkDomainService;
            _metaILADomainService = metaILADomainService;
            _taskQualificationDomainService = taskQualificationDomainService;
        }

        public async Task<ClassSchedule> CreateAsync(ClassScheduleCreateOptions options)
        {
            //var list = new List<string>();
            //foreach(var item in options.RecurringOptions)
            //{
            //    var startDate = TimeZone.CurrentTimeZone.ToLocalTime(item.StartDate);
            //    var endDate = TimeZone.CurrentTimeZone.ToLocalTime(item.EndDate);
            //    list.Add(startDate.ToString() + "-" + endDate.ToString());
            //}
            //var getlist = list.ToList();

            //DateTime localstartDateTime = new DateTime(options.StartDateTime.Year,options.StartDateTime.Month,options.StartDateTime.Day,options.StartDateTime.Hour,options.StartDateTime.Minute,options.StartDateTime.Second);

            //DateTime localendDateTime = new DateTime(options.EndDateTime.Year,options.EndDateTime.Month,options.EndDateTime.Day,options.EndDateTime.Hour,options.EndDateTime.Minute,options.EndDateTime.Second);

            //DateTime utcstartDateTime = localstartDateTime.ToUniversalTime();
            //DateTime utcendDateTime = localendDateTime.ToUniversalTime();

            var obj = (await _classScheduleService.FindAsync(x => x.ProviderID == options.ProviderID && x.LocationId == options.LocationId && x.InstructorId == options.InstructorId && x.StartDateTime == options.StartDateTime && x.EndDateTime == options.EndDateTime && x.ILAID == options.ILAID)).FirstOrDefault();
            if (obj == null)
            {

                obj = new ClassSchedule(null, options.ProviderID ?? 0, options.ILAID ?? 0, options.StartDateTime, options.EndDateTime, options.InstructorId, options.LocationId, options.SpecialInstructions, options.WebinarLink, options.Notes, options.ClassSize, options.IsStartAndEndTimeEmpty, options.IsPubliclyAvailable);
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["RecordAlreadyExists"].Value);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                if (options.RecurringOptions.Count > 0)
                {
                    obj.IsRecurring = true;
                }
                else
                {
                    obj.IsRecurring = false;
                }
                var validationResult = await _classScheduleService.AddAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    if (obj.IsRecurring)
                    {
                        obj.RecurrenceId = obj.Id;
                        await _classScheduleService.UpdateAsync(obj);
                        await CreateRecurrenceAsync(options, obj);
                    }
                    obj.ILA = await _ilaService.FindQuery(x => x.Id == obj.ILAID).FirstOrDefaultAsync();
                    return obj;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async System.Threading.Tasks.Task CreateRecurrenceAsync(ClassScheduleCreateOptions originalOptions, ClassSchedule parentClassSchedule)
        {
            foreach (var dates in originalOptions.RecurringOptions)
            {
                var options = originalOptions;
                options.StartDateTime = dates.StartDate;
                options.EndDateTime = dates.EndDate;
                options.IsPubliclyAvailable = dates.IsPubliclyAvailable;


                var obj = new ClassSchedule(parentClassSchedule.Id, options.ProviderID ?? 0, options.ILAID ?? 0, options.StartDateTime, options.EndDateTime, options.InstructorId, options.LocationId, options.SpecialInstructions, options.WebinarLink, options.Notes, options.ClassSize, false, originalOptions.IsPubliclyAvailable);

                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Create);
                if (result.Succeeded)
                {
                    obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    obj.CreatedDate = DateTime.Now;
                    var validationResult = await _classScheduleService.AddAsync(obj);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {

                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                }
            }
        }

        public async Task<List<ClassSchedule>> GetRecurrencesAsync(int classId, bool includeCurrentClass)
        {
            var clRecs = new List<ClassSchedule>();
            if (includeCurrentClass)
            {
                clRecs = await _classScheduleService.FindQueryWithIncludeAsync(x => x.RecurrenceId == classId, new string[] { "ClassSchedule_Employee.Employee.Person", "ClassSchedule_Employee.Employee.EmployeePositions.Position", "ClassSchedule_Employee.Employee.EmployeeOrganizations.Organization" }).ToListAsync();
            }
            else
            {
                clRecs = await _classScheduleService.FindQueryWithIncludeAsync(x => x.RecurrenceId == classId && !x.IsRecurring, new string[] { "ClassSchedule_Employee.Employee.Person", "ClassSchedule_Employee.Employee.EmployeePositions.Position", "ClassSchedule_Employee.Employee.EmployeeOrganizations.Organization" }).ToListAsync();
            }
            var original = await _classScheduleService.FindQueryWithIncludeAsync(x => x.Id == classId, new string[] { "ClassSchedule_Employee.Employee.Person", "ClassSchedule_Employee.Employee.EmployeePositions.Position", "ClassSchedule_Employee.Employee.EmployeeOrganizations.Organization" }).FirstOrDefaultAsync();
            //original.StartDateTime = TimeZone.CurrentTimeZone.ToLocalTime(original.StartDateTime);
            //original.EndDateTime = TimeZone.CurrentTimeZone.ToLocalTime(original.EndDateTime);
            if (!original.IsRecurring)
            {
                clRecs.Add(original);
            }
            clRecs = clRecs.Where(w => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, w, ClassScheduleOperations.Read).Result.Succeeded).ToList();

            clRecs.ForEach(r =>
            {
                r.ClearDomainEvents();
                foreach (var cse in r.ClassSchedule_Employee)
                {
                    cse.ClassSchedule = null;
                    cse.Employee.ClassSchedule_Employee = null;
                    cse.Employee.Person.Employee = null;
                    cse.ClearDomainEvents();
                    cse.Employee.Person.Image = null;
                    foreach (var csep in cse.Employee.EmployeePositions)
                    {
                        csep.Position.EmployeePositions = null;
                    }
                    foreach (var cseo in cse.Employee.EmployeeOrganizations)
                    {
                        cseo.Organization.EmployeeOrganizations = null;
                    }
                }
            });


            return clRecs;
        }

        public async System.Threading.Tasks.Task<ClassSchedule> DeleteAsync(int id)
        {
            var obj = await _classScheduleService.FindQueryWithIncludeAsync(x => x.Id == id, new string[] { "ILA" }).FirstOrDefaultAsync();

            //if(obj.ClassSchedule_Recurrence == null)
            //{
            //    //it means it is a main class

            //}
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Delete();

                var validationResult = await _classScheduleService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
            return obj;
        }

        public async Task<List<ClassScheduleVM>> GetAsync()
        {
            var model = new List<ClassScheduleVM>();
            var obj_list = await _classScheduleService.AllAsync();


            if (obj_list != null && obj_list.Count() > 0)
            {
                foreach (var item in obj_list)
                {
                    var locationObject = _locationDomainService.GetAsync(item.LocationId ?? 0).Result;
                    var instructor = await _instructorDomainService.GetAsync(item.InstructorId ?? 0);
                    var provider = await _providerDomainService.GetAsync(item.ProviderID ?? 0);
                    var ila = await _ilaService.GetAsync(item.ILAID ?? 0);
                    model.Add(new ClassScheduleVM()
                    {
                        Id = item.Id,
                        ProviderID = item.ProviderID,
                        LocationId = item.LocationId,
                        InstructorId = item.InstructorId,
                        ILAID = item.ILAID,
                        StartDateTime = item.StartDateTime,
                        EndDateTime = item.EndDateTime,
                        SpecialInstructions = item.SpecialInstructions,
                        Notes = item.Notes,
                        WebinarLink = item.WebinarLink,
                        IsRecurring = item.IsRecurring,
                        RecurrenceId = item.RecurrenceId,
                        Instructor = instructor != null ? instructor.InstructorName : "N/A",
                        Provider = provider != null ? provider.Name : "N/A",
                        Location = locationObject != null ? locationObject.LocName + " " + locationObject.LocAddress + " " + locationObject.LocZipCode : "N/A",
                        ILA = ila != null ? ila.Name : "N/A",


                    });

                }
            }
            //obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded);
            return model.ToList();
        }

        public async Task<List<ClassScheduleVM>> GetByILAIdAsync(int ilaId)
        {
            var model = new List<ClassScheduleVM>();
            var obj_list = _classScheduleService.FindQuery(x => x.ILAID == ilaId).ToList();


            if (obj_list != null && obj_list.Count() > 0)
            {
                foreach (var item in obj_list)
                {
                    var locationObject = _locationDomainService.GetAsync(item.LocationId ?? 0).Result;
                    var instructor = await _instructorDomainService.GetAsync(item.InstructorId ?? 0);
                    var provider = await _providerDomainService.GetAsync(item.ProviderID ?? 0);
                    var ila = await _ilaService.GetAsync(item.ILAID ?? 0);
                    model.Add(new ClassScheduleVM()
                    {
                        Id = item.Id,
                        ProviderID = item.ProviderID,
                        LocationId = item.LocationId,
                        InstructorId = item.InstructorId,
                        ILAID = item.ILAID,
                        StartDateTime = item.StartDateTime,
                        EndDateTime = item.EndDateTime,
                        SpecialInstructions = item.SpecialInstructions,
                        Notes = item.Notes,
                        WebinarLink = item.WebinarLink,
                        IsRecurring = item.IsRecurring,
                        RecurrenceId = item.RecurrenceId,
                        Instructor = instructor != null ? instructor.InstructorName : "N/A",
                        Provider = provider != null ? provider.Name : "N/A",
                        Location = locationObject != null ? locationObject.LocName + " " + locationObject.LocAddress + " " + locationObject.LocZipCode : "N/A",
                        ILA = ila != null ? ila.Name : "N/A",


                    });

                }
            }
            //obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded);
            return model.ToList();
        }

        public async Task<List<ClassScheduleVM>> GetByStartDateAndEndDateAsync(DateTime startDate, DateTime endDate)
        {
            var model = new List<ClassScheduleVM>();

            var obj_list = await _classScheduleService.FindWithIncludeAsync(
                    x => !x.ILA.Deleted && (x.StartDateTime.Date > startDate.Date ? x.StartDateTime.Date : startDate) <= (x.EndDateTime.Date < endDate.Date ? x.EndDateTime.Date : endDate.Date),
                    new string[] { "Location", "Instructor", "ILA.Provider", "ILA.ILA_Topic_Links" });

            foreach (var item in obj_list)
            {
                var classScheduleRoster = await _classRosterService.FindQuery(x => x.ClassScheduleId == item.Id).ToListAsync();
                var recordCounts = classScheduleRoster == null ? 0 : classScheduleRoster.Where(r => r.Score.HasValue || !String.IsNullOrEmpty(r.Grade)).Count();

                model.Add(new ClassScheduleVM()
                {
                    Id = item.Id,
                    ProviderID = item.ProviderID,
                    LocationId = item.LocationId,
                    InstructorId = item.InstructorId,
                    ILAID = item.ILAID,
                    StartDateTime = item.StartDateTime,
                    EndDateTime = item.EndDateTime,
                    SpecialInstructions = item.SpecialInstructions,
                    Notes = item.Notes,
                    WebinarLink = item.WebinarLink,
                    IsRecurring = item.IsRecurring,
                    RecurrenceId = item.RecurrenceId,
                    Instructor = item.Instructor?.InstructorName,
                    Provider = item.ILA.Provider.Name,
                    Location = item.Location != null ? item.Location.LocName + " " + item.Location.LocAddress + " " + item.Location.LocZipCode : "N/A",
                    ILA = item.ILA.Name,
                    TopicIds = item.ILA.ILA_Topic_Links.Select(r => r.ILATopicId),
                    //TopicLinks = new List<ILA_Topic_Link>(), // item.ILA.ILA_Topic_Links.ToList(),
                    CanDelete = recordCounts == 0
                });
            }

            return model.ToList();
        }
        public async Task<ClassScheduleDetailVM> GetAsync(int id)
        {
            var obj = await _classScheduleService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (obj != null)
            {
                //string easternZoneId = "Eastern Standard Time";
                //obj.StartDateTime = TimeZone.CurrentTimeZone.ToLocalTime(obj.StartDateTime);
                //obj.EndDateTime = TimeZone.CurrentTimeZone.ToLocalTime(obj.EndDateTime);
                obj.Instructor = await _instructorDomainService.FindQuery(x => x.Id == obj.InstructorId).FirstOrDefaultAsync();
                obj.Location = await _locationDomainService.FindQuery(x => x.Id == obj.LocationId).FirstOrDefaultAsync();
                obj.ILA = (await _ilaService.FindWithIncludeAsync(x => x.Id == obj.ILAID, new string[] { "Provider" })).FirstOrDefault();
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read);
                if (result.Succeeded)
                {
                    return MapClassScheduleToClassScheduleDetailVM(obj);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }
        }

        public async Task<List<ClassScheduleData>> GetClassSchedulesByILA(int ilaId)
        {
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, new ClassSchedule(), ClassScheduleOperations.Read);
            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }




            List<ClassScheduleData> toReturnList = new List<ClassScheduleData>();

            var list = await _classScheduleService.FindQueryWithIncludeAsync(x => x.ILAID == ilaId, new string[] { nameof(_classSchedule.Instructor), nameof(_classSchedule.Location), nameof(_classSchedule.ILA), nameof(_classSchedule.ClassSchedule_StudentEvaluations_Links), "ClassSchedule_Employee" }, true).ToListAsync();
            foreach (var x in list)
            {
                bool canDelete = true;
                var classScheduleRoster = await _classRosterService.FindQuery(y => y.ClassScheduleId == x.Id).ToListAsync();
                var recordCounts = 0;
                var employeeCompletedCount = 0;
                if (classScheduleRoster != null)
                {
                    foreach (var cls in classScheduleRoster)
                    {
                        if (cls.Score != null || cls.Grade != null)
                        {

                            recordCounts++;
                        }
                    }

                }
                if (x.ClassSchedule_Employee != null)
                {
                    foreach (var clsEmp in x.ClassSchedule_Employee)
                    {
                        if (clsEmp.IsComplete)
                        {
                            employeeCompletedCount++;
                        }
                    }
                }
                if (recordCounts > 0 || employeeCompletedCount > 0)
                {
                    canDelete = false;
                }

                var record = new ClassScheduleData
                {
                    Id = x.Id,
                    EmployeeCount = x.ClassSchedule_Employee.Count(x => x.IsEnrolled),
                    ClassSchedule_StudentEvaluations_Links = x.ClassSchedule_StudentEvaluations_Links,
                    ILA = x.ILA,
                    Instructor = x.Instructor,
                    Location = x.Location,
                    Notes = x.Notes,
                    WebinarLink = x.WebinarLink,
                    SpecialInstructions = x.SpecialInstructions,
                    LocationId = x.LocationId,
                    InstructorId = x.InstructorId,
                    EndDateTime = x.EndDateTime,
                    StartDateTime = x.StartDateTime,
                    ILAID = x.ILAID,
                    ProviderID = x.ProviderID,
                    IsRecurring = x.IsRecurring,
                    RecurrenceId = x.RecurrenceId,
                    Active = x.Active,
                    Deleted = x.Deleted,
                    CanDelete = canDelete,
                    IsStartAndEndTimeEmpty = x.IsStartAndEndTimeEmpty

                };

                toReturnList.Add(record);
            }
            if (toReturnList != null)
            {
                //foreach (var item in list)
                //{
                //    string easternZoneId = "Eastern Standard Time";
                //    item.StartDateTime = TimeZone.CurrentTimeZone.ToLocalTime(item.StartDateTime);
                //    item.EndDateTime = TimeZone.CurrentTimeZone.ToLocalTime(item.EndDateTime);
                //}
                //list = list.Where(x => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, x, ClassScheduleOperations.Read).Result.Succeeded).ToList();
                return toReturnList;

            }
            else
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }
        }



        public async Task<ClassSchedule> UpdateAsync(int id, ClassScheduleCreateOptions options)
        {
            var obj = await _classScheduleService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }

            var modifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;

            var isRecurring = true;
            int? recurrenceId = null;

            if (options.RecurringOptions.Count > 0)
            {
                isRecurring = true;
                recurrenceId = obj.Id;
            }
            else
            {
                isRecurring = false;
            }

            obj.Update(
                options.ProviderID, 
                options.ILAID, 
                options.StartDateTime, 
                options.EndDateTime,
                options.InstructorId,
                options.LocationId,
                options.ClassSize,
                options.SpecialInstructions,
                options.WebinarLink,
                options.IsStartAndEndTimeEmpty,
                modifiedBy,
                DateTime.Now,
                options.IsPubliclyAvailable,
                isRecurring,
                recurrenceId);

            var validationResult = await _classScheduleService.UpdateAsync(obj);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }

            if (obj.IsRecurring)
            {
                //Get all previous recurrence
                var clRecs = (await _classScheduleService.FindAsync(x => x.RecurrenceId == id && !x.IsRecurring)).ToList();
                if (clRecs != null && clRecs.Count > 0)
                {
                    foreach (var item in clRecs)
                    {
                        item.Update(
                            options.ProviderID,
                            options.ILAID,
                            item.StartDateTime,
                            item.EndDateTime,
                            options.InstructorId,
                            options.LocationId,
                            options.ClassSize,
                            options.SpecialInstructions,
                            options.WebinarLink,
                            item.IsStartAndEndTimeEmpty,
                            modifiedBy,
                            DateTime.Now,
                            options.IsPubliclyAvailable, 
                            false,
                            recurrenceId);

                    }
                    await _classScheduleService.BulkUpdateAsync(clRecs);
                }
                else
                {
                    await CreateRecurrenceAsync(options, obj);
                }
            }
            obj.ILA = await _ilaService.FindQuery(x => x.Id == obj.ILAID).FirstOrDefaultAsync();
            return obj;
        }


        public async Task<ClassSchedule> LinkEmployee(int classScheduleId, ClassSchedule_EmployeeCreateOptions options)
        {
            var classSchedule = await _classScheduleService.GetWithIncludeAsync(classScheduleId, new string[] { nameof(_classSchedule.ClassSchedule_Employee), nameof(_classSchedule.ClassSchedule_SelfRegistrationOption) });

            if (classSchedule.ClassSchedule_Employee.Where(r => r.IsEnrolled).Count() + options.employeeIds.Count() > classSchedule.ClassSize)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: "You are trying to enroll more employees than the class allows.");
            }

            foreach (var id in options.employeeIds)
            {
                var employee = await _emp_domainService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();

                var classScheduleResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, classSchedule, ClassScheduleOperations.Update);
                var empResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, employee, EmployeeOperations.Read);
                if (classScheduleResult.Succeeded && empResult.Succeeded)
                {
                    var classScheduleEmployee = classSchedule.LinkEmployee(employee, true);

                    var validationResult = await _classScheduleService.UpdateAsync(classSchedule);

                    classScheduleEmployee.EnrollStudent(options.PlannedDate);
                    await _classScheduleEmployeeLinkService.UpdateAsync(classScheduleEmployee);

                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return classSchedule;
        }

        public async System.Threading.Tasks.Task UnlinkEmployee(int classScheduleId, int[] empIDs)
        {
            var classSchedule = await _classScheduleService.GetWithIncludeAsync(classScheduleId, new string[] { nameof(_classSchedule.ClassSchedule_Employee) });
            foreach (var id in empIDs)
            {
                var employee = await _emp_domainService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();

                var classScheduleEmployee = await _classScheduleEmployeeLinkService.GetByEmployeeIdAndClassScheduleIdAsync(employee.Id, classSchedule.Id);

                var classScheduleResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, classSchedule, ClassScheduleOperations.Update);
                var employeeResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, employee, EmployeeOperations.Read);
                if (classScheduleResult.Succeeded && employeeResult.Succeeded)
                {
                    classScheduleEmployee.Delete();
                    await RemoveRosterData(classScheduleId, id);

                    classScheduleEmployee.UnenrollStudent();
                    await _classScheduleEmployeeLinkService.UpdateAsync(classScheduleEmployee);

                    var validationResult = await _classScheduleService.UpdateAsync(classSchedule);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async System.Threading.Tasks.Task RemoveRosterData(int classId, int empId)
        {
            var rosters = await _classRosterService.FindQuery(x => x.ClassScheduleId == classId && x.EmpId == empId).ToListAsync();
            foreach (var roster in rosters)
            {
                roster.Delete();
                var validationResult = await _classRosterService.UpdateAsync(roster);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                }
            }

            var evalRosters = await _cs_eval_rosterService.FindQuery(x => x.ClassScheduleId == classId && x.EmployeeId == empId).ToListAsync();
            foreach (var evalRoster in evalRosters)
            {
                var auth = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, evalRoster, ClassSchedule_Evaluation_RosterOperations.Delete);
                if (auth.Succeeded)
                {
                    evalRoster.Delete();
                    var validationResult = await _cs_eval_rosterService.UpdateAsync(evalRoster);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                }
            }
        }

        public async Task<List<ClassSchedule>> GetClassSchedulesEmployeeIsLinkedTo(int id)
        {
            var data = await _classScheduleEmployeeLinkService.AllQueryWithInclude(new string[] { nameof(_classScheduleEmployee.ClassSchedule) }).Where(x => x.EmployeeId == id).Select(x => x.ClassSchedule).ToListAsync();
            return data;
        }

        public async Task<List<EmployeesLinkedToSchedule>> GetLinkedEmployees(int id)
        {
            var links = await _classScheduleEmployeeLinkService.FindQuery(x => x.ClassScheduleId == id).ToListAsync();
            //List<Domain.Entities.Core.Employee> empList = new List<Domain.Entities.Core.Employee>();
            //empList.AddRange(links.Select(x => x.Employee));
            List<EmployeesLinkedToSchedule> empWithCount = new List<EmployeesLinkedToSchedule>();
            foreach (var link in links.Where(r => r.IsEnrolled || r.IsWaitlisted || r.IsAwaitingForApproval.GetValueOrDefault()))
            {
                link.Employee = await _emp_domainService.FindQuery(x => x.Id == link.EmployeeId).FirstOrDefaultAsync();
                if (link.Employee != null)
                {
                    link.Employee.Person = await _personService.FindQuery(x => x.Id == link.Employee.PersonId).FirstOrDefaultAsync();
                    link.Employee.EmployeePositions = await _emp_posService.FindQuery(x => x.EmployeeId == link.EmployeeId).ToListAsync();
                    link.Employee.EmployeeOrganizations = await _emp_orgService.FindQuery(x => x.EmployeeId == link.EmployeeId).ToListAsync();
                    link.CBTStatus = await _rosterStatusService.FindQuery(x => x.Id == link.CBTStatusId).FirstOrDefaultAsync();
                    link.PreTestStatus = await _rosterStatusService.FindQuery(x => x.Id == link.PreTestStatusId).FirstOrDefaultAsync();
                    link.TestStatus = await _rosterStatusService.FindQuery(x => x.Id == link.TestStatusId).FirstOrDefaultAsync();
                    link.ReTakeStatus = await _rosterStatusService.FindQuery(x => x.Id == link.RetakeStatusId).FirstOrDefaultAsync();
                    for (int i = 0; i < link.Employee.EmployeePositions.Count; i++)
                    {
                        link.Employee.EmployeePositions.ToList()[i].Position = await _positionService.FindQuery(x => x.Id == link.Employee.EmployeePositions.ToList()[i].PositionId).FirstOrDefaultAsync();
                    }

                    for (int i = 0; i < link.Employee.EmployeeOrganizations.Count; i++)
                    {
                        link.Employee.EmployeeOrganizations.ToList()[i].Organization = await _orgService.FindQuery(x => x.Id == link.Employee.EmployeeOrganizations.ToList()[i].OrganizationId).FirstOrDefaultAsync();
                    }

                    empWithCount.Add(new EmployeesLinkedToSchedule(link.Id,
                    (link.Employee?.Person?.FirstName ?? "") + " " + (link.Employee?.Person?.LastName ?? ""),
                    link.Employee.Person.Username,
                    link.Employee.EmployeePositions.Select(x => x.Position.PositionTitle).FirstOrDefault(),
                    link.Employee.EmployeeOrganizations.Select(x => x.Organization.Name).FirstOrDefault(),
                    link.Employee.EmployeePositions.Select(s => s.PositionId).ToList(),
                    link.Employee.EmployeeOrganizations.Select(s => s.OrganizationId).ToList(),
                    link.Employee.Id, link.Employee.Person.Image,
                    link.FinalScore, link.FinalGrade,
                    link.GradeNotes, link.CBTStatus.Name, link.TestStatus.Name, link.PreTestStatus.Name, link.ReTakeStatus.Name));
                }


            }
            //foreach (var emp in empList)
            //{
            //    empWithCount.Add(new EmployeesLinkedToSchedule(emp.Person.FirstName + " " + emp.Person.LastName, emp.Person.Username, emp.EmployeePositions.Select(x => x.Position.PositionTitle).FirstOrDefault(), emp.EmployeeOrganizations.Select(x => x.Organization.Name).FirstOrDefault(), emp.EmployeePositions.Select(s => s.PositionId).ToList(), emp.EmployeeOrganizations.Select(s => s.OrganizationId).ToList(),emp.Id,emp.Person.Image));
            //empWithCount.Add(new EmployeesLinkedToSchedule(l.Id,l.Employee.Person.FirstName+" "+ l.Employee.Person.LastName,l.Employee.Person.Username, l.Employee.EmployeePositions.Select(x => x.Position.PositionTitle).FirstOrDefault(),l.Employee.EmployeeOrganizations.Select(x=>x.Organization.Name).FirstOrDefault()));
            //}

            return empWithCount;
        }

        public async Task<ScheduleClassesStats> GetStatsAsync()
        {
            var stats = new ScheduleClassesStats();

            var ilaQuery = _ilaService.FindQueryWithIncludeAsync(x => x.Active == true && x.IsPublished == true, new string[] { "ClassSchedules.ClassSchedule_SelfRegistrationOption", "ClassSchedules.ClassSchedule_Employee", }, false);
            var roasterQuery = ilaQuery.SelectMany(x => x.ClassSchedules).SelectMany(x => x.ClassSchedule_Rosters).Where(r => r.ClassSchedule.ClassSchedule_Employee.Any(e => e.EmployeeId == r.EmpId));

            // Need Approval

            stats.NeedingApproval = await ilaQuery.SelectMany(x => x.ClassSchedules.Where(w => w.ClassSchedule_SelfRegistrationOption.MakeAvailableForSelfReg && w.ClassSchedule_SelfRegistrationOption.RequireAdminApproval)).SelectMany(e => e.ClassSchedule_Employee.Where(e => !e.IsDenied && !e.IsEnrolled)).CountAsync();

            //Need Scheduling


            stats.NeedScheduling = await ilaQuery.Where(s => s.Active && !s.ClassSchedules.Any(sc => sc.Active) && s.IsPublished).CountAsync();


            var InPgrogressStatus = await _rosterStatusService.FindQueryAsync(s => s.Name == "In Progress").Result.Select(x => x.Id).FirstOrDefaultAsync();

            //Need Re Release
            stats.NeedReRelease = await roasterQuery.Where(x => x.StatusId == InPgrogressStatus).CountAsync();


            //Waitlist
            stats.Waitlist = await ilaQuery.SelectMany(x => x.ClassSchedules.Where(s => s.Active && s.ClassSchedule_SelfRegistrationOption.EnableWaitlist)).SelectMany(y => y.ClassSchedule_Employee.Where(x => x.IsWaitlisted && !x.IsDenied)).CountAsync();


            stats.RetakeRelease = (await GetRetakeToReleaseAsync()).Count();

            return stats;
        }

        public async Task<List<ToDoILAsVM>> GetRetakeToReleaseAsync()
        {
            List<ToDoILAsVM> toReturndata = new List<ToDoILAsVM>();
            var candidateCses = await _classScheduleEmployeeLinkService.GetClassScheduleEmployeesReadyForRetakeAsync();
            var classScheduleRosters = await _classRosterService.GetByClassScheduleIdListAsync(candidateCses.Select(r => r.ClassScheduleId).ToList());

            foreach (var cse in candidateCses)
            {
                var tests = classScheduleRosters.Where(r => r.EmpId == cse.EmployeeId && r.ClassScheduleId == cse.ClassScheduleId);

                var finalTest = tests.Where(r => r.TestTypeId == 2).FirstOrDefault();
                var retakes = tests.Where(r => r.TestTypeId == 3);

                if (finalTest == null || !finalTest.CompletedDate.HasValue) continue;

                if (finalTest.Grade == "P") continue;

                if (retakes.Where(r => r.Grade == "P" || (r.IsReleased.GetValueOrDefault() && !r.CompletedDate.HasValue)).Count() > 0) continue;

                int retakesCount = retakes.Count();

                if (retakesCount >= cse.ClassSchedule.ClassSchedule_TestReleaseEMPSettings.NumberOfRetakes.GetValueOrDefault()) continue;

                if (retakesCount >= cse.ClassSchedule.ClassSchedule_TestReleaseEMPSettings.ClassSchedule_TestReleaseEMPSetting_RetakeLinks.Count()) continue;

                if (cse.ClassSchedule.ClassSchedule_TestReleaseEMPSettings.ClassSchedule_TestReleaseEMPSetting_RetakeLinks.Count() == 0) continue;

                var ilaVm = toReturndata.Where(r => r.Number == cse.ClassSchedule.ILA.Number).FirstOrDefault();

                if (ilaVm == null)
                {
                    ilaVm = new ToDoILAsVM()
                    {
                        ClassSchedules = new List<ToDoClassSchedulesVM>(),
                        Id = cse.ClassSchedule.ILA.Id,
                        Name = cse.ClassSchedule.ILA.Name,
                        Number = cse.ClassSchedule.ILA.Number,
                        SelfRegistration = cse.ClassSchedule.ClassSchedule_SelfRegistrationOption == null ? false : cse.ClassSchedule.ClassSchedule_SelfRegistrationOption.MakeAvailableForSelfReg,
                        WaitListEnabled = cse.ClassSchedule.ClassSchedule_SelfRegistrationOption == null ? false : cse.ClassSchedule.ClassSchedule_SelfRegistrationOption.EnableWaitlist
                    };

                    toReturndata.Add(ilaVm);
                }

                var classScheduleVm = ilaVm.ClassSchedules.Where(r => r.Id == cse.ClassScheduleId).FirstOrDefault();

                if (classScheduleVm == null)
                {
                    classScheduleVm = new ToDoClassSchedulesVM()
                    {
                        ILAId = cse.ClassSchedule.ILAID.GetValueOrDefault(),
                        ClassSchedule_Employee = new List<ToDoEmployeeVM>(),
                        ClassSize = cse.ClassSchedule.ClassSize.GetValueOrDefault(),
                        EndDateTime = cse.ClassSchedule.EndDateTime,
                        Id = cse.ClassScheduleId,
                        Instructor = cse.ClassSchedule.Instructor?.InstructorName,
                        Location = cse.ClassSchedule.Location?.LocName,
                        StartDateTime = cse.ClassSchedule.StartDateTime,
                        WaitListEnabled = cse.ClassSchedule.ClassSchedule_SelfRegistrationOption == null ? false : cse.ClassSchedule.ClassSchedule_SelfRegistrationOption.EnableWaitlist
                    };

                    ilaVm.ClassSchedules.Add(classScheduleVm);
                }

                var retakeLinks = cse.ClassSchedule.ClassSchedule_TestReleaseEMPSettings.ClassSchedule_TestReleaseEMPSetting_RetakeLinks.Where(r => !r.Deleted).Where(r => r.Active);
                var nextRetake = retakeLinks.ElementAt(retakesCount);

                classScheduleVm.ClassSchedule_Employee.Add(new ToDoEmployeeVM()
                {
                    ILAId = cse.ClassSchedule.ILAID.GetValueOrDefault(),
                    ClassScheduleId = cse.ClassScheduleId,
                    EmployeeId = cse.EmployeeId,
                    FirstName = cse.Employee.Person.FirstName,
                    IsEnrolled = cse.IsEnrolled,
                    IsWaitListed = cse.IsWaitlisted,
                    LastName = cse.Employee.Person.LastName,
                    TestTitle = nextRetake.RetakeTest.TestTitle,
                    TestId = nextRetake.RetakeTest.Id
                });
            }

            return toReturndata;
        }

        public async Task<List<ToDoILAsVM>> GetTestNeedingReReleaseAsync()
        {
            List<ToDoILAsVM> toReturndata = new List<ToDoILAsVM>();

            var InProgressStatus = await _rosterStatusService.FindQueryAsync(s => s.Name == "In Progress").Result.Select(x => x.Id).FirstOrDefaultAsync();

            var needingReleaseQuery = await _ilaService.FindQueryWithIncludeAsync(s => s.Active && s.IsPublished && s.ClassSchedules.Any(cs => cs.ClassSchedule_Rosters.Any(x => x.StatusId == InProgressStatus)), new string[] { "ClassSchedules.ClassSchedule_Rosters.Employee.Person", "ClassSchedules.ClassSchedule_Rosters.Test", "ClassSchedules.ClassSchedule_Employee", "ClassSchedules.Instructor", "ClassSchedules.Location" }, false).ToListAsync();


            var ilas2 = needingReleaseQuery
                .Select(s => new
                ToDoILAsVM
                {
                    Id = s.Id,
                    Name = s.Name,
                    Number = s.Number,
                    ClassSchedules = s.ClassSchedules.Where(cs => cs.ClassSchedule_Rosters.Any(x => x.StatusId == InProgressStatus)).Select(sc => new ToDoClassSchedulesVM
                    {
                        Id = sc.Id,
                        ILAId = sc.ILAID.GetValueOrDefault(),
                        StartDateTime = sc.StartDateTime,
                        ClassSize = sc.ClassSize.GetValueOrDefault(),
                        SeatsTaken = sc.ClassSchedule_Employee.Where(x => !x.IsDenied).Count(),
                        EndDateTime = sc.EndDateTime,
                        Instructor = sc.Instructor?.InstructorName,
                        Location = sc.Location?.LocName,
                        ClassSchedule_Employee = sc.ClassSchedule_Rosters.Where(x => x.StatusId == InProgressStatus).Select(e => new ToDoEmployeeVM
                        {
                            ILAId = sc.ILAID.GetValueOrDefault(),
                            ClassScheduleId = sc.Id,
                            EmployeeId = e.EmpId,
                            FirstName = e.Employee.Person.FirstName,
                            LastName = e.Employee.Person.LastName,
                            TestId = e.TestId,
                            TestTitle = e.Test.TestTitle
                        }).ToList()

                    }).ToList(),

                }).ToList();

            return ilas2;

        }

        public async Task<List<ToDoILAsVM>> GetSelfRegNeedingApprovalAsync()
        {
            var ilaQuery = await _ilaService.FindQueryWithIncludeAsync(x => x.Active == true && x.IsPublished == true, new string[] { "TestReleaseEMPSettings", "ClassSchedules.ClassSchedule_SelfRegistrationOption", "ClassSchedules.ClassSchedule_Employee.Employee.Person", "ClassSchedules.Instructor", "ClassSchedules.Location", "TestReleaseEMPSettings.TestReleaseEMPSetting_Retake_Links.RetakeTest" }, false).ToListAsync();
            foreach (var ila in ilaQuery)
            {
                foreach (var classSchedule in ila.ClassSchedules)
                {
                    classSchedule.ClassSchedule_Employee = classSchedule.ClassSchedule_Employee.Where(e => !e.IsDenied && !e.IsEnrolled).ToList();
                }
                ila.ClassSchedules = ila.ClassSchedules.Where(cs => (cs.ClassSchedule_SelfRegistrationOption?.MakeAvailableForSelfReg).GetValueOrDefault()
                                                                && (cs.ClassSchedule_SelfRegistrationOption?.RequireAdminApproval).GetValueOrDefault()
                                                                && cs.ClassSchedule_Employee.Any()).ToList();

            }
            var needingApprovalQuery = ilaQuery.Where(x => x.ClassSchedules.Any());
            var ilas2 = needingApprovalQuery
                .Select(s => new
                ToDoILAsVM
                {
                    Id = s.Id,
                    Name = s.Name,
                    Number = s.Number,
                    ClassSchedules = s.ClassSchedules.Select(sc => new ToDoClassSchedulesVM
                    {
                        Id = sc.Id,
                        ILAId = sc.ILAID.GetValueOrDefault(),
                        StartDateTime = sc.StartDateTime,
                        ClassSize = sc.ClassSize.GetValueOrDefault(),
                        SeatsTaken = sc.ClassSchedule_Employee.Where(x => !x.IsDenied).Count(),
                        EndDateTime = sc.EndDateTime,
                        Instructor = sc.Instructor?.InstructorName,
                        Location = sc.Location?.LocName,
                        WaitListEnabled = sc.ClassSchedule_SelfRegistrationOption.EnableWaitlist,
                        ClassSchedule_Employee = sc.ClassSchedule_Employee.Select(e => new ToDoEmployeeVM
                        {
                            ILAId = sc.ILAID.GetValueOrDefault(),
                            ClassScheduleId = sc.Id,
                            EmployeeId = e.EmployeeId,
                            FirstName = e.Employee.Person.FirstName,
                            LastName = e.Employee.Person.LastName,
                            IsEnrolled = e.IsEnrolled,
                            IsWaitListed = e.IsWaitlisted

                        }).ToList()

                    }).ToList(),

                }).
                ToList();

            return ilas2;
        }

        public async Task<List<ILASummaryVM>> GetILANeedingToBeScheduled()
        {
            var ilas = await _ilaService.FindQueryWithIncludeAsync(x => x.ClassSchedules.Count == 0, new string[] { "ClassSchedules" }).Select(i => new ILASummaryVM
            {
                Id = i.Id,
                Name = i.Name,
                Number = i.Number
            }).ToListAsync();
            return ilas;
        }

        public async Task<List<ToDoILAsVM>> GetWaitlistedDataAsync()
        {
            var classScheduleEmployees = await _classScheduleEmployeeLinkService.GetWaitlistedAsync();

            List<ToDoILAsVM> model = new List<ToDoILAsVM>();

            foreach (var ila in classScheduleEmployees.Select(r => r.ClassSchedule.ILA).Distinct())
            {
                var item = new ToDoILAsVM()
                {
                    ClassSchedules = new List<ToDoClassSchedulesVM>(),
                    Id = ila.Id,
                    Name = ila.Name,
                    Number = ila.Number
                };

                foreach (var classSchedule in classScheduleEmployees.Select(r => r.ClassSchedule).Where(r => r.ILAID == ila.Id).Distinct())
                {
                    var classScheduleVm = new ToDoClassSchedulesVM()
                    {
                        ILAId = ila.Id,
                        ClassSchedule_Employee = new List<ToDoEmployeeVM>(),
                        ClassSize = classSchedule.ClassSize.GetValueOrDefault(),
                        EndDateTime = classSchedule.EndDateTime,
                        Id = classSchedule.Id,
                        Instructor = classSchedule.Instructor?.InstructorName,
                        Location = classSchedule.Location?.LocName,
                        SeatsTaken = classSchedule.ClassSchedule_Employee.Where(r => r.IsEnrolled).Count(),
                        StartDateTime = classSchedule.StartDateTime,
                        WaitListEnabled = classSchedule.ClassSchedule_SelfRegistrationOption == null ? false : classSchedule.ClassSchedule_SelfRegistrationOption.MakeAvailableForSelfReg
                    };

                    foreach (var classScheduleEmployee in classScheduleEmployees.Where(r => r.ClassScheduleId == classSchedule.Id).Distinct())
                    {
                        var classScheduleEmployeeVm = new ToDoEmployeeVM()
                        {
                            ILAId = ila.Id,
                            ClassScheduleId = classSchedule.Id,
                            EmployeeId = classScheduleEmployee.EmployeeId,
                            FirstName = classScheduleEmployee.Employee.Person.FirstName,
                            IsEnrolled = classScheduleEmployee.IsEnrolled,
                            IsWaitListed = classScheduleEmployee.IsWaitlisted,
                            LastName = classScheduleEmployee.Employee.Person.LastName
                        };

                        classScheduleVm.ClassSchedule_Employee.Add(classScheduleEmployeeVm);
                    }

                    item.ClassSchedules.Add(classScheduleVm);
                }

                model.Add(item);
            }

            return model;

        }

        public async Task<ClassSchedule_SelfRegistrationOptions_ViewModel> GetClassSchedule_SelfRegistrationOptionsAsync(int id)
        {
            var classSchedule = (await _classScheduleService.FindWithIncludeAsync(r => r.Id == id, new string[] { "ClassSchedule_SelfRegistrationOption" })).FirstOrDefault();

            if (classSchedule.ClassSchedule_SelfRegistrationOption == null)
            {
                var ila = (await _ilaService.FindWithIncludeAsync(r => r.Id == classSchedule.ILAID, new string[] { "ILA_SelfRegistrationOption" })).FirstOrDefault();
                if (ila.ILA_SelfRegistrationOption != null)
                {
                    var ilaSelfReg = ila.ILA_SelfRegistrationOption;
                    var selfRegOption = new ClassSchedule_SelfRegistrationOptions_ViewModel(ilaSelfReg.MakeAvailableForSelfReg, ilaSelfReg.RequireAdminApproval, ilaSelfReg.AcknowledgeRegDisclaimer,
                        ilaSelfReg.RegDisclaimer, ilaSelfReg.LimitForLinkedPositions, ilaSelfReg.CloseRegOnStartDate, ilaSelfReg.EnableWaitlist, ilaSelfReg.SendApprovedEmail);
                    return selfRegOption;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                var classSelfReg = classSchedule.ClassSchedule_SelfRegistrationOption;
                var classSelfRegOption = new ClassSchedule_SelfRegistrationOptions_ViewModel(classSelfReg.MakeAvailableForSelfReg, classSelfReg.RequireAdminApproval, classSelfReg.AcknowledgeRegDisclaimer,
                        classSelfReg.RegDisclaimer, classSelfReg.LimitForLinkedPositions, classSelfReg.CloseRegOnStartDate, classSelfReg.EnableWaitlist, classSelfReg.SendApprovedEmail);
                return classSelfRegOption;
            }
        }

        public async Task<ClassSchedule_SelfRegistrationOptions_ViewModel> CreateClassSchedule_SelfRegistrationAsync(ClassSchedule_RegistrationCreateOptions options)
        {
            var selfRegistrationServiceSetting = new ClassSchedule_SelfRegistrationOptions(options.ClassScheduleId, options.MakeAvailableForSelfReg,
                options.RequireAdminApproval, options.AcknowledgeRegDisclaimer, options.RegDisclaimer,
                options.LimitForLinkedPositions, options.CloseRegOnStartDate, options.EnableWaitlist, options.SendApprovedEmail);
            var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, selfRegistrationServiceSetting, ClassSchedule_SelfRegistrationOperations.Create);
            if (authResult.Succeeded)
            {
                var validationResult = new ValidationResult();
                var exists = await _classSchedule_SelfRegService.FindQuery(x => x.ClassScheduleId == options.ClassScheduleId).FirstOrDefaultAsync();
                if (exists != null)
                {
                    exists.MakeAvailableForSelfReg = selfRegistrationServiceSetting.MakeAvailableForSelfReg;
                    exists.RequireAdminApproval = selfRegistrationServiceSetting.RequireAdminApproval;
                    exists.AcknowledgeRegDisclaimer = selfRegistrationServiceSetting.AcknowledgeRegDisclaimer;
                    exists.RegDisclaimer = selfRegistrationServiceSetting.RegDisclaimer;
                    exists.LimitForLinkedPositions = selfRegistrationServiceSetting.LimitForLinkedPositions;
                    exists.CloseRegOnStartDate = selfRegistrationServiceSetting.CloseRegOnStartDate;
                    exists.EnableWaitlist = selfRegistrationServiceSetting.EnableWaitlist;
                    exists.SendApprovedEmail = selfRegistrationServiceSetting.SendApprovedEmail;
                    validationResult = await _classSchedule_SelfRegService.UpdateAsync(exists);
                }
                else
                {
                    validationResult = await _classSchedule_SelfRegService.AddAsync(selfRegistrationServiceSetting);
                }
                if (options.UpdateRecurrences)
                {
                    List<int> recurrenceIds = (await _classScheduleService.GetClassScheduleRecurrences(options.ClassScheduleId)).Select(x => x.Id).ToList();
                    foreach (int recurreneceId in recurrenceIds)
                    {
                        var recurrenceExists = (await _classSchedule_SelfRegService.FindAsync(x => x.ClassScheduleId == recurreneceId)).FirstOrDefault();
                        if (recurrenceExists == null)
                        {
                            var newSelfRegServiceSetting = selfRegistrationServiceSetting.DeepCopy();
                            newSelfRegServiceSetting.ClassScheduleId = recurreneceId;
                            newSelfRegServiceSetting.Id = 0;
                            validationResult = await _classSchedule_SelfRegService.AddAsync(newSelfRegServiceSetting);
                            if (!validationResult.IsValid)
                            {
                                throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                            }
                        }

                    }
                }
                if (validationResult.IsValid)
                {
                    var classScheduleSelfReg = new ClassSchedule_SelfRegistrationOptions_ViewModel(options.MakeAvailableForSelfReg,
                        options.RequireAdminApproval, options.AcknowledgeRegDisclaimer, options.RegDisclaimer,
                        options.LimitForLinkedPositions, options.CloseRegOnStartDate, options.EnableWaitlist, options.SendApprovedEmail);
                    return classScheduleSelfReg;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
            }
        }

        public async System.Threading.Tasks.Task EnrollStudentAsync(ClassScheduleEnrollOptions options, bool isCallHander = true)
        {
            var emp = await _classScheduleEmployeeLinkService.FindQueryWithIncludeAsync(x => x.EmployeeId == options.EmployeeId && x.ClassScheduleId == options.ClassId, new string[] { "Employee" }).FirstOrDefaultAsync();
            var cs = await _classScheduleService.FindQueryWithIncludeAsync(x => x.Id == options.ClassId, new string[] { "ClassSchedule_SelfRegistrationOption", "ClassSchedule_Employee" }).FirstOrDefaultAsync();
            var enrolled = cs.ClassSchedule_Employee.Where(w => w.IsEnrolled).Count();
            var seats = cs.ClassSize;
            var availableSeats = seats - enrolled;
            if (emp != null)
            {
                //var ila = await _ilaService.FindQueryWithIncludeAsync(x => x.ClassSchedules.Any(a => a.Id == classId) && x.ILA_SelfRegistrationOption != null,new string[] { "ClassSchedules", "ILA_SelfRegistrationOption" }).FirstOrDefaultAsync();
                //var schedule = await _classScheduleService.FindQueryWithIncludeAsync(x => x.Id == classId, new string[] { "ClassSchedule_Employee" }).FirstOrDefaultAsync();
                if (availableSeats > 0)
                {
                    emp.EnrollStudent(options.PlannedDate,isCallHander);
                    emp.ApproveSelfRegistration();
                }
                else if (cs.ClassSchedule_SelfRegistrationOption != null && cs.ClassSchedule_SelfRegistrationOption.EnableWaitlist)
                {
                    emp.IsEnrolled = false;
                    emp.IsWaitlisted = true;
                    emp.IsDenied = false;
                    emp.IsDropped = false;
                    emp.IsAwaitingForApproval = false;
                }
                var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                emp.Modify(userName);
                var validationResult = await _classScheduleEmployeeLinkService.UpdateAsync(emp);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["EmployeeNotFoundException"]);
            }
        }

        public async System.Threading.Tasks.Task EnrollStudentWithClassSizeByPassAsync(ClassScheduleEnrollOptions options)
        {
            var user = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);

            var emp = (await _classScheduleEmployeeLinkService.FindAsync(x => x.EmployeeId == options.EmployeeId && x.ClassScheduleId == options.ClassId)).FirstOrDefault();
            var cs = (await _classScheduleService.FindWithIncludeAsync(x => x.Id == options.ClassId, new string[] { "ClassSchedule_SelfRegistrationOption", "ClassSchedule_Employee.Employee" })).FirstOrDefault();
            var enrolled = cs.ClassSchedule_Employee.Where(w => w.IsEnrolled).Count();
            var seats = cs.ClassSize;
            var availableSeats = seats - enrolled;
            var isClassSizeIncreased = false;
            if (emp != null)
            {
                if (availableSeats > 0)
                {
                    emp.EnrollStudent(options.PlannedDate);
                    emp.ApproveSelfRegistration();
                }
                else
                {
                    emp.EnrollStudent(options.PlannedDate);
                    emp.ApproveSelfRegistration();
                    isClassSizeIncreased = true;
                }

                var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                emp.Modify(userName);
                var validationResult = await _classScheduleEmployeeLinkService.UpdateAsync(emp);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                }
                else if (isClassSizeIncreased)
                {
                    cs.ClassSize++;
                    validationResult = await _classScheduleService.UpdateAsync(cs);
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["EmployeeNotFoundException"]);
            }
        }

        public async System.Threading.Tasks.Task WaitListStudentAsync(ClassScheduleEnrollOptions options)
        {
            var emp = await _classScheduleEmployeeLinkService.FindQuery(x => x.EmployeeId == options.EmployeeId && x.ClassScheduleId == options.ClassId).FirstOrDefaultAsync();
            if (emp != null)
            {
                var classSchedule = await _classScheduleService.GetWithIncludeAsync(options.ClassId, new string[] { "ClassSchedule_SelfRegistrationOption" });
                //var schedule = await _classScheduleService.FindQueryWithIncludeAsync(x => x.Id == classId, new string[] { "ClassSchedule_Employee" }).FirstOrDefaultAsync();
                if (classSchedule != null)
                {
                    if (classSchedule.ClassSchedule_SelfRegistrationOption != null)
                    {
                        if (classSchedule.ClassSchedule_SelfRegistrationOption.EnableWaitlist)
                        {
                            emp.IsWaitlisted = true;
                            emp.IsEnrolled = false;
                            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                            emp.Modify(userName);
                            var validationResult = await _classScheduleEmployeeLinkService.UpdateAsync(emp);
                            if (!validationResult.IsValid)
                            {
                                throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                            }
                        }
                    }
                    else
                    {
                        throw new BadHttpRequestException(message: _localizer["ClassSchedule_SelfRegistrationOptionNotFoundException"]);
                    }
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["EmployeeNotFoundException"]);
            }
        }

        public async System.Threading.Tasks.Task DeclineEmployee(ClassScheduleEnrollOptions options)
        {
            var emp = (await _classScheduleEmployeeLinkService.FindWithIncludeAsync(x => x.EmployeeId == options.EmployeeId && x.ClassScheduleId == options.ClassId, new string[] { "Employee.Person" })).FirstOrDefault();

            emp.DeclineSelfRegistration();
            var validationResult = await _classScheduleEmployeeLinkService.UpdateAsync(emp);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
            }
        }

        public async Task<List<ClassSchedule_Employee>> UpdateBulkGradeAsync(int id, ClassScheduleGradeCreateOptions options)
        {
            List<ClassSchedule_Employee> toUpdate = new List<ClassSchedule_Employee>();

            var obj_list = (await _classScheduleEmployeeLinkService.FindWithIncludeAsync(x => x.ClassScheduleId == id,
                new[] { "ClassSchedule.ClassSchedule_TQEMPSettings", "ClassSchedule.ILA" })).ToList();

            if (obj_list != null && obj_list.Count > 0)
            {
                var ila = await _ilaService.GetWithIncludeAsync(obj_list[0]?.ClassSchedule.ILAID ?? 0, new string[] { "ILA_TaskObjective_Links.Task" });
                var tasks = ila.ILA_TaskObjective_Links.Where(x => x.UseForTQ == true).Select(x => x.Task).ToList();

                foreach (var obj in obj_list)
                {
                    obj.UpdateGradeNotes(options.bulkGradeNote);
                    obj.CompleteClass(options.BulkCompDate, options.BulkGrade?.ToUpper(), options.bulkScore);
                    var username = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    obj.Modify(username);
                    toUpdate.Add(obj);

                    if (options.IsQualificationCompleted ?? false)
                    {
                        obj.PopulateOJTRecord = true;

                        foreach (var task in tasks)
                        {
                            var taskqualification = (await _taskQualificationDomainService.FindAsync(x => x.TaskId == task.Id && x.EmpId == obj.EmployeeId && x.ClassScheduleId == id)).FirstOrDefault();

                            if (taskqualification == null)
                            {
                                continue;
                            }

                            taskqualification.TaskQualificationDate = options.BulkCompDate?.ToUniversalTime() ?? DateTime.UtcNow;
                            taskqualification.CriteriaMet = true;
                            taskqualification.Comments = $"ILA Completion - {ila.Number}/{ila.Name}";

                            if (taskqualification.TaskQualificationDate != null)
                            {
                                taskqualification.TQStatusId = (taskqualification.TaskQualificationDate < taskqualification.DueDate) ? 2 : 4;
                            }
                            else
                            {
                                taskqualification.TQStatusId = 3;
                            }

                            var res = await _taskQualificationDomainService.UpdateAsync(taskqualification);
                            if (!res.IsValid)
                                throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", res.Errors)]);
                        }
                    }
                }

                var validationResult = await _classScheduleEmployeeLinkService.BulkUpdateAsync(toUpdate);
            }

            var update_obj_list = (await _classScheduleEmployeeLinkService.AllAsync()).Where(x => x.ClassScheduleId == id).ToList();

            return update_obj_list;
        }


        public async Task<ClassSchedule_Employee> UpdateGradeAsync(int id, ClassScheduleGradeCreateOptions options)
        {
            var classEmployee = await _classScheduleEmployeeLinkService.FindQuery(x => x.EmployeeId == id && x.ClassScheduleId == options.ClassId && !x.Deleted).FirstOrDefaultAsync();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, classEmployee, ClassScheduleEmployeeOperations.Update);
            if (result.Succeeded)
            {
                classEmployee.CompleteClass(options.completionDate, options.Grade?.ToUpper(), classEmployee.FinalScore);
                classEmployee.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                classEmployee.ModifiedDate = DateTime.Now;
                var validationResult = await _classScheduleEmployeeLinkService.UpdateAsync(classEmployee);

                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }

            }
            return classEmployee;
        }
        public async Task<ClassSchedule_Employee> UpdateScoreAsync(int id, ClassScheduleGradeCreateOptions options)
        {
            var classEmployee = await _classScheduleEmployeeLinkService.FindQuery(x => x.EmployeeId == id && x.ClassScheduleId == options.ClassId && !x.Deleted).FirstOrDefaultAsync();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, classEmployee, ClassScheduleEmployeeOperations.Update);
            if (result.Succeeded)
            {
                classEmployee.CompleteClass(options.completionDate, classEmployee.FinalGrade, options.Score);
                classEmployee.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                classEmployee.ModifiedDate = DateTime.Now;
                var validationResult = await _classScheduleEmployeeLinkService.UpdateAsync(classEmployee);

                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }

            }
            return classEmployee;
        }
        public async Task<ClassSchedule_Employee> UpdateNotesAsync(int id, ClassScheduleGradeCreateOptions options)
        {
            var classEmployee = await _classScheduleEmployeeLinkService.FindQuery(x => x.EmployeeId == id && x.ClassScheduleId == options.ClassId && !x.Deleted).FirstOrDefaultAsync();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, classEmployee, ClassScheduleEmployeeOperations.Update);
            if (result.Succeeded)
            {
                classEmployee.GradeNotes = options.GradeNotes;
                classEmployee.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                classEmployee.ModifiedDate = DateTime.Now;
                var validationResult = await _classScheduleEmployeeLinkService.UpdateAsync(classEmployee);

                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }

            }
            return classEmployee;
        }

        public async Task<ClassSchedule> UpdateTrainingAsync(int id, ClassScheduleCreateOptions options)
        {
            var obj = await _classScheduleService.FindQueryWithIncludeAsync(x => x.Id == id, new string[] { "ILA" }).FirstOrDefaultAsync();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

            if (result.Succeeded)
            {

                obj.StartDateTime = options.StartDateTime;
                obj.EndDateTime = options.EndDateTime;
                obj.InstructorId = options.InstructorId;
                obj.LocationId = options.LocationId;
                obj.Notes = options.Notes;
                obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.ModifiedDate = DateTime.Now;

                var validationResult = await _classScheduleService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return obj;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async Task<List<ScheduleEvalVM>> GetLinkedStudentEvalsAsync(int ilaId, int classId)
        {
            List<ScheduleEvalVM> evals = new List<ScheduleEvalVM>();
            var links = await _cs_eval_linkService.FindQueryWithIncludeAsync(x => x.ClassScheduleId == classId, new string[] { "StudentEvaluation" }).Select(s => s.StudentEvaluation).ToListAsync();
            var emps = await GetLinkedEmployees(classId);
            foreach (var emp in emps)
            {
                foreach (var link in links)
                {
                    var data = await _cs_eval_rosterService.FindQuery(x => x.ClassScheduleId == classId && x.StudentEvaluationId == link.Id && x.EmployeeId == emp.EmpId).FirstOrDefaultAsync();

                    var eval = new ScheduleEvalVM();
                    eval.Title = link.Title;
                    eval.Id = link.Id;
                    eval.Completed = data != null && data.IsCompleted;
                    eval.EmpId = emp.EmpId;
                    if (eval.Completed)
                    {
                        eval.hasAggregateData = await _eval_withoutEmpService.FindQuery(x => x.IsCompleted == true && x.ClassScheduleId == classId && x.StudentEvaluationId == link.Id && x.EmployeeId == emp.EmpId && x.DataMode == "Aggregate").AnyAsync();
                    }
                    evals.Add(eval);

                }
            }
            return evals;
        }

        public async Task<List<EmpSelfregistrationCourses>> GetSelfRegAvailableCoursesAsync(DateTime currentUtcDateTime)
        {
            var list = new List<EmpSelfregistrationCourses>();
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

            var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();

            if (person == null) return list;

            var employee = await _employeeService.GetByPersonIdAsync(person.Id);
            if (employee == null) return list;
            var employeePositionIds = employee.EmployeePositions.Where(p => p.Active).Select(p => p.PositionId).ToList();

            var classSchedules = await _classScheduleService.GetSelfRegAvailableCoursesAsync();
            if (!classSchedules.Any()) return list;

            var distinctILAIds = classSchedules.Select(cls => cls.ILAID.Value).Distinct().ToList();
            var distinctEmpIds = classSchedules.SelectMany(m => m.ClassSchedule_Employee).Select(r => r.EmployeeId).Distinct().ToList();

            var ilaProviderData = await _ilaService.GetILAsWithProvider(distinctILAIds);
            var employees = await _emp_domainService.GetEmployeesByListOfEmpIds(distinctEmpIds);
            var classScheduleIds = classSchedules.Select(cls => cls.Id).ToList();

            var classScheduleEmployees = await _classScheduleEmployeeLinkService.FindQuery(x => classScheduleIds.Contains(x.ClassScheduleId)).ToListAsync();
            var selfRegOptions = await _classSchedule_SelfRegService.FindQuery(x => classScheduleIds.Contains(x.ClassScheduleId)).ToListAsync();
            var classRosters = await _classRosterService.FindQuery(x => classScheduleIds.Contains(x.ClassScheduleId.Value) && x.EmpId == employee.Id).ToListAsync();

            foreach (var cls in classSchedules)
            {
                cls.ClassSchedule_Employee.ToList().ForEach(clsEmp => clsEmp.Employee = employees.FirstOrDefault(e => e.Id == clsEmp.EmployeeId));
                cls.ILA = ilaProviderData.FirstOrDefault(i => i.Id == cls.ILAID);
            }

            var ilas = classSchedules.Where(cs => cs.ILA != null).Select(x => x.ILA).Distinct().ToList();

            var ilaGroups = classSchedules.Where(cs => cs.ILA != null && cs.EndDateTime > currentUtcDateTime)
                .GroupBy(cs => cs.ILAID).ToDictionary(g => g.Key, g => g.ToList());

            foreach (var ila in ilas)
            {

                if (!ilaGroups.TryGetValue(ila.Id, out var ilaClassSchedules)) continue;

                var course = new EmpSelfregistrationCourses
                {
                    Provider = ilaClassSchedules[0].ILA.Provider?.Name,
                    ILANum = ila.Number,
                    ILATitle = ila.Name,
                    TotalCourses = ilaClassSchedules.Count,
                    EmpSelfregistrationEmployees = new List<EmpSelfregistrationEmployees>()
                };

                foreach (var cls in ilaClassSchedules)
                {
                    var totalSeats = cls.ClassSize.GetValueOrDefault();
                    var classScheduleEmployeeCount = classScheduleEmployees.Count(x => x.ClassScheduleId == cls.Id && x.IsEnrolled == true);
                    var availableSeats = totalSeats - classScheduleEmployeeCount;

                    var classScheduleEmployee = classScheduleEmployees.FirstOrDefault(x => x.ClassScheduleId == cls.Id && x.EmployeeId == employee.Id);
                    var classRoster = classRosters.FirstOrDefault(x => x.ClassScheduleId == cls.Id);
                    var selfRegistrationOptions = selfRegOptions.FirstOrDefault(x => x.ClassScheduleId == cls.Id);

                    if (selfRegistrationOptions?.LimitForLinkedPositions == true)
                    {
                        var linkedPositions = ila.ILA_Position_Links?.Select(p => p.PositionId).ToList();

                        if (linkedPositions == null || !linkedPositions.Intersect(employeePositionIds).Any())
                        {
                            continue;
                        }
                    }

                    if (classScheduleEmployee == null || (classScheduleEmployee.IsEnrolled != true && classScheduleEmployee.IsDenied != true && classScheduleEmployee.IsDropped != true))
                    {
                        course.EmpSelfregistrationEmployees.Add(new EmpSelfregistrationEmployees
                        {
                            ClassId = cls.Id,
                            ILAId = cls.ILA.Id,
                            ILANum = cls.ILA.Number,
                            ILATitle = cls.ILA.Name,
                            Instructor = cls.Instructor?.InstructorName,
                            Location = cls.Location?.LocName,
                            IsEnrolled = classScheduleEmployee?.IsEnrolled,
                            IsDropped = classScheduleEmployee?.IsDropped,
                            IsDenied = classScheduleEmployee?.IsDenied,
                            IsInWaitList = classScheduleEmployee?.IsWaitlisted,
                            SeatsAvailable = availableSeats,
                            RequiredAdminApproval = selfRegistrationOptions?.RequireAdminApproval ?? false,
                            IsWaitListEnabled = selfRegistrationOptions?.EnableWaitlist ?? false,
                            ClassStartDateTime = cls.StartDateTime,
                            ClassEndDateTime = cls.EndDateTime,
                            ClassDetail = $"{cls.StartDateTime} - {cls.EndDateTime}",
                            Acknolwedgement = selfRegistrationOptions?.RegDisclaimer,
                            CloseRegistrationOnStartDate = selfRegistrationOptions?.CloseRegOnStartDate ?? false,
                            IsAwaitingForApproval = classScheduleEmployee?.IsAwaitingForApproval
                        });
                    }
                }

                if (course.EmpSelfregistrationEmployees.Any())
                {
                    list.Add(course);
                }
            }

            return list;
        }

        public async System.Threading.Tasks.Task RegisterBySelfRegistrationAsync(int classId, int ilaId)
        {
            var employee = await _employeeService.GetEmployeeByUsernameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            var emp = await _classScheduleEmployeeLinkService.FindQuery(x => x.EmployeeId == employee.Id && x.ClassScheduleId == classId).FirstOrDefaultAsync();
            var cs = await _classScheduleService.FindQueryWithIncludeAsync(x => x.Id == classId, new string[] { "ClassSchedule_SelfRegistrationOption", "ClassSchedule_Employee" }).FirstOrDefaultAsync();
            var enrolled = cs.ClassSchedule_Employee.Where(w => w.IsEnrolled).Count();
            var seats = cs.ClassSize;
            var availableSeats = seats - enrolled;
            if (emp == null)
            {
                if (cs.ClassSchedule_SelfRegistrationOption != null)
                {

                    cs.RegisterEmployee(employee, cs.ClassSchedule_SelfRegistrationOption.RequireAdminApproval);
                }
                else
                {
                    throw new BadHttpRequestException(message: _localizer["ClassSchedule_SelfRegistrationOptionNotFoundException"]);
                }

                var validationResult = await _classScheduleService.UpdateAsync(cs);
                //await AddIntoRoster(employee);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                //if (availableSeats > 0)
                //{
                //    emp.IsEnrolled = true;
                //    emp.IsWaitlisted = false;
                //}
                //else if (cs.ILA.ILA_SelfRegistrationOption.EnableWaitlist)
                //{
                //    emp.IsEnrolled = false;
                //    emp.IsWaitlisted = true;
                //}
                //var validationResult = await _classScheduleEmployeeLinkService.UpdateAsync(emp);
                //if (!validationResult.IsValid)
                //{
                //    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                //}
            }
            else
            {
                if (cs.ClassSchedule_SelfRegistrationOption != null)
                {
                    if (cs.ClassSchedule_SelfRegistrationOption.RequireAdminApproval == true)
                    {
                        emp.IsEnrolled = false;
                        emp.IsAwaitingForApproval = true;
                    }
                    else
                    {
                        emp.EnrollStudent(null);
                    }

                }
                else
                {
                    throw new BadHttpRequestException(message: _localizer["ClassSchedule_SelfRegistrationOptionNotFoundException"]);
                }
                var validationResult = await _classScheduleEmployeeLinkService.UpdateAsync(emp);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                }
            }
        }

        public async Task<List<EmpCourses>> GetSelfRegEmployeeAprovedCoursesAsync()
        {
            var list = new List<EmpCourses>();
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

            var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();

            if (person != null)
            {
                var employee = await _employeeService.GetByPersonIdAsync(person.Id);
                //Get emplpyee approved courses

                if (employee != null)
                {
                    var classScheduleEmployees = await _classScheduleEmployeeLinkService.FindQueryWithIncludeAsync(x => (x.ClassSchedule.ClassSchedule_SelfRegistrationOption != null && x.ClassSchedule.ClassSchedule_SelfRegistrationOption.MakeAvailableForSelfReg) && x.EmployeeId == employee.Id && x.IsEnrolled == true && x.IsWaitlisted != true && x.IsDenied != true && x.IsDropped != true, new string[] { "ClassSchedule", "ClassSchedule.Instructor", "ClassSchedule.Location", "ClassSchedule.ILA.Provider", "ClassSchedule.ClassSchedule_SelfRegistrationOption" }).ToListAsync();
                    if (classScheduleEmployees.Count > 0)
                    {
                        foreach (var clsSchedule in classScheduleEmployees)
                        {
                            //add the commented part of end date constraint is required for approved courses

                            if (clsSchedule.ClassSchedule != null && clsSchedule.ClassSchedule.ILA != null && clsSchedule.ClassSchedule.EndDateTime.Date >= DateTime.Now.Date)
                            {
                                if (clsSchedule.ClassSchedule.ClassSchedule_SelfRegistrationOption == null)
                                {
                                    throw new BadHttpRequestException(message: _localizer["ClassSchedule_SelfRegistrationOptionNotFoundException"]);
                                }
                                else
                                {
                                    list.Add(new EmpCourses()
                                    {
                                        Provider = clsSchedule?.ClassSchedule.ILA.Provider?.Name ?? "N/A",
                                        ILANum = clsSchedule.ClassSchedule.ILA.Number,
                                        ILATitle = clsSchedule.ClassSchedule.ILA.Name,
                                        ClassId = clsSchedule.ClassScheduleId,
                                        ILAId = clsSchedule.ClassSchedule.ILA.Id,
                                        Instructor = clsSchedule.ClassSchedule.Instructor?.InstructorName ?? "N/A",
                                        Location = clsSchedule.ClassSchedule.Location?.LocName,
                                        ClassStartDateTime = clsSchedule.ClassSchedule.StartDateTime,
                                        ClassEndDateTime = clsSchedule.ClassSchedule.EndDateTime,
                                        Acknolwedgement = clsSchedule?.ClassSchedule.ClassSchedule_SelfRegistrationOption.RegDisclaimer ?? "N/A",

                                    });
                                }

                            }

                        }

                    }
                }
            }
            return list;
        }

        public async Task<List<EmpCourses>> GetSelfRegEmployeeDeniedCoursesAsync()
        {
            var list = new List<EmpCourses>();
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

            var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();

            if (person != null)
            {
                var employee = await _employeeService.GetByPersonIdAsync(person.Id);
                //Get emplpyee approved courses

                var classScheduleEmployees = await _classScheduleEmployeeLinkService.FindQueryWithIncludeAsync(x => x.EmployeeId == employee.Id && x.IsDenied == true, new string[] { "ClassSchedule", "ClassSchedule.Instructor", "ClassSchedule.Location", "ClassSchedule.ILA.Provider" }).ToListAsync();
                if (classScheduleEmployees.Count > 0)
                {
                    foreach (var clsSchedule in classScheduleEmployees)
                    {
                        list.Add(new EmpCourses()
                        {
                            Provider = clsSchedule.ClassSchedule.ILA.Provider?.Name,
                            ILANum = clsSchedule.ClassSchedule.ILA.Number,
                            ILATitle = clsSchedule.ClassSchedule.ILA.Name,
                            ClassId = clsSchedule.ClassScheduleId,
                            ILAId = clsSchedule.ClassSchedule.ILA.Id,
                            Instructor = clsSchedule.ClassSchedule.Instructor?.InstructorName,
                            Location = clsSchedule.ClassSchedule.Location?.LocName,
                            ClassStartDateTime = clsSchedule.ClassSchedule.StartDateTime,
                            ClassEndDateTime = clsSchedule.ClassSchedule.EndDateTime

                        });

                    }
                }
            }
            return list;
        }

        public async Task<List<EmpCourses>> GetSelfRegEmployeeDroppedCoursesAsync()
        {
            var list = new List<EmpCourses>();
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

            var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();

            if (person != null)
            {
                var employee = await _employeeService.GetByPersonIdAsync(person.Id);
                //Get emplpyee approved courses

                var classScheduleEmployees = await _classScheduleEmployeeLinkService.FindQueryWithIncludeAsync(x => x.EmployeeId == employee.Id && x.IsDropped == true && x.IsEnrolled != true && x.IsDenied != true, new string[] { "ClassSchedule.Instructor", "ClassSchedule.Location", "ClassSchedule.ILA.Provider", "ClassSchedule.ClassSchedule_SelfRegistrationOption" }).ToListAsync();
                if (classScheduleEmployees.Count > 0)
                {
                    foreach (var clsSchedule in classScheduleEmployees)
                    {
                        var enrolled = clsSchedule.ClassSchedule.ClassSchedule_Employee.Where(w => w.IsDropped).Count();
                        var seats = clsSchedule.ClassSchedule.ClassSize.GetValueOrDefault();
                        var availableSeats = seats - enrolled;
                        list.Add(new EmpCourses()
                        {
                            Provider = clsSchedule.ClassSchedule.ILA.Provider?.Name,
                            ILANum = clsSchedule.ClassSchedule.ILA.Number,
                            ILATitle = clsSchedule.ClassSchedule.ILA.Name,
                            ClassId = clsSchedule.ClassScheduleId,
                            ILAId = clsSchedule.ClassSchedule.ILA.Id,
                            Instructor = clsSchedule.ClassSchedule.Instructor?.InstructorName,
                            Location = clsSchedule.ClassSchedule.Location?.LocName,
                            ClassStartDateTime = clsSchedule.ClassSchedule.StartDateTime,
                            ClassEndDateTime = clsSchedule.ClassSchedule.EndDateTime,
                            IsEnrolled = clsSchedule.IsEnrolled,
                            IsDenied = clsSchedule.IsDenied,
                            IsDropped = clsSchedule.IsDropped,
                            IsWaitListEnabled = clsSchedule.ClassSchedule.ClassSchedule_SelfRegistrationOption?.EnableWaitlist,
                            IsInWaitList = clsSchedule.IsWaitlisted,
                            RequiredAdminApproval = clsSchedule.ClassSchedule.ClassSchedule_SelfRegistrationOption == null ? false : clsSchedule.ClassSchedule.ClassSchedule_SelfRegistrationOption.RequireAdminApproval,
                            SeatsAvailable = availableSeats,
                            IsAwaitingForApproval = clsSchedule.IsAwaitingForApproval,
                            Acknolwedgement = clsSchedule.ClassSchedule.ClassSchedule_SelfRegistrationOption?.RegDisclaimer,

                        });

                    }
                }
            }
            return list;
        }

        public async System.Threading.Tasks.Task DropCourseAsync(int classId, int ilaId)
        {

            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

            var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();

            if (person != null)
            {
                var employee = await _employeeService.GetByPersonIdAsync(person.Id);
                var emp = await _classScheduleEmployeeLinkService.FindQuery(x => x.EmployeeId == employee.Id && x.ClassScheduleId == classId).FirstOrDefaultAsync();
                emp.DropCourse();
                await RemoveRosterData(classId, employee.Id);
                var validationResult = await _classScheduleEmployeeLinkService.UpdateAsync(emp);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["EmployeeNotFoundException"]);
            }
        }

        public async System.Threading.Tasks.Task JoinWaitListAsync(int classId, int ilaId)
        {

            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

            var person = (await _personService.FindAsync(x => x.Username == userName, true)).FirstOrDefault();

            if (person != null)
            {
                var employee = await _employeeService.GetByPersonIdAsync(person.Id);
                var emp = await _classScheduleEmployeeLinkService.FindQuery(x => x.EmployeeId == employee.Id && x.ClassScheduleId == classId).FirstOrDefaultAsync();
                var cs = await _classScheduleService.FindQueryWithIncludeAsync(x => x.Id == classId, new string[] { "ClassSchedule_SelfRegistrationOption", "ClassSchedule_Employee" }).FirstOrDefaultAsync();
                var enrolled = cs.ClassSchedule_Employee.Where(w => w.IsEnrolled).Count();
                var seats = cs.ClassSize;
                var availableSeats = seats - enrolled;
                if (emp == null)
                {

                    cs.JoinWaitList(employee);


                    var validationResult = await _classScheduleService.UpdateAsync(cs);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }

                }
                else
                {
                    emp.IsWaitlisted = true;
                    var validationResult = await _classScheduleEmployeeLinkService.UpdateAsync(emp);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                    }
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["EmployeeNotFoundException"]);
            }
        }

        public async Task<ILAClassDetailsVM> ViewILAAndClassDetailAsync(int classId, int ilaId)
        {

            var classSchedule = await _classScheduleService.GetClassScheduleByClassAndILAId(classId, ilaId);
            var distinctTaskIds = classSchedule.ILA.ILA_TaskObjective_Links.Select(m => m.TaskId).Distinct().ToList();
            var distinctEOIds = classSchedule.ILA.ILA_EnablingObjective_Links.Select(m => m.EnablingObjectiveId).Distinct().ToList();
            var tasks = await _taskDomainService.GetTasksWithDutySubDutyAreaByTaskIdsAsync(distinctTaskIds);
            var enablingObjectives = await _enablingObjectiveDomainService.GetEnablingObjectivesByEOIDs(distinctEOIds);
            foreach (var tasklink in classSchedule?.ILA?.ILA_TaskObjective_Links)
            {
                tasklink.Task = tasks.Where(x => x.Id == tasklink.TaskId).FirstOrDefault();
            }
            foreach (var eolink in classSchedule?.ILA?.ILA_EnablingObjective_Links)
            {
                eolink.EnablingObjective = enablingObjectives.Where(x => x.Id == eolink.EnablingObjectiveId).FirstOrDefault();
            }
            var totalTrainingHours = classSchedule?.ILA?.TotalTrainingHours;
            ILACertificationDetailsVM ilaCertDetails = null;
            var ilaNercCertificationLink = classSchedule?.ILA?.ILACertificationLinks.FirstOrDefault(x => x.Certification?.CertifyingBody.Name == "NERC");
            var ilaCertData = classSchedule?.ILA?.ILACertificationLinks.Where(x => x.Certification?.CertifyingBody?.Name != "NERC").Select(ic => new Certification_SubRequirementVM(ic.Certification.Name, ic.CEHHours, ic.ILACertificationSubRequirementLink.Select(sr => new CertificationSubRequirementVM(sr?.CertificationSubRequirement?.ReqName, sr.CertificationSubRequirement.ReqHour)).ToList())).ToList();
            if (ilaNercCertificationLink != null)
            {
                var isEmergencyOpHours = ilaNercCertificationLink?.IsEmergencyOpHours;
                var isPartialCreditHours = ilaNercCertificationLink?.IsPartialCreditHours;
                var cehHours = ilaNercCertificationLink?.CEHHours;
                var standardReqhour = ilaNercCertificationLink?.ILACertificationSubRequirementLink?.FirstOrDefault(x => x.CertificationSubRequirement.ReqName == "Standards")?.ReqHour;
                var simulationReqhour = ilaNercCertificationLink?.ILACertificationSubRequirementLink?.FirstOrDefault(x => x.CertificationSubRequirement.ReqName == "Simulations")?.ReqHour;
                ilaCertDetails = new ILACertificationDetailsVM(cehHours, standardReqhour, simulationReqhour, isEmergencyOpHours, isPartialCreditHours);
            }
            var procedureDatas = classSchedule?.ILA?.ILA_Procedure_Links.Select(x => new ProcedureDataVM(x.Procedure.Number, x.Procedure.Title)).ToList();
            var tasksDatas = classSchedule?.ILA?.ILA_TaskObjective_Links.Select(x => new TaskDataVM(x.Task.FullNumber, x.Task.Description, "Task")).ToList();
            var enablingObjectiveDatas = classSchedule?.ILA?.ILA_EnablingObjective_Links.Select(x => new EnablingObjectiveDataVM(x.EnablingObjective.FullNumber, x.EnablingObjective.Description, "EO")).ToList();
            var ilaClassDetail = new ILAClassDetailsVM(classSchedule.StartDateTime, classSchedule.EndDateTime, classSchedule.Location?.LocName, classSchedule?.Instructor?.InstructorName, classSchedule.SpecialInstructions, classSchedule.WebinarLink, procedureDatas, tasksDatas, enablingObjectiveDatas, ilaCertDetails, ilaCertData, totalTrainingHours);
            return ilaClassDetail;
        }

        public async System.Threading.Tasks.Task ReReleaseTest(ReReleaseOptions options)
        {
            var roster = await _classRosterService.FindQuery(x => x.ClassScheduleId == options.ClassId && x.EmpId == options.EmpId && x.TestId == options.TestId).FirstOrDefaultAsync();
            if (roster == null)
            {
                throw new BadHttpRequestException(message: _localizer["Roster Not Found"]);
            }
            else
            {
                var statusId = await _rosterStatusService.FindQuery(x => x.Name == "Not Started").Select(s => s.Id).FirstOrDefaultAsync();
                if (statusId == 0)
                {
                    throw new BadHttpRequestException(message: _localizer["Status not found exception"]);
                }
                else
                {
                    roster.StatusId = statusId;
                    await _classRosterService.UpdateAsync(roster);
                }
            }
        }

        //New Application Services

        public async Task<List<EmpSelfregistrationCourses>> GetSelfRegAvailableCoursesByIdAsync(int employeeId)
        {
            var list = new List<EmpSelfregistrationCourses>();
            if (employeeId != null)
            {
                var classSchedules = await _classScheduleService.GetSelfRegAvailableCoursesAsync();
                var distinctEmpIds = classSchedules.SelectMany(m => m.ClassSchedule_Employee).Select(r => r.EmployeeId).Distinct().ToList();
                var distinctILAIds = classSchedules.Select(cls => cls.ILAID.Value).Distinct().ToList();
                var ilaProviderData = await _ilaService.GetILAsWithProvider(distinctILAIds);
                var employees = await _emp_domainService.GetEmployeesByListOfEmpIds(distinctEmpIds);
                classSchedules.ForEach(cls => cls.ClassSchedule_Employee.ToList().ForEach(clsEmp => clsEmp.Employee = employees.FirstOrDefault(e => e.Id == clsEmp.EmployeeId)));
                classSchedules.ForEach(cls => cls.ILA = ilaProviderData.FirstOrDefault(i => i.Id == cls.ILAID));
                var ilas = classSchedules.Where(cs => cs.ILA != null).Select(x => x.ILA).Distinct().ToList();
                if (classSchedules.Count > 0)
                {
                    for (var i = 0; i < ilas.Count; i++)
                    {
                        var ilaClassSchedules = classSchedules.Where(r => r.ILAID == ilas[i].Id).ToList();
                        var empSelfRegistrationCourse = new EmpSelfregistrationCourses(ilas[i].Provider.Name, ilas[i].Name, ilas[i].Number, ilas[i].ClassSchedules.Count());
                        list.Add(empSelfRegistrationCourse);
                        foreach (var cls in ilaClassSchedules)
                        {
                            var classScheduleEmployeeCount = _classScheduleEmployeeLinkService.FindQuery(x => x.ClassScheduleId == cls.Id && x.IsEnrolled == true).ToList().Count;
                            var availableSeats = cls.ClassSize.GetValueOrDefault() - classScheduleEmployeeCount;
                            var classScheduleEmployee = _classScheduleEmployeeLinkService.FindQuery(x => x.ClassScheduleId == cls.Id && x.EmployeeId == employeeId).FirstOrDefault();
                            var classRoster = _classRosterService.FindQuery(x => x.ClassScheduleId == cls.Id && x.EmpId == employeeId).FirstOrDefault();

                            if (classScheduleEmployee == null || (classScheduleEmployee.IsEnrolled != true && classScheduleEmployee.IsDenied != true && classScheduleEmployee.IsDropped != true))
                            {
                                if (cls.EndDateTime > DateTime.Now)
                                {
                                    var selfRegistrationEmployee = new EmpSelfregistrationEmployees(cls.Id, ilas[i].Id, ilas[i].Name, ilas[i].Number, cls.StartDateTime, cls.EndDateTime, cls.StartDateTime + " - " + cls.EndDateTime, cls.Location.LocName, cls.Instructor.InstructorName, availableSeats, classScheduleEmployee == null ? null : classScheduleEmployee.IsEnrolled, classScheduleEmployee == null ? null : classScheduleEmployee.IsDenied, classScheduleEmployee == null ? null : classScheduleEmployee.IsDropped, cls.ClassSchedule_SelfRegistrationOption.EnableWaitlist, cls.ClassSchedule_SelfRegistrationOption.RequireAdminApproval, classScheduleEmployee == null ? null : classScheduleEmployee.IsWaitlisted, cls.ClassSchedule_SelfRegistrationOption.RegDisclaimer);
                                }
                            }
                        }
                    }
                    return list;
                }
                else
                {
                    throw new QTDServerException("No ILA Found");
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["Employee Not Found"]);
            }
        }

        public async Task<List<EmpCourses>> GetSelfRegEmployeeDroppedCoursesByIdAsync(int employeeId)
        {
            var list = new List<EmpCourses>();
            if (employeeId != null)
            {
                var classScheduleEmployees = await _classScheduleEmployeeLinkService.GetEmployeeSelfRegistrationDroppedCourse(employeeId);
                if (classScheduleEmployees.Count > 0)
                {
                    foreach (var clsSchedule in classScheduleEmployees)
                    {
                        var enrolled = clsSchedule.ClassSchedule.ClassSchedule_Employee.Where(w => w.IsEnrolled).Count();
                        var seats = clsSchedule.ClassSchedule.ClassSize.GetValueOrDefault();
                        var availableSeats = seats - enrolled;

                        var empCourses = new EmpCourses(clsSchedule.ClassSchedule.ILA.Provider.Name, clsSchedule.ClassSchedule.ILA.Name, clsSchedule.ClassSchedule.ILA.Number, clsSchedule.ClassScheduleId, clsSchedule.ClassSchedule.ILA.Id, clsSchedule.ClassSchedule.StartDateTime, clsSchedule.ClassSchedule.EndDateTime, clsSchedule.ClassSchedule.Location.LocName, clsSchedule.ClassSchedule.Instructor.InstructorName);
                        empCourses.SetSeatsAvailable(availableSeats);
                        empCourses.setIsDenied(clsSchedule.IsDenied);
                        empCourses.setIsDropped(clsSchedule.IsDropped);
                        empCourses.setIsWaitListEnabled(clsSchedule.ClassSchedule.ClassSchedule_SelfRegistrationOption.EnableWaitlist);
                        empCourses.setRequiredAdminApproval(clsSchedule.ClassSchedule.ClassSchedule_SelfRegistrationOption == null ? false : clsSchedule.ClassSchedule.ClassSchedule_SelfRegistrationOption.RequireAdminApproval);
                        empCourses.setIsInWaitList(clsSchedule.IsWaitlisted);
                        list.Add(empCourses);
                    }
                    return list;
                }
                else
                {
                    throw new QTDServerException("No ClassSchedule Employees Found For Dropped Courses");
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["Employee Not Found"]);
            }

        }

        public async Task<List<EmpCourses>> GetSelfRegEmployeeApprovedCoursesByIdAsync(int employeeId)
        {
            var list = new List<EmpCourses>();
            if (employeeId != null)
            {
                var classScheduleEmployees = await _classScheduleEmployeeLinkService.GetEmployeeSelfRegistrationApprovedCourse(employeeId);
                if (classScheduleEmployees.Count > 0)
                {
                    foreach (var classSchedule in classScheduleEmployees)
                    {
                        var empCourses = new EmpCourses(classSchedule.ClassSchedule.Provider.Name, classSchedule.ClassSchedule.ILA.Name, classSchedule.ClassSchedule.ILA.Number, classSchedule.ClassScheduleId, classSchedule.ClassSchedule.ILA.Id, classSchedule.ClassSchedule.StartDateTime, classSchedule.ClassSchedule.EndDateTime, classSchedule.ClassSchedule.Location.LocName, classSchedule.ClassSchedule.Instructor.InstructorName);
                        list.Add(empCourses);
                    }
                    return list;
                }
                else
                {
                    throw new QTDServerException("No ClassSchedule Employees Found For Approved Courses");
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["Employee Not Found"]);
            }
        }

        public async Task<List<EmpCourses>> GetSelfRegEmployeeDeniedCoursesByIdAsync(int employeeId)
        {
            var list = new List<EmpCourses>();
            if (employeeId != null)
            {
                var classScheduleEmployees = await _classScheduleEmployeeLinkService.GetEmployeeSelfRegistrationDeniedCourse(employeeId);
                if (classScheduleEmployees.Count > 0)
                {
                    foreach (var classSchedule in classScheduleEmployees)
                    {
                        var empCourses = new EmpCourses(classSchedule.ClassSchedule.ILA.Provider.Name, classSchedule.ClassSchedule.ILA.Name, classSchedule.ClassSchedule.ILA.Number, classSchedule.ClassScheduleId, classSchedule.ClassSchedule.ILA.Id, classSchedule.ClassSchedule.StartDateTime, classSchedule.ClassSchedule.EndDateTime, classSchedule.ClassSchedule.Location.LocName, classSchedule.ClassSchedule.Instructor.InstructorName);
                        list.Add(empCourses);
                    }
                    return list;
                }
                else
                {
                    throw new QTDServerException("No ClassSchedule Employees Found For Denied Courses");
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["Employee Not Found"]);
            }
        }

        public async Task<CBT_ScormRegistration> GetCBT_ScormRegistrationAsync(int classScheduleId, int employeeId)
        {
            var classScheduleEmployee = await _classScheduleEmployeeLinkService.GetEmployeeForClassScheduleAsync(classScheduleId, employeeId);

            if (classScheduleEmployee == null) throw new KeyNotFoundException(_localizer["ClassScheduleEmployeeNotFound"]);

            var cbtScormRegistration = classScheduleEmployee.ScormRegistrations.FirstOrDefault(r => r.Active);

            if (cbtScormRegistration != null && !string.IsNullOrEmpty(cbtScormRegistration.LaunchLink))
            {
                cbtScormRegistration.LocalizeRegistrationLink(_scormServerSettings.Domain);
                return cbtScormRegistration;
            }

            if (cbtScormRegistration == null)
            {
                throw new InvalidOperationException("Scorm registration should already exist.");
            }

            var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, cbtScormRegistration, CBT_ScormRegistrationOperations.Update);
            if (authResult.Succeeded)
            {
                var instanceName = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == Domain.CustomClaimTypes.InstanceName).Select(c => c.Value).SingleOrDefault();
                var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
                CreateRegistrationWithLaunchLinkSchema request = new CreateRegistrationWithLaunchLinkSchema(cbtScormRegistration.CBTScormUploadId, classScheduleEmployee.EmployeeId, classScheduleEmployee.Employee.Person.FirstName, classScheduleEmployee.Employee.Person.LastName, classScheduleEmployee.Id);
                var response = await _scormEngineService.CreateRegistrationWithLaunchLinkAsync(request, instanceSettings.ScormTenant);
                cbtScormRegistration.Register(response.LaunchLink);

                if (cbtScormRegistration.LaunchLink != null)
                {
                    await _cbt_ScormRegistrationService.UpdateAsync(cbtScormRegistration);
                }
            }
            return cbtScormRegistration;
        }

        public async Task<ClassScheduleReviewDataVM> GetClassScheduleReviewData(int classId, int ilaId)
        {
            var model = new ClassScheduleReviewDataVM();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, new ClassSchedule(), ClassScheduleOperations.Read);
            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }

            //Get class Schedule Employees
            var cls = await _classScheduleService.GetAsync(classId);
            var classScheduleEmployees = await _classScheduleEmployeeLinkService.FindQuery(x => x.ClassScheduleId == classId && x.IsEnrolled).ToListAsync();
            var employeesEnrolled = classScheduleEmployees.Count();
            //Check total size
            int classSize = cls.ClassSize.GetValueOrDefault();
            var selfRegistrationOptions = await _classSchedule_SelfRegService.FindQuery(x => x.ClassScheduleId == classId).FirstOrDefaultAsync();
            var testAndPretestReleaseSettings = await _classTestReleaseEmpSettingDomainService.FindQuery(x => x.ClassScheduleId == classId).FirstOrDefaultAsync();
            var tqreleaseEmpSettings = await _tqreleaseEmpSettings.FindQuery(x => x.ILAId == ilaId).FirstOrDefaultAsync();
            var evaluationReleaseEmpSettings = await _evaluationReleaseSettings.FindQuery(x => x.ILAId == ilaId).FirstOrDefaultAsync();
            var cbtReleaseEmpSettings = await _ilaService.FindQuery(x => x.Id == ilaId).FirstOrDefaultAsync();

            var selectionMade = string.Empty;
            var retakeSelection = false;
            var pretestSelection = false;
            var testSelection = false;
            var tqSelection = false;
            var cbtSelection = false;
            var evaluationSelection = false;
            if (testAndPretestReleaseSettings == null && tqreleaseEmpSettings == null && evaluationReleaseEmpSettings == null && cbtReleaseEmpSettings.CBTRequiredForCourse == false)
            {
                selectionMade = "N/A";
            }
            if (testAndPretestReleaseSettings != null)
            {
                if (testAndPretestReleaseSettings.PreTestRequired == true)
                {
                    pretestSelection = true;
                    testSelection = true;

                }
                if (testAndPretestReleaseSettings.RetakeEnabled == true)
                {
                    retakeSelection = true;
                }
            }
            if (tqreleaseEmpSettings != null)
            {
                tqSelection = true;
            }

            if (cbtReleaseEmpSettings.CBTRequiredForCourse == true)
            {
                cbtSelection = true;
            }
            if (evaluationReleaseEmpSettings != null)
            {
                evaluationSelection = true;
            }
            //Now check Notification settings

            //var clientNotificationServiceImp = _clientNotificationService.GetClientNotificationSettings();
            var clientNotificationService = (await _clientNotificationService.AllAsync()).ToList();
            var listNotifications = new List<string>();
            var commaSepartedNotications = string.Empty;
            if (clientNotificationService != null && clientNotificationService.Count > 0)
            {

                foreach (var item in clientNotificationService)
                {
                    if (item.Enabled == true)
                    {
                        listNotifications.Add(item.Name);
                    }


                }
            }
            if (listNotifications != null && listNotifications.Count > 0)
            {
                commaSepartedNotications = string.Join(",", listNotifications);
            }
            else
            {
                commaSepartedNotications = "N/A";
            }

            if (pretestSelection)
            {
                selectionMade = "Pretest";
            }

            if (testSelection)
            {
                if (!string.IsNullOrEmpty(selectionMade))
                {
                    selectionMade += ", ";
                }
                selectionMade += "Test";
            }

            if (tqSelection)
            {
                if (!string.IsNullOrEmpty(selectionMade))
                {
                    selectionMade += ", ";
                }
                selectionMade += "TQ Release";
            }

            if (cbtSelection)
            {
                if (!string.IsNullOrEmpty(selectionMade))
                {
                    selectionMade += ", ";
                }
                selectionMade += "CBT";
            }

            if (evaluationSelection)
            {
                if (!string.IsNullOrEmpty(selectionMade))
                {
                    selectionMade += ", ";
                }
                selectionMade += "ILA Evaluation";
            }

            if (retakeSelection)
            {
                if (!string.IsNullOrEmpty(selectionMade))
                {
                    selectionMade += ", ";
                }
                selectionMade += "Retake";
            }
            if (!pretestSelection && !testSelection && !evaluationSelection && retakeSelection && !tqSelection && !cbtSelection)
            {
                selectionMade = "N/A";
            }
            model.SelectionsMadeFor = selectionMade;
            model.EnrolledStudents = employeesEnrolled;
            model.TotalStudents = classSize;
            model.NotificationsEnabledFor = commaSepartedNotications;


            return model;


        }

        public async Task<ClassSchedule_Employee> UpdateClassScheduleEnrollmentOptions(int id, ClassScheduleEnrollmentOptions options)
        {
            var classEmployee = (await _classScheduleEmployeeLinkService.FindWithIncludeAsync(x => x.EmployeeId == id && x.ClassScheduleId == options.ClassId && !x.Deleted, new[] { "ClassSchedule.ClassSchedule_TQEMPSettings", "ClassSchedule.ILA" })).FirstOrDefault();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, classEmployee, ClassScheduleEmployeeOperations.Update);
            if (result.Succeeded)
            {
                if (options.GradeNotes != null)
                {
                    classEmployee.UpdateGradeNotes(options.GradeNotes);
                }
                if (options.IsQualificationCompleted ?? false)
                {
                    classEmployee.PopulateOJTRecord = options.IsQualificationCompleted.HasValue ? options.IsQualificationCompleted.Value : false;
                    var ila = await _ilaService.GetWithIncludeAsync(classEmployee?.ClassSchedule.ILAID ?? 0, new string[] { "ILA_TaskObjective_Links.Task" });
                    var tasks = ila.ILA_TaskObjective_Links.Where(x => x.UseForTQ == true).Select(x => x.Task);
                    foreach (var task in tasks)
                    {
                        var taskqualification = (await _taskQualificationDomainService.FindAsync(x => x.TaskId == task.Id && x.EmpId == id && x.ClassScheduleId == options.ClassId)).FirstOrDefault();
                        if (taskqualification == null) continue;
                        taskqualification.TaskQualificationDate = options.completionDate?.ToUniversalTime() ?? DateTime.UtcNow;
                        taskqualification.CriteriaMet = true;
                        taskqualification.Comments = $"ILA Completion - {ila.Number}/{ila.Name}";
                        taskqualification.TQStatusId = taskqualification.TaskQualificationDate != null ? (taskqualification.TaskQualificationDate < taskqualification.DueDate ? 2 : 4) : 3;
                        var res = await _taskQualificationDomainService.UpdateAsync(taskqualification);
                        if (!res.IsValid)
                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", res.Errors)]);
                    }
                }
                classEmployee.CompleteClass(options.completionDate, options.Grade, options.Score);
                classEmployee.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                classEmployee.ModifiedDate = DateTime.Now;
                var validationResult = await _classScheduleEmployeeLinkService.UpdateAsync(classEmployee);

                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }

            }
            return classEmployee;
        }

        public ClassScheduleDetailVM MapClassScheduleToClassScheduleDetailVM(ClassSchedule cls)
        {
            return new ClassScheduleDetailVM(cls.Id, cls.ILAID, cls.InstructorId, cls.LocationId, cls.ILA?.ProviderId, cls.StartDateTime, cls.EndDateTime, cls.Location?.LocName, cls.Instructor?.InstructorName, cls.ILA?.Number, cls.ILA?.Name, cls.ILA?.IsSelfPaced, cls.Location?.LocAddress, cls.ILA?.UseForEMP, cls.IsStartAndEndTimeEmpty, cls.ClassSize, cls.IsRecurring, cls.SpecialInstructions, cls.WebinarLink, cls.ILA?.Provider?.Name, cls.IsPubliclyAvailable);
        }

        public async Task<List<PublicallyAvailableClassSchedulesVM>> GetPublicallyAvailableClassesAsync()
        {
            var classSchedules = await _classScheduleService.GetPublicallyAvailableClassSchedulesAsync();
            var countsByIla = classSchedules.GroupBy(cs => cs.ILAID).ToDictionary(g => g.Key, g => g.Count());
            var publicallyAvailableClass = new List<PublicallyAvailableClassSchedulesVM>();
            foreach (var classSchedule in classSchedules)
            {

                var latestCertificationLink = classSchedule.ILA.ILACertificationLinks.OrderByDescending(a => a.CreatedDate).FirstOrDefault();

                var publicClass = new PublicallyAvailableClassSchedulesVM
                {
                    Id = classSchedule.Id,
                    ILAId = classSchedule.ILAID,
                    StartDateTime = classSchedule.StartDateTime,
                    EndDateTime = classSchedule.EndDateTime,
                    LocationName = classSchedule.Location?.LocName,
                    InstructorName = classSchedule.Instructor?.InstructorName,
                    PublicILA = new Public_ILA_VM
                    {
                        Id = classSchedule.ILA.Id,
                        DeliveryMethodId = classSchedule.ILA.DeliveryMethodId,
                        ILAName = classSchedule.ILA.Name,
                        ILANickName = classSchedule.ILA.NickName,
                        ILANumber = classSchedule.ILA.Number,
                        DeliveryMethodName = classSchedule.ILA.DeliveryMethod?.Name,
                        CreditHours = latestCertificationLink?.CEHHours ?? 0,
                        ClassesCount = countsByIla.TryGetValue(classSchedule.ILAID, out var cnt) ? cnt : 0,
                        TotalTrainingHours = classSchedule.ILA.TotalTrainingHours ?? 0,
                        Description = classSchedule.ILA.Description,
                    }
                };
                publicallyAvailableClass.Add(publicClass);
            }

            return publicallyAvailableClass.ToList();
        }

    }
}

