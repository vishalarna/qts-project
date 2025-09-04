using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.QtdUser
{
    public class QtdUserVM
    {
        public int? Id { get; set; }
        public QTD2.Domain.Entities.Core.Person Person { get; set; }

        public QtdUserVM()
        {
           
        }
        public QtdUserVM(int id, QTD2.Domain.Entities.Core.Person person)
        {
            Id = id;
            Person = person;
        }
    }
}
