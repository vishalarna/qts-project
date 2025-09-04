using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class CreateRegistrationSchema
    {
            public string CourseId { get; set; }
            public LearnerSchema Learner { get; set; }
            public string RegistrationId { get; set; }
            public bool ForCredit { get; set; } = true;
            public string XapiRegistrationId { get; set; }
            public PostBack PostBack { get; set; }
            public RegistrationSchema InitialRegistrationState { get; set; }
            public SettingsPostSchema InitialSettings { get; set; }

        }
    
}
