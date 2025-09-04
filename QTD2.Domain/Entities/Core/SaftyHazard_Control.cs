namespace QTD2.Domain.Entities.Core
{
    public class SaftyHazard_Control : Common.Entity
    {
        public int SaftyHazardId { get; set; }

        public int? Number { get; set; }

        public string Description { get; set; }

        public virtual SaftyHazard SaftyHazard { get; set; }

        public SaftyHazard_Control(int saftyHazardId, int number, string description)
        {
            SaftyHazardId = saftyHazardId;
            Number = number;
            Description = description;
        }

        public SaftyHazard_Control()
        {
        }
    }
}
