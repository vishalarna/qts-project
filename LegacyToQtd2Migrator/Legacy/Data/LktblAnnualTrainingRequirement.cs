using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class LktblAnnualTrainingRequirement
    {
        public string TrainingType { get; set; }
        public int? TrainingHours { get; set; }
        public string Acronym { get; set; }
        public byte[] Ts { get; set; }
        public int TrainingTypeId { get; set; }
    }
}
