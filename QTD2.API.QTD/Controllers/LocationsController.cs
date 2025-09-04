using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Locations;
using QTD2.Infrastructure.Model.Location_History;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly ILocationHistoryService _locationHistoryService;
        private readonly ILocation_CategoryService _location_CategoryService;
        private readonly ILocation_CategoryHistoryService _locCatHistoryService;
        private readonly IStringLocalizer<LocationsController> _localizer;

        public LocationsController(
            ILocationService locationService,
            ILocationHistoryService locationHistoryService,
            ILocation_CategoryService location_CategoryService,
            ILocation_CategoryHistoryService locCatHistoryService,
            IStringLocalizer<LocationsController> localizer)
        {
            _locationService = locationService;
            _locationHistoryService = locationHistoryService;
            _location_CategoryService = location_CategoryService;
            _locCatHistoryService = locCatHistoryService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of locations
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/locations")]
        public async Task<IActionResult> GetAsync()
        {
            var locList = await _locationService.GetAsync();
            return Ok( new { locList });
        }

        /// <summary>
        /// Gets location
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/locations/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var loc = await _locationService.GetAsync(id);
            return Ok( new { loc });
        }

        /// <summary>
        /// Creates a location
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/locations")]
        public async Task<IActionResult> CreateAsync(Location_CreateOptions options)
        {
            var loc = await _locationService.CreateAsync(options);
            var histOptions = new Location_HistoryCreateOptions();
            histOptions.Notes = options.LocDescription;
            histOptions.LocationId = loc.Id;
            histOptions.EffectiveDate = options.EffectiveDate;
            await _locationHistoryService.CreateAsync(histOptions);
            return Ok( new { loc, message = _localizer["LocCreated"] });
        }

        /// <summary>
        /// Updates location
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPut]
        [Route("/locations/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Location_CreateOptions options)
        {
            var loc = await _locationService.UpdateAsync(id, options);
            var histOptions = new Location_HistoryCreateOptions();
            histOptions.Notes = options.Notes;
            histOptions.LocationId = loc.Id;
            histOptions.EffectiveDate = options.EffectiveDate;
            await _locationHistoryService.CreateAsync(histOptions);
            return Ok( new { loc, message = _localizer["LocUpdated"] });
        }

        /// <summary>
        /// Deletes a location
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/locations/")]
        public async Task<IActionResult> DeleteAsync(Location_HistoryCreateOptions options)
             {
            Location loc = null;
            switch (options.ActionType)
            {
                case "inactive":
                default:
                    await _locationService.InActiveAsync(options);
                    break;
                case "active":
                    await _locationService.ActiveAsync(options);
                    break;
                case "delete":
                    await _locationService.DeleteAsync(options);
                    break;
            }
            foreach(var locId in options.locationIds)
            {
                options.LocationId = locId;
                await _locationHistoryService.CreateAsync(options);
            }
            return Ok( new { message = _localizer["LocationDeleted"] });
        }

        [HttpGet]
        [Route("/locations/count")]
        public async Task<IActionResult> GetLocationCountAsync()
        {
            var result = await _locationService.getCount();
            return Ok( new { result });
        }

        /// <summary>
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/locations/stats")]
        public async Task<IActionResult> GetStatsAsync()
        {
            var stats = await _locationService.GetStatsCount();
            return Ok( new { stats });
        }

        //active inactive cat and ins along with workbook admins
        [HttpGet]
        [Route("/locations/{option}/catlist")]
        public async Task<IActionResult> GetCatActiveInactiveList(string option)
        {
            var result = await _locationService.GetCatActiveInactive(option);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/locations/{option}/loclist")]
        public async Task<IActionResult> GetInsActiveInactiveList(string option)
        {
            var result = await _locationService.GetLocActiveInactive(option);
            return Ok( new { result });
        }
    }
}
