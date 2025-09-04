using Microsoft.EntityFrameworkCore;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Services.Core;
using QTD2.Infrastructure.Model.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.Application.Services.Summary
{
    public class EmployeeSummaryService : IEmployeeSummaryService
    {
        private IEmployeeRepository _employeeRepository;
        private IEmployeeCertificationRepository _employeeCertificationRepository;
        
        public EmployeeSummaryService(IEmployeeRepository employeeRepository, IEmployeeCertificationRepository employeeCertificationRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeCertificationRepository = employeeCertificationRepository;
        }
        public async Task<List<EmployeeSummaryDTO>> GetEmployeeLists(string name)
        {
            var employeeList = new List<EmployeeSummaryDTO>();

            switch (name.ToLower().Trim())
            {
                case "all":
                    {
                        var list = await _employeeRepository.FindQueryAsync(e => !e.Deleted, false);
                        employeeList = await list.Select(s => new EmployeeSummaryDTO
                        {
                            Id = s.Id,
                            EmployeeNumber = s.EmployeeNumber,
                            Image= s.Person.Image,
                            FirstName = s.Person.FirstName,
                            LastName = s.Person.LastName,
                            UserName = s.Person.Username,
                            Active= s.Active,
                        }).OrderBy(em => em.FirstName).ToListAsync();
                        break;
                    }
                case "active":
                    {

                        var list = await _employeeRepository.FindQueryAsync(e => e.Active, false);
                        employeeList = await list.Select(s => new EmployeeSummaryDTO
                        {
                            Id = s.Id,
                            EmployeeNumber = s.EmployeeNumber,
                            Image = s.Person.Image,
                            FirstName = s.Person.FirstName,
                            LastName = s.Person.LastName,
                            UserName = s.Person.Username,
                            Active = s.Active,
                        }).OrderBy(em => em.FirstName).ToListAsync();
                        break;
                    }

                case "inactive":
                    {
                        var list = await _employeeRepository.FindQueryAsync(e => !e.Active, false);
                        employeeList = await list.Select(s => new EmployeeSummaryDTO
                        {
                            Id = s.Id,
                            EmployeeNumber = s.EmployeeNumber,
                            Image = s.Person.Image,
                            FirstName = s.Person.FirstName,
                            LastName = s.Person.LastName,
                            UserName = s.Person.Username,
                            Active = s.Active,
                        }).OrderBy(em => em.FirstName).ToListAsync();
                        break;
                    }

                case "trainee":
                    {
                        var list = await _employeeRepository.FindQueryAsync(e => e.Active && e.EmployeePositions.Any(x=>x.Trainee), false);
                        employeeList = await list.Select(s => new EmployeeSummaryDTO
                        {
                            Id = s.Id,
                            EmployeeNumber = s.EmployeeNumber,
                            Image = s.Person.Image,
                            FirstName = s.Person.FirstName,
                            LastName = s.Person.LastName,
                            UserName = s.Person.Username,
                            Active = s.Active,
                        }).OrderBy(em=>em.FirstName).ToListAsync();
                        break;
                    }

                case "expiredcertificates":
                    {
                        var list = await _employeeCertificationRepository.FindQueryWithIncludeAsync(ec=>ec.ExpirationDate< DateOnly.FromDateTime(DateTime.Now) && ec.Employee.Active, new string[]{"Employee" }, false);
                        employeeList = await list.Distinct().Select(s => new EmployeeSummaryDTO
                        {
                            Id = s.EmployeeId,
                            EmployeeNumber = s.Employee.EmployeeNumber,
                            Image = s.Employee.Person.Image,
                            FirstName = s.Employee.Person.FirstName,
                            LastName = s.Employee.Person.LastName,
                            UserName = s.Employee.Person.Username,
                            Active = s.Active,
                        }).Distinct().OrderBy(e=>e.FirstName).ToListAsync();
                        break;
                    }
                case "nocertificates":
                    {
                        var list = await _employeeRepository.FindQueryWithIncludeAsync(e => e.Active && e.EmployeeCertifications.Count==0, new string[] { "EmployeeCertifications" }, false);
                        employeeList = await list.Select(s => new EmployeeSummaryDTO
                        {
                            Id = s.Id,
                            EmployeeNumber = s.EmployeeNumber,
                            Image = s.Person.Image,
                            FirstName = s.Person.FirstName,
                            LastName = s.Person.LastName,
                            UserName = s.Person.Username,
                            Active = s.Active,
                        }).OrderBy(em => em.FirstName).ToListAsync();
                        break;
                    }

            }
            return employeeList;
        }

        public async Task<EmpDashboardStatistics> GetEmpDashboardStatisticsAsync()
        {
            //var trainees = await _employeeService.GetTotalEmployeeTrainees();
            var trainees = await _employeeRepository.FindQueryAsync(e => e.Active && e.EmployeePositions.Any(x => x.Trainee), false).GetAwaiter().GetResult().CountAsync();
            var expiredCertificatesEmployee = await _employeeCertificationRepository.FindQueryWithIncludeAsync(ec => ec.ExpirationDate < DateOnly.FromDateTime(DateTime.Now) && ec.Employee.Active, new string[] { "Employee" }, false).GetAwaiter().GetResult().Select(x=>x.Employee).Distinct().CountAsync();
            var noCertificateEmployees =  await _employeeRepository.FindQueryWithIncludeAsync(e => e.Active && e.EmployeeCertifications.Count == 0, new string[] { "EmployeeCertifications" }, false).GetAwaiter().GetResult().CountAsync();
          
            var dashboardStats = new EmpDashboardStatistics(trainees, noCertificateEmployees, expiredCertificatesEmployee, 0);
            return dashboardStats;
        }
    }
}
