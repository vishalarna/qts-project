using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class LktblInstructor
    {
        public LktblInstructor()
        {
            TblClasses = new HashSet<TblClass>();
            TblInstructorsAdministrators = new HashSet<TblInstructorsAdministrator>();
            TblPerspectiveCourseInstructors = new HashSet<TblPerspectiveCourse>();
            TblPerspectiveCourseReviewers = new HashSet<TblPerspectiveCourse>();
            TblSmudcourseImplementClassSchedules = new HashSet<TblSmudcourseImplementClassSchedule>();
        }

        public int Inid { get; set; }
        public string Inname { get; set; }
        public string InNote1 { get; set; }
        public string InNote2 { get; set; }
        public bool Inactive { get; set; }
        public string Inemail { get; set; }
        public byte[] Ts { get; set; }

        public virtual ICollection<TblClass> TblClasses { get; set; }
        public virtual ICollection<TblInstructorsAdministrator> TblInstructorsAdministrators { get; set; }
        public virtual ICollection<TblPerspectiveCourse> TblPerspectiveCourseInstructors { get; set; }
        public virtual ICollection<TblPerspectiveCourse> TblPerspectiveCourseReviewers { get; set; }
        public virtual ICollection<TblSmudcourseImplementClassSchedule> TblSmudcourseImplementClassSchedules { get; set; }
    }
}
