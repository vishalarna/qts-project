
using QTD2.Infrastructure.Model.CertifyingBody;

namespace QTD2.Test.Data.Infrastructure.Model.CertifyingBody
{
    public class CertifyingBodyUpdateOptionsTestData
    {
        public static CertifyingBodyUpdateOptions Null()
        {
            return null;
        }


        public static CertifyingBodyUpdateOptions Empty()
        {
            return new CertifyingBodyUpdateOptions()
            {

            };
        }

        public static CertifyingBodyUpdateOptions NoName()
        {
            return new CertifyingBodyUpdateOptions()
            {
                Name = ""
            };
        }

        public static CertifyingBodyUpdateOptions NERC()
        {
            return new CertifyingBodyUpdateOptions()
            {
                Name = "NERC"
            };
        }

        public static CertifyingBodyUpdateOptions TheNoDuplicateGuy()
        {
            return new CertifyingBodyUpdateOptions()
            {
                Name = "TheNoDuplicateGuy"
            };
        }

        public static CertifyingBodyUpdateOptions NameTooLong()
        {
            return new CertifyingBodyUpdateOptions()
            {
                Name = "NameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLong"
            };
        }
    }
}
