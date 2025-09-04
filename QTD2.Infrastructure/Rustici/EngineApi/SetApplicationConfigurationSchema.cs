using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class SetApplicationConfigurationSchema
    {
        public ConfigurationSettingSchema configurationSettings { get; set; }
        public string engineTenantName { get; set; }
        public IEnumerable<string> EngineTenantName { get; set; }
        public string learningStandard { get; set; }
        public bool singleSco { get; set; }
    }
}
