using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Certifications.Models
{
    public class CertifyingBodyConsistencyResult
    {
        public CertifyingBody CertifyingBody { get; set; }
        public bool IsConsistent { get { return (CertifyingBodyInconsistencies == null || CertifyingBodyInconsistencies.Count() == 0);  } }
        public List<CertifyingBodyInconsistency> CertifyingBodyInconsistencies { get; set; }

        public CertifyingBodyConsistencyResult(CertifyingBody certifyingBody)
        {
            CertifyingBody = certifyingBody;
        }

        public void SetInconsistency(string name, string message, bool displayName)
        {
            if (CertifyingBodyInconsistencies == null) CertifyingBodyInconsistencies = new List<CertifyingBodyInconsistency>();

            CertifyingBodyInconsistencies.Add(new CertifyingBodyInconsistency(name, message, displayName));
        }
    }
}
