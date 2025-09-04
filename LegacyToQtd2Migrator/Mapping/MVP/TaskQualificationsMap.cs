using LegacyToQtd2Migrator.Helpers;
using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public struct UsedTaskQualifications
    {
        public int ParentId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime CompletedDate { get; set; }
    }

    public class TaskQualificationsMap : Common.MigrationMap<TblOjthistory, TaskQualification>
    {
        List<UsedTaskQualifications> UsedParentIds { get; set; }

        List<TblOjthistory> _ojthistories;
        List<TblOjthistoryQuestion> _ojthistoriesQuestions;
        List<TblOjthistoryStep> _ojthistoriesSteps;

        List<TblEmpsetting> _empSettings;
        List<TblEmployee> _employees;

        List<TaskQualificationStatus> _taskQualificationStatuses;

        List<Employee> _addedEmployees = new List<Employee>();

        List<Employee> _targetEmployees;
        List<Person> _targetPersons;
        List<EvaluationMethod> _targetEvalMethods;

        public TaskQualificationsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblOjthistory> getSourceRecords()
        {
            _ojthistories = (_source as EMP_DemoContext).TblOjthistories.ToList();
            _ojthistoriesQuestions = (_source as EMP_DemoContext).TblOjthistoryQuestions.ToList();
            _ojthistoriesSteps = (_source as EMP_DemoContext).TblOjthistorySteps.ToList();

            _ojthistories = (_source as EMP_DemoContext).TblOjthistories.ToList();

            _empSettings = (_source as EMP_DemoContext).TblEmpsettings.ToList();
            _employees = (_source as EMP_DemoContext).TblEmployees.ToList();

            _taskQualificationStatuses = (_target as QTD2.Data.QTDContext).TaskQualificationStatuses.ToList();
            _targetEmployees = (_target as QTD2.Data.QTDContext).Employees.ToList();
            _targetPersons = (_target as QTD2.Data.QTDContext).Persons.ToList();
            _targetEvalMethods = (_target as QTD2.Data.QTDContext).EvaluationMethods.ToList();

            UsedParentIds = new List<UsedTaskQualifications>();

            return _ojthistories;
        }

        protected override TaskQualification mapRecord(TblOjthistory obj)
        {
            var sourceEmployeeTask = (_source as EMP_DemoContext).RsTblEmployeesTasks.Include("TidNavigation.Da").Where(r => r.RsId == obj.ParentId).FirstOrDefault();

            if (sourceEmployeeTask == null) return null;

            if (obj.ReleaseDate.HasValue)
            {
                if (UsedParentIds
                .Where(r => r.ParentId == sourceEmployeeTask.RsId)
                .Where(r => r.ReleaseDate == obj.ReleaseDate.Value).Count() > 0) return null;

                UsedParentIds.Add(new UsedTaskQualifications()
                {
                    ParentId = sourceEmployeeTask.RsId,
                    ReleaseDate = obj.ReleaseDate.Value
                });
            }
            else if (obj.EvalDate.HasValue)
            {
                if (UsedParentIds
               .Where(r => r.ParentId == sourceEmployeeTask.RsId)
               .Where(r => r.CompletedDate == obj.EvalDate.Value).Count() > 0) return null;

                UsedParentIds.Add(new UsedTaskQualifications()
                {
                    ParentId = sourceEmployeeTask.RsId,
                    CompletedDate = obj.EvalDate.Value
                });
            }
            else
            {
                return null;
            }

            var sourceEmployee = sourceEmployeeTask.EidNavigation;
            var sourceTask = sourceEmployeeTask.TidNavigation;
            var sourceDutyArea = sourceEmployeeTask.TidNavigation.Da;

            string evalField = obj.Evaluator ?? "";
            bool containsComma = evalField.Contains(',');
            string[] parts = evalField.Replace(",", "").Split(" ");

            string evaluatorFirstName = containsComma ? (parts.Length > 1 ? parts[1] : "") : (parts.Length > 1 ? parts[0] : "");
            string evaluatorLastName = containsComma ? parts[0] : (parts.Length > 1 ? parts[1] : parts[0]);

            var targetEmployee = _targetEmployees.Where(r => r.EmployeeNumber == sourceEmployee.Enum && r.Person.FirstName == sourceEmployee.EfirstName && r.Person.LastName == sourceEmployee.ElastName).First();
            var targetEvaluator = _targetEmployees
                    .Where(r => String.IsNullOrEmpty(evaluatorFirstName) || r.Person.FirstName == evaluatorFirstName)
                    .Where(r => r.Person.LastName == evaluatorLastName).FirstOrDefault();

            var targetTask = (_target as QTD2.Data.QTDContext).Tasks
                .Where(r => r.Number == sourceTask.Tnum
                                && r.SubdutyArea.SubNumber == sourceDutyArea.DasubNum
                                && r.SubdutyArea.DutyArea.Number == sourceDutyArea.Danum
                                && r.SubdutyArea.DutyArea.Letter == sourceDutyArea.Daletter).FirstOrDefault();

            var sourceEvalMethod = obj.EvalMethod;
            var targetEvalMethod = _targetEvalMethods.Where(r => r.Description.ToUpper() == (sourceEvalMethod ?? "").ToUpper()).FirstOrDefault();

            List<TblOjthistory> histories = new List<TblOjthistory>();

            if (obj.ReleaseDate.HasValue)
            {
                histories = _ojthistories.Where(r => r.ParentId == obj.ParentId).Where(r => r.ReleaseDate.HasValue && r.ReleaseDate.Value == obj.ReleaseDate.Value).ToList();
            }
            else if (obj.EvalDate.HasValue)
            {
                histories = _ojthistories.Where(r => r.ParentId == obj.ParentId).Where(r => r.EvalDate.HasValue && r.EvalDate.Value == obj.EvalDate.Value).ToList();
            }
            else
            {
                histories = new List<TblOjthistory>()
                {
                    obj
                };
            }

            var statuses = histories.Select(r => getStatus(r));

            var status = getStatus(obj);

            if (statuses.Where(r => r.Name == "On Time").Count() > 0)
            {
                status = statuses.Where(r => r.Name == "On Time").First();
            }

            if (statuses.Where(r => r.Name == "Completed").Count() > 0)
            {
                status = statuses.Where(r => r.Name == "Completed").First();
            }


            var evaluators = getTargetEvaluatorLinks(obj, sourceEmployee);

            evaluators = evaluators.Where(r => r.Evaluator.Id != 0).GroupBy(r => r.Evaluator.Id).Select(r => r.First()).ToList().Union(evaluators.Where(r => r.Evaluator.Id == 0).GroupBy(r => new { r.Evaluator.Person.FirstName, r.Evaluator.Person.LastName }).Select(r => r.First())).ToList();

            foreach (var evaluator in evaluators)
            {
                evaluator.Number = evaluators.IndexOf(evaluator);
            }

            var evalDate = histories.Max(r => r.EvalDate);

            return new TaskQualification()
            {
                TaskId = targetTask.Id,
                EmpId = targetEmployee.Id,
                //EvaluationId
                TaskQualificationDate = evalDate.HasValue ? evalDate.Value.ToQtsTime(false) : null,
                TaskQualificationEvaluator = evaluatorFirstName + " " + evaluatorLastName,
                TaskQualification_Evaluator_Links = evaluators,
                TaskReQualificationEmp_SignOff = getSignOffs(obj, sourceEmployee, targetEmployee, evaluators),
                TaskReQualificationEmp_QuestionAnswers = getQuestionAnswers(obj, targetTask),
                TaskReQualificationEmp_Steps = getSteps(obj, targetTask),
                //DueDate 
                //CriteriaMet
                Comments = obj.Comments,
                IsReleasedToEMP = obj.ReleaseDate.HasValue,
                Evaluator = targetEvaluator,
                //CreatedBy
                //CreatedDate
                //ModifiedBy
                //ModifiedDate,
                CriteriaMet = histories.Max(r => r.Complete),
                TaskQualificationStatus = status,
                TQEmpSetting = getSetting(obj),
                EvaluationMethod = targetEvalMethod,
                Deleted = false,
                Active = true,
            };
        }

        private ICollection<TaskReQualificationEmp_Steps> getSteps(TblOjthistory history, Task targetTask)
        {
            List<TaskReQualificationEmp_Steps> steps = new List<TaskReQualificationEmp_Steps>();

            var sourceSteps = _ojthistoriesSteps.Where(r => r.Ojthid == history.Ojthid);

            foreach (var sourceStep in sourceSteps)
            {
                var sourceEmployee = sourceStep.EidNavigation;
                var sourceEvaluator = sourceStep.EvalE;

                var targetEmployee = _targetEmployees.Where(r => r.EmployeeNumber == sourceEmployee.Enum && r.Person.FirstName == sourceEmployee.EfirstName && r.Person.LastName == sourceEmployee.ElastName).First();
                var targetEvaluator = _targetEmployees.Where(r => r.EmployeeNumber == sourceEvaluator.Enum && r.Person.FirstName == sourceEvaluator.EfirstName && r.Person.LastName == sourceEvaluator.ElastName).First();

                var targetTaskStep = (_target as QTD2.Data.QTDContext).Task_Steps.Where(r => r.TaskId == targetTask.Id).Where(r => r.Description == sourceStep.StepDesc).FirstOrDefault();

                if (targetTaskStep == null) continue;

                steps.Add(new TaskReQualificationEmp_Steps()
                {
                    Active = true,
                    CommentDate = history.EvalDate.GetValueOrDefault().ToQtsTime(false),
                    Comments = sourceStep.Comment,
                    Evaluator = targetEvaluator,
                    IsCompleted = sourceStep.IsQualified,
                    TaskStepId = targetTaskStep.Id,
                    TraineeId = targetEmployee.Id
                });
            }

            return steps;
        }

        private ICollection<TaskReQualificationEmp_QuestionAnswer> getQuestionAnswers(TblOjthistory history, Task targetTask)
        {
            List<TaskReQualificationEmp_QuestionAnswer> qas = new List<TaskReQualificationEmp_QuestionAnswer>();

            var sourceQas = _ojthistoriesQuestions.Where(r => r.Ojthid == history.Ojthid);

            foreach (var sourceQa in sourceQas)
            {
                var sourceEmployee = sourceQa.EidNavigation;
                var sourceEvaluator = sourceQa.EvalE;

                var targetEmployee = _targetEmployees.Where(r => r.EmployeeNumber == sourceEmployee.Enum && r.Person.FirstName == sourceEmployee.EfirstName && r.Person.LastName == sourceEmployee.ElastName).First();
                var targetEvaluator = _targetEmployees.Where(r => r.EmployeeNumber == sourceEvaluator.Enum && r.Person.FirstName == sourceEvaluator.EfirstName && r.Person.LastName == sourceEvaluator.ElastName).First();

                var targetTaskQuestion = (_target as QTD2.Data.QTDContext).Task_Questions.Where(r => r.TaskId == targetTask.Id).Where(r => r.Question == sourceQa.Tqquestion).FirstOrDefault();

                if (targetTaskQuestion == null) continue;

                qas.Add(new TaskReQualificationEmp_QuestionAnswer()
                {
                    Active = true,
                    CommentDate = history.EvalDate.GetValueOrDefault().ToQtsTime(false),
                    Comments = sourceQa.Comment,
                    Deleted = false,
                    Evaluator = targetEvaluator,
                    IsCompleted = history.Complete,
                    TaskQuestionId = targetTaskQuestion.Id,
                    TraineeId = targetEmployee.Id
                });
            }

            return qas;
        }

        private ICollection<TaskReQualificationEmp_SignOff> getSignOffs(TblOjthistory history, TblEmployee sourceEmployee, Employee targetEmployee, List<TaskQualification_Evaluator_Link> evalLinks)
        {
            List<TaskReQualificationEmp_SignOff> signOffs = new List<TaskReQualificationEmp_SignOff>();

            var sourceEvalMethod = history.EvalMethod;
            var targetEvalMethod = _targetEvalMethods.Where(r => r.Description.ToUpper() == (sourceEvalMethod ?? "").ToUpper()).FirstOrDefault();

            DateTime? signOffDate = history.ReleaseDate.HasValue ? history.ReleaseDate.Value : null;

            foreach (var evalLink in evalLinks)
            {
                signOffs.Add(new TaskReQualificationEmp_SignOff()
                {
                    Active = true,
                    Comments = history.Comments,
                    Deleted = false,
                    EvaluationMethodId = targetEvalMethod == null ? null : targetEvalMethod.Id,
                    //EvaluatorId = evalLink.EvaluatorId,
                    Evaluator = evalLink.Evaluator,
                    IsCompleted = history.Complete,
                    IsCriteriaMet = history.Complete,
                    IsLocked = false,
                    IsEvaluatorSignOff = true,
                    IsTraineeSignOff = history.HasTraineeSigned,
                    IsStarted = history.Complete,
                    SignOffDate = signOffDate.HasValue ? signOffDate.Value.ToQtsTime(false) : null,
                    TraineeId = targetEmployee.Id
                });
            }

            return signOffs;
        }

        private List<TaskQualification_Evaluator_Link> getEvaluatorsFromOtherTaskQuals(List<TblOjthistory> histories, TblEmployee sourceEmployee)
        {
            List<TaskQualification_Evaluator_Link> evalLinks = new List<TaskQualification_Evaluator_Link>();

            foreach (var history in histories)
            {
                evalLinks.AddRange(getTargetEvaluatorLinks(history, sourceEmployee));
            }

            return evalLinks;
        }

        private List<TaskQualification_Evaluator_Link> getTargetEvaluatorLinks(TblOjthistory obj, TblEmployee sourceEmployee)
        {
            List<TaskQualification_Evaluator_Link> links = new List<TaskQualification_Evaluator_Link>();

            var evaluators = String.IsNullOrEmpty(obj.Evaluator) ? new List<string>() : obj.Evaluator.Split("/").ToList();
            var targetEmployee = _targetEmployees.Where(r => r.EmployeeNumber == sourceEmployee.Enum && r.Person.FirstName == sourceEmployee.EfirstName && r.Person.LastName == sourceEmployee.ElastName).First();

            foreach (var eval in evaluators)
            {
                bool containsComma = eval.Contains(',');
                string[] parts = eval.Replace(",", "").Split(" ");

                string evaluatorFirstName = containsComma ? (parts.Length > 1 ? parts[1] : "") : (parts.Length > 1 ? parts[0] : "");
                string evaluatorLastName = containsComma ? parts[0] : (parts.Length > 1 ? parts[1] : parts[0]);

                Employee targetEvaluator = _targetEmployees
                       .Where(r => String.IsNullOrEmpty(evaluatorFirstName) || r.Person.FirstName == evaluatorFirstName)
                       .Where(r => r.Person.LastName == evaluatorLastName).FirstOrDefault();

                if (targetEvaluator == null)
                {
                    targetEvaluator = _targetEmployees
                       .Where(r => String.IsNullOrEmpty(evaluatorLastName) || r.Person.FirstName == evaluatorLastName)
                       .Where(r => r.Person.LastName == evaluatorFirstName).FirstOrDefault();
                }

                if (targetEvaluator == null)
                {
                    targetEvaluator = _addedEmployees
                       .Where(r => String.IsNullOrEmpty(evaluatorFirstName) || r.Person.FirstName == evaluatorFirstName)
                       .Where(r => r.Person.LastName == evaluatorLastName).FirstOrDefault();

                    if (targetEvaluator == null)
                    {
                        var person = _targetPersons
                        .Where(r => String.IsNullOrEmpty(evaluatorFirstName) || r.FirstName == evaluatorFirstName)
                        .Where(r => r.LastName == evaluatorLastName)
                        .FirstOrDefault();

                        targetEvaluator = new Employee()
                        {
                            Person = person == null ? new Person()
                            {
                                Active = false,
                                FirstName = evaluatorFirstName,
                                LastName = evaluatorLastName,
                                Username = "",
                                Deleted = false
                            } : person,
                            Active = false,
                            Deleted = false,
                            EmployeeNumber = evaluatorFirstName + evaluatorLastName
                        };

                        _addedEmployees.Add(targetEvaluator);
                    }
                }

                links.Add(new TaskQualification_Evaluator_Link()
                {
                    Active = true,
                    Evaluator = targetEvaluator,
                    Number = evaluators.IndexOf(eval)
                });
            }

            if (obj.EvalEid.GetValueOrDefault() > 0)
            {
                var sourceEvaluator = _employees.Where(r => r.Eid == obj.EvalEid).FirstOrDefault();

                if (sourceEvaluator != null)
                {
                    var targetEvaluator = _targetEmployees.Where(r => r.Person.FirstName == sourceEvaluator.EfirstName).Where(r => r.Person.LastName == sourceEvaluator.ElastName).FirstOrDefault();

                    if (links.Where(r => r.EvaluatorId == targetEvaluator.Id || (r.Evaluator?.Id ?? -1) == targetEvaluator.Id).Count() == 0)
                    {
                        links.Add(new TaskQualification_Evaluator_Link()
                        {
                            Active = true,
                            Evaluator = targetEvaluator,
                            Number = evaluators.Count() + 1
                        });
                    }

                }
            }

            return links;
        }

        private TaskQualificationStatus getStatus(TblOjthistory obj)
        {
            if (obj.Complete) return _taskQualificationStatuses.Where(r => r.Name == "On Time").First();

            if (obj.EvalDate < DateTime.Now)
                if (obj.EvalDate.Value.AddMonths(6) > DateTime.Now) return _taskQualificationStatuses.Where(r => r.Name == "Delayed").First();
                else return _taskQualificationStatuses.Where(r => r.Name == "Overdue").First();

            return _taskQualificationStatuses.Where(r => r.Name == "On Time").First();
        }

        private TQEmpSetting getSetting(TblOjthistory obj)
        {
            var signOffsRequired = _empSettings.Where(r => r.EmpsettingDesc.ToUpper() == "TaskQual_SignoffsRequired".ToUpper()).FirstOrDefault();
            return new TQEmpSetting()
            {
                Active = true,
                Deleted = false,
                ReleaseToAllSingleSignOff = signOffsRequired == null ? false : Convert.ToInt32(signOffsRequired.EmpsettingValue) == 1,
                MultipleSignOff = signOffsRequired == null ? 0 : Convert.ToInt32(signOffsRequired.EmpsettingValue),
                ReleaseDate = obj.ReleaseDate.HasValue ? obj.ReleaseDate.Value.ToQtsTime(false) : obj.ReleaseDate.ToQtsTime(false),
                ReleaseInSpecificOrder = false,
                ReleaseOnReleaseDate = true,
                ShowTaskQuestions = false,
                ShowTaskSuggestions = false,
            };
        }

        private TaskQualificationStatus getTaskQualificationStatusId(TblOjthistory obj, TblEmployee sourceEmployee, TblTask sourceTask)
        {
            TaskQualificationStatus trainieeInitialQualification = _taskQualificationStatuses.Where(r => r.Name == "Trainee Initial Qualification").First();
            TaskQualificationStatus onTime = _taskQualificationStatuses.Where(r => r.Name == "On Time").First();
            TaskQualificationStatus pending = _taskQualificationStatuses.Where(r => r.Name == "Pending").First();
            TaskQualificationStatus delayed = _taskQualificationStatuses.Where(r => r.Name == "Delayed").First();
            TaskQualificationStatus overdue = _taskQualificationStatuses.Where(r => r.Name == "Overdue").First();
            TaskQualificationStatus requalificationNotRequired = _taskQualificationStatuses.Where(r => r.Name == "Requalification Not Required").First();
            TaskQualificationStatus noPositionQualDate = _taskQualificationStatuses.Where(r => r.Name == "No Position Qual Date").First();
            TaskQualificationStatus completed = _taskQualificationStatuses.Where(r => r.Name == "Completed").First();
            TaskQualificationStatus unmappable = _taskQualificationStatuses.Where(r => r.Name == "Unmappable").First();

            var taskHistories = (_source as EMP_DemoContext).TblTasksHistories.Where(r => r.Tid == sourceTask.Tid);
            var taskPositions = (_source as EMP_DemoContext).RstblPositionsTasks.Where(r => r.Tid == sourceTask.Tid).Select(r => r.PidNavigation);

            //TblEmployees
            //TblEmployeeAdditionalPositions
            //TblEmployeePositionHistories
            //var employeePositions = (new[] { new { PositionId = sourceEmployee.Pid.GetValueOrDefault(), PositionStartDate = sourceEmployee.PosStartDate, PositionEndDate = sourceEmployee.PosEndDate, PositionQualitificationDate = sourceEmployee.PosQualDate, IsTrainee = sourceEmployee.Trainee, Active = sourceEmployee.PosEndDate.HasValue } }).ToList();
            var employeePositions = (_source as EMP_DemoContext).TblEmployeeAdditionalPositions.Where(r => r.Eid == sourceEmployee.Eid).Select(r => new { PositionId = r.Pid, PositionStartDate = r.StartDate, PositionEndDate = r.EndDate, PositionQualitificationDate = r.QualificationDate, IsTrainee = r.Trainee, Active = r.EndDate.HasValue }).ToList();


            var employeeHistories = (_source as EMP_DemoContext).TblEmployeePositionHistories.Where(r => r.Eid == sourceEmployee.Eid).Select(r => new { PositionId = r.Pid, PositionStartDate = r.StartDate, PositionEndDate = r.EndDate, PositionQualitificationDate = r.QualificationDate, IsTrainee = r.Trainee, Active = r.EndDate.HasValue });

            foreach (var employeeHistory in employeeHistories)
            {
                var employeePosition = employeePositions.Where(r => r.PositionId == employeeHistory.PositionId).FirstOrDefault();

                if (employeePosition == null)
                {
                    employeePositions.Add(employeeHistory);
                }
                else
                {
                    var positionId = employeePosition.PositionId;
                    var positionQualitificationDate = employeePosition.PositionQualitificationDate == null ? employeeHistory.PositionQualitificationDate : null;
                    var positionStartDate = employeePosition.PositionStartDate == null ? employeeHistory.PositionStartDate : null;
                    var positionEndDate = employeePosition.PositionEndDate == null ? employeeHistory.PositionEndDate : null;
                    var isTrainee = employeePosition.IsTrainee == null ? employeeHistory.IsTrainee : null;
                    var active = employeePosition.Active;

                    employeePositions.Remove(employeeHistory);
                    employeePositions.Add(new
                    {
                        PositionId = positionId,
                        PositionStartDate = positionStartDate.HasValue ? positionStartDate.Value : positionStartDate,
                        PositionEndDate = positionEndDate.HasValue ? positionEndDate.Value : positionEndDate,
                        PositionQualitificationDate = positionQualitificationDate.HasValue ? positionQualitificationDate.Value : positionQualitificationDate,
                        IsTrainee = isTrainee,
                        Active = active
                    });
                }
            }

            if (employeePositions.Where(r => r.PositionStartDate.HasValue).Count() == 0)
            {
                return unmappable;
            }

            var employeeTaskPositions = employeePositions.Where(r => taskPositions.Select(r => r.Pid).Contains(r.PositionId)).ToList();
            DateOnly taskLastChange = taskHistories.Count() == 0 ? DateOnly.MinValue : taskHistories.Max(r => r.Thdate.HasValue ? DateOnly.FromDateTime( r.Thdate.Value) : DateOnly.MinValue);
            DateOnly earliestPositionChangeDate = employeePositions.Where(r => r.PositionStartDate.HasValue).Count() == 0 ? DateOnly.MinValue : employeePositions.Where(r => r.PositionStartDate.HasValue).Min(r => DateOnly.FromDateTime(r.PositionStartDate.Value));
            bool qualified = obj.EvalDate.HasValue;
            bool isCurrentTrainee = employeeTaskPositions.Where(r => r.Active).Count() == 0 ? false : employeeTaskPositions.Where(r => r.Active).Min(r => r.IsTrainee.GetValueOrDefault());
            bool isHistoricallyTrainee = employeeTaskPositions.Count() == 0 ? false : employeeTaskPositions.Min(r => r.IsTrainee.GetValueOrDefault());
            DateOnly requalificationExpirationDate = (earliestPositionChangeDate > taskLastChange ? earliestPositionChangeDate : taskLastChange).AddMonths(6);


            if (qualified)
            {
                if (requalificationExpirationDate >= DateOnly.FromDateTime(obj.EvalDate.Value)) return onTime;

                if (requalificationExpirationDate < DateOnly.FromDateTime(obj.EvalDate.Value)) return delayed;
            }
            else
            {
                if (employeePositions.Max(r => r.PositionQualitificationDate.HasValue) == false) return noPositionQualDate;

                if (isHistoricallyTrainee) return trainieeInitialQualification;

                if (isCurrentTrainee && taskLastChange > earliestPositionChangeDate) return requalificationNotRequired;

                if (requalificationExpirationDate >= DateOnly.FromDateTime(DateTime.Now)) return pending;

                if (requalificationExpirationDate < DateOnly.FromDateTime(DateTime.Now)) return overdue;
            }

            return completed;


            // link an employee to a task, and then create a qualification from these and determine the qualification status

            // figure out what position causes the task to link to this employee
            // we can't worry about if they were hisotirically a trainee (data lost)
            // we should only look at active positions
            // IsTrainee = Min(ActivePositions.IsTrainee)

            //"qualified" means OJTHistory.EvalDate != null

            //for ontime -> use "HistoricalRqualDateWinow"
            // requalification date == max(AllPositions.Min(PositionStartDate) and tblTaskHistory.ThDate)

            //for pending -> use "RequalDateWindow"
            // requalification date == max(AllPositions.Min(PositionStartDate) and tblTaskHistory.ThDate)

            //for delayed -> use "HisotircalRequalDateWindow"

            //overdue -> use "HistoricalRequalDateWindow"

            //requalification not required
            // only look at Active positions
            // inTrainee = ActivePositions.Min(IsTrainee)
            // position start date and taskhistory.thdate and is trainne
            // if isTrainee and thDate > positionStartDate

            //No Position Qual Date
            // AllPositions have to not have a PositionQualDate
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _ojthistories.Count();
        }
        protected override void updateTarget(TaskQualification record)
        {
            if (record == null) return;

            (_target as QTD2.Data.QTDContext).TaskQualifications.Add(record);
        }
    }
}
