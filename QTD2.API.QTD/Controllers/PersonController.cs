using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.API.Infrastructure.Model.Client;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ClientUser;
using QTD2.Infrastructure.Model.Employee;
using QTD2.Infrastructure.Model.Instructor;
using QTD2.Infrastructure.Model.Person;
using QTD2.Infrastructure.Model.QtdUser;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IEmployeeService _employeeService;
        private readonly IClientUserService _clientUserService;
        private readonly IInstructorService _instructorrService;
        private readonly IQTDService _qTDService;
        private readonly IStringLocalizer<PersonController> _stringLocalizer;

        public PersonController(IPersonService personService, IStringLocalizer<PersonController> stringLocalizer, 
            IEmployeeService employeeService, IClientUserService clientUserService, IInstructorService instructorrService, IQTDService qTDService)
        {
            _personService = personService;
            _stringLocalizer = stringLocalizer;
            _employeeService = employeeService;
            _clientUserService = clientUserService;
            _instructorrService = instructorrService;
            _qTDService = qTDService;
        }

        /// <summary>
        /// Gets a list of Persons
        /// </summary>
        /// <returns>Http Response code with client users</returns>
        [HttpGet]
        [Route("/persons")]
        public async Task<IActionResult> GetAsync()
        {
            var persons = await _personService.GetAsync();
            return Ok( new { persons });
        }

        /// <summary>
        /// Creates a new person
        /// </summary>
        /// <param name="option"></param>
        /// <returns>Http Response code with client user</returns>
        [HttpPost]
        [Route("/persons")]
        public async Task<IActionResult> CreateAsync(PersonCreateOptions option)
        {
            var person = await _personService.CreateAsync(option);
            return Ok( new { person, message = _stringLocalizer["PersonCreated"] });
        }

        /// <summary>
        /// Gets a person by name
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response code with client user</returns>
        [HttpGet]
        [Route("/persons/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var person = await _personService.GetAsync(id);
            return Ok( new { person });
        }

        /// <summary>
        /// Updates a person
        /// </summary>
        /// <param name="id"></param>
        /// <param name="option"></param>
        /// <returns>Http Response code with client user</returns>
        [HttpPut]
        [Route("/persons/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, PersonUpdateOptions option)
        {
            var person = await _personService.UpdateAsync(id, option);
            return Ok( new { person, message = _stringLocalizer["PersonUpdated"] });
        }

        /// <summary>
        /// Deletes a person
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response code with message</returns>
        [HttpDelete]
        [Route("/persons/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _personService.DeleteAsync(id);
            return Ok( new { message = _stringLocalizer["PersonDeleted"] });
        }

        [HttpGet]
        [Route("/persons/withoutQtdUsers")]
        public async Task<IActionResult> GetPersonsWithoutQtdUsers()
        {
            var persons = await _personService.GetPersonsWithoutQtdUser();
            return Ok( new { persons });
        }

        [HttpGet]
        [Route("/persons/getByUserName/{userName}")]
        public async Task<IActionResult> GetPersonByUserNameAsync(string userName)
        {
            var person = await _personService.GetByUserNameAsync(userName);
            return Ok(new { person });
        }
    }
}
