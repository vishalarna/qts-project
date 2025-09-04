using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ProcedureReviewEmp
{
    public class ProcedureReviewEmpModel
    {
        public int ProcedureReviewId { get; set; }
        public string ProcedureNumber { get; set; }
        public string ProcedureName { get; set; }
        public string Comments { get; set; }
        public bool? IsEmployeeShowResponses { get; set; }

        public string ProcedureReviewTitle { get; set; }

        public DateTime ProcedureReviewDueDate { get; set; }

        public string Status { get; set; }

        public string Instructions { get; set; }

        public string Response { get; set; }
        public byte[] File { get; set; }

        public string HyperLink { get; set; }
        public string FileName { get; set; }

        public int ProcedureId { get; set; }
        public DateTime? ProcedureReviewCompletionDate { get; set; }
        public DateTime? EndDateTime { get; set; }
        public DateTime? StartDateTime { get; set; }
        public string Acknowledgement { get; set; }

        public ProcedureReviewEmpModel() { }

        public ProcedureReviewEmpModel(int procedureReviewId,string procedureNumber,string procedureName, bool? isEmployeeShowResponses, string procedureReviewTitle, DateTime procedureReviewDueDate, string status, string instructions, byte[] file, string hyperLink, string fileName, int procedureId, DateTime? procedureReviewCompletionDate, DateTime? endDateTime, DateTime? startDateTime, string acknowledgement)
        {
            ProcedureReviewId = procedureReviewId;
            ProcedureNumber = procedureNumber;
            ProcedureName = procedureName;
            IsEmployeeShowResponses = isEmployeeShowResponses;
            ProcedureReviewTitle = procedureReviewTitle;
            ProcedureReviewDueDate = procedureReviewDueDate;
            Status = status;
            Instructions = instructions;
            File = file;
            HyperLink = hyperLink;
            FileName = fileName;
            ProcedureId = procedureId;
            ProcedureReviewCompletionDate = procedureReviewCompletionDate;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            Acknowledgement = acknowledgement;
        }
    }
}
