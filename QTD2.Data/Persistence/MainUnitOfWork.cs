using QTD2.Domain.Persistence;

namespace QTD2.Data.Persistence
{
    public class MainUnitOfWork : GenericUnitOfWork<QTDContext>, IMainUnitOfWork
    {
        public MainUnitOfWork(QTDContext dbContext) : base(dbContext)
        {
        }
    }
}