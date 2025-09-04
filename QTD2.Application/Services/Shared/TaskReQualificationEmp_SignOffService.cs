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
using ITaskReQualificationEmp_SignOffDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskReQualificationEmp_SignOffService;
using IEmployee_PositionDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeePositionService;
using ITaskQualification_Evaluator_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskQualification_Evaluator_LinkService;
using ITaskQualificationDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskQualificationService;
using ITaskReQualificationEmp_StepDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskReQualificationEmp_StepsService;
using ITaskReQualificationEmp_QuestionAnswerDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskReQualificationEmp_QuestionAnswerService;
using IPositionTask_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IPosition_TaskService;
using QTD2.Infrastructure.Model.TaskReQualificationEmp;

namespace QTD2.Application.Services.Shared
{
    public class TaskReQualificationEmp_SignOffService : ITaskReQualificationEmp_SignOffService
    {
        private readonly IStringLocalizer<Domain.Entities.Core.TaskReQualificationEmp_SignOff> _localizer;
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
        private readonly ITaskReQualificationEmp_SignOffDomainService _empSignOffDomainService;
        private readonly IEmployee_PositionDomainService _emp_positionService;
        private readonly ITaskQualification_Evaluator_LinkDomainService _evaluatorLinkService;
        private readonly ITaskQualificationDomainService _taskQualificationDomainService;
        private readonly ITaskReQualificationEmp_StepDomainService _empStepsService;
        private readonly ITaskReQualificationEmp_QuestionAnswerDomainService _empQuestionAnswerService;
        private readonly IPositionTask_LinkDomainService _position_TaskService;

        public TaskReQualificationEmp_SignOffService(
           IStringLocalizer<Domain.Entities.Core.TaskReQualificationEmp_SignOff> localizer,
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
           ITaskQualEvalLinkDomainService tq_evalService, IPersonDomainService personService, ITaskReQualificationEmp_SignOffDomainService empSignOffDomainService, IEmployee_PositionDomainService emp_positionService, ITaskQualification_Evaluator_LinkDomainService evaluatorLinkService, ITaskQualificationDomainService taskQualificationDomainService, ITaskReQualificationEmp_StepDomainService empStepsService, ITaskReQualificationEmp_QuestionAnswerDomainService empQuestionAnswerService, IPositionTask_LinkDomainService position_TaskService)
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
            _empSignOffDomainService = empSignOffDomainService;
            _emp_positionService = emp_positionService;
            _evaluatorLinkService = evaluatorLinkService;
            _taskQualificationDomainService = taskQualificationDomainService;
            _empStepsService = empStepsService;
            _empQuestionAnswerService = empQuestionAnswerService;
            _position_TaskService = position_TaskService;
        }

        public async Task<TaskReQualificationEmpSignOffVM> GetSignOffData(int qualificationId, int employeeId)
        {
            //Get Current Evaluator 
            var signOffVm = new TaskReQualificationEmpSignOffVM();
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

            var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();


            var employee = await _empService.FindQueryWithIncludeAsync(x => x.PersonId == person.Id, new string[] { "Person" }).FirstOrDefaultAsync();
            var qualificationEmp = await _empSignOffDomainService.FindQueryWithIncludeAsync(x => x.TaskQualificationId == qualificationId && x.TraineeId == employeeId && x.EvaluatorId == employee.Id, new string[] { "TaskQualification.TaskQualification_Evaluator_Links.Evaluator" }).FirstOrDefaultAsync();
            var trainee = await _empService.FindQueryWithIncludeAsync(x => x.Id == employeeId, new string[] { "Person" }).FirstOrDefaultAsync();
            var traineeName = trainee.Person.FirstName + " " + trainee.Person.LastName;
            var evaluator = await _empService.FindQueryWithIncludeAsync(x => x.Id == employee.Id, new string[] { "Person" }).FirstOrDefaultAsync();
            var evaluatorName = evaluator.Person.FirstName + " " + evaluator.Person.LastName;

            if (qualificationEmp == null)
            {
                //make an entry in signoff

                qualificationEmp = new TaskReQualificationEmp_SignOff(qualificationId, null, null, employee.Id, null, null, employeeId, null, true, false, null,false,false);
                var result = await _empSignOffDomainService.AddAsync(qualificationEmp);
                if (result.IsValid)
                {
                    signOffVm.TaskQualificationId = qualificationEmp.TaskQualificationId;
                    signOffVm.IsCriteriaMet = qualificationEmp.IsCriteriaMet;
                    signOffVm.Comments = qualificationEmp.Comments;
                    signOffVm.EvaluatorId = qualificationEmp.EvaluatorId;
                    signOffVm.EvaluationMethodId = qualificationEmp.EvaluationMethodId;
                    signOffVm.TaskQualificationDate = qualificationEmp.TaskQualificationDate;
                    signOffVm.SignOffDate = qualificationEmp.SignOffDate;
                    signOffVm.TraineeName = traineeName;
                    signOffVm.EvaluatorName = evaluatorName;
                    signOffVm.IsTraineeSignOff = qualificationEmp.IsTraineeSignOff;
                    signOffVm.IsEvaluatorSignOff = qualificationEmp.IsEvaluatorSignOff;
                    return signOffVm;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', result.Errors));
                }

            }
            signOffVm.TaskQualificationId = qualificationEmp.TaskQualificationId;
            signOffVm.IsCriteriaMet = qualificationEmp.IsCriteriaMet;
            signOffVm.Comments = qualificationEmp.Comments;
            signOffVm.EvaluatorId = qualificationEmp.EvaluatorId;
            signOffVm.EvaluationMethodId = qualificationEmp.EvaluationMethodId;
            signOffVm.TaskQualificationDate = qualificationEmp.TaskQualificationDate;
            signOffVm.SignOffDate = qualificationEmp.SignOffDate;
            signOffVm.TraineeName = traineeName;
            signOffVm.EvaluatorName = evaluatorName;
            signOffVm.IsTraineeSignOff = qualificationEmp.IsTraineeSignOff;
            signOffVm.IsEvaluatorSignOff = qualificationEmp.IsEvaluatorSignOff;
            return signOffVm;
        }

        public async Task<TaskReQualificationEmp_SignOff> CreateOrUpdateSignOffAsync(TaskReQualificationEmpSignOffVM options)
        {
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

            var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();


            var employee = await _empService.FindQueryWithIncludeAsync(x => x.PersonId == person.Id, new string[] { "Person" }).FirstOrDefaultAsync();

            var qualificationEmp = await _empSignOffDomainService.FindQuery(x => x.TaskQualificationId == options.TaskQualificationId && x.TraineeId == options.TraineeId && x.EvaluatorId == employee.Id).FirstOrDefaultAsync();

            qualificationEmp.TaskQualificationId = options.TaskQualificationId;
            qualificationEmp.TaskQualificationDate = options.TaskQualificationDate;
            qualificationEmp.Comments = options.Comments;
            qualificationEmp.TraineeId = options.TraineeId;
            qualificationEmp.EvaluatorId = employee.Id;
            qualificationEmp.EvaluationMethodId = options.EvaluationMethodId;
            qualificationEmp.IsCriteriaMet = options.IsCriteriaMet;
            qualificationEmp.IsTraineeSignOff = options.IsTraineeSignOff;
            qualificationEmp.IsEvaluatorSignOff = options.IsEvaluatorSignOff;
            if (options.IsFormSubmitted.GetValueOrDefault())
            {
                qualificationEmp.SignOffDate = DateTime.UtcNow;
                qualificationEmp.IsCompleted = true;
            }
            else
            {
                qualificationEmp.SignOffDate = null;
                qualificationEmp.IsCompleted = null;
            }
            var validationResult = await _empSignOffDomainService.UpdateAsync(qualificationEmp);
            if (options.IsFormSubmitted.GetValueOrDefault() && validationResult.IsValid)
            {
                var listEvaluators = new List<string>();
                string RequiredRequals = string.Empty;
                var setting = await _tqEmpSettingService.FindQuery(x => x.TaskQualificationId == options.TaskQualificationId).FirstOrDefaultAsync();
                //recentTQ.EmpReleaseDate = setting.ReleaseDate;
                if (setting != null)
                {
                    var checkSignOffs = await _empSignOffDomainService.FindQueryWithIncludeAsync(x => x.TaskQualificationId == options.TaskQualificationId && x.IsCompleted == true, new string[] { "Evaluator.Person" }).ToListAsync();

                    //var listEvaluators = new List<string>();
                    if ((setting.MultipleSignOffDisplay == 1 || setting.ReleaseToAllSingleSignOff) && checkSignOffs.Count > 0)
                    {
                        var taskQualStatus = await _tqStatusService.FindQuery(x => x.Name == "Completed").Select(x => x.Id).FirstOrDefaultAsync();
                        var taskQual = await _taskQualificationDomainService.FindQuery(x => x.Id == options.TaskQualificationId).FirstOrDefaultAsync();
                        taskQual.TQStatusId = taskQualStatus;
                        taskQual.TaskQualificationDate = options.TaskQualificationDate?.ToUniversalTime();
                        taskQual.CriteriaMet = options.IsCriteriaMet ?? false;
                        taskQual.Completed();
                        await _taskQualificationDomainService.UpdateAsync(taskQual);

                    }
                    else
                    {
                        var requiredSignOff = setting.MultipleSignOffDisplay;
                        if (checkSignOffs.Count >= requiredSignOff)
                        {
                            var taskQualStatus = await _tqStatusService.FindQuery(x => x.Name == "Completed").Select(x => x.Id).FirstOrDefaultAsync();
                            var taskQual = await _taskQualificationDomainService.FindQuery(x => x.Id == options.TaskQualificationId).FirstOrDefaultAsync();
                            //string status = "";
                            //if (DateTime.Now.Date <= taskQual.DueDate.Value.Date)
                            //{
                            //    status = "On Time";
                            //}
                            //else if(DateTime.Now.Date >= taskQual.DueDate.Value.Date && DateTime.Now.Date <= taskQual.DueDate.Value.Date.AddMonths(6))
                            //{
                            //    status = "Delayed";
                            //}
                            taskQual.TQStatusId = taskQualStatus;
                            taskQual.TaskQualificationDate = options.TaskQualificationDate?.ToUniversalTime();
                            taskQual.CriteriaMet = options.IsCriteriaMet ?? false;
                            taskQual.Completed();
                            await _taskQualificationDomainService.UpdateAsync(taskQual);
                        }
                    }

                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }

            }
            return qualificationEmp;
        }

        public async Task<List<TaskReQualificationCompletedVM>> GetCompletedTaskRequalifications(bool isEvaluator)
        {
            //Get Current Employee 
            var completedTaskQualificationVM = new List<TaskReQualificationCompletedVM>();
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

            var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();


            var employee = await _empService.FindQueryWithIncludeAsync(x => x.PersonId == person.Id, new string[] { "Person" }).FirstOrDefaultAsync();
            var qualificationEmp = new List<TaskReQualificationEmp_SignOff>();
            if (isEvaluator)
            {
                qualificationEmp = await _empSignOffDomainService.FindQueryWithIncludeAsync(x => x.EvaluatorId == employee.Id && x.IsCompleted == true, new string[] { "TaskQualification.Task", "TaskQualification.TQEmpSetting" }).ToListAsync();
                if (qualificationEmp != null && qualificationEmp.Count > 0)
                {
                    foreach (var emp in qualificationEmp)
                    {
                        //Positons


                        var setting = await _tqEmpSettingService.FindQuery(x => x.TaskQualificationId == emp.TaskQualificationId).FirstOrDefaultAsync();
                        //recentTQ.EmpReleaseDate = setting.ReleaseDate;
                        if (setting.ReleaseToAllSingleSignOff)
                        {
                            //recentTQ.RequiredRequals = "One Sign Off Required - X/1";
                        }
                        else
                        {
                            //recentTQ.RequiredRequals = setting.MultipleSignOff + " Sign Offs Required - X/" + setting.MultipleSignOff;
                        }

                        var traineeemployee = await _empService.FindQueryWithIncludeAsync(x => x.Id == emp.TraineeId, new string[] { "Person" }).FirstOrDefaultAsync();
                        var employeeName = traineeemployee.Person.LastName + " " + traineeemployee.Person.FirstName;
                        var number = await _taskService.GetTaskNumberWithLetter(emp.TaskQualification.Task.SubdutyAreaId, emp.TaskQualification.Task.Id);

                        var concatenatedNumber = number.Letter + number.DANumber + "." + number.SDANumber + "." + number.TaskNumber;
                        completedTaskQualificationVM.Add(new TaskReQualificationCompletedVM()
                        {
                            TaskId = emp.TaskQualification.Task.Id,
                            TaskDescription = emp.TaskQualification.Task.Description,
                            TaskQualificationId = emp.TaskQualificationId,
                            TaskNumber = emp.TaskQualification.Task.Number,
                            ReleaseDate = setting.ReleaseDate,
                            DueDate = emp.TaskQualification.DueDate,
                            EmployeeName = employeeName,
                            TraineeId = traineeemployee.Id,
                            SignOffDate = emp.SignOffDate,
                            EvaluatorId = employee.Id,
                            ConcatenatedTaskNumber = concatenatedNumber,
                  
                        });
                    }


                }
            }
            else
            {
                
                string RequiredRequals = string.Empty;
                qualificationEmp = await _empSignOffDomainService.FindQueryWithIncludeAsync(x => x.TraineeId == employee.Id && x.IsCompleted == true, new string[] { "TaskQualification.Task", "TaskQualification.TQEmpSetting" }).ToListAsync();
                if (qualificationEmp != null && qualificationEmp.Count > 0)
                {
                    foreach (var emp in qualificationEmp)
                    {
                        var listEvaluatorsModel = new List<TQEvaluatorDate>();
                        var listEvaluators = new List<string>();
                        string positions = string.Empty;
                        var posIds = await _position_TaskService.FindQuery(x => x.TaskId == emp.TaskQualification.TaskId).Select(s => s.PositionId).ToArrayAsync();
                        var listPos = new List<string>();
                        foreach (var posId in posIds)
                        {
                            var position = await _positionService.GetAsync(posId);
                            if(position != null)
                            {
                                listPos.Add(position.PositionDescription);
                            }
                        }
                        if (listPos.Count > 0)
                        {
                            positions = string.Join(',', listPos);
                        }


                        var setting = await _tqEmpSettingService.FindQuery(x => x.TaskQualificationId == emp.TaskQualificationId).FirstOrDefaultAsync();
                        //recentTQ.EmpReleaseDate = setting.ReleaseDate;
                        if (setting.ReleaseToAllSingleSignOff)
                        {
                            var checkSignOffs = await _empSignOffDomainService.FindQueryWithIncludeAsync(x => x.TaskQualificationId == emp.TaskQualificationId && x.IsCompleted == true, new string[] { "Evaluator.Person" }).FirstOrDefaultAsync();
                            //var signOffs = checkSignOffs.Select(x => x.EvaluatorId).ToList();
                            //signoffs required
                            //var evaluators = await _evaluatorLinkService.FindQuery(x => x.TaskQualificationId == emp.TaskQualificationId).Select(x => x.EvaluatorId).ToListAsync();

                            var reaminingEvals = 0;
                            if(checkSignOffs != null)
                            {
                                reaminingEvals = 1;
                                var name = checkSignOffs.Evaluator.Person.FirstName + " " + checkSignOffs.Evaluator.Person.LastName;
                                var date = checkSignOffs.SignOffDate ?? null;
                                var concat = name + " - " + date;
                                listEvaluators.Add(concat);
                                listEvaluatorsModel.Add(new TQEvaluatorDate
                                {
                                    Name = name,
                                    signOffDate = date
                                });
                            }
                            RequiredRequals = "One Sign Off Required - " + reaminingEvals + "/1";
                           
                        }
                        else
                        {

                            //check how many signoffs are present
                            var checkSignOffs = await _empSignOffDomainService.FindQueryWithIncludeAsync(x => x.TaskQualificationId == emp.TaskQualificationId && x.IsCompleted == true, new string[] { "Evaluator.Person" }).ToListAsync();
                            var signOffs = checkSignOffs.Select(x => x.EvaluatorId).ToList();
                            //signoffs required
                            var evaluators = await _evaluatorLinkService.FindQuery(x => x.TaskQualificationId == emp.TaskQualificationId).Select(x => x.EvaluatorId).ToListAsync();

                            var reaminingEvals = evaluators.Count - signOffs.Count;
                            RequiredRequals = setting?.MultipleSignOffDisplay + " Sign Offs Required - " + checkSignOffs.Count + "/" + setting?.MultipleSignOffDisplay;

                            if (evaluators.Count > 0)
                            {
                                foreach (var evaluator in checkSignOffs)
                                {
                                    var name = evaluator.Evaluator.Person.FirstName + " " + evaluator.Evaluator.Person.LastName;
                                    var date = evaluator.SignOffDate ?? null;
                                    var concat = name + " - " + date;
                                    listEvaluators.Add(concat);
                                    listEvaluatorsModel.Add(new TQEvaluatorDate
                                    {
                                        Name=name,
                                        signOffDate=date
                                    });
                                }
                            }
                        }

                        var number = await _taskService.GetTaskNumberWithLetter(emp.TaskQualification.Task.SubdutyAreaId, emp.TaskQualification.Task.Id);
                        var concatenatedNumber = number.Letter + number.DANumber + "." + number.SDANumber + "." + number.TaskNumber;
                        completedTaskQualificationVM.Add(new TaskReQualificationCompletedVM()
                        {
                            TaskId = emp.TaskQualification.Task.Id,
                            TaskDescription = emp.TaskQualification.Task.Description,
                            TaskQualificationId = emp.TaskQualificationId,
                            TaskNumber = emp.TaskQualification.Task.Number,
                            ReleaseDate = setting.ReleaseDate,
                            DueDate = emp.TaskQualification.DueDate,
                            TraineeId = emp.TraineeId,
                            EvaluatorNamesWithDates = listEvaluators,
                            RequiredRequals = RequiredRequals,
                            EvaluatorId = emp.EvaluatorId,
                            Positions = positions,
                            ConcatenatedTaskNumber = concatenatedNumber,
                            EvaluatorNameDates= listEvaluatorsModel


                        }) ;
                    }
                }
            }

            return completedTaskQualificationVM;
            //return qualificationEmp;
        }
        public async Task<TaskReQualificatioFeedBackVM> GetFeedBackData(int qualificationId, int traineeId)
        {
            //Get Current Evaluator 
            var feedBackVM = new TaskReQualificatioFeedBackVM();

            var steps = await _empStepsService.FindQueryWithIncludeAsync(x => x.TaskQualificationId == qualificationId && x.TraineeId == traineeId, new string[] { "Task_Step" }).ToListAsync();
            if (steps.Count > 0)
            {
                foreach (var step in steps)
                {
                    var stepCommentsVM = new List<StepCommentsVM>();
                    var commentsOfEmployees = await _empStepsService.FindQuery(x => x.TaskStepId == step.TaskStepId).ToListAsync();
                    foreach (var commentWithEmployee in commentsOfEmployees)
                    {
                        var evaluator = await _empService.FindQueryWithIncludeAsync(x => x.Id == commentWithEmployee.EvaluatorId, new string[] { "Person" }).FirstOrDefaultAsync();
                        var evaluatorName = evaluator.Person.FirstName + " " + evaluator.Person.LastName;
                        stepCommentsVM.Add(new StepCommentsVM()
                        {
                            EmployeeName = evaluatorName,
                            Comment = commentWithEmployee.Comments,
                            CommentDate = commentWithEmployee.CommentDate,
                            Image = evaluator.Person.Image,

                        });
                    }
                    feedBackVM.StepsList.Add(new Steps()
                    {
                        StepId = step.TaskStepId,
                        StepDescription = step.Task_Step.Description,
                        EvaluatorsStepComments = stepCommentsVM,
                    });
                }
            }
            //Question Answers
            var questionAnswers = await _empQuestionAnswerService.FindQueryWithIncludeAsync(x => x.TaskQualificationId == qualificationId && x.TraineeId == traineeId, new string[] { "Task_Question" }).ToListAsync();
            if (questionAnswers.Count > 0)
            {
                foreach (var questionAnswer in questionAnswers)
                {
                    var questionAnswerCommentsVM = new List<QuestionAnswerCommentsVM>();
                    var questioncommentsOfEmployees = await _empQuestionAnswerService.FindQuery(x => x.TaskQuestionId == questionAnswer.TaskQuestionId).ToListAsync();
                    foreach (var questioncommentsOfEmployee in questioncommentsOfEmployees)
                    {
                        var evaluatorQA = await _empService.FindQueryWithIncludeAsync(x => x.Id == questioncommentsOfEmployee.EvaluatorId, new string[] { "Person" }).FirstOrDefaultAsync();
                        var evaluatorQAName = evaluatorQA.Person.FirstName + " " + evaluatorQA.Person.LastName;
                        questionAnswerCommentsVM.Add(new QuestionAnswerCommentsVM()
                        {
                            EmployeeName = evaluatorQAName,
                            Comment = questioncommentsOfEmployee.Comments,
                            CommentDate = questioncommentsOfEmployee.CommentDate,
                            Image = evaluatorQA.Person.Image,

                        });
                    }
                    feedBackVM.QuesionAnswerList.Add(new QuesionAnswer()
                    {
                        QuestionId = questionAnswer.TaskQuestionId,
                        QuestionDescription = questionAnswer.Task_Question.Question,
                        Answer = questionAnswer.Task_Question.Answer,
                        EvaluatorsQuestionAnswerComments = questionAnswerCommentsVM,
                    });
                }
            }

            var taskQual =  (await _taskQualificationDomainService.FindWithIncludeAsync(x => x.Id == qualificationId, new string[] { "Task" })).FirstOrDefault();
            var number = await _taskService.GetTaskNumberWithLetter(taskQual.Task.SubdutyAreaId, taskQual.Task.Id);
            var concatenatedNumber = number.Letter + number.DANumber + "." + number.SDANumber + "." + number.TaskNumber;
            feedBackVM.TaskDescription = taskQual.Task.Description;
            feedBackVM.concatednatedTaskNumber = concatenatedNumber;
            return feedBackVM;
        }

        public async Task<List<TaskReQualificationCompletedVM>> GetCompletedTaskRequalificationsByEmpId(bool isEvaluator, int employeeId)
        {
            var completedTaskQualificationVM = new List<TaskReQualificationCompletedVM>();
            var qualificationEmp = new List<TaskReQualificationEmp_SignOff>();
            qualificationEmp = await _empSignOffDomainService.GetTaskReQualificationEmp_SignOffByEmployeeId(employeeId);
            if (isEvaluator)
            {
                if (qualificationEmp != null && qualificationEmp.Count > 0)
                {
                    foreach (var emp in qualificationEmp)
                    {
                        var setting = await _tqEmpSettingService.GetTQEmpSettingByTQId(emp.TaskQualificationId);
                        var employeeName = await _empService.GetEmployeeNameByIdAsync(emp.TraineeId);
                       
                        var number = await _taskService.GetTaskNumberWithLetter(emp.TaskQualification.Task.SubdutyAreaId, emp.TaskQualification.Task.Id);
                        var concatenatedNumber = number.Letter + number.DANumber + "." + number.SDANumber + "." + number.TaskNumber;
                        var taskRequalification = new TaskReQualificationCompletedVM(emp.TaskQualificationId, emp.TaskQualification.Task.Id,  emp.TaskQualification.Task.Number, emp.TaskQualification.Task.Description, 
                            setting.ReleaseDate, emp.TaskQualification.DueDate, emp.TraineeId, employeeId, emp.SignOffDate, concatenatedNumber);
                        
                        taskRequalification.SetEmployeeName(employeeName);
                        completedTaskQualificationVM.Add(taskRequalification);
                    }
                }
            } else
            {
                if (qualificationEmp != null && qualificationEmp.Count > 0)
                {
                    foreach (var emp in qualificationEmp)
                    {
                        var listPos = new List<string>();
                        string positions = string.Empty;

                        var posIds = await _position_TaskService.FindQuery(x => x.TaskId == emp.TaskQualification.TaskId).Select(s => s.PositionId).ToListAsync();

                        foreach (var posId in posIds)
                        {
                            var position = await _positionService.GetAsync(posId);
                            if(position != null)
                            {
                                listPos.Add(position.PositionDescription);
                            }
                        }
                        if (listPos.Count > 0)
                        {
                            positions = string.Join(',', listPos);
                        }
                        var setting = await _tqEmpSettingService.FindQuery(x => x.TaskQualificationId == emp.TaskQualificationId).FirstOrDefaultAsync();

                        var listEvaluators = await GetEvaluatorsList(setting, emp);

                        var number = await _taskService.GetTaskNumberWithLetter(emp.TaskQualification.Task.SubdutyAreaId, emp.TaskQualification.Task.Id);
                        var concatenatedNumber = number.Letter + number.DANumber + "." + number.SDANumber + "." + number.TaskNumber;
                        
                        var taskRequalification = new TaskReQualificationCompletedVM(emp.TaskQualificationId, emp.TaskQualification.Task.Id, emp.TaskQualification.Task.Number, emp.TaskQualification.Task.Description,
                            setting.ReleaseDate, emp.TaskQualification.DueDate, emp.TraineeId, emp.EvaluatorId, emp.SignOffDate, concatenatedNumber);
                        
                        taskRequalification.SetPositions(positions);
                        taskRequalification.SetEvaluatorNamesWithDates(listEvaluators);

                        completedTaskQualificationVM.Add(taskRequalification);
                    }
                }
            }
            return completedTaskQualificationVM;
        }

        public async Task<List<string>> GetEvaluatorsList(TQEmpSetting setting, TaskReQualificationEmp_SignOff emp)
        {
            var listEvaluators = new List<string>();
            string RequiredRequals = string.Empty;
            if (setting.ReleaseToAllSingleSignOff)
            {
                var checkSignOffs = (await _empSignOffDomainService.GetTaskReQualificationEmp_SignOffByTQId(emp.TaskQualificationId)).FirstOrDefault();
                var reaminingEvals = 0;
                if (checkSignOffs != null)
                {
                    reaminingEvals = 1;
                    var name = checkSignOffs.Evaluator.Person.FirstName + " " + checkSignOffs.Evaluator.Person.LastName;
                    var date = checkSignOffs.SignOffDate ?? null;
                    var concat = name + " - " + date;
                    listEvaluators.Add(concat);
                }
                RequiredRequals = "One Sign Off Required - " + reaminingEvals + "/1";
            }
            else
            {
                var checkSignOffs = await _empSignOffDomainService.GetTaskReQualificationEmp_SignOffByTQId(emp.TaskQualificationId);
                var signOffs = checkSignOffs.Select(x => x.EvaluatorId).ToList();
                var evaluators = await _evaluatorLinkService.FindQuery(x => x.TaskQualificationId == emp.TaskQualificationId).Select(x => x.EvaluatorId).ToListAsync();
                var reaminingEvals = evaluators.Count - signOffs.Count;
                RequiredRequals = setting?.MultipleSignOffDisplay + " Sign Offs Required - " + reaminingEvals + "/" + setting?.MultipleSignOffDisplay;
                if (evaluators.Count > 1)
                {
                    foreach (var evaluator in checkSignOffs)
                    {
                        var name = evaluator.Evaluator.Person.FirstName + " " + evaluator.Evaluator.Person.LastName;
                        var date = evaluator.SignOffDate ?? null;
                        var concat = name + " - " + date;
                        listEvaluators.Add(concat);
                    }
                }
            }
            return listEvaluators;
        }
    }

   
}

