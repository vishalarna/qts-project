using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Authentication
{
    public class SelectInstanceEvent
    {
        public string Instance { get; set; }
        public SelectInstanceEvent(string instance)
        {
            Instance = instance;
        }
    }
}
