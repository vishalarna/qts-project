using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsAddress
{
    public decimal Id { get; set; }

    public string Address1 { get; set; }

    public string Address2 { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Zip { get; set; }

    public string Country { get; set; }

    public string Phone { get; set; }

    public string Ext { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual ICollection<Learner> Learners { get; set; } = new List<Learner>();

    public virtual ICollection<LsCompany> LsCompanies { get; set; } = new List<LsCompany>();
}
