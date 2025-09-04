using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Provider : Entity
    {
        public string Name { get; set; }

        public string Number { get; set; }

        public int? ProviderLevelId { get; set; }

        public string ContactName { get; set; }

        public string ContactTitle { get; set; }

        public string? ContactPhone { get; set; }

        public int? ContactExt { get; set; }

        public string? ContactMobile { get; set; }

        public string ContactEmail { get; set; }

        public string CompanyWebsite { get; set; }

        public string RepName { get; set; }

        public string RepTitle { get; set; }

        public string? RepPhone { get; set; }

        public string RepEmail { get; set; }

        public string RepSignature { get; set; } // sould store base64 image string

        public bool IsPriority { get; set; }

        public bool IsNERC { get; set; }

        public virtual ProviderLevel ProviderLevel { get; set; }

        public virtual ICollection<ILA> ILAs { get; set; } = new List<ILA>();
        public virtual ICollection<MetaILA> MetaILAs { get; set; } = new List<MetaILA>();

        public virtual ICollection<ClassSchedule> ClassSchedules { get; set; } = new List<ClassSchedule>();

        public Provider(bool isNERC, bool isPriority, string repSignature, string repEmail, string? repPhone, string repTitle, string repName, string companyWebsite, string contactEmail, string? contactMobile, int? contactExt, string? contactPhone, string contactTitle, string contactName, int? providerLevelId, string number, string name)
        {
            IsNERC = isNERC;
            IsPriority = isPriority;
            RepSignature = repSignature;
            RepEmail = repEmail;
            RepPhone = repPhone;
            RepTitle = repTitle;
            RepName = repName;
            CompanyWebsite = companyWebsite;
            ContactEmail = contactEmail;
            ContactMobile = contactMobile;
            ContactExt = contactExt;
            ContactPhone = contactPhone;
            ContactTitle = contactTitle;
            ContactName = contactName;
            ProviderLevelId = providerLevelId;
            Number = number;
            Name = name;
        }

        public Provider()
        {
        }
    }
}
