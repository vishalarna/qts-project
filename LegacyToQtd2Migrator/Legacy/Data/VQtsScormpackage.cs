using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsScormpackage
    {
        public int ScormPackageId { get; set; }
        public string WebPath { get; set; }
        public int LearningStandardId { get; set; }
        public int VersionId { get; set; }
        public string CorId { get; set; }
    }
}
