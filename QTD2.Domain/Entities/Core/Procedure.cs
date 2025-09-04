using System;
using System.Collections.Generic;
using System.Linq;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Procedure : Entity
    {
        public int IssuingAuthorityId { get; set; }

        public string Number { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string RevisionNumber { get; set; }

        public DateOnly EffectiveDate { get; set; }

        public byte[] ProceduresFileUpload { get; set; }


        public bool IsDeleted { get; set; }

        public bool IsPublished { get; set; }

        public string Hyperlink { get; set; }

        public string FileName { get; set; }

        public virtual Procedure_IssuingAuthority Procedure_IssuingAuthority { get; set; }

        //public virtual ICollection<Task_Procedure_Link> Task_Procedure_Links { get; set; } = new List<Task_Procedure_Link>();

        public virtual ICollection<Procedure_SaftyHazard_Link> Procedure_SaftyHazard_Links { get; set; } = new List<Procedure_SaftyHazard_Link>();

        public virtual ICollection<Procedure_EnablingObjective_Link> Procedure_EnablingObjective_Links { get; set; } = new List<Procedure_EnablingObjective_Link>();

        public virtual ICollection<Procedure_RR_Link> Procedure_RegRequirement_Links { get; set; } = new List<Procedure_RR_Link>();

        //public virtual ICollection<EnablingObjective_Procedure_Link> EnablingObjective_Procedure_Links { get; set; } = new List<EnablingObjective_Procedure_Link>();

        public virtual ICollection<Version_Procedure> Version_Procedures { get; set; } = new List<Version_Procedure>();

        public virtual ICollection<ILA_Procedure_Link> ILA_Procedure_Links { get; set; } = new List<ILA_Procedure_Link>();

        public virtual ICollection<Procedure_ILA_Link> Procedure_ILA_Links { get; set; } = new List<Procedure_ILA_Link>();

        //public virtual ICollection<SafetyHazard_Procedure_Link> SafetyHazard_Procedure_Links { get; set; } = new List<SafetyHazard_Procedure_Link>();

        public virtual ICollection<Procedure_Task_Link> Procedure_Task_Links { get; set; } = new List<Procedure_Task_Link>();

        //public virtual ICollection<RR_Procedure_Link> RR_Procedure_Links { get; set; } = new List<RR_Procedure_Link>();

        public virtual ICollection<Procedure_StatusHistory> Procedure_StatusHistories { get; set; } = new List<Procedure_StatusHistory>();
        public virtual ICollection<ProcedureReview> ProcedureReviews { get; set; } = new List<ProcedureReview>();
        public virtual ICollection<SimulatorScenario_Procedure> SimulatorScenario_Procedures { get; set; } = new List<SimulatorScenario_Procedure>();
        public Procedure()
        {
        }

        public Procedure(int issuingAuthorityId, string number, string title, string description, string revisionNumber, DateOnly effectiveDate, byte[] proceduresFileUpload,  bool isDeleted, bool isPublished, string hyperlink, string fileName)
        {
            IssuingAuthorityId = issuingAuthorityId;
            Number = number;
            Title = title;
            Description = description;
            RevisionNumber = revisionNumber;
            EffectiveDate = effectiveDate;
            ProceduresFileUpload = proceduresFileUpload;

            IsDeleted = isDeleted;
            IsPublished = isPublished;
            Hyperlink = hyperlink;
            FileName = fileName;
        }

        public Procedure_EnablingObjective_Link LinkEnablingObjective(EnablingObjective enablingObjective)
        {
            Procedure_EnablingObjective_Link proc_EO_Link = Procedure_EnablingObjective_Links.FirstOrDefault(x => x.EnablingObjectiveId == enablingObjective.Id);
            if (proc_EO_Link == null)
            {
                proc_EO_Link = new Procedure_EnablingObjective_Link(this, enablingObjective);
                AddEntityToNavigationProperty<Procedure_EnablingObjective_Link>(proc_EO_Link);
            }

            return proc_EO_Link;
        }

        public void UnlinkEnablingObjective(EnablingObjective enablingObjective)
        {
            Procedure_EnablingObjective_Link proc_EO_Link = Procedure_EnablingObjective_Links.FirstOrDefault(x => x.EnablingObjectiveId == enablingObjective.Id);
            if (proc_EO_Link != null)
            {
                RemoveEntityFromNavigationProperty<Procedure_EnablingObjective_Link>(proc_EO_Link);
            }
        }

        public Procedure_SaftyHazard_Link LinkSaftyHazard(SaftyHazard saftyHazard)
        {
            Procedure_SaftyHazard_Link proc_SH_Link = Procedure_SaftyHazard_Links.FirstOrDefault(x => x.SaftyHazardId == saftyHazard.Id);
            if (proc_SH_Link == null)
            {
                proc_SH_Link = new Procedure_SaftyHazard_Link(this, saftyHazard);
                AddEntityToNavigationProperty<Procedure_SaftyHazard_Link>(proc_SH_Link);
            }

            return proc_SH_Link;
        }

        public void UnlinkSaftyHazard(SaftyHazard saftyHazard)
        {
            Procedure_SaftyHazard_Link proc_SH_Link = Procedure_SaftyHazard_Links.FirstOrDefault(x => x.SaftyHazardId == saftyHazard.Id);
            if (proc_SH_Link != null)
            {
                RemoveEntityFromNavigationProperty<Procedure_SaftyHazard_Link>(proc_SH_Link);
            }
        }

        public Procedure_ILA_Link LinkIla(ILA ila)
        {
            Procedure_ILA_Link proc_Ila_Link = Procedure_ILA_Links.FirstOrDefault(x => x.ProcedureId == ila.Id && x.ILAId == this.Id);
            if (proc_Ila_Link != null)
            {
                return proc_Ila_Link;
            }

            proc_Ila_Link = new Procedure_ILA_Link(this, ila);
            AddEntityToNavigationProperty<Procedure_ILA_Link>(proc_Ila_Link);
            return proc_Ila_Link;
        }

        public ILA_Procedure_Link LinkProcedureToIla(ILA ila)
        {
            ILA_Procedure_Link ila_Proc_Link = ILA_Procedure_Links.FirstOrDefault(x => x.ProcedureId == ila.Id && x.ILAId == this.Id);
            if (ila_Proc_Link != null)
            {
                return ila_Proc_Link;
            }

            ila_Proc_Link = new ILA_Procedure_Link(ila, this);
            AddEntityToNavigationProperty<ILA_Procedure_Link>(ila_Proc_Link);
            return ila_Proc_Link;
        }

        public void UnlinkIla(ILA ila)
        {
            Procedure_ILA_Link proc_Ila_Link = Procedure_ILA_Links.FirstOrDefault(x => x.ProcedureId == this.Id && x.ILAId == ila.Id);
            if (proc_Ila_Link != null)
            {
                RemoveEntityFromNavigationProperty<Procedure_ILA_Link>(proc_Ila_Link);
            }
        } 
        public void UnlinkIlaFromProcedure(ILA ila)
        {
            ILA_Procedure_Link ila_Proc_Link = ILA_Procedure_Links.FirstOrDefault(x => x.ProcedureId == this.Id && x.ILAId == ila.Id);
            if (ila_Proc_Link != null)
            {
                RemoveEntityFromNavigationProperty<ILA_Procedure_Link>(ila_Proc_Link);
            }
        }

        public void UnlinkIlas()
        {
            List<Procedure_ILA_Link> proc_Ila_Link = Procedure_ILA_Links.Where(x => x.ProcedureId == this.Id).ToList();
            if (proc_Ila_Link.Count != 0)
            {
                RemoveEntitiesFromNavigationProperty<Procedure_ILA_Link>(proc_Ila_Link);
            }
        }

        public Procedure_RR_Link LinkRegulatoryRequirement(RegulatoryRequirement rr)
        {
            Procedure_RR_Link proc_Rr_link = Procedure_RegRequirement_Links.FirstOrDefault(x => x.RegulatoryRequirementId == rr.Id && x.ProcedureId == this.Id);
            if (proc_Rr_link != null)
            {
                return proc_Rr_link;
            }

            proc_Rr_link = new Procedure_RR_Link(this, rr);
            AddEntityToNavigationProperty<Procedure_RR_Link>(proc_Rr_link);
            return proc_Rr_link;
        }

        public void UnlinkRegulatoryRequirement(RegulatoryRequirement rr)
        {
            Procedure_RR_Link proc_Rr_link = Procedure_RegRequirement_Links.FirstOrDefault(x => x.RegulatoryRequirementId == rr.Id && x.ProcedureId == this.Id);
            if (proc_Rr_link != null)
            {
                RemoveEntityFromNavigationProperty<Procedure_RR_Link>(proc_Rr_link);
            }
        }

        public Procedure_Task_Link LinkTask(Task task)
        {
            Procedure_Task_Link proc_task_link = Procedure_Task_Links.FirstOrDefault(x => x.TaskId == task.Id && x.ProcedureId == this.Id);
            if (proc_task_link != null)
            {
                return proc_task_link;
            }

            proc_task_link = new Procedure_Task_Link(this, task);
            AddEntityToNavigationProperty<Procedure_Task_Link>(proc_task_link);
            return proc_task_link;
        }

        public void UnlinkTask(Task task)
        {
            Procedure_Task_Link proc_task_link = Procedure_Task_Links.FirstOrDefault(x => x.TaskId == task.Id && x.ProcedureId == this.Id);
            if (proc_task_link != null)
            {
                RemoveEntityFromNavigationProperty<Procedure_Task_Link>(proc_task_link);
            }
        }

        public Version_Procedure CreateSnapshot()
        {
            return new Version_Procedure(this);
        }
    }
}
