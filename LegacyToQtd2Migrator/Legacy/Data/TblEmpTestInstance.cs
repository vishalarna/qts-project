using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblEmpTestInstance
    {
        public int Id { get; set; }
        public int? Eid { get; set; }
        public int? TestId { get; set; }
        public bool? Status { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public int? Etid { get; set; }
    }
}
