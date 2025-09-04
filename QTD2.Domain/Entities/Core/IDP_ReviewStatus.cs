using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class IDP_ReviewStatus : Common.Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<IDP_Review> IDP_Reviews { get; set; } = new List<IDP_Review>();

        public IDP_ReviewStatus()
        {

        }

        public IDP_ReviewStatus(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
