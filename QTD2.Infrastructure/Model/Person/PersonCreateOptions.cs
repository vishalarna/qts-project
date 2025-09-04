namespace QTD2.Infrastructure.Model.Person
{
    public class PersonCreateOptions
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Image { get; set; }
        public PersonCreateOptions()
        {

        }
        public PersonCreateOptions(string firstName, string middleName, string lastName, string userName, string image)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Username = userName;
            Image = image;
        }
    }
}
