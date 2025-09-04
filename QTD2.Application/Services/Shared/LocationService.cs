using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.Locations;
using QTD2.Infrastructure.Model.Location_History;
using ILocationCategoryDomainService = QTD2.Domain.Interfaces.Service.Core.ILocation_CategoryService;
using ILocationDomainService = QTD2.Domain.Interfaces.Service.Core.ILocation_Service;
using QTD2.Domain.Exceptions;

namespace QTD2.Application.Services.Shared
{
    public class LocationService: ILocationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<LocationService> _localizer;
        private readonly ILocationDomainService _locationService;
        private readonly ILocationCategoryDomainService _locationCategoryService;
        private readonly UserManager<AppUser> _userManager;


        public LocationService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<LocationService> localizer, ILocationDomainService locationService, UserManager<AppUser> userManager, ILocationCategoryDomainService locationCategoryService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _locationService = locationService;
            _userManager = userManager;
            _locationCategoryService = locationCategoryService;
        }


        public async System.Threading.Tasks.Task ActiveAsync(Location_HistoryCreateOptions options)
        {
            if (options != null && options.locationIds.Count() > 0)
            {
                foreach (var location in options.locationIds)
                {
                    var obj = await GetAsync(location);
                    obj.Activate();

                    var validationResult = await _locationService.UpdateAsync(obj);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));

                    }

                }

            }
            else
            {
                throw new QTDServerException("Location Ids not found");
            }

        }


        public async Task<Location> CreateAsync(Location_CreateOptions options)
        {
            var obj = (await _locationService.FindAsync(x => x.LocName == options.LocName)).FirstOrDefault();
            if (obj == null)
            {
                obj = new Location(options.LocCategoryID, options.LocNumber, options.LocName, options.LocDescription, options.LocAddress, options.LocCity, options.LocState, options.LocZipCode, options.LocPhone, DateOnly.FromDateTime(options.EffectiveDate));
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["Location Title Already Exists"].Value);
            }

           
            obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            obj.CreatedDate = DateTime.Now;
            var validationResult = await _locationService.AddAsync(obj);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                return obj;
            }

        }

        public async System.Threading.Tasks.Task DeleteAsync(Location_HistoryCreateOptions options)
        {
            if (options != null && options.locationIds.Count() > 0)
            {
                foreach (var location in options.locationIds)
                {
                    var obj = await GetAsync(location);
                    obj.Delete();

                    var validationResult = await _locationService.UpdateAsync(obj);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));

                    }

                }

            }
            else
            {
                throw new QTDServerException("Location Ids not found");
            }
        }


        public async Task<List<Location>> GetAsync()
        {
            var obj_list = await _locationService.AllAsync();
            //obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded);
            return obj_list?.OrderBy(x=>x.LocName).ToList();
        }


        public async Task<Location> GetAsync(int id)
        {
            var obj = await _locationService.GetWithIncludeAsync(id, new string[] { "Location_Category", "ClassSchedules" });
           // var obj = await _locationService.GetWithIncludeAsync(id, new string[] { nameof(Location_Category) });
            if (obj != null)
            {

                return obj;

            }
            else
            {
                throw new QTDServerException( _localizer["RecordNotFound"].Value);
            }
        }

        public async System.Threading.Tasks.Task InActiveAsync(Location_HistoryCreateOptions options)
        {
            if (options != null && options.locationIds.Count() > 0)
            {
                foreach (var location in options.locationIds)
                {
                    var obj = await GetAsync(location);

                    // var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

                    // if (result.Succeeded)
                    // {
                    obj.Deactivate();

                    var validationResult = await _locationService.UpdateAsync(obj);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));

                    }

                }

            }
            else
            {
                throw new QTDServerException("Location Ids not found");
            }
        }

        public async Task<Location> UpdateAsync(int id, Location_CreateOptions options)
        {
            var obj = await GetAsync(id);

            obj.LocCategoryID = options.LocCategoryID;
            obj.LocName = options.LocName;
            obj.LocNumber = options.LocNumber;
            obj.LocDescription = options.LocDescription;
            obj.LocAddress = options.LocAddress;
            obj.LocState = options.LocState;
            obj.LocCity = options.LocCity;
            obj.LocZipCode = options.LocZipCode;
            obj.LocPhone = options.LocPhone;
            obj.EffectiveDate = DateOnly.FromDateTime(options.EffectiveDate);
            
            obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            obj.ModifiedDate = DateTime.Now;

            var validationResult = await _locationService.UpdateAsync(obj);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                return obj;
            }

        }

        public async Task<int> getCount()
        {
            var result = await _locationService.AllQueryWitDeletedCount();
            return result;
        }

        public async Task<LocationStatsVM> GetStatsCount()
        {
            var instructors = await _locationService.AllQuery().Select(x => x.Id).ToListAsync();
            var categories = await _locationCategoryService.AllQuery().Select(x => x.Id).ToListAsync();

            var stats = new LocationStatsVM()
            {
                LocationActive = await _locationService.GetCount(x => x.Active == true),
                LocationInactive = await _locationService.GetCount(x => x.Active == false),
                LocCategoryActive = await _locationCategoryService.GetCount(x => x.Active == true),
                LocCategoryInactive = await _locationCategoryService.GetCount(x => x.Active == false),
                
            };

            return stats;
        }

        public async System.Threading.Tasks.Task LocationDeactivateAsync(int id, LocationOptions options)
        {
            var procedure = await _locationService.GetAsync(id);
            //var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, AuthorizationOperations.Delete);
            //if (result.Succeeded)
            //{
                procedure.Deactivate();

                var validationResult = await _locationService.UpdateAsync(procedure);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            //}
            //else
            //{
            //    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            //}

        }

        public async System.Threading.Tasks.Task LocationActivateAsync(int id, LocationOptions options)
        {
            var procedure = await _locationService.GetAsync(id);
            //var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, RegulatoryRequirementOperations.Delete);
            //if (result.Succeeded)
            //{
                procedure.Activate();

                var validationResult = await _locationService.UpdateAsync(procedure);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            //}
            //else
            //{
            //    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            //}

        }


        //active inactive locations and categories
        public async Task<List<Location>> GetLocActiveInactive(string option)
        {
            var rrList = new List<Location>();

            switch (option.ToLower().Trim())
            {
                case "locactive":
                    rrList = await _locationService.FindQuery(x => x.Active == true).Select(s => new Location
                    {
                        Id = s.Id,
                        LocNumber = s.LocNumber,
                        LocName = s.LocName,
                    }).ToListAsync();
                    break;
                case "locinactive":
                    rrList = await _locationService.FindQuery(x => x.Active == false).Select(s => new Location
                    {
                        Id = s.Id,
                        LocNumber = s.LocNumber,
                        LocName = s.LocName,
                    }).ToListAsync();
                    break;
            }

            return rrList.OrderBy(x=>x.LocNumber).ThenBy(x=>x.LocName).ToList();

        }

        public async Task<List<Location_Category>> GetCatActiveInactive(string option)
        {
            var rrList = new List<Location_Category>();

            switch (option.ToLower().Trim())
            {
                case "catactive":
                    rrList = await _locationCategoryService.FindQuery(x => x.Active == true).Select(s => new Location_Category
                    {
                        Id = s.Id,
                        LocCategoryTitle = s.LocCategoryTitle,
                    }).ToListAsync();
                    break;
                case "catinactive":
                    rrList = await _locationCategoryService.FindQuery(x => x.Active == false).Select(s => new Location_Category
                    {
                        Id = s.Id,
                        LocCategoryTitle = s.LocCategoryTitle,
                    }).ToListAsync();
                    break;
            }

            return rrList.OrderBy(x=>x.LocCategoryTitle).ToList();
        }



    }
}
