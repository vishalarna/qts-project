using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ILA_Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        [HttpPost]
        [Route("/ila/{ilaId}/resource")]
        public async Task<IActionResult> CreateAsync(int ilaId, ILAResourceCreateOptions option)
        {
            var result = await _iLAResourceService.CreateAsync(ilaId, option);
            return Ok( new { result });
        }
        [HttpPost]
        [Route("/ila/{ilaId}/{editILAResourceId}/resource/update")]
        public async Task<IActionResult> UpdateAsync(int ilaId, int editILAResourceId, ILAResourceCreateOptions option)
        {
            var result = await _iLAResourceService.UpdateAsync(ilaId, editILAResourceId, option);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/ila/{ilaId}/resource")]
        public async Task<IActionResult> GetAsync(int ilaId)
        {
            var result = await _iLAResourceService.GetAsync(ilaId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/ila/{ilaResourceId}/removeResourceIla")]
        public async Task<IActionResult> RemoveResourceILA(int ilaResourceId)
        {
            var result = await _iLAResourceService.RemoveResourceILA(ilaResourceId);
            return Ok( new { result });
        }
    }
}
