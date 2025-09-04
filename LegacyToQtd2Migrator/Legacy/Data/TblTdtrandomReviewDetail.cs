using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTdtrandomReviewDetail
    {
        public int RandomReviewDetailId { get; set; }
        public int TestId { get; set; }
        public int TestItemId { get; set; }
        public byte[] Ts { get; set; }

        public virtual TblTest Test { get; set; }
        public virtual TblTestItem TestItem { get; set; }
    }
}
