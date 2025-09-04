using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClientUserSettings
{
    public class CustomizeDashboardUpdateOptions
    {
        public List<CustomDashboardSettingOption> Updates { get; set; }
    }

    public class CustomDashboardSettingOption
    {
        public string Settings { get; set; }
        public bool Enable { get; set; }
        public bool Disable { get; set; }

    }
}
