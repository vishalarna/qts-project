using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.DIFSurvey
{
    public class DifSurveyOverview_VM
    {
        public int PublishedDIFSurveys { get; set; }
        public int DraftDIFSurveys { get; set; }
        public int EmployeesPendingEMPDIFSurveys { get; set; }
        public List<DIFSurveyOverview_DIFSurvey_VM> DIFSurveys { get; set; } = new List<DIFSurveyOverview_DIFSurvey_VM>();

        public DifSurveyOverview_VM(int publishedDIFSurveys, int draftDIFSurveys, int employeesPendingEMPDIFSurveys, List<DIFSurveyOverview_DIFSurvey_VM> dIFSurveys)
        {
            PublishedDIFSurveys = publishedDIFSurveys;
            DraftDIFSurveys = draftDIFSurveys;
            EmployeesPendingEMPDIFSurveys = employeesPendingEMPDIFSurveys;
            DIFSurveys = dIFSurveys;
        }
    }
}
