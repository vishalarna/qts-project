using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class CreateUploadImportCourseSchema
    {
        public string CourseId { get; set; }
        public bool? MayCreateNewVersion { get { return true; } }
        public IFormFile File { get; set; }
        public string ContentMetadata { get; set; }
        public string UploadedContentType { get; set; }
        public string EngineTenantName { get; set; }

        public CreateUploadImportCourseSchema(string courseId, IFormFile file, string fileName, string engineTenantName)
        {
            CourseId = courseId;
            File = file;
            EngineTenantName = engineTenantName;
        }
    }
}
