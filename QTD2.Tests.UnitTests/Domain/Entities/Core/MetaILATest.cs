using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Infrastructure.ExtensionMethods;
using QTD2.Test.Data.Domain.Entities.Core;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class MetaILATest
    {
        public static IEnumerable<object[]> GetMeta_ILAMembers_LinkTestData()
        {
            var data = new List<object[]>();
            var meta_ila = MetaILATestData.getAll();
            var ilas = ILATestData.getAll();
            var meta_ila_config = MetaILAConfigurationPublishOptionTestData.getAll();


            foreach (var m_ila in meta_ila)
            {
                foreach (var ila in ilas)
                {
                    foreach(var m_ila_config in meta_ila_config)
                    {
                        data.Add(new object[] { m_ila, ila, m_ila_config, 1});
                    }
                }
            }

            return data;
        }
    }
}
