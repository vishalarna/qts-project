using QTD2.Infrastructure.Model.Segment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA
{
    public class ILAPreviewVM
    {
        public string ProviderName { get; set; }
        public string ProviderNumber { get; set; }
        public string ContactName { get; set; }
        public string? ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string RepEmail { get; set; }
        public string ILAName { get; set; }
        public string ILANumber { get; set; }
        public DateOnly? StartDate { get; set; }
        public string Description { get; set; }
        public double? CEHHours { get; set; }
        public double? StandardReqHour { get; set; }
        public double? SimulationReqHour { get; set; }
        public List<ILAObjectivesVM> ILAObjectives { get; set; } = new List<ILAObjectivesVM>();
        public List<SegmentVM> Segments { get; set; } = new List<SegmentVM>();

        public ILAPreviewVM()
        {
        }

        public ILAPreviewVM(string providerName, string providerNumber, string contactName,
                            string? contactPhone, string contactEmail, string repEmail,
                            string ilaName, string ilaNumber,
                            DateOnly? startDate, string description, double? cEHHours, double? standardReqHour, double? simulationReqHour,
                            List<ILAObjectivesVM> ilaObjectives,
                            List<SegmentVM> segments)
        {
            ProviderName = providerName;
            ProviderNumber = providerNumber;
            ContactName = contactName;
            ContactPhone = contactPhone;
            ContactEmail = contactEmail;
            RepEmail = repEmail;
            ILAName = ilaName;
            ILANumber = ilaNumber;
            StartDate = startDate;
            Description = description;
            CEHHours = cEHHours;
            StandardReqHour = standardReqHour;
            SimulationReqHour = simulationReqHour;
            ILAObjectives = ilaObjectives ?? new List<ILAObjectivesVM>();
            Segments = segments ?? new List<SegmentVM>();
        }
    }

}
