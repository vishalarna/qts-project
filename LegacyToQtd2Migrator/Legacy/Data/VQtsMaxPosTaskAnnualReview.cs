using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsMaxPosTaskAnnualReview
    {
        public int Tharid { get; set; }
        public string ArrevBy { get; set; }
        public DateTime? ArrevDate { get; set; }
        public byte[] Arsig { get; set; }
        public int? ArposId { get; set; }
        public string Arnotes { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
