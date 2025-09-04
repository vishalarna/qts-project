using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Application.Interfaces.Services.QTD
{
    public interface INotificationService
    {      
        Task<bool> SendReportAsync(Report report, string file, List<string> tos);       
    }
}
