using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class RegulatoryRequirement : Entity
    {
        public int IssuingAuthorityId { get; set; }

        public string Number { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string RevisionNumber { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public byte[] Uploads { get; set; }

        public string HyperLink { get; set; }

        public string FileName { get; set; }

        public virtual RR_IssuingAuthority RR_IssuingAuthority { get; set; }

        //public virtual ICollection<RR_SafetyHazard_Link> RR_SafetyHazard_Links { get; set; } = new List<RR_SafetyHazard_Link>();

        public virtual ICollection<RR_Task_Link> RR_Task_Links { get; set; } = new List<RR_Task_Link>();

        public virtual ICollection<ILA_RegRequirement_Link> ILA_RegRequirement_Links { get; set; } = new List<ILA_RegRequirement_Link>();


        public virtual ICollection<RegRequirement_EO_Link> RegRequirement_EO_Links { get; set; } = new List<RegRequirement_EO_Link>();

        //public virtual ICollection<RR_Procedure_Link> RR_Procedure_Links { get; set; } = new List<RR_Procedure_Link>();

        public virtual ICollection<Procedure_RR_Link> Procedure_RegRequirement_Links { get; set; } = new List<Procedure_RR_Link>();

        public virtual ICollection<SaftyHazard_RR_Link> SaftyHazard_RR_Links { get; set; } = new List<SaftyHazard_RR_Link>();

        public virtual ICollection<RR_StatusHistory> RR_StatusHistories { get; set; } = new List<RR_StatusHistory>();

        //public virtual ICollection<Task_RR_Link> Task_RR_Links { get; set; } = new List<Task_RR_Link>();

        public virtual ICollection<Version_RegulatoryRequirement> Version_RegulatoryRequirements { get; set; } = new List<Version_RegulatoryRequirement>();

        public RegulatoryRequirement(int issuingAuthorityId, string number, string title, string description, string revisionNumber, DateTime? effectiveDate, byte[] uploads, string hyperLink, string fileName)
        {
            IssuingAuthorityId = issuingAuthorityId;
            Number = number;
            Title = title;
            Description = description;
            RevisionNumber = revisionNumber;
            EffectiveDate = effectiveDate;
            Uploads = uploads;
            HyperLink = hyperLink;
            FileName = fileName;
        }

        public RegulatoryRequirement()
        {
        }

        public SaftyHazard_RR_Link LinkSafetyHazard(SaftyHazard saftyHazard)
        {
            SaftyHazard_RR_Link saftyHazard_rr_link = SaftyHazard_RR_Links.FirstOrDefault(x => x.RegulatoryRequirementId == this.Id && x.SafetyHazardId == saftyHazard.Id);
            if (saftyHazard_rr_link != null)
            {
                return saftyHazard_rr_link;
            }

            saftyHazard_rr_link = new SaftyHazard_RR_Link(this, saftyHazard);
            AddEntityToNavigationProperty<SaftyHazard_RR_Link>(saftyHazard_rr_link);
            return saftyHazard_rr_link;
            //SaftyHazard_RR_Link rr_SH_Link = RR_SafetyHazard_Links.FirstOrDefault(x => x.SafetyHazardId == saftyHazard.Id && x.RegulatoryRequirementId == this.Id);
            //if (rr_SH_Link != null)
            //{
            //    return rr_SH_Link;
            //}

            //rr_SH_Link = new RR_SafetyHazard_Link(this, saftyHazard);
            //AddEntityToNavigationProperty<RR_SafetyHazard_Link>(rr_SH_Link);
            //return rr_SH_Link;
        }

        public void UnlinkSafetyHazard(SaftyHazard saftyHazard)
        {
            SaftyHazard_RR_Link rr_SH_Link = SaftyHazard_RR_Links.FirstOrDefault(x => x.SafetyHazardId == saftyHazard.Id && x.RegulatoryRequirementId == this.Id);
            RemoveEntityFromNavigationProperty<SaftyHazard_RR_Link>(rr_SH_Link);
        }

        public ILA_RegRequirement_Link LinkILA(ILA ila)
        {
            ILA_RegRequirement_Link rr_ila_link = ILA_RegRequirement_Links.FirstOrDefault(x => x.ILAId == ila.Id && x.RegulatoryRequirementId == this.Id);
            if (rr_ila_link != null)
            {
                return rr_ila_link;
            }

            rr_ila_link = new ILA_RegRequirement_Link(ila,this);
            AddEntityToNavigationProperty<ILA_RegRequirement_Link>(rr_ila_link);
            return rr_ila_link;
        }

        public void UnlinkIla(ILA ila)
        {
            ILA_RegRequirement_Link rr_ila_link = ILA_RegRequirement_Links.FirstOrDefault(x => x.ILAId == ila.Id && x.RegulatoryRequirementId == this.Id);
            RemoveEntityFromNavigationProperty<ILA_RegRequirement_Link>(rr_ila_link);
        }

        public RegRequirement_EO_Link LinkEO(EnablingObjective eo)
        {
            RegRequirement_EO_Link rr_eo_link = RegRequirement_EO_Links.FirstOrDefault(x => x.EOID == eo.Id && x.RegulatoryRequirementId == this.Id);
            if (rr_eo_link != null)
            {
                return rr_eo_link;
            }

            rr_eo_link = new RegRequirement_EO_Link(this, eo);
            AddEntityToNavigationProperty<RegRequirement_EO_Link>(rr_eo_link);
            return rr_eo_link;
        }

        public void UnlinkEO(EnablingObjective eo)
        {
            RegRequirement_EO_Link rr_eo_link = RegRequirement_EO_Links.FirstOrDefault(x => x.EOID == eo.Id && x.RegulatoryRequirementId == this.Id);
            if (rr_eo_link != null)
            {
                RemoveEntityFromNavigationProperty<RegRequirement_EO_Link>(rr_eo_link);
            }
        }

        public Procedure_RR_Link LinkProcedure(Procedure procedure)
        {
            Procedure_RR_Link proc_Rr_link = Procedure_RegRequirement_Links.FirstOrDefault(x => x.RegulatoryRequirementId == this.Id && x.ProcedureId == procedure.Id);
            if (proc_Rr_link != null)
            {
                return proc_Rr_link;
            }

            proc_Rr_link = new Procedure_RR_Link(procedure, this);
            AddEntityToNavigationProperty<Procedure_RR_Link>(proc_Rr_link);
            return proc_Rr_link;

            //RR_Procedure_Link rr_proc_link = RR_Procedure_Links.FirstOrDefault(x => x.RegulatoryRequirementId == this.Id && x.ProcedureId == procedure.Id);
            //if (rr_proc_link != null)
            //{
            //    return rr_proc_link;
            //}

            //rr_proc_link = new RR_Procedure_Link(this, procedure);
            //AddEntityToNavigationProperty<RR_Procedure_Link>(rr_proc_link);
            //return rr_proc_link;
        }

        public void UnlinkProcedure(Procedure procedure)
        {
            Procedure_RR_Link rr_proc_link = Procedure_RegRequirement_Links.FirstOrDefault(x => x.RegulatoryRequirementId == this.Id && x.ProcedureId == procedure.Id);
            if (rr_proc_link != null)
            {
                RemoveEntityFromNavigationProperty<Procedure_RR_Link>(rr_proc_link);
            }
        }

        public RR_Task_Link LinkTask(Task task)
        {
            RR_Task_Link rr_task_link = RR_Task_Links.Where(x => x.TaskId == task.Id && x.RegRequirementId == this.Id).FirstOrDefault();
            if (rr_task_link != null)
            {
                return rr_task_link;
            }

            rr_task_link = new RR_Task_Link(this, task);
            AddEntityToNavigationProperty<RR_Task_Link>(rr_task_link);
            return rr_task_link;
        }

        public void UnlinkTask(Task task)
        {
            RR_Task_Link rr_task_link = RR_Task_Links.Where(x => x.TaskId == task.Id && x.RegRequirementId == this.Id).FirstOrDefault();
            if (rr_task_link != null)
            {
                RemoveEntityFromNavigationProperty<RR_Task_Link>(rr_task_link);
            }
        }

        public Version_RegulatoryRequirement CreateSnapshot()
        {
            return new Version_RegulatoryRequirement(this);
        }
    }
}
