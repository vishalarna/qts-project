using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.DeliveryMethod
{
    public class DeliveryMethodUpdateOptions
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public bool IsNerc { get; set; }
    }
}
