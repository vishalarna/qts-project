using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblGroupSchedule
    {
        public TblGroupSchedule()
        {
            TblGroupScheduleEmployees = new HashSet<TblGroupScheduleEmployee>();
            TblGroupScheduleIlas = new HashSet<TblGroupScheduleIla>();
        }

        public int Gsid { get; set; }
        public int Ilagid { get; set; }
        public int Egid { get; set; }
        public string GroupTitle { get; set; }
        public DateTime Dra { get; set; }
        public string AddedBy { get; set; }
        public DateTime? Dru { get; set; }
        public string UpdatedBy { get; set; }
        public bool Committed { get; set; }

        public virtual ICollection<TblGroupScheduleEmployee> TblGroupScheduleEmployees { get; set; }
        public virtual ICollection<TblGroupScheduleIla> TblGroupScheduleIlas { get; set; }
    }
}
