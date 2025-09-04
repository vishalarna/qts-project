using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ClientSettings_Feature:Entity
    {
        public string Feature { get; set; }
        public bool Enabled { get; set; }
    }
}
