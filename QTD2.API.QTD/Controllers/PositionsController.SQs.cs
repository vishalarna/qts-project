using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Position_SQ_Link;
using QTD2.Infrastructure.Model.PositionHistory;
using System;
using System.Threading.Tasks;


namespace QTD2.API.QTD.Controllers
{
   
    public partial class PositionsController : ControllerBase
    {
        [HttpPost]
        [Route("/positions/{id}/enablingObjectives")]
        public async Task<IActionResult> LinkSQ(int id, Position_SQ_LinkCreateOptions options)
        {
            var result = await _positionService.LinkEO(id, options);

            //foreach (var item in options.EOIds)
            //{
                await _positionhistoryService.CreateAsync(new Position_HistoryCreateOptions()
                {
                    ChangeNotes = options.EOIds.Length + " SQ Linked to Position",
                    EffectiveDate = DateTime.Now,
                    PositionId = id
                });
            //}

            return Ok( new { result });
        }

        /// <summary>
        /// Get eos linked to position
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/positions/{id}/enablingObjectives")]
        public async Task<IActionResult> GetLinkedSQs(int id)
        {
            var result = await _positionService.GetLinkedEos(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get all positions that the eo is linked to
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/positions/enablingObjectives/{id}")]
        public async Task<IActionResult> GetPosEoIsLinkedTo(int id)
        {
            var result = await _positionService.GetPositionsEoIsLinkedTo(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlink specific Eos linked to position provided by positionId
        /// </summary>
        /// <param name="posId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/positions/{posId}/enablingObjectives/")]
        public async Task<IActionResult> UnlinkEO(int posId, Position_SQ_LinkCreateOptions options)
        {
            await _positionService.UnlinkEO(posId, options.EOIds);
            //foreach (var item in options.EOIds)
            //{
                await _positionhistoryService.CreateAsync(new Position_HistoryCreateOptions()
                {
                    ChangeNotes = options.EOIds.Length + " SQ UnLinked from Position",
                    EffectiveDate = DateTime.Now,
                    PositionId = posId
                }) ;
            //}
            return Ok( new { message = _localier["EOUnlinked"].Value });
        }
    }
}

