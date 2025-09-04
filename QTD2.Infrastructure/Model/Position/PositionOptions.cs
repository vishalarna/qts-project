using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Position
{
    public class PositionOptions
    {
        public string PositionNumber { get; set; }

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
