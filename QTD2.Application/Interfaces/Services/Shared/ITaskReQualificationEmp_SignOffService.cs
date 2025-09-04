using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.TaskReQualificationEmp;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITaskReQualificationEmp_SignOffService
    {
        public Task<TaskReQualificationEmpSignOffVM> GetSignOffData(int qualificationId, int employeeId);
        public Task<TaskReQualificationEmp_SignOff> CreateOrUpdateSignOffAsync(TaskReQualificationEmpSignOffVM options);
        public Task<List<TaskReQualificationCompletedVM>> GetCompletedTaskRequalifications(bool isEvaluator);
        public Task<TaskReQualificatioFeedBackVM> GetFeedBackData(int qualificationId, int traineeId);
        public Task<List<TaskReQualificationCompletedVM>> GetCompletedTaskRequalificationsByEmpId(bool isEvaluator, int employeeId);

    }
}
