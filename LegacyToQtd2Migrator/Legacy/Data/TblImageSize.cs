using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblImageSize
    {
        public int ImageSizeId { get; set; }
        public string ImageType { get; set; }
        public byte[] Ts { get; set; }
    }
}
