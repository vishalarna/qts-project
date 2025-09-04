using System;

namespace QTD2.Infrastructure.Model.SaftyHazard
{
    public class SaftyHazardCreateOptions
    {
        public int SaftyHazardCategoryId { get; set; }

        public string Number { get; set; }

        public string Title { get; set; }

        public string RevisionNumber { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string HyperLinks { get; set; }

        public string Set { get; set; }

        public string Text { get; set; }

        public string Files { get; set; }

        public string Image { get; set; }

        public string FileName { get; set; }

        public string ChangeNotes { get; set; }
    }
}
