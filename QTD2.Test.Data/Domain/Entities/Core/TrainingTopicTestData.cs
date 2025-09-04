using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class TrainingTopicTestData
    {
        public static List<TrainingTopic> getAll()
        {
            return new List<TrainingTopic>
            {
                tt01(),tt02()
            };
        }

        static TrainingTopic tt01()
        {
            var tt = new TrainingTopic("first topic",1);
            tt.Set_Id(1);
            return tt;
        }

        static TrainingTopic tt02()
        {
            var tt = new TrainingTopic("second topic",1);
            tt.Set_Id(2);
            return tt;
        }
    }
}
