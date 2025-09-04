using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class InstructorWorkbook_ILADesign_DelieveryMethodsRepository : Common.Repository<InstructorWorkbook_ILADesign_DelieveryMethods>, IInstructorWorkbook_ILADesign_DelieveryMethodsRepository
    {
        public InstructorWorkbook_ILADesign_DelieveryMethodsRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
