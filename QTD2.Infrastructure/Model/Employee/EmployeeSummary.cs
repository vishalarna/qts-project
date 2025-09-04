namespace QTD2.Infrastructure.Model.Employee
{
    public class EmployeeSummaryDTO
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string EmployeeNumber { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string UserName { get; set; }

        public bool Active { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }

        }

    }
}
