using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Version_SaftyHazard
{
    public class Version_SaftyHazardUpdateOptions
    {
        public int SaftyHazardId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string PersonalProtectiveEquipment { get; set; }

        public int MinorVersion { get; set; }

        public int MajorVersion { get; set; }

        public string VersionNumber { get; set; }
    }
}
