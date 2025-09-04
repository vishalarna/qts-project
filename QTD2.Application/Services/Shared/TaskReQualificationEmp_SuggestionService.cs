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
using ITaskDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskService;
using IEmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeService;
using IEmployee_TaskLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployee_TaskService;
using ITask_PositionDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_PositionService;
using IPositionDomainService = QTD2.Domain.Interfaces.Service.Core.IPositionService;
using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using ITaskQualificationStatusDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskQualificationStatusService;
using ITQEmpSettingDomainService = QTD2.Domain.Interfaces.Service.Core.ITQEmpSettingService;
using ITaskQualEvalLinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskQualification_Evaluator_LinkService;
using IPersonDomainService = QTD2.Domain.Interfaces.Service.Core.IPersonService;
using ITaskReQualificationEmp_SuggestionDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskReQualificationEmp_SuggestionService;
using ITask_SuggestionDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_SuggestionService;
using ITaskReQualificationEmp_SignOffDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskReQualificationEmp_SignOffService;
using QTD2.Infrastructure.Model.TaskReQualificationEmp;

namespace QTD2.Application.Services.Shared
{
    public class TaskReQualificationEmp_SuggestionService : ITaskReQualificationEmp_SuggestionService
    {
        private readonly IStringLocalizer<Domain.Entities.Core.TaskReQualificationEmp_Suggestion> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITaskService _taskService;
        private readonly IEmployeeDomainService _empService;
        private readonly IEmployee_TaskLinkDomainService _empTaskLinkService;
        private readonly ITask_PositionDomainService _task_positionService;
        private readonly IPositionDomainService _positionService;
        private readonly ITaskQualificationStatusDomainService _tqStatusService;
        private readonly ITQEmpSettingDomainService _tqEmpSettingService;
        private readonly ITaskQualEvalLinkDomainService _tq_evalService;
        private readonly IPersonDomainService _personService;
        private readonly ITaskReQualificationEmp_SuggestionDomainService _empSuggestionsDomainService;
        private readonly ITask_SuggestionDomainService _taskSuggestionService;
        private readonly ITaskReQualificationEmp_SignOffDomainService _signOffDomainService;

        public TaskReQualificationEmp_SuggestionService(
           IStringLocalizer<Domain.Entities.Core.TaskReQualificationEmp_Suggestion> localizer,
           IHttpContextAccessor httpContextAccessor,
           IAuthorizationService authorizationService,
           UserManager<AppUser> userManager,
           ITaskService taskService,
           IEmployeeDomainService empService,
           IEmployee_TaskLinkDomainService empTaskLinkService,
           ITask_PositionDomainService task_positionService,
           IPositionDomainService positionService,
           ITaskService task_AppService,
           ITaskQualificationStatusDomainService tqStatusService,
           ITQEmpSettingDomainService tqEmpSettingService,
           ITaskQualEvalLinkDomainService tq_evalService, IPersonDomainService personService, ITaskReQualificationEmp_SuggestionDomainService empSuggestionsDomainService, ITask_SuggestionDomainService taskSuggestionService, ITaskReQualificationEmp_SignOffDomainService signOffDomainService)
        {
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _taskService = taskService;
            _empService = empService;
            _empTaskLinkService = empTaskLinkService;
            _task_positionService = task_positionService;
            _positionService = positionService;
            _tqStatusService = tqStatusService;
            _tqEmpSettingService = tqEmpSettingService;
            _tq_evalService = tq_evalService;
            _personService = personService;
            _empSuggestionsDomainService = empSuggestionsDomainService;
            _taskSuggestionService = taskSuggestionService;
            _signOffDomainService = signOffDomainService;
        }

        public async Task<bool> GetShowSuggestionBit(int qualificationId)
        {
            var tqEMPSettingData = await _tqEmpSettingService.FindQuery(x => x.TaskQualificationId == qualificationId).FirstOrDefaultAsync();
            return tqEMPSettingData.ShowTaskSuggestions;
        }

        public async Task<bool> GeQuestionAnswerBit(int qualificationId)
        {
            var tqEMPSettingData = await _tqEmpSettingService.FindQuery(x => x.TaskQualificationId == qualificationId).FirstOrDefaultAsync();
            return tqEMPSettingData.ShowTaskQuestions;
        }

        public async Task<TaskReQualificationEmpSuggestionVM> GetSuggestionData(int qualificationId, int taskId, int employeeId)
        {
            //Get Current Evaluator 
            var qualWithSuggestions = new TaskReQualificationEmpSuggestionVM();
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

            var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();

            if (person != null)
            {
                var employee = await _empService.FindQueryWithIncludeAsync(x => x.PersonId == person.Id, new string[] { "Person" }).FirstOrDefaultAsync();
                var qualificationEmp = await _empSuggestionsDomainService.FindQueryWithIncludeAsync(x => x.TaskQualificationId == qualificationId && x.TraineeId == employeeId && x.EvaluatorId == employee.Id, new string[] { "TaskQualification.Task.Task_Suggestions" }).ToListAsync();
                
                var suggestionVM = new Suggestion();
                var suggestionListVM = new List<Suggestion>();
                if (qualificationEmp.Count == 0)
                {
                    var task = await _taskService.GetAsync(taskId);
                    //check sugegstions in tasks
                    var sugegstions =(await _taskService.GetAllSuggestionsAsync(taskId)).ToList();
                    if (sugegstions != null && sugegstions.Count > 0) 
                    {
                        foreach(var suggestion in sugegstions)
                        {
                            qualWithSuggestions.SuggestionList.Add(new Suggestion()
                            {
                                SuggestionId = suggestion.Id,
                                SuggesntionDescription = suggestion.Description,
                                Comments = string.Empty,
                                IsCompleted= false,
                                

                            }) ;
                        }
                      
                    }
                    else
                    {
                        qualWithSuggestions.SuggestionList = new List<Suggestion>();
                    }
                    if (task.IsMeta)
                    {
                        foreach (var mt in task.Task_MetaTask_Links)
                        {
                            var metaSuggestions = (await _taskService.GetAllSuggestionsAsync(mt.TaskId)).ToList();
                            foreach (var suggestion in metaSuggestions)
                            {
                                qualWithSuggestions.SuggestionList.Add(new Suggestion()
                                {
                                    SuggestionId = suggestion.Id,
                                    SuggesntionDescription = suggestion.Description,
                                    Comments = string.Empty,
                                    IsCompleted = false,
                                });
                            }
                        }
                    }
                    var number = await _taskService.GetTaskNumberWithLetter(task.SubdutyAreaId, task.Id);
                    var concatenatedNumber = number.Letter + number.DANumber + "." + number.SDANumber + "." + number.TaskNumber;
                    qualWithSuggestions.concateNatedTaskNumber = concatenatedNumber;
                    qualWithSuggestions.TaskDescription = task.Description;
                    qualWithSuggestions.TaskQualificationId = qualificationId;
                    qualWithSuggestions.TaskId = task.Id;
                }
                else
                {
                    //get a list of suggestions
                    var task = await _taskService.GetAsync(taskId);

                    var suggestions = ( await _taskService.GetAllSuggestionsAsync(taskId)).ToList();
                    foreach(var suggestion in suggestions)
                    {
                        var qual = qualificationEmp.FirstOrDefault(x => x.TaskSuggestionId == suggestion.Id);
                        qualWithSuggestions.SuggestionList.Add(new Suggestion()
                        {
                            SuggestionId = suggestion.Id,
                            SuggesntionDescription = suggestion.Description,
                            Comments = qual == null ? string.Empty : qual.Comments,
                            IsCompleted = qual == null ? false : qual.IsCompleted,
                        });
                    }
                    if (task.IsMeta)
                    {
                        foreach(var mt in task.Task_MetaTask_Links)
                        {
                            var metaSuggestions = (await _taskService.GetAllSuggestionsAsync(mt.TaskId)).ToList();
                            foreach (var suggestion in metaSuggestions)
                            {
                                var qual = qualificationEmp.FirstOrDefault(x => x.TaskSuggestionId == suggestion.Id);
                                qualWithSuggestions.SuggestionList.Add(new Suggestion()
                                {
                                    SuggestionId = suggestion.Id,
                                    SuggesntionDescription = suggestion.Description,
                                    Comments = qual == null ? string.Empty : qual.Comments,
                                    IsCompleted = qual == null ? false : qual.IsCompleted,
                                });
                            }
                        }
                    }
                    
                    var number = await _taskService.GetTaskNumberWithLetter(task.SubdutyAreaId, task.Id);
                    var concatenatedNumber = number.Letter + number.DANumber + "." + number.SDANumber + "." + number.TaskNumber;
                    qualWithSuggestions.concateNatedTaskNumber = concatenatedNumber;
                    qualWithSuggestions.TaskDescription = task.Description;
                    qualWithSuggestions.TaskQualificationId = qualificationId;
                    qualWithSuggestions.TaskId = task.Id;

                }

                //Make an entry in sign off Table
                var signOffObj = new TaskReQualificationEmp_SignOff(qualificationId, null, null, employee.Id, null, null, employeeId, null, true, false,null,false,false);
                var result = await _signOffDomainService.AddAsync(signOffObj);

            }
            return qualWithSuggestions;
        }


        public async System.Threading.Tasks.Task CreateOrUpdateSuggestionsAsync(TaskReQualificationEmpSuggestionVM options)
        {
                var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

            var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();

            if (person != null)
            {
                var employee = await _empService.FindQueryWithIncludeAsync(x => x.PersonId == person.Id, new string[] { "Person" }).FirstOrDefaultAsync();
                if(options.SuggestionList != null && options.SuggestionList.Count > 0)
                {
                    foreach(var suggestion in options.SuggestionList)
                    {
                        var qualificationEmp = await _empSuggestionsDomainService.FindQuery(x => x.TaskQualificationId == options.TaskQualificationId && x.TraineeId == options.TraineeId && x.EvaluatorId == employee.Id && x.TaskSuggestionId == suggestion.SuggestionId).FirstOrDefaultAsync();

                        if(qualificationEmp == null)
                        {
                            //add case
                            var requalification = new TaskReQualificationEmp_Suggestion(options.TaskQualificationId, suggestion.SuggestionId, suggestion.Comments, employee.Id, DateTime.UtcNow, options.TraineeId, suggestion.IsCompleted);
                            var validationResult = await _empSuggestionsDomainService.AddAsync(requalification);

                        }
                        else
                        {
                            qualificationEmp.TaskQualificationId = options.TaskQualificationId;
                            qualificationEmp.TaskSuggestionId = suggestion.SuggestionId;
                            qualificationEmp.Comments = suggestion.Comments;
                            qualificationEmp.TraineeId = options.TraineeId;
                            qualificationEmp.IsCompleted = suggestion.IsCompleted;
                            qualificationEmp.CommentDate = DateTime.UtcNow;
                            qualificationEmp.EvaluatorId = employee.Id;
                            var validationResult = await _empSuggestionsDomainService.UpdateAsync(qualificationEmp);
                        }
                    }    
                   

                }
              

            }
        }
    }
}
