namespace QTD2.Domain.Entities.Core
{
    public class Version_SaftyHazard_Abatement : Common.Entity
    {
        public int Version_SaftyHazardId { get; set; }

        public string Description { get; set; }

        public int Number { get; set; }

        public virtual Version_SaftyHazard Version_SaftyHazard { get; set; }

        public Version_SaftyHazard_Abatement()
        {
        }

        public Version_SaftyHazard_Abatement(int version_SaftyHazardId, string description, int number)
        {
            Version_SaftyHazardId = version_SaftyHazardId;
            Description = description;
            Number = number;
        }
    }
}
