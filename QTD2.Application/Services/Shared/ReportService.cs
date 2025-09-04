using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QTD2.Domain.Services.Core;
using QTD2.Infrastructure.ExtensionMethods;
using QTD2.Infrastructure.Model.TrainingProgram;

namespace QTD2.Application.Services.Shared
{
    public class ReportService : IReportsService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<ReportService> _localizer;
        private readonly Domain.Interfaces.Service.Core.IReportService _reportService;
        private readonly QTD2.Application.Interfaces.Services.Shared.ITestService _testService;
        private readonly QTD2.Application.Interfaces.Services.Shared.IEmployeeService _employeeService;
        private readonly QTD2.Application.Interfaces.Services.Shared.IStudentEvaluationFormService _studentEvaluationFormService;
        private readonly QTD2.Application.Interfaces.Services.Shared.IStudentEvaluationService _studentEvaluationService;
        private readonly QTD2.Application.Interfaces.Services.Shared.IDeliveryMethodService _deliveryMethodService;
        private readonly QTD2.Application.Interfaces.Services.Shared.IReportSkeletonService _reportSkeletonService;
        private readonly IIDP_ReviewService _IDP_reviewService;
        private readonly IIDP_ReviewStatusService _idp_ReviewStatusService;
        private readonly QTD2.Application.Interfaces.Services.Shared.IProviderService _providerService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly Domain.Interfaces.Service.Core.IOrganizationService _organizationService;
        private readonly Domain.Interfaces.Service.Core.ITask_TrainingGroupService _taskService;
        private readonly Domain.Interfaces.Service.Core.IILAService _iLAService;
        private readonly Domain.Interfaces.Service.Core.IEnablingObjectiveService _enablingObjectiveService;
        private readonly Domain.Interfaces.Service.Core.IEmployeeService _employeeServices;
        private readonly Domain.Interfaces.Service.Core.ITrainingProgramService _trainingProgramService;
        private readonly Domain.Interfaces.Service.Core.ITrainingProgramTypeService _trainingProgramTypeService;
        private readonly Domain.Interfaces.Service.Core.IPositionService _positionService;
        private readonly Domain.Interfaces.Service.Core.IClassScheduleService _classScheduleService;
        private readonly Domain.Interfaces.Service.Core.ITaskService _tasksService;
        private readonly Domain.Interfaces.Service.Core.IProcedureService _procedureService;
        private readonly Domain.Interfaces.Service.Core.IInstructor_Service _instructorService;
        private readonly Domain.Interfaces.Service.Core.IProcedureReview_EmployeeService _procedureReview_EmployeeService;
        private readonly Domain.Interfaces.Service.Core.IStudentEvaluationService _studentEvaluationDomainService;
        private readonly Domain.Interfaces.Service.Core.IMetaILAService _metaILAService;
        private readonly Domain.Interfaces.Service.Core.ITrainingProgramReviewService _trainingProgramReviewService;
        private readonly Domain.Interfaces.Service.Core.ITaskListReviewService _taskListReviewService;
        private readonly Domain.Interfaces.Service.Core.IDIFSurveyService _dIFSurveyService;
        private readonly Domain.Interfaces.Service.Core.IDIFSurvey_Task_TrainingFrequencyService _dIFSurvey_Task_TrainingFrequencyService;
        private readonly Domain.Interfaces.Service.Core.ICertificationService _certificationService;
        private readonly Domain.Interfaces.Service.Core.IPosition_TaskService _position_TaskService;
        private readonly Domain.Interfaces.Service.Core.ILocation_Service _location_Service;
        private readonly Domain.Interfaces.Service.Core.IRegulatoryRequirementService _regulatoryRequirementService;
        private readonly Domain.Interfaces.Service.Core.ITestItemTypeService _testItemTypeService;
        private readonly Domain.Interfaces.Service.Core.ITaxonomyLevelService _taxonomyLevelService;
        private readonly Domain.Interfaces.Service.Core.IEnablingObjective_CategoryService _enablingObjective_CategoryService;
        private readonly Domain.Interfaces.Service.Core.IProcedure_IssuingAuthorityService _procedure_IssuingAuthorityService;
        private readonly Domain.Interfaces.Service.Core.IEmployeeCertificationService _employeeCertificationService;
        private readonly Domain.Interfaces.Service.Core.ISaftyHazard_CategoryService _saftyHazard_CategoryService;
        private readonly Domain.Interfaces.Service.Core.ITestTypeService _testTypeService;
        private readonly Domain.Interfaces.Service.Core.ITestStatusService _testStatusService;
        private readonly Domain.Interfaces.Service.Core.ISaftyHazardService _saftyHazardService;
        private readonly Domain.Interfaces.Service.Core.ITrainingIssueService _trainingIssueService;
        private readonly Domain.Interfaces.Service.Core.ITrainingIssue_ActionItemStatusService _trainingIssue_ActionItemStatusService;
        private readonly Domain.Interfaces.Service.Core.ITrainingIssue_DriverTypeService _trainingIssue_DriverTypeService;
        private readonly Domain.Interfaces.Service.Core.ITrainingIssue_DriverSubTypeService _trainingIssue_DriverSubTypeService;
        private readonly Domain.Interfaces.Service.Core.ITrainingIssue_SeverityService _trainingIssue_SeverityService;
        private readonly Domain.Interfaces.Service.Core.ITrainingGroupService _trainingGroupService;
        private readonly Domain.Interfaces.Service.Core.ITrainingIssue_StatusService _trainingIssue_StatusService;


        public ReportService(
            Domain.Interfaces.Service.Core.IReportService reportService,
            QTD2.Application.Interfaces.Services.Shared.IReportSkeletonService reportSkeletonService,
            Domain.Interfaces.Service.Core.IPositionService positionService,
            QTD2.Application.Interfaces.Services.Shared.IEmployeeService employeeService,
            QTD2.Application.Interfaces.Services.Shared.ITestService testService,
            QTD2.Application.Interfaces.Services.Shared.IProviderService providerService,
            QTD2.Application.Interfaces.Services.Shared.IStudentEvaluationFormService studentEvaluationFormService,
            QTD2.Application.Interfaces.Services.Shared.IStudentEvaluationService studentEvaluationService,
            QTD2.Application.Interfaces.Services.Shared.IDeliveryMethodService deliveryMethodService,
            Domain.Interfaces.Service.Core.IILAService iLAService,
            IIDP_ReviewService IDP_reviewService,
            Domain.Interfaces.Service.Core.ITask_TrainingGroupService taskService,
            IHttpContextAccessor httpContextAccessor,
            IIDP_ReviewStatusService idp_ReviewStatusService,
            IAuthorizationService authorizationService,
            IStringLocalizer<ReportService> localizer,
            UserManager<AppUser> userManager,
            Domain.Interfaces.Service.Core.IOrganizationService organizationService,
            Domain.Interfaces.Service.Core.IEnablingObjectiveService enablingObjectiveService,
            Domain.Interfaces.Service.Core.IEmployeeService employeeServices,
            Domain.Interfaces.Service.Core.IClassScheduleService classScheduleService,
            Domain.Interfaces.Service.Core.ITrainingProgramService trainingProgramService,
            Domain.Interfaces.Service.Core.ITrainingProgramTypeService trainingProgramTypeService,
            Domain.Interfaces.Service.Core.ITaskService tasksService,
            Domain.Interfaces.Service.Core.IInstructor_Service instructorService,
            Domain.Interfaces.Service.Core.IProcedureService procedureService,
            Domain.Interfaces.Service.Core.IStudentEvaluationService studentEvaluationDomainService,
            Domain.Interfaces.Service.Core.IMetaILAService metaILAService,
            Domain.Interfaces.Service.Core.IProcedureReview_EmployeeService procedureReview_EmployeeService,
            Domain.Interfaces.Service.Core.ITrainingProgramReviewService trainingProgramReviewService,
            Domain.Interfaces.Service.Core.ITaskListReviewService taskListReviewService,
            Domain.Interfaces.Service.Core.IDIFSurveyService dIFSurveyService,
            Domain.Interfaces.Service.Core.IDIFSurvey_Task_TrainingFrequencyService dIFSurvey_Task_TrainingFrequencyService,
            Domain.Interfaces.Service.Core.ICertificationService certificationService,
            Domain.Interfaces.Service.Core.IPosition_TaskService position_TaskService,
            Domain.Interfaces.Service.Core.ILocation_Service location_Service,
            Domain.Interfaces.Service.Core.IRegulatoryRequirementService regulatoryRequirementService,
            Domain.Interfaces.Service.Core.ITestItemTypeService testItemTypeService,
            Domain.Interfaces.Service.Core.ITaxonomyLevelService taxonomyLevelService,
            IEnablingObjective_CategoryService enablingObjective_CategoryService,
            IProcedure_IssuingAuthorityService procedure_IssuingAuthorityService,
            IEmployeeCertificationService employeeCertificationService,
            Domain.Interfaces.Service.Core.ISaftyHazard_CategoryService saftyHazard_CategoryService,
            Domain.Interfaces.Service.Core.ITestTypeService testTypeService,
            Domain.Interfaces.Service.Core.ITestStatusService testStatusService,
            Domain.Interfaces.Service.Core.ISaftyHazardService saftyHazardService,
            Domain.Interfaces.Service.Core.ITrainingIssueService trainingIssueService,
            ITrainingIssue_ActionItemStatusService trainingIssue_ActionItemStatusService,
            ITrainingIssue_DriverTypeService trainingIssue_DriverTypeService,
            ITrainingIssue_DriverSubTypeService trainingIssue_DriverSubTypeService,
            ITrainingIssue_SeverityService trainingIssue_SeverityService,
            Domain.Interfaces.Service.Core.ITrainingGroupService trainingGroupService,
            ITrainingIssue_StatusService trainingIssue_StatusService
            )
        {
            _reportService = reportService;
            _reportSkeletonService = reportSkeletonService;
            _positionService = positionService;
            _employeeService = employeeService;
            _testService = testService;
            _providerService = providerService;
            _taskService = taskService;
            _IDP_reviewService = IDP_reviewService;
            _idp_ReviewStatusService = idp_ReviewStatusService;
            _studentEvaluationFormService = studentEvaluationFormService;
            _studentEvaluationService = studentEvaluationService;
            _deliveryMethodService = deliveryMethodService;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _iLAService = iLAService;
            _userManager = userManager;
            _organizationService = organizationService;
            _enablingObjectiveService = enablingObjectiveService;
            _employeeServices = employeeServices;
            _classScheduleService = classScheduleService;
            _trainingProgramService = trainingProgramService;
            _trainingProgramTypeService = trainingProgramTypeService;
            _tasksService = tasksService;
            _procedureService = procedureService;
            _instructorService = instructorService;
            _procedureReview_EmployeeService = procedureReview_EmployeeService;
            _metaILAService = metaILAService;
            _studentEvaluationDomainService = studentEvaluationDomainService;
            _trainingProgramReviewService = trainingProgramReviewService;
            _taskListReviewService = taskListReviewService;
            _dIFSurveyService = dIFSurveyService;
            _dIFSurvey_Task_TrainingFrequencyService = dIFSurvey_Task_TrainingFrequencyService;
            _certificationService = certificationService;
            _position_TaskService = position_TaskService;
            _location_Service = location_Service;
            _regulatoryRequirementService = regulatoryRequirementService;
            _testItemTypeService = testItemTypeService;
            _taxonomyLevelService = taxonomyLevelService;
            _enablingObjective_CategoryService = enablingObjective_CategoryService;
            _procedure_IssuingAuthorityService = procedure_IssuingAuthorityService;
            _employeeCertificationService = employeeCertificationService;
            _saftyHazard_CategoryService = saftyHazard_CategoryService;
            _testTypeService = testTypeService;
            _testStatusService = testStatusService;
            _saftyHazardService = saftyHazardService;
            _trainingIssueService = trainingIssueService;
            _trainingIssue_ActionItemStatusService = trainingIssue_ActionItemStatusService;
            _trainingIssue_DriverTypeService = trainingIssue_DriverTypeService;
            _trainingIssue_DriverSubTypeService = trainingIssue_DriverSubTypeService;
            _trainingIssue_SeverityService = trainingIssue_SeverityService;
            _trainingGroupService = trainingGroupService;
            _trainingIssue_StatusService = trainingIssue_StatusService;
        }

        public async Task<Report> CreateReportAsync(ReportCreateOrUpdateOptions options, bool saveToDatabase)
        {
            var reportSkeleton = await _reportSkeletonService.GetReportSkeletonAsync(options.ReportSkeletonId);
            var report = createReport(reportSkeleton, options);

            if (saveToDatabase)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, report, ReportOperations.Create);
                if (result.Succeeded)
                {
                    report.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    report.CreatedDate = DateTime.Now;
                    var validationResult = await _reportService.AddAsync(report);

                    if (validationResult.IsValid)
                    {
                        return report;
                    }
                    else
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: "OperationNotAllowed");
                }
            }

            return report;
        }

        public async System.Threading.Tasks.Task UpdateReportAsync(int reportId, ReportCreateOrUpdateOptions options)
        {
            var report = await GetAsync(reportId);

            var skeleton = await _reportSkeletonService.GetReportSkeletonAsync(report.ReportSkeletonId);
            updateReport(report, skeleton, options);

            report.Modify(_httpContextAccessor.HttpContext.User.Identity.Name);
            var result = await _reportService.UpdateAsync(report);
            if (!result.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', result.Errors));
            }
        }

        public async System.Threading.Tasks.Task<Report> UpdateReportLastRunAsync(int reportId, ReportCreateOrUpdateOptions options)
        {
            var report = await GetAsync(reportId);
            report.SetLastRunDate(DateTime.Now);
            var result = await _reportService.UpdateAsync(report);
            if (!result.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', result.Errors));
            }
            return report;
        }

        public async Task<List<ReportFilterOption>> GetReportFilterOptionsAsync(string filterName)
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            switch (filterName.ToLower())
            {
                case "positions":
                    return await getPositionsOptionAsync();
                case "employees":
                    return await getFilterOptionsByEmployeeAsync();
                case "organizations":
                    return await getFilterOptionsByOrganizationAsync();
                case "evaluationforms":
                    return await getFilterOptionsByEvaluationFormsAsync();
                case "providers":
                    return await getFilterOptionsForProvidersFormsAsync();
                case "taskgroupdescriptions":
                    return await getTaskGroupDescriptionsAsync();
                case "idp status":
                    return await getIdpStatusOptionsAsync();
                case "idp reviewers":
                    return await getIDPreviewersAsync();
                case "taskgroups":
                    return await getFilterOptionsByTaskGroupsAsync();
                case "activestatus":
                    return await getActiveInactiveAsync();
                case "courses":
                    return await getCoursesByILAAsync();
                case "enabling objective":
                    return await getFilterOptionsByEnablingObjectiveAsync();
                case "completedstatus":
                    return await getCompletedStatusAsync();
                case "tests":
                    return await getFilterOptionByTestsAsync();
                case "studentevaluations":
                    return await getStudentEvaluationsAsync();
                case "deliverymethods":
                    return await getDeliveryMethodsAsync();
                case "classschedule":
                    return await getClassScheduleAsync();
                case "trainingprograms":
                    return await getTrainingProgramOptionsAsync();
                case "tasks":
                    return await getTaskOptionsAsync();
                case "procedures":
                    return await getProceduresOptionsAsync();
                case "enrollmentstatus":
                    return await getEnrollmentAsync();
                case "programtypes":
                    return await getTrainingProgramTypeOptionsAsync();
                case "instructor":
                    return await getInstructorsAsync();
                case "monthnumber":
                    return getMonthNumbers();
                case "studentevaluationforms":
                    return await getStudentEvaluationFormAsync();
                case "version":
                    return await getVersionAsync();
                case "taskoptions":
                    return getTaskOptions();
                case "trainingmoduleoption":
                    return getTrainingModuleOptionAsync();
                case "trainingmodule":
                    return await getTrainingModuleAsync();
                case "trainingprogramreviews":
                    return await getTrainingProgramReviewsAsync();
                case "locationtype":
                    return await getLocationTypeAsync();
                case "completiontype":
                    return await getCompletionTypeAsync();
                case "classsigninschedule":
                    return await getClassSignInScheduleAsync();
                case "classrosterschedule":
                    return await getClassRosterScheduleAsync();
                case "tasklistreview":
                    return await getTaskListReviewAsync();
                case "tasklistreviewstatus":
                    return await getActiveInactiveTaskListReviewAsync();
                case "taskeoincludeoption":
                    return await getTaskEoIncludeOptionAsync();
                case "difsurvey":
                    return await getDifSurveyAsync();
                case "trainingfrequency":
                    return await getTrainingFrequencyAsync();
                case "trainingclasses":
                    return await getTrainingClassAsync();
                case "studenteval":
                    return await getStudentEvaluationFormTableAsync();
                case "selfpacedcourses":
                    return await getSelfPacedCoursesByILAAsync();
                case "trainingprogramtype":
                    return await getTrainingProgramTypeAsync();
                case "certifications":
                    return await getCertificatesAsync();
                case "yearfilter":
                    return await getAvailableYearsAsync();
                case "procedurereviewstatus":
                    return getProcedureReviewStatus();
                case "completionstatus":
                    return getCompletionstatus();
                case "taskr5impacted":
                    return await getR5ImpactedTasksAsync();
                case "allclassschedule":
                    return await getAllClassSchedulesAsync();
                case "onlycompletionstatus":
                    return await getonlyCompletionStatus();
                case "location":
                    return await getLocationAsync();
                case "regulatoryrequirement":
                    return await getRegulatoryRequirementAsync();
                case "testitemtypes":
                    return await getTestItemTypeOptionAsync();
                case "taxonomylevel":
                    return await getTaxonomyLevelOptionAsync();
                case "taskqualification":
                    return await getTaskQualificationAsync();
                case "classschedulewithilaproviderlocation":
                    return await getAllClassSchedulesWithIlaProviderLocationAsync();
                case "selectenablingobjective":
                    return await getEnablingObjectiveAsync();
                case "enablingobjectivecategories":
                    return await getEnablingObjectiveCategoriesAsync();
                case "selectskillqualification":
                    return await GetSkillQualificationsForSelectionAsync();
                case "issuingauthority":
                    return await getIssuingAuthorityAsync();
                case "employeewithnerccertification":
                    return await getEmployeeWithNERCCertificationOptionAsync();
                case "onlyactiveinactivestatus":
                    return getOnlyActiveInactiveStatusAsync();
                case "safetyhazardscategories":
                    return await getSafetyHazardsCategoriesAsync();
                case "selectcertificate":
                    return await getFilteredCertificatesAsync();
                case "classseswithilastatusproviderlocation":
                    return await getAllClassesWithIlaStatusProviderLocationAsync();
                case "testtype":
                    return await getTestTypeOptionAsync();
                case "teststatus":
                    return await getTestStatusOptionAsync();
                case "metaila":
                    return await getMetaILAAsync();
                case "objectivelinkedtoila":
                    return getObjectivesLinkedToILAAsync();
                case "safetyhazard":
                    return await getsafetyhazardAsync();
                case "enablingobjectivetypes":
                    return getEnablingObjectivesTypesAsync();
                case "selectilas":
                    return await getILAAsync();
                case "initialtrainingprograms":
                    return await getInitialTrainingProgramsAsync();
                case "rrttypes":
                    return await getRRTTypeAsync();
                case "trainingissue":
                    return await getTrainingIssueAsync();
                case "actionitemstepstatus":
                    return await getTrainingIssueActionItemStatusAsync();
                case "coursecompletionstatus":
                    return await getCourseCompletionTypeAsync();
                case "trainingissuecomponent":
                    return await getTrainingIssueComponentAsync();
                case "trainingissueseveritylevel":
                    return await getTrainingIssueSeveritiesAsync();
                case "trainingissuestatus":
                    return getTrainingIssueStatus();
                case "employeename":
                    return await getEmployeeNameAsync();
                case "trainingtasksgroup":
                    return await getTrainingTaskGroupAsync();
                case "tasktype":
                    return getTaskTypes();
                default:
                    throw new QTDServerException("No report filter Option found with name " + filterName);
            }
        }

        public async Task<List<ReportFilterOption>> getProceduresOptionsAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var proceduresList = await _procedureService.GetAllProceduresByIssuingAuthorityAsync();

            foreach (var procedureList in proceduresList.OrderBy(r => r.Number))
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = procedureList.Number + " - " + procedureList.Title;
                filterOption.Value = procedureList.Id.ToString();
                filterOption.Active = procedureList.Active;
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                   new ReportFilterOptionParent()
                   {
                       Name = "Issuing Authority",
                       Values = new List<string>()
                       {
                           procedureList.Procedure_IssuingAuthority.Title
                       }
                   },

                    new ReportFilterOptionParent()
                     {
                           Name="Active Status",
                           Values= new List<string>() { procedureList.Active?"Active":"Inactive"}
                     }
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }
        public async Task<List<ReportFilterOption>> getTaskOptionsAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var tasksList = await _tasksService.GetTasksAsync();

            foreach (var taskList in tasksList)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = taskList.SubdutyArea.DutyArea.Letter + " " + taskList.SubdutyArea.DutyArea.Number + "." + taskList.SubdutyArea.SubNumber + "." + taskList.Number + " - " + taskList.Description;
                filterOption.Value = taskList.Id.ToString();
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                   new ReportFilterOptionParent()
                   {
                       Name = "Duty Area",
                       Values = new List<string>()
                       {
                         taskList.SubdutyArea.DutyArea.Letter + " " + taskList.SubdutyArea.DutyArea.Number + " - " + taskList.SubdutyArea.DutyArea.Title
                       },
                       IsCascade=true
                   },
                   new ReportFilterOptionParent()
                   {
                       Name = "SubDuty Area",
                       Values = new List<string>()
                       {
                          taskList.SubdutyArea.DutyArea.Letter + " " + taskList.SubdutyArea.DutyArea.Number + "." + taskList.SubdutyArea.SubNumber + " - " + taskList.SubdutyArea.Title
                       }
                   },
                    new ReportFilterOptionParent()
                   {
                       Name = "Task Status",
                       Values= new List<string>() {taskList.Active?"Active":"Inactive"}
                   },
                    new ReportFilterOptionParent()
                    {
                        Name = "R-R Tasks",
                        Values = new List<string>(){taskList.IsReliability ? "true":"false"},
                        IsTableVisible=false,
                        ControlType = "Checkbox"

                    }
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getPositionsOptionAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var positionList = await _positionService.AllAsync();
            var sortedPositions = positionList.OrderBy(p => p.PositionNumber).ThenBy(p => p.PositionTitle).ToList();

            foreach (var position in sortedPositions)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = position.PositionNumber.ToString() + " - " + position.PositionTitle;
                filterOption.Value = position.Id.ToString();
                filterOption.Active = position.Active;
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                     new ReportFilterOptionParent()
                     {
                           Name="Active Status",
                           Values= new List<string>() {position.Active?"Active":"Inactive"}

                     }
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getFilterOptionByTestsAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var testList = await _testService.GetILAWithAllTestAsync();
            var orderedTestList = testList.OrderByDescending(x => x.ILATraineeEvaluations.Select(m => m.ILA?.Provider?.IsPriority).FirstOrDefault()).ThenBy(x => x.ILATraineeEvaluations.Select(m => m.ILA?.Provider?.Name).FirstOrDefault());
            foreach (var test in orderedTestList)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = test.TestTitle;
                filterOption.Value = test.Id.ToString();
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                   new ReportFilterOptionParent()
                   {
                       Name = "Provider",
                       Values = test.ILATraineeEvaluations.Select(r => r.ILA.Provider.Name).ToList(),
                       IsTableVisible = false,
                       IsCascade = true
                   },
                   new ReportFilterOptionParent()
                   {
                       Name = "ILA",
                       Values = test.ILATraineeEvaluations.Select(r => r.ILA.Name).ToList(),
                       IsTableVisible = false
                   },
                   new ReportFilterOptionParent()
                    {
                       Name = "Status",
                       Values = new List<string>() { test.Active ? "Active" : "Inactive" }
                    }
                };
                filterOption.FilterTableColumns = new List<ReportFilterTableColumns>()
                {
                   new ReportFilterTableColumns()
                   {
                       Name = "Test Type",
                       Values = test.ILATraineeEvaluations.Select(r => r.TestType?.Description).ToList()
                   }
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }
        public async Task<List<ReportFilterOption>> getFilterOptionsByEmployeeAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var employeeList = await _employeeServices.GetEmployeesWithPersonAsync();
            var employeesWithOrgnaization = await _employeeServices.GetEmployeesWithOrganizationAsync();
            var employeesWithPositions = await _employeeServices.GetAllEmployeesWithPositionsAsync();

            foreach (var employee in employeeList)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = employee.Person.FirstName + " " + employee.Person.LastName;
                filterOption.Value = employee.Id.ToString();
                filterOption.Active = employee.Active;
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                    {
                       new ReportFilterOptionParent()
                       {
                           Name = "Organization",
                           Values = employeesWithOrgnaization.Where(r => r.Id == employee.Id).First().EmployeeOrganizations.Where(r => r.Active).Select(r => r.Organization.Name).Distinct().ToList()
                       },
                       new ReportFilterOptionParent()
                       {
                           Name = "Position",
                           Values = employeesWithPositions.Where(r => r.Id == employee.Id).First().EmployeePositions.Where(r => r.Active).Select(c => c.Position.PositionTitle).Distinct().ToList()
                       },
                      new ReportFilterOptionParent()
                      {
                           Name="Status",
                            Values= new List<string>() {employee.Active?"Active":"Inactive"}
                      },
                    };

                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getFilterOptionsByOrganizationAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var organizationList = await _organizationService.AllAsync();
            foreach (var organization in organizationList)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = organization.Name;
                filterOption.Value = organization.Id.ToString();
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getIDPreviewersAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var idpreviewers = await _IDP_reviewService.AllAsync();

            foreach (var reviewer in idpreviewers)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = reviewer.Title;
                filterOption.Value = reviewer.Id.ToString();
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getFilterOptionsByTaskGroupsAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var taskGroups = await _taskService.AllWithIncludeAsync(new string[] { "Task" });

            foreach (var status in taskGroups)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = status.Task.Description.ToString(); ;
                filterOption.Value = status.Id.ToString();
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getIdpStatusOptionsAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var idpStatus = await _idp_ReviewStatusService.AllAsync();
            foreach (var status in idpStatus)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = status.Name;
                filterOption.Value = status.Id.ToString();
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getTaskGroupDescriptionsAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            string[] task = new string[] { "All Task Groups including ''Task not assigned to a Group'", "All Assigned Task Groups", "Single Task Group(Choose Below)" };

            for (int i = 0; i < task.Count(); i++)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = task[i];
                filterOption.Value = task[i].ToString();
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getActiveInactiveAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            string[] options = new string[] { "All", "Active Only", "Inactive Only" };
            for (int i = 0; i < options.Count(); i++)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = options[i];
                filterOption.Value = options[i];
                filterOption.IsDefaultOrder = true;
                filterOptions.Add(filterOption);
            }

            return filterOptions;
        }

        public async Task<List<ReportFilterOption>> getCompletedStatusAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            string[] options = new string[] { "Completed", "Not Completed", "All" };
            for (int i = 0; i < options.Count(); i++)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = options[i];
                filterOption.Value = options[i];
                filterOptions.Add(filterOption);
            }
            return filterOptions;
        }

        public async Task<List<ReportFilterOption>> getStudentEvaluationsAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var evaluationsList = await _studentEvaluationService.GetAsync();
            foreach (var evaluation in evaluationsList)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = evaluation.Title;
                filterOption.Value = evaluation.Id.ToString();
                filterOptions.Add(filterOption);
            }
            return filterOptions;
        }

        public async Task<List<ReportFilterOption>> getDeliveryMethodsAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var deliveryMethodList = await _deliveryMethodService.GetAsync();
            foreach (var deliveryMethod in deliveryMethodList)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = deliveryMethod.Name;
                filterOption.Value = deliveryMethod.Id.ToString();
                filterOptions.Add(filterOption);
            }
            return filterOptions;
        }

        public async Task<List<ReportFilterOption>> getFilterOptionsByEvaluationFormsAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var evalFormsList = await _studentEvaluationFormService.GetAsync();
            foreach (var form in evalFormsList)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = form.Name;
                filterOption.Value = form.Id.ToString();
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getFilterOptionsForProvidersFormsAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var providersList = await _providerService.GetAsync();
            var orderedProviders = providersList.OrderByDescending(p => p.IsPriority).ThenBy(name => name?.Name);
            foreach (var provider in orderedProviders)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = provider.Name;
                filterOption.Value = provider.Id.ToString();
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getCoursesByILAAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var coursesList = await _iLAService.GetCoursesByILAAsync();
            var orderedCourses = coursesList
                .OrderByDescending(course => course.Provider.IsPriority)
                .ThenBy(course => course.Provider.Name)
                .ThenBy(course => course.Number);

            foreach (var course in orderedCourses)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = course.Number + " - " + course.Name;
                filterOption.Value = course.Id.ToString();
                filterOption.Active = course.Active;
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                   new ReportFilterOptionParent()
                   {
                       Name = "Provider",
                        Values =new List<string>()
                           {
                               course.Provider.Name
                           },
                    },
                   new ReportFilterOptionParent()
                   {
                       Name="Active Status",
                      Values= new List<string>() {course.Active?"Active":"Inactive" }
                   }
                };

                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getStudentEvaluationFormAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var studentEvaluationList = await _studentEvaluationDomainService.GetAllStudentEvaluationsAsync();

            foreach (var studentEvaluation in studentEvaluationList)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = studentEvaluation.Title;
                filterOption.Value = studentEvaluation.Id.ToString();
                filterOption.Active = studentEvaluation.Active;
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                   new ReportFilterOptionParent()
                   {
                       Name = "Provider",
                       Values =  studentEvaluation.ILA_StudentEvaluation_Links.Select(r => r.ILA.Provider?.Name).Distinct().ToList(),
                       IsCascade=true
                    },
                   new ReportFilterOptionParent()
                   {
                       Name = "ILA",
                       Values =  studentEvaluation.ILA_StudentEvaluation_Links.Select(r => r.ILA.Name).Distinct().ToList(),
                   },
                };

                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }
        public async Task<List<ReportFilterOption>> getFilterOptionsByEnablingObjectiveAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var enablingObjectiveList = await _enablingObjectiveService.AllAsync();
            foreach (var objective in enablingObjectiveList)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = objective.Description;
                filterOption.Value = objective.Id.ToString();
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getTrainingProgramOptionsAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var trainingPrograms = await _trainingProgramService.GetAllTrainingProgramAsync();
            foreach (var trainingProgram in trainingPrograms)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = trainingProgram.ProgramTitle;
                filterOption.Value = trainingProgram.Id.ToString();
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                   new ReportFilterOptionParent()
                   {
                       Name = "Position",
                       Values = new List<string>()
                       {
                           trainingProgram.Position.PositionTitle
                       }
                    }
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getTrainingProgramTypeOptionsAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var trainingProgramTypes = await _trainingProgramTypeService.GetAllTrainingProgramTypesAsync();
            foreach (var trainingProgramType in trainingProgramTypes)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = trainingProgramType.TrainingProgramTypeTitle;
                filterOption.Value = trainingProgramType.Id.ToString();

                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }


        public async Task<List<ReportFilterOption>> getClassScheduleAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var ClassScheduleList = await _classScheduleService.GetAllClassSchedulesAsync();
            var orderedClasses = ClassScheduleList.OrderByDescending(cs => cs.ILA.Provider?.IsPriority).ThenBy(name => name?.ILA?.Provider?.Name);
            foreach (var classSchedule in orderedClasses)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = classSchedule.ILA.Name + " - " + classSchedule.StartDateTime;
                filterOption.Value = classSchedule.Id.ToString();
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                   new ReportFilterOptionParent()
                   {
                       Name = "Provider",
                       Values = new List<string>()
                       {
                           classSchedule.ILA.Provider?.Name
                       }
                   },
                   new ReportFilterOptionParent()
                   {
                       Name = "ILA",
                       Values = new List<string>()
                       {
                           classSchedule.ILA.Name
                       }
                   },
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getClassRosterScheduleAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var ClassScheduleList = await _classScheduleService.GetAllInstructorLedClassSchedulesAsync();
            var orderedClasses = ClassScheduleList.OrderByDescending(cs => cs.StartDateTime).ToList();
            foreach (var classSchedule in orderedClasses)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = classSchedule.ILA.Name + " - " + classSchedule.StartDateTime;
                filterOption.Value = classSchedule.Id.ToString();
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                   new ReportFilterOptionParent()
                   {
                       Name = "Provider",
                       Values = new List<string>()
                       {
                           classSchedule.ILA.Provider?.Name
                       },
                       IsCascade = true
                   },
                   new ReportFilterOptionParent()
                   {
                       Name = "ILA",
                       Values = new List<string>()
                       {
                           classSchedule.ILA.Name
                       },
                       IsCascade = true
                   },
                   new ReportFilterOptionParent()
                   {
                       Name = "Location",
                        Values = new List<string>()
                       {
                           classSchedule.Location?.LocName
                       },
                   },
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();

        }

        public async Task<List<ReportFilterOption>> getClassSignInScheduleAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var ClassScheduleList = await _classScheduleService.GetAllInstructorLedClassSchedulesAsync();
            var orderedClasses = ClassScheduleList.OrderByDescending(cs => cs.ILA.Provider?.IsPriority).ThenBy(name => name?.ILA?.Provider?.Name);
            foreach (var classSchedule in orderedClasses)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = classSchedule.ILA.Name + " - " + classSchedule.StartDateTime;
                filterOption.Value = classSchedule.Id.ToString();
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                   new ReportFilterOptionParent()
                   {
                       Name = "Provider",
                       Values = new List<string>()
                       {
                           classSchedule.ILA.Provider?.Name
                       },
                       IsCascade = true
                   },
                   new ReportFilterOptionParent()
                   {
                       Name = "ILA",
                       Values = new List<string>()
                       {
                           classSchedule.ILA.Name
                       },
                       IsCascade = true
                   },
                   new ReportFilterOptionParent()
                   {
                       Name = "Location",
                        Values = new List<string>()
                       {
                           classSchedule.Location?.LocName
                       },
                   }
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getEnrollmentAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            string[] options = new string[] { "All Enrolled Students", "Completed", "Not Completed" };
            for (int i = 0; i < options.Count(); i++)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = options[i];
                filterOption.Value = options[i];
                filterOptions.Add(filterOption);
            }
            return filterOptions;
        }

        public async Task<List<ReportFilterOption>> getInstructorsAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var instructorList = await _instructorService.GetAllInstructorsAsync();
            foreach (var instructor in instructorList)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = instructor.InstructorName;
                filterOption.Value = instructor.Id.ToString();
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public List<ReportFilterOption> getMonthNumbers()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();

            for (int i = 0; i < 25; i++)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = i.ToString();
                filterOption.Value = i.ToString();
                filterOptions.Add(filterOption);

            }

            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getVersionAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var trainingPrograms = await _trainingProgramService.GetAllTrainingProgramWithTypesAsync();
            HashSet<string> encounteredNames = new HashSet<string>();
            foreach (var program in trainingPrograms)
            {
                string name = program.Version;
                if (!encounteredNames.Contains(name))
                {
                    ReportFilterOption filterOption = new ReportFilterOption();
                    filterOption.Name = name;
                    filterOption.Value = program.Id.ToString();
                    filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                    {
                        new ReportFilterOptionParent()
                        {
                            Name = "TrainingType",
                            Values = new List<string>()
                            {
                                program.TrainingProgramType.TrainingProgramTypeTitle
                            }
                        },
                    };
                    filterOptions.Add(filterOption);
                    encounteredNames.Add(name);
                }
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getTrainingProgramReviewsAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var trainingProgramReviews = await _trainingProgramReviewService.GetAllAsync();
            foreach (var trainingProgramReview in trainingProgramReviews)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = String.Format("{0} - {1}", trainingProgramReview.StartDate.GetValueOrDefault().ToShortDateString(), trainingProgramReview.EndDate.GetValueOrDefault().ToShortDateString());
                filterOption.Value = trainingProgramReview.Id.ToString();
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                   new ReportFilterOptionParent()
                   {
                       Name = "Position",
                       Values = new List<string>()
                       {
                           trainingProgramReview.TrainingProgram.Position.PositionTitle,
                       },
                       IsCascade =true
                   },
                    new ReportFilterOptionParent()
                   {
                       Name = "Program Type",
                       Values = new List<string>()
                       {
                           trainingProgramReview.TrainingProgram.TrainingProgramType.TrainingProgramTypeTitle,
                       },
                       IsCascade =true
                   },
                   new ReportFilterOptionParent()
                    {
                        Name = "Version / Start Date / Year",
                        Values = new List<string>()
                        {
                            trainingProgramReview.TrainingProgram.Version,
                        }

                    },
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getLocationTypeAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            string[] options = new string[] { "All", "Classroom Based", "Self Paced" };
            for (int i = 0; i < options.Count(); i++)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = options[i];
                filterOption.Value = options[i];
                filterOptions.Add(filterOption);
            }
            return filterOptions;
        }
        public async Task<List<ReportFilterOption>> getTrainingProgramTypeAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var trainingPrograms = await _trainingProgramService.GetAllAsync();
            foreach (var trainingProgram in trainingPrograms)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = trainingProgram?.Position?.PositionTitle +" - "+ trainingProgram.Version;
                filterOption.Value = trainingProgram.Id.ToString();
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                   new ReportFilterOptionParent()
                   {
                       Name = "Position",
                       Values = new List<string>()
                       {
                           trainingProgram?.Position?.PositionTitle,
                       },
                       IsCascade =true
                   },
                    new ReportFilterOptionParent()
                   {
                       Name = "Training Program Type",
                       Values = new List<string>()
                       {
                           trainingProgram?.TrainingProgramType?.TrainingProgramTypeTitle,
                       },
                       IsCascade =true
                   },
                   new ReportFilterOptionParent()
                    {
                        Name = "Status",
                        Values= new List<string>() { trainingProgram.Active ? "Active":"Inactive"}

                    },
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getAvailableYearsAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            int startYear = 2006;
            int currentYear = DateTime.Now.Year;

            for (int year = startYear; year <= currentYear; year++)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = year.ToString();
                filterOption.Value = year.ToString();
                filterOptions.Add(filterOption);
            }

            return filterOptions;
        }

        public async Task<List<ReportFilterOption>> getTaskQualificationAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var employeeList = await _employeeServices.GetEmployeesWithPersonForTQEvaluatorRoleAsync();
            var employeesWithOrgnaization = await _employeeServices.GetEmployeesWithOrganizationAsync();
            var employeesWithPositions = await _employeeServices.GetAllEmployeesWithPositionsAsync();

            foreach (var employee in employeeList)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = employee.Person.FirstName + " " + employee.Person.LastName;
                filterOption.Value = employee.Id.ToString();
                filterOption.Active = employee.Active;
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                    {
                       new ReportFilterOptionParent()
                       {
                           Name = "Organization",
                           Values = employeesWithOrgnaization.Where(r => r.Id == employee.Id).First().EmployeeOrganizations.Where(r => r.Active).Select(r => r.Organization.Name).Distinct().ToList()
                       },
                       new ReportFilterOptionParent()
                       {
                           Name = "Position",
                           Values = employeesWithPositions.Where(r => r.Id == employee.Id).First().EmployeePositions.Where(r => r.Active).Select(c => c.Position.PositionTitle).Distinct().ToList()
                       },
                      new ReportFilterOptionParent()
                      {
                           Name="Status",
                           Values= new List<string>() {employee.Active?"Active":"Inactive"}
                      },
                    };

                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<IEnumerable<Report>> GetAllAsync()
        {
            return await _reportService.GetAllActiveAsync();
        }

        public async Task<Report> GetAsync(int reportId)
        {
            return await _reportService.GetWithIncludeAsync(reportId, new string[] { "Filters", "DisplayColumns" });
        }

        public async Task<Report> GetAsync(int reportId, ReportCreateOrUpdateOptions options)
        {
            var report = await GetAsync(reportId);

            if (options != null)
            {
                var skeleton = await _reportSkeletonService.GetReportSkeletonAsync(report.ReportSkeletonId);
                updateReport(report, skeleton, options);
            }

            return report;
        }

        protected Report createReport(ReportSkeleton reportSkeleton, ReportCreateOrUpdateOptions options)
        {
            var report = new Report(options.ReportSkeletonId, options.InternalReportTitle, reportSkeleton.DefaultTitle);
            if (reportSkeleton.DisplayColumns.Count == 0)
            {
                report.EnableDisplayColumn("");
            }
            foreach (var column in reportSkeleton.DisplayColumns)
            {

                if (options.DisplayColumns.Where(r => r.ToUpper().Trim() == column.ColumnName.ToUpper().Trim()).Any())
                {
                    report.EnableDisplayColumn(column.ColumnName);
                }
                else
                {
                    report.DisableDisplayColumn(column.ColumnName);
                }
            }

            foreach (var filter in reportSkeleton.AvailableFilters)
            {
                var optionFilter = options.Filters.Where(r => r.Name.ToUpper().Trim() == filter.Name.ToUpper().Trim()).FirstOrDefault();
                if (optionFilter != null)
                {
                    report.AddFilter(filter, optionFilter.Name, optionFilter.Value);
                }
            }

            return report;
        }

        protected void updateReport(Report report, ReportSkeleton skeleton, ReportCreateOrUpdateOptions options)
        {
            foreach (var col in skeleton.DisplayColumns)
            {
                report.DisableDisplayColumn(col.ColumnName);
                if ((options.DisplayColumns.Where(r => r == col.ColumnName)).Count() > 0)
                {
                    report.EnableDisplayColumn(col.ColumnName);
                }
            }

            foreach (var option in options.Filters ?? new List<Infrastructure.Model.Reports.ReportFilter>())
            {
                report.ClearFilter(option.Name);
                var skeletonFilter = skeleton.GetFilter(option.Name);
                report.AddFilter(skeletonFilter, option.Name, option.Value);
            }

            if (!String.IsNullOrEmpty(options.InternalReportTitle))
            {
                report.SetInternalReportTitle(options.InternalReportTitle);
            }
        }

        public List<ReportFilterOption> getTaskOptions()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            string[] options = new string[] { "All Active Tasks", "Inactive Tasks Only", "R5 Impact", "R-R* Tasks Only", "Non R-R* Tasks Only", "Reliability Impact Tasks Only (R6)", "Meta Tasks Only" };
            for (int i = 0; i < options.Count(); i++)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = options[i];
                filterOption.Value = options[i];
                filterOptions.Add(filterOption);
            }
            return filterOptions;
        }

        public async Task<List<ReportFilterOption>> getTrainingModuleAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var metaIlasList = await _metaILAService.GetMetaILAsActiveAsync();

            foreach (var metaILA in metaIlasList)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = metaILA.Name;
                filterOption.Value = metaILA.Id.ToString();
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }


        public List<ReportFilterOption> getTrainingModuleOptionAsync()
        {

            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            string[] options = new string[] { "Completed", "Not Completed" };
            for (int i = 0; i < options.Count(); i++)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = options[i];
                filterOption.Value = options[i];
                filterOptions.Add(filterOption);
            }
            return filterOptions;
        }

        public async Task<List<ReportFilterOption>> getCompletionTypeAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            string[] options = new string[] { "Completed within Date Range", "Not Completed within Date Range" };
            for (int i = 0; i < options.Count(); i++)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = options[i];
                filterOption.Value = options[i];
                filterOptions.Add(filterOption);
            }
            return filterOptions;
        }

        public async Task<List<ReportFilterOption>> getTaskListReviewAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var taskListReviews = await _taskListReviewService.GetAllAsync();
            foreach (var taskList in taskListReviews)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = taskList.Title;
                filterOption.Value = taskList.Id.ToString();
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getActiveInactiveTaskListReviewAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            string[] options = new string[] { "Active", "Inactive", "All" };
            for (int i = 0; i < options.Count(); i++)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = options[i];
                filterOption.Value = options[i];
                filterOptions.Add(filterOption);
            }
            return filterOptions;
        }

        public async Task<List<ReportFilterOption>> getTaskEoIncludeOptionAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            string[] options = new string[] { "Include Tasks","Include Meta Tasks", "Include Enabling Objectives", "Include Skill Qualifications", "Include Meta Enabling Objectives", "Include Custom Objectives" };
            for (int i = 0; i < options.Count(); i++)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = options[i];
                filterOption.Value = (i + 1).ToString();
                filterOptions.Add(filterOption);
            }
            return filterOptions;
        }

        public async Task<List<ReportFilterOption>> getDifSurveyAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var difSurveys = await _dIFSurveyService.GetAllAsync();
            foreach (var difSurvey in difSurveys)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = difSurvey.Title;
                filterOption.Value = difSurvey.Id.ToString();
                filterOption.Active = difSurvey.Active;
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                   new ReportFilterOptionParent()
                   {
                       Name = "Position",
                       Values = new List<string>()
                       {
                           difSurvey.Position.PositionTitle,
                       },
                      IsTableVisible=false
                   },
                    new ReportFilterOptionParent()
                   {
                       Name = "Survey Status",
                       Values = new List<string>()
                       {
                             difSurvey.SurveyStatus
                       },
                   },
                   new ReportFilterOptionParent()
                   {
                       Name="Active Status",
                      Values= new List<string>() {  difSurvey.Active?"Active":"Inactive"  }
                   }
                };
                filterOption.FilterTableColumns = new List<ReportFilterTableColumns>()
                {
                    new ReportFilterTableColumns()
                    {
                        Name = "Start Date",
                        Values = new List<string> { difSurvey.StartDate.ToString("MM/dd/yyyy") }
                    },
                    new ReportFilterTableColumns()
                    {
                        Name = "Due Date",
                        Values = new List<string> { difSurvey.DueDate.ToString("MM/dd/yyyy") }
                    }
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getTrainingFrequencyAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();

            var dIFSurvey_Task_TrainingFrequencies = await _dIFSurvey_Task_TrainingFrequencyService.GetAllAsync();
            ReportFilterOption filterOptionAll = new ReportFilterOption();
            filterOptionAll.Name = "All";
            filterOptionAll.Value = "0";
            filterOptionAll.IsDefaultOrder = true;
            filterOptions.Add(filterOptionAll);
            foreach (var dIFSurvey_Task_Training in dIFSurvey_Task_TrainingFrequencies)
            {
                ReportFilterOption filterOption = new ReportFilterOption();

                filterOption.Name = dIFSurvey_Task_Training.Status;
                filterOption.Value = dIFSurvey_Task_Training.Id.ToString();
                filterOption.IsDefaultOrder = true;
                filterOptions.Add(filterOption);
            }

            return filterOptions;
        }

        public async Task<List<ReportFilterOption>> getTrainingClassAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var classSchedules = await _classScheduleService.GetAllAsync();
            foreach (var classSchedule in classSchedules)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = classSchedule?.ILA?.Name ?? "";
                filterOption.Value = classSchedule.Id.ToString();
                filterOption.Active = classSchedule.Active;
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                   new ReportFilterOptionParent()
                   {
                       Name = "Provider",
                       Values = new List<string>()
                       {
                           classSchedule?.ILA?.Provider?.Name
                       },
                       IsTableVisible =false,
                       IsCascade =true
                   },
                   new ReportFilterOptionParent()
                   {
                       Name = "ILA",
                       Values = new List<string>()
                       {
                           classSchedule?.ILA?.Name
                       },
                       IsTableVisible =false,
                       IsCascade =true
                   },
                   new ReportFilterOptionParent()
                   {
                       Name = "Instructor",
                       Values = new List<string>()
                       {
                           classSchedule?.Instructor?.InstructorName
                       },
                       IsCascade =true
                   },
                   new ReportFilterOptionParent()
                   {
                       Name = "Location",
                       Values = new List<string>()
                       {
                           classSchedule?.Location?.LocName
                       }
                   },
                };
                filterOption.FilterTableColumns = new List<ReportFilterTableColumns>()
                {
                    new ReportFilterTableColumns()
                    {
                        Name = "Class Start Date",
                        Values = new List<string>
                        {
                            $"{classSchedule.StartDateTime.ToString("MM/dd/yyyy")} - {classSchedule.EndDateTime.ToString("MM/dd/yyyy")}"
                        }
                    }
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getStudentEvaluationFormTableAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var studentEvaluationList = await _studentEvaluationDomainService.GetAllStudentEvaluationsAsync();

            foreach (var studentEvaluation in studentEvaluationList)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = studentEvaluation.Title;
                filterOption.Value = studentEvaluation.Id.ToString();
                filterOption.Active = studentEvaluation.Active;
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                   new ReportFilterOptionParent()
                   {
                       Name = "ILA",
                       Values =  studentEvaluation.ILA_StudentEvaluation_Links.Select(r => r.ILA.Name).Distinct().ToList()
                   },
                };

                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getSelfPacedCoursesByILAAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var coursesList = await _iLAService.GetSelfPacedCoursesByILAAsync();
            var orderedCourses = coursesList
                .OrderByDescending(course => course.Provider.IsPriority)
                .ThenBy(course => course.Provider.Name)
                .ThenBy(course => course.Number);

            foreach (var course in orderedCourses)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = course.Number + " - " + course.Name;
                filterOption.Value = course.Id.ToString();
                filterOption.Active = course.Active;
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                   new ReportFilterOptionParent()
                   {
                       Name = "Provider",
                        Values =new List<string>()
                           {
                               course.Provider.Name
                           },
                    },
                   new ReportFilterOptionParent()
                   {
                       Name="Active Status",
                      Values= new List<string>() {course.Active?"Active":"Inactive" }
                   }
                };

                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getCertificatesAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var certificates = await _certificationService.GetCertificationListAsync();

            foreach (var cert in certificates)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = cert.Name;
                filterOption.Value = cert.Id.ToString();
                filterOption.Active = cert.Active;
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                    new ReportFilterOptionParent()
                     {
                       Name = "Certifying Body",
                       Values =new List<string>()
                           {
                               cert.CertifyingBody.Name
                           },
                     },
                    new ReportFilterOptionParent()
                    {
                       Name = "Status",
                       Values =new List<string>(){cert.Active ? "Active" : "InActive"},
                    }
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public List<ReportFilterOption> getProcedureReviewStatus()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            string[] options = new string[] { "All", "Published", "Draft" };
            for (int i = 0; i < options.Count(); i++)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = options[i];
                filterOption.Value = options[i];
                filterOptions.Add(filterOption);
            }
            return filterOptions;
        }

        public List<ReportFilterOption> getCompletionstatus()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            string[] options = new string[] { "All", "Completed", "In Progress", "Pending" };
            for (int i = 0; i < options.Count(); i++)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = options[i];
                filterOption.Value = options[i];
                filterOptions.Add(filterOption);
            }
            return filterOptions;
        }

        public async Task<List<ReportFilterOption>> getR5ImpactedTasksAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var position_Tasks = await _position_TaskService.GetPositionTaskAsync();

            foreach (var positionTask in position_Tasks)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = positionTask.Task.SubdutyArea?.DutyArea?.Letter + " " + positionTask.Task.SubdutyArea?.DutyArea?.Number + "." + positionTask.Task.SubdutyArea?.SubNumber + "." + positionTask.Task.Number + " - " + positionTask.Task?.Description ?? "";
                filterOption.Value = positionTask.Id.ToString();
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                    new ReportFilterOptionParent()
                   {
                       Name = "Position",
                       Values = new List<string>()
                       {
                         positionTask?.Position?.PositionTitle ?? ""
                       },
                       IsCascade=true,
                       IsTableVisible=false
                   },
                      new ReportFilterOptionParent()
                   {
                       Name = "Task Status",
                       Values= new List<string>() { positionTask.Active ? "Active":"Inactive"}
                   },
                };
                filterOption.FilterTableColumns = new List<ReportFilterTableColumns>()
                {
                    new ReportFilterTableColumns()
                    {
                        Name = "Duty Area",
                        Values = new List<string>
                        {
                            positionTask.Task.SubdutyArea?.DutyArea?.Letter + positionTask.Task.SubdutyArea?.DutyArea?.Number + " - " + positionTask.Task.SubdutyArea?.DutyArea?.Title
                        }
                    },
                    new ReportFilterTableColumns()
                    {
                        Name = "Sub Duty Area",
                        Values = new List<string>
                        {
                            positionTask.Task.SubdutyArea?.DutyArea?.Letter + positionTask.Task.SubdutyArea?.DutyArea?.Number + "." + positionTask.Task.SubdutyArea?.SubNumber + " - " + positionTask.Task.SubdutyArea?.Title
                        }
                    }
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getAllClassSchedulesAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var ClassScheduleList = await _classScheduleService.GetAllAsync();
            var orderedClasses = ClassScheduleList.OrderByDescending(cs => cs?.ILA?.Provider?.IsPriority).ThenBy(name => name?.ILA?.Provider?.Name);
            foreach (var classSchedule in orderedClasses)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = classSchedule?.ILA?.Name + " - " + classSchedule?.StartDateTime;
                filterOption.Value = classSchedule?.Id.ToString();
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                   new ReportFilterOptionParent()
                   {
                       Name = "Provider",
                       Values = new List<string>()
                       {
                           classSchedule?.ILA?.Provider?.Name
                       },
                       IsCascade=true
                   },
                   new ReportFilterOptionParent()
                   {
                       Name = "ILA",
                       Values = new List<string>()
                       {
                           classSchedule?.ILA?.Name
                       }
                   },
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }
        public async Task<List<ReportFilterOption>> getonlyCompletionStatus()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            string[] options = new string[] { "Completed Only", "Not Completed Only", "All" };
            for (int i = 0; i < options.Count(); i++)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = options[i];
                filterOption.Value = options[i];
                filterOptions.Add(filterOption);
            }
            return filterOptions;
        }

        public async Task<List<ReportFilterOption>> getLocationAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var locationList = await _location_Service.GetAllLocationAsync();
            foreach (var location in locationList)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = location.LocName;
                filterOption.Value = location.Id.ToString();
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }
        public async Task<List<ReportFilterOption>> getRegulatoryRequirementAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var regulatoryRequirements = await _regulatoryRequirementService.GetRegulatoryRequirementsWithIssuingAuthorityAsync();

            foreach (var regulatoryList in regulatoryRequirements)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = regulatoryList.Number + " - " + regulatoryList.Title;
                filterOption.Value = regulatoryList.Id.ToString();
                filterOption.Active = regulatoryList.Active;
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                  {
                  new ReportFilterOptionParent()
                  {
                      Name = "Issuing Authority",
                      Values = new List<string>()
                      {
                           regulatoryList.RR_IssuingAuthority.Title
                      }
                  },

                  new ReportFilterOptionParent()
                  {
                      Name="Active Status",
                      Values= new List<string>() { regulatoryList.Active?"Active":"Inactive"}
                  }
             };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getTestItemTypeOptionAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var testItemTypes = await _testItemTypeService.AllAsync();
            foreach (var item in testItemTypes)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = item.Description;
                filterOption.Value = item.Id.ToString();
                filterOptions.Add(filterOption);
            }
            return filterOptions;
        }

        public async Task<List<ReportFilterOption>> getTaxonomyLevelOptionAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var taxonomyLevels = await _taxonomyLevelService.AllAsync();
            foreach (var item in taxonomyLevels)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = item.Description;
                filterOption.Value = item.Id.ToString();
                filterOptions.Add(filterOption);
            }
            return filterOptions;
        }

        public async Task<List<ReportFilterOption>> getAllClassSchedulesWithIlaProviderLocationAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var ClassScheduleList = await _classScheduleService.GetAllSelfPacedClassSchedulesAsync();
            var orderedClasses = ClassScheduleList.OrderByDescending(cs => cs?.ILA?.Provider?.IsPriority).ThenBy(name => name?.ILA?.Provider?.Name);
            foreach (var classSchedule in orderedClasses)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = classSchedule?.ILA?.Name + " - " + classSchedule?.StartDateTime;
                filterOption.Value = classSchedule?.Id.ToString();
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                   new ReportFilterOptionParent()
                   {
                       Name = "Provider",
                       Values = new List<string>()
                       {
                           classSchedule?.ILA?.Provider?.Name
                       },
                       IsCascade=true
                   },
                   new ReportFilterOptionParent()
                   {
                       Name = "ILA",
                       Values = new List<string>()
                       {
                           classSchedule?.ILA?.Name
                       },
                       IsCascade = true
                   },
                    new ReportFilterOptionParent()
                   {
                       Name = "Location",
                       Values = new List<string>()
                       {
                           classSchedule?.Location?.LocName
                       }
                   },
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getEnablingObjectiveAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var enablingObjectiveList = await _enablingObjectiveService.GetEnablingObjectivesAsync();

            foreach (var enablingObjective in enablingObjectiveList)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = enablingObjective?.FullNumber + " - " + enablingObjective?.Description;
                filterOption.Value = enablingObjective.Id.ToString();
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                   new ReportFilterOptionParent()
                   {
                       Name = "Category",
                       Values = new List<string>()
                       {
                           enablingObjective?.EnablingObjective_Category?.Number +" - "+ enablingObjective?.EnablingObjective_Category?.Title
                       },
                       IsCascade=true
                   },
                   new ReportFilterOptionParent()
                   {
                       Name = "Sub-Category",
                       Values = new List<string>()
                       {
                          enablingObjective?.EnablingObjective_Category?.Number +"." + enablingObjective?.EnablingObjective_SubCategory?.Number +" - " +enablingObjective?.EnablingObjective_SubCategory?.Title
                       },
                        IsCascade=true
                   },
                   new ReportFilterOptionParent()
                   {
                       Name = "Topic",
                       Values = new List<string>()
                       {
                          enablingObjective.EnablingObjective_Topic != null ? enablingObjective?.EnablingObjective_Category?.Number +"." + enablingObjective?.EnablingObjective_SubCategory?.Number +"." + enablingObjective?.EnablingObjective_Topic?.Number +" - " +enablingObjective?.EnablingObjective_Topic?.Title : ""
                       },
                   },
                    new ReportFilterOptionParent()
                   {
                       Name = "Status",
                       Values= new List<string>() { enablingObjective.Active?"Active":"Inactive"},
                       IsTableVisible = false
                   },
                    new ReportFilterOptionParent()
                    {
                        Name = "Include Meta Enabling Objectives only",
                        Values = new List<string>(){ enablingObjective.isMetaEO ? "true":"false"},
                        IsTableVisible=false,
                        ControlType = "Checkbox"
                    },
                     new ReportFilterOptionParent()
                    {
                        Name = "Include Skill Qualifications Only",
                        Values = new List<string>(){ enablingObjective.IsSkillQualification ? "true":"false"},
                        IsTableVisible=false,
                        ControlType = "Checkbox"
                    }
                };
                filterOption.FilterTableColumns = new List<ReportFilterTableColumns>()
                {
                   new ReportFilterTableColumns()
                   {
                       Name = "Type",
                       Values = new List<string>(){enablingObjective.isMetaEO ? "MetaEO" : enablingObjective.IsSkillQualification ? "SQ" : "EO" },
                   },
                   new ReportFilterTableColumns()
                   {
                       Name = "Status",
                       Values = new List<string>(){enablingObjective.Active?"Active":"Inactive"},
                   }
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }
        public async Task<List<ReportFilterOption>> getEnablingObjectiveCategoriesAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var enablingObjectiveCategoryList = await _enablingObjective_CategoryService.AllAsync();

            foreach (var enablingObjective_Category in enablingObjectiveCategoryList)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = enablingObjective_Category?.Number + " - " + enablingObjective_Category?.Title;
                filterOption.Value = enablingObjective_Category.Id.ToString();
                filterOption.Active = enablingObjective_Category.Active;
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                    new ReportFilterOptionParent()
                     {
                           Name="Active Status",
                           Values= new List<string>() { enablingObjective_Category.Active? "Active":"Inactive"}
                     }
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }
        public async Task<List<ReportFilterOption>> GetSkillQualificationsForSelectionAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var enablingObjectiveList = await _enablingObjectiveService.GetSQEnablingObjectivesAsync();

            foreach (var enablingObjective in enablingObjectiveList)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = enablingObjective?.FullNumber + " - " + enablingObjective?.Description;
                filterOption.Value = enablingObjective.Id.ToString();
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                   new ReportFilterOptionParent()
                   {
                       Name = "Category",
                       Values = new List<string>()
                       {
                           enablingObjective?.EnablingObjective_Topic?.EnablingObjectives_SubCategory?.EnablingObjectives_Category?.Number +" - "+ enablingObjective?.EnablingObjective_Topic?.EnablingObjectives_SubCategory?.EnablingObjectives_Category?.Title
                       },
                       IsCascade=true
                   },
                   new ReportFilterOptionParent()
                   {
                       Name = "Sub-Category",
                       Values = new List<string>()
                       {
                          enablingObjective?.EnablingObjective_Topic?.EnablingObjectives_SubCategory?.EnablingObjectives_Category?.Number +"." + enablingObjective?.EnablingObjective_Topic?.EnablingObjectives_SubCategory?.Number +" - " +enablingObjective?.EnablingObjective_Topic?.EnablingObjectives_SubCategory?.Title
                       },
                        IsCascade=true
                   },
                   new ReportFilterOptionParent()
                   {
                       Name = "Topic",
                       Values = new List<string>()
                       {
                          enablingObjective?.EnablingObjective_Topic?.EnablingObjectives_SubCategory?.EnablingObjectives_Category?.Number +"." + enablingObjective?.EnablingObjective_Topic?.EnablingObjectives_SubCategory?.Number +"." + enablingObjective?.EnablingObjective_Topic?.Number +" - " +enablingObjective?.EnablingObjective_Topic?.Title
                       }
                   },
                    new ReportFilterOptionParent()
                   {
                       Name = "Status",
                       Values= new List<string>() { enablingObjective.Active?"Active":"Inactive"}
                   }
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getIssuingAuthorityAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var proceduresIssuingAuthorityList = await _procedure_IssuingAuthorityService.GetAllProceduresByIssuingAuthorityAsync();

            foreach (var issuingAuthority in proceduresIssuingAuthorityList)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = issuingAuthority.Title;
                filterOption.Value = issuingAuthority.Id.ToString();
                filterOption.Active = issuingAuthority.Active;
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                 new ReportFilterOptionParent()
                  {
                        Name="Active Status",
                        Values= new List<string>() { issuingAuthority.Active?"Active":"Inactive"}
                  }
                    };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getEmployeeWithNERCCertificationOptionAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var employeeList = await _employeeServices.GetEmployeesWithPersonAsync();
            var employeeCertifications = await _employeeCertificationService.GetActiveEmployeeNERCCertificationsAsync();
            foreach (var employee in employeeList)
            {
                var employeeCert = employeeCertifications.Where(x => x.EmployeeId == employee.Id).FirstOrDefault();
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = $"{employee.Person.FirstName} {employee.Person.LastName}" + (employeeCert != null ? (" - " + employeeCert.CertificationNumber) : "");
                filterOption.Value = employee.Id.ToString();
                filterOptions.Add(filterOption);
            }
            return filterOptions;
        }

        public List<ReportFilterOption> getOnlyActiveInactiveStatusAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            string[] options = new string[] { "Active", "Inactive" };
            for (int i = 0; i < options.Count(); i++)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = options[i];
                filterOption.Value = options[i];
                filterOption.IsDefaultOrder = true;
                filterOptions.Add(filterOption);
            }

            return filterOptions;
        }

        public async Task<List<ReportFilterOption>> getSafetyHazardsCategoriesAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var safetyHazardCategoryList = await _saftyHazard_CategoryService.AllAsync();

            foreach (var sH_Category in safetyHazardCategoryList)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = sH_Category?.Number + " - " + sH_Category?.Title;
                filterOption.Value = sH_Category.Id.ToString();
                filterOption.Active = sH_Category.Active;
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
        {
            new ReportFilterOptionParent()
             {
                   Name="Active Status",
                   Values= new List<string>() { sH_Category.Active? "Active":"Inactive"}
             }
        };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }
        public async Task<List<ReportFilterOption>> getFilteredCertificatesAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var certificates = await _certificationService.GetCertificationListAsync();

            foreach (var cert in certificates)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = cert.Name;
                filterOption.Value = cert.Id.ToString();
                filterOption.Active = cert.Active;

                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                    new ReportFilterOptionParent()
                     {
                       Name = "Issuing Authority",
                       Values =new List<string>()
                           {
                               cert.CertifyingBody.Name
                           },
                       IsTableVisible=false
                     },
                    new ReportFilterOptionParent()
                    {
                       Name = "Status",
                       Values =new List<string>(){cert.Active ? "Active" : "InActive"},
                       IsTableVisible=false,
                    }
                };
                filterOption.FilterTableColumns = new List<ReportFilterTableColumns>()
                {
                   new ReportFilterTableColumns()
                   {
                       Name = "Certificate Acronym",
                       Values = new List<string>(){ cert.CertAcronym },
                   },
                    new ReportFilterTableColumns()
                   {
                       Name = "Status",
                       Values =new List<string>(){cert.Active ? "Active" : "InActive"},
                   }
                };

                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getAllClassesWithIlaStatusProviderLocationAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var ClassScheduleList = await _classScheduleService.GetAllAsync();
            var orderedClasses = ClassScheduleList.OrderByDescending(cs => cs?.ILA?.Provider?.IsPriority).ThenBy(name => name?.ILA?.Provider?.Name);
            foreach (var classSchedule in orderedClasses)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = classSchedule?.ILA?.Name + " - " + classSchedule?.StartDateTime;
                filterOption.Value = classSchedule?.Id.ToString();
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                   new ReportFilterOptionParent()
                   {
                       Name = "Provider",
                       Values = new List<string>()
                       {
                           classSchedule?.ILA?.Provider?.Name
                       },
                       IsCascade=true
                   },
                   new ReportFilterOptionParent()
                    {
                       Name = "ILA Status",
                       Values =new List<string>(){classSchedule.ILA.Active ? "Active" : "Inactive"},
                       IsCascade=true,
                    },
                   new ReportFilterOptionParent()
                   {
                       Name = "ILA",
                       Values = new List<string>()
                       {
                           classSchedule?.ILA?.Name
                       },
                       IsCascade = true
                   },
                    new ReportFilterOptionParent()
                   {
                       Name = "Location",
                       Values = new List<string>()
                       {
                           classSchedule?.Location?.LocName
                       }
                   },
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getTestTypeOptionAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var testTypes = await _testTypeService.AllAsync();
            foreach (var item in testTypes.Take(3))
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = item.Description;
                filterOption.Value = item.Id.ToString();
                filterOptions.Add(filterOption);
            }
            return filterOptions;
        }

        public async Task<List<ReportFilterOption>> getTestStatusOptionAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var testStatus = await _testStatusService.AllAsync();
            foreach (var item in testStatus.Take(3))
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = item.Description;
                filterOption.Value = item.Id.ToString();
                filterOptions.Add(filterOption);
            }
            return filterOptions;
        }
        public async Task<List<ReportFilterOption>> getMetaILAAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var metaILAs = await _metaILAService.GetMetaILAsAsync();

            foreach (var metaILA in metaILAs)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = metaILA.Name;
                filterOption.Value = metaILA.Id.ToString();
                filterOption.Active = metaILA.Active;

                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                    new ReportFilterOptionParent()
                    {
                       Name = "Status",
                       Values =new List<string>(){ metaILA.Active ? "Active" : "InActive"},
                       IsTableVisible = false
                    }
                };
                filterOption.FilterTableColumns = new List<ReportFilterTableColumns>()
                {
                   new ReportFilterTableColumns()
                   {
                       Name = "No of Linked ILAs",
                       Values =  new List<string>(){metaILAs.SelectMany(metaILA => metaILA.Meta_ILAMembers_Links).Count(link => link.MetaILAID == metaILA.Id).ToString()},
                   },
                    new ReportFilterTableColumns()
                   {
                       Name = "Status",
                       Values =new List<string>(){ metaILA.Active ? "Active" : "InActive"},
                   }
                };

                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }
        public List<ReportFilterOption> getObjectivesLinkedToILAAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            string[] options = new string[] { "Meta Tasks", "Tasks", "Meta Enabling Objectives", "Enabling Objectives", "Skill Qualifications", "Custom Objectives" };
            for (int i = 0; i < options.Count(); i++)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = options[i];
                filterOption.Value = (i + 1).ToString();
                filterOptions.Add(filterOption);
            }

            return filterOptions;
        }

        public async Task<List<ReportFilterOption>> getsafetyhazardAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var safetyHazardList = await _saftyHazardService.GetForSafetyHazardsAsync();

            foreach (var saftyList in safetyHazardList.OrderBy(r => r.Number))
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = saftyList.Number + " - " + saftyList.Title;
                filterOption.Value = saftyList.Id.ToString();
                filterOption.Active = saftyList.Active;
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                   new ReportFilterOptionParent()
                   {
                       Name = "safety Hazard Category",
                       Values = new List<string>()
                       {
                           saftyList.SaftyHazard_Category.Title
                       }
                   },

                    new ReportFilterOptionParent()
                     {
                           Name="Active Status",
                           Values= new List<string>() { saftyList.Active?"Active":"Inactive"}
                     }
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public List<ReportFilterOption> getEnablingObjectivesTypesAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            string[] options = new string[] { "Enabling Objectives", "Meta EOs", "Skill Qualifications" };
            for (int i = 0; i < options.Count(); i++)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = options[i];
                filterOption.Value = (i + 1).ToString();
                filterOptions.Add(filterOption);
            }

            return filterOptions;
        }

        public async Task<List<ReportFilterOption>> getILAAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var coursesList = await _iLAService.GetILAWithProvidersAsync();
            var orderedCourses = coursesList
                .OrderByDescending(course => course.Provider.IsPriority)
                .ThenBy(course => course.Provider.Name)
                .ThenBy(course => course.Number);

            foreach (var course in orderedCourses)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = course.Number + " - " + course.Name;
                filterOption.Value = course.Id.ToString();
                filterOption.Active = course.Active;
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                   new ReportFilterOptionParent()
                   {
                       Name = "Provider",
                        Values =new List<string>()
                           {
                               course.Provider.Name
                           },
                    },
                   new ReportFilterOptionParent()
                   {
                       Name="Active Status",
                      Values= new List<string>() {course.Active?"Active":"Inactive" }
                   }
                };

                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getInitialTrainingProgramsAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var trainingPrograms = await _trainingProgramService.GetInitialTrainingProgramsAsync();
            foreach (var trainingProgram in trainingPrograms)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = trainingProgram.Version;
                filterOption.Value = trainingProgram.Id.ToString();
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                   new ReportFilterOptionParent()
                   {
                       Name = "Position",
                       Values = new List<string>()
                       {
                           trainingProgram?.Position?.PositionTitle,
                       },
                       IsCascade =true
                   },
                    new ReportFilterOptionParent()
                   {
                       Name = "Training Program Type",
                       Values = new List<string>()
                       {
                           trainingProgram?.TrainingProgramType?.TrainingProgramTypeTitle,
                       },
                       IsCascade =true
                   },
                   new ReportFilterOptionParent()
                    {
                        Name = "Status",
                        Values= new List<string>() { trainingProgram.Active ? "Active":"Inactive"}

                    },
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getRRTTypeAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            string[] options = new string[] { "All", "Active RRT", "Inactive RRT" };
            for (int i = 0; i < options.Count(); i++)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = options[i];
                filterOption.Value = options[i];
                filterOptions.Add(filterOption);
            }
            return filterOptions;
        }

        public async Task<List<ReportFilterOption>> getTrainingIssueAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var trainingIssues = await _trainingIssueService.GetAllTrainingIssueAsync();
            foreach (var trainingIssue in trainingIssues)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = trainingIssue.IssueTitle;
                filterOption.Value = trainingIssue.Id.ToString();

                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                   new ReportFilterOptionParent
                   {
                       Name = "Driver",
                       Values = new List<string>
                       {
                           $"{trainingIssue?.DriverType?.Type}{(string.IsNullOrWhiteSpace(trainingIssue?.DriverType?.Type) || string.IsNullOrWhiteSpace(trainingIssue?.DriverSubType?.SubType) ? "" : " - ")}{trainingIssue?.DriverSubType?.SubType}"
                       }.Where(v => !string.IsNullOrWhiteSpace(v)).ToList(),
                       IsTableVisible = false
                   },
                    new ReportFilterOptionParent()
                   {
                       Name = "Data Element",
                       Values = new List<string>(){ trainingIssue?.DataElement?.DataElementDisplay }.Where(v => !string.IsNullOrWhiteSpace(v)).ToList(),
                       IsTableVisible=false
                   },
                     new ReportFilterOptionParent()
                     {
                         Name = "Issue Status",
                         Values= new List<string>() { trainingIssue?.Status?.Status },
                         IsTableVisible=false
                    },
                   new ReportFilterOptionParent()
                    {
                        Name = "Active/Inactive Status",
                        Values= new List<string>() { trainingIssue.Active ? "Active":"Inactive"},
                        IsTableVisible=false
                    },
                };
                filterOption.FilterTableColumns = new List<ReportFilterTableColumns>()
                {
                   new ReportFilterTableColumns()
                   {
                       Name = "Issue ID",
                       Values =  new List<string>(){trainingIssue?.IssueID},
                   },
                   new ReportFilterTableColumns()
                   {
                       Name = "Driver",
                       Values = new List<string>(){$"{trainingIssue?.DriverType?.Type}{(trainingIssue?.DriverType?.Type != null && trainingIssue?.DriverSubType?.SubType != null ? " - " : "")}{trainingIssue?.DriverSubType?.SubType}"},
                   },
                   new ReportFilterTableColumns()
                   {
                       Name = "Data Element",
                       Values = new List<string>(){trainingIssue?.DataElement?.DataElementDisplay},
                   },
                   new ReportFilterTableColumns()
                   {
                       Name = "Issue Status",
                       Values= new List<string>() { trainingIssue?.Status?.Status },
                   }
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getTrainingIssueActionItemStatusAsync()
        {
            var filterOptions = new List<ReportFilterOption>
            {
                new ReportFilterOption { Name = "All", Value = "All" }
            };
            var trainingIssueActionItemStatuses = await _trainingIssue_ActionItemStatusService.AllAsync();
            foreach (var trainingIssueActionItem in trainingIssueActionItemStatuses)
            {
                var filterOption = new ReportFilterOption
                {
                    Name = trainingIssueActionItem.Status,
                    Value = trainingIssueActionItem.Id.ToString()
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public async Task<List<ReportFilterOption>> getCourseCompletionTypeAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            string[] options = new string[] { "Completed & Not Completed within Date Range", "Completed within Date Range", "Not Completed within Date Range" };
            for (int i = 0; i < options.Count(); i++)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = options[i];
                filterOption.Value = options[i];
                filterOptions.Add(filterOption);
            }
            return filterOptions;
        }

        public async Task<List<ReportFilterOption>> getTrainingIssueComponentAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var trainingIssueCategories = ExtensionMethods.GetAllDataElementsWithCategories();

            int index = 1;
            foreach (var category in trainingIssueCategories)
            {
                foreach (var dataElement in category.DataElementVMs)
                {
                    ReportFilterOption filterOption = new ReportFilterOption
                    {
                        Name = dataElement.DataElementDisplay,
                        Value = index.ToString(),
                        FilterOptionParents = new List<ReportFilterOptionParent>
                        {
                            new ReportFilterOptionParent
                            {
                                Name = "Driver Type",
                                Values = new List<string> { dataElement.DataElementCategory },
                            }
                        }
                    };
                    filterOptions.Add(filterOption);
                    index++;
                }
            }

            return filterOptions;
        }



        public async Task<List<ReportFilterOption>> getTrainingIssueSeveritiesAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var trainingIssueSeverities = await _trainingIssue_SeverityService.AllAsync();
            foreach (var severity in trainingIssueSeverities)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = severity.Severity;
                filterOption.Value = severity.Id.ToString();
                filterOptions.Add(filterOption);
            }
            return filterOptions;
        }

        public List<ReportFilterOption> getTrainingIssueStatus()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            string[] options = new string[] { "All", "Open", "Closed" };
            for (int i = 0; i < options.Count(); i++)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = options[i];
                filterOption.Value = options[i];
                filterOptions.Add(filterOption);
            }
            return filterOptions;
        }

        public async Task<List<ReportFilterOption>> getEmployeeNameAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var employeeList = await _employeeServices.GetEmployeesWithPersonAsync();
            foreach (var employee in employeeList)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = employee.Person.FirstName + " " + employee.Person.LastName;
                filterOption.Value = employee.Id.ToString();
                filterOptions.Add(filterOption);
            }
            return filterOptions;
        }

        public async Task<List<ReportFilterOption>> getTrainingTaskGroupAsync()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            var trainingTaskGroups = await _trainingGroupService.GetAllTrainingTaskGroupWithCategories();
            foreach(var tg in trainingTaskGroups)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = tg.GroupName;
                filterOption.Value = tg.Id.ToString();
                filterOption.FilterOptionParents = new List<ReportFilterOptionParent>()
                {
                    new ReportFilterOptionParent()
                    {
                        Name = "Position",
                        Values = tg?.Task_TrainingGroups.SelectMany(t=>t.Task.Position_Tasks).Select(pt=>pt.Position.PositionTitle).Distinct().ToList(),
                    }
                };
                filterOptions.Add(filterOption);
            }
            return filterOptions.ToList();
        }

        public List<ReportFilterOption> getTaskTypes()
        {
            List<ReportFilterOption> filterOptions = new List<ReportFilterOption>();
            string[] options = new string[] { "Tasks (Regular only)", "Tasks (Regular and Meta Only)", "Meta Tasks Only", "R5 Impact Tasks Only", "R6 Reliability Impact Tasks Only" };
            for (int i = 0; i < options.Count(); i++)
            {
                ReportFilterOption filterOption = new ReportFilterOption();
                filterOption.Name = options[i];
                filterOption.Value = options[i];
                filterOptions.Add(filterOption);
            }
            return filterOptions;
        }
    }
}
