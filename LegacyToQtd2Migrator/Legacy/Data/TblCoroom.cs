using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblCoroom
    {
        public TblCoroom()
        {
            TblCoracks = new HashSet<TblCorack>();
        }

        public int RoomId { get; set; }
        public int RoomNumber { get; set; }
        public string RoomDesc { get; set; }
        public string RoomLtr { get; set; }

        public virtual ICollection<TblCorack> TblCoracks { get; set; }
    }
}
