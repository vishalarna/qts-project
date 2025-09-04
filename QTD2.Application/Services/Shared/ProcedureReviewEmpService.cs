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
using IProcedureReviewDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedureReviewService;
using IEmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeService;
using IPersonDomainService = QTD2.Domain.Interfaces.Service.Core.IPersonService;
using IProcedureReviewEmployeeLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedureReview_EmployeeService;
using IProcedureDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedureService;
using QTD2.Infrastructure.Model.ProcedureReviewEmp;
using QTD2.Infrastructure.Model.ProcedureReview;
using QTD2.Domain.Exceptions;

namespace QTD2.Application.Services.Shared
{
    public class ProcedureReviewEmpService : IProcedureReviewEmpService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<ProcedureReviewEmpService> _localizer;
        private readonly IProcedureReviewDomainService _procedureReviewService;
        private readonly IProcedureDomainService _procedureDomainService;
        private readonly IEmployeeDomainService _empService;
        private readonly IPersonDomainService _personService;
        private readonly IProcedureReviewEmployeeLinkDomainService _procedureReviewEmployeeLinkDomainService;
        private readonly UserManager<AppUser> _userManager;

        public ProcedureReviewEmpService(
            IHttpContextAccessor httpContextAccessor, 
            IAuthorizationService authorizationService, 
            IStringLocalizer<ProcedureReviewEmpService> localizer, 
            IProcedureReviewDomainService procedureReviewService, 
            UserManager<AppUser> userManager, 
            IEmployeeDomainService empService, 
            IPersonDomainService personService, 
            IProcedureReviewEmployeeLinkDomainService procedureReviewEmployeeLinkDomainService,
            IProcedureDomainService procedureDomainService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _procedureReviewService = procedureReviewService;
            _userManager = userManager;
            _empService = empService;
            _personService = personService;
            _procedureReviewEmployeeLinkDomainService = procedureReviewEmployeeLinkDomainService;
            _procedureDomainService = procedureDomainService;
        }
        public async Task<List<ProcedureReviewEmpModel>> GetEmpProcedureReviewsAsync()
        {

            var list = new List<ProcedureReviewEmpModel>();

            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

            var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();

            if (person != null)
            {
                var employee = await _empService.FindQueryWithIncludeAsync(x => x.PersonId == person.Id, new string[] { "Person" }).FirstOrDefaultAsync();

                var procedureReviewsEmployees = await _procedureReviewEmployeeLinkDomainService.GetCurrentProcedureReviewsForEmployee(employee.Id);
                var procedureReviewIds = procedureReviewsEmployees.Select(r => r.ProcedureReviewId).Distinct().ToList();
                var procedureReviews = await _procedureReviewService.GetProcedureReviewsByIdAsync(procedureReviewIds);

                foreach (var procedureReviewEmployee in procedureReviewsEmployees)
                {
                    var status = "";
                    DateTime? procedureCompletionDate = null;
                    if (procedureReviewEmployee.IsStarted == false)
                    {
                        status = "Pending";
                    }
                    else if (procedureReviewEmployee.IsStarted == true && procedureReviewEmployee.IsCompleted == false)
                    {
                        status = "In Progress";
                    }
                    else if (procedureReviewEmployee.IsStarted == true && procedureReviewEmployee.IsCompleted == true)
                    {
                        status = "Completed";
                        procedureCompletionDate = procedureReviewEmployee.CompletedDate;
                    }

                    var procedureReview = procedureReviews.Where(r => r.Id == procedureReviewEmployee.ProcedureReviewId).First();

                    list.Add(new ProcedureReviewEmpModel()
                    {
                        ProcedureReviewId = procedureReviewEmployee.ProcedureReviewId,
                        ProcedureName = procedureReview.Procedure.Title,
                        ProcedureNumber = procedureReview.Procedure.Number,
                        ProcedureReviewTitle = procedureReview.ProcedureReviewTitle,
                        ProcedureReviewDueDate = procedureReview.EndDateTime,
                        Status = status,
                        File = procedureReview.Procedure.ProceduresFileUpload,
                        FileName = procedureReview.Procedure.FileName,
                        HyperLink = procedureReview.Procedure.Hyperlink,
                        ProcedureId = procedureReview.ProcedureId,
                        StartDateTime = procedureReview.StartDateTime,
                        EndDateTime = procedureReview.EndDateTime,
                        ProcedureReviewCompletionDate = procedureCompletionDate,
                        Instructions = procedureReview.ProcedureReviewInstructions,
                        IsEmployeeShowResponses = procedureReview.IsEmployeeShowResponses
                    });
                }
            }
            return list;
        }

        public async Task<ProcedureReviewEmpModel> GetEmpProcedureReviewDataByIdAsync(int procedureReviewId)
        {
            var procedureReviewModel = new ProcedureReviewEmpModel();

            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

            var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();

            if (person != null)
            {
                var employee = await _empService.FindQueryWithIncludeAsync(x => x.PersonId == person.Id, new string[] { "Person" }).FirstOrDefaultAsync();

                var pr_emp = (await _procedureReviewEmployeeLinkDomainService.FindWithIncludeAsync(x => x.EmployeeId == employee.Id && x.ProcedureReviewId == procedureReviewId, new string[] { "ProcedureReview.Procedure", "Employee" })).FirstOrDefault();
                pr_emp.IsStarted = true;
                await _procedureReviewEmployeeLinkDomainService.UpdateAsync(pr_emp);

                var pr_emp_updated = (await _procedureReviewEmployeeLinkDomainService.FindWithIncludeAsync(x => x.EmployeeId == employee.Id && x.ProcedureReviewId == procedureReviewId, new string[] { "ProcedureReview.Procedure", "Employee" })).FirstOrDefault();
                var status = "";
                if (pr_emp_updated.IsStarted == false)
                {
                    status = "Pending";
                }
                else if (pr_emp_updated.IsStarted == true && pr_emp_updated.IsCompleted == false)
                {
                    status = "In Progress";
                }
                else if (pr_emp_updated.IsStarted == true && pr_emp_updated.IsCompleted == true)
                {
                    status = "Completed";
                }

                procedureReviewModel = new ProcedureReviewEmpModel()
                {
                    ProcedureReviewId = pr_emp_updated.ProcedureReviewId,
                    ProcedureName = pr_emp_updated.ProcedureReview.Procedure.Title,
                    ProcedureNumber = pr_emp_updated.ProcedureReview.Procedure.Number,
                    ProcedureReviewTitle = pr_emp_updated.ProcedureReview.ProcedureReviewTitle,
                    ProcedureReviewDueDate = pr_emp_updated.ProcedureReview.EndDateTime,
                    Status = status,
                    Instructions = pr_emp_updated.ProcedureReview.ProcedureReviewInstructions,
                    Response = pr_emp_updated.ProcedureReviewResponse != null ? pr_emp_updated.ProcedureReviewResponse == true ? "true" : "false" : null,
                    File = pr_emp_updated.ProcedureReview.Procedure.ProceduresFileUpload,
                    FileName = pr_emp_updated.ProcedureReview.Procedure.FileName,
                    HyperLink = pr_emp_updated.ProcedureReview.Procedure.Hyperlink,
                    ProcedureId = pr_emp_updated.ProcedureReview.ProcedureId,
                    IsEmployeeShowResponses = pr_emp_updated.ProcedureReview.IsEmployeeShowResponses,
                    Comments = pr_emp_updated.Comments,
                    Acknowledgement = pr_emp_updated.ProcedureReview.ProcedureReviewAcknowledgement

                };

            }
            return procedureReviewModel;
        }

        public async Task<ProcedureReview_Employee> SubmitProcedureReviewAsync(SubmitProcedureReviewDto submitOptions)
        {
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

            var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();

            if (person != null)
            {
                var employee = await _empService.FindQueryWithIncludeAsync(x => x.PersonId == person.Id, new string[] { "Person" }).FirstOrDefaultAsync();

                var pr_emp = (await _procedureReviewEmployeeLinkDomainService.FindWithIncludeAsync(x => x.EmployeeId == employee.Id && x.ProcedureReviewId == submitOptions.ProcedureReviewId, new string[] { "ProcedureReview", "Employee" })).FirstOrDefault();
                pr_emp.Complete(DateTime.UtcNow);
                pr_emp.ProcedureReviewResponse = submitOptions.Response != null ? (submitOptions.Response == "true" ? true : false) : null;
                pr_emp.Comments = submitOptions.Comments;
                await _procedureReviewEmployeeLinkDomainService.UpdateAsync(pr_emp);
                return pr_emp;
            }
            return new ProcedureReview_Employee();
        }
        public async Task<ProcedureReview_Employee> CancelProcedureReviewAsync(int procedureReviewId, string response, string comments)
        {
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

            var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();

            if (person != null)
            {
                var employee = await _empService.FindQueryWithIncludeAsync(x => x.PersonId == person.Id, new string[] { "Person" }).FirstOrDefaultAsync();

                var pr_emp = (await _procedureReviewEmployeeLinkDomainService.FindWithIncludeAsync(x => x.EmployeeId == employee.Id && x.ProcedureReviewId == procedureReviewId, new string[] { "ProcedureReview", "Employee" })).FirstOrDefault();
                pr_emp.ProcedureReviewResponse = response != null ? (response == "true" ? true : false) : null;
                pr_emp.Comments = comments;
                await _procedureReviewEmployeeLinkDomainService.UpdateAsync(pr_emp);
                return pr_emp;
            }
            return new ProcedureReview_Employee();
        }

        //New Application Services
        public async Task<List<ProcedureReviewEmpModel>> GetEmpProcedureReviewsByIdAsync(int employeeId)
        {
            var list = new List<ProcedureReviewEmpModel>();
            if (employeeId != null)
            {
                var obj_list = await _procedureReviewEmployeeLinkDomainService.GetEmpProcedureReviewByIdAsync(employeeId);
                if (obj_list != null && obj_list.Count() > 0)
                {
                    foreach (var obj in obj_list)
                    {
                        var status = "";
                        DateTime? procedureCompletionDate = null;
                        if (obj.IsStarted == false)
                        {
                            status = "Pending";
                        }
                        else if (obj.IsStarted == true && obj.IsCompleted == false)
                        {
                            status = "In Progress";
                        }
                        else if (obj.IsStarted == true && obj.IsCompleted == true)
                        {
                            status = "Completed";
                            procedureCompletionDate = obj.CompletedDate;
                        }
                        var procedureReviewEmpModel = new ProcedureReviewEmpModel(obj.ProcedureReviewId, obj.ProcedureReview.Procedure.Number, obj.ProcedureReview.Procedure.Title,
                            obj.ProcedureReview.IsEmployeeShowResponses, obj.ProcedureReview.ProcedureReviewTitle,
                            obj.ProcedureReview.EndDateTime, status, obj.ProcedureReview.ProcedureReviewInstructions,
                            obj.ProcedureReview.Procedure.ProceduresFileUpload,
                            obj.ProcedureReview.Procedure.Hyperlink, obj.ProcedureReview.Procedure.FileName, obj.ProcedureReview.ProcedureId, procedureCompletionDate,
                            obj.ProcedureReview.EndDateTime, obj.ProcedureReview.StartDateTime, obj.ProcedureReview.ProcedureReviewAcknowledgement);
                        list.Add(procedureReviewEmpModel);
                    }
                    return list;
                }
                else
                {
                    throw new QTDServerException("No Employee Procedure Found");
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["Employee Not Found"]);
            }
        }

    }
}
