using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblInstructorsAdministrator
    {
        public int Id { get; set; }
        public int InstuctorId { get; set; }
        public bool? IsAdministrator { get; set; }

        public virtual LktblInstructor Instuctor { get; set; }
    }
}
