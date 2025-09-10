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
using QTD2.Infrastructure.Model.SimulatorScenario;
using QTD2.Infrastructure.Model.SimulatorScenario_EnablingObjectives_Link;
using QTD2.Infrastructure.Model.SimulatorScenarioILA_Link;
using QTD2.Infrastructure.Model.SimulatorScenarioPositon_Link;
using QTD2.Infrastructure.Model.SimulatorScenarioTaskObjectives_Link;
using ISimulatorScenarioDomainService = QTD2.Domain.Interfaces.Service.Core.ISimulatorScenarioService;
using ISimulatorScenarioPositionDomainService = QTD2.Domain.Interfaces.Service.Core.ISimulatorScenario_PositionService;
using ISimulatorScenarioTaskDomainService = QTD2.Domain.Interfaces.Service.Core.ISimulatorScenario_TaskService;
using ISimulatorScenarioEnablingObjectiveDomainService = QTD2.Domain.Interfaces.Service.Core.ISimulatorScenario_EnablingObjectiveService;
using ISimulatorScenarioProcedureDomainService = QTD2.Domain.Interfaces.Service.Core.ISimulatorScenario_ProcedureService;
using ISimulatorScenarioTaskCriteriaDomainService = QTD2.Domain.Interfaces.Service.Core.ISimulatorScenario_Task_CriteriaService;
using ISimulatorScenarioEventAndScriptDomainService = QTD2.Domain.Interfaces.Service.Core.ISimulatorScenario_EventAndScriptService;
using ISimulatorScenarioILADomainService = QTD2.Domain.Interfaces.Service.Core.ISimulatorScenario_ILAService;
using ISimulatorScenarioPrerequisiteDomainService = QTD2.Domain.Interfaces.Service.Core.ISimulatorScenario_PrerequisiteService;
using ISimulatorScenarioCollaboratorDomainService = QTD2.Domain.Interfaces.Service.Core.ISimulatorScenario_CollaboratorService;
using IPositionDomainService = QTD2.Domain.Interfaces.Service.Core.IPositionService;
using ITaskDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskService;
using IEnablingObjectiveDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjectiveService;
using IQTDUserDomainService = QTD2.Domain.Interfaces.Service.Core.IQTDUserService;
using ISimulatorScenarioCollaboratorPermissionDomainService = QTD2.Domain.Interfaces.Service.Core.ISimulatorScenario_CollaboratorPermissionService;
using IPersonDomainService = QTD2.Domain.Interfaces.Service.Core.IPersonService;
using QTD2.Infrastructure.Model.SimulatorScenario_CollaboratorPermission;
using QTD2.Domain.Exceptions;

namespace QTD2.Application.Services.Shared
{
    public class SimulatorScenarioService : ISimulatorScenarioService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<SimulatorScenarioService> _localizer;
        private readonly ISimulatorScenarioDomainService _simulatorScenarioService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IILAService _ilaService;
        private readonly IPositionService _positionService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly ITaskService _taskService;
        private readonly IPositionDomainService _positionDomainService;
        private readonly ITaskDomainService _taskDomainService;
        private readonly IEnablingObjectiveDomainService _enablingObjectiveDomainService;
        private readonly ISimulatorScenarioPositionDomainService _simulatorScenarioPositionDomainService;
        private readonly ISimulatorScenarioTaskDomainService _simulatorScenarioTaskDomainService;
        private readonly ISimulatorScenarioEnablingObjectiveDomainService _simulatorScenarioEnablingObjectiveDomainService;
        private readonly IProcedureService _procedureService;
        private readonly ISimulatorScenarioProcedureDomainService _simulatorScenarioProcedureDomainService;
        private readonly ISimulatorScenarioTaskCriteriaDomainService _simulatorScenarioTaskCriteriaDomainService;
        private readonly ISimulatorScenarioEventAndScriptDomainService _simulatorScenarioEventAndScriptDomainService;
        private readonly ISimulatorScenarioILADomainService _simulatorScenarioILADomainService;
        private readonly ISimulatorScenarioPrerequisiteDomainService _simulatorScenarioPrerequisiteDomainService;
        private readonly ISimulatorScenario_CollaboratorService _simulatorScenario_CollaboratorService;
        private readonly ISimulatorScenarioCollaboratorDomainService _simulatorScenarioCollaboratorDomainService;
        private readonly IQTDUserDomainService _qtdUserDomainService;
        private readonly ISimulatorScenarioCollaboratorPermissionDomainService _simulatorScenarioCollaboratorPermissionDomainService;
        private readonly IPersonDomainService _personDomainService;

        public SimulatorScenarioService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<SimulatorScenarioService> localizer,
            ISimulatorScenarioDomainService simulatorScenarioService, UserManager<AppUser> userManager, IILAService ilaService, IPositionService positionService,
            IEnablingObjectiveService enablingObjectiveService, ITaskService taskService,
            ISimulatorScenarioPositionDomainService simulatorScenarioPositionDomainService, ISimulatorScenarioTaskDomainService simulatorScenarioTaskDomainService,
            ISimulatorScenarioEnablingObjectiveDomainService simulatorScenarioEnablingObjectiveDomainService, ISimulatorScenarioProcedureDomainService simulatorScenarioProcedureDomainService,
            IProcedureService procedureService, ISimulatorScenarioTaskCriteriaDomainService simulatorScenarioTaskCriteriaDomainService,
            ISimulatorScenarioEventAndScriptDomainService simulatorScenarioEventAndScriptDomainService, ISimulatorScenarioILADomainService simulatorScenarioILADomainService,
            ISimulatorScenarioPrerequisiteDomainService simulatorScenarioPrerequisiteDomainService, ISimulatorScenario_CollaboratorService simulatorScenario_CollaboratorService,
            ISimulatorScenarioCollaboratorDomainService simulatorScenarioCollaboratorDomainService,
             IPositionDomainService positionDomainService, ITaskDomainService taskDomainService,
             IEnablingObjectiveDomainService enablingObjectiveDomainService, IQTDUserDomainService qtdUserDomainService, ISimulatorScenarioCollaboratorPermissionDomainService simulatorScenarioCollaboratorPermissionDomainService,
             IPersonDomainService personDomainService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _simulatorScenarioService = simulatorScenarioService;
            _userManager = userManager;
            _ilaService = ilaService;
            _simulatorScenarioPositionDomainService = simulatorScenarioPositionDomainService;
            _simulatorScenarioTaskDomainService = simulatorScenarioTaskDomainService;
            _simulatorScenarioEnablingObjectiveDomainService = simulatorScenarioEnablingObjectiveDomainService;
            _procedureService = procedureService;
            _simulatorScenarioProcedureDomainService = simulatorScenarioProcedureDomainService;
            _simulatorScenarioTaskCriteriaDomainService = simulatorScenarioTaskCriteriaDomainService;
            _simulatorScenarioEventAndScriptDomainService = simulatorScenarioEventAndScriptDomainService;
            _simulatorScenarioILADomainService = simulatorScenarioILADomainService;
            _simulatorScenarioPrerequisiteDomainService = simulatorScenarioPrerequisiteDomainService;
            _simulatorScenario_CollaboratorService = simulatorScenario_CollaboratorService;
            _simulatorScenarioCollaboratorDomainService = simulatorScenarioCollaboratorDomainService;
            _positionService = positionService;
            _taskService = taskService;
            _enablingObjectiveService = enablingObjectiveService;
            _positionDomainService = positionDomainService;
            _taskDomainService = taskDomainService;
            _enablingObjectiveDomainService = enablingObjectiveDomainService;
            _qtdUserDomainService = qtdUserDomainService;
            _simulatorScenarioCollaboratorPermissionDomainService = simulatorScenarioCollaboratorPermissionDomainService;
            _personDomainService = personDomainService;
        }



        public async Task<SimulatorScenarioOverview_VM> GetOverviewAsync()
        {
            var person = await _personDomainService.GetPersonByUserName(_httpContextAccessor.HttpContext?.User.Identity?.Name);
            var simScenarios = await _simulatorScenarioService.AllWithIncludeAsync(new string[] { "ILAs.ILA", "Positions.Position", "Collaborators.User.Person", "Collaborators.Permission" });
            var simScenarioOverview = new SimulatorScenarioOverview_VM();
            if (simScenarios != null)
            {
                foreach (var scenario in simScenarios)
                {
                    var simScenarioOverview_VM = new SimulatorScenarioOverview_SimulatorScenario_VM(scenario.Id, scenario.Title, string.Join(", ", scenario.ILAs.Select(x => x.ILA.Name)), string.Join(", ", scenario.Positions.Select(x => x.Position.PositionTitle)), scenario.StatusId.ToString(), scenario.Active, scenario.DifficultyId.ToString());
                    simScenarioOverview_VM.setCollaborators(scenario.Collaborators.ToList(), person.Id);
                    simScenarioOverview.SimulatorScenarios.Add(simScenarioOverview_VM);
                }
                return simScenarioOverview;
            }
            else
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }
        }

        public async Task<SimulatorScenario_VM> CreateAsync(SimulatorScenario_VM options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            else
            {
                var person = await _personDomainService.GetPersonByUserName(_httpContextAccessor.HttpContext?.User.Identity?.Name);
                var qtduser = (await _qtdUserDomainService.FindAsync(r=>r.PersonId == person.Id)).FirstOrDefault();
                var collabPermission = (await _simulatorScenarioCollaboratorPermissionDomainService.FindAsync(r=>r.Permission == "Editor")).FirstOrDefault();
                var simScenario = new QTD2.Domain.Entities.Core.SimulatorScenario(options.DifficultyId, options.Title, options.Description, options.DurationHours, options.DurationMinutes,1);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, simScenario, AuthorizationOperations.Create);
                if (result.Succeeded)
                {
                    var userName = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
                    simScenario.Create(userName.Id);
                    if(qtduser != null && collabPermission != null)
                    {
                        simScenario.UpdateCollaborators(qtduser, collabPermission);
                    }
                    var validationResult = await _simulatorScenarioService.AddAsync(simScenario);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        return await GetAsync(simScenario.Id);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                }

            }
        }

        public async Task<SimulatorScenario_VM> GetAsync(int id)
        {
            var person = await _personDomainService.GetPersonByUserName(_httpContextAccessor.HttpContext?.User.Identity?.Name);
            var simScenario = await _simulatorScenarioService.GetAsync(id);
            if (simScenario != null)
            {
                var simScenarioVM = new SimulatorScenario_VM(simScenario.Id, simScenario.Title, simScenario.Description, simScenario.DurationHours, simScenario.DurationMinutes, simScenario.DifficultyId, simScenario.NetworkConfiguration, simScenario.LoadingConditions, simScenario.Generation, simScenario.Interchange, simScenario.OtherBaseCase, simScenario.ValidityChecks, simScenario.RolePlays, simScenario.Documentation, simScenario.RatingScaleId, simScenario.OperatingSkillsEvaluationMethod, simScenario.Notes, false, simScenario.Message, simScenario.PublishedDate, simScenario.PublishedReason);
                foreach (var simScenario_Position in simScenario.Positions)
                {
                    var simulatorScenario_Position_VM = new SimulatorScenario_Position_VM(simScenario_Position.Id, simScenario_Position.PositionID, simScenario_Position.Position.PositionTitle);
                    simScenarioVM.Positions.Add(simulatorScenario_Position_VM);
                }

                foreach (var simScenario_Task in simScenario.Tasks)
                {
                    var simulatorScenario_Task_VM = new SimulatorScenario_Task_VM(simScenario_Task.Id, simScenario_Task.TaskId, simScenario_Task.Task.FullNumber, simScenario_Task.Task.Description, "Task");
                    simScenarioVM.Tasks.Add(simulatorScenario_Task_VM);
                }

                foreach (var simScenario_EnablingObjective in simScenario.EnablingObjectives)
                {
                    var simulatorScenario_EnablingObjective_VM = new SimulatorScenario_EnablingObjective_VM(simScenario_EnablingObjective.Id, simScenario_EnablingObjective.EnablingObjectiveID, simScenario_EnablingObjective.EnablingObjective.FullNumber, simScenario_EnablingObjective.EnablingObjective.Description, "EO");
                    simScenarioVM.EnablingObjectives.Add(simulatorScenario_EnablingObjective_VM);
                }
                foreach (var simScenario_Procedure in simScenario.Procedures)
                {
                    var simulatorScenario_Procedure_VM = new SimulatorScenario_Procedure_VM(simScenario_Procedure.Id, simScenario_Procedure.ProcedureId, simScenario_Procedure.Procedure.Number, simScenario_Procedure.Procedure.Description, simScenario_Procedure.Procedure.Title);
                    simScenarioVM.Procedures.Add(simulatorScenario_Procedure_VM);
                }
                foreach (var simScenario_TaskCriteria in simScenario.TaskCriterias)
                {
                    var simulatorScenario_Task_Criteria_VM = new SimulatorScenario_Task_Criteria_VM(simScenario_TaskCriteria.Id, simScenario_TaskCriteria.TaskId, simScenario_TaskCriteria.Task.FullNumber, simScenario_TaskCriteria.Task.Description, simScenario_TaskCriteria.Criteria);
                    simScenarioVM.TaskCriterias.Add(simulatorScenario_Task_Criteria_VM);
                }
                foreach (var simScenario_EventsAndScript in simScenario.EventsAndScritps)
                {
                    var simulatorScenario_SimulatorScenarioEventAndScript_VM = new SimulatorScenario_SimulatorScenarioEventAndScript_VM(simScenario_EventsAndScript.Id, simScenario_EventsAndScript.Order, simScenario_EventsAndScript.Title, simScenario_EventsAndScript.Description);
                    simScenarioVM.EventsAndScripts.Add(simulatorScenario_SimulatorScenarioEventAndScript_VM);
                }
                foreach (var simScenario_ILA in simScenario.ILAs)
                {
                    var simulatorScenario_ILA_VM = new SimulatorScenario_ILA_VM(simScenario_ILA.Id, simScenario_ILA.ILAID, simScenario_ILA.ILA.Number, simScenario_ILA.ILA.Description);
                    simScenarioVM.ILAs.Add(simulatorScenario_ILA_VM);
                }
                foreach (var simScenario_Prerequisite in simScenario.Prerequisites)
                {
                    var simulatorScenario_Prerequisite_VM = new SimulatorScenario_Prerequisite_VM(simScenario_Prerequisite.Id, simScenario_Prerequisite.PrerequisiteId, simScenario_Prerequisite.Prerequisite.Number, simScenario_Prerequisite.Prerequisite.Description);
                    simScenarioVM.Prerequisites.Add(simulatorScenario_Prerequisite_VM);
                }
                foreach (var simScenario_Collaborator in simScenario.Collaborators)
                {
                    var simulatorScenario_Collaborator_VM = new SimulatorScenario_Collaborator_VM(simScenario_Collaborator.Id,simScenario_Collaborator.UserId, $"{simScenario_Collaborator.User.Person.FirstName} {simScenario_Collaborator.User.Person.LastName}", simScenario_Collaborator.User.Person.Username, simScenario_Collaborator.PermissionId);
                    simScenarioVM.Collaborators.Add(simulatorScenario_Collaborator_VM);
                }
                var currentUser = simScenario.Collaborators.FirstOrDefault(x => x.User.PersonId == person.Id);
                simScenarioVM.CurrentUserPermissions = currentUser != null ? new SimulatorScenario_CollaboratorPermissions_VM(currentUser.Permission.Id, currentUser.Permission.Permission) : null;
                return simScenarioVM;
            }
            else
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }
        }

        public async Task<SimulatorScenario_VM> UpdateAsync(int id, SimulatorScenario_VM options)
        {
            var simScenario = await _simulatorScenarioService.GetAsync(id);
            if (simScenario == null)
            {
                throw new ArgumentNullException();
            }
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, simScenario, DIFSurveyOperations.Update);

            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException();
            }
            if (!String.IsNullOrEmpty(options.Title))
            {
                simScenario.Title = options.Title;
            }
            simScenario.Description = options.Description;
            simScenario.DifficultyId = options.DifficultyId;
            simScenario.DurationHours = options.DurationHours;
            simScenario.DurationMinutes = options.DurationMinutes;
            simScenario.RatingScaleId = options.RatingScaleId;
            simScenario.OperatingSkillsEvaluationMethod = options.OperatingSkillsEvaluationMethod;
            simScenario.Notes = options.Notes;
            simScenario.Message = options.Message;
            simScenario = UpdateScenarioSpecifications(simScenario, options);
            var validationResult = await _simulatorScenarioService.UpdateAsync(simScenario);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                return await GetAsync(simScenario.Id);
            }
        }

        public SimulatorScenario UpdateScenarioSpecifications(SimulatorScenario simScenario, SimulatorScenario_VM options)
        {
            simScenario.NetworkConfiguration = options.NetworkConfiguration;
            simScenario.LoadingConditions = options.LoadingConditions;
            simScenario.Generation = options.Generation;
            simScenario.Interchange = options.Interchange;
            simScenario.OtherBaseCase = options.OtherBaseCase;
            simScenario.ValidityChecks = options.ValidityChecks;
            simScenario.RolePlays = options.RolePlays;
            simScenario.Documentation = options.Documentation;
            return simScenario;
        }

        public async System.Threading.Tasks.Task<object> CopyAsync(int id)
        {
            var createdBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            var obj = await _simulatorScenarioService.GetForCopy(id);
            obj = obj.Copy<SimulatorScenario>(createdBy);

            var result = await _simulatorScenarioService.AddAsync(obj);
            if (!result.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', result.Errors));
            }
            foreach(var eventScript in obj.EventsAndScritps)
            {
                eventScript.Criterias.ForEach(x => x.CriteriaId = obj.TaskCriterias.FirstOrDefault(y => y.TaskId == x.Criteria.TaskId)?.Id ?? 0);
            }
            result = await _simulatorScenarioService.UpdateAsync(obj);
            if (!result.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', result.Errors));
            }
            var idResult = new
            {
                id = obj.Id
            };

            return idResult;
        }

        public async Task<List<SimulatorScenario_Position_VM>> UpdatePositionsAsync(int id, SimulatorScenario_UpdatePositions_VM options)
        {
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            var simScenario = await _simulatorScenarioService.GetWithIncludeAsync(id, new[] { "Positions" });
            var currentPositionLinks = simScenario.Positions.ToList();
            var optionPositions = options.Positions.ToList();
            if (simScenario == null)
            {
                throw new ArgumentNullException();
            }
            var simScenario_result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, simScenario, SimulatorScenarioOperations.Update);
            foreach (var scenario in currentPositionLinks)
            {
                var position = options.Positions.FirstOrDefault(x => x.PositionId == scenario.PositionID);

                if (position == null)
                {
                    scenario.Delete();
                    scenario.Modify(userName);
                }
            }
            foreach (var option in optionPositions)
            {
                var positionData = currentPositionLinks.FirstOrDefault(x => x.PositionID == option.PositionId);
                if (positionData == null)
                {
                    var data = await _positionDomainService.GetAsync(option.PositionId);
                    var scenaior = simScenario.UpdatePositions(data);
                    scenaior.Create(userName);
                }
            }
            var validationResult = await _simulatorScenarioService.UpdateAsync(simScenario);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                return (await _simulatorScenarioPositionDomainService.GetSimulatorScenarioBySimulatorIdAsync(simScenario.Id)).Select(x => MapSimulatorScenarioToSimulatorScenario_Position_VM(x)).ToList();
            }
        }

        public async Task<SimulatorScenario_TasksResponseVM> UpdateTaskAsync(int id, SimulatorScenario_UpdateTasks_VM options)
        {
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            string[] includes = new string[] { "Tasks" };
            includes = options.IncludeEnablingObjectives ? includes.Append("EnablingObjectives").ToArray() : includes;
            includes = options.IncludeProcedures ? includes.Append("Procedures").ToArray() : includes;
            var simScenario = await _simulatorScenarioService.GetWithIncludeAsync(id,includes);
            var currentTasksLinks = simScenario.Tasks.ToList();
            var simulatorScenario_TasksResponseVM = new SimulatorScenario_TasksResponseVM();
            var optionTasks = options.Tasks.ToList();
            if (simScenario == null)
            {
                throw new ArgumentNullException();
            }
            var simScenario_result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, simScenario, SimulatorScenarioOperations.Update);
            foreach (var scenario in currentTasksLinks)
            {
                var task = options.Tasks.FirstOrDefault(x => x.TaskId == scenario.TaskId);

                if (task == null)
                {
                    scenario.Delete();
                    scenario.Modify(userName);
                }
            }
            foreach (var option in optionTasks)
            {
                var taskData = currentTasksLinks.FirstOrDefault(x => x.TaskId == option.TaskId);
                if (taskData == null)
                {
                    var data = await _taskDomainService.GetAsync(option.TaskId);
                    var scenaior = simScenario.UpdateTasks(data);
                    scenaior.Create(userName);
                    if (options.IncludeProcedures)
                    {
                        var proceduresLinkedToTasks = await _taskService.GetLinkedProceduresAsync(option.TaskId);
                        foreach (var procedure in proceduresLinkedToTasks)
                        {
                            if (!simScenario.Procedures.Any(x => x.ProcedureId == procedure.Id))
                            {
                                var procedureLink = simScenario.UpdateProcedures(procedure);
                                procedureLink.Create(userName);
                            }
                        }
                    }
                    if (options.IncludeEnablingObjectives)
                    {
                        var eosLinkedToTasks = await _taskService.GetLinkedEnablingObjectivesAsync(option.TaskId);
                        foreach (var eo in eosLinkedToTasks)
                        {
                            if (!simScenario.EnablingObjectives.Any(x => x.EnablingObjectiveID == eo.Id))
                            {
                                var eoLink = simScenario.UpdateEnablingObjectives(eo);
                                eoLink.Create(userName);
                            }
                        }
                    }
                }
            }
            var validationResult = await _simulatorScenarioService.UpdateAsync(simScenario);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                if (options.IncludeProcedures)
                {
                    simulatorScenario_TasksResponseVM.SimulatorScenarioProcedureVMs = (await _simulatorScenarioProcedureDomainService.GetProcedureBySimulatorIdAsync(simScenario.Id)).Select(x => MapSimulatorScenarioToSimulatorScenario_Procedure_VM(x)).ToList();
                }
                if (options.IncludeEnablingObjectives)
                {
                    simulatorScenario_TasksResponseVM.SimulatorScenarioEnablingObjectiveVMs = (await _simulatorScenarioEnablingObjectiveDomainService.GetEnablingObjectiveBySimulatorIdAsync(simScenario.Id)).Select(x => MapSimulatorScenarioToSimulatorScenario_EnablingObjectives_VM(x)).ToList();
                }
                simulatorScenario_TasksResponseVM.SimulatorScenarioTaskVMs = (await _simulatorScenarioTaskDomainService.GetTasksBySimulatorIdAsync(simScenario.Id)).Select(x => MapSimulatorScenarioToSimulatorScenario_Task_VM(x)).ToList();
                return simulatorScenario_TasksResponseVM;
            }

        }

        public async System.Threading.Tasks.Task DeleteSimScenarioById(int id)
        {
            var simulatorScenario = await _simulatorScenarioService.GetAsync(id);
            if (simulatorScenario == null)
            {
                throw new ArgumentNullException();
            }
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, simulatorScenario, AuthorizationOperations.Update);
            if (result.Succeeded)
            {
                simulatorScenario.Delete();
                var validationResult = await _simulatorScenarioService.UpdateAsync(simulatorScenario);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public async Task<List<SimulatorScenario_EnablingObjective_VM>> UpdateEnablingObjectivesAsync(int id, SimulatorScenario_UpdateEnablingObjectives_VM options)
        {
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            var simScenario = await _simulatorScenarioService.GetWithIncludeAsync(id, new[] { "EnablingObjectives" });
            var currentEOsLinks = simScenario.EnablingObjectives.ToList();
            var optionEos = options.EnablingObjectives.ToList();
            if (simScenario == null)
            {
                throw new ArgumentNullException();
            }
            var simScenario_result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, simScenario, SimulatorScenarioOperations.Update);
            foreach (var scenario in currentEOsLinks)
            {
                var objective = options.EnablingObjectives.FirstOrDefault(x => x.EnablingObjectiveId == scenario.EnablingObjectiveID);

                if (objective == null)
                {
                    scenario.Delete();
                    scenario.Modify(userName);
                }
            }
            foreach (var option in optionEos)
            {
                var eoData = currentEOsLinks.FirstOrDefault(x => x.EnablingObjectiveID == option.EnablingObjectiveId);
                if (eoData == null)
                {
                    var data = await _enablingObjectiveDomainService.GetAsync(option.EnablingObjectiveId);
                    var scenaior = simScenario.UpdateEnablingObjectives(data);
                    scenaior.Create(userName);
                }
            }
            var validationResult = await _simulatorScenarioService.UpdateAsync(simScenario);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            {
                return (await _simulatorScenarioEnablingObjectiveDomainService.GetEnablingObjectiveBySimulatorIdAsync(simScenario.Id)).Select(x => MapSimulatorScenarioToSimulatorScenario_EnablingObjectives_VM(x)).ToList();
            }

        }

        public async Task<List<SimulatorScenario_Procedure_VM>> UpdateProceduresAsync(int id, SimulatorScenario_UpdateProcedures_VM options)
        {
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            var simScenario = await _simulatorScenarioService.GetWithIncludeAsync(id, new[] { "Procedures" });
            var currentProceduresLinks = simScenario.Procedures.ToList();
            var optionProcedures = options.Procedures.ToList();
            if (simScenario == null)
            {
                throw new ArgumentNullException();
            }
            var simScenario_result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, simScenario, SimulatorScenarioOperations.Update);
            foreach (var scenario in currentProceduresLinks)
            {
                var objective = options.Procedures.FirstOrDefault(x => x.ProcedureId == scenario.ProcedureId);

                if (objective == null)
                {
                    scenario.Delete();
                    scenario.Modify(userName);
                }
            }

            foreach (var item in optionProcedures)
            {
                var eoData = currentProceduresLinks.FirstOrDefault(x => x.ProcedureId == item.ProcedureId);
                if (eoData == null)
                {
                    var data = await _procedureService.GetAsync(item.ProcedureId);
                    var scenaior = simScenario.UpdateProcedures(data);
                    scenaior.Create(userName);
                }
            }

            var validationResult = await _simulatorScenarioService.UpdateAsync(simScenario);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            {
                return (await _simulatorScenarioProcedureDomainService.GetProcedureBySimulatorIdAsync(simScenario.Id)).Select(x => MapSimulatorScenarioToSimulatorScenario_Procedure_VM(x)).ToList();
            }

        }

        public async Task<List<SimulatorScenario_Task_Criteria_By_Position_VM>> GetTaskCriteriasForPositionAsync(int id, int positionId)
        {
            var simScenario = await _simulatorScenarioService.GetWithIncludeAsync(id, new[] { "Tasks", "TaskCriterias.Task" });
            if (simScenario == null)
            {
                throw new ArgumentNullException($"SimulatorScenario with id {id} not found.");
            }

            var position = (await _positionDomainService.FindWithIncludeAsync(k => k.Id == positionId, new[] { "Position_Tasks.Task.SubdutyArea.DutyArea" })).FirstOrDefault();
            if (position == null)
            {
                throw new ArgumentNullException($"Position with id {positionId} not found.");
            }
            var scenarioTaskIds = simScenario.Tasks.Select(t => t.TaskId);
            var simLinkTasks = position.Position_Tasks.Where(x => scenarioTaskIds.Contains(x.TaskId)).Select(x => x.Task).ToList();
            var taskCriteriasForPosition = new List<SimulatorScenario_Task_Criteria_By_Position_VM>();
            foreach (var task in simLinkTasks)
            {
                var taskCriteriaVM = new SimulatorScenario_Task_Criteria_By_Position_VM(
                    null, task.Id,position.PositionAbbreviation, task.getFullNumber(), task.Description, task.Criteria
                );
                taskCriteriasForPosition.Add(taskCriteriaVM);
            }
            if (simScenario.TaskCriterias != null)
            {
                foreach (var task in simScenario.TaskCriterias)
                {
                    var taskCriteria = taskCriteriasForPosition.Where(r => r.TaskId == task.TaskId).FirstOrDefault();
                    if (taskCriteria != null)
                    {
                        taskCriteria.Id = task.Id;
                        taskCriteria.Criteria = task.Criteria;
                    }
                }
            }

            return taskCriteriasForPosition;
        }

        public async Task<List<SimulatorScenario_Task_Criteria_By_Position_VM>> GetAllTaskCriteriasForPositionAsync(int id)
        {
            var simScenario = await _simulatorScenarioService.GetWithIncludeAsync(id, new[] { "Tasks","TaskCriterias.Task.SubdutyArea.DutyArea", "Positions" });

            var taskCriteriaList = new List<SimulatorScenario_Task_Criteria_By_Position_VM>();
            if (simScenario != null)
            {
                var distinctTasks = (await _positionDomainService.GetPositionTasksByIdAsync(simScenario.Positions.Select(x => x.PositionID).ToList())).SelectMany(x => x.Position_Tasks.Select(y => y.Task)).Distinct();
                var scenarioTaskIds = simScenario.Tasks.Select(t => t.TaskId);
                var simLinkTasks = distinctTasks.Where(x => scenarioTaskIds.Contains(x.Id)).ToList();
                foreach (var distinctTask in simLinkTasks)
                {
                    var taskCriteriaVM = new SimulatorScenario_Task_Criteria_By_Position_VM(
                        null, distinctTask.Id,distinctTask.Position_Tasks.Select(x => x.Position.PositionAbbreviation).FirstOrDefault(), distinctTask.getFullNumber(), distinctTask.Description, distinctTask.Criteria
                    );
                    taskCriteriaList.Add(taskCriteriaVM);
                }
                foreach (var taskCriteria in simScenario.TaskCriterias)
                {
                    var taskCriteriaVM = taskCriteriaList.Where(r => r.TaskId == taskCriteria.TaskId).FirstOrDefault();
                    if (taskCriteriaVM != null)
                    {
                        taskCriteriaVM.Id = taskCriteria.Id;
                        taskCriteriaVM.Criteria = taskCriteria.Criteria;
                    }
                }
                return taskCriteriaList;
            }
            else
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }
        }

        public SimulatorScenario_Position_VM MapSimulatorScenarioToSimulatorScenario_Position_VM(SimulatorScenario_Position simulatorScenario_Position)
        {
            var simulatorScenario_Position_VM = new SimulatorScenario_Position_VM(simulatorScenario_Position.Id, simulatorScenario_Position.PositionID, simulatorScenario_Position.Position.PositionTitle);
            return simulatorScenario_Position_VM;
        }

        public SimulatorScenario_Task_VM MapSimulatorScenarioToSimulatorScenario_Task_VM(SimulatorScenario_Task simulatorScenario_Task)
        {
            var simulatorScenario_Task_VM = new SimulatorScenario_Task_VM(simulatorScenario_Task.Id, simulatorScenario_Task.TaskId, simulatorScenario_Task.Task.FullNumber, simulatorScenario_Task.Task.Description, "Task");
            return simulatorScenario_Task_VM;
        }

        public SimulatorScenario_EnablingObjective_VM MapSimulatorScenarioToSimulatorScenario_EnablingObjectives_VM(SimulatorScenario_EnablingObjective simulatorScenario_EnablingObjective)
        {
            var simulatorScenario_EnablingObjective_VM = new SimulatorScenario_EnablingObjective_VM(simulatorScenario_EnablingObjective.Id, simulatorScenario_EnablingObjective.EnablingObjectiveID, simulatorScenario_EnablingObjective.EnablingObjective.FullNumber, simulatorScenario_EnablingObjective.EnablingObjective.Description, "EO");
            return simulatorScenario_EnablingObjective_VM;
        }

        public SimulatorScenario_Procedure_VM MapSimulatorScenarioToSimulatorScenario_Procedure_VM(SimulatorScenario_Procedure simulatorScenario_Procedure)
        {
            var simulatorScenario_Procedure_VM = new SimulatorScenario_Procedure_VM(simulatorScenario_Procedure.Id, simulatorScenario_Procedure.ProcedureId, simulatorScenario_Procedure.Procedure.Number, simulatorScenario_Procedure.Procedure.Description, simulatorScenario_Procedure.Procedure.Title);
            return simulatorScenario_Procedure_VM;
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var simulatorScenario = await _simulatorScenarioService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, simulatorScenario, AuthorizationOperations.Update);

            if (result.Succeeded)
            {
                simulatorScenario.Activate();
                var validationResult = await _simulatorScenarioService.UpdateAsync(simulatorScenario);
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

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            var simulatorScenario = await _simulatorScenarioService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, simulatorScenario, AuthorizationOperations.Update);

            if (result.Succeeded)
            {
                simulatorScenario.Deactivate();
                var validationResult = await _simulatorScenarioService.UpdateAsync(simulatorScenario);
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

        public async Task<SimulatorScenario_Task_Criteria_VM> CreateTaskCriteriaAsync(int id, SimulatorScenario_Task_Criteria_VM options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            else
            {
                var simScenario_TaskCriteria = new QTD2.Domain.Entities.Core.SimulatorScenario_Task_Criteria(id, options.TaskId, options.Criteria);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, simScenario_TaskCriteria, AuthorizationOperations.Create);
                if (result.Succeeded)
                {
                    var userName = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
                    simScenario_TaskCriteria.Create(userName.Id);
                    var validationResult = await _simulatorScenarioTaskCriteriaDomainService.AddAsync(simScenario_TaskCriteria);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        options.Id = simScenario_TaskCriteria.Id;
                        return options;
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                }

            }
        }

        public async Task<SimulatorScenario_Task_Criteria_VM> UpdateTaskCriteriaAsync(int id, int simulatorScenarioTaskCriteriaId, SimulatorScenario_Task_Criteria_VM options)
        {
            var simScenario = (await _simulatorScenarioService.FindWithIncludeAsync(r => r.Id == id, new[] { "TaskCriterias" })).FirstOrDefault();
            if (simScenario == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            else
            {
                var simScenarioTaskCriteria = simScenario.TaskCriterias.FirstOrDefault(k => k.Id == simulatorScenarioTaskCriteriaId && k.TaskId == options.TaskId);
                if (simScenarioTaskCriteria != null)
                {
                    simScenarioTaskCriteria.UpdateCriteria(options.Criteria);
                    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, simScenarioTaskCriteria, AuthorizationOperations.Update);
                    if (result.Succeeded)
                    {
                        var userName = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
                        simScenarioTaskCriteria.Modify(userName.Id);
                        var validationResult = await _simulatorScenarioTaskCriteriaDomainService.UpdateAsync(simScenarioTaskCriteria);
                        if (!validationResult.IsValid)
                        {
                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                        }
                        else
                        {
                            return options;
                        }
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                    }
                }
                else
                {
                    throw new InvalidOperationException("SimulatorScenarioTaskCriteria not found.");
                }
            }
        }

        public async System.Threading.Tasks.Task DeleteTaskCriteriaAsync(int id, int simulatorScenarioTaskCriteriaId)
        {
            var simulatorScenario = await _simulatorScenarioService.GetWithIncludeAsync(id, new[] { "TaskCriterias" });
            if (simulatorScenario == null)
            {
                throw new ArgumentNullException();
            }
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, simulatorScenario, AuthorizationOperations.Delete);
            if (result.Succeeded)
            {
                var simScenarioTaskCriteria = simulatorScenario.TaskCriterias.FirstOrDefault(k => k.Id == simulatorScenarioTaskCriteriaId);
                simScenarioTaskCriteria.Delete();
                var validationResult = await _simulatorScenarioTaskCriteriaDomainService.UpdateAsync(simScenarioTaskCriteria);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException();
            }

        }

        public async Task<SimulatorScenario_EventAndScript_VM> CreateEventAndScriptAsync(int id, SimulatorScenario_EventAndScript_VM options)
        {
            var simScenario = await _simulatorScenarioService.GetWithIncludeAsync(id, new[] { "EventsAndScritps" });
            if (simScenario == null || options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            else
            {
                if (simScenario.EventsAndScritps.Count() == 0)
                {
                    options.Order = 1;
                }
                else
                {
                    var maxOrder = (await _simulatorScenarioEventAndScriptDomainService.FindAsync(r => r.SimulatorScenarioId == id)).Max(s => s.Order);
                    options.Order = maxOrder + 1;
                }
                var simulatorScenario_EventAndScript = new QTD2.Domain.Entities.Core.SimulatorScenario_EventAndScript(id, options.Order, options.Title, options.Description, options.InitiatorId, options.Time);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, simulatorScenario_EventAndScript, AuthorizationOperations.Create);
                if (result.Succeeded)
                {
                    var userName = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
                    simulatorScenario_EventAndScript.Create(userName.Id);
                    foreach (var value in options.Criterias)
                    {
                        var eventAndScriptCriteria = new SimulatorScenario_EventAndScript_Criteria(value.Id, value.CriteriaId);
                        simulatorScenario_EventAndScript.SetCriterias(eventAndScriptCriteria);
                    }
                    var validationResult = await _simulatorScenarioEventAndScriptDomainService.AddAsync(simulatorScenario_EventAndScript);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        options.Id = simulatorScenario_EventAndScript.Id;
                        return options;
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                }

            }
        }

        public async Task<SimulatorScenario_EventAndScript_VM> GetEventAndScriptAsync(int id, int EventAndScriptId)
        {
            var simScenarios = await _simulatorScenarioService.GetWithIncludeAsync(id, new string[] { "EventsAndScritps.Criterias" });

            if (simScenarios == null)
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }
            else
            {
                var eventAndScripts = simScenarios.EventsAndScritps.Where(k => k.Id == EventAndScriptId).FirstOrDefault();
                if (eventAndScripts != null)
                {
                    var simScenarioEventandScript_VM = new SimulatorScenario_EventAndScript_VM(eventAndScripts.Id, eventAndScripts.Order, eventAndScripts.Title, eventAndScripts.Description, eventAndScripts.InitiatorId, eventAndScripts.Time);

                    foreach (var criteria in eventAndScripts.Criterias)
                    {
                        var simulatorScenario_EventAndScript_Criteria_VM = new SimulatorScenario_EventAndScript_Criteria_VM(criteria.Id, criteria.CriteriaId);
                        simScenarioEventandScript_VM.Criterias.Add(simulatorScenario_EventAndScript_Criteria_VM);
                    }
                    return simScenarioEventandScript_VM;
                }
                else
                {
                    throw new QTDServerException(_localizer["RecordNotFound"].Value);
                }
            }
        }
        public async System.Threading.Tasks.Task<SimulatorScenario_EventAndScript_VM> CopyEventAndScriptAsync(int id, int eventAndScriptId)
        {
            var createdBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            var obj = await _simulatorScenarioService.GetSimulatorScenarioWithEventsAndScriptsAsync(id);

            if (obj == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                if (obj.EventsAndScritps == null)
                {
                    throw new ArgumentNullException();
                }
                else
                {
                    var eventAndScripts = obj.EventsAndScritps.Where(k => k.Id == eventAndScriptId).FirstOrDefault();
                    var maxOrder = obj.EventsAndScritps.Max(x => x.Order);
                    var copiedEvent = eventAndScripts.Copy<SimulatorScenario_EventAndScript>(createdBy);
                    copiedEvent.Order = maxOrder + 1;
                    var result = await _simulatorScenarioEventAndScriptDomainService.AddAsync(copiedEvent);

                    if (!result.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', result.Errors));
                    }
                    else
                    {
                        var simScenarioEventAndScriptVM = await GetEventAndScriptAsync(id, copiedEvent.Id);
                        return simScenarioEventAndScriptVM;
                    }
                }
            }
        }

        public async System.Threading.Tasks.Task DeleteEventAndScriptAsync(int id, int EventAndScriptId)
        {
            var simulatorScenario = await _simulatorScenarioService.GetWithIncludeAsync(id, new[] { "EventsAndScritps" });
            if (simulatorScenario == null)
            {
                throw new ArgumentNullException();
            }
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, simulatorScenario, AuthorizationOperations.Delete);
            if (result.Succeeded)
            {
                var simScenarioEventandScript = simulatorScenario.EventsAndScritps.FirstOrDefault(k => k.Id == EventAndScriptId);
                simScenarioEventandScript.Delete();
                var validationResult = await _simulatorScenarioEventAndScriptDomainService.UpdateAsync(simScenarioEventandScript);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public async Task<SimulatorScenario_EventAndScript_VM> UpdateEventAndScriptAsync(int id, int EventAndScriptId, SimulatorScenario_EventAndScript_VM options)
        {
            var simScenario = (await _simulatorScenarioService.FindWithIncludeAsync(r => r.Id == id, new[] { "EventsAndScritps.Criterias" })).FirstOrDefault();
            if (simScenario == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            else
            {
                var simScenarioEventAndScript = simScenario.EventsAndScritps.FirstOrDefault(k => k.Id == EventAndScriptId);
                if (simScenarioEventAndScript != null)
                {
                    var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    simScenarioEventAndScript.SetDescription(options.Description);
                    simScenarioEventAndScript.SetOrder(options.Order);
                    simScenarioEventAndScript.SetTitle(options.Title);
                    simScenarioEventAndScript.SetTime(options.Time);
                    simScenarioEventAndScript.SetInitiatorId(options.InitiatorId);
                    
                    foreach (var currentCriteria in simScenarioEventAndScript.Criterias)
                    {
                        var criteria = options.Criterias.FirstOrDefault(x => x.Id == currentCriteria.Id);

                        if (criteria == null)
                        {
                            currentCriteria.Delete();
                            currentCriteria.Modify(userName);
                        }
                    }
                    foreach (var newCriteria in options.Criterias)
                    {
                        var criteria = simScenarioEventAndScript.Criterias.FirstOrDefault(x => x.CriteriaId == newCriteria.CriteriaId);
                        if (criteria == null)
                        {
                            var eventAndScriptCriteria = new SimulatorScenario_EventAndScript_Criteria(simScenarioEventAndScript.Id, newCriteria.CriteriaId);
                            eventAndScriptCriteria.Create(userName);
                            simScenarioEventAndScript.SetCriterias(eventAndScriptCriteria);
                        }
                    }
                    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, simScenarioEventAndScript, AuthorizationOperations.Update);
                    if (result.Succeeded)
                    {
                       
                        simScenarioEventAndScript.Modify(userName);
                        var validationResult = await _simulatorScenarioEventAndScriptDomainService.UpdateAsync(simScenarioEventAndScript);
                        if (!validationResult.IsValid)
                        {
                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                        }
                        else
                        {
                            return options;
                        }
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                    }
                }
                else
                {
                    throw new InvalidOperationException("SimulatorScenarioEventandScript not found.");
                }
            }
        }

        public async System.Threading.Tasks.Task UpdateEventsAndScriptsOrderAsync(int id, SimulatorScenario_UpdateEventsAndScriptsOrder_VM options)
        {
            var simScenario = (await _simulatorScenarioService.FindWithIncludeAsync(r => r.Id == id, new[] { "EventsAndScritps" })).FirstOrDefault();
            if (simScenario == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            foreach (var eventAndScriptOrder in options.EventsAndScripts)
            {
                var simScenarioEventAndScript = simScenario.EventsAndScritps.FirstOrDefault(k => k.Id == eventAndScriptOrder.EventAndScriptId);
                if (simScenarioEventAndScript != null)
                {
                    simScenarioEventAndScript.SetOrder(eventAndScriptOrder.Order);
                    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, simScenarioEventAndScript, AuthorizationOperations.Update);
                    if (result.Succeeded)
                    {
                        var userName = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
                        simScenarioEventAndScript.Modify(userName.Id);
                        var validationResult = await _simulatorScenarioEventAndScriptDomainService.UpdateAsync(simScenarioEventAndScript);
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
                else
                {
                    throw new InvalidOperationException("SimulatorScenarioEventandScript not found.");
                }
            }
        }

        public async Task<List<SimulatorScenario_ILA_VM>> UpdateILAsAsync(int id, SimulatorScenario_UpdateILAs_VM options)
        {
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            var simScenario = await _simulatorScenarioService.GetWithIncludeAsync(id, new[] { "ILAs" });
            var currentILALinks = simScenario.ILAs.ToList();
            var optionILAs = options.ILAs.ToList();

            if (simScenario == null)
            {
                throw new ArgumentNullException();
            }
            var simScenario_result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, simScenario, SimulatorScenarioOperations.Update);
            foreach (var scenario in currentILALinks)
            {
                var objective = options.ILAs.FirstOrDefault(x => x.ILAId == scenario.ILAID);

                if (objective == null)
                {
                    scenario.Delete();
                    scenario.Modify(userName);
                }
            }

            foreach (var item in optionILAs)
            {
                var ilaData = currentILALinks.FirstOrDefault(x => x.ILAID == item.ILAId);
                if (ilaData == null)
                {
                    var data = await _ilaService.GetAsync(item.ILAId);
                    var scenaior = simScenario.UpdateILAs(data);
                    scenaior.Create(userName);
                }
            }

            var validationResult = await _simulatorScenarioService.UpdateAsync(simScenario);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                return (await _simulatorScenarioILADomainService.GetILABySimulatorIdAsync(simScenario.Id)).Select(x => MapSimulatorScenarioToSimulatorScenario_ILA_VM(x)).ToList();
            }
        }

        public SimulatorScenario_ILA_VM MapSimulatorScenarioToSimulatorScenario_ILA_VM(SimulatorScenario_ILA simulatorScenario_ILA)
        {
            var simulatorScenario_ILA_VM = new SimulatorScenario_ILA_VM(simulatorScenario_ILA.Id, simulatorScenario_ILA.ILAID, simulatorScenario_ILA.ILA.Number, simulatorScenario_ILA.ILA.Description);
            return simulatorScenario_ILA_VM;
        }

        public async Task<List<SimulatorScenario_Prerequisite_VM>> UpdatePrerequisitesAsync(int id, SimulatorScenario_UpdatePrerequisites_VM options)
        {
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            var simScenario = await _simulatorScenarioService.GetWithIncludeAsync(id, new[] { "Prerequisites" });
            var currentPrerequisites = simScenario.Prerequisites.ToList();
            var optionPrerequisites = options.Prerequisites.ToList();

            if (simScenario == null)
            {
                throw new ArgumentNullException();
            }
            var simScenario_result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, simScenario, SimulatorScenarioOperations.Update);
            foreach (var scenario in currentPrerequisites)
            {
                var prerequisite = options.Prerequisites.FirstOrDefault(x => x.ILAId == scenario.PrerequisiteId);

                if (prerequisite == null)
                {
                    scenario.Delete();
                    scenario.Modify(userName);
                }
            }
            foreach (var item in optionPrerequisites)
            {
                var prerequisites = currentPrerequisites.FirstOrDefault(x => x.PrerequisiteId == item.ILAId);
                if (prerequisites == null)
                {
                    var data = await _ilaService.GetAsync(item.ILAId);
                    var scenaior = simScenario.UpdatePrerequisites(data);
                    scenaior.Create(userName);
                }
            }

            var validationResult = await _simulatorScenarioService.UpdateAsync(simScenario);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                return (await _simulatorScenarioPrerequisiteDomainService.GetPrerequisiteBySimulatorIdAsync(simScenario.Id)).Select(x => MapSimulatorScenarioToSimulatorScenario_Prerequisite_VM(x)).ToList();
            }

        }
        public SimulatorScenario_Prerequisite_VM MapSimulatorScenarioToSimulatorScenario_Prerequisite_VM(SimulatorScenario_Prerequisite simulatorScenario_Prerequisite)
        {
            var simulatorScenario_Prerequisite_VM = new SimulatorScenario_Prerequisite_VM(simulatorScenario_Prerequisite.Id, simulatorScenario_Prerequisite.PrerequisiteId, simulatorScenario_Prerequisite.Prerequisite.Number, simulatorScenario_Prerequisite.Prerequisite.Description);
            return simulatorScenario_Prerequisite_VM;
        }
        public async Task<List<SimulatorScenario_Collaborator_VM>> UpdateCollaboratorsAsync(int id, SimulatorScenario_UpdateCollaborators_VM options)
        {
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            var simScenario = await _simulatorScenarioService.GetWithIncludeAsync(id, new[] { "Collaborators" });
            var currentCollaborators = simScenario.Collaborators.ToList();
            var optionPrerequisites = options.Collaborators.ToList();

            if (simScenario == null)
            {
                throw new ArgumentNullException();
            }
            var simScenario_result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, simScenario, SimulatorScenarioOperations.Update);

            foreach (var scenario in currentCollaborators)
            {
                var collaborator = options.Collaborators.FirstOrDefault(x => x.Id == scenario.Id);

                if (collaborator == null)
                {
                    scenario.Delete();
                    scenario.Modify(userName);
                }
            }

            foreach (var item in options.Collaborators)
            {
                var collaboratordata = currentCollaborators.FirstOrDefault(x => x.UserId == item.UserId);
                if (collaboratordata == null)
                {
                    var user = await _qtdUserDomainService.GetAsync(item.UserId);
                    var permission = await _simulatorScenarioCollaboratorPermissionDomainService.GetAsync(item.CollaboratorPermissionId);
                    var scenaior = simScenario.UpdateCollaborators(user,permission);
                    scenaior.Create(userName);
                }
                else
                {
                    collaboratordata.SetPermission(item.CollaboratorPermissionId);
                }
            }

            var validationResult = await _simulatorScenarioService.UpdateAsync(simScenario);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                return (await _simulatorScenarioCollaboratorDomainService.GetCollaboratorBySimulatorIdAsync(simScenario.Id)).Select(x => MapSimulatorScenarioToSimulatorScenario_Collaborator_VM(x)).ToList();
            }
        }

        public SimulatorScenario_Collaborator_VM MapSimulatorScenarioToSimulatorScenario_Collaborator_VM(SimulatorScenario_Collaborator simulatorScenario_Collaborator)
        {
            var simulatorScenario_Collaborator_VM = new SimulatorScenario_Collaborator_VM(simulatorScenario_Collaborator.Id,simulatorScenario_Collaborator.UserId, $"{simulatorScenario_Collaborator.User.Person.FirstName} {simulatorScenario_Collaborator.User.Person.LastName}", simulatorScenario_Collaborator.User.Person.Username, simulatorScenario_Collaborator.PermissionId);
            return simulatorScenario_Collaborator_VM;
        }

        public async Task<SimulatorScenario> LinkSimulatorScenarioILA(int id, SimulatorScenarioILA_LinkOptions options)
        {
            var obj = await _simulatorScenarioService.GetWithIncludeAsync(id, new string[] { "SimulatorScenarioILA" });
            var sim = await _simulatorScenarioService.GetAsync(options.SimulatorScenarioID);
            var ila = await _ilaService.GetAsync(options.ILAID);

            if (sim == null)
            {
                throw new QTDServerException(_localizer["SimulatorScenarioNotFound"]);
            }

            if (ila == null)
            {
                throw new QTDServerException(_localizer["ILANotFound"]);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Create);
            var simResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sim, AuthorizationOperations.Create);
            var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Create);

            if (result.Succeeded && simResult.Succeeded && ilaResult.Succeeded)
            {
                //var sim_ila_link = obj.LinkSimulatorScenarioILA(ila);
                //sim_ila_link.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                //sim_ila_link.CreatedDate = DateTime.Now;

                await _simulatorScenarioService.UpdateAsync(sim);
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }

            obj = await _simulatorScenarioService.GetAsync(id);
            return null;
        }

        public async System.Threading.Tasks.Task UnLinkSimulatorScenarioILA(int id, SimulatorScenarioILA_LinkOptions options)
        {
            var obj = await _simulatorScenarioService.GetWithIncludeAsync(id, new string[] { "SimulatorScenarioILA_Links" });
            var sim = await _simulatorScenarioService.GetAsync(options.SimulatorScenarioID);
            var ila = await _ilaService.GetAsync(options.ILAID);

            if (sim == null)
            {
                throw new QTDServerException(_localizer["TestItemNotFound"]);
            }

            if (ila == null)
            {
                throw new QTDServerException(_localizer["TestNotFound"]);
            }

            //var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Create);
            var simResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sim, AuthorizationOperations.Create);
            var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, AuthorizationOperations.Create);

            if (simResult.Succeeded && ilaResult.Succeeded)
            {
                //obj.UnLinkSimulatorScenarioILA(ila);

                await _simulatorScenarioService.UpdateAsync(obj);
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<List<SimulatorScenario>> GetLinkedSimulatorScenarioILAAsync(int id)
        {
            var result = await _simulatorScenarioService.GetAsync(id);
            List<SimulatorScenario> linked_list = new List<SimulatorScenario>();
            linked_list = linked_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded).ToList();
            return linked_list;
        }

        public async Task<SimulatorScenario> LinkSimulatorScenarioPosititon(int id, SimulatorScenarioPositon_LinkOptions options)
        {
            var obj = await _simulatorScenarioService.GetWithIncludeAsync(id, new string[] { "SimulatorScenarioPositon" });
            foreach (var eoid in options.PositionIds)
            {
                var position = await _positionService.GetAsync(eoid);

                if (position == null)
                {
                    throw new QTDServerException(_localizer["RecordsNotFound"]);
                }

                var simResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

                var posResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, position, AuthorizationOperations.Read);

                if (simResult.Succeeded && posResult.Succeeded)
                {
                    //var sim_position_link = obj.LinkSimulatorScenarioPositon(position);
                    //sim_position_link.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    //sim_position_link.CreatedDate = DateTime.Now;

                    await _simulatorScenarioService.UpdateAsync(obj);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            obj = await _simulatorScenarioService.GetAsync(id);
            return null;
        }

        public async System.Threading.Tasks.Task UnLinkSimulatorScenarioPosition(int id, SimulatorScenarioPositon_LinkOptions options)
        {
            var obj = await _simulatorScenarioService.GetWithIncludeAsync(id, new string[] { "SimulatorScenarioPositon" });

            foreach (var posId in options.PositionIds)
            {
                var position = await _positionService.GetAsync(posId);

                if (position == null)
                {
                    throw new QTDServerException(_localizer["PositionNotFound"]);
                }

                var simResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

                var position_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, position, PositionOperations.Read);

                if (simResult.Succeeded && position_Result.Succeeded)
                {
                    //obj.UnLinkSimulatorScenarioPositon(position);
                    await _simulatorScenarioService.UpdateAsync(null);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async Task<List<Position>> GetLinkedSimulatorScenarioPositionAsync(int id)
        {
            //var result = await _simScenario_position_link.FindWithIncludeAsync(x => x.SimulatorScenarioID == id, new string[] { nameof(Position) });
            //List<Position> linked_list = new List<Position>();
            //linked_list.AddRange(result.Select(x => x.Position));
            //linked_list = linked_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, PositionOperations.Read).Result.Succeeded).ToList();
            //return linked_list;
            return null;
        }

        public async Task<SimulatorScenario> LinkSimulatorScenarioEO(int id, SimulatorScenario_EnablingObjectives_LinkOptions options)
        {
            var obj = await _simulatorScenarioService.GetWithIncludeAsync(id, new string[] { "SimulatorScenario_EnablingObjectives" });
            foreach (var eoid in options.EnablingObjectiveIds)
            {
                var eo = await _enablingObjectiveService.GetAsync(eoid);

                if (eo == null)
                {
                    throw new QTDServerException(_localizer["RecordsNotFound"]);
                }

                var simResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

                var eoResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, AuthorizationOperations.Read);

                if (simResult.Succeeded && eoResult.Succeeded)
                {
                    //var sim_eo_link = obj.LinkSimulatorScenarioEO(eo);
                    //sim_eo_link.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    //sim_eo_link.CreatedDate = DateTime.Now;

                    await _simulatorScenarioService.UpdateAsync(obj);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            obj = await _simulatorScenarioService.GetAsync(id);
            return null;
        }

        public async System.Threading.Tasks.Task UnLinkSimulatorScenarioEO(int id, SimulatorScenario_EnablingObjectives_LinkOptions options)
        {
            var obj = await _simulatorScenarioService.GetWithIncludeAsync(id, new string[] { "SimulatorScenario_EnablingObjectives" });

            foreach (var eoId in options.EnablingObjectiveIds)
            {
                var eo = await _enablingObjectiveService.GetAsync(eoId);

                if (eo == null)
                {
                    throw new QTDServerException(_localizer["RecordsNotFound"]);
                }

                var simResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

                var eo_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, AuthorizationOperations.Read);

                if (simResult.Succeeded && eo_Result.Succeeded)
                {
                    //obj.UnLinkSimulatorScenarioEO(eo);
                    await _simulatorScenarioService.UpdateAsync(null);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async Task<List<EnablingObjective>> GetLinkedSimulatorScenarioEOAsync(int id)
        {
            //var result = await _simScenario_eo_link.FindWithIncludeAsync(x => x.SimulatorScenarioID == id, new string[] { nameof(EnablingObjective) });
            //List<EnablingObjective> linked_list = new List<EnablingObjective>();
            //linked_list.AddRange(result.Select(x => x.EnablingObjective));
            //linked_list = linked_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, EnablingObjectiveOperations.Read).Result.Succeeded).ToList();
            //return linked_list;
            return null;
        }

        public async Task<SimulatorScenario> LinkSimulatorScenarioTask(int id, SimulatorScenarioTaskObjectives_LinkOptions options)
        {
            var obj = await _simulatorScenarioService.GetWithIncludeAsync(id, new string[] { "SimulatorScenarioTaskObjectives" });
            foreach (var eoid in options.TaskIds)
            {
                var task = await _taskService.GetAsync(eoid);

                if (task == null)
                {
                    throw new QTDServerException(_localizer["RecordsNotFound"]);
                }

                var simResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

                var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, AuthorizationOperations.Read);

                if (simResult.Succeeded && taskResult.Succeeded)
                {
                    //var sim_task_link = obj.LinkSimulatorScenarioTask(task);
                    //sim_task_link.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    //sim_task_link.CreatedDate = DateTime.Now;

                    await _simulatorScenarioService.UpdateAsync(obj);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            obj = await _simulatorScenarioService.GetAsync(id);
            return null;
        }

        public async System.Threading.Tasks.Task UnLinkSimulatorScenarioTask(int id, SimulatorScenarioTaskObjectives_LinkOptions options)
        {
            var obj = await _simulatorScenarioService.GetWithIncludeAsync(id, new string[] { "SimulatorScenarioTaskObjectives" });

            foreach (var eoId in options.TaskIds)
            {
                var task = await _taskService.GetAsync(eoId);

                if (task == null)
                {
                    throw new QTDServerException(_localizer["RecordsNotFound"]);
                }

                var simResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

                var task_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, AuthorizationOperations.Read);

                if (simResult.Succeeded && task_Result.Succeeded)
                {
                    //obj.UnLinkSimulatorScenarioTask(task);
                    await _simulatorScenarioService.UpdateAsync(obj);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async Task<List<Domain.Entities.Core.Task>> GetLinkedSimulatorScenarioTaskAsync(int id)
        {

            //var result = await _simScenario_TO_link.FindWithIncludeAsync(x => x.SimulatorScenarioID == id, new string[] { nameof(Domain.Entities.Core.Task) });
            //List<Domain.Entities.Core.Task> linked_list = new List<Domain.Entities.Core.Task>();
            //linked_list.AddRange(result.Select(x => x.Task));
            //linked_list = linked_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded).ToList();
            //return linked_list;
            return null;
        }

        public async System.Threading.Tasks.Task PublishAsync(int id, SimulatorScenario_VM options)
        {
            var simScenario = await _simulatorScenarioService.GetAsync(id);
            if (simScenario == null)
            {
                throw new ArgumentNullException();
            }
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, simScenario, SimulatorScenarioOperations.Update);

            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException();
            }
            simScenario.Publish(options.PublishedDate, options.PublishedReason);
            var validationResult = await _simulatorScenarioService.UpdateAsync(simScenario);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
        }
    }
}
