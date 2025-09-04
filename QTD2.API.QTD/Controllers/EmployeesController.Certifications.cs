using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.EmployeeCertification;
using QTD2.Infrastructure.Model.EmployeeCertificationHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class EmployeesController : ControllerBase
    {
        /// <summary>
        /// Gets a list of certifications for a specific employee
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns>Http response code with employeeCertification</returns>
        [HttpGet]
        [Route("/employees/{id}/certifications")]
        public async Task<IActionResult> GetCertificationsAsync(int id)
        {
            var employeeCertification = await _employeeService.GetCertificationsAsync(id);
            return Ok( new { employeeCertification });
        }

        /// <summary>
        /// Gets a list of certifications linked for a specific employee
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns>Http response code with employeeCertification</returns>
        [HttpGet]
        [Route("/employees/linkedCertifications/{id}/{filter}")]
        public async Task<IActionResult> GetCertificationEmployeeIsLinkedTo(int id, string filter)
        {
            var result = await _employeeService.GetCertificationsLinkedToEmployee(id,filter);
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new Employee Certification
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with employeeCertification</returns>
        [HttpPost]
        [Route("/employees/{id}/certifications")]
        public async Task<IActionResult> CreateCertificationsAsync(int id, EmployeeCertificateCreateOptions options)
        {
            var employeeCertification = await _employeeService.AddCertificationAsync(id, options);
            return Ok( new { employeeCertification, message = _localizer["empCertCreated"] });
        }

        /// <summary>
        /// Updates specific certification for a specific employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="certificationId"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with employeeCertification</returns>
        [HttpPut]
        [Route("/employees/empcertifications/{id}")]
        public async Task<IActionResult> EditCertificationsAsync(int id, EmployeeCertificateUpdateOptions options)
        {
            var employeeCertification = await _employeeService.EditCertificationAsync(id, options);
            return Ok( new { employeeCertification, message = _localizer["empCertUpdated"] });
        }

        /// <summary>
        /// Removes specific certification for a specific employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="certificationId"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/employees/certifications/{id}")]
        public async Task<IActionResult> DeleteCertificationsAsync(int id)
        {
            await _employeeService.DeleteCertificationAsync(id);
            return Ok( new { message = _localizer["empCertificationDeleted"] });
        }

        [HttpGet]
        [Route("/employees/certifications/{id}/history")]
        public async Task<IActionResult> GetEmployeeCertificationFromHistory(int id)
        {
            var result = await _employeeService.GetEmployeeCertificationFromHistory(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/employees/renew/empcertifications/{id}")]
        public async Task<IActionResult> RenewCertificatesAsync(int id, EmployeeCertificateUpdateOptions options)
        {
            var oldCert = await _employeeService.GetCertificationsAsync(id);
            if(oldCert != null)
            {
                var histoptions = new EmployeeCertificationHistoryCreateOptions();
                histoptions.ChangeEffectiveDate = options.EffectedDate;
                histoptions.ChangeNotes = options.Reason;
                histoptions.EmployeeCertificationId = oldCert.Id;
                histoptions.ExpirationDate = oldCert.ExpirationDate;
                histoptions.IssueDate = oldCert.IssueDate;
                histoptions.RenewalDate = oldCert.RenewalDate;
                histoptions.CertificationNumber = oldCert.CertificationNumber;
                await _empCertification_historyService.CreateAsync(histoptions);
            }
            
            var employeeCertification = await _employeeService.RenewCertificationAsync(id, options);
            return Ok( new { employeeCertification, message = _localizer["empCertUpdated"] });
        }
        //[HttpPut]
        //[Route("/employees/{employeeId}/certifications")]
        //public async Task<IActionResult> CertificationStatusAsync(int employeeId, EmployeeCertificateUpdateOptions options)
        //{
        //    await _employeeService.CertificationRequired(employeeId, options.CertRequired);
        //    return Ok( new { message = _localizer["empCertificationStatusChange"] });
        //}

        [HttpGet]
        [Route("/employees/{empId}/allempcertifications")]
        public async Task<IActionResult> GetEmployeeCertificationsByEmpIdAsync(int empId)
        {
            var employeeCertifications = await _employeeService.GetCertificationsByEmpIdAsync(empId);
            return Ok(new { employeeCertifications });
        }
    }
}
