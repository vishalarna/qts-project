using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsClassesAll
    {
        public int Clid { get; set; }
        public DateTime? Cldate { get; set; }
        public string Lcdesc { get; set; }
        public string Inname { get; set; }
        public int? Lcid { get; set; }
        public int? Inid { get; set; }
        public int? Corid { get; set; }
        public DateTime? StartDate { get; set; }
        public bool SelfPased { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
