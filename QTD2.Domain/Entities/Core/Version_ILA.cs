using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Version_ILA : Entity
    {
        public int ILAId { get; set; }
        public string Name { get; set; }

        public int VersionNumber { get; set; }
        
        public string NickName { get; set; }

        public string Number { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string TrainingPlan { get; set; }

        public int ProviderId { get; set; }

        public bool IsSelfPaced { get; set; }

        public bool IsOptional { get; set; }

        public bool IsPublished { get; set; }

        public DateTime? PublishDate { get; set; }

        public int? DeliveryMethodId { get; set; }

        public bool? HasPilotData { get; set; }

        public bool IsProgramManual { get; set; }

        public DateOnly? SubmissionDate { get; set; }

        public DateOnly? ApprovalDate { get; set; }

        public DateOnly? ExpirationDate { get; set; }

        public string? OtherAssesmentTool { get; set; }

        public string? OtherNercTargetAudience { get; set; }

        public DateOnly? StartDate { get; set; }

        public string ChangeReason { get; set; }

        public DateTime? EffectiveDate { get; set; }

        // 0 for deleted, 1 for added, 2 for updated
        public int State { get; set; }

        public virtual ILA ILA { get; set; }

        public virtual ICollection<Version_Task_ILA_Link> Version_Task_ILA_Links { get; set; } = new List<Version_Task_ILA_Link>();

        public ICollection<Version_EnablingObjective_ILALink> Version_EnablingObjective_ILALinks { get; set; } = new List<Version_EnablingObjective_ILALink>();
        public virtual ICollection<Version_TrainingProgram_ILA_Link> Version_TrainingProgram_ILA_Links { get; set; } = new List<Version_TrainingProgram_ILA_Link>();

        public Version_ILA()
        {
        }

        public Version_ILA(ILA ila)
        {
            ILAId = ila.Id;
            Name = ila.Name;
            NickName = ila.NickName;
            Number = ila.Number;
            Description = ila.Description;
            Image = ila.Image;
            TrainingPlan = ila.TrainingPlan;
            ProviderId = ila.ProviderId;
            IsSelfPaced = ila.IsSelfPaced;
            IsOptional = ila.IsOptional;
            IsPublished = ila.IsPublished;
            PublishDate = ila.PublishDate;
            DeliveryMethodId = ila.DeliveryMethodId;
            HasPilotData = ila.HasPilotData ?? false;
            IsProgramManual = ila.IsProgramManual;
            SubmissionDate = ila.SubmissionDate;
            ApprovalDate = ila.ApprovalDate;
            ExpirationDate = ila.ExpirationDate;
            OtherAssesmentTool = ila.OtherAssesmentTool;
            OtherNercTargetAudience = ila.OtherNercTargetAudience;
            StartDate = ila.StartDate;
        }
    }
}
