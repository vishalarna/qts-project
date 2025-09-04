using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TimesheetSpecs
{
    public class Timesheet_NoteRequiredSpec : ISpecification<Timesheet>
    {
        public bool IsSatisfiedBy(Timesheet entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Note);
        }
    }
}
