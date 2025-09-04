using CsvHelper.Configuration;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Export
{
    public class ILAExport: ClassMap<ILA>
    {
        public ILAExport()
        {
            Map(m => m.Provider.Name).Name("contact_user_id");
            Map(m => m.Name).Name("entity.client_course_id");
            Map(m => m.Number).Name("entity.course_title");
            Map(m => m.StartDate).Name("start_date");
            Map(m => m.TrainingPlan).Name("course_description");
            //Map(m => m.ILA_NercStandard_Links.).Name("entity.operating_topics_ceh");
            //Map(m => m.StandardsCeh).Name("entity.standards_ceh");
            //Map(m => m.SimulationsCeh).Name("entity.simulations_ceh");
            //Map(m => m.).Name("criteria");
        }
    }
}
