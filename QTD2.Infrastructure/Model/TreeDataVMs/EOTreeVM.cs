using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TreeDataVMs
{
    public class EOTreeVM
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Number { get; set; }

        public bool Active { get; set; }

        public bool IsMeta { get; set; }

        public bool IsSkillQualification { get; set; }
    }

    public class EOSubCatTreeVM
    {
        public int Id { get; set; }

        public int? Number { get; set; }

        public bool Active { get; set; }

        public string Title { get; set; }

        public List<EOTreeVM> EnablingObjectives = new List<EOTreeVM>();

        public List<EOTopicTreeVM> EnablingObjective_Topics = new List<EOTopicTreeVM>();
    }

    public class EOTopicTreeVM
    {
        public int Id { get; set; }

        public int? Number { get; set; }

        public bool Active { get; set; }

        public string Title { get; set; }

        public List<EOTreeVM> EnablingObjectives = new List<EOTreeVM>();
    }

    public class EOCatTreeVM
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public bool Active { get; set; }

        public string Title { get; set; }

        public List<EOSubCatTreeVM> EnablingObjective_SubCategories = new List<EOSubCatTreeVM>();
    }
}
