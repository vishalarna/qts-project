using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.Person;
using QTD2.Infrastructure.Model;
using IPersonDomainService = QTD2.Domain.Interfaces.Service.Core.IPersonService;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Drawing.Drawing2D;
using QTD2.Infrastructure.ExtensionMethods;
using QTD2.API.Infrastructure.Model.Client;
using QTD2.Infrastructure.Model.ClientUser;
using QTD2.Infrastructure.Model.Employee;
using QTD2.Infrastructure.Model.Instructor;
using QTD2.Infrastructure.Database.Interfaces;
using IEmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeService;
using IInstructorDomainService = QTD2.Domain.Interfaces.Service.Core.IInstructor_Service;
using IClientUserDomainService = QTD2.Domain.Interfaces.Service.Core.IClientUserService;
using IQTDUserDomainService = QTD2.Domain.Interfaces.Service.Core.IQTDUserService;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.PersonActivityNotificationVM;
using System.Net;
using System.Linq.Expressions;

namespace QTD2.Application.Services.Shared
{
    public class PersonService : IPersonService
    {
        private readonly IPersonDomainService _personService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<PersonService> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmployeeDomainService _employeeDomainService;
        private readonly IInstructorDomainService _instructorDomainService;
        private readonly IClientUserDomainService _clientUserDomainService;
        private readonly IQTDUserDomainService _qTDUserDomainService;
        private readonly Domain.Interfaces.Service.Core.IActivityNotificationService _activityNotificationService;
        private readonly Domain.Interfaces.Service.Core.IPersonActivityNotificationService _personActivityNotificationService;
        public PersonService(
            IPersonDomainService personService,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer<PersonService> localizer,
            UserManager<AppUser> userManager,
             IEmployeeDomainService employeeDomainService,
             IInstructorDomainService instructorDomainService,
             IClientUserDomainService clientUserDomainService,
             IQTDUserDomainService qTDUserDomainService,
             Domain.Interfaces.Service.Core.IActivityNotificationService activityNotificationService,
             Domain.Interfaces.Service.Core.IPersonActivityNotificationService personActivityNotificationService
            )
        {
            _personService = personService;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
            _userManager = userManager;
            _employeeDomainService = employeeDomainService;
            _instructorDomainService = instructorDomainService;
            _clientUserDomainService = clientUserDomainService;
            _qTDUserDomainService = qTDUserDomainService;
            _activityNotificationService = activityNotificationService;
            _personActivityNotificationService = personActivityNotificationService;
        }

        public async Task<Person> ActivateAsync(int id)
        {
            var person = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, person, PersonOperations.Update);
            if (result.Succeeded)
            {
                person.Activate();
                var validationResult = await _personService.UpdateAsync(person);

                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
            return person;
        }

        public async Task<Person> CreateAsync(PersonCreateOptions options, bool isReturnConflictExp = false)
        {
            var person = new Person(options.FirstName, options.MiddleName, options.LastName, options.Username, options.Image == "" || options.Image == null ? "" : options.Image.CompressImage());
            var existingPerson = (await _personService.FindAsync(r => r.Username == options.Username)).FirstOrDefault();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, person, PersonOperations.Create);

            if (result.Succeeded)
            {
                if (existingPerson == null)
                {
                    person.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    person.CreatedDate = DateTime.Now;
                    var validationResult = await _personService.AddAsync(person);

                    if (validationResult.IsValid)
                    {
                        person = (await _personService.FindAsync(r => r.Username == options.Username)).FirstOrDefault();
                        return person;
                    }
                    else
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
                else
                {
                    if (isReturnConflictExp)
                    {
                        throw new ConflictExceptionHelper(existingPerson);
                    }
                    else
                    {
                        throw new QTDServerException(displayMessage: _localizer["Employee Username already in use"],false, statusCode:System.Net.HttpStatusCode.Conflict);
                    }
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<Person> DeactivateAsync(int id)
        {
            var person = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, person, PersonOperations.Update);
            if (result.Succeeded)
            {
                person.Deactivate();
                if (string.IsNullOrEmpty(person.FirstName))
                {
                    person.FirstName = "NoName";
                }
                if (string.IsNullOrEmpty(person.LastName))
                {
                    person.LastName = "NoName";
                }
                if (string.IsNullOrEmpty(person.Username))
                {
                    person.Username = string.Concat("NoName", person.Id, "@qualitytrainingsystems.com");
                }
                var validationResult = await _personService.UpdateAsync(person);

                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
            return person;
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var person = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, person, PersonOperations.Delete);
            if (result.Succeeded)
            {
                person.Delete();
                var validationResult = await _personService.UpdateAsync(person);

                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<List<Person>> GetAsync()
        {
            var persons = await _personService.AllAsync();
            persons = persons.Where(p => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, p, PersonOperations.Read).Result.Succeeded);
            return persons.ToList();
        }

        public async Task<Person> GetAsync(int id)
        {
            var person = await _personService.GetAsync(id);
            if (person != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, person, PersonOperations.Read);
                if (result.Succeeded)
                {
                    return person;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return person;
        }
        public async Task<Person> GetByUserNameAsync(string userName)
        {
            var person = await _personService.GetPersonByUserName(userName);
            if (person != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, person, PersonOperations.Read);
                if (result.Succeeded)
                {
                    return person;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return person;
        }

        public async Task<Person> UpdateAsync(int id, PersonUpdateOptions option)
        {
            var person = await GetAsync(id);
            var personWithSameUsername = await _personService.FindAsync(x => x.Id != id && x.Username == option.UserName);
            if (personWithSameUsername.Any())
            {
                throw new QTDServerException(displayMessage: "Failed to Update Person: Email Address exists in database", false, HttpStatusCode.Conflict);
            }
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, person, PersonOperations.Update);
            if (result.Succeeded)
            {
                person.FirstName = option.FirstName;
                person.MiddleName = option.MiddleName;
                person.LastName = option.LastName;
                person.Username = option.UserName;
                person.Image = (option.Image == null || option.Image == "") ? (person.Image == null || person.Image == "" ? person.Image : person.Image.CompressImage()) : option.Image.CompressImage();
                person.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                person.ModifiedDate = DateTime.Now;
                var validationResult = await _personService.UpdateAsync(person);

                if (validationResult.IsValid)
                {
                    return person;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new QTDServerException(displayMessage: _localizer["OperationNotAllowed"], false, HttpStatusCode.Unauthorized);
            }
        }

        public async Task<List<PersonWithUserDataVm>> GetPersonsWithUserDataAsync()
        {
            var persons = await _personService.AllWithIncludeAsync(new string[] { "ClientUser", "Employee", "QTDUser" });
            List<PersonWithUserDataVm> personWithUserDataVms = new List<PersonWithUserDataVm>();

            foreach (var person in persons)
            {
                var instructor = (await _instructorDomainService.FindWithIncludeAsync(x => x.InstructorEmail == person.Username, new string[] { "Instructor_Category" })).FirstOrDefault();

                PersonWithUserDataVm personWithUserDataVm = new PersonWithUserDataVm
                {
                    PersonId = person.Id,
                    EmployeeId = person?.Employee?.Id,
                    InstructorId = instructor?.Id,
                    QTDUserId = person.QTDUser?.Id,
                    Username = person.Username,
                    FirstName = person.FirstName,
                    MiddleName = person.MiddleName,
                    LastName = person.LastName,
                    IsEmployee = person.Employee?.Active ?? false,
                    EmployeeNumber = person.Employee != null ? person.Employee.EmployeeNumber : "",
                    EmployeeAddress = person.Employee != null ? person.Employee.Address : "",
                    EmployeeCity = person.Employee != null ? person.Employee.City : "",
                    EmployeeState = person.Employee != null ? person.Employee.State : "",
                    EmployeeZipCode = person.Employee != null ? person.Employee.ZipCode : "",
                    EmployeePhoneNumber = person.Employee != null ? person.Employee.PhoneNumber : "",
                    EmployeeWorkLocation = person.Employee != null ? person.Employee.WorkLocation : "",
                    EmployeeNotes = person.Employee != null ? person.Employee.Notes : "",
                    IsTQEvaluator = person.Employee != null ? person.Employee.TQEqulator : false,
                    IsInstructor = instructor?.Active ?? false,
                    InstructorCategoryTitle = instructor != null ? instructor.Instructor_Category?.ICategoryTitle : "",
                    InstructorCategoryId = instructor != null ? instructor.Instructor_Category.Id : null,
                    InstructorNumber = instructor != null ? instructor.InstructorNumber : "",
                    InstructorDescription = instructor != null ? instructor.InstructorDescription : "",
                    InstructorIsworkbookadmin = instructor != null ? instructor.IsWorkBookAdmin : false,
                    IsQTDUser = person.QTDUser != null ? person.QTDUser.Active : false,
                    IsQTDUserActive = person.QTDUser?.Active,
                    IsEmployeeActive = person.Employee?.Active,
                    IsInstructorActive = instructor?.Active,
                    Active = person.Active
                };
                personWithUserDataVms.Add(personWithUserDataVm);
            }
            return personWithUserDataVms;
        }

        public async Task<List<Person>> GetPersonsWithoutQtdUser()
        {
            List<Expression<Func<Person, bool>>> predicates = new List<Expression<Func<Person, bool>>>();
            predicates.Add(x => x.Id != x.QTDUser.PersonId);
            var persons = await _personService.FindWithIncludeAsync(predicates, new string[] { "QTDUser", "Employee.EmployeePositions", "Employee.EmployeeOrganizations" }, true);
            return persons.ToList();
        }

        public async Task<PersonWithUserDataVm> GetUserDetailAsync(int personId)
        {
            var person = (await _personService.FindWithIncludeAsync(x => x.Id == personId, new string[] { "ClientUser", "Employee", "QTDUser" })).FirstOrDefault();
            var instructor = (await _instructorDomainService.FindWithIncludeAsync(x => x.InstructorEmail == person.Username, new string[] { "Instructor_Category" })).FirstOrDefault();
            PersonWithUserDataVm personWithUserDataVm = new PersonWithUserDataVm
            {
                PersonId = person.Id,
                EmployeeId = person?.Employee?.Id,
                InstructorId = instructor?.Id,
                QTDUserId = person.QTDUser?.Id,
                Username = person.Username,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                LastName = person.LastName,
                IsEmployee = person.Employee?.Active ?? false,
                EmployeeNumber = person.Employee != null ? person.Employee.EmployeeNumber : "",
                EmployeeAddress = person.Employee != null ? person.Employee.Address : "",
                EmployeeCity = person.Employee != null ? person.Employee.City : "",
                EmployeeState = person.Employee != null ? person.Employee.State : "",
                EmployeeZipCode = person.Employee != null ? person.Employee.ZipCode : "",
                EmployeePhoneNumber = person.Employee != null ? person.Employee.PhoneNumber : "",
                EmployeeWorkLocation = person.Employee != null ? person.Employee.WorkLocation : "",
                EmployeeNotes = person.Employee != null ? person.Employee.Notes : "",
                IsTQEvaluator = person.Employee != null ? person.Employee.TQEqulator : false,
                IsInstructor = instructor?.Active ?? false,
                InstructorCategoryTitle = instructor != null ? instructor.Instructor_Category?.ICategoryTitle : "",
                InstructorCategoryId = instructor != null ? instructor.Instructor_Category.Id : null,
                InstructorNumber = instructor != null ? instructor.InstructorNumber : "",
                InstructorDescription = instructor != null ? instructor.InstructorDescription : "",
                InstructorIsworkbookadmin = instructor != null ? instructor.IsWorkBookAdmin : false,
                InstructorEffectiveDate = instructor != null ? instructor.EffectiveDate : DateTime.MinValue,
                IsQTDUser = person.QTDUser != null ? person.QTDUser.Active : false,
                IsQTDUserActive = person.QTDUser?.Active,
                IsEmployeeActive = person.Employee?.Active,
                IsInstructorActive = instructor?.Active,
                Active = person.Active
            };
            return personWithUserDataVm;
        }

        public async Task<PersonWithUserDataVm> GetUserDetailByUserNameAsync(string userName)
        {
            var person = (await _personService.FindAsync(x => x.Username == userName)).FirstOrDefault();
            return person != null ? await GetUserDetailAsync(person.Id) : null;
        }

        public async Task<Person> LinkActivityNotificationAsync(int id, PersonActivityNotification_VM options)
        {
            var person = await _personService.GetWithIncludeAsync(id, new string[] { "PersonActivityNotifications" });

            List<int> existingLinkedActivityNotificationIds = person.PersonActivityNotifications.Select(pan => pan.ActivityNotificationId).ToList();

            // Remove ActivityNotifciations no longer linked
            foreach (var personActivityNotification in person.PersonActivityNotifications)
            {
                if (!options.ActivityNotificationIds.Contains(personActivityNotification.ActivityNotificationId))
                {
                    personActivityNotification.Delete();
                }
            }

            //Add net-new ActivityNotification links
            foreach (var activityNotificationId in options.ActivityNotificationIds.Where(ani => !existingLinkedActivityNotificationIds.Contains(ani)))
            {
                var activityNotification = await _activityNotificationService.GetAsync(activityNotificationId);

                if (activityNotification == null)
                {
                    throw new QTDServerException(_localizer["ActivityNotificationNotFound"]);
                }

                var personResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, person, AuthorizationOperations.Update);

                var activityResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, activityNotification, AuthorizationOperations.Read);

                if (personResult.Succeeded && activityResult.Succeeded)
                {
                    var person_activityNotification = person.LinkActivityNotification(activityNotification);
                    person_activityNotification.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    person_activityNotification.CreatedDate = DateTime.Now;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            await _personService.UpdateAsync(person);
            person = await GetAsync(id);
            return person;
        }

        public async System.Threading.Tasks.Task UnlinkActivityNotificationAsync(int id, PersonActivityNotification_VM options)
        {
            var person = await _personService.GetWithIncludeAsync(id, new string[] { "PersonActivityNotifications" });
            var activityNotification = await _activityNotificationService.GetAsync(options.ActivityNotificationId);

            if (activityNotification == null)
            {
                throw new QTDServerException(_localizer["ActivityNotificationNotFound"]);
            }

            var personResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, person, AuthorizationOperations.Update);
            var activityResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, activityNotification, AuthorizationOperations.Read);

            if (personResult.Succeeded && activityResult.Succeeded)
            {
                person.UnlinkActivityNotification(activityNotification);
                await _personService.UpdateAsync(person);
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<List<ActivityNotification>> GetLinkedActivityNotificationAsync(int id)
        {
            var result = await _personActivityNotificationService.FindWithIncludeAsync(x => x.PersonId == id, new string[] { "ActivityNotification" });
            List<ActivityNotification> linkedPositions = new List<ActivityNotification>();
            linkedPositions.AddRange(result.Select(x => x.ActivityNotification));
            linkedPositions = linkedPositions.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded).ToList();
            return linkedPositions;
        }

    }
}
