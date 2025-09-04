using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.ActivityNotification;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ActivityNotificationController : ControllerBase
    {
        private readonly IActivityNotificationService _activityNotificationService;
        private readonly IStringLocalizer<ActivityNotificationController> _localizer;

        public ActivityNotificationController(IActivityNotificationService activityNotificationService, IStringLocalizer<ActivityNotificationController> localizer)
        {
           _activityNotificationService = activityNotificationService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of Activity Notification
        /// </summary>
        /// <returns>Http Response Code with Activity Notification</returns>
        [HttpGet]
        [Route("/activitynotification")]
        public async Task<IActionResult> GetActivityNotificationAsync()
        {
            var result = await _activityNotificationService.GetAsync();
            return Ok(new { result });
        }

        /// <summary>
        /// Creates a new Activity Notification
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/activitynotification")]
        public async Task<IActionResult> CreateActivityNotificationAsync(ActivityNotificationsCreateOptions options)
        {
            var result = await _activityNotificationService.CreateAsync(options);
            return Ok(new { message = _localizer["ActivityNotificationCreated"].Value });
        }

        /// <summary>
        /// Gets a specific Activity Notification by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Activity Notification</returns>
        [HttpGet]
        [Route("/activitynotification/{id}")]
        public async Task<IActionResult> GetActivityNotificationAsync(int id)
        {
            var result = await _activityNotificationService.GetAsync(id);
            return Ok(new { result });
        }

        /// <summary>
        /// Updates  a specific Activity Notification by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/activitynotification/{id}")]
        public async Task<IActionResult> UpdateActivityNotificationAsync(int id, ActivityNotificationsUpdateOptions options)
        {
            var result = await _activityNotificationService.UpdateAsync(id, options);
            return Ok(new { message = _localizer["ActivityNotificationUpdated"].Value });
        }

    }
}
