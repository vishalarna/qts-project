using System;
using System.Collections.Generic;
using System.Linq;

namespace QTD2.Domain.Entities.Core
{
    public class SaftyHazard : Common.Entity
    {
        public int SaftyHazardCategoryId { get; set; }

        public string Number { get; set; }

        public string Title { get; set; }

        public string RevisionNumber { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string HyperLinks { get; set; }

        public string Text { get; set; }

        public string Files { get; set; }

        public string Image { get; set; }

        public string FileName { get; set; }

        public virtual SaftyHazard_Category SaftyHazard_Category { get; set; }

        //public virtual ICollection<Task_SaftyHazard_Link> Task_SaftyHazard_Links { get; set; } = new List<Task_SaftyHazard_Link>();

        public virtual ICollection<Procedure_SaftyHazard_Link> Procedure_SaftyHazard_Links { get; set; } = new List<Procedure_SaftyHazard_Link>();

        //public virtual ICollection<EnablingObjective_SaftyHazard_Link> EnablingObjective_SaftyHazard_Links { get; set; } = new List<EnablingObjective_SaftyHazard_Link>();

        public virtual ICollection<SaftyHazard_Abatement> SaftyHazard_Abatements { get; set; } = new List<SaftyHazard_Abatement>();

        public virtual ICollection<SaftyHazard_Control> SaftyHazard_Controls { get; set; } = new List<SaftyHazard_Control>();

        public virtual ICollection<Version_SaftyHazard> Version_SaftyHazards { get; set; } = new List<Version_SaftyHazard>();

        //public virtual ICollection<RR_SafetyHazard_Link> RR_SafetyHazard_Links { get; set; } = new List<RR_SafetyHazard_Link>();

        public virtual ICollection<ILA_SafetyHazard_Link> ILA_SafetyHazard_Links { get; set; } = new List<ILA_SafetyHazard_Link>();

        public virtual ICollection<SaftyHazard_RR_Link> SaftyHazard_RR_Links { get; set; } = new List<SaftyHazard_RR_Link>();

        public virtual ICollection<SafetyHazard_ILA_Link> SafetyHazard_ILA_Links { get; set; } = new List<SafetyHazard_ILA_Link>();

        //public virtual ICollection<SafetyHazard_Procedure_Link> SafetyHazard_Procedure_Links { get; set; } = new List<SafetyHazard_Procedure_Link>();

        public virtual ICollection<SafetyHazard_EO_Link> SafetyHazard_EO_Links { get; set; } = new List<SafetyHazard_EO_Link>();

        public virtual ICollection<SafetyHazard_Task_Link> SafetyHazard_Task_Links { get; set; } = new List<SafetyHazard_Task_Link>();

        public virtual ICollection<SafetyHazard_Set_Link> SafetyHazard_Set_Links { get; set; } = new List<SafetyHazard_Set_Link>();

        public virtual ICollection<SafetyHazard_History> SafetyHazard_Histories { get; set; } = new List<SafetyHazard_History>();

        public virtual ICollection<SafetyHazard_Tool_Link> SafetyHazard_Tool_Links { get; set; } = new List<SafetyHazard_Tool_Link>();

        public SaftyHazard()
        {
        }

        public SaftyHazard(
            int saftyHazardCategoryId,
            string title,
            string number,
            string revisionNumber,
            DateTime effectiveDate,
            string hyperLinks,
            string set,
            string text,
            string files,
            string image,
            string fileName)
        {
            SaftyHazardCategoryId = saftyHazardCategoryId;
            Title = title;
            Number = number;
            RevisionNumber = revisionNumber;
            EffectiveDate = effectiveDate;
            HyperLinks = hyperLinks;
            Text = text;
            Files = files;
            Image = image;
            FileName = fileName;
        }

        public SaftyHazard_Abatement AddAbatement(SaftyHazard_Abatement saftyHazard_Abatement)
        {
            var abatement = SaftyHazard_Abatements.FirstOrDefault(x => x.Description.ToLower() == saftyHazard_Abatement.Description.ToLower());
            if (abatement == null)
            {
                AddEntityToNavigationProperty<SaftyHazard_Abatement>(saftyHazard_Abatement);
            }

            return saftyHazard_Abatement;
        }

        public void RemoveAbatement(SaftyHazard_Abatement saftyHazard_Abatement)
        {
            var abatement = SaftyHazard_Abatements.FirstOrDefault(x => x.Description.ToLower() == saftyHazard_Abatement.Description.ToLower());
            if (abatement != null)
            {
                RemoveEntityFromNavigationProperty<SaftyHazard_Abatement>(saftyHazard_Abatement);
            }
        }

        public SaftyHazard_Control AddControl(SaftyHazard_Control saftyHazard_Control)
        {
            var control = SaftyHazard_Controls.FirstOrDefault(x => x.Description.ToLower() == saftyHazard_Control.Description.ToLower());
            if (control == null)
            {
                AddEntityToNavigationProperty<SaftyHazard_Control>(saftyHazard_Control);
            }

            return saftyHazard_Control;
        }

        public void RemoveControl(SaftyHazard_Control saftyHazard_Control)
        {
            var control = SaftyHazard_Controls.FirstOrDefault(x => x.Description.ToLower() == saftyHazard_Control.Description.ToLower());
            if (control != null)
            {
                RemoveEntityFromNavigationProperty<SaftyHazard_Control>(saftyHazard_Control);
            }
        }

        /* Link and unlink functions for safty hazard rr link start */
        public SaftyHazard_RR_Link LinkRegRequirement(RegulatoryRequirement regulatoryRequirement)
        {
            SaftyHazard_RR_Link saftyHazard_rr_link = SaftyHazard_RR_Links.FirstOrDefault(x => x.RegulatoryRequirementId == regulatoryRequirement.Id && x.SafetyHazardId == this.Id);
            if (saftyHazard_rr_link != null)
            {
                return saftyHazard_rr_link;
            }

            saftyHazard_rr_link = new SaftyHazard_RR_Link(regulatoryRequirement, this);
            AddEntityToNavigationProperty<SaftyHazard_RR_Link>(saftyHazard_rr_link);
            return saftyHazard_rr_link;
        }

        public void UnLinkRegRequirement(RegulatoryRequirement regulatoryRequirement)
        {
            SaftyHazard_RR_Link saftyHazard_rr_link = SaftyHazard_RR_Links.FirstOrDefault(x => x.RegulatoryRequirementId == regulatoryRequirement.Id && x.SafetyHazardId == this.Id);
            if (saftyHazard_rr_link != null)
            {
                RemoveEntityFromNavigationProperty<SaftyHazard_RR_Link>(saftyHazard_rr_link);
            }
        }

        /* Link and unlink functions for safty hazard rr link end */

        public Procedure_SaftyHazard_Link LinkProcedure(Procedure procedure)
        {
            Procedure_SaftyHazard_Link proc_SH_Link = Procedure_SaftyHazard_Links.FirstOrDefault(x => x.SaftyHazardId == this.Id && x.ProcedureId == procedure.Id);
            if (proc_SH_Link == null)
            {
                proc_SH_Link = new Procedure_SaftyHazard_Link(procedure, this);
                AddEntityToNavigationProperty<Procedure_SaftyHazard_Link>(proc_SH_Link);
            }

            return proc_SH_Link;

            //SafetyHazard_Procedure_Link safetyHazard_Procedure_Link = SafetyHazard_Procedure_Links.FirstOrDefault(x => x.ProcedureId == procedure.Id && x.SafetyHazardId == this.Id);
            //if (safetyHazard_Procedure_Link != null)
            //{
            //    return safetyHazard_Procedure_Link;
            //}

            //safetyHazard_Procedure_Link = new SafetyHazard_Procedure_Link(procedure, this);
            //AddEntityToNavigationProperty<SafetyHazard_Procedure_Link>(safetyHazard_Procedure_Link);
            //return safetyHazard_Procedure_Link;
        }

        public void UnLinkProcedure(Procedure procedure)
        {
            Procedure_SaftyHazard_Link safetyHazard_Procedure_Link = Procedure_SaftyHazard_Links.FirstOrDefault(x => x.ProcedureId == procedure.Id && x.SaftyHazardId == this.Id);
            if (safetyHazard_Procedure_Link != null)
            {
                RemoveEntityFromNavigationProperty<Procedure_SaftyHazard_Link>(safetyHazard_Procedure_Link);
            }
        }

        public SafetyHazard_ILA_Link LinkILA(ILA ila)
        {
            SafetyHazard_ILA_Link safetyHazard_ILA_Link = SafetyHazard_ILA_Links.FirstOrDefault(x => x.ILAId == ila.Id && x.SafetyHazardId == this.Id);
            if (safetyHazard_ILA_Link != null)
            {
                return safetyHazard_ILA_Link;
            }

            safetyHazard_ILA_Link = new SafetyHazard_ILA_Link(ila, this);
            AddEntityToNavigationProperty<SafetyHazard_ILA_Link>(safetyHazard_ILA_Link);
            return safetyHazard_ILA_Link;
        }

        public void UnLinkILA(ILA ila)
        {
            SafetyHazard_ILA_Link safetyHazard_ILA_Link = SafetyHazard_ILA_Links.FirstOrDefault(x => x.ILAId == ila.Id && x.SafetyHazardId == this.Id);
            if (safetyHazard_ILA_Link != null)
            {
                RemoveEntityFromNavigationProperty<SafetyHazard_ILA_Link>(safetyHazard_ILA_Link);
            }
        }

        public SafetyHazard_EO_Link LinkEO(EnablingObjective eo)
        {
            SafetyHazard_EO_Link sh_eo_link = SafetyHazard_EO_Links.FirstOrDefault(x => x.EOID == eo.Id && x.SafetyHazardId == this.Id);
            if (sh_eo_link != null)
            {
                return sh_eo_link;
            }

            sh_eo_link = new SafetyHazard_EO_Link(this, eo);
            AddEntityToNavigationProperty<SafetyHazard_EO_Link>(sh_eo_link);
            return sh_eo_link;
        }

        public void UnlinkEO(EnablingObjective eo)
        {
            SafetyHazard_EO_Link sh_eo_link = SafetyHazard_EO_Links.FirstOrDefault(x => x.EOID == eo.Id && x.SafetyHazardId == this.Id);
            if (sh_eo_link != null)
            {
                RemoveEntityFromNavigationProperty<SafetyHazard_EO_Link>(sh_eo_link);
            }
        }

        public SafetyHazard_Task_Link LinkTask(Task task)
        {
            SafetyHazard_Task_Link sh_task_link = SafetyHazard_Task_Links.FirstOrDefault(x => x.TaskId == task.Id && x.SaftyHazardId == this.Id);
            if (sh_task_link != null)
            {
                return sh_task_link;
            }

            sh_task_link = new SafetyHazard_Task_Link(this, task);
            AddEntityToNavigationProperty<SafetyHazard_Task_Link>(sh_task_link);
            return sh_task_link;
        }

        public void UnlinkTask(Task task)
        {
            SafetyHazard_Task_Link sh_task_link = SafetyHazard_Task_Links.FirstOrDefault(x => x.TaskId == task.Id && x.SaftyHazardId == this.Id);
            if (sh_task_link != null)
            {
                RemoveEntityFromNavigationProperty<SafetyHazard_Task_Link>(sh_task_link);
            }
        }

        public SafetyHazard_Set_Link LinkSafetyHazardSet(SafetyHazard_Set shzSet)
        {
            SafetyHazard_Set_Link shz_set_link = SafetyHazard_Set_Links.FirstOrDefault(x => x.SafetyHazardSetId == shzSet.Id && x.SafetyHazardId == this.Id);
            if (shz_set_link != null)
            {
                return shz_set_link;
            }

            shz_set_link = new SafetyHazard_Set_Link(this, shzSet);
            AddEntityToNavigationProperty<SafetyHazard_Set_Link>(shz_set_link);
            return shz_set_link;
        }

        public void UnlinkSafetyHazardSet(SafetyHazard_Set shzSet)
        {
            SafetyHazard_Set_Link shz_set_link = SafetyHazard_Set_Links.FirstOrDefault(x => x.SafetyHazardSetId == shzSet.Id && x.SafetyHazardId == this.Id);
            if (shz_set_link != null)
            {
                RemoveEntityFromNavigationProperty<SafetyHazard_Set_Link>(shz_set_link);
            }
        }

        public SafetyHazard_Tool_Link LinkSafetyHazardTool(Tool tool)
        {
            SafetyHazard_Tool_Link sh_tool_link = SafetyHazard_Tool_Links.FirstOrDefault(x => x.ToolId == tool.Id && x.SafetyHazardId == this.Id);
            if (sh_tool_link != null)
            {
                return sh_tool_link;
            }

            sh_tool_link = new SafetyHazard_Tool_Link(this, tool);
            AddEntityToNavigationProperty<SafetyHazard_Tool_Link>(sh_tool_link);
            return sh_tool_link;
        }

        public void UnlinkSafetyHazardTool(Tool tool)
        {
            SafetyHazard_Tool_Link sh_tool_link = SafetyHazard_Tool_Links.FirstOrDefault(x => x.ToolId == tool.Id && x.SafetyHazardId == this.Id);

            if (sh_tool_link != null)
            {
                RemoveEntityFromNavigationProperty<SafetyHazard_Tool_Link>(sh_tool_link);
            }
        }

        public Version_SaftyHazard CreateSnapshot()
        {
            return new Version_SaftyHazard(this);
        }
    }
}
