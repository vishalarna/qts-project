using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSmudsegmentsNercStandard
    {
        public int Id { get; set; }
        public int? SegmentId { get; set; }
        public bool? Standards { get; set; }
        public bool? OperatingTopic { get; set; }
        public bool? Simulation { get; set; }
        public bool? ProfessionalCredit { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
    }
}
