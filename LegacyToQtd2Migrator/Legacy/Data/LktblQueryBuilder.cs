using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class LktblQueryBuilder
    {
        public string DisplayName { get; set; }
        public string FieldName { get; set; }
        public string TableName { get; set; }
        public string FieldType { get; set; }
        public int? SortOrder { get; set; }
        public bool? Visible { get; set; }
        public string Category { get; set; }
        public int? FormTab { get; set; }
    }
}
