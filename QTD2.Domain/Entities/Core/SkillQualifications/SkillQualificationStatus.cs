using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class SkillQualificationStatus : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillQualificationStatus(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public SkillQualificationStatus()
        {
        }
    }
}
