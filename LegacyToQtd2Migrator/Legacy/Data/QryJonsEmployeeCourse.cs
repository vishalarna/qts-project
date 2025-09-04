using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QryJonsEmployeeCourse
    {
        public int Pid { get; set; }
        public int Corid { get; set; }
        public int Eid { get; set; }
        public string Employee { get; set; }
        public string Cornum { get; set; }
        public string Cordesc { get; set; }
    }
}
