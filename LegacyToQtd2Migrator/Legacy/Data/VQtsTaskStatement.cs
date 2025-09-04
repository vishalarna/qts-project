using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsTaskStatement
    {
        public int Thid { get; set; }
        public DateTime? Thdate { get; set; }
        public int? Tid { get; set; }
        public string Thstatement { get; set; }
        public bool? RequalRequired { get; set; }
    }
}
