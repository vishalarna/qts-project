using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IPersonService : Common.IService<Person>
    {
        public System.Threading.Tasks.Task<Person> GetPersonById(int id);
        public System.Threading.Tasks.Task<Person> GetPersonByUserName(string userName);
    }
}
