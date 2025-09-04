using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IEMPReleaseCheckService
    {
        public System.Threading.Tasks.Task CheckEvaluationRelease();

        public System.Threading.Tasks.Task CheckTestRelease();

    }
}
