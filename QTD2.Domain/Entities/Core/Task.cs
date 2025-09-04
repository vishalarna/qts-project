using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Task : Common.Entity
    {
        public int SubdutyAreaId { get; set; }

        public string Description { get; set; }

        public string Abreviation { get; set; }

        public int Number { get; set; }

        public string Criteria { get; set; }

        public string TaskCriteriaUpload { get; set; }

        public DateOnly? RequalificationDueDate { get; set; }
        public bool? RequalificationRequired { get; set; }
        public string? RequalificationNotes { get; set; }

        public string Image { get; set; }

        public bool Critical { get; set; }

        public string References { get; set; }

        public int RequiredTime { get; set; }

        public bool IsMeta { get; set; }

        public bool IsReliability { get; set; }

        public string Conditions { get; set; }

        public DateOnly EffectiveDate { get; set; }

        public string FullNumber
        {
            get
            {
                //todo ensure fully loading and not  unloaded nav propers
                return getFullNumber();
            }
        }

        // public int MajorVersion { get; set; }
        // public int MinorVersion { get; set; }
        public virtual SubdutyArea SubdutyArea { get; set; }

        public virtual ICollection<Task_Step> Task_Steps { get; set; } = new List<Task_Step>();

        //public virtual ICollection<Task_SaftyHazard_Link> Task_SaftyHazard_Links { get; set; } = new List<Task_SaftyHazard_Link>();

        public virtual ICollection<Task_EnablingObjective_Link> Task_EnablingObjective_Links { get; set; } = new List<Task_EnablingObjective_Link>();

        public virtual ICollection<Task_Tool> Task_Tools { get; set; } = new List<Task_Tool>();

        //public virtual ICollection<Task_Procedure_Link> Task_Procedure_Links { get; set; } = new List<Task_Procedure_Link>();

        public virtual ICollection<Employee_Task> Employee_Tasks { get; set; } = new List<Employee_Task>();

        public virtual ICollection<Task_Question> Task_Questions { get; set; } = new List<Task_Question>();

        public virtual ICollection<Task_Position> Task_Positions { get; set; } = new List<Task_Position>();

        public virtual ICollection<Version_Task> Version_Tasks { get; set; } = new List<Version_Task>();

        public virtual ICollection<RR_Task_Link> RR_Task_Links { get; set; } = new List<RR_Task_Link>();

        public virtual ICollection<ILA_TaskObjective_Link> ILA_TaskObjective_Links { get; set; } = new List<ILA_TaskObjective_Link>();

        public virtual ICollection<SegmentObjective_Link> SegmentObjective_Links { get; set; } = new List<SegmentObjective_Link>();

        public virtual ICollection<Procedure_Task_Link> Procedure_Task_Links { get; set; } = new List<Procedure_Task_Link>();

        public virtual ICollection<SafetyHazard_Task_Link> SafetyHazard_Task_Links { get; set; } = new List<SafetyHazard_Task_Link>();

        public virtual ICollection<Task_Reference_Link> Task_Reference_Links { get; set; } = new List<Task_Reference_Link>();

        public virtual ICollection<Task_ILA_Link> Task_ILA_Links { get; set; } = new List<Task_ILA_Link>();

        //public virtual ICollection<Task_RR_Link> Task_RR_Links { get; set; } = new List<Task_RR_Link>();

        public virtual ICollection<Task_Collaborator_Link> Task_Collaborator_Links { get; set; } = new List<Task_Collaborator_Link>();

        public virtual ICollection<SimulatorScenarioTaskObjectives_Link_Old> SimulatorScenarioTaskObjectives_Links { get; set; } = new List<SimulatorScenarioTaskObjectives_Link_Old>();

        public virtual ICollection<Task_History> Task_Histories { get; set; } = new List<Task_History>();

        public virtual ICollection<Task_MetaTask_Link> Task_MetaTask_Links { get; set; } = new List<Task_MetaTask_Link>();

        public virtual ICollection<Task_Suggestion> Task_Suggestions { get; set; } = new List<Task_Suggestion>();

        public virtual ICollection<Task_TrainingGroup> Task_TrainingGroups { get; set; } = new List<Task_TrainingGroup>();


        public virtual ICollection<Position_Task> Position_Tasks { get; set; } = new List<Position_Task>();

        public virtual ICollection<TaskQualification> TaskQualifications { get; set; } = new List<TaskQualification>();
        public virtual ICollection<DIFSurvey_Task> DIFSurvey_Tasks { get; set; } = new List<DIFSurvey_Task>();
        public virtual ICollection<TaskReview> TaskReviews { get; set; } = new List<TaskReview>();
        public virtual ICollection<SimulatorScenario_Task> SimulatorScenario_Tasks { get; set; } = new List<SimulatorScenario_Task>();
        public virtual ICollection<SimulatorScenario_Task_Criteria> SimulatorScenario_Task_Criterias { get; set; } = new List<SimulatorScenario_Task_Criteria>();

        public string getFullNumber()
        {
            return
                (SubdutyArea?.DutyArea == null ? "" : SubdutyArea?.DutyArea.Letter)
                 + 
                (SubdutyArea?.DutyArea == null ? "0" : SubdutyArea?.DutyArea.Number)
                + "." +
                (SubdutyArea == null ? "0" : SubdutyArea.SubNumber)
                + "." +
                Number.ToString();
        }

        public Task()
        {
        }

        public Task(int subdutyAreaId, string description, int number, string criteria, bool critical, string references, int requiredTime, string abreviation, string taskCriteriaUpload, string image, bool isMeta, bool isReliability, string conditions, DateOnly effectiveDate)
        {
            SubdutyAreaId = subdutyAreaId;
            Description = description;
            Number = number;
            Criteria = criteria;
            Critical = critical;
            References = references;
            RequiredTime = requiredTime;
            Abreviation = abreviation;
            TaskCriteriaUpload = taskCriteriaUpload;
            Image = image;
            IsMeta = isMeta;
            IsReliability = isReliability;
            Conditions = conditions;
            EffectiveDate = effectiveDate;
        }

        public Task_Step AddStep(Task_Step task_Step)
        {
            if (!Task_Steps.Any(x => x.Description.Trim().ToLower() == task_Step.Description.Trim().ToLower()))
            {
                AddEntityToNavigationProperty<Task_Step>(task_Step);
            }

            return task_Step;
        }

        public void RemoveStep(Task_Step task_Step)
        {
            if (Task_Steps.Any(x => x.Id == task_Step.Id))
            {
                RemoveEntityFromNavigationProperty<Task_Step>(task_Step);
            }
        }

        public Task_Suggestion AddSuggestion(Task_Suggestion task_Suggestion)
        {
            if (!Task_Suggestions.Any(x => x.Description.Trim().ToLower() == task_Suggestion.Description.Trim().ToLower()))
            {
                AddEntityToNavigationProperty<Task_Suggestion>(task_Suggestion);
            }
            return task_Suggestion;
        }

        public void RemoveSuggestion(Task_Suggestion task_Suggestion)
        {
            if (Task_Suggestions.Any(x => x.Description.Trim().ToLower() == task_Suggestion.Description.Trim().ToLower()))
            {
                RemoveEntityFromNavigationProperty<Task_Suggestion>(task_Suggestion);
            }
        }

        public Task_Tool AddTool(Tool tool, int taskId)
        {
            Task_Tool task_Tool = Task_Tools.FirstOrDefault(x => x.ToolId == tool.Id);
            if (task_Tool == null)
            {
                task_Tool = new Task_Tool(this, tool);
                AddEntityToNavigationProperty<Task_Tool>(task_Tool);
            }

            return task_Tool;
        }

        public void RemoveTool(Tool tool)
        {
            var task_Tool = Task_Tools.FirstOrDefault(x => x.ToolId == tool.Id);
            if (task_Tool != null)
            {
                RemoveEntityFromNavigationProperty<Task_Tool>(task_Tool);
            }
        }

        public Task_Question AddQuestion(Task_Question question)
        {
            if (!Task_Questions.Any(x => x.Question.Trim().ToLower() == question.Question.Trim().ToLower() && x.Answer.Trim().ToLower() == question.Answer.Trim().ToLower()))
            {
                AddEntityToNavigationProperty<Task_Question>(question);
            }

            return question;
        }

        public void RemoveQuestion(Task_Question question)
        {
            if (Task_Questions.Any(x => x.Id == question.Id))
            {
                RemoveEntityFromNavigationProperty<Task_Question>(question);
            }
        }

        public Task_TrainingGroup LinkTrainingGroup(TrainingGroup group)
        {
            Task_TrainingGroup task_tr = Task_TrainingGroups.FirstOrDefault(x => x.TrainingGroupId == group.Id && x.TaskId == this.Id);
            if (task_tr == null)
            {
                task_tr = new Task_TrainingGroup(this, group);
                AddEntityToNavigationProperty<Task_TrainingGroup>(task_tr);
            }
            return task_tr;
        }

        public void UnlinkTrainingGroup(TrainingGroup group)
        {
            Task_TrainingGroup task_tr = Task_TrainingGroups.FirstOrDefault(x => x.TrainingGroupId == group.Id && x.TaskId == this.Id);
            if (task_tr != null)
            {
                RemoveEntityFromNavigationProperty<Task_TrainingGroup>(task_tr);
            }
        }

        public Task_EnablingObjective_Link LinkEnablingObjectives(EnablingObjective enablingObjective, bool linkProc)
        {
            Task_EnablingObjective_Link taskEO_Link = Task_EnablingObjective_Links.FirstOrDefault(x => x.EnablingObjectiveId == enablingObjective.Id);
            if (taskEO_Link == null)
            {
                taskEO_Link = new Task_EnablingObjective_Link(this, enablingObjective);
                AddEntityToNavigationProperty<Task_EnablingObjective_Link>(taskEO_Link);
                if (linkProc)
                {
                    if (enablingObjective.Procedure_EnablingObjective_Links != null)
                    {
                        foreach (var proc in enablingObjective.Procedure_EnablingObjective_Links)
                        {
                            LinkProcedure(proc.Procedure, false);
                        }
                    }

                    if (enablingObjective.SafetyHazard_EO_Links != null)
                    {
                        foreach (var sh in enablingObjective.SafetyHazard_EO_Links)
                        {
                            LinkSaftyHazard(sh.SaftyHazard);
                        }
                    }
                }
            }

            return taskEO_Link;
        }

        public void UnlinkEnablingObjective(EnablingObjective enablingObjective)
        {
            Task_EnablingObjective_Link taskEO_Link = Task_EnablingObjective_Links.FirstOrDefault(x => x.EnablingObjectiveId == enablingObjective.Id);
            if (taskEO_Link != null)
            {
                RemoveEntityFromNavigationProperty<Task_EnablingObjective_Link>(taskEO_Link);
            }
        }

        public Procedure_Task_Link LinkProcedure(Procedure procedure, bool linkEOs)
        {
            Procedure_Task_Link tasks_Procedure = Procedure_Task_Links.FirstOrDefault(x => x.ProcedureId == procedure.Id && x.TaskId == this.Id);
            if (tasks_Procedure == null)
            {
                tasks_Procedure = new Procedure_Task_Link(procedure, this);
                AddEntityToNavigationProperty<Procedure_Task_Link>(tasks_Procedure);
                if (linkEOs)
                {
                    if (procedure.Procedure_EnablingObjective_Links != null)
                    {
                        foreach (var eo in procedure.Procedure_EnablingObjective_Links)
                        {
                            LinkEnablingObjectives(eo.EnablingObjective, false);
                        }
                    }

                    //        if (procedure.Procedure_SaftyHazard_Links != null)
                    //        {
                    //            foreach (var sh in procedure.Procedure_SaftyHazard_Links)
                    //            {
                    //                LinkSaftyHazard(sh.SaftyHazard);
                    //            }
                    //        }
                    //    }
                    //}

                    //return tasks_Procedure;
                }

            }
            return tasks_Procedure;
        }
        public void UnlinkProcedure(Procedure procedure)
        {
            Procedure_Task_Link tasks_Procedure = Procedure_Task_Links.FirstOrDefault(x => x.ProcedureId == procedure.Id);
            if (tasks_Procedure != null)
            {
                RemoveEntityFromNavigationProperty<Procedure_Task_Link>(tasks_Procedure);
            }
        }

        public SafetyHazard_Task_Link LinkSaftyHazard(SaftyHazard saftyHazard)
        {
            SafetyHazard_Task_Link tasks_SaftyHazard_Link = SafetyHazard_Task_Links.FirstOrDefault(x => x.SaftyHazardId == saftyHazard.Id);
            if (tasks_SaftyHazard_Link == null)
            {
                tasks_SaftyHazard_Link = new SafetyHazard_Task_Link(saftyHazard, this);
                AddEntityToNavigationProperty<SafetyHazard_Task_Link>(tasks_SaftyHazard_Link);
            }

            return tasks_SaftyHazard_Link;
        }

        public void UnlinkSaftyHazard(SaftyHazard saftyHazard)
        {
            SafetyHazard_Task_Link tasks_SaftyHazard_Link = SafetyHazard_Task_Links.FirstOrDefault(x => x.SaftyHazardId == saftyHazard.Id);
            if (tasks_SaftyHazard_Link != null)
            {
                RemoveEntityFromNavigationProperty<SafetyHazard_Task_Link>(tasks_SaftyHazard_Link);
            }
        }

        public Position_Task LinkPosition(Position position)
        {
            Position_Task task_Position = Position_Tasks.FirstOrDefault(x => x.PositionId == position.Id);
            if (task_Position == null)
            {
                task_Position = new Position_Task(position, this);
                AddEntityToNavigationProperty(task_Position);
            }

            return task_Position;
        }

        public void UnlinkPosition(Position position)
        {
            Position_Task task_Position = Position_Tasks.FirstOrDefault(x => x.PositionId == position.Id);
            if (task_Position != null)
            {
                RemoveEntityFromNavigationProperty(task_Position);
            }
        }

        public Task_Reference_Link LinkTaskReference(Task_Reference tRef)
        {
            Task_Reference_Link t_ref_link = Task_Reference_Links.FirstOrDefault(x => x.TaskId == this.Id && x.TaskReferenceId == tRef.Id);
            if (t_ref_link != null)
            {
                return t_ref_link;
            }

            t_ref_link = new Task_Reference_Link(this, tRef);
            AddEntityToNavigationProperty<Task_Reference_Link>(t_ref_link);
            return t_ref_link;
        }

        public void UnlinkTaskReference(Task_Reference tRef)
        {
            Task_Reference_Link t_ref_link = Task_Reference_Links.FirstOrDefault(x => x.TaskId == this.Id && x.TaskReferenceId == tRef.Id);
            if (t_ref_link != null)
            {
                RemoveEntityFromNavigationProperty<Task_Reference_Link>(t_ref_link);
            }
        }

        public ILA_TaskObjective_Link LinkILA(ILA ila)
        {
            ILA_TaskObjective_Link t_ila_link = ILA_TaskObjective_Links.FirstOrDefault(x => x.TaskId == this.Id && x.ILAId == ila.Id);
            if (t_ila_link != null)
            {
                return t_ila_link;
            }

            t_ila_link = new ILA_TaskObjective_Link(ila, this);
            AddEntityToNavigationProperty<ILA_TaskObjective_Link>(t_ila_link);
            return t_ila_link;
        }

        public void UnlinkILA(ILA ila)
        {
            ILA_TaskObjective_Link t_ila_link = ILA_TaskObjective_Links.FirstOrDefault(x => x.TaskId == this.Id && x.ILAId == ila.Id);
            if (t_ila_link != null)
            {
                RemoveEntityFromNavigationProperty<ILA_TaskObjective_Link>(t_ila_link);
            }
        }

        public RR_Task_Link LinkRR(RegulatoryRequirement rr)
        {
            RR_Task_Link t_rr_link = RR_Task_Links.FirstOrDefault(x => x.TaskId == this.Id && x.RegRequirementId == rr.Id);
            if (t_rr_link != null)
            {
                return t_rr_link;
            }

            t_rr_link = new RR_Task_Link(rr, this);
            AddEntityToNavigationProperty<RR_Task_Link>(t_rr_link);
            return t_rr_link;
        }

        public void UnlinkRR(RegulatoryRequirement rr)
        {
            RR_Task_Link t_rr_link = RR_Task_Links.FirstOrDefault(x => x.TaskId == this.Id && x.RegRequirementId == rr.Id);
            if (t_rr_link != null)
            {
                RemoveEntityFromNavigationProperty<RR_Task_Link>(t_rr_link);
            }
        }

        public void UnlinkRRs()
        {
            RemoveEntitiesFromNavigationProperty<RR_Task_Link>(RR_Task_Links);
        }

        public Task_Collaborator_Link LinkTaskCollab(Task_CollaboratorInvitation tcolab, Boolean isCreator)
        {
            Task_Collaborator_Link t_colab_link = Task_Collaborator_Links.FirstOrDefault(x => x.TaskId == this.Id && x.TaskCollabInviteId == tcolab.Id);
            if (t_colab_link != null)
            {
                return t_colab_link;
            }

            t_colab_link = new Task_Collaborator_Link(this, tcolab);
            t_colab_link.isTaskCreator = isCreator;
            AddEntityToNavigationProperty<Task_Collaborator_Link>(t_colab_link);
            return t_colab_link;
        }

        public void UnlinkTaskCollab(Task_CollaboratorInvitation tcolab)
        {
            Task_Collaborator_Link t_colab_link = Task_Collaborator_Links.FirstOrDefault(x => x.TaskId == this.Id && x.TaskCollabInviteId == tcolab.Id);
            if (t_colab_link != null)
            {
                RemoveEntityFromNavigationProperty<Task_Collaborator_Link>(t_colab_link);
            }
        }

        public void UnlinkTaskCollab()
        {
            RemoveEntitiesFromNavigationProperty<Task_Collaborator_Link>(Task_Collaborator_Links);
        }

        public Task_MetaTask_Link LinkMetaTask(Task task)
        {
            Task_MetaTask_Link t_meta_link = Task_MetaTask_Links.FirstOrDefault(x => x.Meta_TaskId == this.Id && x.TaskId == task.Id);
            if (t_meta_link != null)
            {
                return t_meta_link;
            }
            t_meta_link = new Task_MetaTask_Link(task, this);
            AddEntityToNavigationProperty<Task_MetaTask_Link>(t_meta_link);
            return t_meta_link;
        }

        public void UnlinkMetaTask(Task task)
        {
            Task_MetaTask_Link t_meta_link = Task_MetaTask_Links.FirstOrDefault(x => x.Meta_TaskId == this.Id && x.TaskId == task.Id);

            if (t_meta_link != null)
            {
                RemoveEntityFromNavigationProperty<Task_MetaTask_Link>(t_meta_link);
            }
        }

        public void UnlinkMetaTasks()
        {
            List<Task_MetaTask_Link> t_meta_links = Task_MetaTask_Links.Where(x => x.Meta_TaskId == this.Id).ToList();

            if (t_meta_links.Count() > 0)
            {
                RemoveEntitiesFromNavigationProperty<Task_MetaTask_Link>(t_meta_links);
            }
        }

        public void UpdateVersion(bool isSignificant)
        {
            // if (isSignificant)
            // {
            //     MinorVersion = 0;
            //     MajorVersion++;
            // }
            // else
            // {
            //     MinorVersion++;
            // }
        }

        public Version_Task CreateSnapshot(int state = 2)
        {
            return new Version_Task(this, false, "", state);
        }

        public List<Employee_Task> CreateEmployeeTask(Position position)
        {
            var emp_tasks = new List<Employee_Task>();
            foreach (var emp in position.EmployeePositions.Select(r => r.Employee))
            {
                emp_tasks.Add(CreateEmployeeTask(emp));
            }

            return emp_tasks;
        }

        public Employee_Task CreateEmployeeTask(Employee employee)
        {
            var employee_task = new Employee_Task(employee.Id, Id);
            AddEntityToNavigationProperty(employee_task);
            return employee_task;
        }

        public List<Employee_Task> CreateEmployeeTask()
        {
            List<Employee_Task> employee_Tasks = new List<Employee_Task>();
            foreach (var pos in Task_Positions)
            {
                employee_Tasks.AddRange(CreateEmployeeTask(pos.Position));
            }

            return employee_Tasks;
        }
        public override void Delete()
        {
            base.Delete();
            AddDomainEvent(new Domain.Events.Core.OnTaskDeleted(this));
        }

        public Task Copy(string createdBy, int subDutyAreaId, string description, DateTime effectiveDate, int taskNumber, bool isReliability, List<int> positions)
        {
            var copy = Copy<Task>(createdBy) as Task;

            copy.IsReliability = isReliability;
            copy.SubdutyAreaId = subDutyAreaId;
            copy.Description = description;
            copy.Number = taskNumber;

            copy.RequalificationDueDate = DateOnly.FromDateTime(effectiveDate).AddMonths(6);

            copy.Position_Tasks = copy.Position_Tasks.Where(r => positions.IndexOf(r.PositionId) >= 0).ToList();

            return copy;
        }

        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<T>(createdBy) as Task;

            //Task_Steps
            copy.Task_Steps = new List<Task_Step>();
            foreach (var taskStep in this.Task_Steps)
            {
                var taskStepCopy = taskStep.Copy<Task_Step>(createdBy);
                taskStepCopy.TaskId = 0;
                copy.Task_Steps.Add(taskStepCopy);
            }

            //Task_Tools
            copy.Task_Tools = new List<Task_Tool>();
            foreach (var taskTool in this.Task_Tools)
            {
                var taskToolCopy = taskTool.Copy<Task_Tool>(createdBy);
                taskToolCopy.TaskId = 0;
                copy.Task_Tools.Add(taskToolCopy);
            }

            //Task_Questions
            copy.Task_Questions = new List<Task_Question>();
            foreach (var taskQuestion in this.Task_Questions)
            {
                var taskQuestionCopy = taskQuestion.Copy<Task_Question>(createdBy);
                taskQuestionCopy.TaskId = 0;
                copy.Task_Questions.Add(taskQuestionCopy);
            }

            //Task_TrainingGroups
            copy.Task_TrainingGroups = new List<Task_TrainingGroup>();
            foreach (var taskTrainingGroup in this.Task_TrainingGroups)
            {
                var taskTrainingGroupCopy = taskTrainingGroup.Copy<Task_TrainingGroup>(createdBy);
                taskTrainingGroupCopy.TaskId = 0;
                copy.Task_TrainingGroups.Add(taskTrainingGroupCopy);
            }

            //Task_Suggestions
            copy.Task_Suggestions = new List<Task_Suggestion>();
            foreach (var taskSuggestion in this.Task_Suggestions)
            {
                var taskSuggestionCopy = taskSuggestion.Copy<Task_Suggestion>(createdBy);
                taskSuggestionCopy.TaskId = 0;
                copy.Task_Suggestions.Add(taskSuggestionCopy);
            }

            //Procedure_Task_Links
            copy.Procedure_Task_Links = new List<Procedure_Task_Link>();
            foreach (var procedureTaskLink in this.Procedure_Task_Links)
            {
                var procedureTaskLinkCopy = procedureTaskLink.Copy<Procedure_Task_Link>(createdBy);
                procedureTaskLinkCopy.TaskId = 0;
                copy.Procedure_Task_Links.Add(procedureTaskLinkCopy);
            }

            //ILA_TaskObjective_Links
            copy.ILA_TaskObjective_Links = new List<ILA_TaskObjective_Link>();
            foreach (var ilaTaskObjectiveLink in this.ILA_TaskObjective_Links)
            {
                var ilaTaskObjectiveLinkCopy = ilaTaskObjectiveLink.Copy<ILA_TaskObjective_Link>(createdBy);
                ilaTaskObjectiveLinkCopy.TaskId = 0;
                copy.ILA_TaskObjective_Links.Add(ilaTaskObjectiveLinkCopy);
            }

            //RR_Task_Links
            copy.RR_Task_Links = new List<RR_Task_Link>();
            foreach (var rrTaskLink in this.RR_Task_Links)
            {
                var rrTaskLinkCopy = rrTaskLink.Copy<RR_Task_Link>(createdBy);
                rrTaskLinkCopy.TaskId = 0;
                copy.RR_Task_Links.Add(rrTaskLinkCopy);
            }

            //SafetyHazard_Task_Links
            copy.SafetyHazard_Task_Links = new List<SafetyHazard_Task_Link>();
            foreach (var safteyHazardTaskLink in this.SafetyHazard_Task_Links)
            {
                var safteyHazardTaskLinkCopy = safteyHazardTaskLink.Copy<SafetyHazard_Task_Link>(createdBy);
                safteyHazardTaskLinkCopy.TaskId = 0;
                copy.SafetyHazard_Task_Links.Add(safteyHazardTaskLinkCopy);
            }

            //Task_EnablingObjective_Links
            copy.Task_EnablingObjective_Links = new List<Task_EnablingObjective_Link>();
            foreach (var taskEnablingObjectiveLink in this.Task_EnablingObjective_Links)
            {
                var taskEnablingObjectiveLinkCopy = taskEnablingObjectiveLink.Copy<Task_EnablingObjective_Link>(createdBy);
                taskEnablingObjectiveLinkCopy.TaskId = 0;
                copy.Task_EnablingObjective_Links.Add(taskEnablingObjectiveLinkCopy);
            }

            //Position_Tasks
            copy.Position_Tasks = new List<Position_Task>();
            foreach (var positionTask in this.Position_Tasks)
            {
                var positionTaskCopy = positionTask.Copy<Position_Task>(createdBy);
                positionTaskCopy.TaskId = 0;
                copy.Position_Tasks.Add(positionTaskCopy);
            }

            //Task_MetaTask_Links
            copy.Task_MetaTask_Links = new List<Task_MetaTask_Link>();
            foreach (var taskMetaTaskLink in this.Task_MetaTask_Links)
            {
                var taskMetaTaskLinkCopy = taskMetaTaskLink.Copy<Task_MetaTask_Link>(createdBy);
                taskMetaTaskLinkCopy.TaskId = 0;
                copy.Task_MetaTask_Links.Add(taskMetaTaskLinkCopy);
            }

            return (T)(object)copy;
        }

    }
}
