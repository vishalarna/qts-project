using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SafetyHazard_Set : Entity
    {
        public string SafetyHzAbatementText { get; set; }

        public string SafetyHzAbatementFiles { get; set; }

        public string SafetyHzAbatementImage { get; set; }

        public int? SafetyHzAbatementNumber { get; set; }

        public string SafetyHzControlsText { get; set; }

        public string SafetyHzControlsFiles { get; set; }

        public string SafetyHzControlsImage { get; set; }

        public int? SafetyHzControlsNumber { get; set; }

        public virtual ICollection<SafetyHazard_Set_Link> SafetyHazard_Set_Links { get; set; } = new List<SafetyHazard_Set_Link>();

        public SafetyHazard_Set()
        {
        }

        public SafetyHazard_Set(string safetyHzAbatementText, string safetyHzAbatementFiles, string safetyHzAbatementImage, string safetyHzControlsText, string safetyHzControlsFiles, string safetyHzControlsImage)
        {
            SafetyHzAbatementText = safetyHzAbatementText;
            SafetyHzAbatementFiles = safetyHzAbatementFiles;
            SafetyHzAbatementImage = safetyHzAbatementImage;
            SafetyHzControlsText = safetyHzControlsText;
            SafetyHzControlsFiles = safetyHzControlsFiles;
            SafetyHzControlsImage = safetyHzControlsImage;
        }
    }
}
