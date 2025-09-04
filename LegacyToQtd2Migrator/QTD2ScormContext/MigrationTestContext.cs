using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class MigrationTestContext : DbContext
    {
        public MigrationTestContext()
        {
        }

        public MigrationTestContext(DbContextOptions<MigrationTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cbt> Cbts { get; set; }
        public virtual DbSet<CbtScormRegistration> CbtScormRegistrations { get; set; }
        public virtual DbSet<CbtScormUpload> CbtScormUploads { get; set; }
        public virtual DbSet<ClassSchedule> ClassSchedules { get; set; }
        public virtual DbSet<ClassScheduleEmployee> ClassScheduleEmployees { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Ila> Ilas { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<ScormActivity> ScormActivities { get; set; }
        public virtual DbSet<ScormActivityObjective> ScormActivityObjectives { get; set; }
        public virtual DbSet<ScormActivityRt> ScormActivityRts { get; set; }
        public virtual DbSet<ScormActivityRtcomment> ScormActivityRtcomments { get; set; }
        public virtual DbSet<ScormActivityRtintCorrectResp> ScormActivityRtintCorrectResps { get; set; }
        public virtual DbSet<ScormActivityRtintLearnerResp> ScormActivityRtintLearnerResps { get; set; }
        public virtual DbSet<ScormActivityRtintObjective> ScormActivityRtintObjectives { get; set; }
        public virtual DbSet<ScormActivityRtinteraction> ScormActivityRtinteractions { get; set; }
        public virtual DbSet<ScormActivityRtobjective> ScormActivityRtobjectives { get; set; }
        public virtual DbSet<ScormAiccSession> ScormAiccSessions { get; set; }
        public virtual DbSet<ScormApiLearner> ScormApiLearners { get; set; }
        public virtual DbSet<ScormApiRegToLearner> ScormApiRegToLearners { get; set; }
        public virtual DbSet<ScormLaunchHistory> ScormLaunchHistories { get; set; }
        public virtual DbSet<ScormMetadatum> ScormMetadata { get; set; }
        public virtual DbSet<ScormObject> ScormObjects { get; set; }
        public virtual DbSet<ScormObjectHierarchy> ScormObjectHierarchies { get; set; }
        public virtual DbSet<ScormObjectIdentifier> ScormObjectIdentifiers { get; set; }
        public virtual DbSet<ScormObjectSeqDatum> ScormObjectSeqData { get; set; }
        public virtual DbSet<ScormObjectSeqObjective> ScormObjectSeqObjectives { get; set; }
        public virtual DbSet<ScormObjectSeqObjectiveMap> ScormObjectSeqObjectiveMaps { get; set; }
        public virtual DbSet<ScormObjectSeqRollupRule> ScormObjectSeqRollupRules { get; set; }
        public virtual DbSet<ScormObjectSeqRollupRuleCond> ScormObjectSeqRollupRuleConds { get; set; }
        public virtual DbSet<ScormObjectSeqRule> ScormObjectSeqRules { get; set; }
        public virtual DbSet<ScormObjectSeqRuleCond> ScormObjectSeqRuleConds { get; set; }
        public virtual DbSet<ScormObjectSharedDataMap> ScormObjectSharedDataMaps { get; set; }
        public virtual DbSet<ScormObjectSspbucket> ScormObjectSspbuckets { get; set; }
        public virtual DbSet<ScormObjectStore> ScormObjectStores { get; set; }
        public virtual DbSet<ScormPackage> ScormPackages { get; set; }
        public virtual DbSet<ScormPackageToLtiContextId> ScormPackageToLtiContextIds { get; set; }
        public virtual DbSet<ScormRegistration> ScormRegistrations { get; set; }
        public virtual DbSet<ScormRegistrationGlobalObj> ScormRegistrationGlobalObjs { get; set; }
        public virtual DbSet<ScormRegistrationSharedDataVal> ScormRegistrationSharedDataVals { get; set; }
        public virtual DbSet<ScormRegistrationSharedDatum> ScormRegistrationSharedData { get; set; }
        public virtual DbSet<ScormRegistrationSspbucket> ScormRegistrationSspbuckets { get; set; }
        public virtual DbSet<ScormRegistrationStatementMap> ScormRegistrationStatementMaps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=WSAMZN-MO1QATJM\\SQL2012;Initial Catalog=QTDMigrationTest; Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cbt>(entity =>
            {
                entity.ToTable("CBTs");

                entity.HasIndex(e => e.Ilaid, "IX_CBTs_ILAId");

                entity.Property(e => e.CbtlearningContractInstructions).HasColumnName("CBTLearningContractInstructions");

                entity.Property(e => e.Deleted)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.Ilaid).HasColumnName("ILAId");

                entity.HasOne(d => d.Ila)
                    .WithMany(p => p.Cbts)
                    .HasForeignKey(d => d.Ilaid);
            });

            modelBuilder.Entity<CbtScormRegistration>(entity =>
            {
                entity.ToTable("CBT_ScormRegistration");

                entity.HasIndex(e => e.ClassScheduleEmployeeId, "IX_CBT_ScormRegistration_ClassScheduleEmployeeId")
                    .IsUnique();

                entity.Property(e => e.CbtscormUploadId).HasColumnName("CBTScormUploadId");

                entity.HasOne(d => d.ClassScheduleEmployee)
                    .WithOne(p => p.CbtScormRegistration)
                    .HasForeignKey<CbtScormRegistration>(d => d.ClassScheduleEmployeeId);

                entity.HasOne(d => d.ScormUpload)
                    .WithMany(p => p.CbtScormRegistrations)
                    .HasForeignKey(d => d.CbtscormUploadId);
            });

            modelBuilder.Entity<CbtScormUpload>(entity =>
            {
                entity.ToTable("CBT_ScormUpload");

                entity.HasIndex(e => e.CbtId, "IX_CBT_ScormUpload_CbtId");

                entity.HasOne(d => d.Cbt)
                    .WithMany(p => p.CbtScormUploads)
                    .HasForeignKey(d => d.CbtId);
            });

            modelBuilder.Entity<ClassSchedule>(entity =>
            {
                entity.HasIndex(e => e.Ilaid, "IX_ClassSchedules_ILAID");

                entity.HasIndex(e => e.InstructorId, "IX_ClassSchedules_InstructorId");

                entity.HasIndex(e => e.LocationId, "IX_ClassSchedules_LocationId");

                entity.HasIndex(e => e.ProviderId, "IX_ClassSchedules_ProviderID");

                entity.HasIndex(e => e.RecurrenceId, "IX_ClassSchedules_RecurrenceId");

                entity.Property(e => e.Deleted)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.Ilaid).HasColumnName("ILAID");

                entity.Property(e => e.IsRecurring)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.ProviderId).HasColumnName("ProviderID");

                entity.HasOne(d => d.Ila)
                    .WithMany(p => p.ClassSchedules)
                    .HasForeignKey(d => d.Ilaid);

                entity.HasOne(d => d.Recurrence)
                    .WithMany(p => p.InverseRecurrence)
                    .HasForeignKey(d => d.RecurrenceId);
            });

            modelBuilder.Entity<ClassScheduleEmployee>(entity =>
            {
                entity.HasIndex(e => e.CbtstatusId, "IX_ClassScheduleEmployees_CBTStatusId");

                entity.HasIndex(e => e.ClassScheduleId, "IX_ClassScheduleEmployees_ClassScheduleId");

                entity.HasIndex(e => e.EmployeeId, "IX_ClassScheduleEmployees_EmployeeId");

                entity.HasIndex(e => e.PreTestStatusId, "IX_ClassScheduleEmployees_PreTestStatusId");

                entity.HasIndex(e => e.RetakeStatusId, "IX_ClassScheduleEmployees_RetakeStatusId");

                entity.HasIndex(e => e.TestId, "IX_ClassScheduleEmployees_TestId");

                entity.HasIndex(e => e.TestStatusId, "IX_ClassScheduleEmployees_TestStatusId");

                entity.Property(e => e.CbtstatusId).HasColumnName("CBTStatusId");

                entity.Property(e => e.Deleted)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.ClassSchedule)
                    .WithMany(p => p.ClassScheduleEmployees)
                    .HasForeignKey(d => d.ClassScheduleId);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ClassScheduleEmployees)
                    .HasForeignKey(d => d.EmployeeId);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.PersonId, "IX_Employees_PersonId")
                    .IsUnique();

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Deleted)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.EmployeeNumber).IsRequired();

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.State).HasMaxLength(50);

                entity.Property(e => e.Tqequlator).HasColumnName("TQEqulator");

                entity.Property(e => e.WorkLocation).HasMaxLength(50);

                entity.HasOne(d => d.Person)
                    .WithOne(p => p.Employee)
                    .HasForeignKey<Employee>(d => d.PersonId);
            });

            modelBuilder.Entity<Ila>(entity =>
            {
                entity.ToTable("ILAs");

                entity.HasIndex(e => e.DeliveryMethodId, "IX_ILAs_DeliveryMethodId");

                entity.HasIndex(e => e.ProviderId, "IX_ILAs_ProviderId");

                entity.Property(e => e.CbtrequiredForCourse).HasColumnName("CBTRequiredForCourse");

                entity.Property(e => e.Deleted)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.HasPilotData).HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.IsOptional)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.Property(e => e.IsProgramManual)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.IsPublished)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.IsSelfPaced)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.NickName).HasMaxLength(500);

                entity.Property(e => e.UseForEmp)
                    .IsRequired()
                    .HasColumnName("UseForEMP")
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Deleted)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<ScormActivity>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormActivityId })
                    .HasName("ScormActivity_pkey");

                entity.ToTable("ScormActivity");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormObjectId }, "IX_SA_object_id");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormRegistrationId }, "IX_ScormActivity");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormActivityId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("scorm_activity_id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.ActivityAbsoluteDur)
                    .HasColumnName("activity_absolute_dur")
                    .HasDefaultValueSql("((-1))");

                entity.Property(e => e.ActivityAttemptCount).HasColumnName("activity_attempt_count");

                entity.Property(e => e.ActivityExpDurReported).HasColumnName("activity_exp_dur_reported");

                entity.Property(e => e.ActivityExpDurTracked).HasColumnName("activity_exp_dur_tracked");

                entity.Property(e => e.ActivityProgressStatus).HasColumnName("activity_progress_status");

                entity.Property(e => e.ActivityStartTimestampUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("activity_start_timestamp_utc");

                entity.Property(e => e.AiccSessionId)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("aicc_session_id");

                entity.Property(e => e.AttemptAbsoluteDur).HasColumnName("attempt_absolute_dur");

                entity.Property(e => e.AttemptCompletionAmount)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("attempt_completion_amount");

                entity.Property(e => e.AttemptCompletionAmountStat).HasColumnName("attempt_completion_amount_stat");

                entity.Property(e => e.AttemptCompletionStatus).HasColumnName("attempt_completion_status");

                entity.Property(e => e.AttemptExpDurReported).HasColumnName("attempt_exp_dur_reported");

                entity.Property(e => e.AttemptExpDurTracked).HasColumnName("attempt_exp_dur_tracked");

                entity.Property(e => e.AttemptProgressStatus).HasColumnName("attempt_progress_status");

                entity.Property(e => e.AttemptStartTimestampUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("attempt_start_timestamp_utc");

                entity.Property(e => e.AttemptedDuringThisAttempt).HasColumnName("attempted_during_this_attempt");

                entity.Property(e => e.FirstCompletionTimestampUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("first_completion_timestamp_utc");

                entity.Property(e => e.Included).HasColumnName("included");

                entity.Property(e => e.IsLatestAttempt)
                    .IsRequired()
                    .HasColumnName("is_latest_attempt")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LatestLti13ScoreUpdate)
                    .HasColumnName("latest_lti13_score_update")
                    .HasDefaultValueSql("((-1))");

                entity.Property(e => e.Ordinal).HasColumnName("ordinal");

                entity.Property(e => e.PrevAttemptCompletionStatus).HasColumnName("prev_attempt_completion_status");

                entity.Property(e => e.PrevAttemptProgressStatus).HasColumnName("prev_attempt_progress_status");

                entity.Property(e => e.RandomizedChildren).HasColumnName("randomized_children");

                entity.Property(e => e.ScormObjectId).HasColumnName("scorm_object_id");

                entity.Property(e => e.ScormRegistrationId).HasColumnName("scorm_registration_id");

                entity.Property(e => e.SelectedChildren).HasColumnName("selected_children");

                entity.Property(e => e.Suspended).HasColumnName("suspended");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormObject)
                    .WithMany(p => p.ScormActivities)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormObjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormActivity_ScormObject");

                entity.HasOne(d => d.ScormRegistration)
                    .WithMany(p => p.ScormActivities)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormRegistrationId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormActivity_ScormRegis_1");
            });

            modelBuilder.Entity<ScormActivityObjective>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormActivityId, e.ScormActivityObjectiveId })
                    .HasName("ScormActivityObjective_pkey");

                entity.ToTable("ScormActivityObjective");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormActivityId).HasColumnName("scorm_activity_id");

                entity.Property(e => e.ScormActivityObjectiveId).HasColumnName("scorm_activity_objective_id");

                entity.Property(e => e.CompletionStatus).HasColumnName("completion_status");

                entity.Property(e => e.CompletionStatusValue).HasColumnName("completion_status_value");

                entity.Property(e => e.FirstObjNormalizedMeasure)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("first_obj_normalized_measure")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FirstSuccessTimestampUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("first_success_timestamp_utc");

                entity.Property(e => e.ObjectiveIdentifier)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("objective_identifier");

                entity.Property(e => e.ObjectiveMeasureStatus).HasColumnName("objective_measure_status");

                entity.Property(e => e.ObjectiveNormalizedMeasure)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("objective_normalized_measure");

                entity.Property(e => e.ObjectiveProgressStatus).HasColumnName("objective_progress_status");

                entity.Property(e => e.ObjectiveSatisfiedStatus).HasColumnName("objective_satisfied_status");

                entity.Property(e => e.PrevObjMeasureStatus).HasColumnName("prev_obj_measure_status");

                entity.Property(e => e.PrevObjNormalizedMeasure)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("prev_obj_normalized_measure");

                entity.Property(e => e.PrevObjProgressStatus).HasColumnName("prev_obj_progress_status");

                entity.Property(e => e.PrevObjSatisfiedStatus).HasColumnName("prev_obj_satisfied_status");

                entity.Property(e => e.PrimaryObjective).HasColumnName("primary_objective");

                entity.Property(e => e.ProgressMeasure)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("progress_measure");

                entity.Property(e => e.ProgressMeasureStatus).HasColumnName("progress_measure_status");

                entity.Property(e => e.ScoreMax)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("score_max");

                entity.Property(e => e.ScoreMin)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("score_min");

                entity.Property(e => e.ScoreRaw)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("score_raw");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormActivity)
                    .WithMany(p => p.ScormActivityObjectives)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormActivityId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormActivityObjective_S_1");
            });

            modelBuilder.Entity<ScormActivityRt>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormActivityId })
                    .HasName("ScormActivityRT_pkey");

                entity.ToTable("ScormActivityRT");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormActivityId).HasColumnName("scorm_activity_id");

                entity.Property(e => e.AudioCaptioning).HasColumnName("audio_captioning");

                entity.Property(e => e.AudioLevel)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("audio_level");

                entity.Property(e => e.CompletionStatus).HasColumnName("completion_status");

                entity.Property(e => e.Credit).HasColumnName("credit");

                entity.Property(e => e.DeliverySpeed)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("delivery_speed");

                entity.Property(e => e.Entry).HasColumnName("entry");

                entity.Property(e => e.ExitMode).HasColumnName("exit_mode");

                entity.Property(e => e.LanguagePreference)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("language_preference");

                entity.Property(e => e.LessonMode).HasColumnName("lesson_mode");

                entity.Property(e => e.Location).HasColumnName("location");

                entity.Property(e => e.LocationNull)
                    .IsRequired()
                    .HasColumnName("location_null")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ProgressMeasure)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("progress_measure");

                entity.Property(e => e.ScoreMax)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("score_max");

                entity.Property(e => e.ScoreMin)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("score_min");

                entity.Property(e => e.ScoreRaw)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("score_raw");

                entity.Property(e => e.ScoreScaled)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("score_scaled");

                entity.Property(e => e.SuccessStatus).HasColumnName("success_status");

                entity.Property(e => e.SuspendData).HasColumnName("suspend_data");

                entity.Property(e => e.SuspendDataNull)
                    .IsRequired()
                    .HasColumnName("suspend_data_null")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SuspendDataOverflow).HasColumnName("suspend_data_overflow");

                entity.Property(e => e.SuspendDataOverflowNull)
                    .IsRequired()
                    .HasColumnName("suspend_data_overflow_null")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TotalTime).HasColumnName("total_time");

                entity.Property(e => e.TotalTimeTracked).HasColumnName("total_time_tracked");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormActivity)
                    .WithOne(p => p.ScormActivityRt)
                    .HasForeignKey<ScormActivityRt>(d => new { d.EngineTenantId, d.ScormActivityId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormActivityRT_ScormAct_1");
            });

            modelBuilder.Entity<ScormActivityRtcomment>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormActivityId, e.CommentIndex, e.FromLms })
                    .HasName("ScormActivityRTComment_pkey");

                entity.ToTable("ScormActivityRTComment");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormActivityId).HasColumnName("scorm_activity_id");

                entity.Property(e => e.CommentIndex).HasColumnName("comment_index");

                entity.Property(e => e.FromLms).HasColumnName("from_lms");

                entity.Property(e => e.CommentText).HasColumnName("comment_text");

                entity.Property(e => e.CommentTextNull)
                    .IsRequired()
                    .HasColumnName("comment_text_null")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Id)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Language)
                    .HasMaxLength(260)
                    .IsUnicode(false)
                    .HasColumnName("language");

                entity.Property(e => e.LanguageNull)
                    .IsRequired()
                    .HasColumnName("language_null")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Location)
                    .HasMaxLength(250)
                    .HasColumnName("location");

                entity.Property(e => e.LocationNull)
                    .IsRequired()
                    .HasColumnName("location_null")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TimestampNull)
                    .IsRequired()
                    .HasColumnName("timestamp_null")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TimestampText)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("timestamp_text");

                entity.Property(e => e.TimestampUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("timestamp_utc");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormActivityRt)
                    .WithMany(p => p.ScormActivityRtcomments)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormActivityId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormActivityRTComment_S_1");
            });

            modelBuilder.Entity<ScormActivityRtintCorrectResp>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormActivityId, e.InteractionIndex, e.InteractionCorrectRespIndex })
                    .HasName("mActivityRTIntCorrectResp_pkey");

                entity.ToTable("ScormActivityRTIntCorrectResp");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormActivityId).HasColumnName("scorm_activity_id");

                entity.Property(e => e.InteractionIndex).HasColumnName("interaction_index");

                entity.Property(e => e.InteractionCorrectRespIndex).HasColumnName("interaction_correct_resp_index");

                entity.Property(e => e.CorrectResponse).HasColumnName("correct_response");

                entity.Property(e => e.CorrectResponseOverflow).HasColumnName("correct_response_overflow");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormActivityRtinteraction)
                    .WithMany(p => p.ScormActivityRtintCorrectResps)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormActivityId, d.InteractionIndex })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormActivityRTIntCorrec_1");
            });

            modelBuilder.Entity<ScormActivityRtintLearnerResp>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormActivityId, e.InteractionIndex })
                    .HasName("mActivityRTIntLearnerResp_pkey");

                entity.ToTable("ScormActivityRTIntLearnerResp");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormActivityId).HasColumnName("scorm_activity_id");

                entity.Property(e => e.InteractionIndex).HasColumnName("interaction_index");

                entity.Property(e => e.LearnerResponse).HasColumnName("learner_response");

                entity.Property(e => e.LearnerResponseNull)
                    .IsRequired()
                    .HasColumnName("learner_response_null")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LearnerResponseOverflow).HasColumnName("learner_response_overflow");

                entity.Property(e => e.LearnerResponseOverflowNull)
                    .IsRequired()
                    .HasColumnName("learner_response_overflow_null")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormActivityRtinteraction)
                    .WithOne(p => p.ScormActivityRtintLearnerResp)
                    .HasForeignKey<ScormActivityRtintLearnerResp>(d => new { d.EngineTenantId, d.ScormActivityId, d.InteractionIndex })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormActivityRTIntLearne_1");
            });

            modelBuilder.Entity<ScormActivityRtintObjective>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormActivityId, e.InteractionIndex, e.InteractionObjectiveIndex })
                    .HasName("ormActivityRTIntObjective_pkey");

                entity.ToTable("ScormActivityRTIntObjective");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormActivityId).HasColumnName("scorm_activity_id");

                entity.Property(e => e.InteractionIndex).HasColumnName("interaction_index");

                entity.Property(e => e.InteractionObjectiveIndex).HasColumnName("interaction_objective_index");

                entity.Property(e => e.ObjectiveId)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("objective_id");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormActivityRtinteraction)
                    .WithMany(p => p.ScormActivityRtintObjectives)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormActivityId, d.InteractionIndex })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormActivityRTIntObject_1");
            });

            modelBuilder.Entity<ScormActivityRtinteraction>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormActivityId, e.InteractionIndex })
                    .HasName("cormActivityRTInteraction_pkey");

                entity.ToTable("ScormActivityRTInteraction");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormActivityId).HasColumnName("scorm_activity_id");

                entity.Property(e => e.InteractionIndex).HasColumnName("interaction_index");

                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .HasColumnName("description");

                entity.Property(e => e.DescriptionNull)
                    .IsRequired()
                    .HasColumnName("description_null")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.InteractionId)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("interaction_id");

                entity.Property(e => e.Latency).HasColumnName("latency");

                entity.Property(e => e.Result).HasColumnName("result");

                entity.Property(e => e.ResultNumeric)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("result_numeric");

                entity.Property(e => e.TimestampNull)
                    .IsRequired()
                    .HasColumnName("timestamp_null")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TimestampText)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("timestamp_text");

                entity.Property(e => e.TimestampUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("timestamp_utc");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Weighting)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("weighting");

                entity.HasOne(d => d.ScormActivityRt)
                    .WithMany(p => p.ScormActivityRtinteractions)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormActivityId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormActivityRTInteracti_1");
            });

            modelBuilder.Entity<ScormActivityRtobjective>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormActivityId, e.ObjectiveIndex })
                    .HasName("ScormActivityRTObjective_pkey");

                entity.ToTable("ScormActivityRTObjective");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormActivityId).HasColumnName("scorm_activity_id");

                entity.Property(e => e.ObjectiveIndex).HasColumnName("objective_index");

                entity.Property(e => e.CompletionStatus).HasColumnName("completion_status");

                entity.Property(e => e.Description)
                    .HasMaxLength(510)
                    .HasColumnName("description");

                entity.Property(e => e.DescriptionNull)
                    .IsRequired()
                    .HasColumnName("description_null")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ObjectiveId)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("objective_id");

                entity.Property(e => e.ObjectiveIdNull)
                    .IsRequired()
                    .HasColumnName("objective_id_null")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ProgressMeasure)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("progress_measure");

                entity.Property(e => e.ScoreMax)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("score_max");

                entity.Property(e => e.ScoreMin)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("score_min");

                entity.Property(e => e.ScoreRaw)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("score_raw");

                entity.Property(e => e.ScoreScaled)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("score_scaled");

                entity.Property(e => e.SuccessStatus).HasColumnName("success_status");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormActivityRt)
                    .WithMany(p => p.ScormActivityRtobjectives)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormActivityId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormActivityRTObjective_1");
            });

            modelBuilder.Entity<ScormAiccSession>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.AiccSessionId })
                    .HasName("ScormAiccSession_pkey");

                entity.ToTable("ScormAiccSession");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormRegistrationId }, "IX_ScormAiccSession");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.AiccSessionId).HasColumnName("aicc_session_id");

                entity.Property(e => e.ExternalConfiguration)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("external_configuration");

                entity.Property(e => e.ExternalRegistrationId)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("external_registration_id");

                entity.Property(e => e.IsTracking).HasColumnName("is_tracking");

                entity.Property(e => e.LaunchHistoryId).HasColumnName("launch_history_id");

                entity.Property(e => e.LegacyAiccSessionId)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("legacy_aicc_session_id");

                entity.Property(e => e.ScormRegistrationId).HasColumnName("scorm_registration_id");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormRegistration)
                    .WithMany(p => p.ScormAiccSessions)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormRegistrationId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormAiccSession_ScormRe_1");
            });

            modelBuilder.Entity<ScormApiLearner>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ApiLearnerId })
                    .HasName("ScormApiLearner_pkey");

                entity.ToTable("ScormApiLearner");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ApiLearnerId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("api_learner_id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("last_name");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ScormApiRegToLearner>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ApiRegistrationId })
                    .HasName("ScormApiRegToLearner_pkey");

                entity.ToTable("ScormApiRegToLearner");

                entity.HasIndex(e => new { e.EngineTenantId, e.ApiLearnerId }, "IX_ARL_learner_id");

                entity.HasIndex(e => new { e.EngineTenantId, e.ApiRegistrationId, e.ApiLearnerId }, "IX_ARL_reg_learner_id");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ApiRegistrationId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("api_registration_id");

                entity.Property(e => e.ApiLearnerId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("api_learner_id");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormApiLearner)
                    .WithMany(p => p.ScormApiRegToLearners)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ApiLearnerId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormRegToLearn_learner");
            });

            modelBuilder.Entity<ScormLaunchHistory>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.LaunchHistoryId })
                    .HasName("ScormLaunchHistory_pkey");

                entity.ToTable("ScormLaunchHistory");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormRegistrationId, e.LaunchTime }, "IX_ScormLaunchHistory");

                entity.HasIndex(e => new { e.EngineTenantId, e.UpdateDt }, "IX_ScormLaunchHistory_update");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.LaunchHistoryId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("launch_history_id");

                entity.Property(e => e.Completion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("completion");

                entity.Property(e => e.ExitTime)
                    .HasColumnType("datetime")
                    .HasColumnName("exit_time");

                entity.Property(e => e.ExitTimeUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("exit_time_utc");

                entity.Property(e => e.ExperiencedDurationTracked).HasColumnName("experienced_duration_tracked");

                entity.Property(e => e.HistoryLog).HasColumnName("history_log");

                entity.Property(e => e.LastRuntimeUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("last_runtime_update");

                entity.Property(e => e.LastRuntimeUpdateUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("last_runtime_update_utc");

                entity.Property(e => e.LaunchTime)
                    .HasColumnType("datetime")
                    .HasColumnName("launch_time");

                entity.Property(e => e.LaunchTimeUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("launch_time_utc");

                entity.Property(e => e.MeasureStatus).HasColumnName("measure_status");

                entity.Property(e => e.NormalizedMeasure)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("normalized_measure");

                entity.Property(e => e.Satisfaction)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("satisfaction");

                entity.Property(e => e.ScormRegistrationId).HasColumnName("scorm_registration_id");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateDtUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt_utc");

                entity.HasOne(d => d.ScormRegistration)
                    .WithMany(p => p.ScormLaunchHistories)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormRegistrationId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormLaunchHistory_Scorm_1");
            });

            modelBuilder.Entity<ScormMetadatum>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormMetadataId })
                    .HasName("ScormMetadata_pkey");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormObjectId }, "IX_SMD_object_id");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormPackageId }, "IX_SMD_package_id");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormMetadataId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("scorm_metadata_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Duration)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("duration");

                entity.Property(e => e.FileHref)
                    .HasMaxLength(1000)
                    .HasColumnName("file_href");

                entity.Property(e => e.Identifier)
                    .HasMaxLength(1000)
                    .HasColumnName("identifier");

                entity.Property(e => e.Keywords).HasColumnName("keywords");

                entity.Property(e => e.LanguageCode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("language_code");

                entity.Property(e => e.MetadataIndex).HasColumnName("metadata_index");

                entity.Property(e => e.MetadataXml).HasColumnName("metadata_xml");

                entity.Property(e => e.ScormObjectId).HasColumnName("scorm_object_id");

                entity.Property(e => e.ScormPackageId).HasColumnName("scorm_package_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(1000)
                    .HasColumnName("title");

                entity.Property(e => e.TypicalLearningTime)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("typical_learning_time");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Version)
                    .HasMaxLength(50)
                    .HasColumnName("version");

                entity.HasOne(d => d.ScormObject)
                    .WithMany(p => p.ScormMetadata)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormObjectId })
                    .HasConstraintName("FK_ScormMetadata_ScormObject");

                entity.HasOne(d => d.ScormPackage)
                    .WithMany(p => p.ScormMetadata)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormPackageId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormMetadata_ScormPackage");
            });

            modelBuilder.Entity<ScormObject>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormObjectId })
                    .HasName("ScormObject_pkey");

                entity.ToTable("ScormObject");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormPackageId }, "IX_SO_package_id");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormObjectId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("scorm_object_id");

                entity.Property(e => e.CompletedByMeasure).HasColumnName("completed_by_measure");

                entity.Property(e => e.CompletionProgressWeight)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("completion_progress_weight")
                    .HasDefaultValueSql("((1.0))");

                entity.Property(e => e.CompletionThreshold)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("completion_threshold");

                entity.Property(e => e.DataFromLms).HasColumnName("data_from_lms");

                entity.Property(e => e.FileList).HasColumnName("file_list");

                entity.Property(e => e.Href)
                    .HasMaxLength(2000)
                    .HasColumnName("href");

                entity.Property(e => e.MasteryScore)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("mastery_score");

                entity.Property(e => e.MaxTimeAllowed).HasColumnName("max_time_allowed");

                entity.Property(e => e.Parameters)
                    .HasMaxLength(1000)
                    .HasColumnName("parameters");

                entity.Property(e => e.PersistState).HasColumnName("persist_state");

                entity.Property(e => e.Prerequisites)
                    .HasMaxLength(200)
                    .HasColumnName("prerequisites");

                entity.Property(e => e.ScormObjectTypeId).HasColumnName("scorm_object_type_id");

                entity.Property(e => e.ScormPackageId).HasColumnName("scorm_package_id");

                entity.Property(e => e.TimeLimitAction).HasColumnName("time_limit_action");

                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .HasColumnName("title");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Visible).HasColumnName("visible");

                entity.HasOne(d => d.ScormPackage)
                    .WithMany(p => p.ScormObjects)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormPackageId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObject_ScormPackage");
            });

            modelBuilder.Entity<ScormObjectHierarchy>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ParentScormObjectId, e.ChildScormObjectId })
                    .HasName("ScormObjectHierarchy_pkey");

                entity.ToTable("ScormObjectHierarchy");

                entity.HasIndex(e => new { e.EngineTenantId, e.ChildScormObjectId }, "IX_SOH_child_object_id");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ParentScormObjectId).HasColumnName("parent_scorm_object_id");

                entity.Property(e => e.ChildScormObjectId).HasColumnName("child_scorm_object_id");

                entity.Property(e => e.Ordinal).HasColumnName("ordinal");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormObject)
                    .WithMany(p => p.ScormObjectHierarchyScormObjects)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ChildScormObjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObjectHierarchyChil_1");

                entity.HasOne(d => d.ScormObjectNavigation)
                    .WithMany(p => p.ScormObjectHierarchyScormObjectNavigations)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ParentScormObjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObjectHierarchyPare_1");
            });

            modelBuilder.Entity<ScormObjectIdentifier>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormObjectId })
                    .HasName("ScormObjectIdentifiers_pkey");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormObjectId).HasColumnName("scorm_object_id");

                entity.Property(e => e.ExternalIdentifier).HasColumnName("external_identifier");

                entity.Property(e => e.ItemIdentifier).HasColumnName("item_identifier");

                entity.Property(e => e.ResourceIdentifier).HasColumnName("resource_identifier");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormObject)
                    .WithOne(p => p.ScormObjectIdentifier)
                    .HasForeignKey<ScormObjectIdentifier>(d => new { d.EngineTenantId, d.ScormObjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObjectIdentifiers_S_1");
            });

            modelBuilder.Entity<ScormObjectSeqDatum>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormObjectId })
                    .HasName("ScormObjectSeqData_pkey");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormObjectId).HasColumnName("scorm_object_id");

                entity.Property(e => e.CompletionSetByContent).HasColumnName("completion_set_by_content");

                entity.Property(e => e.ConstrainChoice).HasColumnName("constrain_choice");

                entity.Property(e => e.ControlChoice).HasColumnName("control_choice");

                entity.Property(e => e.ControlChoiceExit).HasColumnName("control_choice_exit");

                entity.Property(e => e.ControlFlow).HasColumnName("control_flow");

                entity.Property(e => e.ControlForwardOnly).HasColumnName("control_forward_only");

                entity.Property(e => e.HideAbandon).HasColumnName("hide_abandon");

                entity.Property(e => e.HideAbandonAll).HasColumnName("hide_abandon_all");

                entity.Property(e => e.HideContinue).HasColumnName("hide_continue");

                entity.Property(e => e.HideExit).HasColumnName("hide_exit");

                entity.Property(e => e.HideExitAll).HasColumnName("hide_exit_all");

                entity.Property(e => e.HidePrevious).HasColumnName("hide_previous");

                entity.Property(e => e.HideSuspendAll).HasColumnName("hide_suspend_all");

                entity.Property(e => e.LimitCondAttemptControl).HasColumnName("limit_cond_attempt_control");

                entity.Property(e => e.LimitCondAttemptDurControl).HasColumnName("limit_cond_attempt_dur_control");

                entity.Property(e => e.LimitCondAttemptDurLimit)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("limit_cond_attempt_dur_limit");

                entity.Property(e => e.LimitCondAttemptLimit).HasColumnName("limit_cond_attempt_limit");

                entity.Property(e => e.MeasureSatisfactionIfActive).HasColumnName("measure_satisfaction_if_active");

                entity.Property(e => e.ObjectiveSetByContent).HasColumnName("objective_set_by_content");

                entity.Property(e => e.PreventActivation).HasColumnName("prevent_activation");

                entity.Property(e => e.RandomizationTiming).HasColumnName("randomization_timing");

                entity.Property(e => e.RandomizeChildren).HasColumnName("randomize_children");

                entity.Property(e => e.RequiredForCompleted).HasColumnName("required_for_completed");

                entity.Property(e => e.RequiredForIncomplete).HasColumnName("required_for_incomplete");

                entity.Property(e => e.RequiredForNotSatisfied).HasColumnName("required_for_not_satisfied");

                entity.Property(e => e.RequiredForSatisfied).HasColumnName("required_for_satisfied");

                entity.Property(e => e.RollupObjMeasureWeight)
                    .HasColumnType("decimal(5, 4)")
                    .HasColumnName("rollup_obj_measure_weight");

                entity.Property(e => e.RollupObjectiveSatisfied).HasColumnName("rollup_objective_satisfied");

                entity.Property(e => e.RollupProgressCompletion).HasColumnName("rollup_progress_completion");

                entity.Property(e => e.SelectionCount).HasColumnName("selection_count");

                entity.Property(e => e.SelectionCountStatus).HasColumnName("selection_count_status");

                entity.Property(e => e.SelectionTiming).HasColumnName("selection_timing");

                entity.Property(e => e.Tracked).HasColumnName("tracked");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UseCurrentAttemptObjInfo).HasColumnName("use_current_attempt_obj_info");

                entity.Property(e => e.UseCurrentAttemptProgInfo).HasColumnName("use_current_attempt_prog_info");

                entity.HasOne(d => d.ScormObject)
                    .WithOne(p => p.ScormObjectSeqDatum)
                    .HasForeignKey<ScormObjectSeqDatum>(d => new { d.EngineTenantId, d.ScormObjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObjectSeqData_Scorm_1");
            });

            modelBuilder.Entity<ScormObjectSeqObjective>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormObjectId, e.ScormObjectSeqObjectiveId })
                    .HasName("ScormObjectSeqObjective_pkey");

                entity.ToTable("ScormObjectSeqObjective");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormObjectId).HasColumnName("scorm_object_id");

                entity.Property(e => e.ScormObjectSeqObjectiveId).HasColumnName("scorm_object_seq_objective_id");

                entity.Property(e => e.MinNormalizedMeasure)
                    .HasColumnType("decimal(5, 4)")
                    .HasColumnName("min_normalized_measure");

                entity.Property(e => e.ObjectiveIdentifier)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("objective_identifier");

                entity.Property(e => e.PrimaryObjective).HasColumnName("primary_objective");

                entity.Property(e => e.SatisfiedByMeasure).HasColumnName("satisfied_by_measure");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormObjectSeqDatum)
                    .WithMany(p => p.ScormObjectSeqObjectives)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormObjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObjectSeqObjective_1");
            });

            modelBuilder.Entity<ScormObjectSeqObjectiveMap>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormObjectId, e.ScormObjectSeqObjectiveId, e.ScormObjectSeqObjMapId })
                    .HasName("cormObjectSeqObjectiveMap_pkey");

                entity.ToTable("ScormObjectSeqObjectiveMap");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormObjectId).HasColumnName("scorm_object_id");

                entity.Property(e => e.ScormObjectSeqObjectiveId).HasColumnName("scorm_object_seq_objective_id");

                entity.Property(e => e.ScormObjectSeqObjMapId).HasColumnName("scorm_object_seq_obj_map_id");

                entity.Property(e => e.ReadCompletionStatus).HasColumnName("read_completion_status");

                entity.Property(e => e.ReadNormalizedMeasure).HasColumnName("read_normalized_measure");

                entity.Property(e => e.ReadProgressMeasure).HasColumnName("read_progress_measure");

                entity.Property(e => e.ReadSatisfiedStatus).HasColumnName("read_satisfied_status");

                entity.Property(e => e.ReadScoreMax).HasColumnName("read_score_max");

                entity.Property(e => e.ReadScoreMin).HasColumnName("read_score_min");

                entity.Property(e => e.ReadScoreRaw).HasColumnName("read_score_raw");

                entity.Property(e => e.TargetObjectiveId)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("target_objective_id");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.WriteCompletionStatus).HasColumnName("write_completion_status");

                entity.Property(e => e.WriteNormalizedMeasure).HasColumnName("write_normalized_measure");

                entity.Property(e => e.WriteProgressMeasure).HasColumnName("write_progress_measure");

                entity.Property(e => e.WriteSatisfiedStatus).HasColumnName("write_satisfied_status");

                entity.Property(e => e.WriteScoreMax).HasColumnName("write_score_max");

                entity.Property(e => e.WriteScoreMin).HasColumnName("write_score_min");

                entity.Property(e => e.WriteScoreRaw).HasColumnName("write_score_raw");

                entity.HasOne(d => d.ScormObjectSeqObjective)
                    .WithMany(p => p.ScormObjectSeqObjectiveMaps)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormObjectId, d.ScormObjectSeqObjectiveId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObjectSeqObjectiveM_1");
            });

            modelBuilder.Entity<ScormObjectSeqRollupRule>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormObjectId, e.RollupRuleId })
                    .HasName("ScormObjectSeqRollupRule_pkey");

                entity.ToTable("ScormObjectSeqRollupRule");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormObjectId).HasColumnName("scorm_object_id");

                entity.Property(e => e.RollupRuleId).HasColumnName("rollup_rule_id");

                entity.Property(e => e.Action).HasColumnName("action");

                entity.Property(e => e.ChildActivitySet).HasColumnName("child_activity_set");

                entity.Property(e => e.ConditionCombination).HasColumnName("condition_combination");

                entity.Property(e => e.MinimumCount).HasColumnName("minimum_count");

                entity.Property(e => e.MinimumPercent)
                    .HasColumnType("decimal(5, 4)")
                    .HasColumnName("minimum_percent");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormObjectSeqDatum)
                    .WithMany(p => p.ScormObjectSeqRollupRules)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormObjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObjectSeqRollupRule_1");
            });

            modelBuilder.Entity<ScormObjectSeqRollupRuleCond>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormObjectId, e.RollupRuleId, e.RollupRuleConditionId })
                    .HasName("rmObjectSeqRollupRuleCond_pkey");

                entity.ToTable("ScormObjectSeqRollupRuleCond");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormObjectId).HasColumnName("scorm_object_id");

                entity.Property(e => e.RollupRuleId).HasColumnName("rollup_rule_id");

                entity.Property(e => e.RollupRuleConditionId).HasColumnName("rollup_rule_condition_id");

                entity.Property(e => e.ConditionOperator).HasColumnName("condition_operator");

                entity.Property(e => e.RuleCondition).HasColumnName("rule_condition");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormObjectSeqRollupRule)
                    .WithMany(p => p.ScormObjectSeqRollupRuleConds)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormObjectId, d.RollupRuleId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObjectSeqRollupRule_2");
            });

            modelBuilder.Entity<ScormObjectSeqRule>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormObjectId, e.SeqRuleId })
                    .HasName("ScormObjectSeqRule_pkey");

                entity.ToTable("ScormObjectSeqRule");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormObjectId).HasColumnName("scorm_object_id");

                entity.Property(e => e.SeqRuleId).HasColumnName("seq_rule_id");

                entity.Property(e => e.Action).HasColumnName("action");

                entity.Property(e => e.ConditionCombination).HasColumnName("condition_combination");

                entity.Property(e => e.RuleType).HasColumnName("rule_type");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormObjectSeqDatum)
                    .WithMany(p => p.ScormObjectSeqRules)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormObjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObjectSeqRule_Scorm_1");
            });

            modelBuilder.Entity<ScormObjectSeqRuleCond>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormObjectId, e.SeqRuleId, e.SeqRuleConditionId })
                    .HasName("ScormObjectSeqRuleCond_pkey");

                entity.ToTable("ScormObjectSeqRuleCond");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormObjectId).HasColumnName("scorm_object_id");

                entity.Property(e => e.SeqRuleId).HasColumnName("seq_rule_id");

                entity.Property(e => e.SeqRuleConditionId).HasColumnName("seq_rule_condition_id");

                entity.Property(e => e.ConditionOperator).HasColumnName("condition_operator");

                entity.Property(e => e.MeasureThreshold)
                    .HasColumnType("decimal(5, 4)")
                    .HasColumnName("measure_threshold");

                entity.Property(e => e.ReferencedObjective)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("referenced_objective");

                entity.Property(e => e.RuleCondition).HasColumnName("rule_condition");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormObjectSeqRule)
                    .WithMany(p => p.ScormObjectSeqRuleConds)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormObjectId, d.SeqRuleId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObjectSeqRuleCond_S_1");
            });

            modelBuilder.Entity<ScormObjectSharedDataMap>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormObjectId, e.ScormObjSharedDataMapId })
                    .HasName("ScormObjectSharedDataMap_pkey");

                entity.ToTable("ScormObjectSharedDataMap");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormObjectId).HasColumnName("scorm_object_id");

                entity.Property(e => e.ScormObjSharedDataMapId).HasColumnName("scorm_obj_shared_data_map_id");

                entity.Property(e => e.ReadSharedData).HasColumnName("read_shared_data");

                entity.Property(e => e.TargetSharedDataId)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("target_shared_data_id");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.WriteSharedData).HasColumnName("write_shared_data");

                entity.HasOne(d => d.ScormObject)
                    .WithMany(p => p.ScormObjectSharedDataMaps)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormObjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObjectSharedDataMap_1");
            });

            modelBuilder.Entity<ScormObjectSspbucket>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormObjectId, e.BucketIndex })
                    .HasName("ScormObjectSSPBucket_pkey");

                entity.ToTable("ScormObjectSSPBucket");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormObjectId).HasColumnName("scorm_object_id");

                entity.Property(e => e.BucketIndex).HasColumnName("bucket_index");

                entity.Property(e => e.BucketIdentifier)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("bucket_identifier");

                entity.Property(e => e.BucketType)
                    .HasMaxLength(3000)
                    .IsUnicode(false)
                    .HasColumnName("bucket_type");

                entity.Property(e => e.Persistence).HasColumnName("persistence");

                entity.Property(e => e.Reducible).HasColumnName("reducible");

                entity.Property(e => e.SizeMin).HasColumnName("size_min");

                entity.Property(e => e.SizeRequested).HasColumnName("size_requested");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormObject)
                    .WithMany(p => p.ScormObjectSspbuckets)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormObjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObjectSSPBucket_Sco_1");
            });

            modelBuilder.Entity<ScormObjectStore>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ObjectKeySha1 })
                    .HasName("ScormObjectStore_pkey");

                entity.ToTable("ScormObjectStore");

                entity.HasIndex(e => new { e.EngineTenantId, e.Expiry }, "IX_SOS_e");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ObjectKeySha1)
                    .HasMaxLength(20)
                    .HasColumnName("object_key_sha1")
                    .IsFixedLength(true);

                entity.Property(e => e.Expiry)
                    .HasColumnType("datetime")
                    .HasColumnName("expiry");

                entity.Property(e => e.ObjectKey)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("object_key");

                entity.Property(e => e.ObjectType)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("object_type");

                entity.Property(e => e.ObjectValue).HasColumnName("object_value");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ScormPackage>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormPackageId })
                    .HasName("ScormPackage_pkey");

                entity.ToTable("ScormPackage");

                entity.HasIndex(e => new { e.EngineTenantId, e.ApiCourseId, e.VersionId }, "IX_SP_api_course")
                    .IsUnique();

                entity.HasIndex(e => new { e.EngineTenantId, e.UpdateDt, e.ScormPackageId }, "IX_SP_console_ordering");

                entity.HasIndex(e => new { e.EngineTenantId, e.InvariantTitle }, "IX_SP_invariant_title");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormPackageId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("scorm_package_id");

                entity.Property(e => e.ApiCourseId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("api_course_id");

                entity.Property(e => e.ConnectorContentId)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("connector_content_id");

                entity.Property(e => e.ContentConnectorId)
                    .HasMaxLength(16)
                    .HasColumnName("content_connector_id")
                    .IsFixedLength(true);

                entity.Property(e => e.DisplayTitle)
                    .HasMaxLength(200)
                    .HasColumnName("display_title");

                entity.Property(e => e.InvariantTitle)
                    .HasMaxLength(200)
                    .HasColumnName("invariant_title");

                entity.Property(e => e.LearningStandardId).HasColumnName("learning_standard_id");

                entity.Property(e => e.ObjectivesGlobalToSystem).HasColumnName("objectives_global_to_system");

                entity.Property(e => e.SharedDataGlobalToSystem)
                    .IsRequired()
                    .HasColumnName("shared_data_global_to_system")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.VersionId).HasColumnName("version_id");

                entity.Property(e => e.WebPath)
                    .HasMaxLength(500)
                    .HasColumnName("web_path");
            });

            modelBuilder.Entity<ScormPackageToLtiContextId>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormPackageId })
                    .HasName("cormPackageToLtiContextId_pkey");

                entity.ToTable("ScormPackageToLtiContextId");

                entity.HasIndex(e => new { e.EngineTenantId, e.ContextId }, "IX_ContextIdToScormPkg_ctx_id");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormPackageId).HasColumnName("scorm_package_id");

                entity.Property(e => e.ContextId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("context_id");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ScormRegistration>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormRegistrationId })
                    .HasName("ScormRegistration_pkey");

                entity.ToTable("ScormRegistration");

                entity.HasIndex(e => new { e.EngineTenantId, e.ApiRegistrationId, e.InstanceId }, "IX_SR_api_reg_id")
                    .IsUnique();

                entity.HasIndex(e => new { e.EngineTenantId, e.GlobalObjectiveScope }, "IX_SR_gos");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormPackageId, e.UpdateDtUtc, e.ScormRegistrationId }, "IX_SR_package_id");

                entity.HasIndex(e => new { e.EngineTenantId, e.SuspendedActivityId }, "IX_SR_suspended_activity_id");

                entity.HasIndex(e => new { e.EngineTenantId, e.UpdateDtUtc, e.ScormRegistrationId }, "IX_SR_update_dt_utc");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormRegistrationId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("scorm_registration_id");

                entity.Property(e => e.ApiRegistrationId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("api_registration_id");

                entity.Property(e => e.CompletedDtUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("completed_dt_utc");

                entity.Property(e => e.ConvertedToTincan).HasColumnName("converted_to_tincan");

                entity.Property(e => e.CreateDtUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("create_dt_utc");

                entity.Property(e => e.CreatedForCredit).HasColumnName("created_for_credit");

                entity.Property(e => e.FirstaccessDtUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("firstaccess_dt_utc");

                entity.Property(e => e.GlobalObjectiveScope)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("global_objective_scope");

                entity.Property(e => e.InstanceId).HasColumnName("instance_id");

                entity.Property(e => e.RuntimeData).HasColumnName("runtime_data");

                entity.Property(e => e.ScormPackageId).HasColumnName("scorm_package_id");

                entity.Property(e => e.SuspendedActivityId).HasColumnName("suspended_activity_id");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateDtUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt_utc");

                entity.Property(e => e.UpdateSequence).HasColumnName("update_sequence");

                entity.HasOne(d => d.ScormApiRegToLearner)
                    .WithMany(p => p.ScormRegistrations)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ApiRegistrationId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormRegistration_reglearn");

                entity.HasOne(d => d.ScormPackage)
                    .WithMany(p => p.ScormRegistrations)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormPackageId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormRegistration_ScormP_1");
            });

            modelBuilder.Entity<ScormRegistrationGlobalObj>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormRegistrationId, e.ScormRegistrationObjId })
                    .HasName("cormRegistrationGlobalObj_pkey");

                entity.ToTable("ScormRegistrationGlobalObj");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormRegistrationId).HasColumnName("scorm_registration_id");

                entity.Property(e => e.ScormRegistrationObjId).HasColumnName("scorm_registration_obj_id");

                entity.Property(e => e.CompletionStatus).HasColumnName("completion_status");

                entity.Property(e => e.CompletionStatusValue).HasColumnName("completion_status_value");

                entity.Property(e => e.ObjectiveIdentifier)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("objective_identifier");

                entity.Property(e => e.ObjectiveMeasureStatus).HasColumnName("objective_measure_status");

                entity.Property(e => e.ObjectiveNormalizedMeasure)
                    .HasColumnType("decimal(5, 4)")
                    .HasColumnName("objective_normalized_measure");

                entity.Property(e => e.ObjectiveProgressStatus).HasColumnName("objective_progress_status");

                entity.Property(e => e.ObjectiveSatisfiedStatus).HasColumnName("objective_satisfied_status");

                entity.Property(e => e.ProgressMeasure)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("progress_measure");

                entity.Property(e => e.ProgressMeasureStatus).HasColumnName("progress_measure_status");

                entity.Property(e => e.ScoreMax)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("score_max");

                entity.Property(e => e.ScoreMin)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("score_min");

                entity.Property(e => e.ScoreRaw)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("score_raw");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormRegistration)
                    .WithMany(p => p.ScormRegistrationGlobalObjs)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormRegistrationId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormRegistrationGlobalO_1");
            });

            modelBuilder.Entity<ScormRegistrationSharedDataVal>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormSharedDataValueId })
                    .HasName("RegistrationSharedDataVal_pkey");

                entity.ToTable("ScormRegistrationSharedDataVal");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormRegistrationId }, "IX_SRSV_registration_id");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormSharedDataValueId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("scorm_shared_data_value_id");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.GlobalObjectiveScope)
                    .HasMaxLength(100)
                    .HasColumnName("global_objective_scope");

                entity.Property(e => e.ScormRegistrationId).HasColumnName("scorm_registration_id");

                entity.Property(e => e.SharedDataId)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("shared_data_id");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormRegistration)
                    .WithMany(p => p.ScormRegistrationSharedDataVals)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormRegistrationId })
                    .HasConstraintName("FK_ScormRegistrationSDVal");
            });

            modelBuilder.Entity<ScormRegistrationSharedDatum>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormRegistrationId, e.ScormRegistrationDataId })
                    .HasName("ormRegistrationSharedData_pkey");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormSharedDataValueId }, "IX_SRS_data_value_id");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormRegistrationId).HasColumnName("scorm_registration_id");

                entity.Property(e => e.ScormRegistrationDataId).HasColumnName("scorm_registration_data_id");

                entity.Property(e => e.ScormSharedDataValueId).HasColumnName("scorm_shared_data_value_id");

                entity.Property(e => e.SharedDataId)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("shared_data_id");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormRegistration)
                    .WithMany(p => p.ScormRegistrationSharedData)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormRegistrationId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormRegistrationSharedD_1");

                entity.HasOne(d => d.ScormRegistrationSharedDataVal)
                    .WithMany(p => p.ScormRegistrationSharedData)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormSharedDataValueId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormRegistrationSharedD_2");
            });

            modelBuilder.Entity<ScormRegistrationSspbucket>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormRegistrationId, e.BucketIndex })
                    .HasName("cormRegistrationSSPBucket_pkey");

                entity.ToTable("ScormRegistrationSSPBucket");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormRegistrationId).HasColumnName("scorm_registration_id");

                entity.Property(e => e.BucketIndex).HasColumnName("bucket_index");

                entity.Property(e => e.AllocationSuccess).HasColumnName("allocation_success");

                entity.Property(e => e.BucketIdentifier)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("bucket_identifier");

                entity.Property(e => e.BucketType)
                    .HasMaxLength(3000)
                    .IsUnicode(false)
                    .HasColumnName("bucket_type");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.LocalActivityId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("local_activity_id");

                entity.Property(e => e.Persistence).HasColumnName("persistence");

                entity.Property(e => e.Reducible).HasColumnName("reducible");

                entity.Property(e => e.SizeMin).HasColumnName("size_min");

                entity.Property(e => e.SizeRequested).HasColumnName("size_requested");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormRegistration)
                    .WithMany(p => p.ScormRegistrationSspbuckets)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormRegistrationId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormRegistrationSSPBuck_1");
            });

            modelBuilder.Entity<ScormRegistrationStatementMap>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.MapId })
                    .HasName("mRegistrationStatementMap_pkey");

                entity.ToTable("ScormRegistrationStatementMap");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormRegistrationId }, "UIX_Registration_Stmt_Map_Reg");

                entity.HasIndex(e => new { e.EngineTenantId, e.StatementId }, "UIX_Registration_Stmt_Map_Stmt")
                    .IsUnique();

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.MapId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("map_id");

                entity.Property(e => e.ScormRegistrationId).HasColumnName("scorm_registration_id");

                entity.Property(e => e.StatementId)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("statement_id")
                    .IsFixedLength(true);

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormRegistration)
                    .WithMany(p => p.ScormRegistrationStatementMaps)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormRegistrationId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Xapi_Statement_Map_Reg");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
