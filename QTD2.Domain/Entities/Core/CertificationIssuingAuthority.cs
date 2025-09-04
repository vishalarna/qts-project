using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class CertificationIssuingAuthority : Common.Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Website { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string Notes { get; set; }

        public CertificationIssuingAuthority()
        {
        }

        public CertificationIssuingAuthority(string description, string title, string website, DateTime effectiveDate, string notes)
        {
            Description = description;
            Title = title;
            Website = website;
            EffectiveDate = effectiveDate;
            Notes = notes;
        }
    }
}
