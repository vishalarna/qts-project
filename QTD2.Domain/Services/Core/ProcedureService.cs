using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class ProcedureService : Common.Service<Entities.Core.Procedure>, Interfaces.Service.Core.IProcedureService
    {
        public ProcedureService(IProcedureRepository procedureRepository, IProcedureValidation procedureValidation)
            : base(procedureRepository, procedureValidation)
        {
        }

        public async System.Threading.Tasks.Task<List<Procedure>> GetAllProceduresByIssuingAuthorityAsync()
        {
            return (await AllWithIncludeAsync(new string[] { "Procedure_IssuingAuthority" })).ToList();
        }
        public async System.Threading.Tasks.Task<List<Procedure>> GetAllProcedureCompletionHistoryAsync(string employeeStatus, string completedStatus, DateTime startDate, DateTime endDate, List<int> procedureIds, List<int> positionIds, List<int> organizationIds)
        {
            List<Expression<Func<Procedure, bool>>> predicates = new List<Expression<Func<Procedure, bool>>>();
            predicates.Add(r => procedureIds.Contains(r.Id));
            var procedures = (await FindWithIncludeAsync(predicates, new[] { "Procedure_IssuingAuthority", "ProcedureReviews.ProcedureReview_Employee.Employee.Person", "ProcedureReviews.ProcedureReview_Employee.Employee.EmployeePositions.Position", "ProcedureReviews.ProcedureReview_Employee.Employee.EmployeeOrganizations" })).ToList();

            foreach (var procedure in procedures)
            {

                var reviews = procedure.ProcedureReviews.Where(r =>
                        (startDate < r.StartDateTime.Date && endDate >= r.StartDateTime.Date) ||
                        (startDate >= r.StartDateTime.Date && startDate < r.EndDateTime.Date) ||
                        (endDate <= r.EndDateTime.Date && endDate >= r.StartDateTime.Date)
                    );

                foreach (var pr in reviews)
                {
                    pr.ProcedureReview_Employee = pr.ProcedureReview_Employee
                    .Where(r => (organizationIds.Count == 0 || r.Employee.EmployeeOrganizations.Any(x => organizationIds.Contains(x.OrganizationId)))
                    )
                    .Where(r => employeeStatus == "Active Only" ? r.Employee.Active : (employeeStatus == "Inactive Only" ? !r.Employee.Active : true))
                    .ToList();
                    if (completedStatus.ToUpper() == "NOT COMPLETED WITHIN DATE RANGE") 
                    {
                        pr.ProcedureReview_Employee = pr.ProcedureReview_Employee.Where(x => !x.IsCompleted).ToList();
                    }
                    else if (completedStatus.ToUpper() == "COMPLETED WITHIN DATE RANGE")
                    {
                        pr.ProcedureReview_Employee = pr.ProcedureReview_Employee.Where(x =>
                                                            x.IsCompleted
                                                            && x.CompletedDate.HasValue
                                                            && x.CompletedDate?.Date >= startDate 
                                                            && x.CompletedDate?.Date <= endDate
                                                            ).ToList();
                    }

                    if (positionIds.Count() > 0)
                    {
                        pr.ProcedureReview_Employee = pr.ProcedureReview_Employee.Where(r => r.Employee.EmployeePositions.Where(s => positionIds.Contains(s.PositionId)).Where(r => r.Active).Any()).ToList();
                    }

                    if (organizationIds.Count() > 0)
                    {
                        pr.ProcedureReview_Employee = pr.ProcedureReview_Employee.Where(r => r.Employee.EmployeeOrganizations.Where(s => organizationIds.Contains(s.OrganizationId)).Where(r => r.Active).Any()).ToList();
                    }
                }

                procedure.ProcedureReviews = reviews.ToList();
            }

            return procedures.ToList();
        }

		public async Task<List<Procedure>> GetProceduresForEmployeeTrainingTowardProceduresAndRegulatoryRequirements(List<int> procedureIds)
		{
            List<Expression<Func<Procedure, bool>>> predicates = new List<Expression<Func<Procedure, bool>>>();

            predicates.Add(p => procedureIds.Contains(p.Id));

            var procedures = (await FindWithIncludeAsync(predicates, new string[] {
                "ILA_Procedure_Links"
            })).ToList();

            return procedures;
        }

        public async Task<List<Procedure>> GetAllProceduresByIssuingAuthoritiesAsync(List<int> issuingAuthorityIds, bool includeInactive)
        {
            List<Expression<Func<Procedure, bool>>> predicates = new List<Expression<Func<Procedure, bool>>>();

            predicates.Add(p => issuingAuthorityIds.Contains(p.IssuingAuthorityId));

            if(!includeInactive)
            {
                predicates.Add(p => p.Active);
            }

            var procedures = (await FindWithIncludeAsync(predicates, new string[] {
                "Procedure_IssuingAuthority"
            })).ToList();

            return procedures;
        }

        public async Task<List<Procedure>> GetProceduresByIDAsync(List<int> procedureIds)
        {
            List<Expression<Func<Procedure, bool>>> predicates = new List<Expression<Func<Procedure, bool>>>();

            predicates.Add(p => procedureIds.Contains(p.Id));

            var procedures = (await FindWithIncludeAsync(predicates, new string[] {
                "Procedure_IssuingAuthority","Procedure_Task_Links"
            })).ToList();

            return procedures;
        }

        public async Task<Procedure> GetProceduresByIDAndNumberAsync(int procedureId, string procedureNumber)
        {
            var procedure = (await FindAsync(r => r.Id != procedureId && r.Number.Trim().ToLower() == procedureNumber.Trim().ToLower())).FirstOrDefault();
            return procedure;
        }
    }
}
