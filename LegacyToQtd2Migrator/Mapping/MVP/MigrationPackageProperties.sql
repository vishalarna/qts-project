/****** Object:  Table [dbo].[ScormPackageProperties]    Script Date: 5/9/2023 5:58:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ScormPackageProperties](
	[scorm_package_id] [int] NOT NULL,
	[show_finish_button] [bit] NOT NULL,
	[show_course_structure] [bit] NOT NULL,
	[show_progress_bar] [bit] NOT NULL,
	[show_help] [bit] NOT NULL,
	[show_nav_bar] [bit] NOT NULL,
	[show_titlebar] [bit] NOT NULL,
	[enable_flow_nav] [bit] NOT NULL,
	[enable_choice_nav] [bit] NOT NULL,
	[course_structure_width] [int] NOT NULL,
	[course_structure_starts_open] [bit] NOT NULL,
	[sco_launch_type] [int] NOT NULL,
	[player_launch_type] [int] NOT NULL,
	[prevent_right_click] [bit] NOT NULL,
	[prevent_window_resize] [bit] NOT NULL,
	[required_width] [int] NOT NULL,
	[required_height] [int] NOT NULL,
	[required_fullscreen] [bit] NOT NULL,
	[desired_width] [int] NOT NULL,
	[desired_height] [int] NOT NULL,
	[desired_fullscreen] [bit] NOT NULL,
	[int_sat_logout_action] [int] NOT NULL,
	[int_sat_normal_action] [int] NOT NULL,
	[int_sat_suspend_action] [int] NOT NULL,
	[int_sat_timeout_action] [int] NOT NULL,
	[int_not_sat_logout_action] [int] NOT NULL,
	[int_not_sat_normal_action] [int] NOT NULL,
	[int_not_sat_suspend_action] [int] NOT NULL,
	[int_not_sat_timeout_action] [int] NOT NULL,
	[final_sat_logout_action] [int] NOT NULL,
	[final_sat_normal_action] [int] NOT NULL,
	[final_sat_suspend_action] [int] NOT NULL,
	[final_sat_timeout_action] [int] NOT NULL,
	[final_not_sat_logout_action] [int] NOT NULL,
	[final_not_sat_normal_action] [int] NOT NULL,
	[final_not_sat_suspend_action] [int] NOT NULL,
	[final_not_sat_timeout_action] [int] NOT NULL,
	[status_display] [int] NOT NULL,
	[score_rollup_mode] [int] NOT NULL,
	[number_of_scoring_objects] [int] NULL,
	[status_rollup_mode] [int] NOT NULL,
	[threshold_score_for_completion] [decimal](10, 7) NULL,
	[first_sco_is_pretest] [bit] NOT NULL,
	[wrap_sco_window_with_api] [bit] NOT NULL,
	[finish_causes_immediate_commit] [bit] NOT NULL,
	[debug_control_audit] [bit] NOT NULL,
	[debug_control_detailed] [bit] NOT NULL,
	[debug_rte_audit] [bit] NOT NULL,
	[debug_rte_detailed] [bit] NOT NULL,
	[debug_sequencing_audit] [bit] NOT NULL,
	[debug_sequencing_detailed] [bit] NOT NULL,
	[debug_lookahead_audit] [bit] NOT NULL,
	[debug_lookahead_detailed] [bit] NOT NULL,
	[debug_include_timestamps] [bit] NOT NULL,
	[comm_max_failed_submissions] [int] NOT NULL,
	[comm_commit_frequency] [int] NOT NULL,
	[invalid_menu_item_action] [int] NOT NULL,
	[always_flow_to_first_sco] [bit] NOT NULL,
	[logout_causes_player_exit] [bit] NOT NULL,
	[reset_rt_timing] [int] NOT NULL,
	[update_by] [nvarchar](50) NOT NULL,
	[update_dt] [datetime] NOT NULL,
	[offline_synch_mode] [int] NOT NULL,
	[validate_interaction_responses] [bit] NOT NULL,
	[lookahead_sequencer_mode] [int] NOT NULL,
	[show_close_item] [bit] NOT NULL,
	[score_overrides_status] [bit] NOT NULL,
	[scale_raw_score] [bit] NOT NULL,
	[rollup_empty_set_to_unknown] [bit] NOT NULL,
	[capture_history] [bit] NOT NULL,
	[capture_history_detailed] [bit] NOT NULL,
	[return_to_lms_action] [int] NOT NULL,
	[use_measure_progress_bar] [bit] NOT NULL,
	[use_quick_lookahead_seq] [bit] NOT NULL,
	[force_disable_root_choice] [bit] NOT NULL,
	[rollup_runtime_at_sco_unload] [bit] NOT NULL,
	[force_obj_compl_set_by_content] [bit] NOT NULL,
	[invoke_rollup_at_suspendall] [bit] NOT NULL,
	[compl_stat_of_failed_suc_stat] [int] NOT NULL,
	[satisfied_causes_completion] [bit] NOT NULL,
	[student_prefs_global_to_course] [bit] NOT NULL,
	[debug_sequencing_simple] [bit] NOT NULL,
	[suspend_data_max_length] [int] NOT NULL,
	[allow_complete_status_change] [int] NOT NULL,
	[time_limit] [int] NOT NULL,
	[launch_compl_regs_as_no_credit] [int] NOT NULL,
	[apply_status_to_success] [int] NOT NULL,
	[engine_tenant_id] [smallint] NOT NULL,
	[ie_compatibility_mode] [int] NOT NULL,
	[is_available_offline] [bit] NOT NULL,
 CONSTRAINT [ScormPackageProperties_pkey] PRIMARY KEY CLUSTERED 
(
	[engine_tenant_id] ASC,
	[scorm_package_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  CONSTRAINT [DF_invalid_menu_item_action82933d89-cc30-4444-aa81-6db11c709c71]  DEFAULT ((2)) FOR [invalid_menu_item_action]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  CONSTRAINT [DF_update_byee70ddf0-33ef-47a6-bc30-31155288e950]  DEFAULT (user_name()) FOR [update_by]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  CONSTRAINT [DF_ScormPackageProperties_update_dt]  DEFAULT (getdate()) FOR [update_dt]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  CONSTRAINT [DF__ScormPack__offli__0C50D423]  DEFAULT ((1)) FOR [offline_synch_mode]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  CONSTRAINT [DF__ScormPack__valid__0D44F85C]  DEFAULT ((1)) FOR [validate_interaction_responses]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  CONSTRAINT [DF_lookahead_sequencer_mode5a892a08-08a1-4452-a844-a78c5dc89441]  DEFAULT ((2)) FOR [lookahead_sequencer_mode]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  CONSTRAINT [DF_show_close_itemca61d109-6cab-4f36-96dc-09aa3632e910]  DEFAULT ((0)) FOR [show_close_item]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  CONSTRAINT [DF_score_overrides_status9043582a-3ea9-4567-876e-a29077f8fdeb]  DEFAULT ((0)) FOR [score_overrides_status]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  CONSTRAINT [DF__ScormPack__scale__40F9A68C]  DEFAULT ((0)) FOR [scale_raw_score]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  DEFAULT ((0)) FOR [rollup_empty_set_to_unknown]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  CONSTRAINT [DF_capture_historyce144b43-965d-440d-a584-7212e38134b5]  DEFAULT ((0)) FOR [capture_history]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  CONSTRAINT [DF_capture_history_detailed75c27533-7db0-44a3-82ee-92ae957c66b3]  DEFAULT ((0)) FOR [capture_history_detailed]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  CONSTRAINT [DF_return_to_lms_action7738f0e8-dbd0-46d1-9eb6-80258c3929c5]  DEFAULT ((1)) FOR [return_to_lms_action]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  CONSTRAINT [DF_use_measure_progress_bar77953525-8601-4b89-980f-8ec88a9d7a51]  DEFAULT ((0)) FOR [use_measure_progress_bar]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  CONSTRAINT [DF_use_quick_lookahead_seqeecbb8e2-02d1-4779-a629-92a8cff6813c]  DEFAULT ((1)) FOR [use_quick_lookahead_seq]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  CONSTRAINT [DF_force_disable_root_choice4a0dcdde-831b-44a7-8448-100ee7b4da26]  DEFAULT ((0)) FOR [force_disable_root_choice]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  CONSTRAINT [DF_rollup_runtime_at_sco_unloadf491c561-72cd-402c-889f-834c11ec4d40]  DEFAULT ((0)) FOR [rollup_runtime_at_sco_unload]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  CONSTRAINT [DF_force_obj_compl_set_by_content9d706d4e-d834-4e7d-a957-194639490106]  DEFAULT ((0)) FOR [force_obj_compl_set_by_content]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  CONSTRAINT [DF_invoke_rollup_at_suspendallb82c9a96-0d3d-45c7-9e89-d9085fa097ec]  DEFAULT ((0)) FOR [invoke_rollup_at_suspendall]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  CONSTRAINT [DF_compl_stat_of_failed_suc_stat62237ba5-cc8f-42a4-834d-cefbce604c64]  DEFAULT ((1)) FOR [compl_stat_of_failed_suc_stat]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  CONSTRAINT [DF_satisfied_causes_completion60d70c18-4807-46fd-a628-a57933498fe0]  DEFAULT ((0)) FOR [satisfied_causes_completion]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  CONSTRAINT [DF_student_prefs_global_to_coursef05522d7-4b01-4e4f-ba12-c5f303ab42e9]  DEFAULT ((0)) FOR [student_prefs_global_to_course]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  CONSTRAINT [DF_debug_sequencing_simplef2dd6cea-3e62-4403-8aa0-6a0920090690]  DEFAULT ((0)) FOR [debug_sequencing_simple]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  DEFAULT ((64000)) FOR [suspend_data_max_length]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  DEFAULT ((0)) FOR [allow_complete_status_change]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  DEFAULT ((0)) FOR [time_limit]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  DEFAULT ((1)) FOR [launch_compl_regs_as_no_credit]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  DEFAULT ((0)) FOR [apply_status_to_success]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  DEFAULT ((1)) FOR [ie_compatibility_mode]
GO

ALTER TABLE [dbo].[ScormPackageProperties] ADD  DEFAULT ((0)) FOR [is_available_offline]
GO

ALTER TABLE [dbo].[ScormPackageProperties]  WITH CHECK ADD  CONSTRAINT [FK_ScormPackageProperties_S_1] FOREIGN KEY([engine_tenant_id], [scorm_package_id])
REFERENCES [dbo].[ScormPackage] ([engine_tenant_id], [scorm_package_id])
GO

ALTER TABLE [dbo].[ScormPackageProperties] CHECK CONSTRAINT [FK_ScormPackageProperties_S_1]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The package these properties apply to.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'scorm_package_id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Should the SCP show the Return to LMS button?' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'show_finish_button'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Should the SCP show the course tree structure during delivery?' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'show_course_structure'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Should the SCP show the progress bar during course delivery?' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'show_progress_bar'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Should the SCP show the help button during delivery?' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'show_help'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Should the SCP show the navigation bar containing the navigation buttons?' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'show_nav_bar'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Should the SCP show the title bar?' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'show_titlebar'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Should the SCP allow the user to navigate using previous and next commands?' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'enable_flow_nav'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Should the SCP allow the learner to navigate by selecting an item from the table of contents?' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'enable_choice_nav'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'If the course structure is displayed, how many pixels should be allocated for it?' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'course_structure_width'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'If the course structure is displayed, should it default to being open or closed?' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'course_structure_starts_open'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'How should the SCP launch individual SCOs (frameset or popup window)? Stored as an integer representing the 

vocabulary.
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'sco_launch_type'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'How should the SCP player itself be launched (frameset or popup window)? Stored as an integer representing the 

vocabulary.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'player_launch_type'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Should the SCP prevent users from right clicking in its navigation frames?' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'prevent_right_click'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Should the SCP prevent users from resizing its window?' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'prevent_window_resize'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'If greater than 0, the then SCOs in this package require this many pixels of screen height to play correctly. If 

this height is not available, the SCO may not be delivered correctly.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'required_width'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'If greater than 0, the then SCOs in this package require this many pixels of screen width to play correctly. If this 

width is not available, the SCO may not be delivered correctly.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'required_height'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'If true, then the SCOs in this package require that they occupy the entire screen during delivery.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'required_fullscreen'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'If greater than 0, the number of horizontal pixels the SCOs in this package would like to have available during 

delivery.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'desired_width'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'If greater than 0, the number of vertical pixels the SCOs in this package would like to have available during 

delivery.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'desired_height'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'If true, indicates that the SCO would like to be launched as a full screen window.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'desired_fullscreen'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The navigation behavior the SCP should display when a SCO that has been satisfied in the middle of a course exits 

with an exit type of logout. Stored as an integer representing the vocabulary.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'int_sat_logout_action'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The navigation behavior the SCP should display when a SCO that has been satisfied in the middle of a course exits 

with an exit type of normal. Stored as an integer representing the vocabulary.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'int_sat_normal_action'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The navigation behavior the SCP should display when a SCO that has been satisfied in the middle of a course exits 

with an exit type of suspend. Stored as an integer representing the vocabulary.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'int_sat_suspend_action'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The navigation behavior the SCP should display when a SCO that has been satisfied in the middle of a course exits 

with an exit type of timeout. Stored as an integer representing the vocabulary.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'int_sat_timeout_action'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The navigation behavior the SCP should display when a SCO that has not been satisfied in the middle of a course 

exits with an exit type of logout. Stored as an integer representing the vocabulary.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'int_not_sat_logout_action'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The navigation behavior the SCP should display when a SCO in the middle of a course that has not been satisfied 

exits with an exit type of normal. Stored as an integer representing the vocabulary.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'int_not_sat_normal_action'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The navigation behavior the SCP should display when a SCO that has not been satisfied in the middle of a course 

exits with an exit type of suspend. Stored as an integer representing the vocabulary.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'int_not_sat_suspend_action'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The navigation behavior the SCP should display when a SCO that has not been satisfied in the middle of a course  

exits with an exit type of timeout. Stored as an integer representing the vocabulary.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'int_not_sat_timeout_action'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The navigation behavior the SCP should display when a SCO at the end of the course that has been satisfied exits 

with an exit type of logout. Stored as an integer representing the vocabulary.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'final_sat_logout_action'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The navigation behavior the SCP should display when a SCO at the end of the course that has been satisfied exits 

with an exit type of normal. Stored as an integer representing the vocabulary.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'final_sat_normal_action'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The navigation behavior the SCP should display when a SCO at the end of the course that has been satisfied exits 

with an exit type of suspend. Stored as an integer representing the vocabulary.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'final_sat_suspend_action'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The navigation behavior the SCP should display when a SCO at the end of the course that has been satisfied exits 

with an exit type of timeout. Stored as an integer representing the vocabulary.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'final_sat_timeout_action'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The navigation behavior the SCP should display when a SCO at the end of the course that has not been satisfied exits 

with an exit type of logout. Stored as an integer representing the vocabulary.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'final_not_sat_logout_action'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The navigation behavior the SCP should display when a SCO at the end of the course that has not been satisfied exits 

with an exit type of normal. Stored as an integer representing the vocabulary.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'final_not_sat_normal_action'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The navigation behavior the SCP should display when a SCO at the end of the course that has not been satisfied exits 

with an exit type of suspend. Stored as an integer representing the vocabulary.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'final_not_sat_suspend_action'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The navigation behavior the SCP should display when a SCO at the end of the course that has not been satisfied exits 

with an exit type of timeout. Stored as an integer representing the vocabulary.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'final_not_sat_timeout_action'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'How the SCP should graphically represent the current status of individual scorm objects during delivery. Stored as 

an integer representing the vocabulary.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'status_display'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Determines how scores are rolled up to the course level.  Stored as an integer representing the vocabulary.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'score_rollup_mode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'If the Score Rollup Mode is Fixed Average, this parameter indicates how many SCOs should be reporting a score.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'number_of_scoring_objects'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Determines how completion status is rolled up to the course level.  Stored as an integer representing the vocabulary.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'status_rollup_mode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'If the Status Rollup Mode is Complete When Threshold Score is Met, this parameter indicates what the threshold score for completion is. This value is a decimal between 0-1.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'threshold_score_for_completion'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'This parameter indicates that if the first SCO achieves a lesson status of passed, then the rest of the SCOs in the course will be marked complete.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'first_sco_is_pretest'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Will put an API relay object in a frameset around a SCO that is launched in a new window. This is useful for SCOs 

that incorrectly use the ADL API Finder algorithm from spawned windows.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'wrap_sco_window_with_api'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Will force the SCP to immediately persist data to the server when the SCO calls Finish/Terminate rather than waiting 

for the next periodic commit. Useful for SCOs that only save data on unload and require the player to be launched in a new window.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'finish_causes_immediate_commit'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Record control audit messages in the debug log' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'debug_control_audit'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Record control details in the debug log' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'debug_control_detailed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Record runtime audit messages in the debug log' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'debug_rte_audit'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Record runtime details in the debug log' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'debug_rte_detailed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Record sequencing audit messages in the debug log' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'debug_sequencing_audit'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Record sequencing details in the debug log' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'debug_sequencing_detailed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Record look-ahead audit messages in the debug log' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'debug_lookahead_audit'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Record look-ahead details in the debug log' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'debug_lookahead_detailed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Record timestamps in the debug log' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'debug_include_timestamps'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Maximum number of retries when updating runtime data before declaring failure' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'comm_max_failed_submissions'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'How often to send commit runtime data updates (in seconds)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'comm_commit_frequency'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Defines how to handle menu item links that won''t succeed (show, hide or disable).  Stored as an integer representing the vocabulary.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'invalid_menu_item_action'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Should the SCP always launch the first SCO when the course  is launched regardless of sequencing rules.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'always_flow_to_first_sco'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Should the SCP allow a cmi.exit request of logout to exit the entire player' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'logout_causes_player_exit'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Should the SCP always persist runtime data when the exit type is suspend, or should this be left up to the sequencer?  Stored as an integer representing the vocabulary.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'reset_rt_timing'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The user or process that last updated this record' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'update_by'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The time this record was last updated' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'update_dt'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Defines the method of synchronization when merging offline data with lms data.  Applicable only when RSOP (Rustici Software Offline Player) is enabled.  Stored as an integer representing the vocabulary.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'offline_synch_mode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Determines whether the format of SCORM responses are validated client-side' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'validate_interaction_responses'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Determines if or how lookahead sequencer is enabled client-side.  Stored as an integer representing the vocabulary.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'lookahead_sequencer_mode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Should the SCP show the Close Item button?' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'show_close_item'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Should the score override the status?' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'score_overrides_status'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'For SCORM 2004, if SCO sets a raw score but not a scaled score, determines if the raw score should count as the normative score for the SCO' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'scale_raw_score'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Specifies if the course should return attempt information to the server' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'capture_history'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Specifies if the course should return detailed attempt information to the server' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'capture_history_detailed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Defines what happens when a user clicks "Return To Lms"' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'return_to_lms_action'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Specifies whether the interface should determine progress bar progress using Measure Rollup or individual SCO completion.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'use_measure_progress_bar'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'In SCORM 2004 4th Edition and later, determines whether or not to use the Quick Lookahead Sequencer' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'use_quick_lookahead_seq'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Disables the Root menu item Choice option.  This is to prevent new attempts being initiated on the course.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'force_disable_root_choice'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Initiates rollup and transfer of runtime data at ScoUnload for all SCOs.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'rollup_runtime_at_sco_unload'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Override the manifest settings for "Objective Set By Content" and "Completion Set By Content" with true values.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'force_obj_compl_set_by_content'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Initiates rollup when SuspendAll is invoked.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'invoke_rollup_at_suspendall'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Specifies the Completion Status value to apply in the case of a Failed Success Status.  Only applied if set to "completed" or "incomplete"' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'compl_stat_of_failed_suc_stat'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Determines whether or not activity Satisfaction will set completion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'satisfied_causes_completion'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'When a student makes a preference (audio volume, etc) the preference should be applied to all SCOs' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'student_prefs_global_to_course'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Capture sequencing debugging information in a more human readable fashion than the very technical Audit or Detailed level' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'debug_sequencing_simple'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Maximum length of suspend data to save' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'suspend_data_max_length'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Allow a once-complete status to become incomplete again' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'allow_complete_status_change'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Maximum length of time a user can spend taking a course' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'time_limit'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Once complete, relaunched registrations launch as no credit' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'launch_compl_regs_as_no_credit'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Apply the rudimentary rollup status rules also to the success status' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties', @level2type=N'COLUMN',@level2name=N'apply_status_to_success'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contains a variety of settings that determine how the SCP should deliver the package. These values are all 

proprietary to the SCP and are designed to increase compatibility.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ScormPackageProperties'
GO


