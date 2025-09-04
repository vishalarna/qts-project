namespace QTD2.Infrastructure.Model.EnablingObjective
{
    public class EnablingObjectiveCreateOptions
    {
        public int CategoryId { get; set; }

        public int SubCategoryId { get; set; }

        public int? TopicId { get; set; }

        public string Number { get; set; }

        public string Statement { get; set; }

        public bool isMetaEO { get; set; }

        public bool HasChanges { get; set; }

        public bool IsSkill { get; set; }

        public string Conditions { get; set; }

        public string Criteria { get; set; }

        public string References { get; set; }

        public System.DateTime EffectiveDate { get; set; }
    }
}
