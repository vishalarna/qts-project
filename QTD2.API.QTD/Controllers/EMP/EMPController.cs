using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers.EMP
{
    public class EMPController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmployeeService _employeeService;
        private int _employeeId;
        
        public EMPController(
            UserManager<AppUser> userManager, 
            IEmployeeService employeeService,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _employeeService = employeeService;
        }

        protected async Task<int> GetEmployeeIdAsync()
        {
            if (_employeeId > 0)
            {
                return _employeeId;
            }

            var username = _httpContextAccessor.HttpContext?.User.Identity?.Name;
            var employee = await _employeeService.GetEmployeeByUsernameAsync(username);
            _employeeId = employee?.Id ?? default;
            return _employeeId;
        }
    }
}
