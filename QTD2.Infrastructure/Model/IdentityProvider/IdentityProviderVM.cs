using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.IdentityProvider
{
    public class IdentityProviderVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public IdentityProviderVM() { }
        public IdentityProviderVM(int id,string name,string type) 
        {
            Id = id;
            Name = name;
            Type = type;
        }
    }
}
