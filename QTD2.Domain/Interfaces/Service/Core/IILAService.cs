using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IILAService : Common.IService<ILA>
    {
        System.Threading.Tasks.Task<IEnumerable<ILA>> GetByProvidersListAsync(List<int> providerIds,string ilaStatus);
        System.Threading.Tasks.Task<ILA> GetWithProviderTopicDeliveryMethodAsync(int id);
        public System.Threading.Tasks.Task<List<ILA>> GetILALessonPlanAsync(List<int> iLAIDs);
        public System.Threading.Tasks.Task<List<ILA>> GetEmployeeSelfRegistrationAvailableCourseAsync();
        public System.Threading.Tasks.Task<List<ILA>> GetILAAsync();
        public System.Threading.Tasks.Task<List<ILA>> GetILACompletionHistoryAsync(List<int> iLAIDs, string completedStatus, DateTime startDate, DateTime endDate, string activeStatus);
        public System.Threading.Tasks.Task<List<ILA>> GetItemByILAAsync(List<int> iLAIDs);
        public System.Threading.Tasks.Task<List<ILA>> GetAllILAsByILAIdAsync(int ilaId);
        //New Application Services

        public System.Threading.Tasks.Task<ILA> GetLocationByIdAsync(int? iLaId);
        System.Threading.Tasks.Task<List<ILA>> GetTestItemsByILAAsync(List<int> list);
        System.Threading.Tasks.Task<List<ILA>> GetILAsWithCertificationInformationAsync(List<int> ilaIds);
        System.Threading.Tasks.Task<ILA> GetWithAllClassesAndStudentsAsync(int iLAId);

        //System.Threading.Tasks.Task<List<ILA>> GetTestItemsByILA(List<int> list);
        //System.Threading.Tasks.Task<List<ILA>> GetILAsWithCertificationInformation(List<int> ilaIds);

        System.Threading.Tasks.Task<List<ILA>> GetCompactedILA();

        System.Threading.Tasks.Task<ILA> GetCompactedILA(int ilaId);

        System.Threading.Tasks.Task<List<ILA>> GetCompactedILAActiveOnly();
        System.Threading.Tasks.Task<List<ILA>> GetCoursesByILAAsync();
        System.Threading.Tasks.Task<List<ILA>> GetCompactedByIds(List<int> ilaIds);
        System.Threading.Tasks.Task<string> GetNameByIdAsync(int ilaId);
        System.Threading.Tasks.Task<List<ILA>> GetAllActiveILAsWithNamesAsync();
        System.Threading.Tasks.Task<ILA> GetFullILADetailsAsync(int ilaId);
        System.Threading.Tasks.Task<ILA> GetILARequirementsDetailsByILAId(int ilaId);
        System.Threading.Tasks.Task<List<ILA>> GetILAsWithProvider(List<int> ilaIds);
        System.Threading.Tasks.Task<List<ILA>> GetSelfRegistrationAvailableCourseAsync();
        System.Threading.Tasks.Task<ILA> GetForCopy(int id);
        System.Threading.Tasks.Task<List<ILA>> GetActiveILADetailsAsync();
        System.Threading.Tasks.Task<ILA> GetILAWithILACertLinksAsync(int id);
        System.Threading.Tasks.Task<List<ILA>> GetByListOfIdsAsync(int[] iLAIds);
        System.Threading.Tasks.Task<List<ILA>> GetSelfPacedCoursesByILAAsync();
        System.Threading.Tasks.Task<ILA> GetWithILAEvalsAsync(int ilaId);
        System.Threading.Tasks.Task<List<ILA>> GetILAsWithCertificationLinksOnlyAsync(List<int> ilaIds);
        System.Threading.Tasks.Task<List<ILA>> GetILAsWithObjectivesForMetaILAAsync(List<int> ilaIds,List<int>objectiveTypeIds);
        System.Threading.Tasks.Task<List<ILA>> GetILAWithProvidersAsync();
        System.Threading.Tasks.Task<List<ILA>> GetILAsByILAIdAsync(List<int> ilaIds);
        public Task<List<ILA>> GetAllILAsWithDeliveryMethodAsync(string ilasType);
        public Task<List<ILA>> GetILAsNotLinkedToTopic();
        public Task<ILA> GetILAWithTrainingTopics(int id);
        System.Threading.Tasks.Task<ILA> GetTrainingPlanAsync(int ilaId);
        public Task<ILA> GetILAWithProviderAndDeliveryMethodAsync(int id);
        System.Threading.Tasks.Task<List<ILA>> GetILAsWithCertificationSubRequirementsAsync(int ilaId);
        Task<ILA> GetILARequirementsAsync(int ilaId);
    }
}
