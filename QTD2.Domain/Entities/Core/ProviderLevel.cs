using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ProviderLevel : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<Provider> Providers { get; set; } = new List<Provider>();

        public ProviderLevel(string name)
        {
            this.Name = name;
        }

        public ProviderLevel()
        {
        }
    }
}
