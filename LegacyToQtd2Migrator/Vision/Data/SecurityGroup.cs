using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class SecurityGroup
{
    public decimal Id { get; set; }

    public virtual ICollection<DeveloperAuthorization> DeveloperAuthorizations { get; set; } = new List<DeveloperAuthorization>();

    public virtual ICollection<SecurityGroupImpl> SecurityGroupImplFkParentNavigations { get; set; } = new List<SecurityGroupImpl>();

    public virtual ICollection<SecurityGroupImpl> SecurityGroupImplFkSecurityGroupNavigations { get; set; } = new List<SecurityGroupImpl>();
}
