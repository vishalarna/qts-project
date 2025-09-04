namespace QTD2.Domain.Entities.Core
{
    public class ClientUser : Common.Entity
    {
        public int PersonId { get; set; }

        public virtual Person Person { get; set; }

        public ClientUser(int personId)
        {
            PersonId = personId;
        }

        public ClientUser()
        {
        }
    }
}
