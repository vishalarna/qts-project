namespace QTD2.Data.Repository.Authentication
{
    public class ClientRepository : Common.Repository<Domain.Entities.Authentication.Client>, Domain.Interfaces.Repository.Authentication.IClientRepository
    {
        public ClientRepository(QTDAuthenticationContext context)
            : base(context)
        {
        }
    }
}
