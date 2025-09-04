using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Database.Interfaces;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers.Instance
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstanceInstructorCategoryController : ControllerBase
    {
        private readonly IInstanceFetcher _instanceFetcher;
        private readonly IStringLocalizer<InstanceClientUserController> _localizer;
        private readonly IInstructor_CategoryService _instructorCategoryService;
        private readonly IDatabaseResolver _databaseResolver;

        public InstanceInstructorCategoryController(IInstanceFetcher instanceFetcher,
           IStringLocalizer<InstanceClientUserController> localizer,
           IInstructor_CategoryService instructorCategoryService,
           IDatabaseResolver databaseResolver)
        {
            _instanceFetcher = instanceFetcher;
            _localizer = localizer;
            _instructorCategoryService = instructorCategoryService;
            _databaseResolver = databaseResolver;
        }

        [HttpGet]
        [Route("/instance/{instanceName}/instructors/categories")]
        public async Task<IActionResult> GetAsync(string instanceName)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
            var ins_CatList = await _instructorCategoryService.GetAsync();
            return Ok(new { ins_CatList });
        }



    }
}
