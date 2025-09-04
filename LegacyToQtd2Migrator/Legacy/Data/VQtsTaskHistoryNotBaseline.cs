using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsTaskHistoryNotBaseline
    {
        public int Thid { get; set; }
        public DateTime? Thdate { get; set; }
        public int? Tid { get; set; }
        public string Thnum { get; set; }
        public string Thstatement { get; set; }
        public string Thconditions { get; set; }
        public string Thtools { get; set; }
        public string Threferences { get; set; }
        public string Thcriteria { get; set; }
        public string ThprocList { get; set; }
        public string ThrevisedBy { get; set; }
        public string Thnote { get; set; }
        public bool ThposHistory { get; set; }
        public byte[] Ts { get; set; }
        public int? Baseline { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string ReviewComment { get; set; }
        public bool? RequalRequired { get; set; }
    }
}
