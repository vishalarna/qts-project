using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.MetaILA_SummaryTest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class MetaILA_SummaryTestController : ControllerBase
    {
        private readonly IMetaILA_SummaryTestService _metaILASummaryTestService;

        public MetaILA_SummaryTestController(IMetaILA_SummaryTestService metaILA_SummaryTestService)
        {
            _metaILASummaryTestService = metaILA_SummaryTestService;
        }

        /// <summary>
        /// Create a MetaILA_SummaryTest
        /// </summary>
        [HttpPost]
        [Route("/metaIlaSummaryTest")]
        public async Task<IActionResult> CreateAsync(MetaILA_SummaryTest_ViewModel options)
        {
            var result = await _metaILASummaryTestService.CreateAsync(options);
            return Ok( new {result });
        }

        /// <summary>
        /// Get a MetaILA_SummaryTest by id
        /// </summary>
        [HttpGet]
        [Route("/metaIlaSummaryTest/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _metaILASummaryTestService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Update a MetaILA_SummaryTest by id, and return the MetaILA_SummaryTest
        /// </summary>
        [HttpPut]
        [Route("/metaIlaSummaryTest/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, MetaILA_SummaryTest_ViewModel options)
        {
           var result = await _metaILASummaryTestService.UpdateAsync(id,options);
           return Ok( new { result });
        }

        [HttpPost]
        [Route("/metaIlaSummaryTest/ilas/testItems")]
        public async Task<IActionResult> GetTestItemsFromILAs(GetTestItemsByILAsOption option)
        {
            var result = await _metaILASummaryTestService.GetTestItemsFromILAsAsync(option);
            return Ok( new { result });
        }
    }
}
