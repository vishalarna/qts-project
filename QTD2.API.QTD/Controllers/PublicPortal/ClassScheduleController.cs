using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Application.Interfaces.Services.QTD;

namespace QTD2.API.QTD.Controllers.PublicPortal
{

    [AllowAnonymous]
    public class ClassScheduleController : Controller
    {
        private readonly IClassScheduleService _classScheduleService;
        private readonly IDatabaseResolver _databaseResolver;
        private readonly IInstanceFetcher _instanceFetcher;

        public ClassScheduleController(IClassScheduleService classScheduleService, IDatabaseResolver databaseResolver, IInstanceFetcher instanceFetcher)
        {
            _databaseResolver = databaseResolver;
            _classScheduleService = classScheduleService;
            _instanceFetcher = instanceFetcher;
        }

        [HttpGet]
        [Route("public/classSchedules/{instance}")]
        public async Task<IActionResult> GetAvailableClassSchedules(string instance)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instance);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);

            var classSchedules = await _classScheduleService.GetPublicallyAvailableClassesAsync();

            return Ok(new { classSchedules });
        }
    }
}
