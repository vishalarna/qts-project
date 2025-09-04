using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class Task_StepTestData
    {
        public static IEnumerable<Task_Step> GetAll()
        {
            return new List<Task_Step>()
            {
                Task_Step1(),
                Task_Step2()
            };
        }

        static Task_Step Task_Step1()
        {
            return new Task_Step(1, "Step 1 for this Task", 1, null);
        }

        static Task_Step Task_Step2()
        {
            return new Task_Step(1, "Step 2 for this Task", 2, null);
        }
    }
}
