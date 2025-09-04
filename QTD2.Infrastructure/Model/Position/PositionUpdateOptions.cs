using System;

namespace QTD2.Infrastructure.Model.Position
{
    public class PositionUpdateOptions
    {
        public int PositionNumber { get; set; }

        public string PositionAbbreviation { get; set; }

        public string PositionTitle { get; set; }

        public string PositionDescription { get; set; }

        public string HyperLink { get; set; }
        public bool IsPublished { get; set; }
        public string PositionsFileUpload { get; set; }
        public DateTime EffectiveDate { get; set; }

        public string ChangeNotes { get; set; }

        public string FileName { get; set; }
    }
}
