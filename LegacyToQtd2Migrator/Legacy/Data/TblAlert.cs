using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblAlert
    {
        public int Id { get; set; }
        public string Area { get; set; }
        public int AreaEntityId { get; set; }
        public bool Dismissed { get; set; }
        public DateTime? DismissedOn { get; set; }
        public DateTime? DueDate { get; set; }
        public string Name { get; set; }
        public int Eid { get; set; }
        public DateTime? StartDate { get; set; }

        public virtual TblEmployee EidNavigation { get; set; }
    }
}
