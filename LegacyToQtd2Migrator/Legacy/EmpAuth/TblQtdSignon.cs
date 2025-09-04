using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.EmpAuth
{
    public partial class TblQtdSignon
    {
        public int Id { get; set; }
        public DateTime? Requestdate { get; set; }
        public string CompanyName { get; set; }
        public string ClientId { get; set; }
        public string ActivationCode { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string WorkingMode { get; set; }
        public string AccessLevel { get; set; }
        public bool? Empaccess { get; set; }
        public bool? Tdtaccess { get; set; }
        public string Qtdversion { get; set; }
        public DateTime? QtdreleaseDate { get; set; }
        public string SqlserverName { get; set; }
        public string Sqldbname { get; set; }
        public string CurrentUser { get; set; }
        public string MachineName { get; set; }
        public string Ipaddress { get; set; }
        public DateTime? AppTimestamp { get; set; }
    }
}
