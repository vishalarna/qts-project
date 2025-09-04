using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ILA_Position_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        /// <summary>
        /// Links the position with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpPost]
        [Route("/ila/{id}/position")]
        public async Task<IActionResult> LinkPositionAsync(int id, ILA_Position_LinkOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            if(options.PositionIds.Length > 0)
            {
                await _versioningService.VersionILAAsync(ila, options.PositionIds.Length.ToString() + " Postion(s) Linked", DateTime.Now, 1);
            }
            var result = await _ilaService.LinkPositionAsync(id, options);

            return Ok( new { result });
        }

        /// <summary>
        /// Unlinks the position with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpDelete]
        [Route("/ila/{id}/position")]
        public async Task<IActionResult> UnlinkPositionAsync(int id, ILA_Position_LinkOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            if(options.PositionIds.Length > 0)
            {
                await _versioningService.VersionILAAsync(ila, options.PositionIds.Length.ToString() + " Postion(s) Unlinked", DateTime.Now, 0);
            }
            await _ilaService.UnlinkPositionAsync(id, options);
            return Ok( new { message = _localizer["PositionsUnlinkedFromILA"].Value });
        }

        /// <summary>
        /// Get the positions linked with specific ILA
        /// </summary>
        /// <returns>Http Response Code with Linked positions</returns>
        [HttpGet]
        [Route("/ila/{id}/position")]
        public async Task<IActionResult> GetLinkedPositionAsync(int id)
        {
            var result = await _ilaService.GetLinkedPositionAsync(id);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/ila/{id}/position")]
        public async Task<IActionResult> UpdateLinkedPositionsAsync(int id, ILA_Position_LinkOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            var result = await _ilaService.UpdateLinkedPositionsAsync(id,options);
            if (result.PositionsAdded > 0)
            {
                await _versioningService.VersionILAAsync(ila, result.PositionsAdded.ToString() + " Postion(s) Linked", DateTime.Now, 1);
            }
            if (result.PositionsRemoved > 0)
            {
                await _versioningService.VersionILAAsync(ila, result.PositionsRemoved.ToString() + " Postion(s) Unlinked", DateTime.Now, 0);
            }
            var data = _ilaService.MapILADetailsVMByILA(result.ILA);
            return Ok( new { result= data });
        }
    }
}
