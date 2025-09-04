using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Authentication
{
    public class SamlProvider : IdentityProvider
    {
        public string EntityIDUrl { get; set; }
        public string MetaDataUrl { get; set; }
        public string PathToPfx { get; set; }
        public SamlProvider() { }
    }
}
