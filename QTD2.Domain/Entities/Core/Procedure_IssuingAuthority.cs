using System;
using System.Collections.Generic;
using System.Linq;

namespace QTD2.Domain.Entities.Core
{
    public class Procedure_IssuingAuthority : Common.Entity
    {
        public string Title { get; set; }

        public string Website { get; set; }

        public DateOnly EffectiveDate { get; set; }

        public string Notes { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Procedure> Procedures { get; set; } = new List<Procedure>();

        public virtual ICollection<Proc_IssuingAuthority_History> IssuingAuthorityStatusHistories { get; set; }

        public Procedure_IssuingAuthority(string description, string title, string website, DateOnly effectiveDate, string notes, bool isActive, bool isDeleted)
        {
            Description = description;
            Title = title;
            Website = website;
            EffectiveDate = effectiveDate;
            Notes = notes;
            IsActive = isActive;
            IsDeleted = isDeleted;
        }

        public Procedure_IssuingAuthority()
        {
        }

        public Procedure AddProcedure(Procedure procedure)
        {
            var proc = Procedures.FirstOrDefault(x => x.Title.ToLower() == procedure.Title.ToLower());
            if (proc == null)
            {
                AddEntityToNavigationProperty<Procedure>(procedure);
            }

            return procedure;
        }

        public void RemoveProcedure(Procedure procedure)
        {
            var proc = Procedures.FirstOrDefault(x => x.Title.ToLower() == procedure.Title.ToLower());
            if (proc != null)
            {
                RemoveEntityFromNavigationProperty<Procedure>(procedure);
            }
        }
    }
}
