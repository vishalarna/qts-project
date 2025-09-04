using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace QTD2.API.QTD.Controllers.EMP
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class EmployeeController : EMPController
    {
        private readonly IEmployeeTaskService _employeeTaskService;
        private readonly IStringLocalizer<EmployeesController> _localizer;
        private readonly IEmployeeCertificationHistoryService _empCertification_historyService;
        private readonly IEmployeeHistoryService _employeeHistoryService;
        //private readonly IEmpTestService _employeeTestService;
        private readonly IClassScheduleService _classScheduleService;
        private readonly IClientSettingsService _clientSettingsService;
        private readonly ITaskRequalificationService _taskRequalService;
        private readonly ITaskReQualificationEmp_SignOffService _empSignOffService;
        private readonly IProcedureReviewEmpService _procedureReviewEmpService;
        private readonly IDashboardService _dashboardService;
        private readonly IOnlineCoursesService _onlineCourseService;
        private readonly IDIFSurveyService _dIFSurveyService;

        public EmployeeController(
            UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IEmployeeService employeeService,
            IEmployeeTaskService employeeTaskService,
            IStringLocalizer<EmployeesController> localizer,
            IEmployeeCertificationHistoryService empCertification_historyService, 
            IEmployeeHistoryService employeeHistoryService,
            //IEmpTestService employeeTestService,
            IClassScheduleService classScheduleService,
            ITaskRequalificationService taskRequalService,
            IPersonService personService,
            IClientSettingsService clientSettingsService,
            ITaskReQualificationEmp_SignOffService empSignOffService,
            IProcedureReviewEmpService procedureReviewEmpService,
            IDashboardService dashboardService, 
            IOnlineCoursesService onlineCourseService
            , IDIFSurveyService dIFSurveyService) 
            : base(userManager, employeeService, httpContextAccessor)
        {
            _employeeTaskService = employeeTaskService;
            _localizer = localizer;
            _empCertification_historyService = empCertification_historyService;
            _employeeHistoryService = employeeHistoryService;
            //_employeeTestService = employeeTestService;
            _classScheduleService = classScheduleService;
            _clientSettingsService = clientSettingsService;
            _empSignOffService = empSignOffService;
            _taskRequalService = taskRequalService;
            _procedureReviewEmpService = procedureReviewEmpService;
            _dashboardService = dashboardService;
            _onlineCourseService = onlineCourseService;
            _dIFSurveyService = dIFSurveyService;
        }
    }
}
