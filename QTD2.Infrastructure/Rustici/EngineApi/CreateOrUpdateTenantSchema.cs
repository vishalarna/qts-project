using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class CreateOrUpdateTenantSchema
    {
        public TenantPropertiesSchema TenantProperties { get; set; }
        public IEnumerable<string> EngineTenantName { get; set; }
    }

    public class TenantPropertiesSchema
    {
        public bool Active { get; set; } 
      
    }


}
