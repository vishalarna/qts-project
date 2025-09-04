using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QTD2.Infrastructure.Model.RegulatoryRequirement
{
    public class RegulatoryRequirementCreateOptions
    {
        public int IssuingAuthorityId { get; set; }

        public string Number { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string RevisionNumber { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public byte[] Uploads { get; set; }

        public string HyperLink { get; set; }

        public string File { get; set; }

        public string FileName { get; set; }
    }
}
