using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.Meta_ILAMembers_Link;
using QTD2.Infrastructure.Model.MetaILA_Employee;
using QTD2.Infrastructure.Model.MetaILA;
using IILADomainService = QTD2.Domain.Interfaces.Service.Core.IILAService;
using IMetaILADomainService = QTD2.Domain.Interfaces.Service.Core.IMetaILAService;
using IMetaILALinkDomainService = QTD2.Domain.Interfaces.Service.Core.IMeta_ILAMembers_LinkService;
using IMetaILAConfigurationPublishOptionDomainService = QTD2.Domain.Interfaces.Service.Core.IMetaILAConfigurationPublishOptionService;
using QTD2.Infrastructure.Model.Provider;
using IIILATraineeEvaluationDomainService = QTD2.Domain.Interfaces.Service.Core.IILATraineeEvaluationService;
using ICBTDomainService = QTD2.Domain.Interfaces.Service.Core.ICBTService;
using QTD2.Domain.Exceptions;
using IILAService = QTD2.Application.Interfaces.Services.Shared.IILAService;
using IMetaILAEmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IMetaILA_EmployeeService;
using IClassScheduleRosterDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_RosterService;
using IClassScheduleEvaluationRosterDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_Evaluation_RosterService;
using IMetaILAEmpFullFillmentDomainService = QTD2.Domain.Interfaces.Service.Core.IMetaILA_Employee_MemberLinkFufillmentService;
using IClassScheduleDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleService;
using IClassScheduleEmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleEmployeeService;

namespace QTD2.Application.Services.Shared
{
    public class MetaILAService : IMetaILAService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<MetaILAService> _localizer;
        private readonly IMetaILADomainService _metaILAService;
        private readonly IMetaILAConfigurationPublishOptionDomainService _metaILAConfigurationPublishOptionDomainService;
        private readonly IILADomainService _ilaService;
        private readonly UserManager<AppUser> _userManager;
        private readonly MetaILA _metaila;
        private readonly Meta_ILAMembers_Link _meta_ILAMembers_Link;
        private readonly IMetaILALinkDomainService _metaILALinkDomainService;
        private readonly IIILATraineeEvaluationDomainService _iLATraineeEvaluationDomainService;
        private readonly ICBTDomainService _cbtService;
        private readonly IILAService _iLAService;
        private readonly IMetaILAEmployeeDomainService _metaILAEmployeeDomainService;
        private readonly IClassScheduleRosterDomainService _classScheduleRosterDomainService;
        private readonly IClassScheduleEvaluationRosterDomainService _classScheduleEvaluationRosterDomainService;
        private readonly IMetaILAEmpFullFillmentDomainService _metaILAEmpFullFillmentDomainService;
        private readonly IClassScheduleDomainService _classScheduleDomainService;
        private readonly IClassScheduleEmployeeDomainService _classScheduleEmployeeDomainService;

        public MetaILAService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<MetaILAService> localizer, IMetaILADomainService metaILAService, UserManager<AppUser> userManager, IMetaILAConfigurationPublishOptionDomainService metaILAConfigurationPublishOptionDomainService, IILADomainService iLADomainService, IMetaILALinkDomainService metaILALinkDomainService, IIILATraineeEvaluationDomainService iLATraineeEvaluationDomainService, ICBTDomainService cbtService, IILAService iLAService, IMetaILAEmployeeDomainService metaILAEmployeeDomainService, IClassScheduleRosterDomainService classScheduleRosterDomainService, IClassScheduleEvaluationRosterDomainService classScheduleEvaluationRosterDomainService, IMetaILAEmpFullFillmentDomainService metaILAEmpFullFillmentDomainService, IClassScheduleDomainService classScheduleDomainService, IClassScheduleEmployeeDomainService classScheduleEmployeeDomainService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _metaILAService = metaILAService;
            _ilaService = iLADomainService;
            _userManager = userManager;
            _metaILAConfigurationPublishOptionDomainService = metaILAConfigurationPublishOptionDomainService;
            _metaila = new MetaILA();
            _meta_ILAMembers_Link = new Meta_ILAMembers_Link();
            _metaILALinkDomainService = metaILALinkDomainService;
            _iLATraineeEvaluationDomainService = iLATraineeEvaluationDomainService;
            _cbtService = cbtService;
            _iLAService = iLAService;
            _metaILAEmployeeDomainService = metaILAEmployeeDomainService;
            _classScheduleRosterDomainService = classScheduleRosterDomainService;
            _classScheduleEvaluationRosterDomainService = classScheduleEvaluationRosterDomainService;
            _metaILAEmpFullFillmentDomainService = metaILAEmpFullFillmentDomainService;
            _classScheduleDomainService = classScheduleDomainService;
            _classScheduleEmployeeDomainService = classScheduleEmployeeDomainService;
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Activate();

                var validationResult = await _metaILAService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async Task<MetaILA> CreateAsync(MetaILACreateOptions options)
        {
            var obj = new MetaILA(options.Name, options.Description, options.MetaILAStatusId ?? 1, options.EffectiveDate, options.MetaILA_SummaryFinalTestId,options.MetaILA_SummaryRetakeTestId, options.StudentEvaluationId, options.Reason,options.ProviderId);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                var validationResult = await _metaILAService.AddAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return obj;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Delete();

                var validationResult = await _metaILAService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async Task<List<MetaILAVM>> GetAsync()
        {
            //var obj_list = await _metaILAService.GetWithIncludeAsync(new string[] { nameof(_metaila.Meta_ILAMembers_Links) });

            var obj_list = await _metaILAService.AllWithIncludeAsync(new string[] { "Meta_ILAMembers_Links.ILA.ClassSchedules" });
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded);
            return obj_list?.Select(mila=>new MetaILAVM(mila.Id,mila.Name,mila.Active,mila.IsDeleteAllowed,mila.IsReleasedToEmployees,mila.Meta_ILAMembers_Links?.Count())).ToList();
        }

        public async Task<MetaILA> GetAsync(int id)
        {
            var obj = await _metaILAService.GetWithIncludeAsync(id, new[] { "Meta_ILAMembers_Links.ILA.Provider", "Meta_ILAMembers_Links.ILA.DeliveryMethod", "Meta_ILAMembers_Links.MetaILAConfigurationPublishOption", "MetaILA_Employees" });
            if (obj != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read);
                if (result.Succeeded)
                {
                    return obj;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }
        }

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Deactivate();

                var validationResult = await _metaILAService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async Task<MetaILA> UpdateAsync(int id, MetaILAUpdateOptions options)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

            if (result.Succeeded)
            {
                obj.Name = options.Name;
                obj.Description = options.Description;
                obj.MetaILAStatusId = options.MetaILAStatusId;
                obj.EffectiveDate = options.EffectiveDate;
                obj.Reason = options.Reason;
                obj.MetaILA_SummaryTest_FinalTestId = options.MetaILA_SummaryFinalTestId;
                obj.MetaILA_SummaryTest_RetakeTestId = options.MetaILA_SummaryRetakeTestId;
                obj.StudentEvaluationId = options.StudentEvaluationId;
                obj.ProviderId = options.ProviderId;
                obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.ModifiedDate = DateTime.Now;
                var validationResult = await _metaILAService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return obj;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        /* Meta_ILAMemebers_Link starts*/
        public async System.Threading.Tasks.Task<List<MetaILA_ILAMemberVM>> LinkILAMemeberAsync(Meta_ILAMembers_ListOptions options)
        {
            var metaILAId = options.IlaMetaILAMembers.Select(m=>m.MetaILAID).FirstOrDefault();
            var ilaIds = options.IlaMetaILAMembers.Select(m => m.ILAID).ToList();
            foreach(var option in options.IlaMetaILAMembers)
            {
                var metaILA = await GetAsync(option.MetaILAID);
                var ilaMemeber = await _ilaService.GetAsync(option.ILAID);
                var metaILAConfigPublishOption = await _metaILAConfigurationPublishOptionDomainService.GetAsync(option.MetaILAConfigPublishOptionID.GetValueOrDefault());

                if (ilaMemeber == null)
                {
                    throw new QTDServerException(_localizer["ILAMemeberNotFound"]);
                }

                var meta_ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, metaILA, AuthorizationOperations.Update);

                var ila_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ilaMemeber, AuthorizationOperations.Read);

                if (meta_ilaResult.Succeeded && ila_Result.Succeeded)
                {
                    DateTime? startDate = null;
                    if(option.StartDate != DateTime.MinValue)
                    {
                        startDate = option.StartDate?.ToUniversalTime();
                    }
                    var meta_ila_link = metaILA.LinkILAMemebers(ilaMemeber, metaILAConfigPublishOption, option.SequenceNumber, startDate);
                    meta_ila_link.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    meta_ila_link.CreatedDate = DateTime.Now;

                    var result = await _metaILAService.UpdateAsync(metaILA);
                    if (!result.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', result.Errors));
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            var metaILAMembers = await _metaILALinkDomainService.GetMetaILAMembersByIdsAsync(metaILAId,ilaIds);
            return metaILAMembers.Select(m => MapMetaILAMemberLinksToMetaILA_ILAMemberVM(m)).ToList();
        }

        public async Task<MetaILA_ILAMemberVM> UpdateILAMembersLinkAsync(Meta_ILAMembers_LinkOptions options)
        {
            DateTime? startDate = null;
            if (options.StartDate != DateTime.MinValue)
            {
                startDate = options.StartDate?.ToUniversalTime();
            }
            var meta_ILAMembers_Link = await _metaILALinkDomainService.UpdateAsync(options.ILAID, options.MetaILAID, options.MetaILAConfigPublishOptionID, options.SequenceNumber, startDate);
            return MapMetaILAMemberLinksToMetaILA_ILAMemberVM(meta_ILAMembers_Link);
        }

        public async System.Threading.Tasks.Task UnlinkILAMemeberAsync(int id, int ilaMemeberID)
        {
            var metaila = await GetAsync(id);
            var ilaMemeber = await _ilaService.GetAsync(ilaMemeberID);

            if (ilaMemeber == null)
            {
                throw new QTDServerException(_localizer["ILAMemeberNotFound"]);
            }

            var meta_ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, metaila, AuthorizationOperations.Update);

            var ila_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ilaMemeber, AuthorizationOperations.Read);

            if (ila_Result.Succeeded && meta_ilaResult.Succeeded)
            {
                metaila.UnlinkILAMemeber(ilaMemeber);

                await _ilaService.UpdateAsync(ilaMemeber);
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task RemoveILAMemeberAsync(int id, int linkedId)
        {
            var metaila = await GetAsync(id);
            var ilaMemeber = await _ilaService.GetAsync(linkedId);
            metaila.UnlinkILAMemeber(ilaMemeber);
            await _metaILAService.UpdateAsync(metaila);
        }

        public async Task<List<ILA>> GetLinkedILAMemebersAsync(int id)
        {
            var result = await _metaILAService.GetAsync(id);
            List<ILA> linked_list = new List<ILA>();
            linked_list.AddRange(result.Meta_ILAMembers_Links.Where(x => x.ILAID == id).Select(x => x.ILA));
            linked_list = linked_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded).ToList();
            return linked_list;
        }

        public async Task<List<ILA_ProviderVM>> GetLinkedILAAsync()
        {
            var providers = await _metaILAService.AllWithIncludeAsync(new string[] { nameof(_metaila.Meta_ILAMembers_Links) }); ;
            providers = providers.Where(provider => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, provider, ProviderOperations.Read).Result.Succeeded);
            var ilaProviderVM = new List<ILA_ProviderVM>();
            foreach (var provider in providers)
            {
                // var metaILALinkCount = await _metaILALinkDomainService.GetCount(x =>x.Id == provider.Id);
                ilaProviderVM.Add(new ILA_ProviderVM
                {
                    Id = provider.Id,
                    Active = provider.Active,
                    IsPriority = false,
                    Name = provider.Name,
                    isNerc = false,
                    ILACount = provider.Meta_ILAMembers_Links.Count(),
                });
            }

            return ilaProviderVM.ToList();
        }
        /* Meta_ILAMemebers_Link ends*/

        public async Task<MetaILAILARequirements_VM> GetMetaILAILARequirements(int iLAId)
        {
            var ilaTraineeEvaluations = await _iLATraineeEvaluationDomainService.GetLinkedTestsWithILANumberAsync(iLAId);
            var pretestsWithILANumber = ilaTraineeEvaluations.Where(x => x.TestType?.Description.ToLower() == "pretest").Select(x => x.ILA?.Number + ' ' + x.Test?.TestTitle).ToList();
            var testsWithILANumber = ilaTraineeEvaluations.Where(x => x.TestType?.Description.ToLower() == "final test").Select(x => x.ILA?.Number + ' ' + x.Test?.TestTitle).ToList();
            var cbtList = await _cbtService.GetCBTsScormUploadNamesByILAId(iLAId);
            var metaILAILARequirements_VM = new MetaILAILARequirements_VM(pretestsWithILANumber, testsWithILANumber, cbtList);
            return metaILAILARequirements_VM;
        }
        public async Task<List<MetaILA_EmployeeVM>> GetLinkedMetaILAEmployeesAsync(int metaILAId)
        {
            var result = await _metaILAService.GetMetaILAEmployeesByIdAsync(metaILAId);
            return result.Select(emp=>MapMetaILAEmployeeToMetaILA_EmployeeVM(emp)).ToList();
        }
        public async Task<List<MetaILA_EmployeeVM>> LinkMetaILAEMployeesAsync(MetaILA_EmployeeOptions options)
        {
            var allLinkedEmployees = new List<MetaILA_EmployeeVM>();

            var currentUser = _httpContextAccessor.HttpContext.User;
            var currentUserName = currentUser.Identity?.Name;

            if (string.IsNullOrEmpty(currentUserName))
            {
                throw new UnauthorizedAccessException(_localizer["InvalidUser"]);
            }

            var userId = (await _userManager.FindByEmailAsync(currentUserName))?.Id;

            if (userId == null)
            {
                throw new UnauthorizedAccessException(_localizer["InvalidUser"]);
            }

            foreach (var metaILAId in options.MetaILAIDs)
            {
                var metaILA = await GetAsync(metaILAId);

                if (metaILA == null)
                {
                    throw new QTDServerException(_localizer["MetaILANotFound"]);
                }

                var authorizationResult = await _authorizationService.AuthorizeAsync(currentUser, metaILA, AuthorizationOperations.Update);

                if (!authorizationResult.Succeeded)
                {
                    throw new UnauthorizedAccessException(_localizer["OperationNotAllowed"]);
                }

                foreach (int employeeID in options.EmployeeIDs)
                {
                    switch (options.IsComingFrom.ToLower().Trim())
                    {
                        case "metailawizard":
                        var link = metaILA.AddMetaILA_Employee(employeeID, metaILAId);
                        link.Create(userId);
                        metaILA.EnrollEmployee(employeeID,false);
                        break;

                        case "metaidp":
                        var addLink = metaILA.AddMetaILA_Employee(employeeID, metaILAId);
                        addLink.Create(userId);
                        break;

                        case "metaidpenroll":
                        metaILA.EnrollEmployee(employeeID, true);
                        break;
                    };
                }

                await _metaILAService.UpdateAsync(metaILA);
                var linkedEmployees = await _metaILAService.GetMetaILAEmployeesByIdAsync(metaILA.Id);
                var linkedEmployeeVMs = linkedEmployees.Select(emp => MapMetaILAEmployeeToMetaILA_EmployeeVM(emp));
                allLinkedEmployees.AddRange(linkedEmployeeVMs);
            }

            return allLinkedEmployees;
        }


        public async Task<List<MetaILA_EmployeeVM>> UnlinkLinkMetaILAEMployeesAsync(MetaILA_EmployeeOptions options)
        {
            var allUnlinkedEmployees = new List<MetaILA_EmployeeVM>();

            var currentUser = _httpContextAccessor.HttpContext.User;
            var currentUserName = currentUser.Identity?.Name;

            if (string.IsNullOrEmpty(currentUserName))
            {
                throw new UnauthorizedAccessException(_localizer["InvalidUser"]);
            }

            foreach (var metaILAId in options.MetaILAIDs)
            {
                var metaILA = await GetAsync(metaILAId);

                if (metaILA == null)
                {
                    throw new QTDServerException(_localizer["MetaILANotFound"]);
                }

                var authorizationResult = await _authorizationService.AuthorizeAsync(currentUser, metaILA, AuthorizationOperations.Update);

                if (!authorizationResult.Succeeded)
                {
                    throw new UnauthorizedAccessException(_localizer["OperationNotAllowed"]);
                }

                foreach (int employeeID in options.EmployeeIDs)
                {
                    metaILA.RemoveMetaILA_Employee(employeeID, metaILAId);
                }

                await _metaILAService.UpdateAsync(metaILA);

                var updatedEmployees = await _metaILAService.GetMetaILAEmployeesByIdAsync(metaILA.Id);
                var updatedEmployeeVMs = updatedEmployees.Select(emp => MapMetaILAEmployeeToMetaILA_EmployeeVM(emp));
                allUnlinkedEmployees.AddRange(updatedEmployeeVMs);
            }

            return allUnlinkedEmployees;
        }


        public async Task<List<EnablingObjective>> GetEnablingObjectivesLinkedToMetaILAAsync(int metaILAId)
        {
            var metaILAMembers = await _metaILALinkDomainService.FindAsync(x=>x.MetaILAID == metaILAId);
            List<int> ilaIds = metaILAMembers.Select(m => m.ILAID).Distinct().ToList();
            var enablingobjectives = await _iLAService.GetLinkedEnablingObjectivesByIlaIdsAsync(ilaIds);
            return enablingobjectives.ToList();
        }

        public MetaILAVM MapMetaILAToMetaILAVM(MetaILA metaILA)
        {
            return new MetaILAVM(metaILA.Id, metaILA?.MetaILA_SummaryTest_FinalTestId, metaILA?.MetaILA_SummaryTest_RetakeTestId, metaILA?.StudentEvaluationId, metaILA.MetaILAStatusId, metaILA?.Name, metaILA?.Description, metaILA?.Reason, metaILA.Active, metaILA.IsDeleteAllowed, metaILA.IsReleasedToEmployees, metaILA?.EffectiveDate,metaILA.MetaILA_Employees.Select(emp=>MapMetaILAEmployeeToMetaILA_EmployeeVM(emp)).ToList(),metaILA.Meta_ILAMembers_Links.Select(link=>MapMetaILAMemberLinksToMetaILA_ILAMemberVM(link)).ToList(),metaILA.ProviderId);
        }

        public MetaILA_ILAMemberVM MapMetaILAMemberLinksToMetaILA_ILAMemberVM(Meta_ILAMembers_Link meta_ILAMembers_Link)
        {
            return new MetaILA_ILAMemberVM(meta_ILAMembers_Link.ILAID, meta_ILAMembers_Link.ILA?.ProviderId, meta_ILAMembers_Link.MetaILAConfigPublishOptionID, meta_ILAMembers_Link.ILA?.Active, meta_ILAMembers_Link?.ILA?.DeliveryMethod?.DisplayName, meta_ILAMembers_Link.ILA?.Image, meta_ILAMembers_Link.ILA?.NickName, meta_ILAMembers_Link.ILA?.Name, meta_ILAMembers_Link.ILA?.Number, meta_ILAMembers_Link.ILA?.Provider?.Name, meta_ILAMembers_Link?.MetaILAConfigurationPublishOption?.Description, meta_ILAMembers_Link?.SequenceNumber, meta_ILAMembers_Link.StartDate);
        }

        public MetaILA_EmployeeVM MapMetaILAEmployeeToMetaILA_EmployeeVM(MetaILA_Employee metaILA_Employee)
        {
            return new MetaILA_EmployeeVM(metaILA_Employee?.EmployeeId, metaILA_Employee?.Employee?.Person != null ? $"{metaILA_Employee?.Employee?.Person?.FirstName} {metaILA_Employee?.Employee?.Person?.LastName}" : null, metaILA_Employee.Employee != null ? string.Join(",", metaILA_Employee.Employee?.EmployeePositions.Select(ep => ep.Position?.PositionTitle)): null, metaILA_Employee.Employee != null ? string.Join(",", metaILA_Employee.Employee?.EmployeeOrganizations.Select(ep => ep.Organization?.Name)) : null,metaILA_Employee?.Employee?.Person?.Image);
        }

        public async Task<List<MetaILANercCertificationDetailVM>> GetMetaILANERCCertificationDetailsAsync(int metaILAId)
        {
            var metaIla = await _metaILAService.GetMetaILAWithMembersOnlyAsync(metaILAId);
            var metaILAMemberIds = metaIla.Meta_ILAMembers_Links.Select(im => im.ILAID).ToList();
            var ilaList = await _ilaService.GetILAsWithCertificationInformationAsync(metaILAMemberIds);
            foreach(var ila in ilaList)
            {
                ila.ILACertificationLinks = ila.ILACertificationLinks.Where(x => x.Certification?.CertifyingBody?.Name == "NERC").ToList();
            }
            return ilaList.Select(ila => new MetaILANercCertificationDetailVM(ila.Name, ila.ILACertificationLinks.Select(m => m.CEHHours)?.FirstOrDefault(), ila.ILACertificationLinks.SelectMany(m => m.ILACertificationSubRequirementLink)?.FirstOrDefault(m => m.CertificationSubRequirement.ReqName == "Standards")?.ReqHour, ila.ILACertificationLinks.SelectMany(m => m.ILACertificationSubRequirementLink)?.FirstOrDefault(m => m.CertificationSubRequirement.ReqName == "Simulations")?.ReqHour, ila.ILACertificationLinks.Any(m => m.IsEmergencyOpHours),ila.TotalTrainingHours)).ToList();
        }

        public async Task<List<MetaILA_IDPVM>> GetLinkedMetaILAsByEmployeeIdForIDPAsync(int employeeId)
        {
            var metaIlaEmployee = await _metaILAEmployeeDomainService.GetByEmployeeId(employeeId);
            var metaILAIds = metaIlaEmployee.Select(m => m.MetaILAId).Distinct().ToList();

            var metaILAs = await _metaILAService.GetMetaILAByIDAsync(metaILAIds);
            var classRoster = await _classScheduleRosterDomainService.GetClassScheduleRosterByMetaEmployeeIdAsync(employeeId);

            var evalRosters = await _classScheduleEvaluationRosterDomainService.GetClassScheduleEvaluationRosterByMetaILAIdsAsync(metaILAIds);
            var evalRostersDict = evalRosters.GroupBy(e => e.MetaIlaId).ToDictionary(g => g.Key, g => g.ToList());

            var metaILAsVM = new List<MetaILA_IDPVM>();

            foreach (var metaILA in metaILAs)
            {
                var ilaIds = metaILA.Meta_ILAMembers_Links.Select(x => x.ILAID).ToHashSet();

                var linkedCourseCount = classRoster.Count(x =>
                    x.ClassSchedule?.ILAID != null && ilaIds.Contains(x.ClassSchedule.ILAID.Value));

                var classSchedules = (await _classScheduleDomainService.GetClassSchedulesByILAIdsAsync(ilaIds.ToList()));

                var completedCount = 0;

                foreach(var member in metaILA.Meta_ILAMembers_Links)
                {
                    var classSchedule = classSchedules.Where(c => c.ILAID.Value == member.ILAID && c.InstructorId == null && c.LocationId == null).OrderByDescending(x => x.Id)?.FirstOrDefault();
                    var fulfill = (await _metaILAEmpFullFillmentDomainService.GetWithClassScheduleEmployeeByMemberAndEmpIdAsync(member.Id,employeeId));
                    if (fulfill?.Where(m => m.FufilledBy_ClassScheduleEmployee.ClassScheduleId == classSchedule?.Id)?.FirstOrDefault()?.FufilledBy_ClassScheduleEmployee?.IsComplete == true)
                    {
                        completedCount++;
                    }
                }

                var testId = metaILA.MetaILA_SummaryTest_FinalTest?.TestId;
                var testResult = classRoster.FirstOrDefault(x => x.TestId == testId);

                evalRostersDict.TryGetValue(metaILA.Id, out var evalRosterList);

                var metaILAVM = new MetaILA_IDPVM()
                {
                    MetaILAId = metaILA.Id,
                    MetaILATitle = metaILA.Name,
                    CompletedLinkedCourses = $"{completedCount}/{ilaIds.Count}",
                    MetaStatus = metaILA.MetaILAStatus?.Name,
                    MetaTestCompletedDate = testResult?.CompletedDate,
                    MetaTestGrade = testResult?.Grade,
                    MetaTestScore = testResult?.Score,
                    MetaStudentEvaluationStatus = evalRosterList?.FirstOrDefault()?.getStatus()
                };

                metaILAsVM.Add(metaILAVM);
            }

            return metaILAsVM;
        }


        public async Task<List<MetaILA_MemberIDPVM>> GetLinkedMetaILAsMembersByMetaILAIdForIDPAsync(int metaILAId, int empId)
        {
            var metaILAMembers = (await _metaILALinkDomainService.GetMetaILAMembersByMetaILAIdAsync(metaILAId)).OrderBy(s => s.SequenceNumber);
            var distinctILAIds = metaILAMembers.Select(m => m.ILAID).Distinct().ToList();
            var classSchedules = (await _classScheduleEmployeeDomainService.GetClassSchedulesForILAsAndEmployeeAsync(distinctILAIds,empId));
            var MetaILA_MemberIDPVMList = new List<MetaILA_MemberIDPVM>();
            foreach(var ilaMember in metaILAMembers)
            {
                var metaEmpFulFillmentReq = await _metaILAEmpFullFillmentDomainService.GetWithClassScheduleEmployeeByMemberAndEmpIdAsync(ilaMember.Id, empId);
                var classSchedule = classSchedules.Where(c => c.ILAID.Value == ilaMember.ILAID && c.InstructorId == null && c.LocationId == null).OrderByDescending(x => x.Id)?.FirstOrDefault();
                var metaILAMemberVM = new MetaILA_MemberIDPVM()
                {
                    ILAId = ilaMember.ILAID,
                    ILANumber = ilaMember.ILA?.Number,
                    ILAName = ilaMember.ILA?.Name,
                    StartDate = classSchedule?.StartDateTime,
                    CompletedDate = metaEmpFulFillmentReq?.Where(m=>m.FufilledBy_ClassScheduleEmployee.ClassScheduleId == classSchedule?.Id).FirstOrDefault()?.FufilledBy_ClassScheduleEmployee?.CompletionDate,
                    Grade = metaEmpFulFillmentReq?.Where(m => m.FufilledBy_ClassScheduleEmployee.ClassScheduleId == classSchedule?.Id).FirstOrDefault()?.FufilledBy_ClassScheduleEmployee?.FinalGrade,
                    Score = metaEmpFulFillmentReq?.Where(m => m.FufilledBy_ClassScheduleEmployee.ClassScheduleId == classSchedule?.Id).FirstOrDefault()?.FufilledBy_ClassScheduleEmployee?.FinalScore,
                    IsILACompleted = metaEmpFulFillmentReq?.Where(m => m.FufilledBy_ClassScheduleEmployee.ClassScheduleId == classSchedule?.Id).FirstOrDefault()?.FufilledBy_ClassScheduleEmployee?.IsComplete ?? false
                };
                MetaILA_MemberIDPVMList.Add(metaILAMemberVM);
            }
            return MetaILA_MemberIDPVMList.ToList();
        }
    }
}
