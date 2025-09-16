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
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Persistence;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model;
using QTD2.Infrastructure.Model.ProcedureReview;
using QTD2.Infrastructure.Model.ProcedureReview_Employee;
using IProcedureReviewDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedureReviewService;
using IEmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeService;
using IProcedureReviewEmployee_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedureReview_EmployeeService;

namespace QTD2.Application.Services.Shared
{
    public class ProcedureReviewService : IProcedureReviewService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<ProcedureReview> _localizer;
        private readonly IProcedureReviewDomainService _procedureReviewService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ProcedureReview _procedureReview;
        private readonly IEmployeeDomainService _emp_domainService;
        private readonly IProcedureReviewEmployee_LinkDomainService _procedureReviewEmployee_LinkDomainService;
        private readonly ProcedureReview_Employee _procedureReviewEmployeeLink;
        private readonly IProcedureReviewValidation _validation;
        private readonly IMainUnitOfWork _mainUow;
        
        public ProcedureReviewService(
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<ProcedureReview> localizer,
            IProcedureReviewDomainService procedureReviewService,
            UserManager<AppUser> userManager,
            IEmployeeDomainService emp_domainService,
            IProcedureReviewEmployee_LinkDomainService procedureReviewEmployee_LinkDomainService,
            IProcedureReviewValidation validation,
            IMainUnitOfWork mainUow)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _procedureReviewService = procedureReviewService;
            _userManager = userManager;
            _procedureReview = new ProcedureReview();
            _emp_domainService = emp_domainService;
            _procedureReviewEmployee_LinkDomainService = procedureReviewEmployee_LinkDomainService;
            _procedureReviewEmployeeLink = new ProcedureReview_Employee();
            _validation = validation;
            _mainUow = mainUow;
        }

        public async Task<Result<ProcedureReview>> CreateAsync(CreateProcedureReviewDto options)
        {
            var existingProcedureReview = await _mainUow.Repository<ProcedureReview>()
                .GetAsync(i => i.ProcedureReviewTitle == options.ProcedureReviewTitle);

            if (existingProcedureReview is not null)
            {
                return Result<ProcedureReview>.CreateError(_localizer["RecordAlreadyExists"].Value);
            }

            var newProcedureReview = new ProcedureReview(
                options.ProcedureId,
                options.ProcedureReviewTitle,
                options.StartDateTime,
                options.EndDateTime,
                options.ProcedureReviewInstructions,
                options.IsEmployeeShowResponses, 
                false,
                options.ProcedureReviewAcknowledgement,
                options.ExtensionType,    
                options.ExtensionAmount);
            
            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext!.User,
                newProcedureReview, AuthorizationOperations.Create);
            
            if (!authorizationResult.Succeeded)
            {
                return Result<ProcedureReview>.CreateError(_localizer["OperationNotAllowed"].Value);
            }

            var user = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity!.Name);
            newProcedureReview.CreatedBy = user.Id;
            newProcedureReview.CreatedDate = DateTime.UtcNow;
            
            var validationResult = _validation.Valid(newProcedureReview);
            
            if (!validationResult.IsValid)
            {
                return Result<ProcedureReview>.CreateError(string.Join(',', validationResult.Errors));
            }

            await _mainUow.Repository<ProcedureReview>().AddAsync(newProcedureReview);
            await _mainUow.SaveChangesAsync();
            return Result<ProcedureReview>.CreateSuccess(newProcedureReview);
        }

        public async Task<Result> UpdateAsync(int id, CreateProcedureReviewDto options)
        {
            var procedureReview = await _mainUow.Repository<ProcedureReview>()
                .GetAsync(i => i.Id == id, "Procedure");

            if (procedureReview is null)
            {
                return Result.CreateError(_localizer["RecordNotFound"].Value);
            }
            
            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext!.User, procedureReview, AuthorizationOperations.Update);

            if (!authorizationResult.Succeeded)
            {
                return Result.CreateError(_localizer["OperationNotAllowed"].Value);
            }
            
            var user = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity!.Name);
            procedureReview.ProcedureId = options.ProcedureId;
            procedureReview.ProcedureReviewTitle = options.ProcedureReviewTitle;
            procedureReview.ProcedureReviewInstructions = options.ProcedureReviewInstructions;
            procedureReview.StartDateTime = options.StartDateTime;
            procedureReview.EndDateTime = options.EndDateTime;
            procedureReview.IsEmployeeShowResponses = options.IsEmployeeShowResponses;
            procedureReview.ModifiedBy = user.Id;
            procedureReview.ModifiedDate = DateTime.UtcNow;
            procedureReview.ProcedureReviewAcknowledgement = options.ProcedureReviewAcknowledgement;
            procedureReview.ExtensionType = options.ExtensionType;
            procedureReview.ExtensionAmount = options.ExtensionAmount;

            var validationResult = _validation.Valid(procedureReview);
            if (!validationResult.IsValid)
            {
                return Result.CreateError(string.Join(',', validationResult.Errors));
            }

            _mainUow.Repository<ProcedureReview>().Update(procedureReview);
            await _mainUow.SaveChangesAsync();
            return Result.CreateSuccess();
        }
        public async Task<Result<ProcedureReview>> GetAsync(int id)
        {
            var procedureReview = await _mainUow.Repository<ProcedureReview>()
                .GetAsync(i => i.Id == id, "Procedure");
            
            if (procedureReview is null)
            {
                return Result<ProcedureReview>.CreateError(_localizer["RecordNotFound"].Value);
            }
            
            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext!.User, procedureReview, AuthorizationOperations.Read);

            if (!authorizationResult.Succeeded)
            {
                return Result<ProcedureReview>.CreateError(_localizer["OperationNotAllowed"].Value);
            }

            return Result<ProcedureReview>.CreateSuccess(procedureReview);
        }

        public async Task<List<ProcedureReviewOverviewVM>> GetListAsync()
        {
            var procedureReviews = await _procedureReviewService.GetAllWithProcedureAndProcedureEmployee();
            var procedureReviewVM = new List<ProcedureReviewOverviewVM>();
            foreach(var pr in procedureReviews)
            {
                var isStarted = pr.ProcedureReview_Employee.Any(x => x.IsStarted);
                var procedureReview = new ProcedureReviewOverviewVM(pr.Id,pr.StartDateTime,pr.EndDateTime, isStarted,pr.IsPublished,pr.Active,pr.Procedure.Number,pr.Procedure.Title,pr.ProcedureReviewTitle,pr.Procedure.Procedure_IssuingAuthority.Title,pr.Procedure.IssuingAuthorityId);
                procedureReviewVM.Add(procedureReview);
            }
            return procedureReviewVM;
        }


        public async System.Threading.Tasks.Task DeleteAsync(ProcedureReviewOptions options)
        {
            foreach (var procrev_id in options.procedurereviewIds)
            {
                var procedureReview = await _procedureReviewService.GetAsync(procrev_id);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext!.User, procedureReview, ProcedureReviewOperations.Delete);
                if (result.Succeeded)
                {
                    procedureReview.Delete();
                    var validationResult = await _procedureReviewService.UpdateAsync(procedureReview);
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
        }

        public async System.Threading.Tasks.Task DeactivateAsync(ProcedureReviewOptions options)
        {
            foreach (var procrev_id in options.procedurereviewIds)
            {
                var procedureReview = await _procedureReviewService.GetAsync(procrev_id);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedureReview, ProcedureReviewOperations.Delete);
                if (result.Succeeded)
                {
                    procedureReview.Deactivate();

                    var validationResult = await _procedureReviewService.UpdateAsync(procedureReview);
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
        }

        public async System.Threading.Tasks.Task ActivateAsync(ProcedureReviewOptions options)
        {
            foreach (var procrev_id in options.procedurereviewIds)
            {
                var procedureReview = await _procedureReviewService.GetAsync(procrev_id);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedureReview, ProcedureReviewOperations.Delete);
                if (result.Succeeded)
                {
                    procedureReview.Activate();

                    var validationResult = await _procedureReviewService.UpdateAsync(procedureReview);
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
        }

        public async Task<ProcedureReview> LinkEmployee(int procedureReviewId, ProcedureReview_EmployeeCreateOptions options)
        {

            var procedureReview = await _procedureReviewService.GetWithIncludeAsync(procedureReviewId, new string[] { nameof(_procedureReview.ProcedureReview_Employee) });
            foreach (var id in options.employeeIds)
            {
                var employee = await _emp_domainService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();

                var procedureReviewResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedureReview, ProcedureReviewOperations.Update);
                var empResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, employee, EmployeeOperations.Read);
                if (procedureReviewResult.Succeeded && empResult.Succeeded)
                {
                    procedureReview.LinkEmployee(employee);
                    var validationResult = await _procedureReviewService.UpdateAsync(procedureReview);
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

            return procedureReview;
        }

        public async System.Threading.Tasks.Task UnlinkEmployee(int procedureReviewId, int[] empIDs)
        {
            var procedureReview = await _procedureReviewService.GetWithIncludeAsync(procedureReviewId, new string[] { nameof(_procedureReview.ProcedureReview_Employee) });
            foreach (var id in empIDs)
            {
                var employee = await _emp_domainService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();

                var procedureReviewResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedureReview, ProcedureReviewOperations.Update);
                var employeeResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, employee, EmployeeOperations.Read);
                if (procedureReviewResult.Succeeded && employeeResult.Succeeded)
                {
                    procedureReview.UnlinkEmployee(employee);
                    var validationResult = await _procedureReviewService.UpdateAsync(procedureReview);
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
        }

        public async Task<List<ProcedureReview>> GetProcedureReviewEmployeesLinkedTo(int id)
        {
            var data = await _procedureReviewEmployee_LinkDomainService.AllQueryWithInclude(new string[] { nameof(_procedureReviewEmployeeLink.ProcedureReview) }).Where(x => x.EmployeeId == id).Select(x => x.ProcedureReview).ToListAsync();
            return data;
        }
        
        //public async Task<List<EmployeeWithCountOptions>> GetLinkedEmployees(int procedureReviewId)
        //{
        //    var links = await _procedureReviewEmployee_LinkDomainService.FindQueryWithIncludeAsync(x => x.ProcedureReviewId == procedureReviewId, new string[] { nameof(_procedureReview.ProcedureReview_Employee) }).ToListAsync();
        //    List<Domain.Entities.Core.Employee> empList = new List<Domain.Entities.Core.Employee>();
        //    foreach (var link in links)
        //    {
        //        var data = await _emp_domainService.FindQueryWithIncludeAsync(x => x.Id == link.EmployeeId, new string[] { nameof(_emp.EmployeePositions), nameof(_emp.EmployeeOrganizations) }).FirstOrDefaultAsync();
        //        empList.Add(data);
        //    }

        //    List<EmployeeWithCountOptions> empWithCount = new List<EmployeeWithCountOptions>();
        //    foreach (var emp in empList)
        //    {
        //        var data = await _procedureReviewEmployee_LinkDomainService.GetCount(x => x.EmployeeId == emp.Id);
        //        var pos = emp.EmployeePositions.OrderBy(x => x.Id).LastOrDefault();
        //        if (pos != null)
        //        {
        //            var posTitle = (await _posService.FindQuery(x => x.Id == pos.PositionId).FirstOrDefaultAsync())?.PositionTitle ?? "";
        //            empWithCount.Add(new EmployeeWithCountOptions(emp.EmployeeNumber, emp.Person.FirstName + ' ' + emp.Person.LastName, emp.Id, data, emp.Active, pos.Trainee, pos.StartDate.ToString(), pos.QualificationDate.ToString(), posTitle, emp.Person.Username));
        //        }
        //        else
        //        {
        //            empWithCount.Add(new EmployeeWithCountOptions(emp.EmployeeNumber, emp.Person.FirstName + ' ' + emp.Person.LastName, emp.Id, data, emp.Active, false, "No Start Date", "No Qualification Date", "No Position Title", emp.Person.Username));
        //        }
        //    }

        //    return empWithCount;
        //}

        public async Task<List<EmployeesLinkedToProcedureReview>> GetLinkedEmployees(int id)
        {
            var links = await _procedureReviewEmployee_LinkDomainService.FindQueryWithIncludeAsync(x => x.ProcedureReviewId == id, new string[] { "Employee.Person", "Employee.EmployeePositions.Position", "Employee.EmployeeOrganizations.Organization" }).ToListAsync();
          
            List<EmployeesLinkedToProcedureReview> empWithCount = new List<EmployeesLinkedToProcedureReview>();
            foreach (var link in links)
            {
                var status = string.Empty;
                if(link.IsCompleted == true && link.IsStarted == true)
                {
                    status = "Completed";
                }
                else if(link.IsCompleted == false && link.IsStarted == false)
                {
                    status = "Not Started";
                }
                else if (link.IsCompleted == false && link.IsStarted == true)
                {
                    status = "In Progress";
                }

                empWithCount.Add(new EmployeesLinkedToProcedureReview(link.CompletedDate,link.Id,
                link.Employee.Person.FirstName + " " + link.Employee.Person.LastName,
                link.Employee.Person.Username,
                string.Join(Environment.NewLine, link.Employee.EmployeePositions.Where(x => x.Active == true).GroupBy(x => x.Position.Id).Select(x => x.First().Position.PositionTitle)), // Done this way because if you ever have two Positions with the same PositionTitle, then we should show that and make the user aware that two Positions are applied to the user, possibly redundantly. We don't hide the data with a DISTINCT PositionTitle here.
                link.Employee.EmployeeOrganizations.Select(x => x.Organization.Name).FirstOrDefault(),
                link.Employee.Id, link.Employee.Person.Image,
                link.ProcedureReviewResponse==true ? "true" : "false", link.Comments,status));

            }
           

            return empWithCount;
        }

        public async Task<ProcedureReview> PublishProcedureReview(int id)
        {
            var procedureReview = (await GetAsync(id)).Data;
            procedureReview.Publish();

            procedureReview.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            procedureReview.ModifiedDate = DateTime.Now;
            var validationResult = await _procedureReviewService.UpdateAsync(procedureReview);

            if (validationResult.IsValid)
            {
                return procedureReview;
            }
            else
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
        }

        public async Task<ProcedureReviewVM> GetStatsCount()
        {

            var stats = new ProcedureReviewVM()
            {
                ProcedureReviewPublished = await _procedureReviewService.GetCount(x => x.IsPublished == true),
                ProcedureReviewNumberofEmployeesPending = await _procedureReviewEmployee_LinkDomainService.GetCount(x=>x.IsCompleted == false || x.IsStarted == false),
                //ProcedureReviewNumberofEmployeesPending = 0,
                ProcedureReviewInDrafts = await _procedureReviewService.GetCount(x => x.IsPublished == false),

            };

            return stats;
        }
        public async Task<ProcedureReview_Employee> UpdateResponseAsync(int empId, ProcedureReviewResponseCreateOptions options)
        {
            var procedureReviewEmployee = await _procedureReviewEmployee_LinkDomainService.FindQuery(x => x.EmployeeId == empId && x.ProcedureReviewId == options.ProcedureReviewId && !x.Deleted).FirstOrDefaultAsync();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedureReviewEmployee, ProcedureReview_EmployeeOperations.Update);
            if (result.Succeeded)
            {
                DateTime completedDate = procedureReviewEmployee.CompletedDate ?? DateTime.Now;
                procedureReviewEmployee.Complete(completedDate);
                procedureReviewEmployee.ProcedureReviewResponse = options.Response== "true" ? true : false;
                procedureReviewEmployee.IsStarted = true;
                procedureReviewEmployee.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                procedureReviewEmployee.ModifiedDate = DateTime.Now;
                var validationResult = await _procedureReviewEmployee_LinkDomainService.UpdateAsync(procedureReviewEmployee);

                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }

            }
            return procedureReviewEmployee;
        }
        public async Task<ProcedureReview_Employee> UpdateCommentsAsync(int empId, ProcedureReviewResponseCreateOptions options)
        {
            var procedureReviewEmployee = await _procedureReviewEmployee_LinkDomainService.FindQuery(x => x.EmployeeId == empId && x.ProcedureReviewId == options.ProcedureReviewId && !x.Deleted).FirstOrDefaultAsync();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedureReviewEmployee, ProcedureReview_EmployeeOperations.Update);
            if (result.Succeeded)
            {
                DateTime completedDate = procedureReviewEmployee.CompletedDate ?? DateTime.Now;
                procedureReviewEmployee.Complete(completedDate);
                procedureReviewEmployee.Comments = options.Comments;
                procedureReviewEmployee.IsStarted = true;
                procedureReviewEmployee.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                procedureReviewEmployee.ModifiedDate = DateTime.Now;
                var validationResult = await _procedureReviewEmployee_LinkDomainService.UpdateAsync(procedureReviewEmployee);

                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }

            }
            return procedureReviewEmployee;
        }

        public async Task<List<ProcedureReview>> GetDraftsProcedureReviews()
        {

            var listProcedurereview = await _procedureReviewService.FindQueryWithIncludeAsync(x => x.IsPublished == false && x.Active == true, new string[] { "Procedure.Procedure_IssuingAuthority" }).ToListAsync();
            return listProcedurereview;
        }
        public async Task<List<ProcedureReview_Employee>> GetEmployeesPendingProcedureReviews()
        {

            var listProcedurereviewEmployee = await _procedureReviewEmployee_LinkDomainService.FindQueryWithIncludeAsync(x => x.IsCompleted == false || x.IsStarted == false, new string[] { "ProcedureReview.Procedure", "Employee.Person" }).Select(s => new ProcedureReview_Employee { 
                Id = s.Id,
                ProcedureReviewId = s.ProcedureReviewId,
                EmployeeId = s.EmployeeId,
                Employee = s.Employee != null ? new Employee { 
                    Id = s.Employee.Id,
                    PersonId = s.Employee.PersonId,
                    Person = s.Employee.Person != null ? new Person { Id = s.Employee.Person.Id,
                        Active = s.Employee.Person.Active,
                        FirstName = s.Employee.Person.FirstName,
                        LastName = s.Employee.Person.LastName,
                        MiddleName = s.Employee.Person.LastName,
                        Image = s.Employee.Person.Image
                    }:null
                }:null,
                ProcedureReview = s.ProcedureReview != null ? new ProcedureReview
                {
                    Id = s.ProcedureReview.Id,
                    ProcedureReviewTitle = s.ProcedureReview.ProcedureReviewTitle,
                    Active = s.ProcedureReview.Active,
                    Procedure = s.ProcedureReview.Procedure != null ? new Procedure { 
                        Id = s.ProcedureReview.Procedure.Id,
                        Active = s.ProcedureReview.Procedure.Active,
                        Title = s.ProcedureReview.Procedure.Title,
                        Number = s.ProcedureReview.Procedure.Number
                    }:null,
                }:null
            }).ToListAsync();
            return listProcedurereviewEmployee;
        }


        //published procedure reviews
        public async Task<List<ProcedureReview>> GetPublishedList()
        {


            var ProcedureReviewPublished = await _procedureReviewService.FindQueryWithIncludeAsync(x => x.IsPublished == true && x.Active == true, new string[] {"Procedure"}).Select(s => new ProcedureReview { Id = s.Id, ProcedureReviewTitle = s.ProcedureReviewTitle, Active = s.Active, Procedure = new Procedure { Id = s.Procedure.Id, Active = s.Procedure.Active, Title = s.Procedure.Title, Number = s.Procedure.Number } }).ToListAsync();
            return ProcedureReviewPublished.OrderBy(x=>x.ProcedureReviewTitle).ToList();
        }
    }
}
