using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ClassSchedule_Roster;
using QTD2.Infrastructure.Model.ClassSchedule_StudentEvaluation_Link;
using QTD2.Infrastructure.Model.EMPStudentEvaluationVM;
using QTD2.Infrastructure.Model.QuestionBank;
using QTD2.Infrastructure.Model.StudentEvaluation;
using QTD2.Infrastructure.Model.StudentEvaluation_Question_Link;
using QTD2.Infrastructure.Model.StudentEvaluationForm;
using QTD2.Infrastructure.Model.StudentEvaluationHistory;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IStudentEvaluationService
    {
        public Task<List<StudentEvaluationVM>> GetAsync();

        public Task<List<StudentEvaluationVM>> GetPublishedEvalsAsync();

        public Task<List<EmpStudentEvaluation_VM>> GetEvaluationsAsync();

        public Task<object> CompleteEvaluationAsync(ClassSchedule_Evaluation_RosterOptions options);

        public Task<StudentEvaluation> GetAsync(int id);

        public Task<StudentEvaluation> GetWithRatingScale(int id);

        public Task<StudentEvaluation> CreateAync(StudentEvaluationCreateOptions options);

        public Task<StudentEvaluation> UpdateAsync(int id, StudentEvaluationCreateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id, StudentEvaluationHistoryCreateOptions options);

        public System.Threading.Tasks.Task DeactivateAsync(int id, StudentEvaluationHistoryCreateOptions options);

        public System.Threading.Tasks.Task ActivateAsync(int id, StudentEvaluationHistoryCreateOptions options);

        public Task<StudentEvaluationStatsVM> GetStatsCount();

        public Task<List<StudentEvaluation>> GetList(string option);

        public Task<List<QuestionsWithCountOptions>> GetLinkedQuestions(int id);

        public Task<StudentEvaluation> LinkQuestions(int studentEvaluationId, StudentEvaluation_Question_LinkCreateOptions options);

        public Task<StudentEvaluation> UnLinkQuestions(int studentEvaluationId, StudentEvaluation_Question_LinkCreateOptions options);

        public Task<StudentEvaluation> PublishEvaluation(int id);

        public Task<StudentEvaluation> LinkClass(int evalId, ClassSchedule_StudentEvaluation_LinkCreateOptions options);
        public Task<StudentEvaluation> UpdateLinkClassData(ClassSchedule_StudentEvaluation_LinkUpdateOptions options);

        public Task<object> SaveQuestion(StudentEvaluation_SaveQuestion options);

        public Task<List<ClassScheduleWithCountOptions>> GetLinkedClassesToEvaluation(int id);
        
        public System.Threading.Tasks.Task<object> StartEvaluationAsync(int evaluationId);

        public Task<List<StudentEvaluationWithoutEmp>> GetSavedQuestionsDataAsync(int evalId, int classId, int empId);

        //New Application Services

        public Task<object> GetEvaluationsByIdAsync(int employeeId);

        public System.Threading.Tasks.Task<object> StartEvaluationAsyncByIdAsync(int evaluationId, int employeeId);
        public Task<object> CompleteEvaluationAsyncByIdAsync(ClassSchedule_Evaluation_RosterOptions options,int employeeId);
    }
}
