using QTD2.Infrastructure.Model.ILA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA_Topic
{
    public class ILATopicDataVM
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public bool TopicActive { get; set; }
        public List<ILADetailsVM> ILADetails = new List<ILADetailsVM>();
        public ILATopicDataVM(){}
        public ILATopicDataVM(int topicId, string topicName,bool topicActive, List<ILADetailsVM> iLADetails)
        {
            TopicId = topicId;
            TopicName = topicName;
            ILADetails = iLADetails;
            TopicActive = topicActive;
        }
    }
}
