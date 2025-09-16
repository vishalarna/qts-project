using System;
using System.Collections.Generic;

namespace QTD2.Infrastructure.Model.ILA
{
    public class ILABasicCreateOptions
    {
        public int ProviderId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public double? TotalHours { get; set; }
        public int? DeliveryMethodId { get; set; }
        public bool? IsSelfPacedILA { get; set; }
        public bool? IsPublished { get; set; }
        public List<CEHUpdateOptions> CEHUpdates { get; set; } = new List<CEHUpdateOptions>();
    }
}
