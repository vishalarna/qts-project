using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.NERCTargetAudience
{
    public class NERCTargetAudienceUpdateOptions
    {
        public string Name { get; set; }

        public bool IsOther { get; set; }

        public string OtherName { get; set; }
    }
}
