using Microsoft.EntityFrameworkCore;
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
    public class PositionService : Common.Service<Position>, IPositionService
    {
        public PositionService(IPositionRepository positionRepository, IPositionValidation positionValidation)
            : base(positionRepository, positionValidation)
        {
        }

        public async System.Threading.Tasks.Task<IEnumerable<Position>> GetByIdListAsync(IEnumerable<int> positionIds)
        {
            return await FindAsync(r => positionIds.ToList().Contains(r.Id));
        }

        public async System.Threading.Tasks.Task<List<Position>> GetForMyDataPositionDetailsAsync(List<int> positionIds, string filterType, string version)
        {
            var positions = await FindAsync(r => positionIds.ToList().Contains(r.Id));
            if (filterType.ToUpper() == "ACTIVE ONLY")
            {
                positions = positions.Where(r => r.Active == true);
            }
            else if (filterType.ToUpper() == "INACTIVE ONLY")
            {
                positions = positions.Where(r => r.Active == false);
            }
            return positions.ToList();
        }

        public async System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.Position>> GetTaskRequalificationByPositionAsync(string activePositions, bool includeCustomTasks, bool includeTrainees, DateTime startDate, DateTime endDate)
        {
            List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>> predicates = new List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>>();

            if (activePositions == "Active Only")
                predicates.Add(r => r.Active);

            if (activePositions == "Inactive Only")
                predicates.Add(r => !r.Active);

            var positions = await FindWithIncludeAsync(predicates, new[] { "EmployeePositions.Employee.Person", "ILA_Position_Links.ILA", "Task_Positions.Task.TaskQualifications.TaskQualificationStatus", "Position_Tasks.Task.SubdutyArea.DutyArea" });

            foreach (var position in positions)
            {
                position.EmployeePositions = position.EmployeePositions.Where(ep => ep.StartDate >= DateOnly.FromDateTime(startDate) && ep.EndDate <= DateOnly.FromDateTime(endDate)).ToList();
                if (!includeTrainees)
                {
                    position.EmployeePositions = position.EmployeePositions.Where(r => !r.Trainee).ToList();
                }
            }

            return positions.ToList();
        }

        public async System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.Position>> GetPositionTasksByIdAsync(List<int> positionIds)
        {
            List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>> predicates = new List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>>();

            if (positionIds != null)
                predicates.Add(r => positionIds.Contains(r.Id));

            var positions = await FindWithIncludeAsync(predicates, new[] { "Position_Tasks.Task.SubdutyArea.DutyArea" });

            return positions.ToList();
        }

        public async System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.Position>> GetPositionTasksByIdsAsync(List<int> positionIds)
        {
            List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>> predicates = new List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>>();

            if (positionIds != null)
                predicates.Add(r => positionIds.Contains(r.Id));

            var positions = await FindWithIncludeAsync(predicates, new[] { "Position_Tasks" });

            return positions.ToList();
        }

        public async System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.Position>> GetTaskRequalificationByPositionAsync(List<int> positionIds, bool includeTrainees)
        {
            List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>> predicates = new List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>>();

            if (positionIds != null)
                predicates.Add(r => positionIds.Contains(r.Id));

            var positions = await FindWithIncludeAsync(predicates, new[] { "Position_Tasks", "EmployeePositions.Employee.Person" });
            foreach (var position in positions)
            {
                if (!includeTrainees)
                {
                    position.EmployeePositions = position.EmployeePositions.Where(r => !r.Trainee).ToList();
                }
            }
            return positions.ToList();
        }

        public async System.Threading.Tasks.Task<List<Position>> GetAllActiveCompactPositions()
        {
            var positions = await FindQuery(x => x.Active == true, true).Select(s => new Position
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
            }).ToListAsync();
            return positions;
        }

        public async Task<Position> GetCompactPositions(int posId)
        {
            var pos = await FindQuery(x => x.Id == posId, true).Select(s => new Position
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
            return pos;
        }

        public async System.Threading.Tasks.Task<List<Position>> GetAllTrainingSummaryByPositionAsync(List<int> positionIds, List<int> organizationIds)
        {
            List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>> predicates = new List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>>();
            if (positionIds != null)
                predicates.Add(r => positionIds.Contains(r.Id));
            if (organizationIds != null && organizationIds.Any())
            {
                predicates.Add(r => r.EmployeePositions.Any(ep => ep.Employee.EmployeeOrganizations.Any(eo => organizationIds.Contains(eo.OrganizationId))));
            }
            var positions = await FindWithIncludeAsync(predicates, new[] { "EmployeePositions.Employee.Person", "EmployeePositions.Employee.EmployeeOrganizations.Organization" });

            foreach (var position in positions)
            {
                position.EmployeePositions = position.EmployeePositions.Where(r => r.Active && r.Employee.Active).ToList();

                if (organizationIds != null && organizationIds.Count > 0)
                {
                    var filteredEmployeePositions = new List<EmployeePosition>();

                    foreach (var employeePosition in position.EmployeePositions)
                    {
                        if (employeePosition.Employee.EmployeeOrganizations.Any(eo => organizationIds.Contains(eo.OrganizationId)))
                        {
                            filteredEmployeePositions.Add(employeePosition);
                        }
                    }
                    position.EmployeePositions = filteredEmployeePositions;
                }
            }
            return positions.ToList();
        }

        public async System.Threading.Tasks.Task<List<Position>> GetAllOJTGuideByPositionAsync(int positionId)
        {
            List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>> predicates = new List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>>();
            predicates.Add(position => position.Id == positionId);

            var positions = await FindWithIncludeAsync(predicates, new[] {
                "Position_Tasks.Task.Procedure_Task_Links.Procedure",
                "Position_Tasks.Task.SubdutyArea.DutyArea",
                "Position_Tasks.Task.Task_Tools.Tool",
                "Position_Tasks.Task.Task_EnablingObjective_Links.EnablingObjective",
                "Position_Tasks.Task.Task_Steps",
                "Position_Tasks.Task.Task_Questions",
                "Position_Tasks.Task.Task_Tools.Tool",
                "Position_Tasks.Task.SafetyHazard_Task_Links.SaftyHazard"
            });
            return positions.ToList();
        }
        public async System.Threading.Tasks.Task<List<Position>> GetPositionsByIdsAsync(int positionId)
        {
            List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>> predicates = new List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>>();
            predicates.Add(position => position.Id == positionId);

            var positions = await FindWithIncludeAsync(predicates, new[] { "Position_Tasks", "EmployeePositions.Employee.Person" });
            return positions.ToList();
        }

        public async System.Threading.Tasks.Task<List<Position>> GetAllCompactPositions()
        {
            var positions = await AllQuery().Select(s => new Position
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
            }).OrderBy(x => x.PositionTitle).ToListAsync();
            return positions;
        }

        public async Task<int> GetPositionsNotLinkedToTaskCount()
        {
            var count = await AllQueryWithInclude(new string[] { "Position_Tasks" }).Select(s => new Position
            {
                Active = s.Active,
                Position_Tasks = s.Position_Tasks,
            }).Where(w => w.Position_Tasks.Count == 0).CountAsync();
            return count;
        }

        public async Task<int> GetPositionsNotLinkedToSQsCount()
        {
            var count = await AllQueryWithInclude(new string[] { "Position_SQs" }).Select(s => new Position
            {
                Active = s.Active,
                Position_SQs = s.Position_SQs,
            }).Where(w => w.Position_SQs.Count == 0).CountAsync();
            return count;
        }

        public async Task<int> GetPositionsNotLinkedEMPCount()
        {
            var count = await AllQueryWithInclude(new string[] { "EmployeePositions" }).Select(s => new Position
            {
                Active = s.Active,
                EmployeePositions = s.EmployeePositions,
            }).Where(w => w.EmployeePositions.Count() == 0).CountAsync();
            return count;
        }

        public async System.Threading.Tasks.Task<List<Position>> GetPositionsAsync()
        {
            var positions = await AllWithIncludeAsync(new string[] { "Position_Tasks" });
            return positions.ToList();
        }

        public async Task<string> GetPositionTitleByIdAsync(int positionId)
        {
            var position = await GetAsync(positionId);
            return position?.PositionTitle;
        }

        public async Task<List<Position>> GetAllActivePositionsWithPosTitleAsync()
        {
            return (await FindAsync(r => r.Active)).ToList();
        }

        public async System.Threading.Tasks.Task<Position> GetAnnualPositionsTaskListReviewAsync(int positionId, DateTime startDate, DateTime endDate, bool includePsuedoTasks, bool includeRRPositions)
        {
            List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>> predicates = new List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>>();
            predicates.Add(position => position.Id == positionId);

            var position = (await FindWithIncludeAsync(predicates, new[] { "Position_Tasks.Task.Version_Tasks", "Position_Tasks.Task.SubdutyArea.DutyArea" }, true)).FirstOrDefault();

            position.Position_Tasks = position.Position_Tasks.Where(x => !includeRRPositions || x.Task.IsReliability).ToList();
            position.Position_Tasks = position.Position_Tasks.Where(x => includePsuedoTasks || x.Task.SubdutyArea.DutyArea.Letter != "P").ToList();
            foreach (var positionTask in position.Position_Tasks)
            {
                positionTask.Task.Version_Tasks = positionTask.Task.Version_Tasks.Where(vt => vt.EffectiveDate >= DateOnly.FromDateTime(startDate) && vt.EffectiveDate <= DateOnly.FromDateTime(endDate)).ToList();
            }

            return position;
        }


        public async System.Threading.Tasks.Task<List<Position>> GetPositionsByIdsAsync(List<int> positionIds)
        {

            var positions = await FindWithIncludeAsync(x => positionIds.Contains(x.Id) && x.EmployeePositions.Count() > 0, new[] { "EmployeePositions" });

            return positions.ToList();
        }

        public async System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.Position>> InitialTrainingByPositionAsync(string trainingProgramId, bool includeInactiveILA)
        {
            List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>> predicates = new List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>>();

            var positions = await FindWithIncludeAsync(predicates, new[] { "TrainingPrograms.TrainingProgram_ILA_Links" });

            foreach (var position in positions)
            {
                position.TrainingPrograms = position.TrainingPrograms.Where(tp => tp.Id.ToString() == trainingProgramId).ToList();
            }
            positions = positions.Where(position => position.TrainingPrograms.Count(tp => tp.Id.ToString() == trainingProgramId) > 0).ToList();
            return positions.ToList();
        }

        public async System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.Position>> TrainingProgramCompletionHistoryAsync(List<int> trainingProgramId, List<DateTime> dateRanges, bool includeInActiveIla)
        {
            List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>> predicates = new List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>>();

            var positions = await FindWithIncludeAsync(predicates, new[] { "TrainingPrograms.TrainingProgram_ILA_Links.ILA" });

            foreach (var position in positions)
            {
                position.TrainingPrograms = position.TrainingPrograms.Where(tp => trainingProgramId.Contains(tp.Id)).ToList();

                if (!includeInActiveIla)
                {
                    foreach (var trainingProgram in position.TrainingPrograms)
                    {

                        trainingProgram.TrainingProgram_ILA_Links = trainingProgram.TrainingProgram_ILA_Links
                                                                .Where(link => link.ILA.Active)
                                                                .ToList();
                    }
                }
            }

            return positions.Where(r => r.TrainingPrograms != null && r.TrainingPrograms.Count > 0).ToList();
        }

        public async System.Threading.Tasks.Task<List<Position>> GetEmployeesByPositionAsync(List<int> positionIDs, bool includeCurrentPosition, string activeStatus, bool includeTrainee)
        {
            List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>> predicates = new List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>>();

            if (positionIDs != null)
                predicates.Add(r => positionIDs.Contains(r.Id));

            var positions = await FindWithIncludeAsync(predicates, new[] { "EmployeePositions.Employee.Person", "EmployeePositions.Employee.EmployeeCertifications.Certification" });

            foreach (var position in positions)
            {
                if (!includeTrainee)
                {
                    position.EmployeePositions = position.EmployeePositions.Where(r => !r.Trainee).ToList();
                }

                if (includeCurrentPosition)
                {
                    position.EmployeePositions = position.EmployeePositions.Where(r => r.Active).ToList();
                }

                if (activeStatus == "Active Only")
                {
                    position.EmployeePositions = position.EmployeePositions.Where(r => r.Employee.Active).ToList();
                }

                if (activeStatus == "Inactive Only")
                {
                    position.EmployeePositions = position.EmployeePositions.Where(r => !r.Employee.Active).ToList();
                }
            }

            return positions.ToList();
        }

        public async System.Threading.Tasks.Task<List<Position>> GetPositionTaskHistoryAsync(List<int> positionIds)
        {
            List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>> predicates = new List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>>();
            predicates.Add(position => positionIds.Contains(position.Id));

            var positions = (await FindWithIncludeAsync(predicates, new[] { "Position_Tasks.Task" })).ToList();


            return positions;
        }

        public async Task<List<Position>> GetPositionsForTasksMetbyPosition(List<int> positionIds, string allActiveInactiveOnlyEmployees, bool currentPositionsOnly, bool includeTrainees)
        {
            List<Expression<Func<Position, bool>>> predicates = new List<Expression<Func<Position, bool>>>();

            predicates.Add(p => positionIds.Contains(p.Id));

            var positions = (await FindWithIncludeAsync(predicates, new string[] {
                "EmployeePositions.Employee.Person",
                "Position_Tasks"
            })).ToList();
            var currentDate = DateTime.Now.Date;
            foreach (var position in positions)
            {
                if (allActiveInactiveOnlyEmployees == "Inactive Only")
                {
                    position.EmployeePositions = position.EmployeePositions.Where(ep => !ep.Employee.Active).ToList();
                }
                else if (allActiveInactiveOnlyEmployees == "Active Only")
                {
                    position.EmployeePositions = position.EmployeePositions.Where(ep => ep.Employee.Active).ToList();
                }
                if (currentPositionsOnly)
                {
                    position.EmployeePositions = position.EmployeePositions.Where(ep => ep.Active).ToList();
                }
                if (!includeTrainees)
                {
                    position.EmployeePositions = position.EmployeePositions.Where(ep => !ep.Trainee).ToList();
                }
                position.Position_Tasks = position.Position_Tasks.Where(pt => pt.Active).ToList();
            }


            //Filter to only the TaskQualifications which could count as Completed and are within this Position, so we limit to only the set we care to find later when looking for the existance of a Completed Task
            foreach (var position in positions)
            {
                foreach (var employeePosition in position.EmployeePositions)
                {
                    employeePosition.Employee.TaskQualifications = employeePosition.Employee.TaskQualifications.Where(tq => tq.TaskQualificationStatus.Description == "Completed" && tq.Active && position.Position_Tasks.Select(pt => pt.TaskId).Contains(tq.TaskId)).ToList();
                }
            }

            return positions.ToList();

        }

        public async Task<List<Position>> GetPositionsForProcedureAndRegulatoryRequirementTrainingSummarybyPosition(List<int> positionIds)
        {
            List<Expression<Func<Position, bool>>> predicates = new List<Expression<Func<Position, bool>>>();

            predicates.Add(p => positionIds.Contains(p.Id));

            var positions = (await FindWithIncludeAsync(predicates, new string[] {
                "EmployeePositions.Employee.Person",
                "Position_Tasks"
            })).ToList();

            return positions;
        }

        public async Task<List<Position>> GetILAsByPositionAsync(List<int> positionIds)
        {
            List<Expression<Func<Position, bool>>> predicates = new List<Expression<Func<Position, bool>>>();

            predicates.Add(p => positionIds.Contains(p.Id));

            var positions = (await FindWithIncludeAsync(predicates, new string[] { "Position_SQs.EnablingObjective.ILA_EnablingObjective_Links.ILA.Provider" })).ToList();

            return positions;
        }

        public async Task<List<Position>> GetForSafetyHazardsByPositionMatrix(List<int> positionIds)
        {
            List<Expression<Func<Position, bool>>> predicates = new List<Expression<Func<Position, bool>>>();

            predicates.Add(p => positionIds.Contains(p.Id));

            var positions = (await FindWithIncludeAsync(predicates, new string[] { "Position_Tasks.Task.SafetyHazard_Task_Links" })).ToList();

            return positions;
        }
        public async Task<List<Position>> GetForEnablingObjectivesByPositionMatrixAsync(List<int> positionIds)
        {
            List<Expression<Func<Position, bool>>> predicates = new List<Expression<Func<Position, bool>>>();
            predicates.Add(p => positionIds.Contains(p.Id));
            var positions = (await FindWithIncludeAsync(predicates, new string[] { "Position_SQs", "Position_Tasks" }, true)).ToList();
            return positions;
        }

        public async Task<List<Position>> GetPositionSqsWithEOAsync(List<int> positionIds)
        {
            List<Expression<Func<Position, bool>>> predicates = new List<Expression<Func<Position, bool>>>();
            predicates.Add(p => positionIds.Contains(p.Id));
            var positions = (await FindWithIncludeAsync(predicates, new string[] { "Position_SQs.EnablingObjective.EnablingObjective_Category", "Position_SQs.EnablingObjective.EnablingObjective_SubCategory", "Position_SQs.EnablingObjective.EnablingObjective_Topic" }, true)).ToList();
            return positions;
        }

        public async System.Threading.Tasks.Task<List<Position>> GetPositionsByIdAsync(List<int> positionIds, string trainingProgramId)
        {
            List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>> predicates = new List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>>();
            predicates.Add(r => positionIds.Contains(r.Id));
            var positions = await FindWithIncludeAsync(predicates, new[] {  "TrainingPrograms.TrainingProgram_ILA_Links.ILA.ILA_TaskObjective_Links" });

            foreach (var position in positions)
            {
                position.TrainingPrograms = position.TrainingPrograms.Where(tp => tp.Id == int.Parse(trainingProgramId)).ToList();
            }
            return positions.ToList();
        }

        public async Task<Position> GetPositionByNameAsync(string positionName)
        {
            List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>> predicates = new List<Expression<Func<QTD2.Domain.Entities.Core.Position, bool>>>();
            predicates.Add(x => x.PositionTitle == positionName);
            predicates.Add(x => x.Active);
            return (await FindAsync(predicates)).FirstOrDefault();
        }
    }
}
