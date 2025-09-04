using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.CSE_ILACertLink_PartialCredit;
using QTD2.Infrastructure.Model.Employee;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class CSE_ILACertLink_PartialCreditController : ControllerBase
    {
        private readonly ICSE_ILACertLink_PartialCreditService _cSE_ILACertLink_PartialCreditService;
        public CSE_ILACertLink_PartialCreditController(ICSE_ILACertLink_PartialCreditService cSE_ILACertLink_PartialCreditService)
        {
            _cSE_ILACertLink_PartialCreditService = cSE_ILACertLink_PartialCreditService;
        }

        [HttpPost]
        [Route("/ilacertpartialcredit/cse")]
        public async Task<IActionResult> GetCSE_ILACertLink_PartialCreditByClassEmpIdAsync(EmployeeIdsModel options)
        {
            var result = await _cSE_ILACertLink_PartialCreditService.GetClassScheduleEmployee_ILACertificationLink_PartialCreditByClassEmpIdsAsync(options.EmployeeIds);
            return Ok(new { result });
        }

        [HttpPost]
        [Route("/cseilacertpartialcredit/ila/{id}")]
        public async Task<IActionResult> AddOrUpdateCSE_ILACertLink_PartialCreditHoursAsync(int id,CSE_ILACertPartialCreditCreateUpdateOption option)
        {
            await _cSE_ILACertLink_PartialCreditService.AddOrUpdateCSE_ILACertLink_PartialCreditHoursAsync(id,option);
            return Ok();
        }
    }
}
