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
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.Position;
using QTD2.Infrastructure.Model.Position_SQ_Link;
using QTD2.Infrastructure.Model.Position_Task_Link;
using QTD2.Infrastructure.Model.PositionHistory;
using QTD2.Infrastructure.Model.Task;
using IPosition_Task_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IPosition_TaskService;
using ITask_Position_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_PositionService;
using ITask_TrainingGroupDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_TrainingGroupService;
using IPosition_SQ_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IPosition_SQService;
using IPosition_Employee_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IPosition_EmployeeService;
using IEmployee_Position_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeePositionService;
using IEmployeePositionServiceDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeePositionService;
using IEmployee_DomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeService;
using ITask_DomainService = QTD2.Domain.Interfaces.Service.Core.ITaskService;
using QTD2.Infrastructure.Model.Position_Employee;
using QTD2.Infrastructure.Model.Employee;
using QTD2.Application.Utils;
using ITrainingProgramDomainService = QTD2.Domain.Interfaces.Service.Core.ITrainingProgramService;
using ITrainingProgram_TypeDomainService = QTD2.Domain.Interfaces.Service.Core.ITrainingProgramTypeService;
using ITrainingProgram_ILA_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITrainingPrograms_ILA_LinkService;
using IILADomainService  = QTD2.Domain.Interfaces.Service.Core.IILAService;
using IPersonDomainService  = QTD2.Domain.Interfaces.Service.Core.IPersonService;
using Microsoft.Extensions.Options;
using QTD2.Infrastructure.Model.PositionTask;
using QTD2.Infrastructure.Model.EmployeePosition;
using RazorEngine.Compilation.ImpromptuInterface;
using QTD2.Domain.Exceptions;

namespace QTD2.Application.Services.Shared
{
    public class PositionService : IPositionService
    {
        private readonly Domain.Interfaces.Service.Core.IPositionService _positionService;
        private readonly Position _position;
        private readonly Employee _employee;
        private readonly ITaskService _taskService;
        private readonly ITask_DomainService _taskDomainService;
        private readonly Position_Task _pos_Task;
        private readonly Task_Position _task_Position;
        private readonly Task_TrainingGroup _task_TrainingGroup;
        private readonly Domain.Entities.Core.Task _task;
        private readonly IEnablingObjectiveService _eoService;
        private readonly IEmployee_DomainService _employeeService;
        private readonly Positions_SQ _pos_SQ;
        private readonly Position_Employee _pos_Emp;
        private readonly EmployeePosition _emp_Pos;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<PositionService> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPosition_Task_LinkDomainService _posTaskService;
        private readonly ITask_Position_LinkDomainService _taskPositionService;
        private readonly ITask_TrainingGroupDomainService _taskTrainingGroupService;
        private readonly IPosition_SQ_LinkDomainService _posSQService;
        private readonly IPosition_Employee_LinkDomainService _posEmpService;
        private readonly IEmployeePositionServiceDomainService _empPosService;
        private readonly ITrainingProgramDomainService _trainingProgramService;
        private readonly ITrainingProgram_TypeDomainService _trainingProgramTypeService;
        private readonly ITrainingProgram_ILA_LinkDomainService _trainingProgram_ilaService;
        private readonly IILADomainService _ilaService;
        private readonly IPersonDomainService _personService;

        public PositionService(
            Domain.Interfaces.Service.Core.IPositionService positionService,
            ITaskService taskService,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<PositionService> localizer,
            UserManager<AppUser> userManager,
            IPosition_Task_LinkDomainService posTaskService,
            IEnablingObjectiveService eoService,
            IPosition_SQ_LinkDomainService posEoService,
            IPosition_Employee_LinkDomainService posEmpService,
            IEmployee_DomainService employeeService,
            IEmployeePositionServiceDomainService empPosService,
            ITask_TrainingGroupDomainService taskTrainingGroupService,
            ITask_Position_LinkDomainService taskPositionService,
            ITask_DomainService taskDomainService,
            ITrainingProgramDomainService trainingProgramService,
            ITrainingProgram_TypeDomainService trainingProgramTypeService,
            ITrainingProgram_ILA_LinkDomainService trainingProgram_ilaService,
            IILADomainService ilaService,
            IPersonDomainService personService)
        {
            _positionService = positionService;
            _taskService = taskService;
            _pos_Task = new Position_Task();
            _task_Position = new Task_Position();
            _task = new Domain.Entities.Core.Task();
            _pos_Emp = new Position_Employee();
            _emp_Pos = new EmployeePosition();
            _task_TrainingGroup = new Task_TrainingGroup();
            _position = new Position();
            _employee = new Employee();
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _posTaskService = posTaskService;
            _eoService = eoService;
            _pos_SQ = new Positions_SQ();
            _posSQService = posEoService;
            _posEmpService = posEmpService;
            _employeeService = employeeService;
            _empPosService = empPosService;
            _taskTrainingGroupService = taskTrainingGroupService;
            _taskPositionService = taskPositionService;
            _taskDomainService = taskDomainService;
            _trainingProgramService = trainingProgramService;
            _trainingProgramTypeService = trainingProgramTypeService;
            _trainingProgram_ilaService = trainingProgram_ilaService;
            _ilaService = ilaService;
            _personService = personService;
        }

        public async Task<Position> CreateAsync(PositionCreateOptions options)
        {

            var data = options.PositionsFileUpload;
            var byteData = new byte[] { };
            if (data != null && data.Contains(","))
            {
                data = data.Substring(data.IndexOf(",") + 1);
                byteData = Convert.FromBase64String(data);
            }
            var position = new Position(options.PositionNumber, options.PositionAbbreviation, options.PositionTitle, options.PositionDescription, options.HyperLink, options.IsPublished, byteData,options.EffectiveDate,options.FileName);
            var dbPosition = (await _positionService.FindAsync(r => r.PositionTitle == position.PositionTitle && r.PositionNumber == position.PositionNumber)).FirstOrDefault();
            var posNum = (await _positionService.FindAsync(r => r.PositionNumber == position.PositionNumber)).FirstOrDefault();
            var positionExists = dbPosition != null;
            var positionNumberCheck = posNum != null;

            if (positionExists || positionNumberCheck)
            {
                throw new BadHttpRequestException(message: _localizer["Position Information Already Exists"]);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, position, PositionOperations.Create);

            if (result.Succeeded)
            {
                position.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                position.CreatedDate = DateTime.Now;
                var validationResult = await _positionService.AddAsync(position);
                if (validationResult.IsValid)
                {
                    return position;
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

        public async Task<Position> CopyPositionWithLinkages(int id, PositionCreateOptions options)
        {
            /*var procedureExists = (await _procedureService.FindAsync(r => r.Number.Trim().ToLower() == options.Number.Trim().ToLower() && r.IssuingAuthorityId == options.IssuingAuthorityId)).FirstOrDefault();
            if (procedureExists != null)
            {
                throw new BadHttpRequestException(message: _localizer["ProcedureExists"]);
            }*/
            var data = options.PositionsFileUpload;
            var byteData = new byte[] { };
            if (data != null)
            {
                /*data = data.Substring(data.IndexOf(",") + 1);
                byteData = Convert.FromBase64String(data); */
                byteData = Convert.FromBase64String(options.PositionsFileUpload);
            }

            else if (data != null && data.Contains(","))
            {
                data = data.Substring(data.IndexOf(",") + 1);
                byteData = Convert.FromBase64String(data);
            }



            var procedure = new Position(options.PositionNumber, options.PositionAbbreviation, options.PositionTitle, options.PositionDescription, options.HyperLink, options.IsPublished, byteData, options.EffectiveDate, options.FileName);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, PositionOperations.Create);
            if (result.Succeeded)
            {
                procedure.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                procedure.CreatedDate = DateTime.Now;

                var validationResult = await _positionService.AddAsync(procedure);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    var linkToCopy = await _positionService
                        .FindQueryWithIncludeAsync(x => x.Id == id,
                        new string[] {
                            nameof(_position.Position_SQs),
                            nameof(_position.Position_Tasks),
                            nameof(_position.EmployeePositions)
                        }).FirstOrDefaultAsync();
                    procedure.Position_SQs = linkToCopy.Position_SQs.DeepCopy();
                    procedure.Position_SQs.ToList().ForEach(x => x.Id = 0);
                    procedure.Position_Tasks = linkToCopy.Position_Tasks.DeepCopy();
                    procedure.Position_Tasks.ToList().ForEach(x => x.Id = 0);
                    procedure.EmployeePositions = linkToCopy.EmployeePositions.DeepCopy();
                    procedure.EmployeePositions.ToList().ForEach(x => x.Id = 0);
                    await _positionService.UpdateAsync(procedure);
                    return procedure;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<Position_StatsVM> GetSHStats()
        {
            //var linkedTasks = await _posTaskService.AllQuery().Where(x=>x.Active).Select(x => x.PositionId).ToListAsync();
            //var linkedEOs = await _posSQService.AllQuery().Select(x => x.PositionId).ToListAsync();
            //var linkedEmployeess = await _empPosService.AllQuery().Select(x => x.PositionId).ToListAsync();

            var stats = new Position_StatsVM()
            {
                PosActive = await _positionService.GetCount(x => x.Active == true),
                PosInactive = await _positionService.GetCount(x => x.Active == false),
                PosNotLinkedToTasks = await _positionService.GetPositionsNotLinkedToTaskCount(),
                PosNotLinkedToSQs = await _positionService.GetPositionsNotLinkedToSQsCount(),
                PosNotLinkedToEmployees = await _positionService.GetPositionsNotLinkedEMPCount(),
            };

            return stats;
        }

        public async Task<List<Position>> GetAsync(bool activeOnly = false)
        {
            
            var positons = await _positionService.FindQuery(r => (activeOnly && r.Active) || (!activeOnly)).Select(s => new Position
            {
                Id = s.Id,
                PositionTitle = s.PositionTitle,
                PositionNumber = s.PositionNumber,
                PositionAbbreviation = s.PositionAbbreviation,
                PositionDescription = s.PositionDescription,
                HyperLink = s.HyperLink,
                IsPublished = s.IsPublished,
                EffectiveDate = s.EffectiveDate,
                FileName = s.FileName,
                Active = s.Active,
            }).ToListAsync();
            var trainingProgramsWithType = await _trainingProgramService.GetTrainingProgramCompactWithIncludeTypeAndILALinks();
            for (int i =0; i<positons.Count; i++)
            {
                positons[i].TrainingPrograms = trainingProgramsWithType.Where(x => x.PositionId == positons[i].Id).ToList();
                for(int j =0; j < positons[i].TrainingPrograms.Count; j++)
                {
                    //positons[i].TrainingPrograms.ToList()[j].TrainingProgramType = await _trainingProgramTypeService.FindQuery(x => x.Id == positons[i].TrainingPrograms.ToList()[j].TrainingProgramTypeId).FirstOrDefaultAsync();
                    //positons[i].TrainingPrograms.ToList()[j].TrainingProgram_ILA_Links = await _trainingProgram_ilaService.FindQuery(x => x.TrainingProgramId == positons[i].TrainingPrograms.ToList()[j].Id).ToListAsync();
                    for (var k = 0; k< positons[i].TrainingPrograms.ToList()[j].TrainingProgram_ILA_Links.Count; k++)
                    {
                        positons[i].TrainingPrograms.ToList()[j].TrainingProgram_ILA_Links.ToList()[k].ILA = await _ilaService.GetCompactedILA(positons[i].TrainingPrograms.ToList()[j].TrainingProgram_ILA_Links.ToList()[k].ILAId);
                    }
                }
            }
            positons = positons.Where(position => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, position, PositionOperations.Read).Result.Succeeded).ToList();
            return positons.OrderBy(x=>x.PositionTitle).ToList();
        }

        public async Task<List<Position>> GetWithoutIncludesAsync()
        {
            var positions = await _positionService.GetAllCompactPositions();


            return positions.OrderBy(x=>x.PositionTitle).ToList();
        }

        public async Task<List<Position>> GetAllOrderByAsync(string orderBy)
        {
            var positions = await _positionService.AllQuery().Select(s => new Position
            {
                Id = s.Id,
                PositionTitle = s.PositionTitle,
                PositionNumber = s.PositionNumber,
                PositionAbbreviation = s.PositionAbbreviation,
                PositionDescription = s.PositionDescription,
                HyperLink = s.HyperLink,
                IsPublished = s.IsPublished,
                EffectiveDate = s.EffectiveDate,
                FileName = s.FileName,
                Active = s.Active,

            }).ToListAsync();

            switch (orderBy.Trim().ToLower())
            {
                case "name":
                    positions = positions.OrderBy(o => o.PositionTitle).ToList();
                    break;
            }


            return positions;
        }

        public async Task<int> GetPositionNumberAsync()
        {
            var count = await _positionService.AllQuery().IgnoreQueryFilters().OrderByDescending(o => o.PositionNumber).Select(s => s.PositionNumber).FirstOrDefaultAsync();
            //positons = positons.Where(position => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, position, PositionOperations.Read).Result.Succeeded).ToList();
            //int count = positons.Count();
            return count;
        }

        public async Task<Position> GetAsync(int id)
        {
            
            var position = await _positionService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (position != null)
            {
                position.EmployeePositions = await _empPosService.FindQuery(x => x.PositionId == position.Id).ToListAsync();
                position.Position_SQs = await _posSQService.FindQuery(x => x.PositionId == position.Id).ToListAsync();
                position.Position_Tasks = await _posTaskService.FindQuery(x => x.PositionId == position.Id).ToListAsync();
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, position, PositionOperations.Read);
                if (result.Succeeded)
                {
                    return position;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            throw new QTDServerException(_localizer["PositionNotFound"]);
        }

        public async Task<Position> UpdateAsync(int id, PositionUpdateOptions options)
        {
            var position = await GetAsync(id);
            if (position != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, position, PositionOperations.Update);
                if (result.Succeeded)
                {
                    //var file = new byte[] { };
                    //if (!string.IsNullOrEmpty(options.PositionsFileUpload))
                    //{
                    //    file = Convert.FromBase64String(options.PositionsFileUpload);
                    //}

                    var data = options.PositionsFileUpload;
                    var byteData = new byte[] { };
                    if (data != null && data.Contains(","))
                    {
                        data = data.Substring(data.IndexOf(",") + 1);
                        byteData = Convert.FromBase64String(data);
                    }

                    position.PositionTitle = options.PositionTitle;
                    position.PositionAbbreviation = options.PositionAbbreviation;
                    position.PositionNumber = options.PositionNumber;
                    position.PositionsFileUpload = byteData;
                    position.PositionTitle = options.PositionTitle;
                    position.PositionAbbreviation = options.PositionAbbreviation;
                    position.PositionDescription = options.PositionDescription;
                    position.HyperLink = options.HyperLink;
                    position.EffectiveDate = options.EffectiveDate;
                    position.FileName = options.FileName;
                    position.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    position.ModifiedDate = DateTime.Now;
                    var validationResult = await _positionService.UpdateAsync(position);
                    if (validationResult.IsValid)
                    {
                        return position;
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

            throw new QTDServerException("Unknown Server Error");
        }

        public async System.Threading.Tasks.Task DeletePosition(int id)
        {
            var position = await GetAsync(id);
            if (position != null)
            {
                position.Delete();
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, position, PositionOperations.Delete);
                if (result.Succeeded)
                {
                    // Todo Delete Logic
                    var validationResult = await _positionService.UpdateAsync(position);
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
            else
            {
                throw new QTDServerException(_localizer["PositionNotFound"]);
            }
        }

        public async System.Threading.Tasks.Task InActivePosition(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, CoverSheetTypeOperations.Delete);

            if (result.Succeeded)
            {
                obj.Deactivate();

                var validationResult = await _positionService.UpdateAsync(obj);
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

        public async System.Threading.Tasks.Task PositionEndDateChange(int id, DateTime effectiveDate)
        {
            var links = await _empPosService.FindAsync(x=>x.PositionId == id);
            foreach(var l in links)
            {
                l.EndDate = DateOnly.FromDateTime(effectiveDate);
                await _empPosService.UpdateAsync(l);
            }
        }

        public async System.Threading.Tasks.Task ActivePosition(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, CoverSheetTypeOperations.Delete);

            if (result.Succeeded)
            {
                obj.Activate();

                var validationResult = await _positionService.UpdateAsync(obj);
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

        public async System.Threading.Tasks.Task InActiveAsync(int id, Position_HistoryCreateOptions options)
        {
            if (options != null && options.taskIds.Count() > 0)
            {
                foreach (var instructor in options.taskIds)
                {
                    var obj = await GetAsync(instructor);


                    obj.Deactivate();

                    var validationResult = await _positionService.UpdateAsync(obj);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));

                    }

                }
            }
            else
            {
                throw new QTDServerException("Position Ids not found");
            }
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id, Position_HistoryCreateOptions options)
        {
            if (options != null && options.taskIds.Count() > 0)
            {
                foreach (var instructor in options.taskIds)
                {
                    var obj = await GetAsync(instructor);
                    obj.Activate();

                    var validationResult = await _positionService.UpdateAsync(obj);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));

                    }

                }

            }
            else
            {
                throw new QTDServerException("Position Ids not found");
            }

        }
        public async System.Threading.Tasks.Task DeleteAsync(int id, Position_HistoryCreateOptions options)
        {
            if (options != null && options.taskIds.Count() > 0)
            {
                foreach (var instructor in options.taskIds)
                {
                    var obj = await GetAsync(instructor);
                    obj.Delete();

                    var validationResult = await _positionService.UpdateAsync(obj);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));

                    }

                }

            }
            else
            {
                throw new QTDServerException("Position Ids not found");
            }



        }

        public async Task<Position> LinkTask(int procId, Position_Task_LinkCreateOptions options)
        {
            var position = await _positionService.GetWithIncludeAsync(procId, new string[] { nameof(_position.Position_Tasks) });
            foreach (var id in options.TaskIds)
            {
                var task = await _taskService.GetAsync(id);

                var positionResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, position, PositionOperations.Update);
                var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Read);
                if (positionResult.Succeeded && taskResult.Succeeded)
                {
                    position.LinkTask(task);
                    var validationResult = await _positionService.UpdateAsync(position);
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

            return position;
        }

        public async System.Threading.Tasks.Task UnlinkTask(int posId, int[] taskId)
        {
            var position = await _positionService.GetWithIncludeAsync(posId, new string[] { nameof(_position.Position_Tasks) });
            foreach (var id in taskId)
            {
                var task = await _taskService.GetAsync(id);

                var procResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, position, PositionOperations.Update);
                var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, task, TaskOperations.Read);
                if (taskResult.Succeeded && procResult.Succeeded)
                {
                    position.UnlinkTask(task);
                    var validationResult = await _positionService.UpdateAsync(position);
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

        public async Task<List<TaskWithCountR5R6Options>> GetLinkedTasks(int id)
        {
            var links = await _posTaskService.FindWithIncludeAsync(x => x.PositionId == id, new string[] { nameof(_task_Position.Task), "R5ImpactedTasks.ImpactedTask.SubdutyArea.DutyArea" });
            List<Domain.Entities.Core.Task> taskList = new List<Domain.Entities.Core.Task>();
            taskList.AddRange(links.Select(x => x.Task));

            // return taskList;
            List<TaskWithCountR5R6Options> taskWithCount = new List<TaskWithCountR5R6Options>();
            foreach (var task in taskList)
            {
                var linkCount = await _posTaskService.GetCount(x => x.TaskId == task.Id);

                var taskNumber = await _taskDomainService.FindQueryWithIncludeAsync(x => x.Id == task.Id, new string[] { "SubdutyArea.DutyArea" }).FirstOrDefaultAsync();


                var num = taskNumber.SubdutyArea.DutyArea.Letter + ' ' + taskNumber.SubdutyArea.DutyArea.Number.ToString() + '.' + taskNumber.SubdutyArea.SubNumber.ToString() + '.' + task.Number.ToString();

                var taskGroups = await _taskTrainingGroupService.GetCount(x => x.TaskId == task.Id);
                var positionTask = task.Position_Tasks.FirstOrDefault();
                List<R5ImpactedTaskResponse> r5ImpactedTasks = new List<R5ImpactedTaskResponse>();
                r5ImpactedTasks = positionTask.R5ImpactedTasks
                    .Where(r=>!r.Deleted)
                    .Select(r => 
                        new R5ImpactedTaskResponse
                        { 
                            Id = r.Id,
                            Active = r.Active,
                            PositionTaskId = r.PositionTaskId, 
                            ImpactedTaskId = r.ImpactedTaskId, 
                            ImpactedTaskDescription = r.ImpactedTask.Description,
                            ImpactedTaskFullNumber = r.ImpactedTask.SubdutyArea.DutyArea.Letter + ' ' + r.ImpactedTask.SubdutyArea.DutyArea.Number.ToString() + '.' + r.ImpactedTask.SubdutyArea.SubNumber.ToString() + '.' + r.ImpactedTask.Number.ToString()
                        })
                    .ToList();
                var option = new TaskWithCountR5R6Options(num.ToString(), task.Description, task.Id, linkCount, task.Active, taskGroups,positionTask.IsR6Impacted, positionTask.R6ImpactedReason, positionTask.R6ImpactedEffectiveDate, positionTask.IsR5Impacted,positionTask.Id,r5ImpactedTasks);
                option.TaskNumber = task.Number;
                option.Letter = taskNumber.SubdutyArea.DutyArea.Letter;
                option.SDANumber = taskNumber.SubdutyArea.SubNumber;
                option.DANumber = taskNumber.SubdutyArea.DutyArea.Number;
                option.IsRR = task.IsReliability;
                taskWithCount.Add(option);
            }

            return taskWithCount;
        }

        public async Task<List<Position>> GetPositionsTaskIsLinkedTo(int id)
        {
            var data = await _posTaskService.AllQueryWithInclude(new string[] { nameof(_task_Position.Position) }).Where(x => x.TaskId == id).Select(x => x.Position).ToListAsync();
            return data;
        }

        public async Task<List<TrainingGroup>> GetPositionsTaskIsLinkedToTG(int id)
        {
            var data = await _taskTrainingGroupService.AllQueryWithInclude(new string[] { nameof(_task_TrainingGroup.TrainingGroup) }).Where(x => x.TaskId == id).Select(x => x.TrainingGroup).ToListAsync();
            return data;
        }

        public async Task<Position> LinkEO(int eoId, Position_SQ_LinkCreateOptions options)
        {
            var position = await _positionService.GetWithIncludeAsync(eoId, new string[] { nameof(_position.Position_SQs) });
            foreach (var id in options.EOIds)
            {
                var eo = await _eoService.GetAsync(id);

                var positionResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, position, PositionOperations.Update);
                var eoResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, EnablingObjectiveOperations.Read);
                if (positionResult.Succeeded && eoResult.Succeeded)
                {
                    position.LinkSQ(eo);
                    var validationResult = await _positionService.UpdateAsync(position);
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

            return position;
        }

        public async System.Threading.Tasks.Task UnlinkEO(int posId, int[] eoId)
        {
            var position = await _positionService.GetWithIncludeAsync(posId, new string[] { nameof(_position.Position_SQs) });
            foreach (var id in eoId)
            {
                var eo = await _eoService.GetAsync(id);

                var posResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, position, PositionOperations.Update);
                var eoResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, EnablingObjectiveOperations.Read);
                if (eoResult.Succeeded && posResult.Succeeded)
                {
                    position.UnlinkSQ(eo);
                    var validationResult = await _positionService.UpdateAsync(position);
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

        public async Task<List<EnablingObjectiveWithCountOptions>> GetLinkedEos(int id)
        {
            var links = await _posSQService.FindWithIncludeAsync(x => x.PositionId == id, new string[] { nameof(_pos_SQ.EnablingObjective) });
            List<Domain.Entities.Core.EnablingObjective> eoList = new List<Domain.Entities.Core.EnablingObjective>();
            eoList.AddRange(links.Select(x => x.EnablingObjective));

            List<EnablingObjectiveWithCountOptions> eoWithCount = new List<EnablingObjectiveWithCountOptions>();
            foreach (var eo in eoList)
            {
                var data = await _posSQService.GetCount(x => x.EOId == eo.Id);
                eoWithCount.Add(new EnablingObjectiveWithCountOptions(eo.Number, eo.Description, eo.Id, data, eo.Active));
            }

            return eoWithCount;
        }

        public async Task<List<Position>> GetPositionsEoIsLinkedTo(int id)
        {
            var data = await _posSQService.AllQueryWithInclude(new string[] { nameof(_pos_SQ.Position) }).Where(x => x.EOId == id).Select(x => x.Position).ToListAsync();
            return data;
        }

        public async Task<Position> LinkEmployee(int empId, Position_Employee_LinkCreateOptions options)
        {
            var position = await _positionService.GetWithIncludeAsync(empId, new string[] { nameof(_employee.EmployeePositions) });
            foreach (var id in options.EmployeeIds)
            {
                var emp = await _employeeService.GetAsync(id);

                var positionResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, position, PositionOperations.Update);
                var empResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, emp, Position_EmployeeOperations.Read);
                if (positionResult.Succeeded && empResult.Succeeded)
                {
                    position.LinkEmployee(emp, options.StartDate, options.Trainee,position.Id);
                    position.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    position.CreatedDate = DateTime.Now;
                    var validationResult = await _positionService.UpdateAsync(position);
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

            return position;
        }

        public async System.Threading.Tasks.Task UnlinkEmployee(int posId, int[] empId)
        {
            var position = await _positionService.GetWithIncludeAsync(posId, new string[] { nameof(_position.EmployeePositions) });
            foreach (var id in empId)
            {
                var emp = await _employeeService.GetAsync(id);

                var posResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, position, PositionOperations.Update);
                var empResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, emp, EmployeeOperations.Read);
                if (empResult.Succeeded && posResult.Succeeded)
                {
                    position.UnlinkEmployee(emp);
                    var validationResult = await _positionService.UpdateAsync(position);
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

        public async Task<List<EmployeePositionListVM>> GetLinkedEmployees(int id)
        {

            //new string[] { "Employee.Person", "Position" }
            var query = _positionService.FindQueryWithIncludeAsync(x => x.Id == id, new string[] { "EmployeePositions.Employee.Person" }, false);

            var employees =  query.FirstOrDefault().EmployeePositions.Select(x => new EmployeePositionListVM
            {
                EmployeeId = x.EmployeeId,
                EndDate = x.EndDate,
                FirstName= x.Employee.Person.FirstName,
                Image= x.Employee.Person.Image,
                IsCertificationNotRequired= x.IsCertificationNotRequired,
                IsSignificant= x.IsSignificant,
                LastName= x.Employee.Person.LastName,
                ManagerName= x.ManagerName,
                PositionId= x.PositionId,
                PositionTitle= x.Position.PositionTitle,
                QualificationDate= x.QualificationDate,
                StartDate= x.StartDate,
                Trainee= x.Trainee,
                TrainingProgramVersion= x.TrainingProgramVersion,
                UserName = x.Employee.Person.Username,
                Active= x.Employee.Active
                
            }).OrderBy(x=>x.FullName).ToList();
                
            return employees;
        }

        public async Task<List<Position>> GetPositionsEmployeeIsLinkedTo(int id)
        {
            var data = await _empPosService.AllQueryWithInclude(new string[] { nameof(_emp_Pos.Position) }).Where(x => x.EmployeeId == id).Select(x => x.Position).ToListAsync();
            return data;
        }

        public async Task<List<Position>> GetPosNotLinkedTo(string option)
        {
            var notLinkedPos = new List<Position>();
            List<int> linkedPosIds = new List<int>();
            List<int> notLinkedPosIds = new List<int>();

            switch (option.ToLower().Trim())
            {
                case "task":
                    {
                        linkedPosIds = await _posTaskService.AllQuery().Select(x => x.PositionId).Distinct().ToListAsync();
                        notLinkedPosIds = await _positionService.FindQuery(x => !linkedPosIds.Contains(x.Id))
                            .Select(x => x.Id).ToListAsync();
                        break;
                    }

                case "sq":
                    {
                        linkedPosIds = await _posSQService.AllQuery().Select(x => x.PositionId).Distinct().ToListAsync();
                        notLinkedPosIds = await _positionService.FindQuery(x => !linkedPosIds.Contains(x.Id))
                            .Select(x => x.Id).ToListAsync();
                        break;
                    }

                case "employee":
                    {
                        linkedPosIds = (await _empPosService.AllAsync()).Select(x => x.PositionId).Distinct().ToList();
                        notLinkedPosIds = await _positionService.FindQuery(x => !linkedPosIds.Contains(x.Id))
                            .Select(x => x.Id).ToListAsync();
                        break;
                    }
            }


            //notLinkedPos = await _issuingAuthorityService.AllQuery().ToListAsync();
            notLinkedPos = _positionService.FindQuery(x => notLinkedPosIds.Contains(x.Id)).ToList();
            //foreach (var ia in notLinkedPos)
            //{
            //    ia.Procedures = _positionService.FindQuery(x => x.IssuingAuthorityId == ia.Id && notLinkedProcIds.Contains(x.Id)).ToList();
            //}

            return notLinkedPos.OrderBy(o => o.PositionNumber).ToList();
        }

        public async Task<List<Position>> GetActiveInactivePosition(string notLinkedWith)
        {
            var ProcList = new List<Position>();

            switch (notLinkedWith.ToLower().Trim())
            {
                case "active":
                    {
                        ProcList = await _positionService.FindQuery(x => x.Active == true).Select(s=> new Position
                        {
                            Id = s.Id,
                            PositionTitle = s.PositionTitle,
                            PositionAbbreviation = s.PositionAbbreviation,
                            PositionNumber = s.PositionNumber,
                        }).OrderBy(o => o.PositionNumber).ToListAsync();
                        break;
                    }

                case "inactive":
                    {
                        ProcList = await _positionService.FindQuery(x => x.Active == false).Select(s => new Position
                        {
                            Id = s.Id,
                            PositionTitle = s.PositionTitle,
                            PositionAbbreviation = s.PositionAbbreviation,
                            PositionNumber = s.PositionNumber,
                        }).OrderBy(o => o.PositionNumber).ToListAsync();

                        break;
                    }
            }

            return ProcList;
        }

        public async Task<List<Position>> GetPositonWithPositionTaskAsync()
        {
            var position = await _positionService.AllWithIncludeAsync(new[] { "Position_Tasks" });
            return position.ToList();
        }

        public async Task<Position> GetPositionByNameAsync(string positionName)
        {
            return await _positionService.GetPositionByNameAsync(positionName);
        }
    }
}
