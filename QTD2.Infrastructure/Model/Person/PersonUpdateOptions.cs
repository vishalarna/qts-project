namespace QTD2.Infrastructure.Model.Person
{
    public class PersonUpdateOptions
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Image { get; set; }

        public PersonUpdateOptions()
        {

        }
        public PersonUpdateOptions(string firstName, string middleName, string lastName, string image)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Image = image;
        }
    }
}
