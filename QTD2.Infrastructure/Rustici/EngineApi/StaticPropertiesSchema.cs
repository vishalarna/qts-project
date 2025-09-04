using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class StaticPropertiesSchema
    {
        public string CompletionThreshold { get; set; }
        public string LaunchData { get; set; }
        public string MaxTimeAllowed { get; set; }
        public double ScaledPassingScore { get; set; }
        public bool ScaledPassingScoreUsed { get; set; }
        public string TimeLimitAction { get; set; }
    }
}
