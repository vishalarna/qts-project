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
using QTD2.Infrastructure.Model.Location_Category;
using ILocation_CategoryDomainService = QTD2.Domain.Interfaces.Service.Core.ILocation_CategoryService;
using ILocationDomainService = QTD2.Domain.Interfaces.Service.Core.ILocation_Service;
using QTD2.Domain.Exceptions;

namespace QTD2.Application.Services.Shared
{
    public class Location_CategoryService : ILocation_CategoryService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<Location_CategoryService> _localizer;
        private readonly ILocation_CategoryDomainService _location_CategoryService;
        private readonly ILocationDomainService _locationService;
        private readonly UserManager<AppUser> _userManager;
        private readonly Location_Category _location_category;
        private readonly ILocationService _locationService1;


        public Location_CategoryService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<Location_CategoryService> localizer, ILocation_CategoryDomainService location_CategoryService, UserManager<AppUser> userManager, ILocationDomainService locationService, ILocationService locationService1)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _location_CategoryService = location_CategoryService;
            _userManager = userManager;
            _locationService = locationService;
            _location_category = new Location_Category();
            _locationService1 = locationService1;
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            //var obj = await GetAsync(id);

            //obj.Activate();

            //var validationResult = await _location_CategoryService.UpdateAsync(obj);
            //if (!validationResult.IsValid)
            //{
            //    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            //}

            var proc_issu = await _location_CategoryService.GetWithIncludeAsync(id, new string[] { nameof(_location_category.Locations) });
            List<Domain.Entities.Core.Location> procList = new List<Domain.Entities.Core.Location>();
            procList.AddRange(proc_issu.Locations);
            if (proc_issu != null)
            {
                proc_issu.Activate();
                await _location_CategoryService.UpdateAsync(proc_issu);

                foreach (var proc in procList)
                {
                    var options = new LocationOptions();
                    options.LocId = proc.Id;
                    options.EffectiveDate = proc_issu.EffectiveDate;
                    options.ChangeNotes = "Inactive Due to Category Inactive";
                    await _locationService1.LocationActivateAsync(proc.Id, options);

                }
            }
            else
            {
                throw new QTDServerException(_localizer["Regulation_IssuingAuthorityNotFound"]);
            }
        }

        public async Task<Location_Category> CreateAsync(Location_CategoryCreateOptions options)
        {
            var obj = (await _location_CategoryService.FindAsync(x => x.LocCategoryTitle == options.Title)).FirstOrDefault();
            if (obj == null)
            {
                obj = new Location_Category(options.Title, options.Description, options.website,options.EffectiveDate);
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["Location Title Already Exists"].Value);
            }

            obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            obj.CreatedDate = DateTime.Now;
            var validationResult = await _location_CategoryService.AddAsync(obj);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                return obj;
            }
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var obj = await GetAsync(id);

            obj.Delete();

            var validationResult = await _location_CategoryService.UpdateAsync(obj);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }

        }

        public async Task<List<Location_Category>> GetAsync()
        {
            var obj_list = await _location_CategoryService.AllAsync();

            return obj_list?.ToList();
        }

        public async Task<Location_Category> GetAsync(int id)
        {
            var obj = await _location_CategoryService.GetWithIncludeAsync(id, new string[] { nameof(_location_category.Locations) });
            if (obj != null)
            {

                return obj;
            }
            else
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }
        }

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            //var obj = await GetAsync(id);
            //obj.Deactivate();

            //var validationResult = await _location_CategoryService.UpdateAsync(obj);
            //if (!validationResult.IsValid)
            //{
            //    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            //}

            var proc_issu = await _location_CategoryService.GetWithIncludeAsync(id, new string[] { nameof(_location_category.Locations) });
            List<Domain.Entities.Core.Location> procList = new List<Domain.Entities.Core.Location>();
            procList.AddRange(proc_issu.Locations);
            if (proc_issu != null)
            {
                proc_issu.Deactivate();
                await _location_CategoryService.UpdateAsync(proc_issu);

                foreach (var proc in procList)
                {
                    var options = new LocationOptions();
                    options.LocId = proc.Id;
                    options.EffectiveDate = proc_issu.EffectiveDate;
                    options.ChangeNotes = "Inactive Due to Category Inactive";
                    await _locationService1.LocationDeactivateAsync(proc.Id, options);

                }
            }
            else
            {
                throw new QTDServerException(_localizer["Regulation_IssuingAuthorityNotFound"]);
            }
        }

        public async Task<Location_Category> UpdateAsync(int id, Location_CategoryCreateOptions options)
        {
            var obj = await GetAsync(id);

            obj.LocCategoryTitle = options.Title;
            obj.LocCategoryDesc = options.Description;
            obj.LocCategoryWebsite = options.website;
            obj.EffectiveDate = options.EffectiveDate;
            

            obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            obj.ModifiedDate = DateTime.Now;

            var validationResult = await _location_CategoryService.UpdateAsync(obj);
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
            var result = await _location_CategoryService.AllQueryWitDeletedCount();
            return result;
        }

        public async Task<List<LocationCategoryCompactOptions>> GetLocCategoryWithLoc()
        {
            var shCategory = await _location_CategoryService.AllQuery().OrderBy(x=>x.LocCategoryTitle).ToListAsync();
            var sh = _locationService.AllQuery();
            var data = shCategory.GroupJoin(sh, x => x.Id, x => x.LocCategoryID, (shCategory, sh) => new { shCategory, Category = sh.Select(x => new LocationCompactOptions(x.Id, x.LocNumber, x.LocCategoryID, x.LocName, x.Active)).ToList() }).ToList();
            List<LocationCategoryCompactOptions> shCatCompact = new List<LocationCategoryCompactOptions>();
            foreach (var item in data)
            {
                var shCompact = new LocationCategoryCompactOptions();
                shCompact.Location_Category = item.shCategory;
                shCompact.LocationCompactOptions.AddRange(item.Category);
                shCatCompact.Add(shCompact);
            }

            return shCatCompact;
        }


    }
}
