using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblForm
    {
        public TblForm()
        {
            TblFormQuestions = new HashSet<TblFormQuestion>();
            TblFormSections = new HashSet<TblFormSection>();
            TblSmudcourseImplementClassSchedules = new HashSet<TblSmudcourseImplementClassSchedule>();
            TblStudentForms = new HashSet<TblStudentForm>();
        }

        public int Fid { get; set; }
        public string Fname { get; set; }
        public float? Fversion { get; set; }
        public string Fdescription { get; set; }
        public int? Rsid { get; set; }
        public string Finstructions { get; set; }
        public bool Fexpired { get; set; }
        public byte[] Ts { get; set; }

        public virtual LktblRatingScale Rs { get; set; }
        public virtual ICollection<TblFormQuestion> TblFormQuestions { get; set; }
        public virtual ICollection<TblFormSection> TblFormSections { get; set; }
        public virtual ICollection<TblSmudcourseImplementClassSchedule> TblSmudcourseImplementClassSchedules { get; set; }
        public virtual ICollection<TblStudentForm> TblStudentForms { get; set; }
    }
}
