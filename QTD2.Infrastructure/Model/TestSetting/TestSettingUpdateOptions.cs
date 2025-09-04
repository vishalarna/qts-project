using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TestSetting
{
    public class TestSettingUpdateOptions
    {
        public string Description { get; set; }

        public bool IsDefault { get; set; }

        public bool IsOverride { get; set; }
    }
}
