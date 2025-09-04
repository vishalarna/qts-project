using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class DeliveryMethod : Entity
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }
        public int CreatorIlaId { get; set; }

        public bool? IsUserDefined { get; set; }
        public bool? IsAvailableForAllIlas { get; set; }
        public bool IsNerc { get; set; }

        public virtual ICollection<ILA> ILAs { get; set; } = new List<ILA>();
        public DeliveryMethod(string name, string displayName, bool isNerc = false)
        {
            Name = name;
            DisplayName = displayName;
            IsNerc = isNerc;
        }

        public DeliveryMethod()
        {
        }
    }
}
