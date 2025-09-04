using QTD2.Test.Data.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class TaskTests
    {
        public static IEnumerable<object[]> Tasks_StepGetData()
        {
            var data = new List<object[]>();
            var tasks = TaskTestData.GetAll();
            var taskStep = Task_StepTestData.GetAll();

            foreach (var task in tasks)
            {
                foreach (var step in taskStep)
                {
                    data.Add(new object[] { task, step });
                }
            }

            return data;
        }

        public static IEnumerable<object[]> Tasks_QuestionsGetData()
        {
            var data = new List<object[]>();
            var tasks = TaskTestData.GetAll();
            var questions = QuestionTestData.GetAll();

            foreach (var task in tasks)
            {
                foreach (var question in questions)
                {
                    data.Add(new object[] { task, question });
                }
            }

            return data;
        }

        public static IEnumerable<object[]> Tasks_EnablingObjectiveGetData()
        {
            var data = new List<object[]>();
            var tasks = TaskTestData.GetAll();
            var enablingObjectives = EnablingObjectiveTestData.GetAll();

            foreach (var task in tasks)
            {
                foreach (var eo in enablingObjectives)
                {
                    data.Add(new object[] { task, eo });
                }
            }

            return data;
        }

        public static IEnumerable<object[]> Tasks_ProcedureGetData()
        {
            var data = new List<object[]>();
            var tasks = TaskTestData.GetAll();
            var procedures = ProcedureTestData.GetAll();

            foreach (var task in tasks)
            {
                foreach (var proc in procedures)
                {
                    data.Add(new object[] { task, proc });
                }
            }

            return data;
        }

        public static IEnumerable<object[]> Tasks_SafetyHazardGetData()
        {
            var data = new List<object[]>();
            var tasks = TaskTestData.GetAll();
            var saftyHazards = SafetyHazardTestData.GetAll();

            foreach (var task in tasks)
            {
                foreach (var sh in saftyHazards)
                {
                    data.Add(new object[] { task, sh });
                }
            }

            return data;
        }

        public static IEnumerable<object[]> Tasks_EmployeeGetData()
        {
            var data = new List<object[]>();
            var tasks = TaskTestData.GetAll();
            var employees = EmployeeTestData.GetAll();

            foreach (var task in tasks)
            {
                foreach (var emp in employees)
                {
                    data.Add(new object[] { task, emp });
                }
            }

            return data;
        }
    }
}
