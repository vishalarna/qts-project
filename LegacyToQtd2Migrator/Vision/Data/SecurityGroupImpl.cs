using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class SecurityGroupImpl
{
    public decimal Id { get; set; }

    public decimal FkSecurityGroup { get; set; }

    public string Text { get; set; }

    public string Description { get; set; }

    public decimal? AnalysisAccessLevel { get; set; }

    public decimal? ObjectiveAccessLevel { get; set; }

    public decimal? ProgramAccessLevel { get; set; }

    public decimal? TableAccessLevel { get; set; }

    public decimal? ReportAccessLevel { get; set; }

    public decimal? DocumentAccessLevel { get; set; }

    public decimal? TestAccessLevel { get; set; }

    public decimal? CanLockTest { get; set; }

    public decimal? CanOverrideTestLock { get; set; }

    public decimal? CanLockQues { get; set; }

    public decimal? CanOverrideQuesLock { get; set; }

    public decimal? CanChangeQuesStatus { get; set; }

    public decimal? CanChangeTaskStatus { get; set; }

    public decimal? CanChangeProgStatus { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal CanChangeQualcardStatus { get; set; }

    public decimal CanChangeCoursecatStatus { get; set; }

    public decimal CanChangeLabels { get; set; }

    public decimal CanChangeObStatus { get; set; }

    public decimal? FkParent { get; set; }

    public decimal? FkProject { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public decimal? CanApproveExam { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }

    public virtual SecurityGroup FkParentNavigation { get; set; }

    public virtual Project FkProjectNavigation { get; set; }

    public virtual SecurityGroup FkSecurityGroupNavigation { get; set; }
}
