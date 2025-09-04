using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Employee;
using QTD2.Infrastructure.Model.EmployeePosition;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.Position;
using QTD2.Infrastructure.Model.Position_Employee;
using QTD2.Infrastructure.Model.Position_SQ_Link;
using QTD2.Infrastructure.Model.Position_Task_Link;
using QTD2.Infrastructure.Model.PositionHistory;
using QTD2.Infrastructure.Model.Task;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IPositionService
    {
        public Task<List<Position>> GetAsync(bool activeOnly = false);

        public Task<List<Position>> GetWithoutIncludesAsync();

        public Task<List<Position>> GetAllOrderByAsync(string orderBy);

        public Task<int> GetPositionNumberAsync();

        public Task<Position> GetAsync(int id);

        public Task<Position> CreateAsync(PositionCreateOptions options);

        public Task<Position_StatsVM> GetSHStats();

        public Task<Position> UpdateAsync(int id, PositionUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id, Position_HistoryCreateOptions options);

        public System.Threading.Tasks.Task ActiveAsync(int id, Position_HistoryCreateOptions options);

        public System.Threading.Tasks.Task InActiveAsync(int id, Position_HistoryCreateOptions options);

        public System.Threading.Tasks.Task DeletePosition(int id);

        public System.Threading.Tasks.Task InActivePosition(int id);

        public System.Threading.Tasks.Task PositionEndDateChange(int id, DateTime effectiveDate);


        public System.Threading.Tasks.Task ActivePosition(int id);

        public Task<Position> CopyPositionWithLinkages(int id, PositionCreateOptions options);

        public Task<Position> LinkTask(int procId, Position_Task_LinkCreateOptions options);
        public System.Threading.Tasks.Task UnlinkTask(int posId, int[] taskId);
        public Task<List<TaskWithCountR5R6Options>> GetLinkedTasks(int id);
        public Task<List<Position>> GetPositionsTaskIsLinkedTo(int id);

        public Task<List<TrainingGroup>> GetPositionsTaskIsLinkedToTG(int id);

        //SQS

        public Task<Position> LinkEO(int eoId, Position_SQ_LinkCreateOptions options);
        public System.Threading.Tasks.Task UnlinkEO(int posId, int[] eoId);

        public Task<List<EnablingObjectiveWithCountOptions>> GetLinkedEos(int id);

        public Task<List<Position>> GetPositionsEoIsLinkedTo(int id);

        public Task<Position> LinkEmployee(int eoId, Position_Employee_LinkCreateOptions options);

        public System.Threading.Tasks.Task UnlinkEmployee(int posId, int[] eoId);

        public Task<List<EmployeePositionListVM>> GetLinkedEmployees(int id);

        public Task<List<Position>> GetPositionsEmployeeIsLinkedTo(int id);

        public Task<List<Position>> GetPosNotLinkedTo(string option);

        //active inactive list
        public Task<List<Position>> GetActiveInactivePosition(string notLinkedWith);

        public Task<List<Position>> GetPositonWithPositionTaskAsync();

        public Task<Position> GetPositionByNameAsync(string positionName);

    }
}
