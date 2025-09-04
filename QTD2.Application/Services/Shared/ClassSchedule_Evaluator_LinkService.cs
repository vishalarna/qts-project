using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ClassSchedule_TQEMPSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IClassScheduleEvaluatorLinksDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_Evaluator_LinksService;
using IClassScheduleDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleService;
using IEmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Application.Services.Shared
{
    public class ClassSchedule_Evaluator_LinkService : IClassSchedule_Evaluator_LinksService
    {
        private readonly IClassScheduleEvaluatorLinksDomainService _classScheduleEvaluatorLinksDomainService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer _localizer;
        private readonly IClassScheduleDomainService _classScheduleService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmployeeDomainService _employeeDomainService;

        public ClassSchedule_Evaluator_LinkService(IClassScheduleEvaluatorLinksDomainService classScheduleEvaluatorLinksDomainService, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<ClassScheduleService> localizer, IClassScheduleDomainService classScheduleService, UserManager<AppUser> userManager, IEmployeeDomainService employeeDomainService)
        {
            _classScheduleEvaluatorLinksDomainService = classScheduleEvaluatorLinksDomainService;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _classScheduleService = classScheduleService;
            _userManager = userManager;
            _employeeDomainService = employeeDomainService;
        }

        public async System.Threading.Tasks.Task LinkEvaluators(ClassScheduleEvaluatorLinksVM options)
        {
            var classSchedule = await _classScheduleService.GetClassScheduleWithEvaluatorsAsync(options.ClassScheduleId);
            if (classSchedule != null)
            {
                foreach (var evalId in options.EvaluatorIds)
                {
                    var eval = await _employeeDomainService.GetAsync(evalId);
                    if (eval != null)
                    {
                        classSchedule.LinkEvaluator(eval);
                        await _classScheduleService.UpdateAsync(classSchedule);
                    }
                    else
                    {
                        throw new BadHttpRequestException(message: _localizer["Evaluator Not Found Exception"]);
                    }
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["ClassSchedule Not Found Exception"]);
            }
        }

        public async System.Threading.Tasks.Task UnlinkEvaluator(ClassScheduleEvaluatorLinksVM options)
        {
            var classSchedule = await _classScheduleService.GetClassScheduleWithEvaluatorsAsync(options.ClassScheduleId);
            if (classSchedule != null)
            {
                foreach (var evalId in options.EvaluatorIds)
                {
                    var eval = await _employeeDomainService.GetAsync(evalId);
                    if (eval != null)
                    {
                        classSchedule.UnlinkEvaluator(eval);
                        var result = await _classScheduleService.UpdateAsync(classSchedule);
                        if (!result.IsValid)
                        {
                            throw new BadHttpRequestException(message: result.Errors.FirstOrDefault()?.Message);
                        }
                    }
                    else
                    {
                        throw new BadHttpRequestException(message: _localizer["Evaluator Not Found Exception"]);
                    }
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["Class Schedule Not Found Exception"]);
            }
        }

        public async System.Threading.Tasks.Task<List<Employee>> LinkEvaluatorsFromILA(int classScheduleId)
        {
            var alreadyExistEvaluator = await _classScheduleEvaluatorLinksDomainService.GetClassScheduleTQEvaluators(classScheduleId);
            if(alreadyExistEvaluator.Count() != 0)
            {
                return await GetClassScheduleTQEvaluators(classScheduleId);
            }
            else
            {
                var classSchedule = await _classScheduleService.GetClassScheduleWithIlaTQEMPSettings(classScheduleId);
                var isRecurring = classSchedule.IsRecurring;
                var ilaEvaluatorsLinks = classSchedule?.ILA?.ILA_Evaluator_Links.ToList();
                var classEvaluatorsLinks = new List<ClassSchedule_Evaluator_Link>();
                if (isRecurring)
                {
                    var recurringClassSchedules = (await _classScheduleService.GetRecurringClassSchedules(classSchedule?.RecurrenceId)).Where(cs => cs.Id != classScheduleId);
                    foreach (var recurringClassSchedule in recurringClassSchedules)
                    {
                        foreach (var evaluatorLink in ilaEvaluatorsLinks)
                        {
                            var classEvaluator = new ClassSchedule_Evaluator_Link(recurringClassSchedule.Id, evaluatorLink.EvaluatorId);
                            classEvaluatorsLinks.Add(classEvaluator);
                        }
                    }
                }
                foreach(var evaluatorLink in ilaEvaluatorsLinks)
                {
                    var classEvaluator = new ClassSchedule_Evaluator_Link(classScheduleId, evaluatorLink.EvaluatorId);
                    classEvaluatorsLinks.Add(classEvaluator);
                }
                if (classEvaluatorsLinks.Any())
                {
                    await _classScheduleEvaluatorLinksDomainService.AddRangeAsync(classEvaluatorsLinks);
                }
                return await GetClassScheduleTQEvaluators(classScheduleId);
            }
        }

        public async System.Threading.Tasks.Task<List<Employee>> GetClassScheduleTQEvaluators(int classScheduleId)
        {
            var evaluators = await _classScheduleEvaluatorLinksDomainService.GetClassScheduleTQEvaluators(classScheduleId);
            return evaluators.Select(x => x.Evaluator).ToList();
        }
    }
}
