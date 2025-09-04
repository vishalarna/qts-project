using System;
using System.Collections.Generic;
using System.Linq;
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
using QTD2.Infrastructure.Model.Organization;
using IEmployeeOrganizationDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeOrganizationService;
using IEmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeService;
using IPersonDomainService = QTD2.Domain.Interfaces.Service.Core.IPersonService;
using DocumentFormat.OpenXml.ExtendedProperties;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.HttpClients;

namespace QTD2.Application.Services.Shared
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<OrganizationService> _localizer;
        private readonly Domain.Interfaces.Service.Core.IOrganizationService _organizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly Organization _organization;
        private readonly IEmployeeOrganizationDomainService _emp_orgService;
        private readonly IEmployeeDomainService _empService;
        private readonly IPersonDomainService _personService;
        

        public OrganizationService(
            Domain.Interfaces.Service.Core.IOrganizationService organizationService,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<OrganizationService> localizer,
            UserManager<AppUser> userManager,
            IEmployeeOrganizationDomainService emp_orgService,
            IEmployeeDomainService empService,
            IPersonDomainService personService)
        {
            _organizationService = organizationService;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _organization = new Organization();
            _emp_orgService = emp_orgService;
            _empService = empService;
            _personService = personService;
        }

        public async Task<List<Organization>> GetAsync()
        {
            var orgnizations = await _organizationService.AllQuery().ToListAsync();
            for (int i = 0; i < orgnizations.Count; i++)
            {
                orgnizations[i].EmployeeOrganizations = await _emp_orgService.FindQuery(x => x.OrganizationId == orgnizations[i].Id).ToListAsync();
                for (int j = 0; j < orgnizations[i].EmployeeOrganizations.ToList().Count; j++)
                {
                    orgnizations[i].EmployeeOrganizations.ToList()[j].Employee = await _empService.FindQuery(x => x.Id == orgnizations[i].EmployeeOrganizations.ToList()[j].EmployeeId).Select(s => new Employee
                    {
                        Id = s.Id,
                        Active = s.Active,
                        PersonId = s.PersonId,
                        TQEqulator = s.TQEqulator
                    }).FirstOrDefaultAsync();
                    if (orgnizations[i].EmployeeOrganizations.ToList()[j].Employee != null)
                    {
                        orgnizations[i].EmployeeOrganizations.ToList()[j].Employee.Person = await _personService.FindQuery(x => x.Id == orgnizations[i].EmployeeOrganizations.ToList()[j].Employee.PersonId).OrderBy(x => x.LastName).Select(s => new Person
                        {
                            Active = s.Active,
                            Id = s.Id,
                            FirstName = s.FirstName,
                            LastName = s.LastName,
                            MiddleName = s.MiddleName,
                            Username = s.Username
                        }).FirstOrDefaultAsync();
                    }
                }
            }
            orgnizations = orgnizations.Where(org => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, org, OrganizationOperations.Read).Result.Succeeded).ToList();
            return orgnizations.OrderBy(x => x.Name).ToList();
        }

        public async Task<List<OrganizationIdAndNameVM>> GetSimplifiedDataAsync()
        {
            var orgs = await _organizationService.FindQueryWithIncludeAsync(x => x.Active,new string[] { "EmployeeOrganizations.Employee.Person" }).Select(s => new OrganizationIdAndNameVM
            {
                Id = s.Id,
                Name = s.Name,
                EmployeeOrganizations = s.EmployeeOrganizations.Select(org => new Infrastructure.Model.EmployeeOrganization.EmployeeOrgIdsAndEMPNameVM
                {
                    EmployeeId = org.EmployeeId,
                    Id = org.Id,
                    OrganizationId = org.OrganizationId,
                    EMPFirstName =  org.Employee == null || org.Employee.Person == null ? "":org.Employee.Person.FirstName,
                    EMPLastName = org.Employee == null || org.Employee.Person == null ? "" : org.Employee.Person.LastName,
                    IsManager = org.IsManager,
                }).OrderBy(o => o.EMPFirstName).ToList()
            }).ToListAsync();

            return orgs;
        }

        public async Task<List<Organization>> GetAllOrderByAsync(string orderBy)
        {
            var orgnizations = await _organizationService.AllQuery().ToListAsync();

            switch (orderBy.Trim().ToLower())
            {
                case "name":
                    orgnizations = orgnizations.OrderBy(o => o.Name).ToList();
                    break;
            }

            return orgnizations;
        }

        public async Task<Organization> GetAsync(int id)
        {
            var organization = await _organizationService.FindQueryWithIncludeAsync(x => x.Id == id, new string[] { "EmployeeOrganizations.Employee.Person" }).FirstOrDefaultAsync();
            if (organization != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, organization, OrganizationOperations.Read);
                if (result.Succeeded)
                {
                    return organization;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return organization;
        }

        public async Task<Organization> CreateAsync(OrganizationCreateOptions options)
        {
            var organization = new Organization(options.Name, options.PublicOrganization);
            var organizationExists = (await _organizationService.FindAsync(r => r.Name == options.Name && r.PublicOrganization == options.PublicOrganization)).FirstOrDefault() != null;
            if (organizationExists)
            {
                throw new BadHttpRequestException(message: _localizer["OrganizationExists"]);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, organization, OrganizationOperations.Create);

            if (result.Succeeded)
            {
                organization.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                organization.CreatedDate = DateTime.Now;
                var validationResult = await _organizationService.AddAsync(organization);
                if (validationResult.IsValid)
                {
                    return organization;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<Organization> UpdateAsync(int id, OrganizationUpdateOptions options)
        {
            var organization = await GetAsync(id);
            //if(organization != null && organization.Name.ToLower() == options.Name.ToLower())
            //{
            //    throw new BadHttpRequestException(message: _localizer["OrganizationExists"]);
            //}
            //else
            //{
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, organization, OrganizationOperations.Update);

            if (result.Succeeded)
            {
                // Todo Update Logic
                organization.Name = options.Name;
                organization.PublicOrganization = options.PublicOrganization;
                organization.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                organization.ModifiedDate = DateTime.Now;
                var validationResult = await _organizationService.UpdateAsync(organization);
                if (validationResult.IsValid)
                {
                    return organization;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
            //}


        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var organization = await GetAsync(id);

            if (organization == null)
            {
                throw new QTDServerException(_localizer["OrganizationNotFound"]);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, organization, OrganizationOperations.Delete);

            if (result.Succeeded)
            {
                // Todo Delete Logic
                organization.Delete();
                var validationResult = await _organizationService.UpdateAsync(organization);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<List<Organization>> GetPublicOrganizationAsync()
        {
            return await _organizationService.GetPublicOrganizationAsync();
        }
    }
}
