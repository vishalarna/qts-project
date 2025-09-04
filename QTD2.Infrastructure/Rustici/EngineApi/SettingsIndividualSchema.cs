using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class SettingsIndividualSchema
    {
        public string settingId { get; set; }
        public string Value { get; set; }
        public bool Explicit { get; set; } = false;
    }
}
