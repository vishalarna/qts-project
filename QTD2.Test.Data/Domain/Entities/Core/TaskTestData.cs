using System;
using System.Collections.Generic;
using TaskEntity = QTD2.Domain.Entities.Core.Task;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class TaskTestData
    {
        public static List<TaskEntity> GetAll()
        {
            return new List<TaskEntity>()
            {
                Task1(),
                Task2()
            };
        }

        static TaskEntity Task1()
        {
            return new TaskEntity(1, "Supervise the machines and operators", 1, "Given Criteria", true, "No reference", 10, "Min", "NO CRITERIA", string.Empty,false,false,string.Empty, DateOnly.FromDateTime(System.DateTime.Now));
        }

        static TaskEntity Task2()
        {
            return new TaskEntity(1, "Note down the electricity consumptions by machines", 1, "Given Criteria", true, "No reference", 10, "Min", "NO CRITERIA", string.Empty,false,false,string.Empty, DateOnly.FromDateTime(System.DateTime.Now));
        }
    }
}
