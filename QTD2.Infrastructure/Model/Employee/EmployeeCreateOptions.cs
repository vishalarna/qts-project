namespace QTD2.Infrastructure.Model.Employee
{
    public class EmployeeCreateOptions
    {
        public string Username { get; set; }

        public int PersonId { get; set; }

        public string? EmployeeNumber { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string? PhoneNumber { get; set; }

        public string WorkLocation { get; set; }

        public string Notes { get; set; }

        public string Password { get; set; }

        public bool TQEqulator { get; set; }
        public bool PublicUser { get; set; }

        public bool  EmpEnabled  { get; set; }
        public string WebsiteUrl  { get; set; }
        public EmployeeCreateOptions()
        {

        }
        public EmployeeCreateOptions(int personId, string employeeNumber, bool tQEqulator)
        {
            PersonId = personId;
            EmployeeNumber = employeeNumber;
            TQEqulator = tQEqulator;
        }
    }
}
