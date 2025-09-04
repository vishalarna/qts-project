using System;

namespace QTD2.Infrastructure.Model.CertifyingBody
{
    public class CertifyingBodyCreateOptions
    {
        public string Name { get; set; }

        public string Desc { get; set; }
        public string Website { get; set; }

        public DateTime EffectiveDate { get; set; }
        public string Notes { get; set; }

        public bool? IsNERC { get; set; }
    }
}
