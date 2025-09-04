using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Position;
using QTD2.Infrastructure.Model.PositionHistory;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class PositionsController : ControllerBase
    {
        private readonly IPositionService _positionService;
        private readonly IPositionHistoryService _positionhistoryService;
        private readonly ITrainingProgramService _trainingProgramService;
        private readonly IStringLocalizer<PositionsController> _localier;
        private readonly IVersion_PositionService _ver_posService;
        private readonly IVersion_TaskService _ver_taskService;
        private readonly ITask_HistoryService _task_histService;
        private readonly ITaskService _taskService;

        public PositionsController(
            IPositionService positionService,
            IStringLocalizer<PositionsController> localier,
            ITrainingProgramService trainingProgramService,
            IPositionHistoryService positionhistoryService,
            IVersion_PositionService ver_posService,
            IVersion_TaskService ver_taskService,
            ITask_HistoryService task_histService,
            ITaskService taskService)
        {
            _positionService = positionService;
            _localier = localier;
            _trainingProgramService = trainingProgramService;
            _positionhistoryService = positionhistoryService;
            _ver_posService = ver_posService;
            _ver_taskService = ver_taskService;
            _task_histService = task_histService;
            _taskService = taskService;
        }

        /// <summary>
        /// Gets a list of Positions
        /// </summary>
        /// <returns>Http response code with positions</returns>
        [HttpGet]
        [Route("/positions")]
        public async Task<IActionResult> GetAsync()
        {
            var positions = await _positionService.GetAsync();
            return Ok( new { positions });
        }


        /// <summary>
        /// Gets a list of Positions
        /// </summary>
        /// <returns>Http response code with positions</returns>
        [HttpGet]
        [Route("/positions/active")]
        public async Task<IActionResult> GetActiveAsync()
        {
            var positions = await _positionService.GetAsync(true);
            return Ok( new { positions });
        }

        [HttpGet]
        [Route("/positions/withoutInclude")]
        public async Task<IActionResult> GetPositionsWithoutIncludes()
        {
            var result = await _positionService.GetWithoutIncludesAsync();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/positions/order/{orderBy}")]
        public async Task<IActionResult> GetAllOrderBy(string orderBy)
        {
            var result = await _positionService.GetAllOrderByAsync(orderBy);
            return Ok( new { result });
        }

        /// <summary>
        /// Gets a list of Positions
        /// </summary>
        /// <returns>Http response code with positions</returns>
        [HttpGet]
        [Route("/positions/number")]
        public async Task<IActionResult> GetPositionNumberAsync()
        {
            var positions = await _positionService.GetPositionNumberAsync();
            return Ok( new { positions });
        }

        /// <summary>
        /// Gets a specific position by name
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with position</returns>
        [HttpGet]
        [Route("/positions/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var position = await _positionService.GetAsync(id);
            return Ok(new { position });
        }

        /// <summary>
        /// Creates a new Position
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http response code with position</returns>
        [HttpPost]
        [Route("/positions")]
        public async Task<IActionResult> CreateAsync(PositionCreateOptions options)
        {
            var position = _positionService.CreateAsync(options).Result;
            var histOptions = new Position_HistoryCreateOptions();
            histOptions.EffectiveDate = options.EffectiveDate;
            histOptions.ChangeNotes = options.ChangeNotes;
            histOptions.PositionId = position.Id;
            //histOptions.taskIds = position.Id;
            await _positionhistoryService.CreateAsync(histOptions);
            await _ver_posService.CreateVersionAsync(position);
            return Ok( new { position, message = _localier["PosCreated"] });
        }

        /// <summary>
        /// Get Stats for Positions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/positions/stats")]
        public async Task<IActionResult> GetSHStats()
        {
            var result = await _positionService.GetSHStats();
            return Ok( new { result });
        }


        /// <summary>
        /// Make copy of Position along with linkages
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/positions/{id}/copy")]
        public async Task<IActionResult> CopyProcedureAsync(int id, PositionCreateOptions options)
        {
            var result = await _positionService.CopyPositionWithLinkages(id, options);
            return Ok( new { result, message = _localier["PositionCopyCreated"] });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/positions/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, PositionUpdateOptions options)
        {
            var verPosition = await _positionService.GetAsync(id);
            await _ver_posService.CreateVersionAsync(verPosition);
            var position = await _positionService.UpdateAsync(id, options);

            var histOptions = new Position_HistoryCreateOptions();
            histOptions.EffectiveDate = options.EffectiveDate;
            histOptions.ChangeNotes = "Position updated with position Id => " + position.Id;
            histOptions.PositionId = position.Id;
            await _positionhistoryService.CreateAsync(histOptions);

            return Ok( new { position, message = _localier["PosUpdated"] });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/positions/{id}")]
        public async Task<IActionResult> DeleteAsync(int id, Position_HistoryCreateOptions options)
        {
            switch (options.ActionType)
            {
                case "inactive":
                default:
                    await _positionService.InActiveAsync(id, options);
                    break;
                case "active":
                    await _positionService.ActiveAsync(id, options);
                    break;
                case "delete":
                    await _positionService.DeleteAsync(id, options);
                    break;
            }
            options.PositionId = id;
            await _positionhistoryService.CreateAsync(options);
            return Ok( new { message = _localier["posDeleted"] });
        }

        /// <summary>
        /// Get All Positions not linked to specific option
        /// </summary>
        /// <param name="notlinkedWith"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/positions/notlinked/{notlinkedWith}")]
        public async Task<IActionResult> GetNotLinkedPosition(string notlinkedWith)
        {
            var result = await _positionService.GetPosNotLinkedTo(notlinkedWith);
            return Ok( new { result });
        }

        /// <summary>
        /// Deletes a position
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/positions/parent")]
        public async Task<IActionResult> DeleteAsync(PositionOption options)
        {
            foreach (var tId in options.positionIds)
            {
                string status = "";
                switch (options.ActionType.ToLower())
                {
                    case "inactive":
                    default:
                        await _positionService.InActivePosition(tId);
                        await _positionService.PositionEndDateChange(tId,options.EffectiveDate);
                        status = "Inactive";
                        break;
                    case "active":
                        await _positionService.ActivePosition(tId);
                        status = "Active";
                        break;
                    case "delete":
                        await _positionService.DeletePosition(tId);
                        status = "Delete";
                        break;
                }

                var histOptions = new Position_HistoryCreateOptions();
                histOptions.ChangeNotes = options.ChangeNotes;
                histOptions.EffectiveDate = options.EffectiveDate;
                histOptions.PositionId = tId;
                if(options.ChangeNotes == null)
                {
                    histOptions.ChangeNotes = "Position " + status + "d with Position Id => " + tId;
                }
                await _positionhistoryService.CreateAsync(histOptions);
            }
            
            return Ok( new { message = _localier["Position_" + options.ActionType.ToLower()] });
        }

        [HttpGet]
        [Route("/positions/list/{notlinkedWith}")]
        public async Task<IActionResult> GetActiveInactivePosition(string notlinkedWith)
        {
            var result = await _positionService.GetActiveInactivePosition(notlinkedWith);
            return Ok( new { result });
        }
        [HttpGet]
        [Route("/positions/positionTask")]
        public async Task<IActionResult> GetPositionByPositionTaskAsync()
        {
            var positions = await _positionService.GetPositonWithPositionTaskAsync();
            return Ok( new { positions });
        }

        [HttpGet]
        [Route("/positions/positionByName/{positionName}")]
        public async Task<IActionResult> GetPositionByNameAsync(string positionName)
        {
            var position = await _positionService.GetPositionByNameAsync(positionName);
            return Ok(new { position });
        }
    }
}
