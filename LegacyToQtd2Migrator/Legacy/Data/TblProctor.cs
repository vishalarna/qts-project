using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblProctor
    {
        public int ProctorId { get; set; }
        public string ProctorName { get; set; }
        public bool? Active { get; set; }
        public byte[] Ts { get; set; }
    }
}
