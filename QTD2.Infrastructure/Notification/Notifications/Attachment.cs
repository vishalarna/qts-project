using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Notification.Notifications
{
    public class Attachment
    {
        public string SourceFile { get; set; }

        public Attachment(string file)
        {
            SourceFile = file;
        }
    }
}
