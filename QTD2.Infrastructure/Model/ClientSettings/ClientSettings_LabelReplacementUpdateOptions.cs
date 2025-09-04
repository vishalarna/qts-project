using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClientSettings
{
    public class ClientSettings_LabelReplacementUpdateOptions
    {
        public List<LabelReplacementOptions> LabelReplacements { get; set; }
    }

    public class LabelReplacementOptions
    {
        public string DefaultLabel { get; set; }
        public string LabelReplacement { get; set; }
    }
}
