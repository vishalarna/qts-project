using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ILAHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        [HttpGet]
        [Route("/ila/history")]
        public async Task<IActionResult> GetAllILAHistories()
        {
            var result = await _iLAHistoryService.GetAllILAHistories();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/ila/history/{id}")]
        public async Task<IActionResult> GetILAHistory(int id)
        {
            var result = await _iLAHistoryService.GetILAHistory(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/ila/history")]
        public async Task<IActionResult> CreateILAHistory(ILAHistoryCreateOptions options)
        {
            var result = await _iLAHistoryService.CreateILAHistory(options);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/ila/history/{id}")]
        public async Task<IActionResult> UpdateILAHistory(int id, ILAHistoryCreateOptions options)
        {
            var result = await _iLAHistoryService.UpdateILAHistory(id, options);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/ila/history/{id}")]
        public async Task<IActionResult> DeleteILAHistory(int id, ILAHistoryOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "active":
                    await _iLAHistoryService.ActiveILAHistory(id);
                    break;
                case "inactive":
                    await _iLAHistoryService.InActiveILAHistory(id);
                    break;
                case "delete":
                    await _iLAHistoryService.DeleteILAHistory(id);
                    break;
            }

            return Ok( new { message = _localizer[$"SafetyHazard_History-{options.ActionType.ToLower()}"].Value });
        }
    }
}
