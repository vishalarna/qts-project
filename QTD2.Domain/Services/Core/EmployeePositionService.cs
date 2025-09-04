using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace QTD2.Domain.Services.Core
{
    public class EmployeePositionService : Common.Service<EmployeePosition>, IEmployeePositionService
    {
        public EmployeePositionService(IEmployeePositionRepository employeePositionRepository, IEmployeePositionValidation employeePositionValidation)
            : base(employeePositionRepository, employeePositionValidation)
        {
        }

        public async System.Threading.Tasks.Task<List<EmployeePosition>> GetEmployeesPositionsAsync(string active, List<int> employees)
        {
            List<Expression<Func<EmployeePosition, bool>>> predicates = new List<Expression<Func<EmployeePosition, bool>>>();

            if (!string.IsNullOrEmpty(active) && active == "Active Only")
                predicates.Add(r => r.Employee.Active);

            else if (!string.IsNullOrEmpty(active) && active == "Inactive Only")
                predicates.Add(r => !r.Employee.Active);

            if (employees != null)
                predicates.Add(r => employees.Contains(r.EmployeeId));

            var employeePositions = (await FindWithIncludeAsync(predicates, new[] { "Employee.Person", "Position", "Employee.EmployeeOrganizations.Organization" })).ToList();
            
            employeePositions = employeePositions.OrderByDescending(ep => ep.StartDate).ToList();
            employeePositions.GroupBy(r => r.Employee);
            return employeePositions.ToList();
        }

        public async System.Threading.Tasks.Task<int> GetTraineeEmployees()
        {
            var count = await FindQuery(x => x.Trainee == true,false).Select(s => s.EmployeeId).Distinct().CountAsync();
            return count;
        }

        public async System.Threading.Tasks.Task<List<EmployeePosition>> GetEMPPositionsIdsOnly(Expression<Func<EmployeePosition, bool>> predicates)
        {
            var empPositions = await FindQuery(predicates,true).Select(s => new EmployeePosition { Id = s.Id, EmployeeId = s.EmployeeId, PositionId = s.PositionId }).ToListAsync();

            return empPositions;
        }

        public async System.Threading.Tasks.Task<List<EmployeePosition>> GetEmpPositionsWithCompactPositionsAndConditions(Expression<Func<EmployeePosition, bool>> predicates)
        {
            var empPositions = await FindQueryWithIncludeAsync(predicates, new string[] { "Position" }).Select(s => new EmployeePosition {
                Id = s.Id,
                Active = s.Active,
                EmployeeId = s.EmployeeId,
                PositionId = s.PositionId,
                EndDate = s.EndDate,
                Deleted = s.Deleted,
                CreatedBy =s.CreatedBy,
                CreatedDate = s.CreatedDate,
                IsCertificationNotRequired = s.IsCertificationNotRequired,
                IsSignificant = s.IsSignificant,
                ManagerName = s.ManagerName,
                ModifiedBy = s.ModifiedBy,
                ModifiedDate = s.ModifiedDate,
                QualificationDate = s.QualificationDate,
                StartDate = s.StartDate,
                Trainee = s.Trainee,
                TrainingProgramVersion = s.TrainingProgramVersion,
                Position = new Position
                {
                    Id = s.Position.Id,
                    Active = s.Position.Active,
                    PositionAbbreviation = s.Position.PositionAbbreviation,
                    HyperLink = s.Position.HyperLink,
                    IsPublished = s.Position.IsPublished,
                    PositionDescription = s.Position.PositionDescription,
                    PositionNumber = s.Position.PositionNumber,
                    PositionTitle = s.Position.PositionTitle,
                    FileName = s.Position.FileName,
                    EffectiveDate = s.Position.EffectiveDate
                },
            }).ToListAsync();
            return empPositions;
        }
    }
}
