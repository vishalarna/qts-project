using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Test
{
    public class TestDataVM
    {
        public int Id { get; set; }
        public int? ProviderId { get; set; }
        public int? ILAId { get; set; }
        public string TestTitle { get; set; }
        public bool Active { get; set; }

        public TestDataVM()
        {
        }

        public TestDataVM(int id, int? providerId, int? ilaId, string testTitle, bool active)
        {
            Id = id;
            ProviderId = providerId;
            ILAId = ilaId;
            TestTitle = testTitle;
            Active = active;
        }
    }

}
