using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblClass
    {
        public TblClass()
        {
            RsTblClassStudents = new HashSet<RsTblClassStudent>();
            TblClassTests = new HashSet<TblClassTest>();
        }

        public int Clid { get; set; }
        public int? Corid { get; set; }
        public DateTime? Cldate { get; set; }
        public int? Inid { get; set; }
        public int? Lcid { get; set; }
        public DateTime? ClstartDate { get; set; }
        public string Clnote1 { get; set; }
        public string Clnote2 { get; set; }
        public double? Reg2 { get; set; }
        public byte[] Ts { get; set; }
        public int? ProctorId { get; set; }
        public int? StudentFormsCompleted { get; set; }
        public int? CourseAvailableStart { get; set; }
        public int? CourseAvailableEnd { get; set; }
        public int? CourseAvailableExp { get; set; }
        public DateTime? StartDate { get; set; }
        public bool? SelfReg { get; set; }
        public bool? SelfRegOpen { get; set; }
        public DateTime? SelfRegEndDate { get; set; }
        public int? TotalSeats { get; set; }
        public string StartTimeStr { get; set; }
        public int? StartAmPm { get; set; }
        public string EndTimeStr { get; set; }
        public int? EndAmPm { get; set; }

        public virtual TblCourse Cor { get; set; }
        public virtual LktblInstructor In { get; set; }
        public virtual LktblLocation Lc { get; set; }
        public virtual ICollection<RsTblClassStudent> RsTblClassStudents { get; set; }
        public virtual ICollection<TblClassTest> TblClassTests { get; set; }
    }
}
