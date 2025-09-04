using System;
using System.Collections.Generic;
using System.Linq;

using QTD2.Domain.Events.Common;

namespace QTD2.Domain.Entities.Common
{
    public class Entity : IEntity
    {
        private List<IDomainEvent> _domainEvents;

        public int Id { get; set; }

        public bool Deleted { get; set; }

        public bool Active { get; set; } = true;

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public List<IDomainEvent> DomainEvents { get { return _domainEvents; } }

        public void AddDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public virtual void Activate()
        {
            Active = true;
        }

        public T AddEntityToNavigationProperty<T>(T entity)
            where T : IEntity
        {
            // get navigation property by T
            var collection = getCollection<T>();

            /* !! We don't have Id value here because the entity is not created in DB yet,
             need to figure out other way of comparison if needed, else its been done before calling this function */

            /*
            // Find entity in navigation property
             var item = collection.Where(r => r.Id == entity.Id).FirstOrDefault();

            // If entity exists in db, return dbEntity
            if (item != null)
                return item;
            */

            collection.Add(entity);

            return entity;
        }

        public virtual void Deactivate()
        {
            Active = false;
        }

        public virtual void Delete()
        {
            Deleted = true;
        }

        public void Restore()
        {
            Deleted = false;
        }

        public virtual void Modify(string username)
        {
            ModifiedBy = username;
            ModifiedDate = DateTime.Now;
        }

        public virtual void Create(string username)
        {
            CreatedBy = username;
            CreatedDate = DateTime.Now;
        }

        public void RemoveEntityFromNavigationProperty<T>(T entity)
            where T : IEntity
        {
            // get navigation property by T
            var collection = getCollection<T>();

            // try to find entity in navigation property
            var item = collection.Where(r => r.Id == entity.Id).FirstOrDefault();

            // if it exists, remove it
            if (item != null)
            {
                collection.Remove(item);
            }
        }

        public virtual T Copy<T>(string createdBy)
            where T : Entity, new()
        {
            var clone = new T();

            var t = typeof(T);
            foreach (var prop in t.GetProperties())
            {
                if (!shouldAlwaysClone(prop.PropertyType)) continue;

                string name = prop.Name;

                if (name == "CreatedBy")
                {
                    prop.SetValue(clone, createdBy);
                }

                else if (name == "CreatedDate")
                {
                    prop.SetValue(clone, DateTime.Now.ToUniversalTime());
                }

                else if (name == "UpdatedBy")
                {
                    prop.SetValue(clone, null);
                }

                else if (name == "UpdatedDate")
                {
                    prop.SetValue(clone, null);
                }

                else if (name == "Id")
                {
                    prop.SetValue(clone, 0);
                }

                else if(prop.CanWrite)
                {
                    var value = prop.GetValue(this, null);
                    prop.SetValue(clone, value);
                }
                else
                {
                    //no setter
                }
            }

            return clone;
        }

        private bool shouldAlwaysClone(Type t)
        {
            return new[] {
            typeof(string),
            typeof(char),
            typeof(char?),
            typeof(byte),
            typeof(byte?),
            typeof(sbyte),
            typeof(sbyte?),
            typeof(ushort),
            typeof(ushort?),
            typeof(short),
            typeof(short?),
            typeof(uint),
            typeof(uint?),
            typeof(int),
            typeof(int?),
            typeof(ulong),
            typeof(ulong?),
            typeof(long),
            typeof(long?),
            typeof(float),
            typeof(float?),
            typeof(double),
            typeof(double?),
            typeof(decimal),
            typeof(decimal?),
            typeof(bool),
            typeof(bool?),
            typeof(DateTime),
            typeof(DateTime?),
        }.Contains(t);
        }

        public void RemoveEntitiesFromNavigationProperty<T>(ICollection<T> entity)
            where T : IEntity
        {
            // get navigation property by T
            var collection = getCollection<T>();

            // if entity exists, remove it
            if (entity.Count != 0)
            {
                collection.Clear();
            }
        }

        // For Unit Testing purpose only ,
        public void Set_Id(int id)
        {
            Id = id;
        }

        // if we're going to stick with ICollection
        protected ICollection<T> getCollection<T>()
            where T : IEntity
        {
            var type = GetType();

            foreach (var prop in type.GetProperties())
            {
                if (typeof(ICollection<T>).IsAssignableFrom(prop.PropertyType))
                {
                    return prop.GetValue(this) as ICollection<T>;
                }
            }

            return null;
        }
    }
}
