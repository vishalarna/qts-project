using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestEntity = QTD2.Domain.Entities.Core.Test;
namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class TestTestData
    {
        public static List<TestEntity> GetAll()
        {
            return new List<TestEntity>()
            {
                Test1(),
                //Test2()
            };
        }

        static TestEntity Test1()
        {
            return new TestEntity(1, "test");
        }

        /*static TestEntity Test2()
        {
            return new TestEntity(1, "test2");
        }*/
    }
}
