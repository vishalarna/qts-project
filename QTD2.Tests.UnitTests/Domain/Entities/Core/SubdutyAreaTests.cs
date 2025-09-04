using QTD2.Test.Data.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class SubdutyAreaTests
    {
        public static IEnumerable<object[]> GetTaskData()
        {
            var data = new List<object[]>();
            var subdutyArea = SubdutyAreaTestData.GetAll();
            var tasks = TaskTestData.GetAll();

            foreach (var sda in subdutyArea)
            {
                foreach (var task in tasks)
                {
                    data.Add(new object[] { sda, task });
                }
            }

            return data;
        }
    }
}
