using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsEmpPosChangeDesk
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Eid { get; set; }
        public int Pid { get; set; }
        public DateTime? PosStartDate { get; set; }
        public DateTime? PosEndDate { get; set; }
        public DateTime? QualificationDate { get; set; }
        public int? TrainingProgram { get; set; }
        public bool? Trainee { get; set; }
    }
}
