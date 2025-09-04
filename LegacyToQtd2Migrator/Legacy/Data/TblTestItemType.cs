using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTestItemType
    {
        public TblTestItemType()
        {
            TblTestItems = new HashSet<TblTestItem>();
        }

        public int TestItemTypeId { get; set; }
        public string TestItemType { get; set; }
        public byte[] Ts { get; set; }

        public virtual ICollection<TblTestItem> TblTestItems { get; set; }
    }
}
