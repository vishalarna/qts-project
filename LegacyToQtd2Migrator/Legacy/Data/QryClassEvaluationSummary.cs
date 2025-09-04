using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QryClassEvaluationSummary
    {
        public int? Clid { get; set; }
        public int? Fid { get; set; }
        public int? Fqid { get; set; }
        public float? RatingHigh { get; set; }
        public float? RatingLow { get; set; }
        public double? RatingAverage { get; set; }
    }
}
