using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Version_EnablingObjective
{
    public class Version_EnablingObjectiveUpdateOptions
    {
        public int EnablingObjectiveId { get; set; }

        public string VersionNumber { get; set; }

        public int CategoryId { get; set; }

        public int SubCategoryId { get; set; }

        public int? TopicId { get; set; }

        public string Number { get; set; }

        public string Description { get; set; }

        public bool isMetaEO { get; set; }
    }
}
