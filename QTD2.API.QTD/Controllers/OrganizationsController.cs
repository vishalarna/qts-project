using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Authentication;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.HttpClients;
using QTD2.Infrastructure.Model.Organization;
using Sustainsys.Saml2.Metadata;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        private readonly IStringLocalizer<OrganizationsController> _localizer;
        private readonly IInstanceFetcher _instanceFetcher;
        private readonly IDatabaseResolver _databaseResolver;

        public OrganizationsController(IOrganizationService organizationService, IStringLocalizer<OrganizationsController> localizer, IInstanceFetcher instanceFetcher, IDatabaseResolver databaseResolver)
        {
            _organizationService = organizationService;
            _localizer = localizer;
            _instanceFetcher = instanceFetcher;
            _databaseResolver = databaseResolver;
        }

        /// <summary>
        /// Gets a list of Organizations
        /// </summary>
        /// <returns>return Http response code with organizations</returns>
        [HttpGet]
        [Route("/organizations")]
        public async Task<IActionResult> GetAsync()
        {
            var organizations = await _organizationService.GetAsync();
            return Ok( new { organizations });
        }

        [HttpGet]
        [Route("/organizations/simplified")]
        public async Task<IActionResult> GetSimplifiedData()
        {
            var result = await _organizationService.GetSimplifiedDataAsync();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/organizations/order/{orderBy}")]
        public async Task<IActionResult> GetAllOrderBy(string orderBy)
        {
            var result = await _organizationService.GetAllOrderByAsync(orderBy);
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new Organization
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/organizations")]
        public async Task<IActionResult> CreateAsync(OrganizationCreateOptions options)
        {
            var organization = await _organizationService.CreateAsync(options);
            return Ok( new { organization, message = _localizer["OrgCreated"] });
        }

        /// <summary>
        /// Gets a specific organization by name
        /// </summary>
        /// <returns>return Http response code with organization</returns>
        [HttpGet]
        [Route("/organizations/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var organization = await _organizationService.GetAsync(id);
            return Ok( new { organization });
        }

        /// <summary>
        /// Updates a specific organization by name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/organizations/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, OrganizationUpdateOptions options)
        {
            var organization = await _organizationService.UpdateAsync(id, options);
            return Ok( new { organization, message = _localizer["OrgUpdated"] });
        }

        /// <summary>
        /// Deletes a specific organization by name
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/organizations/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _organizationService.DeleteAsync(id);
            return Ok( new { message = _localizer["OrgDeleted"] });
        }

        [HttpGet]
        [Route("/organizations/publicOrganizations")]
        public async Task<IActionResult> GetPublicOrganization()
        {
            var result = await _organizationService.GetPublicOrganizationAsync();
            return Ok(new { result });
        }
    }
}
