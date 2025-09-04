using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Exceptions;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Model.PublicClassScheduleRequest;
using QTD2.Infrastructure.Model.PublicILA;

namespace QTD2.Application.Services.Shared
{
    public class PublicClassScheduleRequestService : Interfaces.Services.Shared.IPublicClassScheduleRequestService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly Domain.Interfaces.Service.Core.IPublicClassScheduleRequestService _publicClassScheduleRequestService;
        private readonly IClassScheduleEmployeeService _classScheduleEmployeeService;
        private readonly Domain.Interfaces.Service.Core.IILAService _iLAService;
        private readonly Domain.Interfaces.Service.Core.IPersonService _personService;

        public PublicClassScheduleRequestService(
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            UserManager<AppUser> userManager,
            Domain.Interfaces.Service.Core.IPublicClassScheduleRequestService publicClassScheduleRequestService,
            IClassScheduleEmployeeService classScheduleEmployeeService,
            Domain.Interfaces.Service.Core.IILAService iLAService,
            Domain.Interfaces.Service.Core.IPersonService personService
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _publicClassScheduleRequestService = publicClassScheduleRequestService;
            _classScheduleEmployeeService = classScheduleEmployeeService;
            _iLAService = iLAService;
            _personService = personService;
        }

        public Task<PublicClassScheduleRequest> ApproveRequestAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async System.Threading.Tasks.Task CreatePublicClassScheduleRequestAsync(PublicClassScheduleRequest request)
        {
            var lastRequest = await _publicClassScheduleRequestService.GetLastRequestByIpAsync(request.IpAddress);

            if (lastRequest != null && lastRequest.CreatedDate > DateTime.UtcNow.AddMinutes(-10))
                throw new QTDServerException("You must wait 10 minutes between requests", false, System.Net.HttpStatusCode.TooManyRequests);

            var requestAlreadyExist = await _publicClassScheduleRequestService.GetExistingRequestAsync(request.ClassScheduleId, request);
            var personAlreadyExist = await _personService.GetPersonByUserName(request.EmailAddress);
            if (personAlreadyExist != null)
            {
                throw new QTDServerException("Valued Member of our Training Community: Our records indicate that you have an account in our training platform. Complete your registration by logging into your Employee Portal account by clicking the button below.",false);
            }
            if (requestAlreadyExist != null && requestAlreadyExist.Status == PublicClassScheduleRequestStatus.Requested && requestAlreadyExist.ClassScheduleId == request.ClassScheduleId)
            {
                throw new QTDServerException("You currently have a pending request for this class at the chosen date and time. To proceed with registration, return to the Registration page and select a different class date.", false);
            }
            request.RequestPublicClassSchedule();
            await _publicClassScheduleRequestService.AddAsync(request);
        }

        public Task<PublicClassScheduleRequest> DenyRequestAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PublicClassScheduleRequestsVM>> GetAllActiveRequestsAsync()
        {
            var publicRequests = await _publicClassScheduleRequestService.GetAllPublicRequestsAsync();
            var publicClassRequests = new List<PublicClassScheduleRequestsVM>();
            foreach (var publicRequest in publicRequests)
            {
                  var publicClass = new PublicClassScheduleRequestsVM
                 {    
                      Id = publicRequest.Id,
                      FirstName = publicRequest.FirstName,
                      LastName = publicRequest.LastName,
                      EmailAddress = publicRequest.EmailAddress,
                      Company = publicRequest.Company,
                      NercCertNumber = publicRequest.NercCertNumber,
                      NercCertType = publicRequest.NercCertificationType,
                      ExpirationDate = publicRequest.CertificationExpirationDate,
                      PublicClassScheduleIla = new PublicClassScheduleIlaVM
                      {
                          IlaNumber = publicRequest.ClassSchedule.ILA.Number,
                          IlaTitle = publicRequest.ClassSchedule.ILA.Name,
                      },
                      PublicClassSchedule = new PublicClassScheduleVM
                      {
                          IlaId = publicRequest.ClassSchedule.ILAID,
                          ClassId = publicRequest.ClassScheduleId,
                          LocationName = publicRequest.ClassSchedule.Location.LocName,
                          InstructorName = publicRequest.ClassSchedule.Instructor.InstructorName,
                          StartDateTime = publicRequest.ClassSchedule.StartDateTime,
                          EndDateTime = publicRequest.ClassSchedule.EndDateTime,
                      }


                  };
                publicClassRequests.Add(publicClass);
            }

            return publicClassRequests;
        }

        public async Task<int> GetPublicRequestStatsAsync()
        {
            var publicClassRequests = await _publicClassScheduleRequestService.GetPublicRequestStatsAsync();
            return publicClassRequests;
        }

        public async Task<PublicClassScheduleRequest> UpdatePublicClassScheduleRequestAsync(int id,ModifyPublicClassScheduleRequestModel options)
        {
            var publicClassRequest = await _publicClassScheduleRequestService.GetPublicRequestById(id);
            var requestsByClassScheduleEmpId = await _publicClassScheduleRequestService.GetRequestsByClassScheduleEmployeeId(options.ClassScheduleEmployeeId);

            if (publicClassRequest != null && options.RequestedAction == PublicClassScheduleRequestAction.Deny)
            {
                publicClassRequest.DeclinePublicClassScheduleRequest();

            }
            else if (publicClassRequest != null && options.RequestedAction == PublicClassScheduleRequestAction.Accept)
            {
                if (requestsByClassScheduleEmpId == null)
                {
                    publicClassRequest.ClassScheduleEmployeeId = options.ClassScheduleEmployeeId;
                    publicClassRequest.FirstName = options.FirstName;
                    publicClassRequest.LastName = options.LastName;
                    publicClassRequest.EmailAddress = options.EmailAddress;
                    publicClassRequest.NercCertNumber = options.NercCertNumber;
                    publicClassRequest.Company = options.Company;
                    publicClassRequest.CertificationExpirationDate = options.ExpirationDate;
                    publicClassRequest.NercCertificationType = options.NercCertType;
                    publicClassRequest.ApprovePublicClassScheduleRequest();
                }
                else
                {
                    publicClassRequest.Status = PublicClassScheduleRequestStatus.Accepted;
                }
            }
            else
            {
                publicClassRequest.FirstName = options.FirstName;
                publicClassRequest.LastName = options.LastName;
                publicClassRequest.EmailAddress = options.EmailAddress;
                publicClassRequest.NercCertNumber = options.NercCertNumber;
                publicClassRequest.Company = options.Company;
                publicClassRequest.CertificationExpirationDate = options.ExpirationDate;
                publicClassRequest.NercCertificationType = options.NercCertType;
            }
            await _publicClassScheduleRequestService.UpdateAsync(publicClassRequest);
            return publicClassRequest;
        }

        public async Task<ILACompletionRequirementVM> GetILACompletionRequirementAsync(int id)
        {
            var iLACompletionRequirement = new ILACompletionRequirementVM();
            var ila = await _iLAService.GetILARequirementsAsync(id);

            foreach (var ilaTraineeEvaluationDesc in ila.ILATraineeEvaluations.Where(x => x.Active).Select(x => x.TestType.Description))
            {
                if (ilaTraineeEvaluationDesc == "Pretest")
                {
                    iLACompletionRequirement.IsPreTestRequired = true;
                }
                else if (ilaTraineeEvaluationDesc == "Final Test")
                {
                    iLACompletionRequirement.IsFinalTestRequired = true;
                }
            }

            if(ila.ILA_TaskObjective_Links.Where(x => x.ILAId == id && x.UseForTQ).Any())
            {
                iLACompletionRequirement.IsTaskQualificationRequired = true;
            }
            if (ila.CBTRequiredForCourse)
            {
                iLACompletionRequirement.IsCBTRequired = true;
            }
            if (ila.ILA_StudentEvaluation_Links.Any())
            {
                iLACompletionRequirement.IsStudentEvaluationRequired = true;
            }
            if (ila.SimulatorScenario_ILAs.Any())
            {
                iLACompletionRequirement.IsSimulatorScenarioRequired = true;
            }

            foreach (var classSchedule in ila.ClassSchedules)
            {
                var classSize = classSchedule.ClassSize;
                var enrolledEmployees = classSchedule.ClassSchedule_Employee.Count(x => x.ClassScheduleId == classSchedule.Id && x.IsEnrolled);
                var availableSeatCount = classSize - enrolledEmployees;
                iLACompletionRequirement.AvailableSeatsDetails.Add(new PublicClassAvailableSeatsVM
                {
                    ClassScheduleId = classSchedule.Id,
                    AvailableSeat = availableSeatCount
                });
            }
            
            return iLACompletionRequirement;
        }

    }
}
