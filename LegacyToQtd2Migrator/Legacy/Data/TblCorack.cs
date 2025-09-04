using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblCorack
    {
        public TblCorack()
        {
            TblCoshelves = new HashSet<TblCoshelf>();
        }

        public int RackId { get; set; }
        public string RackDesc { get; set; }
        public int? RackNumber { get; set; }
        public int RoomId { get; set; }

        public virtual TblCoroom Room { get; set; }
        public virtual ICollection<TblCoshelf> TblCoshelves { get; set; }
    }
}
