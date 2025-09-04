using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.DIFSurvey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class DIFSurveyController : ControllerBase
    {
        private readonly IDIFSurveyService _dIFSurveyService;
        private readonly IStringLocalizer<DIFSurveyController> _localizer;
        private readonly IDIFSurveyTaskTrainingFrequencyService _dIFSurveyTaskTrainingFrequency;
        private readonly IDIFSurveyTaskStatusService _dIFSurveyTaskStatusService;

        public DIFSurveyController(IDIFSurveyService dIFSurveyService,
            IStringLocalizer<DIFSurveyController> localizer, IDIFSurveyTaskTrainingFrequencyService dIFSurveyTaskTrainingFrequency, IDIFSurveyTaskStatusService dIFSurveyTaskStatusService) {
            _dIFSurveyService = dIFSurveyService;
            _localizer = localizer;
            _dIFSurveyTaskTrainingFrequency = dIFSurveyTaskTrainingFrequency;
            _dIFSurveyTaskStatusService = dIFSurveyTaskStatusService;
        }


        [HttpPost]
        [Route("/difsurvey/create")]
        public async Task<IActionResult> CreateAsync(DIFSurvey_CreateOptions options)
        {
            var result = await _dIFSurveyService.CreateAsync(options);
            return Ok(new { result });
        }

        [HttpPut]
        [Route("/difsurvey/{id}")]
        public async Task<IActionResult> Updatesync(int id, DIFSurvey_UpdateOptions options)
        {
            var result = await _dIFSurveyService.UpdateAsync(id, options);
            return Ok(new { result, message = _localizer["ILAUpdated"].Value });
        }
         
        [HttpPut]
        [Route("/difsurvey/{id}/edit/{editType}")]
        public async Task<IActionResult> EditDIFSurveyAsync(int id, string editType)
        {
            var result = await _dIFSurveyService.EditDIFSurveyAsync(id, editType);
            return Ok( new { result, message = _localizer["DIFUpdated"].Value });
        }

        [HttpGet]
        [Route("/difSurvey/overview")]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _dIFSurveyService.GetAllAsync();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/difSurvey/results/{id}")]
        public async Task<IActionResult> GetDifResultbyIdAsync(int id)
        {
            var result = await _dIFSurveyService.GetTaskRatingAsync(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/difSurvey/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _dIFSurveyService.GetAsync(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/difSurvey/{id}/enrollments")]
        public async Task<IActionResult> GetEnrollmentsBySurveyIdAsync(int id)
        {
            var result = await _dIFSurveyService.GetEnrollmentsBySurveyIdAsync(id);
            return Ok( new { result });
        }
    }
}
