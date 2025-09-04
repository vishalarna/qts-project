using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.CBT
{
    public class CBT_ScormRegistrationVM
    {
        public string LaunchLink { get; set; }
        public CBT_ScormRegistrationSuccess RegistrationSuccess { get; set; }
        public double? Score { get; set; }
        public string? Grade { get; set; }

        public CBT_ScormRegistrationVM(){}

        public CBT_ScormRegistrationVM(string launchLink,CBT_ScormRegistrationSuccess registrationSuccess,double? score,string? grade)
        {
            LaunchLink = launchLink;
            RegistrationSuccess = registrationSuccess;
            Score = score;
            Grade = grade;
        }
    }
}
