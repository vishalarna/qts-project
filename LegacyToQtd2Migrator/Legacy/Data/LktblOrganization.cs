using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class LktblOrganization
    {
        public LktblOrganization()
        {
            TblEmployees = new HashSet<TblEmployee>();
            TblQtsmgrOrganizations = new HashSet<TblQtsmgrOrganization>();
        }

        public int Oid { get; set; }
        public string Oname { get; set; }
        public string Oaddress { get; set; }
        public string Ocity { get; set; }
        public string Ostate { get; set; }
        public string Ozip { get; set; }
        public string Ophone { get; set; }
        public string Ofax { get; set; }
        public string Oemail { get; set; }
        public string OwebSite { get; set; }
        public byte[] Ts { get; set; }

        public virtual ICollection<TblEmployee> TblEmployees { get; set; }
        public virtual ICollection<TblQtsmgrOrganization> TblQtsmgrOrganizations { get; set; }
    }
}
