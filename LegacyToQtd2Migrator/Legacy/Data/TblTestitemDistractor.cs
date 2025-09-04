using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTestitemDistractor
    {
        public int TestitemDistractorId { get; set; }
        public int? TestItemId { get; set; }
        public string DistractorDetails { get; set; }
        public byte[] Ts { get; set; }
        public int ImageSizeId { get; set; }

        public virtual TblTestItem TestItem { get; set; }
    }
}
