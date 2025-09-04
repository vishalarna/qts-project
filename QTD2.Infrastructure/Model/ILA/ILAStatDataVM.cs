using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA
{
    public class ILAStatDataVM
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string DeliveryMethodName { get; set; }

        public ILAStatDataVM()
        {
        }

        public ILAStatDataVM(string number, string name, string nickName, string deliveryMethodName)
        {
            Number = number;
            Name = name;
            NickName = nickName;
            DeliveryMethodName = deliveryMethodName;
        }
    }

}
