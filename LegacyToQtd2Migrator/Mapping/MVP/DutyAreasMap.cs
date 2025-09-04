using LegacyToQtd2Migrator.Helpers;
using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class DutyAreasMap : Common.MigrationMap<TblDutyArea, DutyArea>
    {
        List<TblDutyArea> _dutyAreas;
        List<TblDutyArea> _subDutyAreas;
        List<TblTasksQuestion> _tblTaskQuestion;
        List<TblTask> _taskSteps;
        List<TblTasksAuditHistory> _taskAuditHistories;
        List<TblPositionTasksHistory> _positionTaskHistories;
        List<TblTaskAuditChangeType> _taskAuditChangeTypes;
        List<TblTasksIntroduction> _taskInstructions;
        List<TblTaskIntroductionType> _taskInstructionType;

        List<Task_SuggestionTypes> _suggestionTypes;

        public DutyAreasMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblDutyArea> getSourceRecords()
        {
            _dutyAreas = (_source as EMP_DemoContext).TblDutyAreas.Where(r => !r.DasubNum.HasValue || r.DasubNum.Value == 0).ToListAsync().Result;
            _subDutyAreas = (_source as EMP_DemoContext).TblDutyAreas.Where(r => r.DasubNum.HasValue && r.DasubNum.Value != 0).ToListAsync().Result;
            _tblTaskQuestion = (_source as EMP_DemoContext).TblTasksQuestions.ToListAsync().Result;
            _taskSteps = (_source as EMP_DemoContext).TblTasks.ToListAsync().Result;
            _taskAuditHistories = (_source as EMP_DemoContext).TblTasksAuditHistories.ToList();
            _positionTaskHistories = (_source as EMP_DemoContext).TblPositionTasksHistories.ToList();
            _taskAuditChangeTypes = (_source as EMP_DemoContext).TblTaskAuditChangeTypes.ToList();
            _taskInstructions = (_source as EMP_DemoContext).TblTasksIntroductions.ToList();
            _taskInstructionType = (_source as EMP_DemoContext).TblTaskIntroductionTypes.ToList();

            _suggestionTypes = (_target as QTD2.Data.QTDContext).Task_SuggestionTypes.ToList();

            return _dutyAreas;
        }

        protected override DutyArea mapRecord(TblDutyArea obj)
        {
            List<TblDutyArea> others = _subDutyAreas.Where(r => r.Daletter == obj.Daletter).Where(r => r.Danum == obj.Danum).ToList();

            return new DutyArea()
            {
                Active = true,
                Title = obj.Dadesc,
                Description = obj.Dadesc,
                Letter = obj.Daletter,
                Number = obj.Danum ?? 0,
                //EffectiveDate,
                //ReasonForRevision
                SubdutyAreas = getSubdutyAreas(others),
            };
        }
        private ICollection<SubdutyArea> getSubdutyAreas(List<TblDutyArea> others)
        {
            List<SubdutyArea> subdutyAreas = new List<SubdutyArea>();

            foreach (var obj in others)
            {
                var tasks = (_source as EMP_DemoContext).TblTasks.Where(r => r.Daid == obj.Daid).ToList();

                subdutyAreas.Add(new SubdutyArea()
                {
                    SubNumber = obj.DasubNum.Value,
                    Description = obj.Dadesc,
                    Title = obj.Dadesc,
                    //EffectiveDate,
                    //ReasonForRevision
                    Deleted = false,
                    Active = true,
                    Tasks = getTasks(tasks)
                });
            }
            return subdutyAreas;
        }

        private ICollection<QTD2.Domain.Entities.Core.Task> getTasks(List<TblTask> sourceTasks)
        {
            List<QTD2.Domain.Entities.Core.Task> tasks = new List<QTD2.Domain.Entities.Core.Task>();

            foreach (var sourceTask in sourceTasks.Where(r => r.TsubNum == 0))
            {
                var positionTasks = (_source as EMP_DemoContext).RstblPositionsTasks.Where(r => r.Tid == sourceTask.Tid).ToList();

                var isRelibaility = positionTasks.Count() == 0 ? false : positionTasks.Max(r => r.Critical);

                var task = new QTD2.Domain.Entities.Core.Task()
                {
                    Number = sourceTask.Tnum ?? -1,
                    Abreviation = sourceTask.Tdesc,
                    Conditions = sourceTask.Tconditions,
                    Critical = sourceTask.Critical,
                    Criteria = sourceTask.Tstandards,
                    Description = sourceTask.Tdesc,
                    References = sourceTask.Treferences,
                    //RequiredTime
                    //IsMetaS
                    IsReliability = isRelibaility,
                    //EffectiveDate
                    Task_Suggestions = getTask_Suggestions(sourceTask),
                    Task_Steps = getTask_Steps(sourceTasks.Where(r => r.TsubNum != 0).Where(r => r.Tnum == sourceTask.Tnum).ToList()),
                    Task_Questions = getTask_Questions(sourceTask),
                    Task_TrainingGroups = getTask_TrainingGroups(sourceTask),
                    Task_Tools = getTaskTools(sourceTask),
                    //Task_Histories = getTaskHistories(sourceTask),
                    Version_Tasks = getTaskVersions(sourceTask),
                    Active = sourceTask.Inactive.HasValue ? !sourceTask.Inactive.Value : true
                };

                task.EffectiveDate = task.Version_Tasks.Max(vt => vt.EffectiveDate) ?? new DateOnly(2009, 10, 2);  // Max existing Version_Task.EffectiveDate, else the IndustryDefault date (per https://qtsteam.atlassian.net/browse/QR20-7495?focusedCommentId=64571)

                tasks.Add(task);
            }

            return tasks;
        }

        private ICollection<Version_Task> getTaskVersions(TblTask sourceTask)
        {
            List<Version_Task> taskVersions = new List<Version_Task>();

            var sourceTaskHistories = (_source as EMP_DemoContext).TblTasksHistories.Where(r => r.Tid == sourceTask.Tid).ToList();
            sourceTaskHistories = sourceTaskHistories.OrderBy(r => r.ChangeDateStamp.GetValueOrDefault()).ToList();

            foreach (var sourceTaskHistory in sourceTaskHistories)
            {
                taskVersions.Add(new Version_Task()
                {
                    Abbreviation = sourceTask.Tabbrev,
                    Conditions = sourceTaskHistory.Thconditions,
                    Critical = sourceTask.Critical, //sourceTaskHistory.Crit,
                    Criteria = "",
                    VersionNumber = (sourceTaskHistories.IndexOf(sourceTaskHistory) + 1).ToString(),
                    Description = sourceTask.Tdesc,
                    References = sourceTask.Treferences,
                    EffectiveDate = DateOnly.FromDateTime(sourceTaskHistory.Thdate.GetValueOrDefault()),
                    RequalificationNotes = sourceTaskHistory.ReviewComment,
                    RequalificationRequired = sourceTaskHistory.RequalRequired,
                    RequalificationDueDate = sourceTaskHistory.ReviewDate.HasValue ? DateOnly.FromDateTime(sourceTaskHistory.ReviewDate.Value): null,
                    //RequiredTime
                    //IsMetaSAAAAAAA
                    //IsReliability = sourceTaskHistory.rr
                    //EffectiveDate
                    //Task_Histories = getTaskHistories(sourceTaskHistory),
                    State = sourceTaskHistory.Thid,
                    TaskActive = sourceTask.Inactive.HasValue ? !sourceTask.Inactive.Value : true,
                    Active = true,
                    CreatedBy = sourceTaskHistory.ThrevisedBy,
                    ModifiedBy = sourceTaskHistory.ThrevisedBy
                });
            }

            return taskVersions;
        }

        private ICollection<Task_History> getTaskHistories(TblTask sourceTask)
        {
            List<Task_History> taskHistories = new List<Task_History>();

            var sourceTaskHistories = (_source as EMP_DemoContext).TblTasksHistories.Where(r => r.Tid == sourceTask.Tid);


            foreach (var sourceTaskHistory in sourceTaskHistories)
            {
                var taskAuditHistory = _taskAuditHistories.Where(r => r.Thid == sourceTaskHistory.Thid).FirstOrDefault();
                var positionTaskHistory = _positionTaskHistories.Where(r => r.Thid == sourceTaskHistory.Thid).FirstOrDefault();
                var changeType = taskAuditHistory == null || taskAuditHistory.ChangeType == null ? null : _taskAuditChangeTypes.Where(r => r.Tact == taskAuditHistory.ChangeType).First();

                string thProcList = sourceTaskHistory.ThprocList;
                char thProcListFirstChar = thProcList.Length == 0 ? ' ' : thProcList[0];
                bool setAsThProcList = thProcListFirstChar == '*';

                taskHistories.Add(new Task_History()
                {
                    ChangeEffectiveDate = sourceTaskHistory.ChangeDateStamp.GetValueOrDefault().ToQtsTime(false),
                    Active = true,
                    ChangeNotes = setAsThProcList ? "Task Procedure List" :( changeType == null ? "" : changeType.TaskAuditChangeType),
                    NewStatus = Convert.ToBoolean(sourceTaskHistory.Inactive.Replace("*", "")),
                    OldStatus = true
                });
            }

            return taskHistories;
        }

        private ICollection<Task_History> getTaskHistories(TblTasksHistory sourceTaskHistory)
        {
            List<Task_History> taskHistories = new List<Task_History>();

            var taskAuditHistory = _taskAuditHistories.Where(r => r.Thid == sourceTaskHistory.Thid).FirstOrDefault();
            var positionTaskHistory = _positionTaskHistories.Where(r => r.Thid == sourceTaskHistory.Thid).FirstOrDefault();
            var changeType = taskAuditHistory == null || taskAuditHistory.ChangeType == null ? null : _taskAuditChangeTypes.Where(r => r.Tact == taskAuditHistory.ChangeType).First();

            taskHistories.Add(new Task_History()
            {
                ChangeEffectiveDate = sourceTaskHistory.ChangeDateStamp.GetValueOrDefault().ToQtsTime(false),
                Active = true,
                ChangeNotes = changeType == null ? "" : changeType.TaskAuditChangeType,
                NewStatus = Convert.ToBoolean(sourceTaskHistory.Inactive.Replace("*", "")),
                OldStatus = true
            });

            return taskHistories;
        }

        private ICollection<Task_TrainingGroup> getTask_TrainingGroups(TblTask sourceTask)
        {
            List<Task_TrainingGroup> trainingGroups = new List<Task_TrainingGroup>();

            return trainingGroups;
        }

        private ICollection<Task_Tool> getTaskTools(TblTask sourceTask)
        {
            List<Task_Tool> taskTools = new List<Task_Tool>();

            if (String.IsNullOrEmpty(sourceTask.Ttools)) return taskTools;

            var toolsStringArray = sourceTask.Ttools.Split(new string[] { "\r\n", "\r", "\n", "," }, StringSplitOptions.RemoveEmptyEntries).Select(r => r.Trim()).Where(r => !String.IsNullOrEmpty(r)).Distinct();
            var nextToolNumber = (_target as QTD2.Data.QTDContext).Tools.Max(r => r.Number);

            foreach (var toolString in toolsStringArray)
            {
                var targetTool = (_target as QTD2.Data.QTDContext).Tools.Where(r => r.Name == toolString).FirstOrDefault();

                if (targetTool == null)
                {
                    nextToolNumber = (Convert.ToInt32(nextToolNumber) + 1).ToString();

                    taskTools.Add(new Task_Tool()
                    {
                        Tool = new Tool()
                        {
                            Active = true,
                            Deleted = false,
                            Number = nextToolNumber,
                            Name = toolString,
                            Description = toolString,
                            ToolCategoryId = 2
                        }
                    });
                }
                else
                {
                    taskTools.Add(new Task_Tool()
                    {
                        ToolId = targetTool.Id
                    });
                }
            }

            return taskTools;
        }

        private ICollection<Task_Suggestion> getTask_Suggestions(TblTask obj)
        {
            List<Task_Suggestion> task_Suggestions = new List<Task_Suggestion>();

            var introductions = _taskInstructions.Where(r => r.Tid == obj.Tid);

            foreach (var tasksuggestion in introductions)
            {
                var sourceSuggestionType = _taskInstructionType.Where(r => r.IntroTypeId == tasksuggestion.TypeId).FirstOrDefault();
                var introTypeText = sourceSuggestionType == null ? "Surely this won't match anything" : sourceSuggestionType.IntroTypeText;
                var suggestionType = _suggestionTypes.Where(r => r.Name == introTypeText).FirstOrDefault();
                var suggestionTypeId = suggestionType == null ? (int?)null : suggestionType.Id;

                task_Suggestions.Add(new Task_Suggestion()
                {
                    Description = tasksuggestion.Description,
                    TaskSuggestionTypeId = suggestionTypeId,
                    //Number,
                    Deleted = false,
                    Active = true
                });
            }

            return task_Suggestions;
        }

        private ICollection<Task_Step> getTask_Steps(List<TblTask> tasks)
        {
            List<Task_Step> task_Steps = new List<Task_Step>();

            foreach (var task in tasks)
            {
                var substeps = (_source as EMP_DemoContext).TblTaskSubSteps.Where(r => r.Tid == task.Tid).ToList();

                string description = substeps.Count() == 0 ? task.Tdesc : task.Tdesc + " | " + String.Join(" | ", substeps.Select(r => r.Tssdesc));

                task_Steps.Add(new Task_Step()
                {
                    Description = description,
                    Number = task.TsubNum,
                    Deleted = false,
                    Active = true
                });
            }
            return task_Steps;
        }

        private ICollection<Task_Question> getTask_Questions(TblTask obj)
        {
            var taskQuestions = _tblTaskQuestion.Where(t => t.Tid == obj.Tid).ToList();
            List<Task_Question> task_Questions = new List<Task_Question>();

            foreach (var question in taskQuestions)
            {
                task_Questions.Add(new Task_Question()
                {
                    QuestionNumber = question.Tqnumber ?? -1,
                    Question = question.Tqquestion,
                    Answer = question.Tqanswer,
                    Deleted = false,
                    Active = true
                });
            }


            return task_Questions;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _dutyAreas.Count();
        }

        protected override void updateTarget(DutyArea record)
        {
            var existing = (_target as QTD2.Data.QTDContext).DutyAreas.Where(r => r.Letter == record.Letter).Where(r => r.Number == record.Number).FirstOrDefault();

            //don't add DA if exists
            if (existing == null)
                (_target as QTD2.Data.QTDContext).DutyAreas.Add(record);
        }
    }
}
