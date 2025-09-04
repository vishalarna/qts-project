using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSmudsegmentsLinkObjective
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int? SegmentId { get; set; }
        public int? ObjectiveId { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
    }
}
