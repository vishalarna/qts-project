using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class ViewQtsTask
    {
        public int Tid { get; set; }
        public string Tdesc { get; set; }
        public string TaskNmbr { get; set; }
        public bool Critical { get; set; }
        public string Tconditions { get; set; }
        public string Tstandards { get; set; }
        public string Ttools { get; set; }
        public string Treferences { get; set; }
        public bool? Inactive { get; set; }
        public int? Daid { get; set; }
    }
}
