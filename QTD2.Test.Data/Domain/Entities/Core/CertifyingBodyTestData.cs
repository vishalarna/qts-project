using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public class CertifyingBodyTestData
    {
        public static List<CertifyingBody> GetAll()
        {
            return new List<CertifyingBody>()
            {
                NoName(),
                NERC(),
                Null()
            };
        }

        public static CertifyingBody Null()
        {
            return null;
        }

        public static CertifyingBody NoName()
        {
            return new CertifyingBody("Test","testdesc", "testwebsite", System.DateTime.Now,false );
        }

        public static CertifyingBody NERC()
        {
            return new CertifyingBody("NERC", "NERCdesc", "NERCwebsite", System.DateTime.Now,false);
        }
    }
}
