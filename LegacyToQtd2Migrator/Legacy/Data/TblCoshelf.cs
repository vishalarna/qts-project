using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblCoshelf
    {
        public TblCoshelf()
        {
            TblContentObjects = new HashSet<TblContentObject>();
        }

        public int ShelfId { get; set; }
        public int ShelfNumber { get; set; }
        public string ShelfDesc { get; set; }
        public int RackId { get; set; }

        public virtual TblCorack Rack { get; set; }
        public virtual ICollection<TblContentObject> TblContentObjects { get; set; }
    }
}
