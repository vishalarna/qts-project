using System;
using System.Collections.Generic;
using System.Linq;

namespace QTD2.Domain.Entities.Core
{
    public class EnablingObjective : Common.Entity
    {
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int? TopicId { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }

        public bool isMetaEO { get; set; }

        public bool IsSkillQualification { get; set; }

        public string References { get; set; }

        public string Criteria { get; set; }

        public string Conditions { get; set; }

        public bool IsFullyLoaded
        {
            get
            {
                return GetFullLoadedStatus();
            }
        }
        public string FullNumber
        {
            get
            {
                //todo ensure fully loading and not  unloaded nav propers
                return getFullNumber();
            }
        }

        public DateTime EffectiveDate { get; set; }

        public virtual EnablingObjective_Topic EnablingObjective_Topic { get; set; }

        public virtual EnablingObjective_Category EnablingObjective_Category { get; set; }

        public virtual EnablingObjective_SubCategory EnablingObjective_SubCategory { get; set; }

        public virtual ICollection<Task_EnablingObjective_Link> Task_EnablingObjective_Links { get; set; } = new List<Task_EnablingObjective_Link>();

        public virtual ICollection<Procedure_EnablingObjective_Link> Procedure_EnablingObjective_Links { get; set; } = new List<Procedure_EnablingObjective_Link>();

        //public virtual ICollection<EnablingObjective_SaftyHazard_Link> EnablingObjective_SaftyHazard_Links { get; set; } = new List<EnablingObjective_SaftyHazard_Link>();

        //public virtual ICollection<EnablingObjective_Procedure_Link> EnablingObjective_Procedure_Links { get; set; } = new List<EnablingObjective_Procedure_Link>();

        public virtual ICollection<Version_EnablingObjective> Version_EnablingObjectives { get; set; } = new List<Version_EnablingObjective>();

        public virtual ICollection<RegRequirement_EO_Link> RegRequirement_EO_Links { get; set; } = new List<RegRequirement_EO_Link>();

        public virtual ICollection<ILA_EnablingObjective_Link> ILA_EnablingObjective_Links { get; set; } = new List<ILA_EnablingObjective_Link>();

        public virtual ICollection<SegmentObjective_Link> SegmentObjective_Links { get; set; } = new List<SegmentObjective_Link>();

        public virtual ICollection<TestItem> TestItems { get; set; } = new List<TestItem>();

        public virtual ICollection<SafetyHazard_EO_Link> SafetyHazard_EO_Links { get; set; } = new List<SafetyHazard_EO_Link>();

        public virtual ICollection<EnablingObjectiveHistory> EnablingObjectiveHistories { get; set; } = new List<EnablingObjectiveHistory>();

        public virtual ICollection<SimulatorScenario_EnablingObjectives_Link_Old> SimulatorScenario_EnablingObjectives_Links { get; set; } = new List<SimulatorScenario_EnablingObjectives_Link_Old>();
        public virtual ICollection<SimulatorScenario_EnablingObjective> SimulatorScenario_EnablingObjectives { get; set; } = new List<SimulatorScenario_EnablingObjective>();

        public virtual ICollection<EnablingObjective_MetaEO_Link> EnablingObjective_MetaEO_Links { get; set; } = new List<EnablingObjective_MetaEO_Link>();

        public virtual ICollection<Positions_SQ> Position_SQs { get; set; } = new List<Positions_SQ>();

        public virtual ICollection<EnablingObjective_Step> EnablingObjective_Steps { get; set; } = new List<EnablingObjective_Step>();

        public virtual ICollection<EnablingObjective_Employee_Link> EnablingObjective_Employee_Links { get; set; } = new List<EnablingObjective_Employee_Link>();

        public virtual ICollection<EnablingObjective_Question> EnablingObjective_Questions { get; set; } = new List<EnablingObjective_Question>();

        public virtual ICollection<EnablingObjective_Suggestion> EnablingObjective_Suggestions { get; set; } = new List<EnablingObjective_Suggestion>();

        public virtual ICollection<EnablingObjective_Tool> EnablingObjective_Tools { get; set; } = new List<EnablingObjective_Tool>();
        public virtual ICollection<SkillQualification> SkillQualifications { get; set; }  = new List<SkillQualification>();

        public EnablingObjective()
        {
        }

        public EnablingObjective(int categoryId, int subCategoryId, int? topicId, string number, string statement, bool isMetaEO, bool isSkillQualification, string references, string criteria, string conditions, DateTime effectiveDate)
        {
            CategoryId = categoryId;
            SubCategoryId = subCategoryId;
            TopicId = topicId;
            Number = number;
            Description = statement;
            this.isMetaEO = isMetaEO;
            References = references;
            Criteria = criteria;
            Conditions = conditions;
            EffectiveDate = effectiveDate;
            SetIsSkillQualification(isSkillQualification);
        }

        public bool GetFullLoadedStatus()
        {
            if ((TopicId.GetValueOrDefault() > 0 && EnablingObjective_Topic != null) || (TopicId.GetValueOrDefault() == 0 && EnablingObjective_Topic == null))
            {
                if (SubCategoryId > 0 && EnablingObjective_SubCategory != null)
                {
                    if (CategoryId > 0 && EnablingObjective_Category != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public string getFullNumber()
        {
            if (IsFullyLoaded)
            {
                return (EnablingObjective_Category == null ? "0" : EnablingObjective_Category.Number)
                    + "." +
                    (EnablingObjective_SubCategory == null ? "0" : EnablingObjective_SubCategory.Number)
                    + "." +
                    (EnablingObjective_Topic == null ? "0" : EnablingObjective_Topic.Number) + "." + Number;
            }
            else
            {
                return Number;
            }
        }

        public Procedure_EnablingObjective_Link LinkProcedure(Procedure procedure, int enablingObjectveId)
        {
            Procedure_EnablingObjective_Link eO_Proc_Link = Procedure_EnablingObjective_Links.FirstOrDefault(x => x.ProcedureId == procedure.Id && x.EnablingObjectiveId == enablingObjectveId);
            if (eO_Proc_Link != null)
            {
                return eO_Proc_Link;
            }

            eO_Proc_Link = new Procedure_EnablingObjective_Link(procedure, this);
            AddEntityToNavigationProperty<Procedure_EnablingObjective_Link>(eO_Proc_Link);
            return eO_Proc_Link;
        }

        public void UnlinkProcedure(Procedure procedure)
        {
            var eO_Proc_Link = Procedure_EnablingObjective_Links.FirstOrDefault(x => x.ProcedureId == procedure.Id);
            RemoveEntityFromNavigationProperty<Procedure_EnablingObjective_Link>(eO_Proc_Link);
        }

        public SafetyHazard_EO_Link LinkSaftyHazard(SaftyHazard saftyHazard, int enablingObjectveId)
        {
            SafetyHazard_EO_Link eO_SH_Link = SafetyHazard_EO_Links.FirstOrDefault(x => x.SafetyHazardId == saftyHazard.Id && x.EOID == enablingObjectveId);
            if (eO_SH_Link != null)
            {
                return eO_SH_Link;
            }

            eO_SH_Link = new SafetyHazard_EO_Link(saftyHazard, this);
            AddEntityToNavigationProperty<SafetyHazard_EO_Link>(eO_SH_Link);
            return eO_SH_Link;
        }

        public void UnlinkSaftyHazard(SaftyHazard saftyHazard)
        {
            var eO_SH_Link = SafetyHazard_EO_Links.FirstOrDefault(x => x.SafetyHazardId == saftyHazard.Id && x.EOID == this.Id);
            if (eO_SH_Link != null)
            {
                RemoveEntityFromNavigationProperty<SafetyHazard_EO_Link>(eO_SH_Link);
            }
        }

        public Task_EnablingObjective_Link LinkTask(Task task)
        {
            Task_EnablingObjective_Link task_eo_link = Task_EnablingObjective_Links.FirstOrDefault(x => x.TaskId == task.Id && x.EnablingObjectiveId == this.Id);
            if (task_eo_link != null)
            {
                return task_eo_link;
            }

            task_eo_link = new Task_EnablingObjective_Link(task, this);
            AddEntityToNavigationProperty<Task_EnablingObjective_Link>(task_eo_link);
            return task_eo_link;
        }

        public void UnlinkTask(Task task)
        {
            Task_EnablingObjective_Link task_eo_link = Task_EnablingObjective_Links.FirstOrDefault(x => x.TaskId == task.Id && x.EnablingObjectiveId == this.Id);
            if (task_eo_link != null)
            {
                RemoveEntityFromNavigationProperty<Task_EnablingObjective_Link>(task_eo_link);
            }
        }

        public RegRequirement_EO_Link LinkRR(RegulatoryRequirement rr)
        {
            RegRequirement_EO_Link rr_eo_link = RegRequirement_EO_Links.FirstOrDefault(x => x.RegulatoryRequirementId == rr.Id && x.EOID == this.Id);
            if (rr_eo_link != null)
            {
                return rr_eo_link;
            }
            else
            {
                rr_eo_link = new RegRequirement_EO_Link(rr, this);
                AddEntityToNavigationProperty(rr_eo_link);
                return rr_eo_link;
            }
        }

        public void UnlinkRR(RegulatoryRequirement rr)
        {
            RegRequirement_EO_Link rr_eo_link = RegRequirement_EO_Links.FirstOrDefault(x => x.RegulatoryRequirementId == rr.Id && x.EOID == this.Id);
            if (rr_eo_link != null)
            {
                RemoveEntityFromNavigationProperty(rr_eo_link);
            }
        }

        public EnablingObjective_Employee_Link LinkEmployee(Employee emp, DateTime startDate)
        {
            EnablingObjective_Employee_Link eo_link = EnablingObjective_Employee_Links.FirstOrDefault(x => x.EOID == this.Id && x.EmployeeId == emp.Id);
            if (eo_link != null)
            {
                return eo_link;
            }
            eo_link = new EnablingObjective_Employee_Link(this, emp, startDate);
            AddEntityToNavigationProperty(eo_link);
            return eo_link;
        }

        public void UnlinkEmployee(Employee emp)
        {
            EnablingObjective_Employee_Link eo_link = EnablingObjective_Employee_Links.FirstOrDefault(x => x.EOID == this.Id && x.EmployeeId == emp.Id);
            if (eo_link != null)
            {
                RemoveEntityFromNavigationProperty(eo_link);
            }
        }

        public ILA_EnablingObjective_Link LinkILA(ILA ila)
        {
            ILA_EnablingObjective_Link ila_eo_link = ILA_EnablingObjective_Links.FirstOrDefault(x => x.ILAId == ila.Id && x.EnablingObjectiveId == this.Id);
            if (ila_eo_link != null)
            {
                return ila_eo_link;
            }
            ila_eo_link = new ILA_EnablingObjective_Link(ila, this);
            AddEntityToNavigationProperty<ILA_EnablingObjective_Link>(ila_eo_link);
            return ila_eo_link;
        }

        public void UnlinkILA(ILA ila)
        {
            ILA_EnablingObjective_Link ila_eo_link = ILA_EnablingObjective_Links.FirstOrDefault(x => x.ILAId == ila.Id && x.EnablingObjectiveId == this.Id);
            if (ila_eo_link != null)
            {
                RemoveEntityFromNavigationProperty(ila_eo_link);
            }
        }

        public EnablingObjective_MetaEO_Link LinkMetaEO(EnablingObjective eo)
        {
            EnablingObjective_MetaEO_Link eo_meta_link = EnablingObjective_MetaEO_Links.FirstOrDefault(x => x.MetaEOId == this.Id && x.EOID == eo.Id);
            if (eo_meta_link != null)
            {
                return eo_meta_link;
            }
            eo_meta_link = new EnablingObjective_MetaEO_Link(this, eo);
            AddEntityToNavigationProperty(eo_meta_link);
            return eo_meta_link;
        }

        public void UnlinkMetaEO(EnablingObjective eo)
        {
            EnablingObjective_MetaEO_Link eo_meta_link = EnablingObjective_MetaEO_Links.FirstOrDefault(x => x.MetaEOId == this.Id && x.EOID == eo.Id);
            if (eo_meta_link != null)
            {
                RemoveEntityFromNavigationProperty<EnablingObjective_MetaEO_Link>(eo_meta_link);
            }
        }

        public void UnlinkAllMetaEO()
        {
            List<EnablingObjective_MetaEO_Link> eo_meta_link = EnablingObjective_MetaEO_Links.Where(x => x.MetaEOId == this.Id).ToList();
            if (eo_meta_link.Count > 0)
            {
                RemoveEntitiesFromNavigationProperty(eo_meta_link);
            }
        }

        public Positions_SQ LinkPositions(Position pos)
        {
            Positions_SQ pos_sq = Position_SQs.FirstOrDefault(x => x.PositionId == pos.Id && x.EOId == this.Id);
            if (pos_sq != null)
            {
                return pos_sq;
            }
            pos_sq = new Positions_SQ(pos, this);
            AddEntityToNavigationProperty<Positions_SQ>(pos_sq);
            return pos_sq;
        }

        public void UnlinkPositions(Position pos)
        {
            Positions_SQ pos_sq = Position_SQs.FirstOrDefault(x => x.PositionId == pos.Id && x.EOId == this.Id);
            if (pos_sq != null)
            {
                RemoveEntityFromNavigationProperty<Positions_SQ>(pos_sq);
            }
        }

        public void UpdateVersion(bool isSignificant)
        {
            if (isSignificant)
            {
                // MinorVersion = 0;
                // MajorVersion++;
            }
            else
            {
                // MinorVersion++;
            }
        }

        public Version_EnablingObjective CreateSnapshot()
        {
            return new Version_EnablingObjective(this);
        }

        public EnablingObjective_Step AddStep(EnablingObjective_Step eo_Step)
        {
            if (!EnablingObjective_Steps.Any(x => x.Description.Trim().ToLower() == eo_Step.Description.Trim().ToLower()))
            {
                AddEntityToNavigationProperty<EnablingObjective_Step>(eo_Step);
            }

            return eo_Step;
        }

        public EnablingObjective_Suggestion AddSuggestion(EnablingObjective_Suggestion eo_Suggestion)
        {
            if (!EnablingObjective_Suggestions.Any(x => x.Description.Trim().ToLower() == eo_Suggestion.Description.Trim().ToLower()))
            {
                AddEntityToNavigationProperty<EnablingObjective_Suggestion>(eo_Suggestion);
            }
            return eo_Suggestion;
        }

        public void RemoveSuggestion(EnablingObjective_Suggestion eo_Suggestion)
        {
            if (EnablingObjective_Suggestions.Any(x => x.Description.Trim().ToLower() == eo_Suggestion.Description.Trim().ToLower()))
            {
                RemoveEntityFromNavigationProperty<EnablingObjective_Suggestion>(eo_Suggestion);
            }
        }

        public EnablingObjective_Tool AddTool(Tool tool, int eoId)
        {
            EnablingObjective_Tool enablingObjective_Tool = EnablingObjective_Tools.FirstOrDefault(x => x.ToolId == tool.Id);
            if (enablingObjective_Tool == null)
            {
                enablingObjective_Tool = new EnablingObjective_Tool(this, tool);
                AddEntityToNavigationProperty<EnablingObjective_Tool>(enablingObjective_Tool);
            }

            return enablingObjective_Tool;
        }

        public void RemoveTool(Tool tool)
        {
            var task_Tool = EnablingObjective_Tools.FirstOrDefault(x => x.ToolId == tool.Id);
            if (task_Tool != null)
            {
                RemoveEntityFromNavigationProperty<EnablingObjective_Tool>(task_Tool);
            }
        }

        public EnablingObjective_Question AddQuestion(EnablingObjective_Question question)
        {
            if (!EnablingObjective_Questions.Any(x => x.Question.Trim().ToLower() == question.Question.Trim().ToLower() && x.Answer.Trim().ToLower() == question.Answer.Trim().ToLower()))
            {
                AddEntityToNavigationProperty<EnablingObjective_Question>(question);
            }

            return question;
        }

        public void RemoveQuestion(EnablingObjective_Question question)
        {
            if (EnablingObjective_Questions.Any(x => x.Id == question.Id))
            {
                RemoveEntityFromNavigationProperty<EnablingObjective_Question>(question);
            }
        }

        public override void Delete()
        {
            base.Delete();
            AddDomainEvent(new Domain.Events.Core.OnEnablingObjectiveDeleted(this));
        }

        public void SetIsSkillQualification(bool isSkillQualification)
        {
            if (IsSkillQualification && !isSkillQualification)
            {
                AddDomainEvent(new Domain.Events.Core.OnEnablingObjectiveRemovedSQStatus(this));
            }

            IsSkillQualification = isSkillQualification;
        }

        public override T Copy<T>(string createdBy)
        {
            var copy = new EnablingObjective
            {
                CategoryId = this.CategoryId,
                SubCategoryId = this.SubCategoryId,
                TopicId = this.TopicId,
                Criteria = this.Criteria,
                Conditions = this.Conditions,
                References = this.References
            };

            foreach (var item in this.TestItems)
            {
                var newItem = item.Copy<TestItem>(createdBy);
                newItem.Id = 0;
                copy.TestItems.Add(newItem);
            }

            foreach (var link in this.Task_EnablingObjective_Links)
            {
                var newLink = link.Copy<Task_EnablingObjective_Link>(createdBy);
                newLink.Id = 0;
                copy.Task_EnablingObjective_Links.Add(newLink);
            }

            foreach (var link in this.SafetyHazard_EO_Links)
            {
                var newLink = link.Copy<SafetyHazard_EO_Link>(createdBy);
                newLink.Id = 0;
                copy.SafetyHazard_EO_Links.Add(newLink);
            }

            foreach (var link in this.Procedure_EnablingObjective_Links ?? Enumerable.Empty<Procedure_EnablingObjective_Link>())
            {
                var newLink = link.Copy<Procedure_EnablingObjective_Link>(createdBy);
                newLink.Id = 0;
                copy.Procedure_EnablingObjective_Links.Add(newLink);
            }

            foreach (var link in this.ILA_EnablingObjective_Links)
            {
                var newLink = link.Copy<ILA_EnablingObjective_Link>(createdBy);
                newLink.Id = 0;
                copy.ILA_EnablingObjective_Links.Add(newLink);
            }

            foreach (var link in this.EnablingObjective_MetaEO_Links)
            {
                var newLink = link.Copy<EnablingObjective_MetaEO_Link>(createdBy);
                newLink.Id = 0;
                copy.EnablingObjective_MetaEO_Links.Add(newLink);
            }

            foreach (var link in this.RegRequirement_EO_Links)
            {
                var newLink = link.Copy<RegRequirement_EO_Link>(createdBy);
                newLink.Id = 0;
                copy.RegRequirement_EO_Links.Add(newLink);
            }

            return (T)(object)copy;
        }

        public void UpdateEO(string number,string description,bool isMetaEO,bool isSkillQualification,DateTime effectiveDate)
        {
            this.Number = number;
            this.Description = description;
            this.isMetaEO = isMetaEO;
            this.IsSkillQualification = isSkillQualification;
            this.EffectiveDate = effectiveDate;
        }

        //public Task_TrainingGroup LinkTrainingGroup(TrainingGroup group)
        //{
        //    Task_TrainingGroup task_tr = Task_TrainingGroups.FirstOrDefault(x => x.TrainingGroupId == group.Id && x.TaskId == this.Id);
        //    if (task_tr == null)
        //    {
        //        task_tr = new Task_TrainingGroup(this, group);
        //        AddEntityToNavigationProperty<Task_TrainingGroup>(task_tr);
        //    }
        //    return task_tr;
        //}

        //public void UnlinkTrainingGroup(TrainingGroup group)
        //{
        //    Task_TrainingGroup task_tr = Task_TrainingGroups.FirstOrDefault(x => x.TrainingGroupId == group.Id && x.TaskId == this.Id);
        //    if (task_tr != null)
        //    {
        //        RemoveEntityFromNavigationProperty<Task_TrainingGroup>(task_tr);
        //    }
        //}
    }


}
