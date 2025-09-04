using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using Xunit;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class ILATest
    {
        [Theory, MemberData(nameof(ILATest.GetILA_TrainingTopicTestData))]
        public void ILATest_LinkTrainingTopic(ILA ila, TrainingTopic tt)
        {
            var ilaCount = ila.ILA_TrainingTopic_Links.Count();
            ila.LinkTrainingTopic(tt);
            Assert.Equal(ilaCount + 1, ila.ILA_TrainingTopic_Links.Count());
        }

        [Theory, MemberData(nameof(ILATest.GetILA_TrainingTopicTestData))]
        public void ILATest_UnlinkTrainingTopic(ILA ila,TrainingTopic tt)
        {
            var ilaCount = ila.ILA_TrainingTopic_Links.Count();
            ila.LinkTrainingTopic(tt);
            ila.UnlinkTrainingTopic(tt);
            Assert.Equal(ilaCount, ila.ILA_TrainingTopic_Links.Count());
        }
    }
}
