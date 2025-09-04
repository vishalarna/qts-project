using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSafetyHazardCategory
    {
        public TblSafetyHazardCategory()
        {
            TblSafetyHazards = new HashSet<TblSafetyHazard>();
        }

        public int Hzcid { get; set; }
        public string Hzcategory { get; set; }
        public bool? Hzpriority { get; set; }

        public virtual ICollection<TblSafetyHazard> TblSafetyHazards { get; set; }
    }
}
