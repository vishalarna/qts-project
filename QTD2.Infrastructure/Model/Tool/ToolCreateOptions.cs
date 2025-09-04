using System;
using Microsoft.AspNetCore.Http;

namespace QTD2.Infrastructure.Model.Tool
{
    public class ToolCreateOptions
    {
        public int ToolCategoryId { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        public string Hyperlink { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public IFormFile Upload { get; set; }

        public string Description { get; set; }
    }
}
