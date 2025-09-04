using QTD2.Infrastructure.Model.CertifyingBody;
using System.Collections.Generic;

namespace QTD2.Test.Data.Infrastructure.Model.CertifyingBody
{
    public class CertifyingBodyCreateOptionsTestData
    {
        public static List<CertifyingBodyCreateOptions> GetAll()
        {
            return new List<CertifyingBodyCreateOptions>()
            {
                Empty(),
                NoName(),
                NERC(),
                Null(),
                NameTooLong()
            };
        }

        public static CertifyingBodyCreateOptions Null()
        {
            return null;
        }


        public static CertifyingBodyCreateOptions Empty()
        {
            return new CertifyingBodyCreateOptions()
            {

            };
        }

        public static CertifyingBodyCreateOptions NoName()
        {
            return new CertifyingBodyCreateOptions()
            {
                Name = ""
            };
        }

        public static CertifyingBodyCreateOptions NERC()
        {
            return new CertifyingBodyCreateOptions()
            {
                Name = "NERC"
            };
        }

        public static CertifyingBodyCreateOptions NameTooLong()
        {
            return new CertifyingBodyCreateOptions()
            {
                Name = "NameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLong"
            };
        }
    }
}
