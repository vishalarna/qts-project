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
using ITaskReQualificationEmp_QuestionAnswerDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskReQualificationEmp_QuestionAnswerService;
using ITask_QuestionDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_QuestionService;
using QTD2.Infrastructure.Model.TaskReQualificationEmp;
using ISkillReQualificationEmp_QuestionAnswerDomainService = QTD2.Domain.Interfaces.Service.Core.ISkillReQualificationEmp_QuestionAnswerService;
using IEnablingObjectiveDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjectiveService;


namespace QTD2.Application.Services.Shared
{
    public class TaskReQualificationEmp_QuestionAnswerService : ITaskReQualificationEmp_QuestionAnswerService
    {
        private readonly IStringLocalizer<Domain.Entities.Core.TaskReQualificationEmp_Steps> _localizer;
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
        private readonly ITaskReQualificationEmp_QuestionAnswerDomainService _empQuestionsDomainService;
        private readonly ITask_QuestionDomainService _taskQuestionDomainService;
        private readonly ISkillReQualificationEmp_QuestionAnswerDomainService _skillReQualificationEmp_QuestionAnswerService;
        private readonly IEnablingObjectiveDomainService _enablingObjectiveService;

        public TaskReQualificationEmp_QuestionAnswerService(
           IStringLocalizer<Domain.Entities.Core.TaskReQualificationEmp_Steps> localizer,
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
           ITaskQualEvalLinkDomainService tq_evalService, IPersonDomainService personService, ITaskReQualificationEmp_QuestionAnswerDomainService empQuestionsDomainService, ITask_QuestionDomainService taskQuestionDomainService, ISkillReQualificationEmp_QuestionAnswerDomainService skillReQualificationEmp_QuestionAnswerService, IEnablingObjectiveDomainService enablingObjectiveService)
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
            _empQuestionsDomainService = empQuestionsDomainService;
            _taskQuestionDomainService = taskQuestionDomainService;
            _skillReQualificationEmp_QuestionAnswerService = skillReQualificationEmp_QuestionAnswerService;
            _enablingObjectiveService = enablingObjectiveService;
        }

        public async Task<TaskReQualificationEmpQuestionVM> GetQuestionsData(int qualificationId, int taskId, int employeeId)
        {
            //Get Current Evaluator 
            var qualWithQuestions = new TaskReQualificationEmpQuestionVM();
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

            var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();

            if (person != null)
            {
                var employee = await _empService.FindQueryWithIncludeAsync(x => x.PersonId == person.Id, new string[] { "Person" }).FirstOrDefaultAsync();
                var qualificationEmp = await _empQuestionsDomainService.FindQueryWithIncludeAsync(x => x.TaskQualificationId == qualificationId && x.TraineeId == employeeId && x.EvaluatorId == employee.Id, new string[] { "TaskQualification.Task.Task_Questions" }).ToListAsync();

                var questionVM = new QuesionAnswer();
                var questionListVM = new List<QuesionAnswer>();
                if (qualificationEmp.Count == 0)
                {
                    //check questions in tasks
                    var task = await _taskService.GetAsync(taskId);
                    var questions = (await _taskService.GetTask_QuestionsAsync(taskId)).ToList();
                    if (questions != null && questions.Count > 0)
                    {
                        foreach (var question in questions)
                        {
                            qualWithQuestions.QuesionAnswerList.Add(new QuesionAnswer()
                            {
                                QuestionId = question.Id,
                                QuestionDescription = question.Question,
                                Answer = question.Answer,
                                Comments = string.Empty,
                                IsCompleted = false,
                            });
                        }

                    }
                    else
                    {
                        qualWithQuestions.QuesionAnswerList = new List<QuesionAnswer>();
                    }
                    if (task.IsMeta)
                    {
                        foreach (var mt in task.Task_MetaTask_Links)
                        {
                            var metaQuestions = (await _taskService.GetTask_QuestionsAsync(mt.TaskId)).ToList();
                            foreach (var question in metaQuestions)
                            {
                                qualWithQuestions.QuesionAnswerList.Add(new QuesionAnswer()
                                {
                                    QuestionId = question.Id,
                                    QuestionDescription = question.Question,
                                    Answer = question.Answer,
                                    Comments = string.Empty,
                                    IsCompleted = false,
                                });
                            }
                        }
                    }

                    qualWithQuestions.TaskDescription = task.Description;
                    qualWithQuestions.TaskQualificationId = qualificationId;
                    qualWithQuestions.TaskId = task.Id;
                }
                else
                {
                    //get a list of questions
                    var task = await _taskService.GetAsync(taskId);
                    var questions = (await _taskService.GetTask_QuestionsAsync(taskId)).ToList();
                    foreach (var question in questions)
                    {
                        var qual = qualificationEmp.FirstOrDefault(x => x.TaskQuestionId == question.Id);
                        qualWithQuestions.QuesionAnswerList.Add(new QuesionAnswer()
                        {
                            QuestionId = question.Id,
                            QuestionDescription = question.Question,
                            Answer = question.Answer,
                            Comments = qual == null ? string.Empty : qual.Comments,
                            IsCompleted = qual == null ? false : qual.IsCompleted,


                        });
                    }
                    if (task.IsMeta)
                    {
                        foreach (var mt in task.Task_MetaTask_Links)
                        {
                            var metaQuestions = (await _taskService.GetTask_QuestionsAsync(mt.TaskId)).ToList();
                            foreach (var question in metaQuestions)
                            {
                                var qual = qualificationEmp.FirstOrDefault(x => x.TaskQuestionId == question.Id);
                                qualWithQuestions.QuesionAnswerList.Add(new QuesionAnswer()
                                {
                                    QuestionId = question.Id,
                                    QuestionDescription = question.Question,
                                    Answer = question.Answer,
                                    Comments = qual == null ? string.Empty : qual.Comments,
                                    IsCompleted = qual == null ? false : qual.IsCompleted,


                                });
                            }
                        }
                    }
                    qualWithQuestions.TaskDescription = task.Description;
                    qualWithQuestions.TaskQualificationId = qualificationId;
                    qualWithQuestions.TaskId = task.Id;
                }
            }
            return qualWithQuestions;
        }

        public async Task<TaskReQualificationEmpQuestionVM> GetQuestionsSQData(int skillQualificationId, int skillId, int employeeId)
        {
            //Get Current Evaluator 
            var qualWithQuestions = new TaskReQualificationEmpQuestionVM();
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;
            var person = await _personService.GetPersonByUserName(userName);

            if (person != null)
            {
                var employee = await _empService.GetEmployeeByPersonId(person.Id);
                var qualificationEmp = await _skillReQualificationEmp_QuestionAnswerService.GetBySkillQualificationIdAsync(skillQualificationId, employeeId, employee.Id);

                var questionVM = new QuesionAnswer();
                var questionListVM = new List<QuesionAnswer>();
                if (qualificationEmp.Count == 0)
                {
                    var enablingObjective = await _enablingObjectiveService.GetAsync(skillId);
                    var questions = (await _enablingObjectiveService.GetAllQuestionByIdAsync(skillId)).ToList();
                    if (questions != null && questions.Count > 0)
                    {
                        foreach (var question in questions)
                        {
                            qualWithQuestions.QuesionAnswerList.Add(new QuesionAnswer()
                            {
                                QuestionId = question.Id,
                                QuestionDescription = question.Question,
                                Answer = question.Answer,
                                Comments = string.Empty,
                                IsCompleted = false,
                            });
                        }

                    }
                    else
                    {
                        qualWithQuestions.QuesionAnswerList = new List<QuesionAnswer>();
                    }

                    qualWithQuestions.SkillDescription = enablingObjective.Description;
                    qualWithQuestions.SkillQualificationId = skillQualificationId;
                    qualWithQuestions.SkillId = enablingObjective.Id;
                }
                else
                {
                    //get a list of questions
                    var enablingObjective = await _enablingObjectiveService.GetAsync(skillId);
                    var questions = (await _enablingObjectiveService.GetAllQuestionByIdAsync(skillId)).ToList();
                    foreach (var question in questions)
                    {
                        var qual = qualificationEmp.FirstOrDefault(x => x.SkillQuestionId == question.Id);
                        qualWithQuestions.QuesionAnswerList.Add(new QuesionAnswer()
                        {
                            QuestionId = question.Id,
                            QuestionDescription = question.Question,
                            Answer = question.Answer,
                            Comments = qual == null ? string.Empty : qual.Comments,
                            IsCompleted = qual == null ? false : qual.IsCompleted,


                        });
                    }
                    qualWithQuestions.SkillDescription = enablingObjective.Description;
                    qualWithQuestions.SkillQualificationId = skillQualificationId;
                    qualWithQuestions.SkillId = enablingObjective.Id;
                }
            }
            return qualWithQuestions;
        }


        public async System.Threading.Tasks.Task CreateOrUpdateQuestionsDataAsync(TaskReQualificationEmpQuestionVM options)
        {
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

            var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();

            if (person != null)
            {
                var employee = await _empService.FindQueryWithIncludeAsync(x => x.PersonId == person.Id, new string[] { "Person" }).FirstOrDefaultAsync();
                if (options.QuesionAnswerList != null && options.QuesionAnswerList.Count > 0)
                {
                    foreach (var quesAns in options.QuesionAnswerList)
                    {
                        if (options.TaskId > 0)
                        {
                            var qualificationEmp = await _empQuestionsDomainService.FindQuery(x => x.TaskQualificationId == options.TaskQualificationId && x.TraineeId == options.TraineeId && x.EvaluatorId == employee.Id && x.TaskQuestionId == quesAns.QuestionId).FirstOrDefaultAsync();

                            if (qualificationEmp == null)
                            {
                                //add case
                                var requalification = new TaskReQualificationEmp_QuestionAnswer(options.TaskQualificationId, quesAns.QuestionId, quesAns.Comments, employee.Id, DateTime.UtcNow, options.TraineeId, quesAns.IsCompleted);
                                var validationResult = await _empQuestionsDomainService.AddAsync(requalification);

                            }
                            else
                            {
                                qualificationEmp.TaskQualificationId = options.TaskQualificationId;
                                qualificationEmp.TaskQuestionId = quesAns.QuestionId;
                                qualificationEmp.Comments = quesAns.Comments;
                                qualificationEmp.TraineeId = options.TraineeId;
                                qualificationEmp.IsCompleted = quesAns.IsCompleted;
                                qualificationEmp.CommentDate = DateTime.UtcNow;
                                qualificationEmp.EvaluatorId = employee.Id;
                                var validationResult = await _empQuestionsDomainService.UpdateAsync(qualificationEmp);
                            }
                        }
                        else
                        {
                            var qualificationEmp = (await _skillReQualificationEmp_QuestionAnswerService.GetBySkillQualificationAndQuestionIdIdAsync(options.SkillQualificationId, options.TraineeId, employee.Id, quesAns.QuestionId)).FirstOrDefault();

                            if (qualificationEmp == null)
                            {
                                var requalification = new SkillReQualificationEmp_QuestionAnswer(options.SkillQualificationId, quesAns.QuestionId, quesAns.Comments, employee.Id, DateTime.UtcNow, options.TraineeId, quesAns.IsCompleted);
                                var validationResult = await _skillReQualificationEmp_QuestionAnswerService.AddAsync(requalification);
                            }
                            else
                            {
                                qualificationEmp.SkillQualificationId = options.SkillQualificationId;
                                qualificationEmp.SkillQuestionId = quesAns.QuestionId;
                                qualificationEmp.Comments = quesAns.Comments;
                                qualificationEmp.TraineeId = options.TraineeId;
                                qualificationEmp.IsCompleted = quesAns.IsCompleted;
                                qualificationEmp.CommentDate = DateTime.UtcNow;
                                qualificationEmp.EvaluatorId = employee.Id;
                                var validationResult = await _skillReQualificationEmp_QuestionAnswerService.UpdateAsync(qualificationEmp);
                            }
                        }
                    }
                }
            }
        }
    }
}
