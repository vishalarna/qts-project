using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsTrainingReqItem
{
    public decimal Id { get; set; }

    public string UserDefinedId { get; set; }

    public DateTime DateCreated { get; set; }

    public string Description { get; set; }

    public DateTime? DateDue { get; set; }

    public decimal FkStatus { get; set; }

    public decimal FkLearner { get; set; }

    public string Source { get; set; }

    public decimal? FkSourceCatery { get; set; }

    public DateTime? DateSourceDue { get; set; }

    public decimal? FkDeveloperAssignedTo { get; set; }

    public DateTime? DateAssigned { get; set; }

    public decimal? FkActionType { get; set; }

    public DateTime? DateActionDue { get; set; }

    public string ResolutionAction { get; set; }

    public DateTime? DateResolved { get; set; }

    public decimal? FkDeveloperResolvedBy { get; set; }

    public decimal? FkCompany { get; set; }

    public string Title { get; set; }

    public virtual LsTrainingReqActionType FkActionTypeNavigation { get; set; }

    public virtual LsCompany FkCompanyNavigation { get; set; }

    public virtual Developer FkDeveloperAssignedToNavigation { get; set; }

    public virtual Developer FkDeveloperResolvedByNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsTrainingReqSourceCatery FkSourceCateryNavigation { get; set; }

    public virtual LsTrainingReqStatus FkStatusNavigation { get; set; }

    public virtual ICollection<LsTrainingReqComment> LsTrainingReqComments { get; set; } = new List<LsTrainingReqComment>();

    public virtual ICollection<LsTrainingReqLink> LsTrainingReqLinks { get; set; } = new List<LsTrainingReqLink>();
}
