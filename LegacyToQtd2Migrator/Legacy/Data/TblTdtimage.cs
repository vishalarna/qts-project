using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTdtimage
    {
        public TblTdtimage()
        {
            TblTestItems = new HashSet<TblTestItem>();
        }

        public int ImageId { get; set; }
        public byte[] Image { get; set; }
        public byte[] Ts { get; set; }

        public virtual ICollection<TblTestItem> TblTestItems { get; set; }
    }
}
