using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public class MetaILAConfigurationPublishOptionTestData
    {
        public static List<MetaILAConfigurationPublishOption> getAll()
        {
            return new List<MetaILAConfigurationPublishOption> {
                metaILAConfiguration01()
            };
        }

        static MetaILAConfigurationPublishOption metaILAConfiguration01()
        {
            var metaILAConfig = new MetaILAConfigurationPublishOption("Upon Clicking");
            metaILAConfig.Set_Id(1);
            return metaILAConfig;
        }
    }
}
