using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ILA_Topic_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        [HttpPut]
        [Route("/ila/{id}/ilatopic")]
        public async Task<IActionResult> UpdateLinkedILATopicsAsync(int id, ILA_Topic_LinkOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            var result = await _ilaService.UpdateLinkedILATopicsAsync(id, options);
            if (result.TopicsAdded > 0)
            {
                await _versioningService.VersionILAAsync(ila, result.TopicsAdded.ToString() + " Topic(s) Linked", DateTime.Now, 1);
            }
            if (result.TopicsRemoved > 0)
            {
                await _versioningService.VersionILAAsync(ila, result.TopicsRemoved.ToString() + " Topic(s) Unlinked", DateTime.Now, 0);
            }
            var data = _ilaService.MapILADetailsVMByILA(result.ILA);
            return Ok( new { result = data });
        }
        [HttpGet]
        [Route("/ila/{id}/ilatopic")]
        public async Task<IActionResult> GetLinkedILATopicsAsync(int id)
        {
            var result = await _ilaService.GetLinkedILATopicsAsync(id);
            return Ok( new { result });
        }
    }
}
