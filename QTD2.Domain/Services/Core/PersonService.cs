using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Linq;

namespace QTD2.Domain.Services.Core
{
    public class PersonService : Common.Service<Person>, IPersonService
    {
        public PersonService(IPersonRepository repository, IPersonValidation validation)
            : base(repository, validation)
        {
        }

        public async System.Threading.Tasks.Task<Person> GetPersonById(int id)
        {
            return (await FindAsync(x => x.Id == id)).FirstOrDefault();
        }
        public async System.Threading.Tasks.Task<Person> GetPersonByUserName(string userName)
        {
            return (await FindAsync(x => x.Username == userName)).FirstOrDefault();
        }
    }
}
