using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ClientSettings_LabelReplacement : Entity
    {
        public string DefaultLabel { get; set; }
        public string LabelReplacement { get; set; }

        public void SetLabelReplacementText(string labelReplacement)
        {
            this.LabelReplacement = labelReplacement;
        }
    }
}
