using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Application.Settings;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Employee;
using QTD2.Infrastructure.Model.EmployeeHistory;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeTaskService _employeeTaskService;
        private readonly IStringLocalizer<EmployeesController> _localizer;
        private readonly IEmployeeCertificationHistoryService _empCertification_historyService;
        private readonly IEmployeeHistoryService _employeeHistoryService;
        //private readonly IEmpTestService _employeeTestService;
        private readonly IClassScheduleService _classScheduleService;
        private readonly IClientSettingsService _clientSettingsService;
        private readonly DomainSettings _domainSettings;
        private readonly IClassRosterService _classScheduleRosterService;


        public EmployeesController(
                IOptions<DomainSettings> domainSettingOptions, 
                IEmployeeService employeeService, 
                IEmployeeTaskService employeeTaskService, 
                IStringLocalizer<EmployeesController> localizer, 
                IEmployeeCertificationHistoryService empCertification_historyService, 
                IEmployeeHistoryService employeeHistoryService, 
                //IEmpTestService employeeTestService, 
                IClassScheduleService classScheduleService, 
                IClientSettingsService clientSettingsService,
                IClassRosterService classScheduleRosterService)
        {
            _employeeService = employeeService;
            _employeeTaskService = employeeTaskService;
            _localizer = localizer;
            _empCertification_historyService = empCertification_historyService;
            _employeeHistoryService = employeeHistoryService;
            //_employeeTestService = employeeTestService;
            _classScheduleService = classScheduleService;
            _clientSettingsService = clientSettingsService;
            _domainSettings = domainSettingOptions.Value;
            _classScheduleRosterService = classScheduleRosterService;
        }

        /// <summary>
        /// Gets a list of Employees
        /// </summary>
        /// <returns>Http response code with employees</returns>
        [HttpGet]
        [Route("/employees")]
        public async Task<IActionResult> GetAsync()
        {
            var employees = await _employeeService.GetEmployeeListAsync();
            return Ok( new { employees });
        }

        [HttpGet]
        [Route("/employees/active/forEnroll")]
        public async Task<IActionResult> GetAllActiveEmployeesAsync()
        {
            var result = await _employeeService.GetAllActiveEmployees();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new Employee
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http response code with employee</returns>
        [HttpPost]
        [Route("/employees")]
        public async Task<IActionResult> CreateAsync(EmployeeCreateOptions options)
        {
            //HttpClient client = new HttpClient();
            //string apiUrl = "https://localhost:44376/users";
            //HttpResponseMessage response = await client.GetAsync(apiUrl);
            //var licenses = _clientSettingsService.GetCurrentLicenseAsync();
            options.WebsiteUrl = _domainSettings.SPA;
            var employees = await _employeeService.CreateAsync(options);
            return Ok( new { employees, message = _localizer["employeeCreated"] });
        }

        /// <summary>
        /// Gets a specific employee by employee number
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with employee</returns>
        [HttpGet]
        [Route("/employees/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var employee = await _employeeService.GetAsync(id);
            return Ok( new { employee });
        }

        /// <summary>
        /// Get all Employees with Positions and Organizations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/employees/pos/org")]
        public async Task<IActionResult> GetAllEmpWithPosAndOrg()
        {
            var result = await _employeeService.GetAllEmpWithPosAndOrgAsync();
            return Ok( new { result });
        }


        [HttpGet]
        [Route("/employees/pos/org/ids")]
        public async Task<IActionResult> GetAllEmpWithPosAndOrgIdsOnlyAsync()
        {
            var result = await _employeeService.GetAllEmpWithPosAndOrgIdsOnlyAsync();
            return Ok( new { result });
        }


        /// <summary>
        /// Updates specific employee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with employee</returns>
        [HttpPut]
        [Route("/employees/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, EmployeeUpdateOptions options)
        {
            var employee = await _employeeService.UpdateAsync(id, options);
            return Ok( new { employee, message = _localizer["employeeUpdated"] });
        }

        /// <summary>
        /// Upload The given files data to the Employee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPut]
        [DisableRequestSizeLimit]
        [RequestFormLimits(MultipartBodyLengthLimit = long.MaxValue, ValueLengthLimit = int.MaxValue)]
        [Route("/employees/upload/{id}")]
        public async Task<IActionResult> UploadFile(int id, EmployeeDocumentOptions file)
        {
            await _employeeService.UploadEmployeeFileAsync(id, file);
            return Ok( new { message = _localizer["Files Uploaded"].Value });
        }

        /// <summary>
        /// Get All files upload for this Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/employees/getupload/{id}")]
        public async Task<IActionResult> getUploadedEmployeeFiles(int id)
        {
            var result = await _employeeService.getUploadedFiles(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Download the file.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/employees/{fileId}/download/{id}")]
        public async Task<IActionResult> downloadFile(int id, int fileId)
        {
            var result = await _employeeService.DownloadFile(id, fileId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/employees/{id}/withPos")]
        public async Task<IActionResult> GetEmployeeWithPosition(int id)
        {
            var result = await _employeeService.GetEmployeeWithPositionAsync(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/employees/evals")]
        public async Task<IActionResult> GetAllEvaluators()
        {
            var result = await _employeeService.GetAllEvaluatorsAsync();
            return Ok( new { result });
        }
        [HttpGet]
        [Route("/employees/evals/names")]
        public async Task<IActionResult> GetAllEvaluatorsNamesOnly()
        {
            var result = await _employeeService.GetAllEvaluatorsNamesAsync();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/employees/onlyemp")]
        public async Task<IActionResult> GetOnlyEmployees()
        {
            var result = await _employeeService.GetOnlyEmployeesAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Removes a specific employee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actionType"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/employees/{id}/{actionType}")]
        public async Task<IActionResult> DeleteAsync(int id, string actionType)
        {
            string message; // using this message to show translated message in angular
            switch (actionType)
            {
                case "delete":
                    await _employeeService.DeleteAsync(id);
                    message = "recdelete";
                    break;
                //case "inactive":
                //    await _employeeService.DeactivateAsync(id);
                //    message = "recinactive";
                //    break;
                //case "active":
                //    await _employeeService.ActivateAsync(id);
                //    message = "recactive";
                //    break;
                //default:
                //    await _employeeService.DeactivateAsync(id);
                //    message = "recinactive";
                //    break;
            }
            return Ok( new { message = _localizer["Employee Deleted"] });
        }


        /// <summary>
        /// Deletes a employee
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/employees/parent")]
        public async Task<IActionResult> DeleteAsync(EmployeeOptions options)
        {

            foreach (var tId in options.employeeIds)
            {
                var employeeActiveStatus = await _employeeService.GetEmployeeActiveStatusByEmpId(tId);
                var operationType = new EmployeeHistoryOperationType();
                switch (options.ActionType.ToLower())
                {
                    case "inactive":
                    default:
                        operationType = EmployeeHistoryOperationType.Deactivate;
                        await _employeeService.DeactivateAsync(tId, options);
                        employeeActiveStatus = false;
                        //await _employeeService.UpdateEmployeeInactiveDate(tId, options.ChangeEffectiveDate, options.ChangeNotes);
                        break;
                    case "active":
                        operationType = EmployeeHistoryOperationType.Activate;
                        await _employeeService.ActivateAsync(tId);
                        employeeActiveStatus = true;
                        break;
                    case "delete":
                        operationType = EmployeeHistoryOperationType.Delete;
                        await _employeeService.DeleteAsync(tId);
                        break;
                }
                var histOptions = new EmployeeHistoryCreateOptions(tId, options.ChangeNotes, options.ChangeEffectiveDate, employeeActiveStatus, operationType);
                await _employeeHistoryService.CreateEmployeeHistory(histOptions);
            }

            return Ok( new { message = _localizer["Employee" + " " + options.ActionType.ToLower()] });
        }

      
        [HttpGet]
        [Route("/employees/expiredCertifications")]
        public async Task<IActionResult> GetExpiredCertificates()
        {
            var result = await _employeeService.GetExpiredCertificates();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/employees/{username}/image")]
        public async Task<IActionResult> GetEmpImage(string username)
        {
            var result = await _employeeService.GetEmpImageAsync(username);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/employees/username")]
        public async Task<IActionResult> GetEmpUserName()
        {
            var result = await _employeeService.GetEmpUserName();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/employees/list")]
        public async Task<IActionResult> GetEmployeesList()
        {
            var result = await _employeeService.GetEmployeesList();
            return Ok( new { result });
        }


        [HttpGet]
        [Route("/employees/names/list")]
        public async Task<IActionResult> GetEmployeesListNamesOnly()
        {
            var result = await _employeeService.GetEmployeesListNamesOnly();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/employees/organization/position/list")]
        public async Task<IActionResult> GetEmployeesListWithOrgAndPos()
        {
            var result = await _employeeService.GetEmployeesListWithOrgAndPosAsync();
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/employees/{id}/idpreview")]
        public async Task<IActionResult> SaveIdpReviewAsync(int id, IDPReviewUpdateOptions options)
        {
            await _employeeService.SaveIdpReviewAsync(id, options);
            return Ok( new { message = _localizer["IdpReviewInfomationSaved"] });
        }

        [HttpGet]
        [Route("/employees/ClassScheduleEmployee/{cseId}")]
        public async Task<IActionResult> GetEmployeeByClassScheduleEmployee(int cseid)
        {
            var result = await _employeeService.GetEmployeeByClassScheduleEmployeeAsync(cseid);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/employees/GetByUsername/{userName}")]
        public async Task<IActionResult> GetEmployeeByUsernameAsync(string userName)
        {
            var result = await _employeeService.GetEmployeeByUsernameAsync(userName);
            return Ok(new { result });
        }
    }
}
