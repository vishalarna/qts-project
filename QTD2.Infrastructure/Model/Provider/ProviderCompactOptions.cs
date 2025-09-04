using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Infrastructure.Model.ILA;

namespace QTD2.Infrastructure.Model.Provider
{
    public class ProviderCompactOptions
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public string Number { get; set; }

        public List<ILACompactOption> ILAs { get; set; } = new List<ILACompactOption>();
    }
}
