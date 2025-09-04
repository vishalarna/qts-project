using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Instructor;
using QTD2.Infrastructure.Model.Instructor_History;


namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class InstructorsController : ControllerBase
    {
        private readonly IInstructorService _instructorService;
        private readonly IInstructorHistoryService _instructorHistoryService;
        private readonly IInstructor_CategoryService _instructor_CategoryService;
        private readonly IInstructor_CategoryHistoryService _insCatHistoryService;
        private readonly IStringLocalizer<InstructorsController> _localizer;

        public InstructorsController(
            IInstructorService instructorService,
            IInstructorHistoryService instructorHistoryService,
            IInstructor_CategoryService instructor_CategoryService,
            IInstructor_CategoryHistoryService insCatHistoryService,
            IStringLocalizer<InstructorsController> localizer)
        {
            _instructorService = instructorService;
            _instructorHistoryService = instructorHistoryService;
            _instructor_CategoryService = instructor_CategoryService;
            _insCatHistoryService = insCatHistoryService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of instructors
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/instructors")]
        public async Task<IActionResult> GetAsync()
        {
            var insList = await _instructorService.GetAsync();
            return Ok( new { insList });
        }

        /// <summary>
        /// Gets instructor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/instructors/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var ins = await _instructorService.GetAsync(id);
            return Ok( new { ins });
        }

        /// <summary>
        /// Creates a instructors
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/instructors")]
        public async Task<IActionResult> CreateAsync(Instructor_CreateOptions options)
        {
            var ins = await _instructorService.CreateAsync(options);
            var histOptions = new Instructor_HistoryCreateOptions();
            histOptions.EffectiveDate = (DateTime)options.EffectiveDate;
            histOptions.InstructorNotes = options.InstructorNotes;
            histOptions.InstructorId = ins.Id;
            await _instructorHistoryService.CreateAsync(histOptions);
            return Ok( new { ins, message = _localizer["InsCreated"] });
        }

        /// <summary>
        /// Updates instructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPut]
        [Route("/instructors/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Instructor_CreateOptions options)
        {
            var ins = await _instructorService.UpdateAsync(id, options, false);
            var histOptions = new Instructor_HistoryCreateOptions();
            histOptions.EffectiveDate = (DateTime)options.EffectiveDate;
            histOptions.InstructorNotes = options.InstructorNotes;
            histOptions.InstructorId = ins.Id;
            await _instructorHistoryService.CreateAsync(histOptions);
            return Ok( new { ins, message = _localizer["InsUpdated"] });
        }

        /// <summary>
        /// Deletes an instructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/instructors")]
        public async Task<IActionResult> DeleteAsync(Instructor_HistoryCreateOptions options)
             {
            Instructor ins = null;
            switch (options.ActionType)
            {
                case "inactive":
                default:
                    await _instructorService.InActiveAsync(options);
                    break;
                case "active":
                    await _instructorService.ActiveAsync(options);
                    break;
                case "delete":
                    await _instructorService.DeleteAsync(options);
                    break;
            }
            foreach(var instId in options.instructorIds)
            {
                options.InstructorId = instId;
                await _instructorHistoryService.CreateAsync(options);
            }
            return Ok( new { message = _localizer["InstructorDeleted"] });
        }

        [HttpGet]
        [Route("/instructors/count")]
        public async Task<IActionResult> GetInstructorCountAsync()
        {
            var result = await _instructorService.getCount();
            return Ok( new { result });
        }

        /// <summary>
        /// Get the count of procedures and issuing authorities and linkages
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/instructors/stats")]
        public async Task<IActionResult> GetStatsAsync()
        {
            var stats = await _instructorService.GetStatsCount();
            return Ok( new { stats });
        }

        //active inactive cat and ins along with workbook admins
        [HttpGet]
        [Route("/instructors/{option}/catlist")]
        public async Task<IActionResult> GetCatActiveInactiveList(string option)
        {
            var result = await _instructorService.GetCatActiveInactive(option);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/instructors/{option}/inslist")]
        public async Task<IActionResult> GetInsActiveInactiveList(string option)
        {
            var result = await _instructorService.GetInsActiveInactive(option);
            return Ok( new { result });
        }
    }
}
