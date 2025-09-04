using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA
{
    public class ProviderIlaVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public List<IlaVM> ILAs { get; set; }
    }

    public class IlaVM 
    {
        public int Id { get; set; }
        public int providerId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
