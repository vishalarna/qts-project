using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.AdminMessageAuth
{
    public class AdminMessageAuthCreateOptions
    {
        public string Message {  get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
