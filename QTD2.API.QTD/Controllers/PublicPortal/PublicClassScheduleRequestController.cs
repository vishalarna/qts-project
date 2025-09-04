using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Application.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.Model.PublicClassScheduleRequest;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers.PublicPortal
{

    [Route("api/[controller]")]
    [ApiController]
    public class PublicClassScheduleRequestController : ControllerBase
    {
        private readonly IPublicClassScheduleRequestService _publicClassScheduleRequestService;
        private readonly IDatabaseResolver _databaseResolver;
        private readonly IInstanceFetcher _instanceFetcher; 
        private readonly ICertificationService _certificationService;
        private readonly IOrganizationService _organizationService;
        private readonly IClientSettingsService _clientSettingsService;

        public PublicClassScheduleRequestController(
            IPublicClassScheduleRequestService publicClassScheduleRequestService,
            IDatabaseResolver databaseResolver,
            IInstanceFetcher instanceFetcher,
            ICertificationService certificationService,
            IOrganizationService organizationService,
            IClientSettingsService clientSettingsService
            )
        {
            _publicClassScheduleRequestService = publicClassScheduleRequestService;
            _databaseResolver = databaseResolver;
            _instanceFetcher = instanceFetcher;
            _certificationService = certificationService;
            _organizationService = organizationService;
            _clientSettingsService = clientSettingsService;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("/public/publicClassScheduleRequest/{instance}/{classScheduleId}")]
        public async Task<IActionResult> RequestRegistrationAsync(string instance, int classScheduleId, [FromBody] CreatePublicClassScheduleRequestModel model)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instance);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;

            PublicClassScheduleRequest publicClassScheduleRequest;
            if (!string.IsNullOrEmpty(model.NercCertNumber))
            {
                if (!model.NercCertExpiration.HasValue)
                    return BadRequest("Nerc Certification Expiration must be provided if the Certification Number is included");

                publicClassScheduleRequest = new PublicClassScheduleRequest(classScheduleId, remoteIpAddress.ToString(), model.FirstName, model.LastName, model.Company, model.EmailAddress, model.NercCertNumber, model.NercCertExpiration, model.NercCertType);
            }
            else
            {
                publicClassScheduleRequest = new PublicClassScheduleRequest(classScheduleId, remoteIpAddress.ToString(), model.FirstName, model.LastName, model.Company, model.EmailAddress);
            }

            await _publicClassScheduleRequestService.CreatePublicClassScheduleRequestAsync(publicClassScheduleRequest);

            return Ok();
        }      
              
        [AllowAnonymous]
        [HttpGet]
        [Route("/public/nerc/{instanceName}")]
        public async Task<IActionResult> GetNercCertificatesList(string instanceName)
        {
            var instance = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instance.DatabaseName);
            var result = await _certificationService.GetNercCertificatesAsync();
            return Ok(new { result });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/public/organizations/{instanceName}")]
        public async Task<IActionResult> GetPublicOrganizationsAsync(string instanceName)
        {
            var instance = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instance.DatabaseName);
            var organizations = await _organizationService.GetPublicOrganizationAsync();
            return Ok(new { organizations });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/public/{instanceName}/ila/{id}")]
        public async Task<IActionResult> GetPublicIlaRequirement(string instanceName, int id)
        {
            var instance = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instance.DatabaseName);
            var iLACompletionRequirement = await _publicClassScheduleRequestService.GetILACompletionRequirementAsync(id);
            return Ok(new { iLACompletionRequirement });
        }

    }
}
