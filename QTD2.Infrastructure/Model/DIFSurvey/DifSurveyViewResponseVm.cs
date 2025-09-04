using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.DIFSurvey
{
    public class DifSurveyViewResponseVm
    {
        public string Title { get; set; }
        public string Instructions { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public List<DIFSurveyResponseVM> DIFSurveyResponseVM { get; set; }
        public string? AdditionalComments { get; set; }

        public DifSurveyViewResponseVm( DateTime? completedDate,string title, DateTime startDate,DateTime dueDate,List<DIFSurveyResponseVM> dIFSurveyResponseVM,string? instructions, string? additionalComments)
        {
            StartDate = startDate;
            DueDate = dueDate;
            CompletedDate = completedDate;
            Title = title;
            DIFSurveyResponseVM = dIFSurveyResponseVM;
            Instructions = instructions;
            AdditionalComments = additionalComments;
        }
    }
}
