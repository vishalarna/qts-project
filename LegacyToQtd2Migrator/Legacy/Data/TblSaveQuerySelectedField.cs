using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSaveQuerySelectedField
    {
        public Guid? FkQueryId { get; set; }
        public string FieldVal { get; set; }
        public string FieldText { get; set; }
    }
}
