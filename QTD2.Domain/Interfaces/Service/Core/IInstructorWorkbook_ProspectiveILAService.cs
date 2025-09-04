using QTD2.Domain.Entities.Core;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
   public interface IInstructorWorkbook_ProspectiveILAService : Common.IService<InstructorWorkbook_ProspectiveILA>
    {
        public Task<InstructorWorkbook_ProspectiveILA> GetIWBProspectiveILAByILAId(int ilaId);
    }
}
