using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class SaftyHazard_CategoryService : Common.Service<Entities.Core.SaftyHazard_Category>, Interfaces.Service.Core.ISaftyHazard_CategoryService
    {
        public SaftyHazard_CategoryService(ISaftyHazard_CategoryRepository saftyHazard_CategoryRepository, ISaftyHazard_CategoryValidation saftyHazard_CategoryValidation)
            : base(saftyHazard_CategoryRepository, saftyHazard_CategoryValidation)
        {
        }
    }
}
