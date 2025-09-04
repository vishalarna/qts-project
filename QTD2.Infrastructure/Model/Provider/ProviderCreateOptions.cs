using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Provider
{
    public class ProviderCreateOptions
    {
        public string Name { get; set; }

        public string Number { get; set; }

        public int? ProviderLevelId { get; set; }

        public string? ContactName { get; set; }

        public string? ContactTitle { get; set; }

        public string? ContactPhone { get; set; }

        public int? ContactExt { get; set; }

        public string? ContactMobile { get; set; }

        public string? ContactEmail { get; set; }

        public string? CompanyWebsite { get; set; }

        public string RepName { get; set; }

        public string? RepTitle { get; set; }

        public string? RepPhone { get; set; }

        public string? RepEmail { get; set; }

        public string RepSignature { get; set; } // sould store base64 image string

        public bool? IsPriority { get; set; }

        public bool IsNERC { get; set; }
    }
}
