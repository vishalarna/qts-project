using System;

namespace QTD2.Domain.Entities.Common
{
    public interface IEntity
    {
        public int Id { get; }

        T AddEntityToNavigationProperty<T>(T entity)
            where T : IEntity;

        void RemoveEntityFromNavigationProperty<T>(T entity)
            where T : IEntity;

        public bool Deleted { get; }

        public bool Active { get; }

        public string CreatedBy { get; }

        public DateTime? CreatedDate { get; }

        public string ModifiedBy { get; }

        public DateTime? ModifiedDate { get; }

        void Deactivate();

        void Activate();

        void Delete();

        void Set_Id(int id);
    }
}
