using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.DIFSurvey;
using QTD2.Infrastructure.Model.Task_Review;
using QTD2.Infrastructure.Model.TaskListReview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QTD2.Infrastructure.Model.Reports;
using System.IO;
using QTD2.Infrastructure.Hashing.Interfaces;
using QTD2.Domain.Exceptions;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskListReviewController : ControllerBase
    {
        private readonly ITaskListReviewService _taskListReviewService;
        private readonly IStringLocalizer<TaskListReviewController> _localizer;
        private readonly IReportsService _reportsService;
        private readonly IReportGeneratorService _reportGeneratorService;
        private readonly IHasher _hasher;


        public TaskListReviewController(ITaskListReviewService taskListReviewService,
            IStringLocalizer<TaskListReviewController> localizer,
            IReportsService reportsService,
            IReportGeneratorService reportGeneratorService,
             IHasher hasher)
        {
            _taskListReviewService = taskListReviewService;
            _localizer = localizer;
            _reportsService = reportsService;
            _reportGeneratorService = reportGeneratorService;
            _hasher = hasher;
        }

        [HttpGet]
        [Route("/taskListReviews/overview")]
        public async Task<IActionResult> GetOverviewAsync()
        {
            var result = await _taskListReviewService.GetOverviewAsync();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/taskListReviews/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _taskListReviewService.GetAsync(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/taskListReviews")]
        public async Task<IActionResult> CreateAsync(TaskListReview_VM options)
        {
            var result = await _taskListReviewService.CreateAsync(options);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/taskListReviews/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, TaskListReview_VM options)
        {
            var result = await _taskListReviewService.UpdateAsync(id, options);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/taskListReviews/{id}")]
        public async Task<IActionResult> CopyAsync(int id)
        {
            var result = await _taskListReviewService.CopyAsync(id);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/taskListReviews/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _taskListReviewService.DeleteAsync(id);
            return StatusCode(StatusCodes.Status200OK);
        }
        
        [HttpPut]
        [Route("/taskListReviews/{id}/activate")]
        public async Task<IActionResult> ActivateAsync(int id)
        {
            await _taskListReviewService.ActivateInactivateAsync(id,"activate");
            return StatusCode(StatusCodes.Status200OK);
        }
        
        [HttpPut]
        [Route("/taskListReviews/{id}/inactivate")]
        public async Task<IActionResult> InactivateAsync(int id)
        {
            await _taskListReviewService.ActivateInactivateAsync(id,"inactivate");
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost]
        [Route("/taskListReviews/{id}/taskReviews")]
        public async Task<IActionResult> CreateTaskReviewsAsync(int id,TaskReviewCreateOption option)
        {
            var result = await _taskListReviewService.CreateTaskReviewsAsync(id, option);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/taskListReviews/generateReport")]
        public async Task<IActionResult> GenerateReportAsync(ExportReportModel model)
        {
            var taskListReviewFilter = model.Options.Filters.FirstOrDefault(x => x.Name.ToUpper() == "TASK LIST REVIEWS");
            if(taskListReviewFilter != null && !string.IsNullOrEmpty(taskListReviewFilter.Value))
            {
                var encodedIds = taskListReviewFilter.Value.Split(",").ToList();
                taskListReviewFilter.Value = String.Join(",", encodedIds.Select(x => _hasher.Decode(x)));
            }
            else
            {
                throw new QTDServerException("TaskListReview Id is missing in the given input");
            }
            var report = await _reportsService.CreateReportAsync(model.Options, false);
            var file = await _reportGeneratorService.ExportReportAsync(model.ExportType, report);
            var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.None, 4096, FileOptions.DeleteOnClose);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            Response.Headers.Add("Content-Disposition", new System.Net.Mime.ContentDisposition("attachment") { FileName = Path.GetFileName(file) }.ToString());
            return File(
                fileStream: fs,
                contentType: System.Net.Mime.MediaTypeNames.Application.Octet,
                fileDownloadName: Path.GetFileName(file));
        }
    }
}
