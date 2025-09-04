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
using QTD2.Infrastructure.Model.IDP;
using IIDPDomainService = QTD2.Domain.Interfaces.Service.Core.IIDPService;
using IDeliveryMethodDomainService = QTD2.Domain.Interfaces.Service.Core.IDeliveryMethodService;
using IILA_TraineeEvaluatoionDomainService = QTD2.Domain.Interfaces.Service.Core.IILATraineeEvaluationService;
using ICBTDomainService = QTD2.Domain.Interfaces.Service.Core.ICBTService;
using IIDP_ReviewDomainService = QTD2.Domain.Interfaces.Service.Core.IIDP_ReviewService;
using QTD2.Infrastructure.Authorization.Operations.Core;
using IIDP_ReviewStatusDomainService = QTD2.Domain.Interfaces.Service.Core.IIDP_ReviewStatusService;
using IIlaDomainService = QTD2.Domain.Interfaces.Service.Core.IILAService;
using IILocationDomainService = QTD2.Domain.Interfaces.Service.Core.ILocation_Service;
using IClassScheduleEmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleEmployeeService;
using IIDPScheduleDomainService = QTD2.Domain.Interfaces.Service.Core.IIDPScheduleService;
using IClassScheduleDomainServiceService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleService;
using ITaskQualificationDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskQualificationService;
using IClassScheduleRosterStatusDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_Roster_StatusesService;
using ITestTypeDomainService = QTD2.Domain.Interfaces.Service.Core.ITestTypeService;
using IClassScheduleEvaluatorLinksDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_Evaluator_LinksService;
using QTD2.Domain.Exceptions;


namespace QTD2.Application.Services.Shared
{
    public class IDPService : IIDPService
    {
        private readonly IStringLocalizer<IDP> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIDPDomainService _IDPService;
        private readonly IDP _idp;
        private readonly IDeliveryMethodDomainService _deliveryMethodService;
        private readonly IILA_TraineeEvaluatoionDomainService _ilaTraineeEvalService;
        private readonly ICBTDomainService _cBTDomainService;
        private readonly IIDP_ReviewDomainService _idp_ReviewService;
        private readonly IDP_Review _idpReview;
        private readonly IIDP_ReviewStatusDomainService _idp_review_statusService;
        private readonly IIlaDomainService _ilaService;
        private readonly IILocationDomainService _locationService;
        private readonly IClassScheduleEmployeeDomainService _classScheduleEmployeeService;
        private readonly IIDPScheduleDomainService _iDPScheduleService;
        private readonly IClassScheduleDomainServiceService _classScheduleService;
        private readonly ITaskQualificationDomainService _taskQualificationService;
        private readonly ITestTypeDomainService _testtypeService;
        private readonly IClassScheduleRosterStatusDomainService _classScheduleRosterStatusDomainService;
        private readonly Domain.Interfaces.Service.Core.IClassSchedule_RosterService _classScheduleRosterDomainServive;
        private readonly IClassScheduleEvaluatorLinksDomainService _classScheduleEvaluatorLinksDomainService;

        public IDPService(
            IStringLocalizer<IDP> localizer,
           IIlaDomainService iLAService,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            UserManager<AppUser> userManager,
            IIDPDomainService iDPService,
            IDeliveryMethodDomainService deliveryMethodService,
            IILA_TraineeEvaluatoionDomainService ilaTraineeEvalService,
            IIDP_ReviewDomainService idp_ReviewService,
            IIDP_ReviewStatusDomainService idp_review_statusService,
            IILocationDomainService locationService,
            IClassScheduleEmployeeDomainService classScheduleEmployeeService,
            IIDPScheduleDomainService iDPScheduleService,
            IClassScheduleDomainServiceService classScheduleService,
             ITaskQualificationDomainService taskQualificationService,
             ITestTypeDomainService testtypeService,
             IClassScheduleRosterStatusDomainService classScheduleRosterStatusDomainService,
             Domain.Interfaces.Service.Core.IClassSchedule_RosterService classScheduleRosterDomainServive,
             ICBTDomainService cBTDomainService,
             IClassScheduleEvaluatorLinksDomainService classScheduleEvaluatorLinksDomainService
             )
        {
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _IDPService = iDPService;
            _idp = new IDP();
            _deliveryMethodService = deliveryMethodService;
            _ilaTraineeEvalService = ilaTraineeEvalService;
            _idp_ReviewService = idp_ReviewService;
            _idpReview = new IDP_Review();
            _idp_review_statusService = idp_review_statusService;
            _ilaService = iLAService;
            _locationService = locationService;
            _classScheduleEmployeeService = classScheduleEmployeeService;
            _iDPScheduleService = iDPScheduleService;
            _classScheduleService = classScheduleService;
            _taskQualificationService = taskQualificationService;
            _testtypeService = testtypeService;
            _classScheduleRosterStatusDomainService = classScheduleRosterStatusDomainService;
            _classScheduleRosterDomainServive = classScheduleRosterDomainServive;
            _cBTDomainService = cBTDomainService;
            _classScheduleEvaluatorLinksDomainService = classScheduleEvaluatorLinksDomainService;
        }

        public async Task<List<IDPVM>> GetAllIDPs(int empId, int year)
        {
            List<IDPVM> iDPVMs = new List<IDPVM>();
            var idpCheck = new IDP();
            var authorization = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, idpCheck, IDPOperations.Read);
            if (authorization.Succeeded)
            {
                DateTime customDate = new DateTime(year, 1, 1);
                var idps = (await _IDPService.FindWithIncludeAsync(r => r.EmployeeId == empId, new[] { "ILA.ClassSchedules", "ILA.ILA_SelfRegistrationOption", "ILA.DeliveryMethod", "ILA.CBTs" })).ToList();
                idps = idps.Where(r => r.IDPYear.HasValue && r.IDPYear.Value.Year == year).ToList();
                foreach (var idp in idps)
                {
                    var IsEnrolledAny = false;
                    var testLinked = await _ilaTraineeEvalService.GetCount(x => x.ILAId == idp.ILA.Id) != 0;
                    var cbtLinked = idp.ILA.CBTs.Count() != 0;
                    var classScheduleIds = idp.ILA.ClassSchedules.Select(x => x.Id).ToList();
                    var classSchedules = idp.ILA.ClassSchedules.ToList();
                    var deliveryMethod = idp.ILA.DeliveryMethod?.Name ?? String.Empty;
                    var idpSchedule = await _iDPScheduleService.FindAsync(r => r.IDPId == idp.Id && classScheduleIds.Contains(r.ClassScheduleId));
                    DateTime? plannedDate = idpSchedule.Select(x => x.plannedDate).FirstOrDefault();
                    var selfRegister = idp.ILA.ILA_SelfRegistrationOption?.MakeAvailableForSelfReg ?? false;
                    var csEmployees = await _classScheduleEmployeeService.FindQuery(x => x.EmployeeId == empId && x.ClassSchedule.StartDateTime.Year == year && classScheduleIds.Contains(x.ClassScheduleId) && x.IsEnrolled == true).ToListAsync();
                    if (csEmployees.Count > 0)
                    {
                        IsEnrolledAny = true;
                    }
                    else
                    {
                        IsEnrolledAny = false;
                    }
                    var idpVM = new IDPVM(idp.Id, idp.ILA.Name, idp.ILA.Number, deliveryMethod, plannedDate, testLinked, cbtLinked, selfRegister, IsEnrolledAny);
                    idpVM.EmpId = idp.EmployeeId;
                    idpVM.Active = idp.Active;
                    idpVM.ILAId = idp.ILAId;
                    idpVM.ILAActive = idp.ILA.Active;
                    iDPVMs.Add(idpVM);
                }
                return iDPVMs.OrderBy(x => x.ILATitle).ToList();
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
            }

        }

        public async Task<List<IDP_Review>> GetIDPReviewsForEMPAsync(int empId)
        {
            List<IDP_Review> idpReviewList = new List<IDP_Review>();
            var idpReviews = await _idp_ReviewService.FindQueryWithIncludeAsync(x => x.EmployeeId == empId, new string[] { nameof(_idpReview.IDP_ReviewStatus) }).ToListAsync();
            foreach (var idpReview in idpReviews)
            {
                idpReview.IDP_ReviewStatus = await _idp_review_statusService.FindQuery(x => x.Id == idpReview.StatusId).FirstOrDefaultAsync();
                idpReviewList.Add(idpReview);
            }
            return idpReviewList.Where(x => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, x, IDP_ReviewOperations.Read).Result.Succeeded).ToList();
        }

        public async Task<object> UpdateIDPGrade(UpdateGradeOptions options)
        {
            var csEmp = new ClassSchedule_Employee();
            var authorization = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, csEmp, ClassScheduleEmployeeOperations.Update);
            if (authorization.Succeeded)
            {
                var csEmployee = (await _classScheduleEmployeeService.FindWithIncludeAsync(x => x.EmployeeId == options.EmployeeId && x.ClassScheduleId == options.ClassScheduleId, new string[] { "ClassSchedule.ILA", "ClassSchedule.ClassSchedule_TQEMPSettings" }))?.FirstOrDefault();
                var idp = (await _IDPService.FindWithIncludeAsync(x => x.EmployeeId == options.EmployeeId && x.ILAId == csEmployee.ClassSchedule.ILAID, new[] { "IDPSchedules" })).FirstOrDefault();
                csEmployee.CompleteClass(options.completionDate, options.Grade, Convert.ToInt32(options.Score), options.reason);
                if (csEmployee.ClassSchedule.ILA.IsSelfPaced && options.completionDate.HasValue)
                {
                    csEmployee.ClassSchedule.EndDateTime = options.completionDate.Value;
                }

                if (options.isCompleted ?? false)
                {
                    csEmployee.PopulateOJTRecord = options.isCompleted.HasValue ? options.isCompleted.Value : false;
                    var ila = await _ilaService.GetWithIncludeAsync(csEmployee?.ClassSchedule.ILAID ?? 0, new string[] { "ILA_TaskObjective_Links.Task" });
                    var classScheduleEvaluatorLinks = await _classScheduleEvaluatorLinksDomainService.FindWithIncludeAsync(x => x.ClassScheduleId == csEmployee.ClassScheduleId, new string[] { "Evaluator" });
                    var tasks = ila.ILA_TaskObjective_Links.Where(x => x.UseForTQ == true).Select(x => x.Task);
                    var evaluators = classScheduleEvaluatorLinks.Select(x => x.Evaluator);
                    foreach (var task in tasks)
                    {
                        var taskqualification = (await _taskQualificationService.FindAsync(x => x.TaskId == task.Id && x.EmpId == options.EmployeeId && x.ClassScheduleId == options.ClassScheduleId)).FirstOrDefault();
                        if (taskqualification == null) continue;
                        taskqualification.TaskQualificationDate = options.completionDate?.ToUniversalTime() ?? DateTime.UtcNow;
                        taskqualification.CriteriaMet = true;
                        taskqualification.Comments = $"ILA Completion - {ila.Number}/{ila.Name}";
                        taskqualification.TQStatusId = taskqualification.TaskQualificationDate != null ? (taskqualification.TaskQualificationDate < taskqualification.DueDate ? 2 : 4) : 3;
                        var result = await _taskQualificationService.UpdateAsync(taskqualification);
                        if (!result.IsValid)
                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", result.Errors)]);
                    }
                }
                var idp_Schedule = idp.IDPSchedules.FirstOrDefault(x => x.ClassScheduleId == options.ClassScheduleId);
                if (idp_Schedule == null)
                {
                    var newIdpSchedule = new IDPSchedule(idp.Id, csEmployee.ClassScheduleId, csEmployee.ClassSchedule.StartDateTime, csEmployee.ClassSchedule.EndDateTime, csEmployee.PlannedDate, options.Grade, options.reason, options.Score, options.isCompleted, options.completionDate);
                    idp.AddIdpSchedule(newIdpSchedule);
                }
                else
                {
                    idp_Schedule.UpdateGradeRelatedData(options.Grade, options.reason, options.Score, options.isCompleted, options.completionDate);
                }
                var validationResult = await _classScheduleEmployeeService.UpdateAsync(csEmployee);
                await _IDPService.UpdateAsync(idp);
                if (!validationResult.IsValid)
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                return new { message = "Grades updated successfully" };
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
            }
        }

        public async Task<IDP_Review> CreateIDPReviewAsync(IDP_ReviewCreateOptions options)
        {
            var idp = new IDP_Review();
            idp.Comments = options.Comments;
            idp.CompletedDate = options.CompletedDate;
            idp.EmployeeId = options.EmployeeId;
            idp.EndDate = options.EndDate;
            idp.ReleaseDate = options.ReleaseDate;
            idp.Instructions = options.Instructions;
            idp.Title = options.Title;
            idp.StatusId = GetIDPReviewStatusId(options);
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            idp.Create(userName);
            var authorization = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, idp, IDPOperations.Create);
            if (authorization.Succeeded)
            {
                var validationResult = await _idp_ReviewService.AddAsync(idp);
                if (validationResult.IsValid)
                {
                    return idp;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
            }
        }
        public async System.Threading.Tasks.Task LinkIDPAsync(int empId, IdpTrainingLinkOptions options)
        {
            var idp = new IDP();
            var authorization = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, idp, IDPOperations.Create);
            if (authorization.Succeeded)
            {
                foreach (int ilaId in options.ilaIds)
                {
                    var ilaToDownload = await _ilaService.GetAsync(ilaId);
                    DateTime customDate = new DateTime(options.IdpYear, 1, 1);
                    var idpToAdd = new IDP(empId, ilaId, customDate);
                    var existingIDP = await _IDPService.FindQuery(x => x.EmployeeId == empId && x.ILAId == ilaId && x.IDPYear == customDate).FirstOrDefaultAsync();
                    if (existingIDP == null)
                    {
                        idpToAdd.Create(_httpContextAccessor.HttpContext.User.Identity.Name);
                        var validationResult = await _IDPService.AddAsync(idpToAdd);

                        if (!validationResult.IsValid)
                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                    }
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
            }
        }

        public int GetIDPReviewStatusId(IDP_ReviewCreateOptions options)
        {
            if (options.CompletedDate == null && options.IsStarted)
            {
                return 2;
            }
            else if (options.CompletedDate != null)
            {
                return 3;
            }
            return 1;
        }

        public async System.Threading.Tasks.Task<object> GetLinkedSchedulingClasses(int id, int empId)
        {
            var toReturn = new List<object>();
            var idp = new IDP();
            var authorization = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, idp, IDPOperations.Read);
            if (authorization.Succeeded)
            {
                var idpSchedules = await _iDPScheduleService.FindAsync(r => r.IDPId == id);
                foreach (var schedule in idpSchedules.Where(r => r.Active && !r.Deleted))
                {
                    var location = (await _locationService.FindAsync(x => x.ClassSchedules.Any(x => x.Id == schedule.ClassScheduleId))).FirstOrDefault();
                    var classScheduleEmployee = (await _classScheduleEmployeeService.FindAsync(x => x.EmployeeId == empId && x.ClassScheduleId == schedule.ClassScheduleId && x.Active && x.IsEnrolled)).FirstOrDefault();
                    if (classScheduleEmployee != null)
                    {
                        toReturn.Add(new
                        {
                            IDPScheduleId = schedule.Id,
                            startDate = schedule.startDate,
                            completionDate = schedule.CompletionDate,
                            location = location?.LocName,
                            IsEnrolled = classScheduleEmployee?.IsEnrolled ?? false,
                            plannedDate = schedule.plannedDate,
                            grade = schedule.Grade,
                            score = schedule.Score,
                            empId = classScheduleEmployee.EmployeeId,
                            classScheduleId = schedule.ClassScheduleId,
                            gradeUpdateReason = schedule.GradeReason,
                            ClassScheduleEmployeeId = classScheduleEmployee.Id
                        });
                    }
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
            }

            return toReturn;
        }

        public async Task<object> EnrollEmployeeToClass(int id, int empId, EnrollEmployeeOptions options)
        {
            var idpSchedule = new IDPSchedule();
            var authorization = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, idpSchedule, IDPOperations.Update);
            if (authorization.Succeeded)
            {

                var employeeIDP = await _iDPScheduleService.GetWithIncludeAsync(id, new string[] { "ClassSchedule" });
                employeeIDP.plannedDate = options.plannedDate;
                var validationResult = await _iDPScheduleService.UpdateAsync(employeeIDP);

                if (!validationResult.IsValid)
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);

                var classScheduleEmployee = (await _classScheduleEmployeeService.FindAsync(x => x.EmployeeId == empId && x.ClassScheduleId == employeeIDP.ClassScheduleId)).FirstOrDefault();
                classScheduleEmployee.IsEnrolled = true;
                classScheduleEmployee.IsWaitlisted = false;

                var validationResults = await _classScheduleEmployeeService.UpdateAsync(classScheduleEmployee);

                if (!validationResult.IsValid)
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);

                return new { message = "Employee entrolled successfully" };


            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
            }

        }
        public async Task<object> UnEnrollEmployeeFromIDP(int ilaId, int empId)
        {
            var idpSchedule = new IDPSchedule();
            var authorization = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, idpSchedule, IDPOperations.Update);
            if (authorization.Succeeded)
            {

                var ilaToDownload = await _ilaService.GetWithIncludeAsync(ilaId, new string[] { "ClassSchedules" });
                foreach (var item in ilaToDownload?.ClassSchedules)
                {
                    var classScheduleEmployees = await _classScheduleEmployeeService.FindAsync(x => x.EmployeeId == empId && x.ClassScheduleId == item.Id);
                    foreach (var employee in classScheduleEmployees)
                    {

                        var validationResults = await _classScheduleEmployeeService.DeleteAsync(employee);

                        if (!validationResults.IsValid)
                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResults.Errors)]);

                    }

                }

                return new { message = "Employee UnEnrolled  successfully from ILA" };


            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
            }

        }

        public async System.Threading.Tasks.Task UpdateIDPDate(UpdateIDPScheduleDateOptions options)
        {
            var idps = new IDP();
            var authorization = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, idps, IDPScheduleOperations.Update);
            if (authorization.Succeeded)
            {

                var idpSchedule = (await _iDPScheduleService.FindAsync(x => x.ClassScheduleId == options.classScheduleId && x.IDPId == options.idpId)).FirstOrDefault();
                idpSchedule.startDate = options.startDate;
                idpSchedule.endDate = options.endDate;
                var validationResult = await _iDPScheduleService.UpdateAsync(idpSchedule);

                if (!validationResult.IsValid)
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);

            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
            }

        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var idp = (await _IDPService.FindAsync(x => x.Id == id)).FirstOrDefault();
            var idpSchedules = (await _iDPScheduleService.FindAsync(r => r.IDPId == id && !r.Deleted)).ToList();
            if (idpSchedules.Count == 0)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, idp, IDPOperations.Delete);
                if (result.Succeeded)
                {
                    idp.Delete();

                    var validationResult = await _IDPService.UpdateAsync(idp);

                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: "OperationNotAllowed");
                }
            }
            else
            {
                throw new QTDServerException("IDPSchedule Exists for IDP");
            }
        }
    }
}
