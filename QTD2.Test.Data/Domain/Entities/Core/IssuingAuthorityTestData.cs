using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class IssuingAuthorityTestData
    {
        public static List<Procedure_IssuingAuthority> GetAll()
        {
            return new List<Procedure_IssuingAuthority>()
            {
                Proc_IA1()
            };
        }

        static Procedure_IssuingAuthority Proc_IA1()
        {
            return new Procedure_IssuingAuthority("Issuer for some Procedure","Title","Website", DateOnly.FromDateTime(System.DateTime.Now),"NOTES",true,false);
        }
    }
}
