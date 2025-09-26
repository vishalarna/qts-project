using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Employee_Task;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.EnablingObjectiveHistory;
using QTD2.Infrastructure.Model.Position;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class EnablingObjectivesController : ControllerBase
    {
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly IVersioningService _versioningService;
        private readonly IEmployeeTaskService _employeeTaskService;
        private readonly IStringLocalizer<EnablingObjectivesController> _localizer;
        private readonly IEnablingObjectiveHistoryService _enablingObjectiveHistoryService;
        private readonly IVersion_EnablingObjectiveService _versionEnablingObjectiveService;
        private readonly IVersion_TaskService _ver_taskService;
        private readonly ITask_HistoryService _task_histService;
        private readonly ITaskService _taskService;

        public EnablingObjectivesController(IEnablingObjectiveService enablingObjectiveService, IVersioningService versioningService, IEmployeeTaskService employeeTaskService, IStringLocalizer<EnablingObjectivesController> localizer, IEnablingObjectiveHistoryService enablingObjectiveHistoryService, IVersion_EnablingObjectiveService versionEnablingObjectiveService, IVersion_TaskService ver_taskService, ITask_HistoryService task_histService, ITaskService taskService)
        {
            _enablingObjectiveService = enablingObjectiveService;
            _versioningService = versioningService;
            _employeeTaskService = employeeTaskService;
            _localizer = localizer;
            _enablingObjectiveHistoryService = enablingObjectiveHistoryService;
            _versionEnablingObjectiveService = versionEnablingObjectiveService;
            _ver_taskService = ver_taskService;
            _task_histService = task_histService;
            _taskService = taskService;
        }

        /// <summary>
        /// Gets a list of enabling objectives
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/enablingObjectives")]
        public async Task<IActionResult> GetAsync()
        {
            var listEO = await _enablingObjectiveService.GetAsync();
            return Ok( new { listEO });
        }
        
        /// <summary>
        /// Gets a list of enabling objectives
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/enablingObjectives/tree")]
        public async Task<IActionResult> GetMinimalDataForTree()
        {
            var result = await _enablingObjectiveService.GetMinimalDataForTreeAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Gets a list of enabling objectives
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/enablingObjectives/sq")]
        public async Task<IActionResult> GetSQAsync()
        {
            var listEO = await _enablingObjectiveService.GetSQAsync();
            return Ok( new { listEO });
        }

        [HttpGet]
        [Route("/enablingObjectives/sq/{order}")]
        public async Task<IActionResult> GetAllSQsOrderBy(string order)
        {
            var result = await _enablingObjectiveService.GetSimplifiedCategories(order);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/eos")]
        public async Task<IActionResult> GetEOsAsync()
        {
            var listEO = await _enablingObjectiveService.GetEOAsync();
            return Ok( new { listEO });
        }

        [HttpGet]
        [Route("/eos/number/{selectedCatId}/{selectedSubCatId}/{selectedTopicId}")]
        public async Task<IActionResult> GetEONumberAsync(int selectedCatId, int selectedSubCatId, int selectedTopicId)
        {
            var listEO = await _enablingObjectiveService.GetEONumberWithTopicAsync(selectedCatId, selectedSubCatId, selectedTopicId);
            return Ok( new { listEO });
        }

        [HttpGet]
        [Route("/enablingObjectives/{id}/isMeta")]
        public async Task<IActionResult> CheckIsMeta(int id)
        {
            var result = await _enablingObjectiveService.CheckIsMetaAsync(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/eos/number/{selectedCatId}/{selectedSubCatId}")]
        public async Task<IActionResult> GetEONumberAsync(int selectedCatId, int selectedSubCatId)
        {
            var listEO = await _enablingObjectiveService.GetEONumberWithoutTopicAsync(selectedCatId, selectedSubCatId);
            return Ok( new { listEO });
        }

        [HttpGet]
        [Route("/enablingObjectives/sqs")]
        public async Task<IActionResult> GetAllSqs()
        {
            var result = await _enablingObjectiveService.GetAllSqs();
            return Ok( new { result });
        }

        /// <summary>
        /// Gets an enabling objective
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/enablingObjectives/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var eo = await _enablingObjectiveService.GetAsync(id);
            return Ok( new { eo });
        }

        /// <summary>
        /// Creates an enabling objective
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/enablingObjectives")]
        public async Task<IActionResult> CreateAsync(EnablingObjectiveCreateOptions options)
        {
            var eo = await _enablingObjectiveService.CreateAsync(options);
            //var histOptions = new EnablingObjectiveHistoryCreateOptions();
            //histOptions.EnablingObjectiveId = eo.Id;
            //histOptions.NewStatus = true;
            //histOptions.OldStatus = false;
            //histOptions.ChangeEffectiveDate = options
            //await _versionEnablingObjectiveService.CreateAsync(eo, 2);

            // await _versioningService.VersionEnablingObjectiveAsync(eo, options.IsSignificant);
            // if (options.IsSignificant)
            // {
            //    var versionedTasks = await _versioningService.GetVersionedTasksAsync();
            //    foreach (var vTask in versionedTasks)
            //    {
            //        await _employeeTaskService.ArchiveEmployeeTaskAsync(vTask.TaskId, null, null);
            //        await _employeeTaskService.CreateAsync(new EmployeeTaskCreateOptions { TaskId = vTask.TaskId });
            //    }
            // }
            return Ok( new { eo, message = _localizer["EOCreated"] });
        }

        [HttpPost]
        [Route("/enablingObjectives/ila")]
        public async Task<IActionResult> CreateFromILA(EnablingObjectiveCreateOptions options)
        {
            var result = await _enablingObjectiveService.CreateFromILAAsync(options);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates an enabling objective
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPut]
        [Route("/enablingObjectives/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, EnablingObjectiveCreateOptions options)
        {
            //var enablingObjective = new EnablingObjective(options.CategoryId, options.SubCategoryId, options.TopicId, options.Number, options.Statement, options.isMetaEO, options.IsSkill, options.References, options.Criteria, options.Conditions);

            //await _versionEnablingObjectiveService.CreateAsync(enablingObjective, 2);
            var eo = await _enablingObjectiveService.UpdateAsync(id, options);
            //await _versioningService.VersionEnablingObjectiveAsync(eo);
            //if (options.isSignificant)
            //{
            //    var versionedTasks = await _versioningService.GetVersionedTasksAsync();
            //    foreach (var vTask in versionedTasks)
            //    {
            //        await _employeeTaskService.ArchiveEmployeeTaskAsync(vTask.TaskId, null, null);
            //        await _employeeTaskService.CreateAsync(new EmployeeTaskCreateOptions { TaskId = vTask.TaskId });
            //    }
            //}

            return Ok( new { eo, message = _localizer["EOUpdated"] });
        }

        /// <summary>
        /// Make copy of EO along with linkages
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/enablingObjectives/{id}/copy")]
        public async Task<IActionResult> CopyEOAsync(int id,EnablingObjectiveCreateOptions options)
        {
            var result = await _enablingObjectiveService.CopyEOWithLinkages(id, options);

            return Ok( new { result , message = _localizer["EOCopyCreated"] });
        }

        /// <summary>
        /// Get Linked Stats for Enabling Objective
        /// </summary>
        /// <param name="eoId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/enablingObjectives/{eoId}/linked")]
        public async Task<IActionResult> GetEOLinkedStatsAsync(int eoId)
        {
            var result = await _enablingObjectiveService.GetEOLinkedStats(eoId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/enablingObjectives/{metaId}/meta/linked")]
        public async Task<IActionResult> GetMetaEOLinkedStatsAsync(int metaId)
        {
            var result = await _enablingObjectiveService.GetMetaEOLinkedStatsAsync(metaId);
            return Ok( new { result });
        }

        /// <summary>
        /// Get Not linked Stats for Enabling Objectives
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/enablingObjectives/notlinked")]
        public async Task<IActionResult> GetNotLinkedStatsAsync()
        {
            var result = await _enablingObjectiveService.GetEONotLinkedStats();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/enablingObjectives/{id}/linked/{type}")]
        public async Task<IActionResult> GetLinkedEOsAsync(int id,string type)
        {
            var result = await _enablingObjectiveService.GetLinkedEOsAsync(id,type);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/enablingObjectives/linkedIds/{name}")]
        public async Task<IActionResult> GetLinkedIds(string name)
        {
            var result = await _enablingObjectiveService.GetLinkedIds(name);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/enablingObjectives/{eoId}/all")]
        public async Task<IActionResult> GetEOWithAllData(int eoId)
        {
            var result = await _enablingObjectiveService.GetAllEODataAsync(eoId);
            return Ok( new { result });
        }

        /// <summary>
        /// Get EO along with category,subcategory and topic.
        /// </summary>
        /// <param name="eoId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/enablingObjectives/{eoId}/others")]
        public async Task<IActionResult> GetEOWithCatSubCatAndTopic(int eoId)
        {
            var result = await _enablingObjectiveService.GetEOWithCatSubCatAndTopicAsync(eoId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/enablingObjectives/{eoId}/version")]
        public async Task<IActionResult> GetEOVersionsAsync(int eoId)
        {
            var result = await _versionEnablingObjectiveService.GetAllVersionsForEOAsync(eoId);
            return Ok( new { result });
        }

        /// <summary>
        /// Deletes an enabling objective
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/enablingObjectives/{id}")]
        public async Task<IActionResult> DeleteAsync(int id, EnablingObjectiveOptions options)
        {
            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            //var vEO = _enablingObjectiveService.GetAsync(id).Result;
            //await _versioningService.VersionEnablingObjectiveAsync(vEO);
            string message; // using this message to show translated message in angular
            switch (options.ActionType.Trim().ToLower())
            {
                case "inactive":
                default:
                    foreach(var eoId in options.EOIDs)
                    {
                        await _enablingObjectiveService.DeactivateAsync(eoId);
                    }
                    histOptions.ChangeEffectiveDate = options.EffectiveDate;
                    histOptions.ChangeNotes = options.ChangeNotes;
                    histOptions.EnablingObjectiveId = id;
                    histOptions.OldStatus = true;
                    histOptions.NewStatus = false;


                    var eo = await _enablingObjectiveService.GetAsync(id);
                    var version =await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 0);
                    histOptions.Version_EnablingObjectiveId = version.Id;
                    await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);
                    message = "recinactive";
                    break;
                case "active":
                    foreach (var eoId in options.EOIDs)
                    {
                        await _enablingObjectiveService.ActivateAsync(eoId);
                    }
                    histOptions.ChangeEffectiveDate = options.EffectiveDate;
                    histOptions.ChangeNotes = options.ChangeNotes;
                    histOptions.EnablingObjectiveId = id;
                    histOptions.OldStatus = true;
                    histOptions.NewStatus = false;

                    var eo1 = await _enablingObjectiveService.GetAsync(id);
                    var version1 = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo1, 2);
                    histOptions.Version_EnablingObjectiveId = version1.Id;
                    await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);
                    message = "recactive";
                    break;
                case "delete":
                    foreach(var eoId in options.EOIDs)
                    {
                        await _enablingObjectiveService.DeleteAsync(eoId);
                    }
                    message = "recdelete";
                    break;
            }
            //if (options.isSignificant)
            //{
            //    var versionedTasks = await _versioningService.GetVersionedTasksAsync();
            //    foreach (var vTask in versionedTasks)
            //    {
            //        await _employeeTaskService.ArchiveEmployeeTaskAsync(vTask.TaskId, null, null);
            //        await _employeeTaskService.CreateAsync(new EmployeeTaskCreateOptions { TaskId = vTask.TaskId });
            //    }
            //}

            return Ok( new { message });
        }

        [HttpPut]
        [Route("/enablingObjectives/{id}/specific")]
        public async Task<IActionResult> EditSpecificField(int id, EOSpecificUpdateOptions option)
        {
            await _enablingObjectiveService.EditSpecificField(id, option);
            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.ChangeNotes = "EO " + option.Field + " Updated";
            histOptions.ChangeEffectiveDate = DateTime.Now;
            histOptions.OldStatus = false;
            histOptions.NewStatus = true;
            var eo = await _enablingObjectiveService.GetAsync(id);
            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 2);
            histOptions.Version_EnablingObjectiveId = version.Id;
            histOptions.EnablingObjectiveId = id ;
            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);

            return Ok( new { message = _localizer["taskFieldUpdated"] }); ;
        }

        [HttpPost]
        [Route("/enablingObjectives/subCat/topic/number")]
        public async Task<IActionResult> GetSQ(SQForTQVM options)
        {
            var result = await _enablingObjectiveService.GetSQForSubCatAndTopicAsync(options);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/enablingObjectives/{id}/canDeactivate")]
        public async Task<IActionResult> CheckCanEOBeDeactivated(int id)
        {
            var result = await _enablingObjectiveService.CheckCanEOBeDeactivatedAsync(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/enablingObjectives/list/{option}")]
        public async Task<IActionResult> GetEOActiveInactive(string option)
        {
            var result = await _enablingObjectiveService.GetEOActiveInactive(option);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/enablingObjectives/sqByPositions")]
        public async Task<IActionResult> GetTaskTreeDataByPositionAsync(PositionIdsModel options)
        {
            var listEO = await _enablingObjectiveService.GetSQsByPositionIdsAsync(options.PositionIds);
            return Ok(new { listEO });
        }
    }
}
