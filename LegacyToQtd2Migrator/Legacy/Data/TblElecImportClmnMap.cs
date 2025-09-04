using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblElecImportClmnMap
    {
        public int Impid { get; set; }
        public int ImportFileTypeId { get; set; }
        public string FieldName { get; set; }
        public string DefValStr { get; set; }
        public string Req { get; set; }
        public int? SortOrder { get; set; }
    }
}
