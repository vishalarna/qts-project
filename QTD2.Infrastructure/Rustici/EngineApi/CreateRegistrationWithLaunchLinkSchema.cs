using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class CreateRegistrationWithLaunchLinkSchema
    {
        public CreateRegistrationSchema Registration { get; set; }
        public LaunchLinkRequestSchema LaunchLink { get; set; }

        public CreateRegistrationWithLaunchLinkSchema()
        {

        }

        public CreateRegistrationWithLaunchLinkSchema(CreateRegistrationSchema registrationSchema, LaunchLinkRequestSchema launchLinkSchema)
        {
            Registration = registrationSchema;
            LaunchLink = launchLinkSchema;
        }

        public CreateRegistrationWithLaunchLinkSchema(int cbtScormUploadId, int employeeId, string firstName, string lastName, int classScheduleEmployeeId)
        {
            Registration = new CreateRegistrationSchema()
            {
                CourseId = cbtScormUploadId.ToString(),
                Learner = new LearnerSchema()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Id = employeeId.ToString()
                },
                RegistrationId = $"{cbtScormUploadId}.{classScheduleEmployeeId}"
            };
        }
    }
}
