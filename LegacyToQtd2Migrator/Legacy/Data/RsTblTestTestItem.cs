using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class RsTblTestTestItem
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public int TestItemId { get; set; }
        public int TestOrder { get; set; }
        public byte[] Ts { get; set; }

        public virtual TblTest Test { get; set; }
        public virtual TblTestItem TestItem { get; set; }
    }
}
