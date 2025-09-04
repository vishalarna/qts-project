using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.PublicClassScheduleRequest
{
    public class PublicClassScheduleIlaVM
    {
        public string IlaNumber { get; set; }
        public string IlaTitle { get; set; }
        public PublicClassScheduleIlaVM()
        {

        }
        public PublicClassScheduleIlaVM(string ilaNumber, string ilaTitle)
        {
            IlaTitle = ilaTitle;
            IlaNumber = ilaNumber;
        }
    }
}
