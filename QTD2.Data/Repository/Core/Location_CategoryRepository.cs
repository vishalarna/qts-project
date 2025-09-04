using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class  Location_CategoryRepository : Common.Repository<Location_Category>, ILocation_CategoryRepository
    {

        public Location_CategoryRepository (QTDContext context)
            :base (context)
        {

        }
    }
}
