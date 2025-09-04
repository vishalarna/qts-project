using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VwLinkedDocument
    {
        public string Dtdesc { get; set; }
        public string TypeDesc { get; set; }
        public string LdfileName { get; set; }
        public string ElastName { get; set; }
        public string EfirstName { get; set; }
        public DateTime? Cldate { get; set; }
        public string Cornum { get; set; }
        public string Cordesc { get; set; }
        public int LinkedDocId { get; set; }
        public string Suname { get; set; }
        public int TypeId { get; set; }
        public int DocTypeId { get; set; }
        public int LinkItemId { get; set; }
        public string Comment { get; set; }
        public int DtsortOrder { get; set; }
        public int? Suid { get; set; }
        public int? Pid { get; set; }
        public int? Pnum { get; set; }
        public string Pabbrev { get; set; }
        public int? Corid { get; set; }
        public int? Clid { get; set; }
        public int Dtid { get; set; }
        public int Ldid { get; set; }
        public int? Eid { get; set; }
        public int? Tid { get; set; }
        public string Pdesc { get; set; }
        public int? Tnum { get; set; }
        public int? TsubNum { get; set; }
        public string Tabbrev { get; set; }
        public string Tdesc { get; set; }
        public int? Daid { get; set; }
        public string DutyArea { get; set; }
        public string SubDutyArea { get; set; }
        public string Task { get; set; }
        public string Dadesc { get; set; }
        public string SubDadesc { get; set; }
    }
}
