using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Location_Repository : Common.Repository<Location>, ILocation_Repository
    {

        public Location_Repository(QTDContext qtdContext)
            :base (qtdContext)
        {

        }
    }
}
