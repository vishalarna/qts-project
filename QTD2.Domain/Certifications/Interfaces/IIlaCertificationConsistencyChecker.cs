using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Domain;
using QTD2.Domain.Certifications.Models;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Certifications.Interfaces
{
    public interface IILACertificationConsistencyChecker
    {
        List<CertifyingBodyConsistencyResult> CheckCertificationConsistency(ILA ila);
        CertifyingBodyConsistencyResult CheckCertificationConsistency(ILA ila, CertifyingBody certifyingBody);
    }
}
