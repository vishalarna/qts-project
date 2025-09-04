using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class LktblSupplier
    {
        public LktblSupplier()
        {
            TblCourses = new HashSet<TblCourse>();
            TblPerspectiveCourses = new HashSet<TblPerspectiveCourse>();
        }

        public int Suid { get; set; }
        public string Suname { get; set; }
        public string Nercid { get; set; }
        public string ContactPerson { get; set; }
        public string CpTitle { get; set; }
        public string CpPhone { get; set; }
        public string CpFax { get; set; }
        public string CpStreetAddress { get; set; }
        public string CpCity { get; set; }
        public string CpState { get; set; }
        public string CpZip { get; set; }
        public string CpEmail { get; set; }
        public string CpWebsite { get; set; }
        public string PaymentInfoName { get; set; }
        public string PiTitle { get; set; }
        public string PiOrganization { get; set; }
        public string PiStreetAddress { get; set; }
        public string PiCity { get; set; }
        public string PiState { get; set; }
        public string PiZip { get; set; }
        public string PiEmail { get; set; }
        public string PiPhone { get; set; }
        public string PiFax { get; set; }
        public int? PaymentMethod { get; set; }
        public int? CardNumber { get; set; }
        public string CardHolder { get; set; }
        public DateTime? ExpDate { get; set; }
        public string Note1 { get; set; }
        public string Note2 { get; set; }
        public bool DefaultProvider { get; set; }
        public string TprName { get; set; }
        public string TprTitle { get; set; }
        public string TprPhone { get; set; }
        public string TprFax { get; set; }
        public string TprEmail { get; set; }
        public string TprSignaturePath { get; set; }
        public byte[] Ts { get; set; }
        public int? ProviderStatus { get; set; }
        public string SponsorName { get; set; }
        public string Ext { get; set; }
        public string Cell { get; set; }
        public bool? PhoneContact { get; set; }
        public bool? PhonePreferred { get; set; }
        public bool? PhoneFirst { get; set; }
        public bool? Inactive { get; set; }

        public virtual ICollection<TblCourse> TblCourses { get; set; }
        public virtual ICollection<TblPerspectiveCourse> TblPerspectiveCourses { get; set; }
    }
}
