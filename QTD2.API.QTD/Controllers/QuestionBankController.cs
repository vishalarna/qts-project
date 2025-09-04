using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.QuestionBank;
using QTD2.Infrastructure.Model.QuestionBankHistory;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class QuestionBankController : ControllerBase
    {
        private readonly IQuestionBankService _questionBankService;
        private readonly IQuestionBankHistoryService _questionBankHistoryService;
        private readonly IStringLocalizer<QuestionBankController> _localizer;

        public QuestionBankController(IQuestionBankService questionBankService, IStringLocalizer<QuestionBankController> localizer, IQuestionBankHistoryService questionBankHistoryService)
        {
            _questionBankService = questionBankService;
            _localizer = localizer;
            _questionBankHistoryService = questionBankHistoryService;
        }

        /// <summary>
        /// Gets a list of Rating Scales
        /// </summary>
        /// <returns>Http Response Code with Rating Scales</returns>
        [HttpGet]
        [Route("/questionBank")]
        public async Task<IActionResult> GetQuestionBankAsync()
        {
           
            var questions = await _questionBankService.GetAsync();
            return Ok( new { questions });
        }

        /// <summary>
        /// Creates a new Rating Scale
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/questionBank")]
        public async Task<IActionResult> CreateQuestionAsync(QuestionBankCreateOptions options)
        {
            var result = await _questionBankService.CreateAync(options);
            var histOptions = new QuestionBankHistoryCreateOptions();
            histOptions.EffectiveDate = DateTime.UtcNow;
            histOptions.QuestionBankNotes = "Question Created : "+options.Stem;
            histOptions.QuestionBankId = result.Id;
            await _questionBankHistoryService.CreateAsync(histOptions);
            return Ok( new { result });
        }

        /// <summary>
        /// Gets a specific Question by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Question</returns>
        [HttpGet]
        [Route("/questionBank/{id}")]
        public async Task<IActionResult> GetQuestionAsync(int id)
        {
            var question = await _questionBankService.GetAsync(id);
            return Ok( new { question });
        }

        /// <summary>
        /// Updates  a specific Rating Scale by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/questionBank/{id}")]
        public async Task<IActionResult> UpdateQuestionAsync(int id, QuestionBankCreateOptions options)
        {
            var ratingScale = await _questionBankService.UpdateAsync(id, options);
            var histOptions = new QuestionBankHistoryCreateOptions();
            histOptions.EffectiveDate = DateTime.UtcNow;
            histOptions.QuestionBankNotes = "Question Updated : " + options.Stem;
            histOptions.QuestionBankId = id;
            await _questionBankHistoryService.CreateAsync(histOptions);
            return Ok( new { message = _localizer["questionUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific Question by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/questionBank/{id}")]
        public async Task<IActionResult> DeleteQuestionAsync(int id, QuestionBankHistoryCreateOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _questionBankService.DeactivateAsync(id);
                    break;
                case "active":
                    await _questionBankService.ActivateAsync(id);
                    break;
                case "delete":
                    await _questionBankService.DeleteAsync(id);
                    break;
            }
            var histOptions = new QuestionBankHistoryCreateOptions();
            histOptions.EffectiveDate = (DateTime)options.EffectiveDate;
            histOptions.QuestionBankNotes = options.QuestionBankNotes;
            histOptions.QuestionBankId = id;
            await _questionBankHistoryService.CreateAsync(histOptions);
            return Ok( new { message = _localizer[$"questionBank-{options.ActionType.ToLower()}"].Value });
        }
        [HttpPost]
        [Route("/questionBank/createAndLinkQuestion")]
        public async Task<IActionResult> CreateAndLinkQuestionAsync(QuestionBankCreateOptions options)
        {
            var result = await _questionBankService.CreateEvalutaionAndLinkQUestionAync(options);
            return Ok( new { message = _localizer["questionsCreated"].Value });
        }
    }
}
