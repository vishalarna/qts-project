using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class MetaILAConfigurationPublishOption : Entity
    {
        public string Description { get; set; }

        public virtual ICollection<Meta_ILAMembers_Link> Meta_ILAMembers_Links { get; set; } = new List<Meta_ILAMembers_Link>();

        public MetaILAConfigurationPublishOption()
        {
        }

        public MetaILAConfigurationPublishOption(string description)
        {
            Description = description;
        }
    }
}
