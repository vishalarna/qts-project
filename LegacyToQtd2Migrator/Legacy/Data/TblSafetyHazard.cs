using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSafetyHazard
    {
        public TblSafetyHazard()
        {
            TblSafetyHazardAbatements = new HashSet<TblSafetyHazardAbatement>();
            TblSafetyHazardControls = new HashSet<TblSafetyHazardControl>();
            TblSafetyHazardEos = new HashSet<TblSafetyHazardEo>();
            TblSafetyHazardIlas = new HashSet<TblSafetyHazardIla>();
            TblSafetyHazardTasks = new HashSet<TblSafetyHazardTask>();
            TblSmudcourseDesignSafetyHazards = new HashSet<TblSmudcourseDesignSafetyHazard>();
        }

        public int Shzid { get; set; }
        public int Hzcid { get; set; }
        public string Shznum { get; set; }
        public string Shztitle { get; set; }
        public string Shzdesc { get; set; }
        public string Ppe { get; set; }
        public bool? Inactive { get; set; }

        public virtual TblSafetyHazardCategory Hzc { get; set; }
        public virtual ICollection<TblSafetyHazardAbatement> TblSafetyHazardAbatements { get; set; }
        public virtual ICollection<TblSafetyHazardControl> TblSafetyHazardControls { get; set; }
        public virtual ICollection<TblSafetyHazardEo> TblSafetyHazardEos { get; set; }
        public virtual ICollection<TblSafetyHazardIla> TblSafetyHazardIlas { get; set; }
        public virtual ICollection<TblSafetyHazardTask> TblSafetyHazardTasks { get; set; }
        public virtual ICollection<TblSmudcourseDesignSafetyHazard> TblSmudcourseDesignSafetyHazards { get; set; }
    }
}
