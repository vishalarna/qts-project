using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace QTD2.Domain.Services.Core
{
    public class TaskService : Common.Service<Entities.Core.Task>, ITaskService
    {
        public TaskService(ITaskRepository taskRepository, ITaskValidation taskValidation)
            : base(taskRepository, taskValidation)
        {
        }

        public async System.Threading.Tasks.Task<List<Task>> GetByListOfIds(List<int> list)
        {
            return (await FindWithIncludeAsync(r => list.Contains(r.Id), new[] { "SubdutyArea.DutyArea", "ILA_TaskObjective_Links", "Version_Tasks" })).ToList();
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetByPositionListAsync(IEnumerable<int> positionIds)
        {
            return await FindWithIncludeAsync(r => r.Task_Positions.Where(s => positionIds.Contains(s.PositionId)).Count() > 0, new[] { "Task_Positions", "SubdutyArea", "SubdutyArea.DutyArea", "Task_Tools", "Task_Tools.Tool" });
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetByTaskGroupAsync(int taskGroupId, bool includeGroups)
        {
            List<Expression<Func<Task, bool>>> predicates = new List<Expression<Func<Task, bool>>>();
            if (includeGroups)
            {
                return await AllWithIncludeAsync(new[] { "Task_Positions", "Task_Positions.Position", "SubdutyArea", "SubdutyArea.DutyArea", "Task_TrainingGroups", "Task_TrainingGroups.TrainingGroup" });
            }
            else if (taskGroupId > 0)
            {
                predicates.Add(r => r.Task_TrainingGroups.Where(r => r.Id == taskGroupId).Count() > 0);
            }
            else
            {
                predicates.Add(r => r.Task_TrainingGroups.Count() > 0);
            }
            return await FindWithIncludeAsync(predicates, new[] { "Task_Positions", "Task_Positions.Position", "SubdutyArea", "SubdutyArea.DutyArea", "Task_TrainingGroups", "Task_TrainingGroups.TrainingGroup" });
        }

        public async System.Threading.Tasks.Task<List<Task>> GetLinksForMyDataPositionLinkageAsync(string activePositions, string taskStatus, string employeeStatus, DateTime employeeStartDate, DateTime employeeEndDate)
        {
            List<Expression<Func<Task, bool>>> predicates = new List<Expression<Func<Task, bool>>>();
            if (!string.IsNullOrEmpty(taskStatus) && taskStatus == "Active Only")
                predicates.Add(r => r.Active);
            else if (!string.IsNullOrEmpty(taskStatus) && taskStatus == "Inactive Only")
                predicates.Add(r => !r.Active);
            if (!string.IsNullOrEmpty(employeeStatus) && employeeStatus == "Active Only")
                predicates.Add(r => r.Employee_Tasks.Select(e => e.Employee.Active == true).Count() > 0);
            else if (!string.IsNullOrEmpty(employeeStatus) && employeeStatus == "Inactive Only")
                predicates.Add(r => r.Employee_Tasks.Select(e => e.Employee.Active == false).Count() > 0);
            if (!string.IsNullOrEmpty(activePositions) && activePositions == "Active Only")
                predicates.Add(r => r.Task_Positions.Select(e => e.Position.Active == true).Count() > 0);
            else if (!string.IsNullOrEmpty(activePositions) && activePositions == "Inactive Only")
                predicates.Add(r => r.Task_Positions.Select(e => e.Position.Active == false).Count() > 0);
            var tasks = (await FindWithIncludeAsync(predicates, new[] { "Task_Positions.Position.EmployeePositions", "Employee_Tasks.Employee.Person" })).ToList();
            return tasks;
        }

        public async System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.Task>> GetTaskQualificationEvaluatorAsync(string activeStatus)
        {
            var taskQualifications = await AllWithIncludeAsync(new[] { "TaskQualifications", "TaskQualifications.Employee.Person", "TaskQualifications.Employee.EmployeeOrganizations.Organization" });
            taskQualifications = activeStatus.ToLower() == "active only" ? taskQualifications.Where(r => r.TaskQualifications.Any(s => s.Employee.Active))
                     : activeStatus.ToLower() == "inactive only" ? taskQualifications.Where(r => r.TaskQualifications.Any(s => !s.Employee.Active))
                      : taskQualifications;
            return taskQualifications.ToList();
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetTasksByTaskIdsAsync(List<int> taskIds)
        {
            List<Expression<Func<Domain.Entities.Core.Task, bool>>> predicates = new List<Expression<Func<Domain.Entities.Core.Task, bool>>>();
            predicates.Add(task => taskIds.Contains(task.Id));

            var tasks = (await FindWithIncludeAsync(predicates, new[] { "Task_Steps", "Task_Questions", "Task_EnablingObjective_Links" })).ToList();
            var tasksWithDutyAreas = (await FindWithIncludeAsync(predicates, new[] { "SubdutyArea.DutyArea" })).ToList();
            var tasksWithTools = (await FindWithIncludeAsync(predicates, new[] { "Task_Tools.Tool" }, true)).ToList();
            var taskWithProcedures = (await FindWithIncludeAsync(predicates, new[] { "Procedure_Task_Links.Procedure" })).ToList();
            var tasksWithSaftyHazards = (await FindWithIncludeAsync(predicates, new[] { "SafetyHazard_Task_Links.SaftyHazard" })).ToList();
            var positionTasks = (await FindWithIncludeAsync(predicates, new[] { "Position_Tasks.Position" })).ToList();
            var taskWithTrainingGroups = (await FindWithIncludeAsync(predicates, new[] { "Task_TrainingGroups.TrainingGroup" })).ToList();
            var taskWithTaskSuggestions = (await FindWithIncludeAsync(predicates, new[] { "Task_Suggestions" })).ToList();
            var taskWithEOs = (await FindWithIncludeAsync(predicates, new[] { "Task_EnablingObjective_Links.EnablingObjective" })).ToList();

            foreach (var task in tasks)
            {
                task.SubdutyArea = tasksWithDutyAreas.Where(r => r.Id == task.Id).First().SubdutyArea;
                task.Task_Tools = tasksWithTools.Where(r => r.Id == task.Id).First().Task_Tools;
                task.Procedure_Task_Links = taskWithProcedures.Where(r => r.Id == task.Id).First().Procedure_Task_Links;
                task.SafetyHazard_Task_Links = tasksWithSaftyHazards.Where(r => r.Id == task.Id).First().SafetyHazard_Task_Links;
                task.Position_Tasks = positionTasks.Where(r => r.Id == task.Id).First().Position_Tasks;
                task.Task_TrainingGroups = taskWithTrainingGroups.Where(r => r.Id == task.Id).First().Task_TrainingGroups;
                task.Task_Suggestions = taskWithTaskSuggestions.Where(r => r.Id == task.Id).First().Task_Suggestions;
                task.Task_EnablingObjective_Links = taskWithEOs.Where(r => r.Id == task.Id).First().Task_EnablingObjective_Links;
            }

            return tasks;
        }

        public async System.Threading.Tasks.Task<List<Task>> GetAllTasksOrderByNumber()
        {
            var tasks = await AllQuery().Select(s => new Task
            {
                Id = s.Id,
                IsMeta = s.IsMeta,
                IsReliability = s.IsReliability,
                Abreviation = s.Abreviation,
                Active = s.Active,
                Conditions = s.Conditions,
                CreatedBy = s.CreatedBy,
                CreatedDate = s.CreatedDate,
                Criteria = s.Criteria,
                EffectiveDate = s.EffectiveDate,
                Critical = s.Critical,
                Deleted = s.Deleted,
                Description = s.Description,
                ModifiedBy = s.ModifiedBy,
                ModifiedDate = s.ModifiedDate,
                Number = s.Number,
                References = s.References,
                RequalificationDueDate = s.RequalificationDueDate,
                RequiredTime = s.RequiredTime,
                RequalificationNotes = s.RequalificationNotes,
                RequalificationRequired = s.RequalificationRequired,
                SubdutyAreaId = s.SubdutyAreaId,
                TaskCriteriaUpload = s.TaskCriteriaUpload,
                Position_Tasks = s.Position_Tasks
            }).OrderBy(o => o.Number).ToListAsync();
            return tasks;
        }

        public async System.Threading.Tasks.Task<List<Task>> GetTasksAsync()
        {
            return (await AllWithIncludeAsync(new string[] { "SubdutyArea", "SubdutyArea.DutyArea" })).ToList();
        }

        public async System.Threading.Tasks.Task<List<Task>> GetMinimizedTaskForTree()
        {
            var tasks = await AllQuery().Select(s => new Task
            {
                Id = s.Id,
                IsMeta = s.IsMeta,
                IsReliability = s.IsReliability,
                Active = s.Active,
                Number = s.Number,
                Description = s.Description,
                SubdutyAreaId = s.SubdutyAreaId,
                Position_Tasks = s.Position_Tasks
            }).ToListAsync();

            return tasks;
        }

        public async System.Threading.Tasks.Task<Task> GetTaskByIdAsync(int taskId)
        {
            return (await FindWithIncludeAsync(x => x.Id == taskId, new[] { "Position_Tasks.Position" })).FirstOrDefault();
        }

        public async System.Threading.Tasks.Task<List<Task>> GetEnablingObjectivesByTaskAsync(List<int> taskIds, bool isRR = false)
        {
            List<Expression<Func<Task, bool>>> predicates = new List<Expression<Func<Task, bool>>>();
            if (taskIds != null)
            {
                predicates.Add(x => taskIds.Contains(x.Id));
                predicates.Add(x => x.Task_EnablingObjective_Links.Count() > 0);
            }
            if (isRR)
            {
                predicates.Add(x => x.IsReliability == true);
            }
            return (await FindWithIncludeAsync(predicates, new[] { "SubdutyArea.DutyArea", "Task_EnablingObjective_Links.EnablingObjective.EnablingObjective_Topic", "Task_EnablingObjective_Links.EnablingObjective.EnablingObjective_Category", "Task_EnablingObjective_Links.EnablingObjective.EnablingObjective_SubCategory" })).ToList();
        }

        public async System.Threading.Tasks.Task<List<Task>> GetILAsLinkedToTaskAndEoAsync(List<int> taskIds, bool includeILAsOfEOTask, bool showTrainingResources)
        {
            List<Expression<Func<Task, bool>>> predicates = new List<Expression<Func<Task, bool>>>();
            string[] includes = new[] { "SubdutyArea.DutyArea" };
            predicates.Add(x => taskIds.Contains(x.Id));

            if (showTrainingResources)
                includes = includes.Append("ILA_TaskObjective_Links.ILA.ILA_Resources").ToArray();
            else
                includes = includes.Append("ILA_TaskObjective_Links.ILA").ToArray();

            if (includeILAsOfEOTask)
                includes = includes.Append("Task_EnablingObjective_Links").ToArray();

            return (await FindWithIncludeAsync(predicates, includes)).ToList();
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetTaskDetailsByTaskIdsAsync(List<int> positionIds, List<int> taskIds, bool includePseudoTasks, string tasksType, string activeInactive, bool rrTasksOnly, List<int> trIds)
        {
            List<Expression<Func<Domain.Entities.Core.Task, bool>>> predicates = new List<Expression<Func<Domain.Entities.Core.Task, bool>>>();
            List<Expression<Func<Domain.Entities.Core.Task, bool>>> mainPredicates = new List<Expression<Func<Domain.Entities.Core.Task, bool>>>();
            predicates.Add(task => taskIds.Contains(task.Id));
            if (!includePseudoTasks)
            {
                predicates.Add(task => task.SubdutyArea.DutyArea.Letter != "P");
            }

            if (rrTasksOnly)
            {
                predicates.Add(x => x.IsReliability);
            }
            switch (tasksType.ToUpper())
            {
                case "TASKS (REGULAR ONLY)":
                    predicates.Add(x => !x.IsMeta);
                    break;
                case "META TASKS ONLY":
                    predicates.Add(x => x.IsMeta);
                    break;
                case "R6 RELIABILITY IMPACT TASKS ONLY":
                    mainPredicates.Add(x => x.Position_Tasks.Any(s => s.IsR6Impacted));
                    break;
            }

            switch (activeInactive.ToUpper())
            {
                case "ALL":
                    predicates.Add(x => true);
                    break;
                case "ACTIVE ONLY":
                    predicates.Add(x => x.Active);
                    break;
                case "INACTIVE ONLY":
                    predicates.Add(x => !x.Active);
                    break;
            }
            mainPredicates.AddRange(predicates);
            var tasks = (await FindWithIncludeAsync(mainPredicates, new[] { "Task_Steps", "Position_Tasks.R5ImpactedTasks", "TaskReviews.TaskListReview" })).ToList();
            var tasksWithDutyAreas = (await FindWithIncludeAsync(predicates, new[] { "SubdutyArea.DutyArea" })).ToList();
            var tasksEnablingObjectivesLinks = (await FindWithIncludeAsync(predicates, new[] { "Task_EnablingObjective_Links" })).ToList();
            var tasksWithTools = (await FindWithIncludeAsync(predicates, new[] { "Task_Tools.Tool" })).ToList();
            var tasksWithSaftyHazards = (await FindWithIncludeAsync(predicates, new[] { "SafetyHazard_Task_Links.SaftyHazard" })).ToList();
            var taskWithTrainingGroups = trIds.Count() > 0 ? (await FindWithIncludeAsync(predicates, new[] { "Task_TrainingGroups.TrainingGroup" })).ToList() : new List<Task>();
            foreach (var task in tasks)
            {
                task.SubdutyArea = tasksWithDutyAreas.Where(r => r.Id == task.Id).First().SubdutyArea;
                task.Task_Tools = tasksWithTools.Where(r => r.Id == task.Id).First().Task_Tools;
                task.SafetyHazard_Task_Links = tasksWithSaftyHazards.Where(r => r.Id == task.Id).First().SafetyHazard_Task_Links;
                task.Task_TrainingGroups = trIds.Count() > 0 ? taskWithTrainingGroups.Where(r => r.Id == task.Id).First().Task_TrainingGroups : new List<Task_TrainingGroup>();
                task.Task_EnablingObjective_Links = tasksEnablingObjectivesLinks.Where(r => r.Id == task.Id).First().Task_EnablingObjective_Links;
            }
            if (tasksType.ToUpper() == "R5 IMPACT TASKS ONLY")
            {
                foreach (var task in tasks)
                {
                    task.Position_Tasks = task.Position_Tasks.Where(pt => pt.IsR5Impacted && positionIds.Contains(pt.PositionId)).ToList();
                }
                tasks = tasks.Where(m => m.Position_Tasks.Any()).ToList();
            }
            if (tasksType.ToUpper() == "R6 RELIABILITY IMPACT TASKS ONLY")
            {
                foreach (var task in tasks)
                {
                    task.Position_Tasks = task.Position_Tasks.Where(pt => pt.IsR6Impacted && positionIds.Contains(pt.PositionId)).ToList();
                }
                tasks = tasks.Where(m => m.Position_Tasks.Any()).ToList();
            }
            return tasks;
        }
        public async System.Threading.Tasks.Task<List<Task>> GetTasksWithPositionTasksAsync()
        {
            var tasks = (await FindWithIncludeAsync(x => x.Active, new[] { "Position_Tasks" })).ToList();
            var tasksWithDutyAreas = (await FindWithIncludeAsync(x => x.Active, new[] { "SubdutyArea.DutyArea" })).ToList();
            foreach (var task in tasks)
            {
                task.SubdutyArea = tasksWithDutyAreas.Where(r => r.Id == task.Id).First().SubdutyArea;
            }
            return tasks;
        }

        public async System.Threading.Tasks.Task<List<Task>> GetTasksWithDutySubDutyAreaByTaskIdsAsync(List<int> taskIds)
        {
            List<Expression<Func<Domain.Entities.Core.Task, bool>>> predicates = new List<Expression<Func<Domain.Entities.Core.Task, bool>>>();
            predicates.Add(task => taskIds.Contains(task.Id));
            var tasks = (await FindWithIncludeAsync(predicates, new[] { "SubdutyArea.DutyArea" }, true)).ToList();
            return tasks;
        }

        public async System.Threading.Tasks.Task<List<Task>> GetTasksWithDutySubDutyAreaPositionTaskPositionsByTaskIdsAsync(List<int> taskIds)
        {
            List<Expression<Func<Domain.Entities.Core.Task, bool>>> predicates = new List<Expression<Func<Domain.Entities.Core.Task, bool>>>();
            predicates.Add(task => taskIds.Contains(task.Id));
            var tasks = (await FindWithIncludeAsync(predicates, new[] { "SubdutyArea.DutyArea", "Position_Tasks.R5ImpactedTasks.ImpactedTask.Position_Tasks.Position" }, true)).ToList();
            return tasks;
        }

        public async System.Threading.Tasks.Task<List<Task>> GetTasksHistoryByTaskIdsAsync(List<int> taskIds)
        {
            List<Expression<Func<Domain.Entities.Core.Task, bool>>> predicates = new List<Expression<Func<Domain.Entities.Core.Task, bool>>>();
            predicates.Add(task => taskIds.Contains(task.Id));
            var tasks = (await FindWithIncludeAsync(predicates, new[] { "Version_Tasks.Task_Histories", "SubdutyArea.DutyArea" }, true)).ToList();
            var tasksWithTools = (await FindWithIncludeAsync(predicates, new[] { "Task_Tools.Tool" })).ToList();
            foreach (var task in tasks)
            {
                task.Task_Tools = tasksWithTools.Where(r => r.Id == task.Id).First().Task_Tools;
            }
            return tasks;
        }

        public async System.Threading.Tasks.Task<List<Task>> GetTasksForEmployeeTaskQualificationDatesByTaskGenerator(List<int> taskIds, bool rrTasksOnly)
        {
            List<Expression<Func<Domain.Entities.Core.Task, bool>>> predicates = new List<Expression<Func<Domain.Entities.Core.Task, bool>>>();
            predicates.Add(task => taskIds.Contains(task.Id));
            if (rrTasksOnly)
            {
                predicates.Add(task => task.IsReliability);
            }
            var tasks = (await FindWithIncludeAsync(predicates, new[] { "SubdutyArea.DutyArea" }, true)).ToList();
            return tasks;
        }

        public async System.Threading.Tasks.Task<List<Task>> GetTasksForEmployeeTaskQualificationRecordsForGivenPositionGenerator(List<int> taskIds, bool reliabilityRelatedTasksOnly, bool includeInactiveTasks, bool includePseudoTasks)
        {
            List<Expression<Func<Domain.Entities.Core.Task, bool>>> predicates = new List<Expression<Func<Domain.Entities.Core.Task, bool>>>();
            predicates.Add(task => taskIds.Contains(task.Id));
            if (reliabilityRelatedTasksOnly)
            {
                predicates.Add(task => task.IsReliability);
            }
            if (!includeInactiveTasks)
            {
                predicates.Add(task => task.Active);
            }
            var tasks = (await FindWithIncludeAsync(predicates, new[] { "SubdutyArea.DutyArea" }, true)).ToList();
            if (!includePseudoTasks)
            {
                tasks = tasks.Where(task => task.SubdutyArea.DutyArea.Letter != "P").ToList();
            }
            return tasks;
        }

        public async System.Threading.Tasks.Task<List<Task>> GetProceduresByTaskAsync(List<int> taskIds, bool includeInactive)
        {
            List<Expression<Func<Domain.Entities.Core.Task, bool>>> predicates = new List<Expression<Func<Domain.Entities.Core.Task, bool>>>();
            predicates.Add(task => taskIds.Contains(task.Id));

            if (!includeInactive)
                predicates.Add(task => task.Active);

            var tasks = (await FindWithIncludeAsync(predicates, new[] { "Task_Steps", "Task_Questions", "Task_EnablingObjective_Links" })).ToList();
            var tasksWithDutyAreas = (await FindWithIncludeAsync(predicates, new[] { "SubdutyArea.DutyArea" })).ToList();
            var taskWithProcedures = (await FindWithIncludeAsync(predicates, new[] { "Procedure_Task_Links.Procedure" })).ToList();

            foreach (var task in tasks)
            {
                task.SubdutyArea = tasksWithDutyAreas.Where(r => r.Id == task.Id).First().SubdutyArea;
                task.Procedure_Task_Links = taskWithProcedures.Where(r => r.Id == task.Id).First().Procedure_Task_Links;
            }

            return tasks;
        }

        public async System.Threading.Tasks.Task<List<Task>> GetILAsByTaskAsync(List<int> taskIds, bool includeUnlinkedTasks)
        {
            List<Expression<Func<Task, bool>>> predicates = new List<Expression<Func<Task, bool>>>();
            predicates.Add(task => taskIds.Contains(task.Id));

            if (!includeUnlinkedTasks)
                predicates.Add(task => task.ILA_TaskObjective_Links.Count() > 0);

            var tasks = (await FindWithIncludeAsync(predicates, new[] { "SubdutyArea.DutyArea", "ILA_TaskObjective_Links.ILA.Provider" })).ToList();

            return tasks;
        }

        public async System.Threading.Tasks.Task<List<Task>> GetTasksListBySubDutyAreaIdAsync(int subDutyAreaId)
        {
            var tasks = (await FindAsync(x => x.SubdutyAreaId == subDutyAreaId)).ToList();
            return tasks;
        }
        public async System.Threading.Tasks.Task<List<Task>> GetTasksNotLinkedToILAAsync(string activeStatus, bool reliabilityRelatedTasksOnly, bool includeMetaTasks, bool includePseudoTasks)
        {
            List<Expression<Func<Task, bool>>> predicates = new List<Expression<Func<Task, bool>>>();
            predicates.Add(x => x.ILA_TaskObjective_Links.Count() == 0);

            if (activeStatus == "Active Only")
            {
                predicates.Add(r => r.Active);
            }
            else if (activeStatus == "Inactive Only")
            {
                predicates.Add(r => !r.Active);
            }
            if (reliabilityRelatedTasksOnly)
            {
                predicates.Add(task => task.IsReliability);
            }
            if (!includeMetaTasks)
            {
                predicates.Add(task => !task.IsMeta);
            }
            if (!includePseudoTasks)
            {
                predicates.Add(task => task.SubdutyArea.DutyArea.Letter != "P");
            }
            var tasks = (await FindWithIncludeAsync(predicates, new[] { "SubdutyArea.DutyArea" }, true)).ToList();
            return tasks;
        }

        public async System.Threading.Tasks.Task<List<Task>> GetTasksByProcedureAsync(List<int> taskIds, bool reliabilityRelatedTasksOnly, bool includeInactiveTasks)
        {
            List<Expression<Func<Domain.Entities.Core.Task, bool>>> predicates = new List<Expression<Func<Domain.Entities.Core.Task, bool>>>();
            predicates.Add(task => taskIds.Contains(task.Id));
            if (reliabilityRelatedTasksOnly)
            {
                predicates.Add(task => task.IsReliability);
            }
            if (!includeInactiveTasks)
            {
                predicates.Add(task => task.Active);
            }
            var tasks = (await FindWithIncludeAsync(predicates, new[] { "SubdutyArea.DutyArea" }, true)).ToList();

            return tasks;
        }
        public async System.Threading.Tasks.Task<List<Task>> GetTasksByIdsAsync(List<int> taskIds, bool reliabilityRelatedTasksOnly, bool includeMetaTasks, bool includeInactiveTasks, bool includePseudoTasks)
        {
            List<Expression<Func<Task, bool>>> predicates = new List<Expression<Func<Task, bool>>>();

            predicates.Add(task => taskIds.Contains(task.Id));
            if (reliabilityRelatedTasksOnly)
            {
                predicates.Add(task => task.IsReliability);
            }
            if (!includeMetaTasks)
            {
                predicates.Add(task => !task.IsMeta);
            }
            if (!includePseudoTasks)
            {
                predicates.Add(task => task.SubdutyArea.DutyArea.Letter != "P");
            }
            if (!includeInactiveTasks)
            {
                predicates.Add(task => task.Active);
            }
            var tasks = (await FindWithIncludeAsync(predicates, new[] { "SubdutyArea.DutyArea" }, true)).ToList();
            return tasks;
        }

        public async System.Threading.Tasks.Task<List<Task>> GetTasksNotLinkedToPositionAsync(string activeStatus, bool reliabilityRelatedTasksOnly, bool includeMetaTasks, bool includePseudoTasks)
        {
            List<Expression<Func<Task, bool>>> predicates = new List<Expression<Func<Task, bool>>>();
            predicates.Add(x => x.Position_Tasks.Count() == 0);

            if (activeStatus == "Active Only")
            {
                predicates.Add(r => r.Active);
            }
            else if (activeStatus == "Inactive Only")
            {
                predicates.Add(r => !r.Active);
            }
            if (reliabilityRelatedTasksOnly)
            {
                predicates.Add(task => task.IsReliability);
            }
            if (!includeMetaTasks)
            {
                predicates.Add(task => !task.IsMeta);
            }
            if (!includePseudoTasks)
            {
                predicates.Add(task => task.SubdutyArea.DutyArea.Letter != "P");
            }
            var tasks = (await FindWithIncludeAsync(predicates, new[] { "SubdutyArea.DutyArea" }, true)).ToList();
            return tasks;
        }
        public async System.Threading.Tasks.Task<List<Task>> GetSafetyHazardsByTaskIdsAsync(List<int> taskIds)
        {
            List<Expression<Func<Domain.Entities.Core.Task, bool>>> predicates = new List<Expression<Func<Domain.Entities.Core.Task, bool>>>();
            predicates.Add(task => taskIds.Contains(task.Id));
            var tasks = (await FindWithIncludeAsync(predicates, new[] { "SubdutyArea.DutyArea", "SafetyHazard_Task_Links", "Task_MetaTask_Links.Task.SafetyHazard_Task_Links", "Task_MetaTask_Links.Task.SubdutyArea.DutyArea" }, true)).ToList();
            return tasks;
        }

        public async System.Threading.Tasks.Task<List<Task>> GetEnablingObjectivesMetaEosSQsByTaskAsync(List<int> taskIds, List<int> includeObjectivesLinkIds)
        {
            List<Expression<Func<Task, bool>>> predicates = new List<Expression<Func<Task, bool>>>();
            List<string> includes = new List<string>();

            if (taskIds != null && taskIds.Count > 0)
            {
                predicates.Add(x => taskIds.Contains(x.Id));
                predicates.Add(x => x.Task_EnablingObjective_Links.Any());
            }
            if (includeObjectivesLinkIds.Contains(2))
            {
                includes.Add("Task_EnablingObjective_Links.EnablingObjective.EnablingObjective_MetaEO_Links.EnablingObjective.EnablingObjective_Topic.EnablingObjectives_SubCategory.EnablingObjectives_Category");
            }
            if (includeObjectivesLinkIds.Intersect(new[] { 1, 2, 3 }).Any())
            {
                includes.Add("Task_EnablingObjective_Links.EnablingObjective.EnablingObjective_Topic");
                includes.Add("Task_EnablingObjective_Links.EnablingObjective.EnablingObjective_Category");
                includes.Add("Task_EnablingObjective_Links.EnablingObjective.EnablingObjective_SubCategory");
            }
            if (includes.Any())
            {
                return (await FindWithIncludeAsync(predicates, includes.ToArray())).ToList();
            }
            else
            {
                return (await FindAsync(predicates)).ToList();
            }
        }

        public async System.Threading.Tasks.Task<List<Task>> GetEnablingObjectivesMetaEosLinksByTaskIdsAsync(List<int> taskIds)
        {
            List<Expression<Func<Task, bool>>> predicates = new List<Expression<Func<Task, bool>>>();
            if (taskIds != null && taskIds.Count > 0)
            {
                predicates.Add(x => taskIds.Contains(x.Id));
                predicates.Add(x => x.Task_EnablingObjective_Links.Any());
            }
            return (await FindWithIncludeAsync(predicates, new[] { "Task_EnablingObjective_Links.EnablingObjective.EnablingObjective_MetaEO_Links" })).ToList();
        }

        public async System.Threading.Tasks.Task<List<Task>> GetTasksWithoutTaskTrainingGroupsAsync(bool rrTasks, bool includeInactiveTasks, bool includePseudoTasks)
        {
            List<Expression<Func<Task, bool>>> predicates = new List<Expression<Func<Task, bool>>>();
            predicates.Add(m => !m.Task_TrainingGroups.Any());
            if (!includePseudoTasks)
            {
                predicates.Add(t => t.SubdutyArea.DutyArea.Letter != "P");
            }
            if (!includeInactiveTasks)
            {
                predicates.Add(t => t.Active);
            }
            if (rrTasks)
            {
                predicates.Add(t => t.IsReliability);
            }
            return (await FindWithIncludeAsync(predicates, new string[] { "SubdutyArea.DutyArea", "Position_Tasks.Position" }, true)).ToList();
        }

        public async System.Threading.Tasks.Task<List<Entities.Core.Task>> GetTasksByTrainingTaskGroupIdsAsync(List<int> trIds, bool rrTasks, bool includeInactiveTasks, bool includePseudoTasks)
        {
            List<Expression<Func<Task, bool>>> predicates = new List<Expression<Func<Task, bool>>>();
            predicates.Add(m => m.Task_TrainingGroups.Any(r => trIds.Contains(r.TrainingGroupId)));
            if (!includePseudoTasks)
            {
                predicates.Add(t => t.SubdutyArea.DutyArea.Letter != "P");
            }
            if (!includeInactiveTasks)
            {
                predicates.Add(t => t.Active);
            }
            if (rrTasks)
            {
                predicates.Add(t => t.IsReliability);
            }
            return (await FindWithIncludeAsync(predicates, new string[] { "SubdutyArea.DutyArea", "Task_TrainingGroups.TrainingGroup", "Position_Tasks.Position" }, true)).ToList();
        }
        public async System.Threading.Tasks.Task<List<Entities.Core.Task>> GetTasksByIdAsync(List<int> taskIds, bool includeMetaTasks, bool includePseudoTasks)
        {
            List<Expression<Func<Task, bool>>> predicates = new List<Expression<Func<Task, bool>>>();

            predicates.Add(task => taskIds.Contains(task.Id));

            if (!includeMetaTasks)
            {
                predicates.Add(task => !task.IsMeta);
            }
            if (!includePseudoTasks)
            {
                predicates.Add(task => task.SubdutyArea.DutyArea.Letter != "P");
            }
            var tasks = await FindWithIncludeAsync(predicates, new[] { "SubdutyArea.DutyArea", "TaskQualifications", "Task_MetaTask_Links.Task.SubdutyArea.DutyArea" }, true);
            return tasks.ToList();
        }

        public async System.Threading.Tasks.Task<List<Entities.Core.Task>> GetTasksByIdsAndDatesAsync(List<int> taskIds, DateTime startDate, DateTime endDate, bool includeRRTasks)
        {
            List<Expression<Func<Task, bool>>> predicates = new List<Expression<Func<Task, bool>>>();

            predicates.Add(task => taskIds.Contains(task.Id));

            predicates.Add(r => r.EffectiveDate >= DateOnly.FromDateTime(startDate) && r.EffectiveDate <= DateOnly.FromDateTime(endDate));

            if (includeRRTasks)
            {
                predicates.Add(t => t.IsReliability);
            }
            var tasks = await FindWithIncludeAsync(predicates, new[] { "SubdutyArea.DutyArea", "ILA_TaskObjective_Links", "Version_Tasks" }, true);
            return tasks.ToList();
        }

        public async System.Threading.Tasks.Task<Task> GetForCopyAsync(int taskId)
        {

            var task = (await FindWithIncludeAsync(r => r.Id == taskId,
                            new[] { "ILA_TaskObjective_Links",
                                        "Task_Steps",
                                        "Task_Tools",
                                        "Task_Questions"
                            })).First();

            var task2 = (await FindWithIncludeAsync(r => r.Id == taskId,
                              new[] { "Task_TrainingGroups",
                                            "Task_Suggestions",
                                            "Procedure_Task_Links",                                           
                              })).First();

            var task3 = (await FindWithIncludeAsync(r => r.Id == taskId,
                              new[] { "ILA_TaskObjective_Links",
                                            "RR_Task_Links",
                                            "SafetyHazard_Task_Links"
                                            
                              })).First();

            var task4 = (await FindWithIncludeAsync(r => r.Id == taskId,
                           new[] { 
                                            "Task_EnablingObjective_Links",
                                            "Position_Tasks",
                                            "Task_MetaTask_Links"
                           })).First();

            task.Task_TrainingGroups = task2.Task_TrainingGroups;
            task.Task_Suggestions = task2.Task_Suggestions;
            task.Procedure_Task_Links = task2.Procedure_Task_Links;
            task.ILA_TaskObjective_Links = task3.ILA_TaskObjective_Links;
            task.RR_Task_Links = task3.RR_Task_Links;
            task.SafetyHazard_Task_Links = task3.SafetyHazard_Task_Links;
            task.Task_EnablingObjective_Links = task4.Task_EnablingObjective_Links;
            task.Position_Tasks = task4.Position_Tasks;
            task.Task_MetaTask_Links = task4.Task_MetaTask_Links;

            return task;
        }
    }
}
