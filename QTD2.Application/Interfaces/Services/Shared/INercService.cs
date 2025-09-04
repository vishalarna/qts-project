using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using QTD2.Infrastructure.Model.Nerc;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface INercService
    {
        Task<List<CehUploadResultModel>> GetCehUploadAsync(CehUploadGetOptions options);
    }
}
