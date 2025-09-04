using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsAdditionalCertsForIla
    {
        public int Corid { get; set; }
        public float? Ceh { get; set; }
        public int TrainingTypeId { get; set; }
        public string TrainingType { get; set; }
        public int SortBy { get; set; }
    }
}
