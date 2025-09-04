using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class MetaDataSchema
    {
        public string Default { get; set; }
        public string DataType { get; set; }
        public string SettingDescription { get; set; }
        public string Level { get; set; }
        public string[] LearningStandards { get; set; }
        public string FallBack { get; set; }
        public ValidValuesSchema ValidValues {get;set;}
    }
}
