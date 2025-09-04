using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class OrganizationTestData
    {
        public static List<Organization> GetAll()
        {
            return new List<Organization>()
            {
                Org1(), Org2()
            };
        }
        static Organization Org1()
        {
            return new Organization("West Division");
        }

        static Organization Org2()
        {
            return new Organization("North Division");
        }

    }
}
