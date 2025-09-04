using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class QuestionTestData
    {
        public static IEnumerable<Task_Question> GetAll()
        {
            return new List<Task_Question>()
            {
                Question1(),
                Question2(),
                Question3(),
                Question4(),
            };
        }

        static Task_Question Question1()
        {
            return new Task_Question(1, "Some Question 1", "The answer of the question",1);
        }


        static Task_Question Question2()
        {
            return new Task_Question(1, "Some Question 2", "The answer of the question",2);
        }

        static Task_Question Question3()
        {
            return new Task_Question(1, "Some Question 3", "The answer of the question",3);
        }

        static Task_Question Question4()
        {
            return new Task_Question(1, "Some Question 4", "The answer of the question",4);
        }
    }
}
