using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.TrainingProgram;

namespace QTD2.API.QTD.Controllers
{
    public partial class TrainingProgramsController
    {
        /// <summary>
        /// Gets a list of Training Programs
        /// </summary>
        /// <returns>Http response code with training Programs</returns>
        [HttpGet]
        [Route("/trainingPrograms/trainingProgramTypes")]
        public async Task<IActionResult> GetProgramTypesAsync()
        {
            var trainingProgramsTypes = await _trainingProgramTypeService.GetAsync();
            return Ok(new { trainingProgramsTypes });
        }
    }
}
