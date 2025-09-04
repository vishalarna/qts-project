using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.EnablingObjectiveHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class EnablingObjectivesController : ControllerBase
    {
        [HttpGet]
        [Route("/enablingObjectives/{eoId}/emp")]
        public async Task<IActionResult> GetLinkedEmployees(int eoId)
        {
            var result = await _enablingObjectiveService.GetLinkedEmployees(eoId);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/enablingObjectives/{eoId}/emp")]
        public async Task<IActionResult> LinkEmployeeAsync(EO_LinkOptions options)
        {
            await _enablingObjectiveService.LinkEmployee(options);
            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.NewStatus = true;
            histOptions.OldStatus = false;
            histOptions.ChangeEffectiveDate = DateTime.Now;
            histOptions.ChangeNotes = options.EmployeeIds.Length + " Employees Linked To SQ";
            histOptions.EnablingObjectiveId = options.EOId;
            var eo = await _enablingObjectiveService.GetAsync(options.EOId);

            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 1);
            histOptions.Version_EnablingObjectiveId = version.Id;
            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);
            //await _versionEnablingObjectiveService.CreateLinkVersioning(eo, 1);
            return Ok( new { message = _localizer["EmployeesLinkedToSQ"] });
        }

        [HttpDelete]
        [Route("/enablingObjectives/{eoId}/emp")]
        public async Task<IActionResult> UnlinkEmployeeAsync(EO_LinkOptions options)
        {
            await _enablingObjectiveService.UnlinkEmployee(options);
            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.NewStatus = true;
            histOptions.OldStatus = false;
            histOptions.ChangeEffectiveDate = options.EffectiveDate;
            histOptions.ChangeNotes = options.ChangeNotes;
            histOptions.EnablingObjectiveId = options.EOId;

            var eo = await _enablingObjectiveService.GetAsync(options.EOId);
            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 0);
            histOptions.Version_EnablingObjectiveId = version.Id;
            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);
            //var eo = _enablingObjectiveService.GetAsync(options.EOId).Result;
            //await _versionEnablingObjectiveService.CreateLinkVersioning(eo, 0);
            return Ok( new { message = _localizer["EmployeesUnlinkedFromSQ"] });
        }
    }
}
