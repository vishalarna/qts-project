using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Provider
{
    public class ILA_ProviderVM
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public bool IsPriority { get; set; }

        public string Name { get; set; }
        public string providerNumber { get; set; }

        public int ILACount { get; set; }

        public bool isNerc { get; set; }
    }
}
