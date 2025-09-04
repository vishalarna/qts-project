using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA
{
    public class ILACreateOptions
    {
        public string Name { get; set; }
        public bool? isAvailableForAllILA { get; set; }
        public string? ilaDeliveryMethod { get; set; }

        public string NickName { get; set; }

        public string Number { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string TrainingPlan { get; set; }

        public int ProviderId { get; set; }

        public bool? IsSelfPaced { get; set; }

        public bool IsOptional { get; set; }

        public bool IsPublished { get; set; }

        public DateTime? PublishDate { get; set; }

        public int? DeliveryMethodId { get; set; }

        public bool HasPilotData { get; set; }

        public bool PilotDataNA { get; set; }
        public bool DoesActivityConform { get; set; }

        public bool IsProgramManual { get; set; }

        public DateOnly? SubmissionDate { get; set; }

        public DateOnly? ApprovalDate { get; set; }

        public DateOnly? ExpirationDate { get; set; }

        public byte[]? Uploads { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
