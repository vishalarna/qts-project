using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Authentication
{
    public class AuthenticationSetting : Entity
    {
        public int VersionMajor { get; set; }
        public int VersionMinor { get; set; }
        public int VersionPatch { get; set; }
        public AuthenticationSetting() { }
    }
}
