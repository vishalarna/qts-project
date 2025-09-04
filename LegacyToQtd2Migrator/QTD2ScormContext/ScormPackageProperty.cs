using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class ScormPackageProperty
    {
        public int ScormPackageId { get; set; }
        public bool ShowFinishButton { get; set; }
        public bool ShowCourseStructure { get; set; }
        public bool ShowProgressBar { get; set; }
        public bool ShowHelp { get; set; }
        public bool ShowNavBar { get; set; }
        public bool ShowTitlebar { get; set; }
        public bool EnableFlowNav { get; set; }
        public bool EnableChoiceNav { get; set; }
        public int CourseStructureWidth { get; set; }
        public bool CourseStructureStartsOpen { get; set; }
        public int ScoLaunchType { get; set; }
        public int PlayerLaunchType { get; set; }
        public bool PreventRightClick { get; set; }
        public bool PreventWindowResize { get; set; }
        public int RequiredWidth { get; set; }
        public int RequiredHeight { get; set; }
        public bool RequiredFullscreen { get; set; }
        public int DesiredWidth { get; set; }
        public int DesiredHeight { get; set; }
        public bool DesiredFullscreen { get; set; }
        public int IntSatLogoutAction { get; set; }
        public int IntSatNormalAction { get; set; }
        public int IntSatSuspendAction { get; set; }
        public int IntSatTimeoutAction { get; set; }
        public int IntNotSatLogoutAction { get; set; }
        public int IntNotSatNormalAction { get; set; }
        public int IntNotSatSuspendAction { get; set; }
        public int IntNotSatTimeoutAction { get; set; }
        public int FinalSatLogoutAction { get; set; }
        public int FinalSatNormalAction { get; set; }
        public int FinalSatSuspendAction { get; set; }
        public int FinalSatTimeoutAction { get; set; }
        public int FinalNotSatLogoutAction { get; set; }
        public int FinalNotSatNormalAction { get; set; }
        public int FinalNotSatSuspendAction { get; set; }
        public int FinalNotSatTimeoutAction { get; set; }
        public int StatusDisplay { get; set; }
        public int ScoreRollupMode { get; set; }
        public int? NumberOfScoringObjects { get; set; }
        public int StatusRollupMode { get; set; }
        public decimal? ThresholdScoreForCompletion { get; set; }
        public bool FirstScoIsPretest { get; set; }
        public bool WrapScoWindowWithApi { get; set; }
        public bool FinishCausesImmediateCommit { get; set; }
        public bool DebugControlAudit { get; set; }
        public bool DebugControlDetailed { get; set; }
        public bool DebugRteAudit { get; set; }
        public bool DebugRteDetailed { get; set; }
        public bool DebugSequencingAudit { get; set; }
        public bool DebugSequencingDetailed { get; set; }
        public bool DebugLookaheadAudit { get; set; }
        public bool DebugLookaheadDetailed { get; set; }
        public bool DebugIncludeTimestamps { get; set; }
        public int CommMaxFailedSubmissions { get; set; }
        public int CommCommitFrequency { get; set; }
        public int InvalidMenuItemAction { get; set; }
        public bool AlwaysFlowToFirstSco { get; set; }
        public bool LogoutCausesPlayerExit { get; set; }
        public int ResetRtTiming { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public int OfflineSynchMode { get; set; }
        public bool? ValidateInteractionResponses { get; set; }
        public int LookaheadSequencerMode { get; set; }
        public bool ShowCloseItem { get; set; }
        public bool ScoreOverridesStatus { get; set; }
        public bool ScaleRawScore { get; set; }
        public bool RollupEmptySetToUnknown { get; set; }
        public bool CaptureHistory { get; set; }
        public bool CaptureHistoryDetailed { get; set; }
        public int ReturnToLmsAction { get; set; }
        public bool UseMeasureProgressBar { get; set; }
        public bool? UseQuickLookaheadSeq { get; set; }
        public bool ForceDisableRootChoice { get; set; }
        public bool RollupRuntimeAtScoUnload { get; set; }
        public bool ForceObjComplSetByContent { get; set; }
        public bool InvokeRollupAtSuspendall { get; set; }
        public int ComplStatOfFailedSucStat { get; set; }
        public bool SatisfiedCausesCompletion { get; set; }
        public bool StudentPrefsGlobalToCourse { get; set; }
        public bool DebugSequencingSimple { get; set; }
        public int SuspendDataMaxLength { get; set; }
        public int AllowCompleteStatusChange { get; set; }
        public int TimeLimit { get; set; }
        public int LaunchComplRegsAsNoCredit { get; set; }
        public int ApplyStatusToSuccess { get; set; }
        public short EngineTenantId { get; set; }
        public int IeCompatibilityMode { get; set; }
        public bool IsAvailableOffline { get; set; }
    }
}
