using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblLabelReplacementText
    {
        public int ReplacementLabelId { get; set; }
        public string DefaultText { get; set; }
        public string ReplacementText { get; set; }
    }
}
