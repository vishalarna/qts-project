using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.DeliveryMethod;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryMethodController : ControllerBase
    {
        private readonly IDeliveryMethodService _deliveryMethodService;
        private readonly IStringLocalizer<DeliveryMethodController> _localizer;

        public DeliveryMethodController(IDeliveryMethodService deliveryMethodService, IStringLocalizer<DeliveryMethodController> localizer)
        {
            _deliveryMethodService = deliveryMethodService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of DeliveryMethods
        /// </summary>
        /// <returns>Http Response Code with DeliveryMethods</returns>
        [HttpGet]
        [Route("/deliveryMethods")]
        public async Task<IActionResult> GetDeliveryMethodsAsync()
        {
            var result = await _deliveryMethodService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new DeliveryMethod
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/deliveryMethods")]
        public async Task<IActionResult> CreateDeliveryMethodAsync(DeliveryMethodCreateOptions options)
        {
            var result = await _deliveryMethodService.CreateAsync(options);
            return Ok( new { message = _localizer["DeliveryMethodCreated"].Value });
        }

        /// <summary>
        /// Gets a specific DeliveryMethod by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with DeliveryMethod</returns>
        [HttpGet]
        [Route("/deliveryMethods/{id}")]
        public async Task<IActionResult> GetDeliveryMethodAsync(int id)
        {
            var result = await _deliveryMethodService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Gets a specific DeliveryMethod by isNerc
        /// </summary>
        /// <param name="isNerc"></param>
        /// <returns>Http Response Code with DeliveryMethod</returns>
        [HttpGet]
        [Route("/deliveryMethods/{isNerc}/isNerc")]
        public async Task<IActionResult> GetNercDeliveryMethodAsync(bool isNerc)
        {
            var result = await _deliveryMethodService.GetNercAsync(isNerc);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific DeliveryMethod by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/deliveryMethods/{id}")]
        public async Task<IActionResult> UpdateDeliveryMethodAsync(int id, DeliveryMethodUpdateOptions options)
        {
            var result = await _deliveryMethodService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["DeliveryMethodUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific DeliveryMethod by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/deliveryMethods/{id}")]
        public async Task<IActionResult> DeleteDeliveryMethodAsync(int id, DeliveryMethodOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _deliveryMethodService.InActiveAsync(id);
                    break;
                case "active":
                    await _deliveryMethodService.ActiveAsync(id);
                    break;
                case "delete":
                    await _deliveryMethodService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"DeliveryMethod-{options.ActionType.ToLower()}"].Value });
        }
    }
}
