using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTempEvaluationDatum
    {
        public int Fqid { get; set; }
        public int? RatingHigh { get; set; }
        public int? RatingLow { get; set; }
        public float? RatingAverage { get; set; }
        public string Comments { get; set; }
        public int? ORatingHigh { get; set; }
        public int? ORatingLow { get; set; }
        public float? ORatingAverage { get; set; }
        public string OComments { get; set; }
        public byte[] UpsizeTs { get; set; }
    }
}
