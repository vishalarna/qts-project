using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class LearnerPreferenceSchema
    {
        public double AudioLevel { get; set; }
        public string Language { get; set; }
        public double DeliverySpeed { get; set; }
        public int AudioCaptioning { get; set; }
    }
}
