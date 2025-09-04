using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsCoursesTask
    {
        public int Tid { get; set; }
        public int Corid { get; set; }
        public byte[] Ts { get; set; }
        public int? Sequence { get; set; }
    }
}
