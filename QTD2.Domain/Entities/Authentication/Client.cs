using System.Collections.Generic;

namespace QTD2.Domain.Entities.Authentication
{
    public class Client : Common.Entity
    {
        public string Name { get; set; }

        public virtual ICollection<Instance> Instances { get; set; }

        public Client(string name)
        {
            Name = name;
        }

        public Client()
        {
        }

        private void Update(string name)
        {
        }
    }
}
