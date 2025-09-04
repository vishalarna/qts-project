using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.IDP;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IIDPService
    {
        public Task<List<IDPVM>> GetAllIDPs(int empId, int year);

        public Task<List<IDP_Review>> GetIDPReviewsForEMPAsync(int empId);

        public Task<IDP_Review> CreateIDPReviewAsync(IDP_ReviewCreateOptions options);
        System.Threading.Tasks.Task<object> UpdateIDPGrade(UpdateGradeOptions options);
        public System.Threading.Tasks.Task LinkIDPAsync(int empId, IdpTrainingLinkOptions options);
        System.Threading.Tasks.Task<object> GetLinkedSchedulingClasses(int id, int empId);
        System.Threading.Tasks.Task<object> EnrollEmployeeToClass(int id, int empId, EnrollEmployeeOptions options);
        System.Threading.Tasks.Task<object> UnEnrollEmployeeFromIDP(int ilaId, int empId);
        System.Threading.Tasks.Task UpdateIDPDate(UpdateIDPScheduleDateOptions options);
        System.Threading.Tasks.Task DeleteAsync(int id);
    }
}
