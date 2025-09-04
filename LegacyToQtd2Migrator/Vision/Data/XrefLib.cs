using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class XrefLib
{
    public decimal Id { get; set; }

    public virtual ICollection<LsCertLrnrRecordBycrse> LsCertLrnrRecordBycrses { get; set; } = new List<LsCertLrnrRecordBycrse>();

    public virtual ICollection<LsCertRequirement> LsCertRequirements { get; set; } = new List<LsCertRequirement>();

    public virtual ICollection<LsCertification> LsCertifications { get; set; } = new List<LsCertification>();

    public virtual ICollection<RevisionLogXref> RevisionLogXrefs { get; set; } = new List<RevisionLogXref>();

    public virtual ICollection<XrefLibHtml> XrefLibHtmls { get; set; } = new List<XrefLibHtml>();

    public virtual ICollection<XrefLibImpl> XrefLibImplFkParentNavigations { get; set; } = new List<XrefLibImpl>();

    public virtual ICollection<XrefLibImpl> XrefLibImplFkXrefLibNavigations { get; set; } = new List<XrefLibImpl>();

    public virtual ICollection<XrefLibLink> XrefLibLinkFkItemNavigations { get; set; } = new List<XrefLibLink>();

    public virtual ICollection<XrefLibLink> XrefLibLinkFkParentNavigations { get; set; } = new List<XrefLibLink>();

    public virtual ICollection<LsCompany> FkCompanies { get; set; } = new List<LsCompany>();
}
