using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ILA_TrainingTopic_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        /// <summary>
        /// Links the TrainingTopic with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpPost]
        [Route("/ila/{id}/trTopic")]
        public async Task<IActionResult> LinkTrainingTopicAsync(int id, ILA_TrainingTopic_LinkOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, "Training Topic Link Created", DateTime.Now, 1);
            var result = await _ilaService.LinkTrainingTopicAsync(id, options);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlinks the TrainingTopic with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpDelete]
        [Route("/ila/{id}/trTopic/{linkId}")]
        public async Task<IActionResult> UnlinkTrainingTopicAsync(int id, int linkId)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, "Training Topic Link Removed", DateTime.Now, 0);
            await _ilaService.UnlinkTrainingTopicAsync(id, linkId);
            return Ok( new { message = _localizer["TrainingTopicsUnlinkedFromILA"].Value });
        }

        /// <summary>
        /// Get the TrainingTopics linked with specific ILA
        /// </summary>
        /// <returns>Http Response Code with Linked TrainingTopics</returns>
        [HttpGet]
        [Route("/ila/{id}/trTopic")]
        public async Task<IActionResult> GetLinkedTrainingTopicAsync(int id)
        {
            var result = await _ilaService.GetLinkedTrainingTopicsAsync(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/ila/{id}/trainingTopic")]
        public async Task<IActionResult> GetILALinkedTrainingTopicNamesAsync(int id)
        {
            var result = await _ilaService.GetILALinkedTrainingTopicsNamesAsync(id);
            return Ok(new { result });
        }
    }
}
