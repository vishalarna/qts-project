using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblEmployeeAdditionalPosition
    {
        public int Id { get; set; }
        public int Eid { get; set; }
        public int Pid { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? QualificationDate { get; set; }
        public bool? QualDateVerified { get; set; }
        public int? TrainingProgram { get; set; }
        public bool? Trainee { get; set; }
    }
}
