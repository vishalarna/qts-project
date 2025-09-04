using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblEmployeeGroup
    {
        public TblEmployeeGroup()
        {
            TblEmployeeGroupEmployees = new HashSet<TblEmployeeGroupEmployee>();
        }

        public int Egid { get; set; }
        public string Egdesc { get; set; }

        public virtual ICollection<TblEmployeeGroupEmployee> TblEmployeeGroupEmployees { get; set; }
    }
}
