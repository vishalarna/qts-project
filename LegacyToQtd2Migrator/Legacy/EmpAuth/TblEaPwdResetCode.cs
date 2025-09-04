using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.EmpAuth
{
    public partial class TblEaPwdResetCode
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string ResetCode { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsValid { get; set; }
    }
}
