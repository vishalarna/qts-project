using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblProceduresTasksHistory
    {
        public int Prthid { get; set; }
        public int? Prhid { get; set; }
        public int? Tid { get; set; }
        public string Tdesc { get; set; }
        public byte[] Ts { get; set; }
    }
}
