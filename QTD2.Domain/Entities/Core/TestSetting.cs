using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class TestSetting : Entity
    {
        public string Description { get; set; }

        public bool IsDefault { get; set; }

        public bool IsOverride { get; set; }

        public TestSetting()
        {
        }

        public TestSetting(string description, bool isDefault, bool isOverride)
        {
            Description = description;
            IsDefault = isDefault;
            IsOverride = isOverride;
        }
    }
}
