using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
   public class InstructorWorkbook_ProspectiveILAService : Common.Service<InstructorWorkbook_ProspectiveILA>, IInstructorWorkbook_ProspectiveILAService

    {
        public InstructorWorkbook_ProspectiveILAService(IInstructorWorkbook_ProspectiveILARepository repository, IInstructorWorkbook_ProspectiveILAValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<InstructorWorkbook_ProspectiveILA> GetIWBProspectiveILAByILAId(int ilaId)
        {
            return (await FindAsync(x => x.ILACorID == ilaId)).FirstOrDefault();
        }
    }
}