using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Org.BouncyCastle.Math.EC.Rfc7748;
using QTD2.Application.Interfaces.Services.QTD;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.Employee;
using QTD2.Infrastructure.Model.EmployeeCertification;
using QTD2.Infrastructure.Model.EmployeeOrganization;
using QTD2.Infrastructure.Model.EmployeePosition;
using IPersonDomainService = QTD2.Domain.Interfaces.Service.Core.IPersonService;
using ITaskQualDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskQualificationService;
using IClassScheduleEmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleEmployeeService;
using IPositionDomainService = QTD2.Domain.Interfaces.Service.Core.IPositionService;
using IOrganizationDomainService = QTD2.Domain.Interfaces.Service.Core.IOrganizationService;
using IEmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeService;
using QTD2.Domain.Services.Core;
using QTD2.Infrastructure.ExtensionMethods;
using IEmployeeCertificationHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeCertificationHistoryService;
using DocumentFormat.OpenXml.Office2010.Excel;
using QTD2.API.Infrastructure.Model.Client;
using QTD2.Infrastructure.Model.Person;
using QTD2.Infrastructure.Model;
using QTD2.Domain.Exceptions;
using System.Linq.Expressions;

namespace QTD2.Application.Services.Shared
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<EmployeeService> _localizer;
        private readonly Domain.Interfaces.Service.Core.IClientSettings_LicenseService _clientLicenseService;
        private readonly Domain.Interfaces.Service.Core.IEmployeeService _employeeService;
        private readonly Domain.Interfaces.Service.Core.ICertifyingBodyService _certifyingBodiesService;
        private readonly Domain.Interfaces.Service.Core.ICertificationService _certificationService;
        private readonly Domain.Interfaces.Service.Core.IEmployeeCertificationService _employeeCertificationService;
        private readonly Domain.Interfaces.Service.Core.IEmployeePositionService _employeePositionService;
        private readonly Domain.Interfaces.Service.Core.IEmployeeOrganizationService _employeeOrganizationService;
        private readonly Domain.Interfaces.Service.Core.IActivityNotificationService _activityNotificationService;
        private readonly Domain.Interfaces.Service.Core.IEmployeeDocumentService _employeeDocumentService;
        private readonly IPositionService _positionService;
        private readonly IOrganizationService _organizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly Employee _employee;
        private readonly EmployeePosition _employeePosition;
        private readonly EmployeeOrganization _employeeOrganization;
        private readonly CertifyingBody _certifyingBody;
        private readonly QTD2.Application.Interfaces.Services.QTD.INotificationService _notificationService;
        private readonly IPersonDomainService _personService;
        private readonly ITaskQualDomainService _taskQualService;
        private readonly IClassScheduleEmployeeDomainService _classScheduleService;
        private readonly IPositionDomainService _posDomainService;
        private readonly IOrganizationDomainService _orgDomainService;
        private readonly Domain.Interfaces.Service.Core.IClientSettings_NotificationService _clientSetting_NotifService;
        private readonly IEmployeeCertificationHistoryDomainService _employeeCertificationHistoryService;
        private readonly IEmployeeDomainService _employeeDomainService;

        public EmployeeService(
            Domain.Interfaces.Service.Core.IEmployeeService employeeService,
            Domain.Interfaces.Service.Core.ICertifyingBodyService certifyingBodiesService,
            Domain.Interfaces.Service.Core.ICertificationService certificationService,
            Domain.Interfaces.Service.Core.IEmployeeCertificationService employeeCertificationService,
            Domain.Interfaces.Service.Core.IEmployeePositionService employeePositionService,
            Domain.Interfaces.Service.Core.IEmployeeOrganizationService employeeOrganizationService,
            Domain.Interfaces.Service.Core.IEmployeeDocumentService employeeDocumentService,
            Domain.Interfaces.Service.Core.IClientSettings_LicenseService clientLicenseService,
            IPositionService positionService,
            IOrganizationService organizationService,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<EmployeeService> localizer,
            UserManager<AppUser> userManager,
            Domain.Interfaces.Service.Core.IActivityNotificationService activityNotificationService,
            QTD2.Application.Interfaces.Services.QTD.INotificationService notificationService,
            IPersonDomainService personService,
            ITaskQualDomainService taskQualService,
            IClassScheduleEmployeeDomainService classScheduleService,
            IPositionDomainService posDomainService,
            IOrganizationDomainService orgDomainService,
            Domain.Interfaces.Service.Core.IClientSettings_NotificationService clientSetting_NotifService,
            IEmployeeCertificationHistoryDomainService employeeCertificationHistoryService,
            IEmployeeDomainService employeeDomainService)
        {
            _employeeService = employeeService;
            _certificationService = certificationService;
            _employeeCertificationService = employeeCertificationService;
            _employeePositionService = employeePositionService;
            _employeeOrganizationService = employeeOrganizationService;
            _certifyingBodiesService = certifyingBodiesService;
            _positionService = positionService;
            _organizationService = organizationService;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _employee = new Employee();
            _employeePosition = new EmployeePosition();
            _employeeOrganization = new EmployeeOrganization();
            _activityNotificationService = activityNotificationService;
            _employeeDocumentService = employeeDocumentService;
            _certifyingBody = new CertifyingBody();
            _notificationService = notificationService;
            _personService = personService;
            _taskQualService = taskQualService;
            _classScheduleService = classScheduleService;
            _posDomainService = posDomainService;
            _orgDomainService = orgDomainService;
            _clientSetting_NotifService = clientSetting_NotifService;
            _clientLicenseService = clientLicenseService;
            _employeeCertificationHistoryService = employeeCertificationHistoryService;
            _employeeDomainService = employeeDomainService;
        }

        public async Task<List<Employee>> GetAsync()
        {
            //var Persons = await _personService.AllQuery().Select(s => new Person
            //{
            //    Id = s.Id,
            //    Active = s.Active,
            //    Deleted = s.Deleted,
            //    Image = s.Image,
            //    FirstName = s.FirstName,
            //    LastName = s.LastName,
            //    MiddleName = s.MiddleName,
            //    Username = s.Username
            //}).ToListAsync();
            System.Linq.Expressions.Expression<Func<EmployeePosition, bool>> empPositionPredicate = x => x.Position.Active;
            System.Linq.Expressions.Expression<Func<EmployeeOrganization, bool>> empOrgPredicate = x => x.Organization.Active;
            System.Linq.Expressions.Expression<Func<TaskQualification, bool>> tqPredicate = x => x.Active;
            System.Linq.Expressions.Expression<Func<ClassSchedule_Employee, bool>> csEmpPredicate = x => x.Active;

            var EmpPossitions = await _employeePositionService.GetEmpPositionsWithCompactPositionsAndConditions(empPositionPredicate);
            var EmpOrganisations = await _employeeOrganizationService.AllActiveEmpOrganizationsWithOrganizationAndConditions(empOrgPredicate);
            var TaskQualifications = await _taskQualService.GetCompactTQsWithConditionAndIncludes(tqPredicate, new string[] { });
            var CSEmployees = await _classScheduleService.GetCSEmpsWithConditionAndIncludes(csEmpPredicate, new string[] { });
            //var Organizations = await _orgDomainService.AllActiveOrganiaztions().ToListAsync();
            //var Possitions = await _posDomainService.GetAllActiveCompactPositions();

            var employees = await _employeeService.GetAllEmployeesWithCompactPersons();
            for (int i = 0; i < employees.Count; i++)
            {
                //if (employees[i].Person.Image != null)
                //{
                //    var imgHeader = Regex.Match(employees[i].Person.Image, @"^data:image/[^;]+;base64,").Value;
                //    employees[i].Person.Image = Regex.Replace(employees[i].Person.Image, @"^data:image/[^;]+;base64,", "");

                //    byte[] imageData = Convert.FromBase64String(employees[i].Person.Image);
                //    byte[] compressedData = CompressImageData(imageData);
                //    employees[i].Person.Image = Convert.ToBase64String(compressedData);
                //    employees[i].Person.Image = imgHeader + employees[i].Person.Image;
                //}
                //employees[i].Person = Persons.Where(x => x.Id == employees[i].PersonId).Select(s => new Person
                //{
                //    Id = s.Id,
                //    Active = s.Active,
                //    Deleted = s.Deleted,
                //    Image = s.Image,
                //    FirstName = s.FirstName,
                //    LastName = s.LastName,
                //    MiddleName = s.MiddleName,
                //    Username = s.Username
                //}).FirstOrDefault();
                var hasTQ = TaskQualifications.Where(x => x.EmpId == employees[i].Id).FirstOrDefault();


                var hasCS = CSEmployees.Where(x => x.EmployeeId == employees[i].Id).FirstOrDefault();
                employees[i].EmployeePositions = EmpPossitions.Where(x => x.EmployeeId == employees[i].Id && (DateOnly.FromDateTime(DateTime.Now) <= x.EndDate || !x.EndDate.HasValue)).ToList();
                employees[i].EmployeeOrganizations = EmpOrganisations.Where(x => x.EmployeeId == employees[i].Id).ToList();
                //employees[i].TaskQualifications = TaskQualifications.Where(x => x.EmpId == employees[i].Id).Select(s => new TaskQualification { Id = s.Id }).ToList();
                if (hasTQ != null)
                {
                    employees[i].TaskQualifications.Add(hasTQ);
                }

                if (hasCS != null)
                {
                    employees[i].ClassSchedule_Employee.Add(hasCS);
                }
                //employees[i].ClassSchedule_Employee = CSEmployees.Where(x => x.EmployeeId == employees[i].Id).Select(s => new ClassSchedule_Employee { Id = s.Id}).ToList();
                //for (int j = 0; j < employees[i].EmployeePositions.Count; j++)
                //{
                //    employees[i].EmployeePositions.ToList()[j].Position = Possitions.Where(x => x.Id == employees[i].EmployeePositions.ToList()[j].PositionId).Select(s => new Position
                //    {
                //        Id = s.Id,
                //        Active = s.Active,
                //        PositionAbbreviation = s.PositionAbbreviation,
                //        HyperLink = s.HyperLink,
                //        IsPublished = s.IsPublished,
                //        PositionDescription = s.PositionDescription,
                //        PositionNumber = s.PositionNumber,
                //        PositionTitle = s.PositionTitle,
                //        FileName = s.FileName,
                //        EffectiveDate = s.EffectiveDate
                //    }).FirstOrDefault();
                //}
                //for (int j = 0; j < employees[i].EmployeeOrganizations.Count; j++)
                //{
                //    employees[i].EmployeeOrganizations.ToList()[j].Organization = Organizations.Where(x => x.Id == employees[i].EmployeeOrganizations.ToList()[j].OrganizationId).FirstOrDefault();
                //}
            }
            employees = employees.Where(employee => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, employee, EmployeeOperations.Read).Result.Succeeded).ToList();
            return employees.OrderBy(x => x?.Person?.LastName).ToList();
        }

        public async Task<List<Employee>> GetAllActiveEmployees()
        {
            System.Linq.Expressions.Expression<Func<EmployeePosition, bool>> empPositionPredicate = x => x.Active && x.Position.Active;
            System.Linq.Expressions.Expression<Func<EmployeeOrganization, bool>> empOrgPredicate = x => x.Active && x.Organization.Active;
            System.Linq.Expressions.Expression<Func<TaskQualification, bool>> tqPredicate = x => x.Active;
            System.Linq.Expressions.Expression<Func<ClassSchedule_Employee, bool>> csEmpPredicate = x => x.Active;

            var EmpPossitions = await _employeePositionService.GetEmpPositionsWithCompactPositionsAndConditions(empPositionPredicate);
            var EmpOrganisations = await _employeeOrganizationService.AllActiveEmpOrganizationsWithOrganizationAndConditions(empOrgPredicate);
            var TaskQualifications = await _taskQualService.GetCompactTQsWithConditionAndIncludes(tqPredicate, new string[] { });
            var CSEmployees = await _classScheduleService.GetCSEmpsWithConditionAndIncludes(csEmpPredicate, new string[] { });
            //var Organizations = await _orgDomainService.AllActiveOrganiaztions().ToListAsync();
            //var Possitions = await _posDomainService.GetAllActiveCompactPositions();

            var employees = await _employeeService.GetAllActiveEmployeesWithCompactPersons();
            for (int i = 0; i < employees.Count; i++)
            {
                employees[i].EmployeePositions = EmpPossitions.Where(x => x.EmployeeId == employees[i].Id).ToList();
                employees[i].EmployeeOrganizations = EmpOrganisations.Where(x => x.EmployeeId == employees[i].Id).ToList();
                employees[i].TaskQualifications = TaskQualifications.Where(x => x.EmpId == employees[i].Id).Select(s => new TaskQualification { Id = s.Id }).ToList();
                employees[i].ClassSchedule_Employee = CSEmployees.Where(x => x.EmployeeId == employees[i].Id).Select(s => new ClassSchedule_Employee { Id = s.Id }).ToList();
            }
            employees = employees.Where(employee => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, employee, EmployeeOperations.Read).Result.Succeeded).ToList();
            return employees.OrderBy(x => x?.Person?.LastName).ToList();
        }

        public async Task<Employee> GetAsync(int employeeId)
        {
            var employee = await _employeeService.FindQuery(x => x.Id == employeeId).Select(s => new Employee
            {
                Active = s.Active,
                Deleted = s.Deleted,
                Address = s.Address,
                City = s.City,
                EmployeeNumber = s.EmployeeNumber,
                Id = s.Id,
                CreatedBy = s.CreatedBy,
                CreatedDate = s.CreatedDate,
                ModifiedBy = s.ModifiedBy,
                ModifiedDate = s.ModifiedDate,
                Notes = s.Notes,
                InactiveDate = s.InactiveDate,
                Password = s.Password,
                PersonId = s.PersonId,
                PhoneNumber = s.PhoneNumber,
                Reason = s.Reason,
                State = s.State,
                TQEqulator = s.TQEqulator,
                WorkLocation = s.WorkLocation,
                ZipCode = s.ZipCode,
                PublicUser = s.PublicUser
            }).IgnoreAutoIncludes().FirstOrDefaultAsync();

            //var employee = await _employeeService.FindQueryWithIncludeAsync(x => x.Id == employeeId, new string[] { "EmployeePositions.Position", "EmployeeOrganizations.Organization", "Person" }, true).FirstOrDefaultAsync();
            if (employee != null)
            {

                employee.Person = await _personService.FindQuery(x => x.Id == employee.PersonId).Select(s => new Person
                {
                    Id = s.Id,
                    Active = s.Active,
                    Deleted = s.Deleted,
                    Image = s.Image,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    MiddleName = s.MiddleName,
                    Username = s.Username
                }).FirstOrDefaultAsync();
                employee.EmployeePositions = await _employeePositionService.FindQuery(x => x.EmployeeId == employee.Id).AsNoTracking().ToListAsync();
                employee.EmployeeOrganizations = await _employeeOrganizationService.FindQuery(x => x.EmployeeId == employee.Id).ToListAsync();

                for (int j = 0; j < employee.EmployeePositions.Count; j++)
                {
                    employee.EmployeePositions.ToList()[j].Position = await _posDomainService.FindQuery(x => x.Id == employee.EmployeePositions.ToList()[j].PositionId).Select(s => new Domain.Entities.Core.Position
                    {
                        Id = s.Id,
                        Active = s.Active,
                        PositionAbbreviation = s.PositionAbbreviation,
                        HyperLink = s.HyperLink,
                        IsPublished = s.IsPublished,
                        PositionDescription = s.PositionDescription,
                        PositionNumber = s.PositionNumber,
                        PositionTitle = s.PositionTitle,
                        FileName = s.FileName,
                        EffectiveDate = s.EffectiveDate
                    }).FirstOrDefaultAsync();
                }
                for (int j = 0; j < employee.EmployeeOrganizations.Count; j++)
                {
                    employee.EmployeeOrganizations.ToList()[j].Organization = await _orgDomainService.FindQuery(x => x.Id == employee.EmployeeOrganizations.ToList()[j].OrganizationId).FirstOrDefaultAsync();
                }
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, employee, EmployeeOperations.Read);
                if (result.Succeeded)
                {
                    return employee;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return employee;
        }

        public async Task<Employee> GetByPersonIdAsync(int id)
        {
            //var employee = await _employeeService.FindQueryWithIncludeAsync(x => x.PersonId == id, new string[] { "EmployeePositions.Position", "EmployeeOrganizations.Organization", "Person" }, true).FirstOrDefaultAsync();
            var employee = await _employeeService.FindQuery(x => x.PersonId == id).Select(s => new Employee
            {
                Active = s.Active,
                Deleted = s.Deleted,
                Address = s.Address,
                City = s.City,
                EmployeeNumber = s.EmployeeNumber,
                Id = s.Id,
                CreatedBy = s.CreatedBy,
                CreatedDate = s.CreatedDate,
                ModifiedBy = s.ModifiedBy,
                ModifiedDate = s.ModifiedDate,
                Notes = s.Notes,
                InactiveDate = s.InactiveDate,
                Password = s.Password,
                PersonId = s.PersonId,
                PhoneNumber = s.PhoneNumber,
                Reason = s.Reason,
                State = s.State,
                TQEqulator = s.TQEqulator,
                WorkLocation = s.WorkLocation,
                ZipCode = s.ZipCode
            }).IgnoreAutoIncludes().FirstOrDefaultAsync();



            if (employee != null)
            {

                employee.Person = await _personService.FindQuery(x => x.Id == employee.PersonId).Select(s => new Person
                {
                    Id = s.Id,
                    Active = s.Active,
                    Deleted = s.Deleted,
                    Image = s.Image,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    MiddleName = s.MiddleName,
                    Username = s.Username
                }).FirstOrDefaultAsync();
                employee.EmployeePositions = await _employeePositionService.FindQuery(x => x.EmployeeId == employee.Id).ToListAsync();
                employee.EmployeeOrganizations = await _employeeOrganizationService.FindQuery(x => x.EmployeeId == employee.Id).ToListAsync();
                //employees[i].TaskQualifications = await _taskQualService.FindQuery(x => x.EmpId == employees[i].Id).ToListAsync();
                //employees[i].ClassSchedule_Employee = await _classScheduleService.FindQuery(x => x.EmployeeId == employees[i].Id).ToListAsync();
                for (int j = 0; j < employee.EmployeePositions.Count; j++)
                {
                    employee.EmployeePositions.ToList()[j].Position = await _posDomainService.FindQuery(x => x.Id == employee.EmployeePositions.ToList()[j].PositionId).Select(s => new Domain.Entities.Core.Position
                    {
                        Id = s.Id,
                        Active = s.Active,
                        PositionAbbreviation = s.PositionAbbreviation,
                        HyperLink = s.HyperLink,
                        IsPublished = s.IsPublished,
                        PositionDescription = s.PositionDescription,
                        PositionNumber = s.PositionNumber,
                        PositionTitle = s.PositionTitle,
                        FileName = s.FileName,
                        EffectiveDate = s.EffectiveDate
                    }).FirstOrDefaultAsync();
                }
                for (int j = 0; j < employee.EmployeeOrganizations.Count; j++)
                {
                    employee.EmployeeOrganizations.ToList()[j].Organization = await _orgDomainService.FindQuery(x => x.Id == employee.EmployeeOrganizations.ToList()[j].OrganizationId).FirstOrDefaultAsync();
                }
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, employee, EmployeeOperations.Read);
                if (result.Succeeded)
                {
                    return employee;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return employee;

        }
        public async Task<Employee> CreateAsync(EmployeeCreateOptions options, bool isReturnConflictExp = false)
        {
            if (options.PhoneNumber == null)
            {
                options.PhoneNumber = " ";
            }

            if (options.ZipCode == null)
            {
                options.ZipCode = "";
            }
            var employee = new Employee(options.PersonId, options.EmployeeNumber, options.Address, options.City, options.State, options.ZipCode, options.PhoneNumber, options.WorkLocation, options.Notes, options.TQEqulator, options.Password, null, null, options.PublicUser);
            var empExists = (await _employeeService.FindAsync(emp => emp.PersonId == options.PersonId)).FirstOrDefault();

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, employee, EmployeeOperations.Create);

            if (result.Succeeded)
            {
                if (empExists == null)
                {
                    var empNumber = (await _employeeService.FindAsync(emp => !string.IsNullOrEmpty(options.EmployeeNumber) && emp.EmployeeNumber == options.EmployeeNumber, false)).FirstOrDefault() != null;
                    if (empNumber)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: "Employee Number already in use");
                    }
                    var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    employee.Create(_httpContextAccessor.HttpContext.User.Identity.Name);
                    var validationResult = await _employeeService.AddAsync(employee);

                    if (validationResult.IsValid)
                    {
                        return employee;
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
                        throw new ConflictExceptionHelper(empExists);
                    }
                    else
                    {
                        return empExists;
                    }
                }

            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }

        }

        public async Task<Employee> UpdateAsync(int employeeId, EmployeeUpdateOptions options)
        {
            var employee = await _employeeService.GetAsync(employeeId);
            var employeeWithSameNumber = await _employeeDomainService.FindAsync(x => x.Id != employeeId && !String.IsNullOrWhiteSpace(options.EmployeeNumber) && x.EmployeeNumber == options.EmployeeNumber);
            if (employeeWithSameNumber.Any())
            {
                throw new BadHttpRequestException(message: _localizer["Employee Number already in use"], StatusCodes.Status409Conflict);
            }
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, employee, EmployeeOperations.Update);
            if (result.Succeeded)
            {
                updateEmployee(employee, options);
                var validationResult = await _employeeService.UpdateAsync(employee);
                if (validationResult.IsValid)
                {
                    return employee;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }

        protected void updateEmployee(Employee employee, EmployeeUpdateOptions options)
        {
            if (employee.Address != options.Address)
            {
                employee.SetAddress(options.Address);
            }
            if (employee.City != options.City)
            {
                employee.SetCity(options.City);
            }
            if (employee.State != options.State)
            {
                employee.SetState(options.State);
            }
            if (employee.ZipCode != options.ZipCode)
            {
                employee.SetZipCode(options.ZipCode);
            }
            if (employee.PhoneNumber != options.PhoneNumber)
            {
                employee.SetPhoneNumber(options.PhoneNumber);
            }
            if (employee.WorkLocation != options.WorkLocation)
            {
                employee.SetWorkLocation(options.WorkLocation);
            }
            if (employee.Notes != options.Notes)
            {
                employee.SetNotes(options.Notes);
            }
            if (employee.TQEqulator != options.TQEqulator)
            {
                employee.SetTQEqulator(options.TQEqulator);
            }
            if (employee.EmployeeNumber != options.EmployeeNumber)
            {
                employee.SetEmployeeNumber(options.EmployeeNumber);
            }
            if (employee.PublicUser != options.PublicUser)
            {
                employee.SetPublicUser(options.PublicUser);
            }
            var modifiedBy = _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            if (modifiedBy != null)
            {
                employee.Modify(modifiedBy.Result.UserName);
            }
        }
        //public async System.Threading.Tasks.Task UpdateEmployeeInactiveDate(int employeeId,DateTime inactiveDate, string reason)
        //{
        //    var employee = await GetAsync(employeeId);
        //    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, employee, EmployeeOperations.Update);
        //    if (result.Succeeded)
        //    {
        //        // Todo update employee
        //        employee.InactiveDate = inactiveDate;
        //        employee.Reason = reason;
        //        var validationResult = await _employeeService.UpdateAsync(employee);
        //    }
        //    else
        //    {
        //        throw new UnauthorizedAccessException(message: "OperationNotAllowed");
        //    }
        //}

        public async System.Threading.Tasks.Task DeleteAsync(int employeeId)
        {
            //var employee = await GetAsync(employeeId);
            var employee = await _employeeService.FindQuery(x => x.Id == employeeId).FirstOrDefaultAsync();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, employee, EmployeeOperations.Delete);
            if (result.Succeeded)
            {
                employee.Delete();

                var validationResult = await _employeeService.UpdateAsync(employee);

                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }

        public async System.Threading.Tasks.Task DeactivateAsync(int employeeId, EmployeeOptions options)
        {
            //var employee = await GetAsync(employeeId);
            var employee = await _employeeService.FindQuery(x => x.Id == employeeId).FirstOrDefaultAsync();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, employee, EmployeeOperations.Delete);
            if (result.Succeeded)
            {
                employee.Deactivate();
                employee.InactiveDate = DateOnly.FromDateTime(options.ChangeEffectiveDate);
                employee.Reason = options.ChangeNotes;

                var validationResult = await _employeeService.UpdateAsync(employee);
                await markPositionsEndDate(employeeId, options.ChangeEffectiveDate);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }

        public async System.Threading.Tasks.Task markPositionsEndDate(int empId, DateTime endDate)
        {
            var positionLinks = await _employeePositionService.FindQuery(x => x.EmployeeId == empId && (!x.EndDate.HasValue || x.EndDate >= DateOnly.FromDateTime(DateTime.Now))).ToListAsync();
            foreach (var positionLink in positionLinks)
            {
                positionLink.SetEndDate(DateOnly.FromDateTime(endDate));
                var validationResult = await _employeePositionService.UpdateAsync(positionLink);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
        }

        public async System.Threading.Tasks.Task ActivateAsync(int employeeId)
        {
            //var employee = await GetAsync(employeeId);
            var employee = await _employeeService.FindQuery(x => x.Id == employeeId).FirstOrDefaultAsync();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, employee, EmployeeOperations.Delete);
            if (result.Succeeded)
            {
                employee.Activate();

                var validationResult = await _employeeService.UpdateAsync(employee);

                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }

        public async Task<Employee> DeactivateAsync(int employeeId)
        {
            var employee = await _employeeService.GetAsync(employeeId);
            if (employee != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, employee, EmployeeOperations.Update);
                if (result.Succeeded)
                {
                    employee.Deactivate();

                    var validationResult = await _employeeService.UpdateAsync(employee);

                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: "OperationNotAllowed");
                }
            }
            return employee;
        }

        public async Task<EmployeeCertification> GetCertificationsAsync(int id)
        {
            //var employee = await GetAsync(employeeId);
            var empCertifications = await _employeeCertificationService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
             return empCertifications;
        }

        public async Task<EmployeeCertification> GetCertificationsLinkAsync(int certId, int empId)
        {
            var empCertifications = await _employeeCertificationService.FindQuery(x => x.CertificationId == certId && x.EmployeeId == empId, true).FirstOrDefaultAsync();
            empCertifications.IssueDate = empCertifications.IssueDate;
            empCertifications.RenewalDate = empCertifications.RenewalDate == null ? DateOnly.FromDateTime(DateTime.MinValue) : DateOnly.FromDateTime(DateTime.Parse(empCertifications.RenewalDate?.ToString("yyyy-MM-dd")));
            empCertifications.ExpirationDate = DateOnly.FromDateTime(DateTime.Parse(empCertifications.ExpirationDate?.ToString("yyyy-MM-dd")));
            //empCertifications = empCertifications.Where(cert => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, cert, EmployeeCertificationOperations.Read).Result.Succeeded).ToList();
            return empCertifications;
        }

        public async Task<EmployeeCertification> GetEmployeeCertificationFromHistory(int id)
        {
            List<Expression<Func<EmployeeCertifictaionHistory, bool>>> predicates = new List<Expression<Func<EmployeeCertifictaionHistory, bool>>>();
            predicates.Add(x => x.Id == id);
            var empCertification = (await _employeeCertificationHistoryService.FindWithIncludeAsync(predicates, new[] { "Certification", "EmployeeCertification" }, true)).Select(s => new EmployeeCertification
            {
                Id = s.Id,
                IssueDate = s.IssueDate,
                RenewalDate = s.DRADate,
                ExpirationDate = s.ExpirationDate,
                CertificationId = s.EmployeeCertification.CertificationId,
                EmployeeId = s.EmployeeCertification.EmployeeId,
                CertificationNumber = s.CertificationNumber
            }).FirstOrDefault();

            return empCertification;
        }

        public async Task<List<EMPCertificationVM>> GetCertificationsLinkedToEmployee(int id, string filter)
        {
            List<EMPCertificationVM> dataList = new List<EMPCertificationVM>();
            //var empCert = (await _employeeCertificationService.FindWithIncludeAsync(x => x.EmployeeId == id, new[] { "Certification" })).ToList();

            switch (filter)
            {
                case "current":
                    dataList = await _employeeCertificationService.FindQueryWithIncludeAsync(x => x.EmployeeId == id, new[] { "Certification.CertifyingBody" }).Where(x => (!x.ExpirationDate.HasValue || x.ExpirationDate.Value >= DateOnly.FromDateTime(DateTime.Today))).Select(s => new EMPCertificationVM
                    {
                        EmployeeId = s.EmployeeId,
                        CertificationId = s.CertificationId,
                        CertificationNumber = s.CertificationNumber,
                        ExpirationDate = s.ExpirationDate,
                        IsFromHistory = false,
                        IssueDate = s.IssueDate,
                        Name = s.Certification.Name,
                        RenewalDate = s.RenewalDate,
                        RollOverHours = s.RollOverHours,
                        IsExpired = (s.ExpirationDate < DateOnly.FromDateTime(DateTime.Today)),
                        EmpCertificationId = s.Id,
                        IsNERCCertification = s.Certification.CertifyingBody.IsNERC
                    }).ToListAsync();
                    break;
                case "past":
                    dataList = await _employeeCertificationHistoryService.FindQueryWithIncludeAsync(x => x.EmployeeCertificationId == id, new string[] { "EmployeeCertification.Certification.CertifyingBody" }).Select(s => new EMPCertificationVM
                    {
                        CertificationId = s.EmployeeCertification.CertificationId,
                        EmployeeId = s.EmployeeCertification.EmployeeId,
                        IssueDate = s.IssueDate,
                        Name = s.EmployeeCertification.Certification.Name,
                        RenewalDate = s.DRADate,
                        ExpirationDate = s.ExpirationDate,
                        CertificationNumber = s.CertificationNumber,
                        IsFromHistory = true,
                        RollOverHours = 0,
                        EmpCertificationId = s.Id,
                        IsExpired = (s.ExpirationDate < DateOnly.FromDateTime(DateTime.Today))
                    }).ToListAsync();
                    break;
                case "all":
                    dataList = await _employeeCertificationService.FindQueryWithIncludeAsync(x => x.EmployeeId == id, new[] { "Certification.CertifyingBody" }).Select(s => new EMPCertificationVM
                    {
                        EmployeeId = s.EmployeeId,
                        CertificationId = s.CertificationId,
                        CertificationNumber = s.CertificationNumber,
                        ExpirationDate = s.ExpirationDate,
                        IsFromHistory = false,
                        IssueDate = s.IssueDate,
                        Name = s.Certification.Name,
                        RenewalDate = s.RenewalDate,
                        RollOverHours = s.RollOverHours,
                        IsExpired = (!s.ExpirationDate.HasValue || s.ExpirationDate.Value < DateOnly.FromDateTime(DateTime.Today)),
                        EmpCertificationId = s.Id,
                        IsNERCCertification = s.Certification.CertifyingBody.IsNERC
                    }).ToListAsync();
                    break;
            }
            return dataList.OrderBy(o => o.Name).ThenByDescending(x => x.ExpirationDate).ToList();

        }

        public async Task<Employee> AddCertificationAsync(int employeeId, EmployeeCertificateCreateOptions options)
        {
            var certification = await _certificationService.GetAsync(options.CertificationId);
            var certifyingBody = await _certifyingBodiesService.GetAsync(options.CertifyingBodyId);
            var employee = await _employeeService.GetWithIncludeAsync(employeeId, new string[] { nameof(_employee.EmployeeCertifications) });

            //foreach (var certificationId in options.CertificationIds)
            //{
            //var certification = await _certificationService.GetAsync(options.CertificationId);
            var empCertification = _certifyingBody.Certify(employee, options.CertificationId, options.IssueDate, options.ExpirationDate, options.RenewalDate, null, options.CertificationNumber);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, empCertification, EmployeeCertificationOperations.Create);

            if (result.Succeeded)
            {
                empCertification.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                empCertification.CreatedDate = DateTime.Now;
                empCertification.SetExpirationDate(empCertification.ExpirationDate);
                employee.AddCertification(certification, empCertification);
                var validationResult = await _employeeService.UpdateAsync(employee); // _employeeCertificationService.Add(empCertification);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return employee;
                }

            }
            //}
            return employee;
        }

        public async Task<EmployeeCertification> EditCertificationAsync(int id, EmployeeCertificateUpdateOptions options)
        {

            var empCertification = (await _employeeCertificationService.FindAsync(x => x.Id==id)).FirstOrDefault();

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, empCertification, EmployeeCertificationOperations.Update);
            if (result.Succeeded)
            {
                // Todo EmpCertification Update
                //empCertification.CertificationId = options.CertificationId;
                empCertification.CertificationNumber = options.CertificationNumber;
                empCertification.SetExpirationDate(options.ExpirationDate);
                empCertification.IssueDate = options.IssueDate;
                empCertification.RenewalDate = options.RenewalDate;
                empCertification.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                empCertification.ModifiedDate = DateTime.Now;
                var validationResult = await _employeeCertificationService.UpdateAsync(empCertification);
                if (validationResult.IsValid)
                {
                    return empCertification;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }

        public async System.Threading.Tasks.Task DeleteCertificationAsync(int id)
        {
            var empCertification = (await _employeeCertificationService.FindAsync(x => x.Id==id)).FirstOrDefault();

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, empCertification, EmployeeCertificationOperations.Update);
            if (result.Succeeded)
            {
                empCertification.Delete();
                //var employee = empCertification.Employee;
                ////var certification = empCertification.Certification;
                //employee.Delete(empCertification);
                //employee.DeleteEmpCertification(empCertification);
                var validationResult = await _employeeCertificationService.UpdateAsync(empCertification); // _employeeCertificationService.Update(empCertification);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }

        public async Task<List<EmployeePosition>> GetPositionsAsync(int employeeId, string filter)
        {
            var positions = await _employeePositionService.FindQueryWithIncludeAsync(x => x.EmployeeId == employeeId , new string[] { "Position", "Employee" }, true).ToListAsync();
            if (positions != null && positions.Count() > 0)
            {
                switch (filter)

                {
                    case "current":
                        positions = positions.Where(x => (x.EndDate >= DateOnly.FromDateTime(DateTime.Now) || !x.EndDate.HasValue)).ToList();
                        break;
                    case "past":
                        positions = positions.Where(x => x.EndDate < DateOnly.FromDateTime(DateTime.Now)).ToList();
                        break;
                    case "all":
                        positions = positions.ToList();
                        break;
                }
            }

            positions = positions.Where(position => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, position, EmployeePositionOperations.Read).Result.Succeeded).OrderBy(x => x.StartDate).ToList();
            return positions.OrderByDescending(x => x.StartDate).ToList();
        }

        public async Task<EmployeePosition> AddPositionAsync(int employeeId, EmployeePositionCreateOptions options)
        {
            var employee = await _employeeService.FindQuery(x => x.Id == employeeId).FirstOrDefaultAsync();
            employee.EmployeePositions = await _employeePositionService.FindQuery(x => x.EmployeeId == employeeId, true).ToListAsync();

            var position = await _posDomainService.FindQuery(x => x.Id == options.PositionId).FirstOrDefaultAsync();

            var empPosition = employee.AddPosition(position, employee.Id, options.StartDate, options.posQualificationDate, options.endDate, options.IsTrainee, options.ManagerName, options.TrainingProgramVersion, options.IsSignificant, options.IsCertificationRequired);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, empPosition, EmployeePositionOperations.Create);
            if (result.Succeeded)
            {
                empPosition.SetEndDate(empPosition.EndDate);
                //empPosition.Trainee = true;
                empPosition.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                empPosition.CreatedDate = DateTime.Now;
                var validationResult = await _employeeService.UpdateAsync(employee);
                if (validationResult.IsValid)
                {
                    return empPosition;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new System.UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }

        public async Task<EmployeePosition> EditPositionAsync(int employeeId, int positionId, EmployeePositionUpdateOptions options)
        {
            var employee = await GetAsync(employeeId);
            var position = await _posDomainService.FindQuery(x => x.Id == positionId).FirstOrDefaultAsync();
            var empPosition = (await _employeePositionService.FindAsync(x => x.EmployeeId == employee.Id && x.PositionId == position.Id && options.EmployeePositionId==x.Id)).FirstOrDefault();

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, empPosition, EmployeePositionOperations.Update);

            if (result.Succeeded)
            {
                empPosition.StartDate = options.StartDate;
                empPosition.Trainee = options.Trainee;
                empPosition.SetEndDate(options.EndDate);
                if (options.QualificationDate.HasValue)
                {
                    empPosition.SetAsQualified(options.QualificationDate.Value);
                }
                else
                {
                    empPosition.QualificationDate = options.QualificationDate;
                }
                empPosition.ManagerName = options.ManagerName;
                empPosition.TrainingProgramVersion = options.TrainingProgramVersion;
                empPosition.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                empPosition.ModifiedDate = DateTime.Now;
                empPosition.IsCertificationNotRequired = options.IsCertificationRequired;
                empPosition.IsSignificant = options.IsSignificant;
                var validationResult = await _employeePositionService.UpdateAsync(empPosition);
                if (validationResult.IsValid)
                {
                    return empPosition;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }

        public async Task<EmployeePosition> DeletePositionAsync(int employeeId, int positionId, int empPosId)
        {
            //var employee = await _employeeService.FindQuery(x => x.Id == employeeId).FirstOrDefaultAsync();
            //var position = await _positionService.GetAsync(positionId);
            var empPosition = await _employeePositionService.FindQuery(x => x.EmployeeId == employeeId && x.PositionId == positionId && x.Id==empPosId).FirstOrDefaultAsync();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, empPosition, EmployeePositionOperations.Delete);

            if (result.Succeeded)
            {
                // Todo EmpPosition Delete Logic
                empPosition.Delete();
                var validationResult = await _employeePositionService.UpdateAsync(empPosition);
                if (validationResult.IsValid)
                {
                    empPosition.Position = await _posDomainService.FindQueryWithIncludeAsync(x => x.Id == empPosition.PositionId, new string[] { "Position_Tasks" }).FirstOrDefaultAsync();
                    return empPosition;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new System.UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }

        public async Task<List<EmployeeOrganization>> GetOrganizationsAsync(int employeeId)
        {
            var employeeOrganizations = await _employeeOrganizationService.FindQueryWithIncludeAsync(x => x.EmployeeId == employeeId, new string[] { "Organization" }, true).ToListAsync();
            employeeOrganizations = employeeOrganizations.Where(org => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, org, EmployeeOrganizationOperations.Read).Result.Succeeded).ToList();
            return employeeOrganizations.OrderBy(x => x.Organization.Name).ToList();
        }

        //public async Task<EmployeeOrganization> AddOrganizationAsync(int employeeId, EmployeeOrganizationCreateOptions options)
        //{
        //    var employee = await GetAsync(employeeId);
        //    var organization = await _organizationService.GetAsync(options.OrganizationId);
        //    if (organization == null)
        //    {
        //        throw new BadHttpRequestException(message: "OrganizationNotFound");
        //    }

        //    var empOrg = employee.JoinOrganization(organization, employee.Id, options.IsManager);

        //    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, empOrg, EmployeeOrganizationOperations.Create);
        //    if (result.Succeeded)
        //    {
        //        empOrg.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
        //        empOrg.CreatedDate = DateTime.Now;
        //        var validationResult = await _employeeService.UpdateAsync(employee); // _employeeOrganizationService.Add(empOrg);
        //        if (validationResult.IsValid)
        //        {
        //            return empOrg;
        //        }
        //        else
        //        {
        //            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
        //        }
        //    }
        //    else
        //    {
        //        throw new UnauthorizedAccessException(message: "OperationNotAllowed");
        //    }
        //}

        public async Task<EmployeeOrganization> EditOrganizationAsync(int employeeId, int organizationId, EmployeeOrganizationUpdateOptions options)
        {
            var employee = await GetAsync(employeeId);
            var organization = await _organizationService.GetAsync(organizationId);
            var empOrg = (await _employeeOrganizationService.FindAsync(x => x.EmployeeId == employee.Id && x.OrganizationId == organization.Id)).FirstOrDefault();

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, empOrg, EmployeeOrganizationOperations.Update);
            if (result.Succeeded)
            {
                // Todo Update Logic
                empOrg.IsManager = options.IsManager;
                empOrg.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                empOrg.ModifiedDate = DateTime.Now;
                var validationResult = await _employeeOrganizationService.UpdateAsync(empOrg);
                if (validationResult.IsValid)
                {
                    return empOrg;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }

        public async System.Threading.Tasks.Task DeleteOrganizationAsync(int employeeId, int organizationId)
        {
            var employee = await _employeeService.FindQueryWithIncludeAsync(x => x.Id == employeeId, new string[] { "EmployeeOrganizations" }).FirstOrDefaultAsync();
            var organization = await _organizationService.GetAsync(organizationId);
            //var empOrg = (await _employeeOrganizationService.FindAsync(x => x.EmployeeId == employee.Id && x.OrganizationId == organization.Id)).FirstOrDefault();

            //var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, empOrg, AuthorizationOperations.Read);
            //if (result.Succeeded)
            //{
            // Todo Deleted Logic
            employee.LeaveOrganization(organization, employeeId);
            var validationResult = await _employeeService.UpdateAsync(employee); // _employeeOrganizationService.Delete(empOrg);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            //}
            //else
            //{
            //    throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            //}
        }

        public async Task<Employee> LinkOrganization(int empId, EmployeeOrganizationCreateOptions options)
        {
            var employee = await _employeeService.GetWithIncludeAsync(empId, new string[] { nameof(_employee.EmployeeOrganizations) });
            foreach (var id in options.OrganizationIds)
            {
                var org = await _organizationService.GetAsync(id);

                var employeeResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, employee, EmployeeOperations.Update);
                var orgResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, org, EmployeeOrganizationOperations.Read);
                if (employeeResult.Succeeded && orgResult.Succeeded)
                {
                    employee.LinkOrganization(org);
                    var validationResult = await _employeeService.UpdateAsync(employee);
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

            return employee;
        }
        public async Task<List<EmployeeOrganization>> GetOrganizationssEmployeeIsLinkedTo(int id)
        {
            //var data = await _employeeOrganizationService.AllQueryWithInclude(new string[] { nameof(_employeeOrganization.Organization) }).Where(x => x.EmployeeId == id).Select(x => x.Organization).ToListAsync();
            //return data;

            var empOrg = await _employeeOrganizationService.FindQueryWithIncludeAsync(x => x.OrganizationId == id, new string[] { "Organization" }, true).ToListAsync();
            return empOrg;
        }

        public async System.Threading.Tasks.Task ToggleIsManagerAsync(int employeeId, EmployeeOrganizationCreateOptions options)
        {

            var empOrg = (await _employeeOrganizationService.FindAsync(x => x.EmployeeId == employeeId && x.OrganizationId == options.OrganizationId)).FirstOrDefault();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, empOrg, EmployeeOrganizationOperations.Update);
            if (result.Succeeded)
            {
                // Todo Deleted Logic
                empOrg.IsManager = options.IsManager;
                var validationResult = await _employeeOrganizationService.UpdateAsync(empOrg); // _employeeOrganizationService.Delete(empOrg);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }

        public async System.Threading.Tasks.Task UploadEmployeeFileAsync(int id, EmployeeDocumentOptions file)
        {
            var obj = await _employeeService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);
            if (result.Succeeded)
            {
                foreach (var data in file.uploadFiles)
                {
                    obj.EmployeeDocuments.Add(new EmployeeDocument
                    {
                        FileAsBase64 = data.FileAsBase64,
                        FileName = data.FileName,
                        FileType = data.FileType,
                        EmployeeID = id,
                    });
                }

                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                await _employeeService.UpdateAsync(obj);
            }
        }

        public async Task<EmployeeDocument> DownloadFile(int id, int fileId)
        {
            var obj = _employeeDocumentService.FindQuery(x => x.EmployeeID == id && x.Id == fileId).Select(x => new { x.FileAsBase64, x.FileName });
            var uploads = new List<EmployeeDocument>();
            int count = 0;
            foreach (var data in obj)
            {
                uploads.Add(new EmployeeDocument
                {
                    FileName = data.FileName,
                    FileAsBase64 = data.FileAsBase64,
                });
                count++;
            }



            return uploads[0];
        }

        public async Task<List<Employee>> GetAllEvaluatorsAsync()
        {
            var evals = await _employeeService.FindQueryWithIncludeAsync(x => x.TQEqulator, new string[] { nameof(_employee.Person) }, true).ToListAsync();

            return evals;
        }
        public async Task<List<EmployeeNameOnlyVM>> GetAllEvaluatorsNamesAsync()
        {
            var employeeData = new List<EmployeeNameOnlyVM>();
            var employeeList = await _employeeService.FindQuery(x => x.Active == true && x.TQEqulator == true).Select(x => x.Id).ToListAsync();
            foreach (var employeeId in employeeList)
            {
                var personData = await _personService.FindQuery(x => x.Id == employeeId)?.Select(x => new EmployeeNameOnlyVM
                {
                    firstName = x.FirstName,
                    lastName = x.LastName,
                }).FirstOrDefaultAsync();
                if (personData != null)
                {
                    personData.EmpId = employeeId;
                    employeeData.Add(personData);
                }
            }
            return employeeData;
        }

        public async Task<List<Employee>> GetOnlyEmployeesAsync()
        {
            var employees = await _employeeService.FindQueryWithIncludeAsync(x => !x.TQEqulator, new string[] { "Person" }).ToListAsync();

            employees = employees.Where(x => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, x, EmployeeOperations.Read).Result.Succeeded).ToList();

            return employees;
        }

        public async Task<List<EmployeeDocument>> getUploadedFiles(int id)
        {
            var obj = _employeeDocumentService.FindQuery(x => x.EmployeeID == id).Select(x => new { x.EmployeeID, x.FileName, x.FileSize, x.Id, x.FileType });
            var uploads = new List<EmployeeDocument>();
            int count = 0;
            foreach (var data in obj)
            {
                uploads.Add(new EmployeeDocument
                {
                    FileName = data.FileName,
                    EmployeeID = data.EmployeeID,
                    FileSize = data.FileSize,
                    FileType = data.FileType,
                });
                uploads[count].Set_Id(data.Id);
                count++;
            }

            return uploads;
        }
        public async Task<EmployeePosition> GetPositionsByPositionaAndEmployeeIdAsync(int employeeId, int positionId, int empPosId)
        {
            var employee = await GetAsync(employeeId);
            var positions = await _employeePositionService.FindQueryWithIncludeAsync(x => x.EmployeeId == employee.Id && x.PositionId == positionId && x.Id==empPosId, new string[] { "Position" }).FirstOrDefaultAsync();
            positions.StartDate =positions.StartDate;
            //positions = positions.Where(position => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, position, EmployeePositionOperations.Read).Result.Succeeded).FirstOrDefault();
            return positions;
        }

        public async Task<EmployeeCertification> RenewCertificationAsync(int id, EmployeeCertificateUpdateOptions options)
        {

            var empCertification = (await _employeeCertificationService.FindAsync(x => x.Id==id)).FirstOrDefault();

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, empCertification, EmployeeCertificationOperations.Update);
            if (result.Succeeded)
            {
                // Todo EmpCertification Update
                //empCertification.CertificationId = options.CertificationId;
                empCertification.RollOverHours = options.RolloverHours;
                empCertification.SetExpirationDate(options.ExpirationDate);
                empCertification.IssueDate = options.IssueDate;
                empCertification.RenewalDate = options.RenewalDate;
                empCertification.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                empCertification.ModifiedDate = DateTime.Now;
                var validationResult = await _employeeCertificationService.UpdateAsync(empCertification);
                if (validationResult.IsValid)
                {
                    return empCertification;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }
        //public async Task<Employee> CertificationRequired(int employeeId, bool certRequired)
        //{
        //    var employee = await GetAsync(employeeId);
        //    employee.IsCertificationRequired = certRequired;
        //    await _employeeService.UpdateAsync(employee);
        //    return employee;
        //}

        public async Task<List<Employee>> GetAllEmpWithPosAndOrgIdsOnlyAsync()
        {
            System.Linq.Expressions.Expression<Func<EmployeePosition, bool>> empPositionPredicate = x => x.Active && x.Position.Active;
            System.Linq.Expressions.Expression<Func<EmployeeOrganization, bool>> empOrgPredicate = x => x.Active && x.Organization.Active;
            //System.Linq.Expressions.Expression<Func<TaskQualification, bool>> tqPredicate = x => x.Active;
            //System.Linq.Expressions.Expression<Func<ClassSchedule_Employee, bool>> csEmpPredicate = x => x.Active;

            var EmpPossitions = await _employeePositionService.GetEMPPositionsIdsOnly(empPositionPredicate);
            var EmpOrganisations = await _employeeOrganizationService.GetEMPOrgIdsOnly(empOrgPredicate);
            //var TaskQualifications = await _taskQualService.GetCompactTQsWithConditionAndIncludes(tqPredicate, new string[] { });
            //var CSEmployees = await _classScheduleService.GetCSEmpsWithConditionAndIncludes(csEmpPredicate, new string[] { });
            //var Organizations = await _orgDomainService.AllActiveOrganiaztions().ToListAsync();
            //var Possitions = await _posDomainService.GetAllActiveCompactPositions();

            var employees = await _employeeService.GetAllActiveEmployeesWithCompactPersons();
            for (int i = 0; i < employees.Count; i++)
            {
                //if (employees[i].Person.Image != null)
                //{
                //    var imgHeader = Regex.Match(employees[i].Person.Image, @"^data:image/[^;]+;base64,").Value;
                //    employees[i].Person.Image = Regex.Replace(employees[i].Person.Image, @"^data:image/[^;]+;base64,", "");

                //    byte[] imageData = Convert.FromBase64String(employees[i].Person.Image);
                //    byte[] compressedData = CompressImageData(imageData);
                //    employees[i].Person.Image = Convert.ToBase64String(compressedData);
                //    employees[i].Person.Image = imgHeader + employees[i].Person.Image;
                //}
                //employees[i].Person = Persons.Where(x => x.Id == employees[i].PersonId).Select(s => new Person
                //{
                //    Id = s.Id,
                //    Active = s.Active,
                //    Deleted = s.Deleted,
                //    Image = s.Image,
                //    FirstName = s.FirstName,
                //    LastName = s.LastName,
                //    MiddleName = s.MiddleName,
                //    Username = s.Username
                //}).FirstOrDefault();
                employees[i].EmployeePositions = EmpPossitions.Where(x => x.EmployeeId == employees[i].Id).ToList();
                employees[i].EmployeeOrganizations = EmpOrganisations.Where(x => x.EmployeeId == employees[i].Id).ToList();
                //employees[i].TaskQualifications = TaskQualifications.Where(x => x.EmpId == employees[i].Id).Select(s => new TaskQualification { Id = s.Id }).ToList();
                //employees[i].ClassSchedule_Employee = CSEmployees.Where(x => x.EmployeeId == employees[i].Id).Select(s => new ClassSchedule_Employee { Id = s.Id }).ToList();
                //for (int j = 0; j < employees[i].EmployeePositions.Count; j++)
                //{
                //    employees[i].EmployeePositions.ToList()[j].Position = Possitions.Where(x => x.Id == employees[i].EmployeePositions.ToList()[j].PositionId).Select(s => new Position
                //    {
                //        Id = s.Id,
                //        Active = s.Active,
                //        PositionAbbreviation = s.PositionAbbreviation,
                //        HyperLink = s.HyperLink,
                //        IsPublished = s.IsPublished,
                //        PositionDescription = s.PositionDescription,
                //        PositionNumber = s.PositionNumber,
                //        PositionTitle = s.PositionTitle,
                //        FileName = s.FileName,
                //        EffectiveDate = s.EffectiveDate
                //    }).FirstOrDefault();
                //}
                //for (int j = 0; j < employees[i].EmployeeOrganizations.Count; j++)
                //{
                //    employees[i].EmployeeOrganizations.ToList()[j].Organization = Organizations.Where(x => x.Id == employees[i].EmployeeOrganizations.ToList()[j].OrganizationId).FirstOrDefault();
                //}
            }
            employees = employees.Where(employee => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, employee, EmployeeOperations.Read).Result.Succeeded).ToList();
            return employees.OrderBy(x => x?.Person?.FirstName).ToList();
        }

        public async Task<List<Employee>> GetAllEmpWithPosAndOrgAsync()
        {
            //// HERE TO SEARCH
            System.Linq.Expressions.Expression<Func<EmployeePosition, bool>> empPositionPredicate = x => x.Active && x.Position.Active;
            System.Linq.Expressions.Expression<Func<EmployeeOrganization, bool>> empOrgPredicate = x => x.Active && x.Organization.Active;
            //System.Linq.Expressions.Expression<Func<TaskQualification, bool>> tqPredicate = x => x.Active;
            //System.Linq.Expressions.Expression<Func<ClassSchedule_Employee, bool>> csEmpPredicate = x => x.Active;

            var EmpPossitions = await _employeePositionService.GetEmpPositionsWithCompactPositionsAndConditions(empPositionPredicate);
            var EmpOrganisations = await _employeeOrganizationService.AllActiveEmpOrganizationsWithOrganizationAndConditions(empOrgPredicate);
            //var TaskQualifications = await _taskQualService.GetCompactTQsWithConditionAndIncludes(tqPredicate, new string[] { });
            //var CSEmployees = await _classScheduleService.GetCSEmpsWithConditionAndIncludes(csEmpPredicate, new string[] { });
            //var Organizations = await _orgDomainService.AllActiveOrganiaztions().ToListAsync();
            //var Possitions = await _posDomainService.GetAllActiveCompactPositions();

            var employees = await _employeeService.GetAllActiveEmployeesWithCompactPersons();
            for (int i = 0; i < employees.Count; i++)
            {
                //if (employees[i].Person.Image != null)
                //{
                //    var imgHeader = Regex.Match(employees[i].Person.Image, @"^data:image/[^;]+;base64,").Value;
                //    employees[i].Person.Image = Regex.Replace(employees[i].Person.Image, @"^data:image/[^;]+;base64,", "");

                //    byte[] imageData = Convert.FromBase64String(employees[i].Person.Image);
                //    byte[] compressedData = CompressImageData(imageData);
                //    employees[i].Person.Image = Convert.ToBase64String(compressedData);
                //    employees[i].Person.Image = imgHeader + employees[i].Person.Image;
                //}
                //employees[i].Person = Persons.Where(x => x.Id == employees[i].PersonId).Select(s => new Person
                //{
                //    Id = s.Id,
                //    Active = s.Active,
                //    Deleted = s.Deleted,
                //    Image = s.Image,
                //    FirstName = s.FirstName,
                //    LastName = s.LastName,
                //    MiddleName = s.MiddleName,
                //    Username = s.Username
                //}).FirstOrDefault();
                employees[i].EmployeePositions = EmpPossitions.Where(x => x.EmployeeId == employees[i].Id).ToList();
                employees[i].EmployeeOrganizations = EmpOrganisations.Where(x => x.EmployeeId == employees[i].Id).ToList();
                //employees[i].TaskQualifications = TaskQualifications.Where(x => x.EmpId == employees[i].Id).Select(s => new TaskQualification { Id = s.Id }).ToList();
                //employees[i].ClassSchedule_Employee = CSEmployees.Where(x => x.EmployeeId == employees[i].Id).Select(s => new ClassSchedule_Employee { Id = s.Id }).ToList();
                //for (int j = 0; j < employees[i].EmployeePositions.Count; j++)
                //{
                //    employees[i].EmployeePositions.ToList()[j].Position = Possitions.Where(x => x.Id == employees[i].EmployeePositions.ToList()[j].PositionId).Select(s => new Position
                //    {
                //        Id = s.Id,
                //        Active = s.Active,
                //        PositionAbbreviation = s.PositionAbbreviation,
                //        HyperLink = s.HyperLink,
                //        IsPublished = s.IsPublished,
                //        PositionDescription = s.PositionDescription,
                //        PositionNumber = s.PositionNumber,
                //        PositionTitle = s.PositionTitle,
                //        FileName = s.FileName,
                //        EffectiveDate = s.EffectiveDate
                //    }).FirstOrDefault();
                //}
                //for (int j = 0; j < employees[i].EmployeeOrganizations.Count; j++)
                //{
                //    employees[i].EmployeeOrganizations.ToList()[j].Organization = Organizations.Where(x => x.Id == employees[i].EmployeeOrganizations.ToList()[j].OrganizationId).FirstOrDefault();
                //}
            }
            employees = employees.Where(employee => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, employee, EmployeeOperations.Read).Result.Succeeded).ToList();
            return employees.OrderBy(x => x?.Person?.FirstName).ToList();
        }

        public async Task<EmployeeWithPositionVM> GetEmployeeWithPositionAsync(int id)
        {
            EmployeeWithPositionVM empWithPos = new EmployeeWithPositionVM();
            var employee = await _employeeService.FindQueryWithIncludeAsync(x => x.Id == id, new string[] { nameof(_employee.Person) }).FirstOrDefaultAsync();
            empWithPos.Employee = employee;

            var position = await _employeePositionService.FindQueryWithIncludeAsync(x => x.EmployeeId == id && (x.EndDate.HasValue == false || x.EndDate >= DateOnly.FromDateTime(DateTime.Now)), new string[] { nameof(_employeePosition.Position) }).OrderByDescending(o => o.PositionId).Select(s => s.Position).FirstOrDefaultAsync();
            empWithPos.Position = position;
            return empWithPos;
        }

        public async Task<Employee> GetEmployeeByUsernameAsync(string username)
        {
            return await _employeeService.GetEmployeeByUsernameAsync(username);
        }


        public async Task<object> GetExpiredCertificates()
        {
            var expiredCertificatesEmployee = await _employeeCertificationService.GetExpiredCertificates();
            return expiredCertificatesEmployee;
        }
        public async Task<string> GetEmpImageAsync(string name)
        {
            var person = await _personService.FindQuery(x => x.Username == name).FirstOrDefaultAsync();
            if (person != null)
            {
                if (!string.IsNullOrEmpty(person.Image))
                {
                    person.Image = person.Image.CompressImage();
                    await _personService.UpdateAsync(person);
                }
                return person.Image;
            }
            else
            {
                return "";
            }
        }

        public async Task<string> GetEmpUserName()
        {
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

            var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();

            if (person != null)
            {
                return person.FirstName;
            }
            else
            {
                return "";
            }
        }

        public async Task<List<Employee>> GetEmployeesList()
        {
            var employeeList = await _employeeService.FindQuery(x => x.Active == true).ToListAsync();
            foreach (var employee in employeeList)
            {
                employee.Person = await _personService.FindQuery(x => x.Id == employee.PersonId).FirstOrDefaultAsync();
            }
            return employeeList.ToList();
        }
        public async Task<List<EmployeeNameOnlyVM>> GetEmployeesListNamesOnly()
        {
            var employeeData = new List<EmployeeNameOnlyVM>();
            var employeeList = await _employeeService.FindQuery(x => x.Active == true).ToListAsync();
            foreach (var employee in employeeList)
            {
                var personData = await _personService.FindQuery(x => x.Id == employee.PersonId && x.Active == true)?.Select(x => new EmployeeNameOnlyVM
                {
                    firstName = x.FirstName,
                    lastName = x.LastName,
                }).FirstOrDefaultAsync();
                if (personData != null)
                {
                    personData.EmpId = employee.Id;
                    employeeData.Add(personData);
                }
            }
            return employeeData.OrderBy(x => x.firstName).ThenBy(x => x.lastName).ToList();
        }

        public async Task<List<EmployeeListDTO>> GetEmployeesListWithOrgAndPosAsync()
        {
            var employeeList = await _employeeDomainService.GetEmployeesListWithOrgAndPosAsync();
            var list = employeeList.Select(s => new EmployeeListDTO
            {
                FirstName = s.Person.FirstName,
                LastName = s.Person.LastName,
                Id = s.Id,
                EmployeePositions = s.EmployeePositions.Where(ep => ep.Position.Active && ep.Active).Select(ep => new EmployeePositionSummaryDTO
                {
                    Id = ep.Position.Id,
                    PositionTitle = ep.Position.PositionTitle
                }),
                EmployeeOrganizations = s.EmployeeOrganizations.Where(eo => eo.Organization.Active && eo.Active).Select(eo => new EmployeeOrganizationSummaryDTO
                {
                    Id = eo.Organization.Id,
                    IsManager = eo.IsManager,
                    Name = eo.Organization.Name
                })
            }).ToList();
            return list;
        }

        public async Task<List<EmployeeListDTO>> GetEmployeeListAsync()
        {

            var query = _employeeService.FindQueryWithIncludeAsync(x => x.Person != null, new string[] { "EmployeePositions", "EmployeeOrganizations" }, false);

            var list = await query.Select(s => new EmployeeListDTO
            {
                Active = s.Active,
                EmployeeNumber = s.EmployeeNumber,
                FirstName = s.Person.FirstName,
                LastName = s.Person.LastName,
                Id = s.Id,
                Image = s.Person.Image,
                UserName = s.Person.Username,
                TQEqulator = s.TQEqulator,
                TaskQualifications = s.TaskQualifications.Select(s => new EmployeeTaskQualificationSummaryDTO
                {
                    Id = s.Task.Id,
                    Name = s.Task.Abreviation

                }),
                ClassSchedule_Employee = s.ClassSchedule_Employee.Select(s => s.Id).Count(),

                EmployeePositions = s.EmployeePositions.Where(eo => eo.Position.Active && (!eo.EndDate.HasValue || eo.EndDate >= DateOnly.FromDateTime(DateTime.Now))).Select(eo => new EmployeePositionSummaryDTO
                {
                    Id = eo.Id,
                    PositionTitle = eo.Position.PositionTitle
                }),
                EmployeeOrganizations = s.EmployeeOrganizations.Where(eo => eo.Organization.Active).Select(eo => new EmployeeOrganizationSummaryDTO
                {
                    Id = eo.Id,
                    IsManager = eo.IsManager,
                    Name = eo.Organization.Name
                })
            }).OrderBy(ob => ob.FirstName).ToListAsync();
            return list;
        }

        public async System.Threading.Tasks.Task SaveIdpReviewAsync(int id, IDPReviewUpdateOptions options)
        {
            var employee = await _employeeService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (employee != null)
            {
                employee.IDPReviewInformation = options.IDPReviewInformation;
                var validationResult = await _employeeService.UpdateAsync(employee);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["Employee Not Found"]);
            }
        }

        public async Task<Employee> GetEmployeeByNumberAsync(string employeeNumber)
        {
            var employee = await _employeeDomainService.FindAsync(x => !string.IsNullOrEmpty(x.EmployeeNumber) && x.EmployeeNumber == employeeNumber);
            return employee.FirstOrDefault();
        }
       
        public async Task<bool> GetEmployeeActiveStatusByEmpId(int empId)
        {
            var employee = await _employeeDomainService.GetEmployeeById(empId);
            return employee.Active;
        }

        public async Task<Employee> GetEmployeeByClassScheduleEmployeeAsync(int cseId)
        {
            var employee = await _employeeDomainService.GetEmployeeByClassScheduleEmployeeIdAsync(cseId);
            return employee;
        }

        public async Task<List<EmployeeCertification>> GetCertificationsByEmpIdAsync(int empId)
        {
            var employeeCertifications = await _employeeCertificationService.GetEmployeeCertificationsByEmployeeId(empId);
            return employeeCertifications.ToList();
        }
    }
}

