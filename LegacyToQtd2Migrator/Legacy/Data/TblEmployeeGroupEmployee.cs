using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblEmployeeGroupEmployee
    {
        public int Egid { get; set; }
        public int Empid { get; set; }

        public virtual TblEmployeeGroup Eg { get; set; }
        public virtual TblEmployee Emp { get; set; }
    }
}
