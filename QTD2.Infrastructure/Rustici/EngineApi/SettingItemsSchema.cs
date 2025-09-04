using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class SettingItemsSchema
    {
        public int Id { get; set; }
        public string EffectiveValue { get; set; }
        public string EffectiveValueSource { get; set; }
        public string ExplicitValue { get; set; }
        public MetaDataSchema MetaData { get; set; }
    }
}
