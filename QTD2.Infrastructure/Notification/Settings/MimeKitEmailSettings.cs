using System.Collections.Generic;

namespace QTD2.Infrastructure.Notification.Settings
{
    public class MimeKitEmailSettings
    {
        public string DefaultFrom { get; set; }

        public List<string> BCC { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Server { get; set; }

        public int Port { get; set; }
    }
}
