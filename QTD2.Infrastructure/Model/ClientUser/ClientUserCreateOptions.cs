namespace QTD2.Infrastructure.Model.ClientUser
{
    public class ClientUserCreateOptions
    {
        public int PersonId { get; set; }
        public ClientUserCreateOptions()
        {

        }
        public ClientUserCreateOptions(int personId)
        {
            PersonId = personId;
        }
    }
}
