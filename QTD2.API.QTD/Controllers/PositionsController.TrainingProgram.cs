using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QTD2.API.QTD.Controllers
{
    public partial class PositionsController : ControllerBase
    {
        /// <summary>
        /// Gets a list of Training Program of a specific Position
        /// </summary>
        /// <returns>Http response code with Training Programs</returns>
        [HttpGet]
        [Route("/positions/trainingprograms/{id}")]
        public async Task<IActionResult> GetTrainingProgramsAsync(int id)
        {
            var trainingPrograms = await _trainingProgramService.GetByPositionIdAsync(id);
            return Ok(new { trainingPrograms });
        }
    }
}
