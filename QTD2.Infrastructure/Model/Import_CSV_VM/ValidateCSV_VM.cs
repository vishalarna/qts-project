using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Import_CSV_VM
{
    public class ValidateCSV_VM
    {
        public IFormFile File { get; set; }
        public bool ReturnFile { get; set; }
    }

}
