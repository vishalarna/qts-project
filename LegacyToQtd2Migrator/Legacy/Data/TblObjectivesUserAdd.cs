using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblObjectivesUserAdd
    {
        public int ObjId { get; set; }
        public int? ObjCorid { get; set; }
        public string ObjType { get; set; }
        public string ObjText { get; set; }
        public bool? ObjIsAdded { get; set; }
        public DateTime? ObjDateAdded { get; set; }
        public int? Sequence { get; set; }
    }
}
