using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.PersonActivityNotificationVM;

namespace QTD2.API.QTD.Controllers
{
    public partial class PersonController : ControllerBase
    {
        /// <summary>
        /// Links the ActivityNotification with specific Person
        /// </summary>
        /// <returns>Http Response Code with Person</returns>
        [HttpPost]
        [Route("/persons/{id}/activitynotification")]

        public async Task<IActionResult> LinkActivityNotificationAsync(int id, PersonActivityNotification_VM options)
        {
            var result = await _personService.LinkActivityNotificationAsync(id, options);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlinks the ActivityNotification with specific persons
        /// </summary>
        /// <returns>Http Response Code with Person</returns>
        [HttpDelete]
        [Route("/persons/{id}/activitynotification")]
        public async Task<IActionResult> UnlinkActivityNotificationAsync(int id, PersonActivityNotification_VM options)
        {
            await _personService.UnlinkActivityNotificationAsync(id, options);
            return Ok( new { message = _stringLocalizer["ActivityNotificationUnlinkedFromPerson"].Value });
        }

        /// <summary>
        /// Get the ActivityNotification with specific persons
        /// </summary>
        /// <returns>Http Response Code with Linked Activity Notification</returns>
        [HttpGet]
        [Route("/persons/{id}/activitynotification")]
        public async Task<IActionResult> GetLinkedActivityNotificationAsync(int id)
        {
            var result = await _personService.GetLinkedActivityNotificationAsync(id);
            return Ok( new { result });
        }
    }
}
