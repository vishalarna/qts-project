using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class SysTblMenu
    {
        public string FormName { get; set; }
        public string FormDescription { get; set; }
        public int SortOrder { get; set; }
        public bool? Display { get; set; }
        public int Section { get; set; }
        public bool? ShowToEmp { get; set; }
        public int? AccessLevel { get; set; }
    }
}
