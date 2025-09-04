using System;
using Microsoft.AspNetCore.Hosting;

using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Exceptions;

namespace QTD2.Infrastructure.Notification.Content
{
    public class DefaultContentGeneratorFactory : IContentGeneratorFactory
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public DefaultContentGeneratorFactory(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IContentGenerator GetGenerator(NotificationMethod notificationMethod)
        {
            if (notificationMethod == NotificationMethod.Email)
            {
                return new RazorEmailContentGenerator(_hostingEnvironment);
            }
            else if (notificationMethod == NotificationMethod.SMS)
            {
                return new SMSContentGenerator();
            }
            else
            {
                throw new QTDServerException("Content generator not found.");
            }
        }
    }
}
