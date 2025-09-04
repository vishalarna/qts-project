using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsCertManscreCrdit
{
    public decimal Id { get; set; }

    public decimal FkManscreCrditmain { get; set; }

    public decimal FkLearner { get; set; }

    public decimal FkLsCertLrnrRecordBycrse { get; set; }

    public decimal FkLsCertLrRcrdRuleorreqid { get; set; }

    public decimal Isrule { get; set; }

    public decimal AmountCredited { get; set; }

    public decimal AmountCarryover { get; set; }

    public decimal AmountLost { get; set; }

    public decimal AmountCreditedByrecdate { get; set; }

    public decimal AmountCarryoverByrecdate { get; set; }

    public decimal AmountLostByrecdate { get; set; }

    public decimal AmountReqLost { get; set; }

    public decimal AmountReqGraceperiod { get; set; }

    public decimal FkCreatedby { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkLastModifiedby { get; set; }

    public DateTime DateModified { get; set; }

    public virtual Learner FkCreatedbyNavigation { get; set; }

    public virtual Learner FkLastModifiedbyNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsCertLrnrRecordBycrse FkLsCertLrnrRecordBycrseNavigation { get; set; }

    public virtual LsCertManscreCrditmain FkManscreCrditmainNavigation { get; set; }
}
