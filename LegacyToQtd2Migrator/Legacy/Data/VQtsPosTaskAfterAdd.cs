using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsPosTaskAfterAdd
    {
        public int Pthid { get; set; }
        public DateTime? Pthdate { get; set; }
        public int? Pid { get; set; }
        public int? Tid { get; set; }
        public string Pthtype { get; set; }
        public int? Thid { get; set; }
        public bool Pthcritical { get; set; }
        public string PthrevisedBy { get; set; }
        public string Pthnote { get; set; }
        public byte[] Ts { get; set; }
        public int? Baseline { get; set; }
        public DateTime? ChangeDateStamp { get; set; }
    }
}
