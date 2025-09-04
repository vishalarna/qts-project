using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.EmpAuth
{
    public partial class TblQtsActivationCode
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string PrimaryEmail { get; set; }
        public int? ClientId { get; set; }
        public string ActivationCode { get; set; }
        public bool? ForRelease { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? CodeReleaseYear { get; set; }
    }
}
