using System;

namespace QTD2.Infrastructure.Model.Tool
{
    public class ToolUpdateOptions
    {
        public int ToolCategoryId { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }

        public string Hyperlink { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public byte[] Upload { get; set; }
    }
}
