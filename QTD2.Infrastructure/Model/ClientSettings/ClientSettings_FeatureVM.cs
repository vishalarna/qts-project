using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClientSettings
{
    public class ClientSettings_FeatureVM
    {
        public int Id { get; set; }
        public string Feature { get; set; }
        public bool Enabled { get; set; }
    }    
}
