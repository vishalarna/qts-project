namespace QTD2.Infrastructure.Model.EmployeeOrganization
{
    public class EmployeeOrganizationCreateOptions
    {
        public int[] OrganizationIds { get; set; }

        public bool IsManager { get; set; }
        public int OrganizationId { get; set; }
    }
}
