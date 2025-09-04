using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ILA;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        [HttpGet]
        [Route("/ila/{id}/perform")]
        public async Task<IActionResult> GetPerformEval(int id)
        {
            var result = await _ilaService.GetPerformEvalAsync(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/ila/{id}/perform")]
        public async Task<IActionResult> CreateOrUpdatePerform(int id,ILA_PerformTraineeEvalCreateOptions options)
        {
            var data = await _ilaService.CreateOrUpdatePerformAsync(id, options);
            var result = data.ILA_PerformTraineeEval;
            string cd;
            if(data.State == 1)
            {
                cd = "Perform Trainee Evaluation Created";
            }
            else
            {
                cd = "Perform Trainee Evaluation Updated";
            }
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, cd, DateTime.Now, data.State);
            return Ok( new { result });
        }
    }
}
