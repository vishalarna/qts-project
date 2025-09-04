using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VwContentObject
    {
        public string RoomLtr { get; set; }
        public int RoomNumber { get; set; }
        public int? RackNumber { get; set; }
        public int ShelfNumber { get; set; }
        public int Conum { get; set; }
        public string Coname { get; set; }
        public string Codesc { get; set; }
        public string FileName { get; set; }
        public string SourceFile { get; set; }
        public DateTime CodateStamp { get; set; }
        public int Coid { get; set; }
        public int ShelfId { get; set; }
        public string Conumber { get; set; }
    }
}
