using System;

namespace QTD2.Infrastructure.Model.Procedure_IssuingAuthority
{
    public class Procedure_IssuingAuthorityCreateOptions
    {
        public string Title { get; set; }

        public string Website { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string Notes { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public string Description { get; set; }
    }
}
