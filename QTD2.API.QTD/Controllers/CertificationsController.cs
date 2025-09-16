using System;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.Model.Certification;
using QTD2.Infrastructure.Model.Certification_History;
using QTD2.Infrastructure.Model.CertificationSubRequirement;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class CertificationsController : ControllerBase
    {
        private readonly ICertificationService _certificationService;
        private readonly ICertificationHistoryService _certificationHistoryService;
        private readonly ICertifyingBodiesService _certifyingbodiesService;
        private readonly ICertificationSubRequirementService _certificationSubRequirementService;
        //private readonly ICertifyingBodies_HistoryService _certifyingbodiesHistoryService;
        private readonly IStringLocalizer<CertificationsController> _localizer;
        private readonly IInstanceFetcher _instanceFetcher;
        private readonly IDatabaseResolver _databaseResolver;

        public CertificationsController(
            ICertificationService certificationService,
            ICertificationHistoryService certificationHistoryService,
            ICertifyingBodiesService certifyingbodiesService,
        //ICertifyingBodies_HistoryService certifyingBodiesHistoryService,
        IStringLocalizer<CertificationsController> localizer, ICertificationSubRequirementService certificationSubRequirementService,
        IInstanceFetcher instanceFetcher,
        IDatabaseResolver databaseResolver)
        {
            _certificationService = certificationService;
            _certificationHistoryService = certificationHistoryService;
            _certifyingbodiesService = certifyingbodiesService;
            //_certifyingbodiesHistoryService = certifyingBodiesHistoryService;
            _localizer = localizer;
            _certificationSubRequirementService = certificationSubRequirementService;
            _instanceFetcher = instanceFetcher;
            _databaseResolver = databaseResolver;
        }

        /// <summary>
        /// Gets a list of Certifications
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/certifications")]
        public async Task<IActionResult> GetAsync()
        {
            var locList = await _certificationService.GetAsync();
            return Ok(new { locList });
        }

        /// <summary>
        /// Gets Cert
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/certifications/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var loc = await _certificationService.GetAsync(id);
            return Ok(new { loc });
        }
        
        [HttpGet]
        [Route("/certifications/{id}/ila/{ilaId}")]
        public async Task<IActionResult> GetAsync(int id,int ilaId)
        {
            var loc = await _certificationService.GetWithIlaDataAsync(id, ilaId);
            return Ok( new { loc });
        }
        [HttpDelete]
        [Route("/certifications/{id}/ila/{ilaId}")]
        public async Task<IActionResult> RemoveLinkWithIlaDataAsync(int id,int ilaId)
        {
            var result = await _certificationService.RemoveLinkWithIlaDataAsync(id, ilaId);
            return Ok( new { result });
        }
        [HttpPost]
        [Route("/certifications/ila/save")]
        public async Task<IActionResult> SaveIlaCertificationLinkAsync(ILACertificationVM data)
        {
            var loc = await _certificationService.SaveIlaCertificationLinkAsync(data);
            return Ok( new { loc });
        }

        /// <summary>
        /// Creates a cert
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/certifications")]
        public async Task<IActionResult> CreateAsync(CertificationCreateOptions options)
        {
            var cer = await _certificationService.CreateAsync(options);
            var histOptions = new Certification_HistoryCreateOptions();

            histOptions.Notes = options.Notes;
            histOptions.CertId = cer.Id;
            histOptions.EffectiveDate = options.EffectiveDate;
            await _certificationHistoryService.CreateAsync(histOptions);

            var options1 = new CertificationSubRequirementCreateOptions();
            options1.CertificationId = cer.Id;
            options1.ReqName = options.CertSubReqName;
            options1.ReqHour = options.CertSubReqHours;
            await _certificationSubRequirementService.CreateAsync(options1);
            return Ok( new { cer, message = _localizer["CertCreated"] });
        }

        /// <summary>
        /// Updates certification
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPut]
        [Route("/certifications/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, CertificationCreateOptions options)
        {
            var cer = await _certificationService.UpdateAsync(id, options);
            var histOptions = new Certification_HistoryCreateOptions();
            histOptions.Notes = options.Notes;
            histOptions.CertId = cer.Id;
            histOptions.EffectiveDate = options.EffectiveDate;
            await _certificationHistoryService.CreateAsync(histOptions);
            var options1 = new CertificationSubRequirementCreateOptions();
            options1.CertificationId = cer.Id;
            options1.ReqName = options.CertSubReqName;
            options1.ReqHour = options.CertSubReqHours;
            options1.CertificationSubRequirementsIds = options.CertificationSubRequirementsIds;
            await _certificationSubRequirementService.UpdateAsync(options1);
            return Ok( new { cer, message = _localizer["CertUpdated"] });
        }

        /// <summary>
        /// Deletes a certification
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/certifications")]
        public async Task<IActionResult> DeleteAsync(Certification_HistoryCreateOptions options)
             {
            Certification cer = null;
            switch (options.ActionType)
            {
                case "inactive":
                default:
                    await _certificationService.InActiveAsync(options);
                    break;
                case "active":
                    await _certificationService.ActiveAsync(options);
                    break;
                case "delete":
                    await _certificationService.DeleteAsync(options);
                    break;
            }
            foreach(var cerId in options.certIds)
            {
                options.CertId = cerId;
                await _certificationHistoryService.CreateAsync(options);
            }
            return Ok( new { message = _localizer["CertDeleted"] });
        }

        [HttpGet]
        [Route("/certifications/count")]
        public async Task<IActionResult> GetCertificationCountAsync()
        {
            var result = await _certificationService.getCount();
            return Ok( new { result });
        }

        /// <summary>
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/certifications/stats")]
        public async Task<IActionResult> GetStatsAsync()
        {
            var stats = await _certificationService.GetStatsCount();
            return Ok( new { stats });
        }


        [HttpGet]
        [Route("/certifications/issuingauthority/nest")]
        public async Task<IActionResult> GetCertCategoryWithCert()
        {
            var result = await _certifyingbodiesService.GetCertCategoryWithCert();
            return Ok( new { result });
        }



        [HttpGet]
        [Route("/certifications/ila/{id}")]
        public async Task<IActionResult> GetIlaLinkedCertifications(int id)
        {
            var result = await _certificationService.getLinkedCertifications(id);
            return Ok( new { result });
        }


        /// <summary>
        /// Gets a list of Certifying Bodies
        /// </summary>
        /// <returns>Http Response Code with certifyingBodies</returns>
        [HttpGet]
        [Route("/certifications/certifyingBodies")]
        public async Task<IActionResult> GetCertifyingBodiesAsync()
        {
            var certifyingBodies = await _certifyingbodiesService.GetAsync();
            return Ok( new { certifyingBodies });
        }

        /// <summary>
        /// Gets a list of Certifications sub requirement
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/certifications/{id}/subrequirement")]
        public async Task<IActionResult> GetCertificationSubRequirementAsync(int id)
        {
            var result = await _certificationSubRequirementService.GetAsync(id);
            return Ok( new { result });
        }

        //active inactive certifications and catgeroies
        [HttpGet]
        [Route("/certifications/{option}/catlist")]
        public async Task<IActionResult> GetCatActiveInactiveList(string option)
        {
            var result = await _certificationService.GetCatActiveInactive(option);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/certifications/{option}/certlist")]
        public async Task<IActionResult> GetCertActiveInactiveList(string option)
        {
            var result = await _certificationService.GetCertActiveInactive(option);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/certifications/nercCertList")]
        public async Task<IActionResult> GetNercCertList()
        {
            var result = await _certificationService.GetNercCertificatesAsync();
            return Ok(new { result });
        }

        [HttpGet]
        [Route("/certifications/{certId}/subrequirements")]
        public async Task<IActionResult> GetSubRequirementsByCertIdAsync(int certId)
        {
            var subReqs = await _certificationService.GetSubRequirementsByCertIdAsync(certId);
            return Ok(new { subReqs });
        }

    }
}
