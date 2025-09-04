using System;
using System.Collections.Generic;

namespace QTD2.Domain.Entities.Core
{
    public class Employee_ScormUpload : Common.Entity
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int ScormUploadId { get; set; }

        public int Grade { get; set; }

        public string PassOrFail { get; set; }

        public DateTime LastAccessDate { get; set; }

        public DateTime CompletedDate { get; set; }

        public string Status { get; set; }
    } 
}
