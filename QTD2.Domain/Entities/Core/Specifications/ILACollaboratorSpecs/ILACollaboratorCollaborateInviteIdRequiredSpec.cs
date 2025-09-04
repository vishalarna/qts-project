using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ILACollaboratorSpecs
{
    public class ILACollaboratorCollaborateInviteIdRequiredSpec : ISpecification<ILACollaborator>
    {
        public bool IsSatisfiedBy(ILACollaborator entity, params object[] args)
        {
            return entity.CollaboratorInviteId > 0;
        }
    }
}
