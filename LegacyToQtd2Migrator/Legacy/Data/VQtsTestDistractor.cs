using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsTestDistractor
    {
        public int TestItemId { get; set; }
        public string Distractor { get; set; }
        public string DistractorDetails { get; set; }
        public string Text { get; set; }
    }
}
