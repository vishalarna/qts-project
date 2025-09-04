using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Certification;
using QTD2.Infrastructure.Model.CertifyingBody;

namespace QTD2.Infrastructure.Model.CertifyingBody
{
    public class CertifyingBodyCompactOptions
    {
        public QTD2.Domain.Entities.Core.CertifyingBody CertifyingBody { get; set; }

        public List<CertificationCompactOptions> CertificationCompactOptions { get; set; } = new List<CertificationCompactOptions>();
    }
}
