using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.API.Infrastructure.Model.Client;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Person;
using QTD2.Infrastructure.Model.PersonActivityNotificationVM;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IPersonService
    {
        public Task<List<Person>> GetAsync();

        public Task<Person> GetAsync(int id);

        public Task<Person> CreateAsync(PersonCreateOptions options, bool isReturnConflictExp = false);
        public Task<List<PersonWithUserDataVm>> GetPersonsWithUserDataAsync();
        public Task<PersonWithUserDataVm> GetUserDetailAsync(int personId);
        public Task<PersonWithUserDataVm> GetUserDetailByUserNameAsync(string userName);

        public Task<Person> UpdateAsync(int id, PersonUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public Task<Person> DeactivateAsync(int id);

        public Task<Person> ActivateAsync(int id);

        public Task<Person> GetByUserNameAsync(string userName);
        public Task<List<Person>> GetPersonsWithoutQtdUser();
        public Task<Person> LinkActivityNotificationAsync(int id, PersonActivityNotification_VM options);
        public System.Threading.Tasks.Task UnlinkActivityNotificationAsync(int id, PersonActivityNotification_VM options);
        public Task<List<ActivityNotification>> GetLinkedActivityNotificationAsync(int id);
    }
}
