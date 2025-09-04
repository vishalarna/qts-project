using System;

namespace QTD2.Infrastructure.Model.EnablingObjective_SubCategory
{
    public class EnablingObjective_SubCategoryOptions
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public int Number { get; set; }

        public string Reason { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
