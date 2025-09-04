using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class  Location_CategoryService : Common.Service<Entities.Core.Location_Category>, Interfaces.Service.Core.ILocation_CategoryService
    {
           public Location_CategoryService (ILocation_CategoryRepository location_CategoryRepository, ILocation_CategoryValidation location_CategoryValidation)
            :base (location_CategoryRepository, location_CategoryValidation)
        {

        }

    }
}
