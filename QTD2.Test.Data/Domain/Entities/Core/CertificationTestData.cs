using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class CertificationTestData
    {
        public static List<Certification> GetAll()
        {
            return new List<Certification> { Cert1(), Cert2() };
        }

        static Certification Cert1()
        {

            //return new Certification("Training Supervisor", 1,"111", "mydesc",true, true, true, true, 2, true, 1);
            return new Certification(1, "TSS", "Training Supervisor", "my tss desc", true, 2, false, 1, true, "testsubname", 1, true, true, true, true, false, 1, DateTime.Now);
        }

        static Certification Cert2()
        {
            return new Certification(1, "TSS", "Training Supervisor", "my tss desc", true, 2, false, 1, true, "testsubname", 1, true, true, true, true, false, 1, DateTime.Now);
        }
    }
}
