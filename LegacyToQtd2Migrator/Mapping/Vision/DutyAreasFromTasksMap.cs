using LegacyToQtd2Migrator.Helpers;
using LegacyToQtd2Migrator.Legacy.Data;
using LegacyToQtd2Migrator.Vision.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace LegacyToQtd2Migrator.Mapping.Vision
{
    public class DutyAreasFromTasksMap : Common.MigrationMap<DutyAreaParsingDTO, DutyArea>
    {
        List<AnalysisImpl> _sourceTasks;
        List<AnalysisImpl> _sourceTaskSteps;
        List<AnalysisHierarchy> _sourceAnalysisHierarchy;
        List<AnalysisCondStand> _sourceAnalysisConditions;
        List<AnalysisOjtNote> _sourceOjtNotes;

        List<XrefLibLink> _refLinks;

        List<DutyAreaParsingDTO> _dutyAreaParsings;
        List<TaskParsingDTO> _taskParsings;

        List<Tool> _targetTools;
        List<Task_SuggestionTypes> _suggestionTypes;

        int _projectId;

        public DutyAreasFromTasksMap(DbContext source, DbContext target, int projectId) : base(source, target)
        {
            _projectId = projectId;
        }

        protected override List<DutyAreaParsingDTO> getSourceRecords()
        {
            _sourceTasks = (_source as VisionContext).GetVisionAnalysisImps(_projectId, "Task", new List<Func<AnalysisImpl, bool>>());
            _sourceAnalysisHierarchy = (_source as VisionContext).AnalysisHierarchies.ToList();
            _sourceAnalysisConditions = (_source as VisionContext).AnalysisCondStands.ToList();
            _sourceTaskSteps = (_source as VisionContext).GetVisionAnalysisImps(_projectId, "Element", new List<Func<AnalysisImpl, bool>>());
            _refLinks = (_source as VisionContext).XrefLibLinks.Include("FkItemNavigation.XrefLibImplFkXrefLibNavigations").ToList();
            _sourceOjtNotes = (_source as VisionContext).AnalysisOjtNotes.ToList();

            _dutyAreaParsings = _sourceTasks.Select(r => r.Text.RtfToPlainText().ParseVisionDutyAreaText(r))
                    .GroupBy(r => r.DutyAreaNumber)
                    .Select(r => r.First())
                    .Distinct().ToList();
            _taskParsings = _sourceTasks.Select(r => r.Text.RtfToPlainText().ParseVisionTaskText(r)).ToList();

            _suggestionTypes = (_target as QTD2.Data.QTDContext).Task_SuggestionTypes.ToList();
            _targetTools = (_target as QTD2.Data.QTDContext).Tools.ToList();

            return _dutyAreaParsings;
        }

        protected override DutyArea mapRecord(DutyAreaParsingDTO obj)
        {
            if (!obj.TaskNumberFound) return null;

            return new DutyArea()
            {
                Active = true,
                Title = obj.DutyAreaNumber,
                Description = obj.DutyAreaNumber,
                Letter = "§",
                Number = Convert.ToInt32(obj.DutyAreaNumber),
                SubdutyAreas = getSubdutyAreas(obj)
            };
        }
        private ICollection<SubdutyArea> getSubdutyAreas(DutyAreaParsingDTO obj)
        {
            var subDutyParsings = _taskParsings
                    .Where(r => r.DutyAreaNumber == obj.DutyAreaNumber)
                    .Select(r => new { r.DutyAreaNumber, r.SubDutyAreaNumber })
                    .Distinct();

            List<SubdutyArea> subdutyAreas = new List<SubdutyArea>();

            foreach (var subDutyParsing in subDutyParsings)
            {
                var taskParsings = _taskParsings
                        .Where(r => r.SubDutyAreaNumber == subDutyParsing.SubDutyAreaNumber)
                        .Where(r => r.DutyAreaNumber == subDutyParsing.DutyAreaNumber)
                        .ToList();

                subdutyAreas.Add(new SubdutyArea()
                {
                    Active = true,
                    Description = subDutyParsing.SubDutyAreaNumber,
                    SubNumber = Convert.ToInt32(subDutyParsing.SubDutyAreaNumber),
                    Title = subDutyParsing.SubDutyAreaNumber,
                    Tasks = getTasks(taskParsings)
                });
            }

            return subdutyAreas;
        }

        private ICollection<QTD2.Domain.Entities.Core.Task> getTasks(List<TaskParsingDTO> taskParsings)
        {
            List<QTD2.Domain.Entities.Core.Task> tasks = new List<QTD2.Domain.Entities.Core.Task>();

            foreach (var taskParsing in taskParsings)
            {
                var sourceTask = _sourceTasks.Where(r => r.Id == taskParsing.VisionId).First();

                var sourceConditions = _sourceAnalysisConditions.Where(r => r.Type == 0 && r.FkAnalysis == sourceTask.FkAnalysis).ToList();
                var sourceStandards = _sourceAnalysisConditions.Where(r => r.Type == 1 && r.FkAnalysis == sourceTask.FkAnalysis).ToList();

                var conditions = sourceConditions.Select(r => r.Text).ToList().CombineRtfStrings();
                var criteria = sourceStandards.Select(r => r.Text).ToList().CombineRtfStrings();

                int taskNumber;

                if (taskParsing.TaskNumberFound)
                {
                    taskNumber = Convert.ToInt32(taskParsing.TaskNumber);
                }
                else
                {
                    taskNumber = tasks.Count() + 1;
                }

                tasks.Add(new QTD2.Domain.Entities.Core.Task()
                {
                    Active = true,
                    Description = taskParsing.TaskText,
                    Number = taskNumber,
                    Conditions = conditions,
                    Criteria = criteria,
                    Critical = false,
                    IsMeta = false,
                    IsReliability = true,
                    References = getReferences(sourceTask),
                    Task_Steps = getTask_Steps(sourceTask),
                    Task_Tools = getTaskTools(sourceTask),
                    Task_Suggestions = getTaskSuggestions(sourceTask)
                });
            }

            return tasks;
        }

        private ICollection<Task_Suggestion> getTaskSuggestions(AnalysisImpl sourceTask)
        {
            List<Task_Suggestion> taskSuggestions = new List<Task_Suggestion>();

            var sourceOjtNotes = _sourceOjtNotes.Where(r => r.FkExpiredBy == null).Where(r => r.FkAnalysis == sourceTask.FkAnalysis);

            foreach (var sourceOjtNote in sourceOjtNotes)
            {
                taskSuggestions.Add(new Task_Suggestion()
                {
                    TaskSuggestionTypeId = 1,
                    Description = sourceOjtNote.Notes.RtfToPlainText().Trim(),
                    Active = true
                });
            }

            return taskSuggestions;
        }

        private string getReferences(AnalysisImpl sourceTask)
        {
            var refLinks = _refLinks
                     .Where(r => r.LinkToType == 1)
                     .Where(r => r.FkLinkTo == sourceTask.FkAnalysis)
                     //select * From XREF_LIB_IMPL where text_sort like '%ATC REFERENCES%'  and FK_EXPIRED_BY is null
                     .Where(r => r.FkParent == 312).ToList();

            return String.Join(Environment.NewLine, refLinks.SelectMany(r => r.FkItemNavigation.XrefLibImplFkXrefLibNavigations.Where(r => r.FkExpiredBy == null)).Select(r => r.Text.RtfToPlainText()));

        }

        private ICollection<Task_Step> getTask_Steps(AnalysisImpl sourceTask)
        {
            var sourceHeirarchies = _sourceAnalysisHierarchy.Where(r => r.FkExpiredBy == null).Where(r => r.FkParent == sourceTask.FkAnalysis).OrderBy(r => r.Sequence);

            List<Task_Step> task_Steps = new List<Task_Step>();

            foreach (var sourceHeirarchy in sourceHeirarchies)
            {
                var sourceTaskStep = _sourceTaskSteps
                                .Where(r => r.FkAnalysis == sourceHeirarchy.FkChild)
                                .FirstOrDefault();

                if (sourceTaskStep == null) continue;

                task_Steps.Add(new Task_Step()
                {
                    Active = true,
                    Description = sourceTaskStep.Text.RtfToHtml(),
                    Number = (int)sourceHeirarchy.Sequence
                });
            }

            return task_Steps;
        }

        private ICollection<Version_Task> getTaskVersions(TblTask sourceTask)
        {
            List<Version_Task> taskVersions = new List<Version_Task>();

            return taskVersions;
        }

        private ICollection<Task_History> getTaskHistories(TblTasksHistory sourceTaskHistory)
        {
            List<Task_History> taskHistories = new List<Task_History>();

            return taskHistories;
        }

        private ICollection<Task_TrainingGroup> getTask_TrainingGroups(TblTask sourceTask)
        {
            List<Task_TrainingGroup> trainingGroups = new List<Task_TrainingGroup>();

            return trainingGroups;
        }

        private ICollection<Task_Tool> getTaskTools(AnalysisImpl sourceTask)
        {
            List<Task_Tool> taskTools = new List<Task_Tool>();

            var sourceLinks = _refLinks
                    .Where(r => r.FkLinkTo == sourceTask.FkAnalysis)
                    .Where(r => r.FkExpiredBy == null)
                    .Where(r => r.LinkToType == 1);

            foreach (var sourceLink in sourceLinks)
            {
                string sourceText = sourceLink.FkItemNavigation.XrefLibImplFkXrefLibNavigations.Where(r => r.FkExpiredBy == null).FirstOrDefault().Text.RtfToPlainText();

                if (string.IsNullOrEmpty(sourceText)) continue;

                var tools = _targetTools.Where(r => r.Name == sourceText);

                if (tools.Count() > 1)
                {
                    string s = "break";
                }

                if (tools.Count() == 0) continue;

                taskTools.Add(new Task_Tool()
                {
                    Tool = tools.First()
                });
            }

            return taskTools;
        }

        private ICollection<Task_Suggestion> getTask_Suggestions(TblTask obj)
        {
            List<Task_Suggestion> task_Suggestions = new List<Task_Suggestion>();

            return task_Suggestions;
        }

        private ICollection<Task_Question> getTask_Questions(TblTask obj)
        {
            List<Task_Question> task_Questions = new List<Task_Question>();

            return task_Questions;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _dutyAreaParsings.Count();
        }

        protected override void updateTarget(DutyArea record)
        {
            if (record == null) return;

            (_target as QTD2.Data.QTDContext).DutyAreas.Add(record);
        }
    }
}
