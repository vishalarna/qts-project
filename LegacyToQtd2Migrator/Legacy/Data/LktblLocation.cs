using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class LktblLocation
    {
        public LktblLocation()
        {
            TblClasses = new HashSet<TblClass>();
            TblSmudcourseImplementClassSchedules = new HashSet<TblSmudcourseImplementClassSchedule>();
        }

        public int Lcid { get; set; }
        public string Lcdesc { get; set; }
        public string Lccity { get; set; }
        public string Lcstate { get; set; }
        public string Lczip { get; set; }
        public string Lcphone { get; set; }
        public string Lcfax { get; set; }
        public string Lcemail { get; set; }
        public string LcwebSite { get; set; }
        public string Lcnote1 { get; set; }
        public string Lcnote2 { get; set; }
        public bool Inactive { get; set; }
        public byte[] Ts { get; set; }

        public virtual ICollection<TblClass> TblClasses { get; set; }
        public virtual ICollection<TblSmudcourseImplementClassSchedule> TblSmudcourseImplementClassSchedules { get; set; }
    }
}
