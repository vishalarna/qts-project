using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.TrainingProgram;
using QTD2.Infrastructure.Model.TrainingProgram_History;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class TrainingProgramsController : ControllerBase
    {
        private readonly ITrainingProgramService _trainingProgramService;
        private readonly ITrainingProgramTypeService _trainingProgramTypeService;
        private readonly ITrainingProgram_HistoryService _trainingProgramHistoryService;
        private readonly IStringLocalizer<TrainingProgramsController> _localizer;

        public TrainingProgramsController(
            ITrainingProgramService trainingProgramService,
            ITrainingProgramTypeService trainingProgramTypeService,
            ITrainingProgram_HistoryService trainingProgramHistoryService,
            IStringLocalizer<TrainingProgramsController> localizer)
        {
            _trainingProgramService = trainingProgramService;
            _trainingProgramTypeService = trainingProgramTypeService;
            _trainingProgramHistoryService = trainingProgramHistoryService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of Training Programs
        /// </summary>
        /// <returns>Http response code with training Programs</returns>
        [HttpGet]
        [Route("/trainingPrograms")]
        public async Task<IActionResult> GetAsync()
        {
            var trainingPrograms = await _trainingProgramService.GetAsync();
            return Ok( new { trainingPrograms });
        }

        [HttpGet]
        [Route("/trainingPrograms/position/{posId}")]
        public async Task<IActionResult> GetTPByPositionId(int posId)
        {
            var result = await _trainingProgramService.GetTPByPositionIdAsync(posId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/trainingPrograms/position/{posId}/{trainingProgramType}")]
        public async Task<IActionResult> GetTPByPositionId(int posId, string trainingProgramType)
        {
            var result = await _trainingProgramService.GetTPByPositionIdAsync(posId, trainingProgramType);
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new Training Program for a position
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/trainingPrograms")]
        public async Task<IActionResult> CreateTrainingProgramAsync(TrainingProgramCreateOptions options)
        {
            var trainingProgram = await _trainingProgramService.CreateAsync(options);
            return Ok( new { trainingProgram });
        }

        /// <summary>
        /// Gets a specific Training Program
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with certifying body</returns>
        [HttpGet]
        [Route("/trainingPrograms/{id}")]
        public async Task<IActionResult> GetTrainingProgramAsync(int id)
        {
            var trainingProgram = await _trainingProgramService.GetAsync(id);
            return Ok( new { trainingProgram });
        }

        /// <summary>
        /// Updates  a specific Training Program
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/trainingPrograms/{id}")]
        public async Task<IActionResult> UpdateTrainingProgramAsync(int id, TrainingProgramUpdateOptions options)
        {
            await _trainingProgramService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["trainingProgramUpdated"].Value });
        }

        /// <summary>
        /// Delets  a specific Training Program
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpDelete]
        //[Route("/trainingPrograms/{id}")]
        //public async Task<IActionResult> DeleteTrainingProgramAsync(int id)
        //{
        //    await _trainingProgramService.DeleteAsync(id);
        //    return Ok( new { message = _localizer["trainingProgramDeleted"].Value });
        //}

        [HttpGet]
        [Route("/trainingPrograms/stats")]
        public async Task<IActionResult> GetStatsAsync()
        {
            var stats = await _trainingProgramService.GetStatsCount();
            return Ok( new { stats });
        }

        [HttpPost]
        [Route("/trainingPrograms/{filter}")]
        public async Task<IActionResult> GetTrainingProgamsByFilter(string filter, TrainingProgramFilterOptions options)
        {
            var result = await _trainingProgramService.GetTrainingProgamsByFilter(filter, options);
            return Ok( new { result });
        }
        [HttpDelete]
        [Route("/trainingPrograms/{id}")]
        public async Task<IActionResult> DeleteAsync(int id, TrainingProgram_HistoryCreateOption options)
        {
            //Instructor ins = null;
            switch (options.ActionType)
            {
                case "inactive":
                default:
                    await _trainingProgramService.DeactivateAsync(id);
                    break;
                case "Active":
                    
                    await _trainingProgramService.ActivateAsync(id);
                    break;
                case "delete":
                    await _trainingProgramService.DeleteAsync(id);
                    break;
            }
            //options.InstructorId = id;
            //await _instructorHistoryService.CreateAsync(options);
            return Ok( new { message = _localizer["InstructorDeleted"] });
        }

        [HttpPost]
        [Route("/trainingPrograms/{id}/publish")]
        public async Task<IActionResult> PublishTrainingProgramAsync(int id, TrainingProgram_HistoryCreateOption options)
        {
            await _trainingProgramService.PublishTrainingProgramAsync(id, options);
            //var trainingProgHistory = new TrainingProgram_HistoryCreateOptions();
            //trainingProgHistory.ChangeEffectiveDate = options.EffectiveDate;
            //trainingProgHistory.ChangeNotes = options.ChangeNotes;
            //trainingProgHistory.TrainingProgramId = id;
            //trainingProgHistory.TrainingProgramId = 1;
            //await _trainingProgramHistoryService.CreateAsync(trainingProgHistory);
            return Ok( new { message = _localizer["trainingProgramUpdated"].Value });
        }


        // get list for initail, continuing and cycle training program active and inactive



        /// <summary>
        /// Creates a new Training Program for a position
        /// </summary>
        /// <param name="typeName"></param>
        /// /// <param name="status"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpGet]
        [Route("/trainingPrograms/notlinked/{typeName}/{status}")]
        public async Task<IActionResult> GetNotLinkedProcedure(string typeName,bool status)
        {
            var result = await _trainingProgramService.GetActiveInactiveList(typeName, status);
            return Ok( new { result });
        }

        /// <summary>
        /// Get Version and ProgramTitle of active TrainingPrograms with no associated TrainingProgramReview
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/trainingPrograms/active/versionAndTitle/noReview")]
        public async Task<IActionResult> GetActiveVersionTitleWithNoReview()
        {
            var result = await _trainingProgramService.GetActiveVersionTitleWithNoReviewAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Get Version and ProgramTitle of active TrainingPrograms for a Position and of a TrainingProgramType
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/trainingPrograms/active/versionAndTitle/position/{positionId}/trainingProgramType/{trainingProgramTypeId}")]
        public async Task<IActionResult> GetActiveVersionTitleByPositionAndTrainingProgramTypeAsync(int positionId, int trainingProgramTypeId)
        {
            var result = await _trainingProgramService.GetActiveVersionTitleByPositionAndTrainingProgramTypeAsync(positionId, trainingProgramTypeId);
            return Ok( new { result });
        }

        /// <summary>
        /// Get GetTrainingProgramLinks and TrainingProgramReview
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/trainingPrograms/links")]
        public async Task<IActionResult> GetTrainingProgramLinksAndTrainingProgramReviewAsync()
        {
            var result = await _trainingProgramService.GetTrainingProgramLinksAndTrainingProgramReviewAsync();
            return Ok(new { result });
        }
    }
}
