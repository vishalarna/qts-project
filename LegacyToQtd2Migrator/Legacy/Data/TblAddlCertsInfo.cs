using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblAddlCertsInfo
    {
        public int Id { get; set; }
        public int Eid { get; set; }
        public int TrainingTypeId { get; set; }
        public DateTime? AddlCertIssueDate { get; set; }
        public DateTime? AddlCertExpDate { get; set; }
        public string AddlCertNum { get; set; }
        public DateTime? DateAdded { get; set; }
    }
}
