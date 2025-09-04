using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA
{
    public class ILAProviderVM
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string ProviderName { get; set; }
        public List<int> TopicIds = new List<int>();
        public double? TotalHours { get; set; }
        public int ClassScheduleEmpCount { get; set; }
        public string DeliveryMethodName { get; set; }
        public bool IsPublished { get; set; }
        public bool Active { get; set; }
        public string Image { get; set; }
        public bool IsSelfPaced { get; set; }
        public int? ClassSize { get; set; }
        public bool UseForEMP { get; set; }
        public bool IsPubliclyAvailable { get; set; }
        public bool? IsNerc { get; set; }

        public ILAProviderVM() { }
    }
}
