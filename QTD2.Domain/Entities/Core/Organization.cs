using System.Collections.Generic;

namespace QTD2.Domain.Entities.Core
{
    public class Organization : Common.Entity
    {
        public string Name { get; set; }
        public bool PublicOrganization { get; set; }
        public virtual ICollection<EmployeeOrganization> EmployeeOrganizations { get; set; } = new List<EmployeeOrganization>();

        public Organization(string name)
        {
            Name = name;
        }

        public Organization()
        {
        }
        public Organization(string name, bool publicOrganization)
        {
            Name = name;
            PublicOrganization = publicOrganization;
        }
        public override void Delete()
        {
            AddDomainEvent(new Domain.Events.Core.OnOrganizationDeleted(this));
            base.Delete();
        }

    }
}
