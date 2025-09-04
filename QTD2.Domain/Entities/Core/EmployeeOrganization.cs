namespace QTD2.Domain.Entities.Core
{
    public class EmployeeOrganization : Common.Entity
    {
        public int EmployeeId { get; set; }

        public int OrganizationId { get; set; }

        public bool IsManager { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Organization Organization { get; set; }

        public EmployeeOrganization(int employeeId, int organizationId, bool isManager)
        {
            EmployeeId = employeeId;
            OrganizationId = organizationId;
            IsManager = isManager;
        }

        public EmployeeOrganization()
        {
        }
    }
}
