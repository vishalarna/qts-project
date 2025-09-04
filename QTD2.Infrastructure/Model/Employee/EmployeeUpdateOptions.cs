namespace QTD2.Infrastructure.Model.Employee
{
    public class EmployeeUpdateOptions
    {
        public bool Active { get; set; }

        public string Address { get; set; }

        public string City { get; set; }
        
        public string? EmployeeNumber { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string PhoneNumber { get; set; }

        public string WorkLocation { get; set; }

        public string Notes { get; set; }

        public bool TQEqulator { get; set; }
        public bool PublicUser { get; set; }

        public EmployeeUpdateOptions()
        {

        }
    }
}
