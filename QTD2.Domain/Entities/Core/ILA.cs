using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Certifications;
using QTD2.Domain.Certifications.Implimentations.ConsistencyChecker;
using QTD2.Domain.Certifications.Interfaces;
using QTD2.Domain.Certifications.Models;
using QTD2.Domain.Entities.Common;
using QTD2.Domain.Helpers;

namespace QTD2.Domain.Entities.Core
{
    public class ILA : Entity
    {
        public string Name { get; set; }

        public string NickName { get; set; }

        public string Number { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public bool? WriitenOrOnlineAssessmentTool { get; set; }

        public bool? PerformAssessmentTool { get; set; }

        public string? OtherAssesmentTool { get; set; }

        public string? OtherNercTargetAudience { get; set; }

        public string TrainingPlan { get; set; }

        public int ProviderId { get; set; }

        public bool IsSelfPaced { get; set; }

        public bool IsOptional { get; set; }

        public bool IsPublished { get; set; }

        public DateTime? PublishDate { get; set; }
        public int? ClassSize { get; set; }

        public int? DeliveryMethodId { get; set; }

        public bool? HasPilotData { get; set; }  //2nd checkbox make nullable

        public bool? PilotDataNA { get; set; }  //2nd checkbox make nullable

        public bool? DoesActivityConform { get; set; }

        public bool IsProgramManual { get; set; }

        public DateOnly? SubmissionDate { get; set; }

        public DateOnly? ApprovalDate { get; set; }

        public DateOnly? ExpirationDate { get; set; }

        public DateTime EffectiveDate { get; set; }
        public DateOnly? StartDate { get; set; }

        public bool CBTRequiredForCourse { get; set; }

        public string TrainingEvalMethods { get; set; }

        public bool UseForEMP { get; set; }

        public double? TotalTrainingHours { get; set; }

        public string Prerequisites { get; set; }
        public bool IsPubliclyAvailable { get; set; }

        public virtual Provider Provider { get; set; }

        public virtual DeliveryMethod DeliveryMethod { get; set; }

        public virtual ILA_PerformTraineeEval ILA_PerformTraineeEval { get; set; }
        public virtual ICollection<ILACertificationLink> ILACertificationLinks { get; set; } = new List<ILACertificationLink>();

        public virtual ICollection<ILA_NercStandard_Link> ILA_NercStandard_Links { get; set; } = new List<ILA_NercStandard_Link>();

        public virtual ICollection<ILA_SafetyHazard_Link> ILA_SafetyHazard_Links { get; set; } = new List<ILA_SafetyHazard_Link>();

        public virtual ICollection<ILA_Segment_Link> ILA_Segment_Links { get; set; } = new List<ILA_Segment_Link>();

        public virtual ICollection<ILA_Position_Link> ILA_Position_Links { get; set; } = new List<ILA_Position_Link>();

        public virtual ICollection<ILACollaborator> ILACollaborators { get; set; } = new List<ILACollaborator>();

        public virtual ICollection<ILA_TaskObjective_Link> ILA_TaskObjective_Links { get; set; } = new List<ILA_TaskObjective_Link>();

        public virtual ICollection<ILA_EnablingObjective_Link> ILA_EnablingObjective_Links { get; set; } = new List<ILA_EnablingObjective_Link>();

        public virtual ICollection<ILA_Procedure_Link> ILA_Procedure_Links { get; set; } = new List<ILA_Procedure_Link>();

        public virtual ICollection<ILA_TrainingTopic_Link> ILA_TrainingTopic_Links { get; set; } = new List<ILA_TrainingTopic_Link>();

        public virtual ICollection<ILA_RegRequirement_Link> ILA_RegRequirement_Links { get; set; } = new List<ILA_RegRequirement_Link>();


        public virtual ICollection<ILA_AssessmentTool_Link> ILA_AssessmentTool_Links { get; set; } = new List<ILA_AssessmentTool_Link>();   //assesment tools

        public virtual ICollection<Procedure_ILA_Link> Procedure_ILA_Links { get; set; } = new List<Procedure_ILA_Link>();

        public virtual ICollection<SafetyHazard_ILA_Link> SafetyHazard_ILA_Links { get; set; } = new List<SafetyHazard_ILA_Link>();

        public virtual ICollection<ILA_StudentEvaluation_Link> ILA_StudentEvaluation_Links { get; set; } = new List<ILA_StudentEvaluation_Link>();

        // [InverseProperty(nameof(ILA_PreRequisite_Link.PreRequisite))]
        public virtual ICollection<ILA_PreRequisite_Link> ILA_PreRequisite_Links { get; set; } = new List<ILA_PreRequisite_Link>();
        public virtual ICollection<ILA_PreRequisite_Link> DependentILAs { get; set; } = new List<ILA_PreRequisite_Link>();

        public virtual ICollection<ILA_NERCAudience_Link> ILA_NERCAudience_Links { get; set; } = new List<ILA_NERCAudience_Link>();

        public virtual ICollection<Meta_ILAMembers_Link> Meta_ILAMembers_Links { get; set; } = new List<Meta_ILAMembers_Link>();

        public virtual ICollection<ILACustomObjective_Link> ILACustomObjective_Links { get; set; } = new List<ILACustomObjective_Link>();

        public virtual ICollection<ILATraineeEvaluation> ILATraineeEvaluations { get; set; } = new List<ILATraineeEvaluation>();

        public virtual ICollection<ILA_Upload> ILA_Uploads { get; set; } = new List<ILA_Upload>();

        public virtual ICollection<Task_ILA_Link> Task_ILA_Links { get; set; } = new List<Task_ILA_Link>();

        public virtual ICollection<ILAHistory> ILAHistories { get; set; } = new List<ILAHistory>();

        public virtual ICollection<SimulatorScenarioILA_Link_Old> SimulatorScenarioILA_Links { get; set; } = new List<SimulatorScenarioILA_Link_Old>();

        public virtual ICollection<Version_ILA> Version_ILAs { get; set; } = new List<Version_ILA>();

        public virtual ICollection<ClassSchedule> ClassSchedules { get; set; } = new List<ClassSchedule>();
        public virtual ICollection<IDP> IDPs { get; set; } = new List<IDP>();

        public virtual ICollection<CBT> CBTs { get; set; } = new List<CBT>();

        public virtual EvaluationReleaseEMPSettings EvaluationReleaseEMPSetting { get; set; }

        public virtual TestReleaseEMPSettings TestReleaseEMPSettings { get; set; }

        public virtual ILA_SelfRegistrationOptions ILA_SelfRegistrationOption { get; set; }

        public virtual ICollection<TQILAEmpSetting> TQILAEmpSettings { get; set; } = new List<TQILAEmpSetting>();

        public virtual ICollection<ILA_Evaluator_Link> ILA_Evaluator_Links { get; set; } = new List<ILA_Evaluator_Link>();

        public virtual ICollection<ILA_Resource> ILA_Resources { get; set; } = new List<ILA_Resource>();

        public virtual ICollection<TrainingPrograms_ILA_Link> TrainingPrograms_ILA_Links { get; set; } = new List<TrainingPrograms_ILA_Link>();
        public virtual ICollection<ILA_Topic_Link> ILA_Topic_Links { get; set; } = new List<ILA_Topic_Link>();
        public virtual ICollection<SimulatorScenario_Prerequisite> SimulatorScenario_Prerequisites { get; set; } = new List<SimulatorScenario_Prerequisite>();
        public virtual ICollection<SimulatorScenario_ILA> SimulatorScenario_ILAs { get; set; } = new List<SimulatorScenario_ILA>();
       
        public ILACertificationConsistencyChecker _consistencyChecker = new ILACertificationConsistencyChecker();

        [NotMapped]
        public List<CertifyingBodyConsistencyResult> ILACertificationConsistencyResults
        {
            get
            {
                var list = _consistencyChecker.CheckCertificationConsistency(this);
                return list;
            }
        }

        public ILA(string name, string nickName, string number, string description, string image, string trainingPlan, int providerId, bool isSelfPaced, bool isOptional, bool isPublished, DateTime? publishDate, int? deliveryMethodId, bool hasPilotData, bool isProgramManual, DateOnly? submissionDate, DateOnly? approvalDate, DateOnly? expirationDate, DateTime effectiveDate, bool useForEMP, string trainingEvalMethods, bool? pilotDataNA, bool? doesActivityConform)
        {
            Name = name;
            NickName = nickName;
            Number = number;
            Description = description;
            Image = image;
            ProviderId = providerId;
            IsSelfPaced = isSelfPaced;
            IsOptional = isOptional;
            IsPublished = isPublished;
            PublishDate = publishDate;
            DeliveryMethodId = deliveryMethodId;
            HasPilotData = hasPilotData;
            IsProgramManual = isProgramManual;
            SubmissionDate = submissionDate;
            ApprovalDate = approvalDate;
            ExpirationDate = expirationDate;
            TrainingPlan = trainingPlan;
            EffectiveDate = effectiveDate;
            UseForEMP = useForEMP;
            TrainingEvalMethods = trainingEvalMethods;
            PilotDataNA = pilotDataNA;
            DoesActivityConform = doesActivityConform;
        }

        public ILA(string name, string number, string description, double? totalHours, int providerId,int deliveryMethodId, bool isSelfPaced, DateTime effectiveDate)
        {
            Name = name;
            Number = number;
            Description = description;
            TotalTrainingHours = totalHours;
            ProviderId = providerId;
            DeliveryMethodId = deliveryMethodId;
            IsSelfPaced = isSelfPaced;
            EffectiveDate = effectiveDate;
            IsPublished = false;
            CBTRequiredForCourse = false;
            Deleted = false;
            Active = true;
            UseForEMP = false;
            IsOptional = true;
            IsProgramManual = false;
        }

        public ILA()
        {
        }

        public void SetCBTRequiredForCourse(bool cbtRequiredForCource)
        {
            CBTRequiredForCourse = cbtRequiredForCource;
        }

        public ILA_RegRequirement_Link LinkRegRequirement(RegulatoryRequirement regulatoryRequirement)
        {
            ILA_RegRequirement_Link ila_rr_link = ILA_RegRequirement_Links.FirstOrDefault(x => x.RegulatoryRequirementId == regulatoryRequirement.Id && x.ILAId == this.Id);
            if (ila_rr_link != null)
            {
                return ila_rr_link;
            }

            ila_rr_link = new ILA_RegRequirement_Link(this, regulatoryRequirement);
            AddEntityToNavigationProperty<ILA_RegRequirement_Link>(ila_rr_link);
            return ila_rr_link;
        }

        public void UnlinkRegRequirement(RegulatoryRequirement regulatoryRequirement)
        {
            ILA_RegRequirement_Link ila_rr_link = ILA_RegRequirement_Links.FirstOrDefault(x => x.RegulatoryRequirementId == regulatoryRequirement.Id && x.ILAId == this.Id);
            if (ila_rr_link != null)
            {
                RemoveEntityFromNavigationProperty<ILA_RegRequirement_Link>(ila_rr_link);
            }
        }

        public ILA_Evaluator_Link LinkEvaluator(Employee evaluator)
        {
            ILA_Evaluator_Link ila_eval_link = ILA_Evaluator_Links.FirstOrDefault(x => x.EvaluatorId == evaluator.Id && this.Id == x.ILAId);
            if (ila_eval_link != null)
            {
                return ila_eval_link;
            }

            ila_eval_link = new ILA_Evaluator_Link(this.Id, evaluator.Id);
            AddEntityToNavigationProperty<ILA_Evaluator_Link>(ila_eval_link);
            return ila_eval_link;
        }

        public void UnlinkEvaluator(Employee evaluator)
        {
            ILA_Evaluator_Link ila_eval_link = ILA_Evaluator_Links.FirstOrDefault(x => x.EvaluatorId == evaluator.Id && this.Id == x.ILAId);
            if (ila_eval_link != null)
            {
                RemoveEntityFromNavigationProperty<ILA_Evaluator_Link>(ila_eval_link);
            }
        }

        public void UnlinkRegRequirements()
        {
            List<ILA_RegRequirement_Link> ila_rr_link = ILA_RegRequirement_Links.Where(x => x.ILAId == this.Id).ToList();
            if (ila_rr_link.Count != 0)
            {
                RemoveEntitiesFromNavigationProperty<ILA_RegRequirement_Link>(ila_rr_link);
            }
        }

        public ILA_TrainingTopic_Link LinkTrainingTopic(TrainingTopic trainingTopic)
        {
            ILA_TrainingTopic_Link ila_tt_link = ILA_TrainingTopic_Links.FirstOrDefault(x => x.TrTopicId == trainingTopic.Id && x.ILAId == this.Id);
            if (ila_tt_link != null)
            {
                return ila_tt_link;
            }

            ila_tt_link = new ILA_TrainingTopic_Link(this, trainingTopic);
            AddEntityToNavigationProperty<ILA_TrainingTopic_Link>(ila_tt_link);
            return ila_tt_link;
        }

        public void UnlinkTrainingTopic(TrainingTopic trainingTopic)
        {
            ILA_TrainingTopic_Link ila_tt_link = ILA_TrainingTopic_Links.FirstOrDefault(x => x.TrTopicId == trainingTopic.Id && x.ILAId == this.Id);
            RemoveEntityFromNavigationProperty<ILA_TrainingTopic_Link>(ila_tt_link);
        }

        public ILA_Segment_Link LinkSegment(Segment segment, int order)
        {
            ILA_Segment_Link ila_seg_link = ILA_Segment_Links.FirstOrDefault(x => x.SegmentId == segment.Id && x.ILAId == this.Id);
            if (ila_seg_link != null)
            {
                return ila_seg_link;
            }

            ila_seg_link = new ILA_Segment_Link(this, segment);
            ila_seg_link.DisplayOrder = order;
            AddEntityToNavigationProperty<ILA_Segment_Link>(ila_seg_link);
            return ila_seg_link;
        }

        public void UnlinkSegment(Segment segment)
        {
            ILA_Segment_Link ila_seg_link = ILA_Segment_Links.FirstOrDefault(x => x.SegmentId == segment.Id && x.ILAId == this.Id);
            RemoveEntityFromNavigationProperty<ILA_Segment_Link>(ila_seg_link);
        }

        public ILA_Procedure_Link LinkProcedure(Procedure procedure)
        {
            ILA_Procedure_Link ila_proc_link = ILA_Procedure_Links.FirstOrDefault(x => x.ProcedureId == procedure.Id && x.ILAId == this.Id);
            if (ila_proc_link != null)
            {
                return ila_proc_link;
            }

            ila_proc_link = new ILA_Procedure_Link(this, procedure);
            AddEntityToNavigationProperty<ILA_Procedure_Link>(ila_proc_link);
            return ila_proc_link;
        }

        public void UnlinkProcedure(Procedure procedure)
        {
            ILA_Procedure_Link ila_proc_link = ILA_Procedure_Links.FirstOrDefault(x => x.ProcedureId == procedure.Id && x.ILAId == this.Id);
            if (ila_proc_link != null)
            {
                RemoveEntityFromNavigationProperty<ILA_Procedure_Link>(ila_proc_link);
            }
        }

        public void UnlinkProcedures()
        {
            List<ILA_Procedure_Link> ila_proc_link = ILA_Procedure_Links.Where(x => x.ILAId == this.Id).ToList();

            if (ila_proc_link.Count != 0)
            {
                RemoveEntitiesFromNavigationProperty<ILA_Procedure_Link>(ila_proc_link);
            }
        }

        public ILA_AssessmentTool_Link LinkAssessmentTool(AssessmentTool assessmentTool)
        {
            ILA_AssessmentTool_Link ila_AsTool_Link = ILA_AssessmentTool_Links.FirstOrDefault(x => x.AssessmentToolId == assessmentTool.Id && x.ILAId == this.Id);
            if (ila_AsTool_Link != null)
            {
                return ila_AsTool_Link;
            }

            ila_AsTool_Link = new ILA_AssessmentTool_Link(this, assessmentTool);
            AddEntityToNavigationProperty<ILA_AssessmentTool_Link>(ila_AsTool_Link);
            return ila_AsTool_Link;
        }

        public void UnlinkAssessmentTool(AssessmentTool assessmentTool)
        {
            ILA_AssessmentTool_Link ila_AsTool_Link = ILA_AssessmentTool_Links.FirstOrDefault(x => x.AssessmentToolId == assessmentTool.Id && x.ILAId == this.Id);
            if (ila_AsTool_Link != null)
            {
                RemoveEntityFromNavigationProperty<ILA_AssessmentTool_Link>(ila_AsTool_Link);
            }
        }

        public ILACollaborator LinkCollaborator(CollaboratorInvitation collaboratorInvitation)
        {
            ILACollaborator ilaCollaborator = ILACollaborators.FirstOrDefault(x => x.ILAId == this.Id && x.CollaboratorInviteId == collaboratorInvitation.Id);
            if (ilaCollaborator != null)
            {
                return ilaCollaborator;
            }

            ilaCollaborator = new ILACollaborator(this, collaboratorInvitation);
            AddEntityToNavigationProperty<ILACollaborator>(ilaCollaborator);
            return ilaCollaborator;
        }

        public void UnlinkCollaboratorTool(CollaboratorInvitation collaboratorInvitation)
        {
            ILACollaborator ilaCollaborator = ILACollaborators.FirstOrDefault(x => x.ILAId == this.Id && x.CollaboratorInviteId == collaboratorInvitation.Id);
            if (ilaCollaborator != null)
            {
                RemoveEntityFromNavigationProperty<ILACollaborator>(ilaCollaborator);
            }
        }

        public ILA_TaskObjective_Link LinkTaskObjective(Task task,int sequenceNumber = 0)
        {
            ILA_TaskObjective_Link ila_to_link = ILA_TaskObjective_Links.FirstOrDefault(x => x.ILAId == this.Id && x.TaskId == task.Id);
            if (ila_to_link != null)
            {
                ila_to_link.SequenceNumber = sequenceNumber;
                return ila_to_link;
            }

            ila_to_link = new ILA_TaskObjective_Link(this, task,sequenceNumber);
            AddEntityToNavigationProperty<ILA_TaskObjective_Link>(ila_to_link);
            return ila_to_link;
        }

        public void UnlinkTaskObjective(Task task)
        {
            ILA_TaskObjective_Link ila_to_link = ILA_TaskObjective_Links.FirstOrDefault(x => x.ILAId == this.Id && x.TaskId == task.Id);
            if (ila_to_link != null)
            {
                RemoveEntityFromNavigationProperty<ILA_TaskObjective_Link>(ila_to_link);
            }
        }

        public ILA_EnablingObjective_Link LinkEnablingObjective(EnablingObjective eo)
        {
            ILA_EnablingObjective_Link ila_eo_link = ILA_EnablingObjective_Links.FirstOrDefault(x => x.ILAId == this.Id && x.EnablingObjectiveId == eo.Id);
            if (ila_eo_link != null)
            {
                return ila_eo_link;
            }

            ila_eo_link = new ILA_EnablingObjective_Link(this, eo);
            AddEntityToNavigationProperty<ILA_EnablingObjective_Link>(ila_eo_link);
            return ila_eo_link;
        }

        public void UnlinkEnablingObjectives(EnablingObjective eo)
        {
            ILA_EnablingObjective_Link ila_eo_link = ILA_EnablingObjective_Links.FirstOrDefault(x => x.ILAId == this.Id && x.EnablingObjectiveId == eo.Id);
            if (ila_eo_link != null)
            {
                RemoveEntityFromNavigationProperty<ILA_EnablingObjective_Link>(ila_eo_link);
            }
        }

        public ILA_SafetyHazard_Link LinkSafetyHazards(SaftyHazard sh)
        {
            ILA_SafetyHazard_Link ila_sh_link = ILA_SafetyHazard_Links.FirstOrDefault(x => x.ILAId == this.Id && x.SafetyHazardId == sh.Id);
            if (ila_sh_link != null)
            {
                return ila_sh_link;
            }

            ila_sh_link = new ILA_SafetyHazard_Link(this, sh);
            AddEntityToNavigationProperty<ILA_SafetyHazard_Link>(ila_sh_link);
            return ila_sh_link;
        }

        public void UnlinkSafetyHazards(SaftyHazard sh)
        {
            ILA_SafetyHazard_Link ila_sh_link = ILA_SafetyHazard_Links.FirstOrDefault(x => x.ILAId == this.Id && x.SafetyHazardId == sh.Id);
            if (ila_sh_link != null)
            {
                RemoveEntityFromNavigationProperty<ILA_SafetyHazard_Link>(ila_sh_link);
            }
        }

        public void UnlinkSafetyHazards()
        {
            List<ILA_SafetyHazard_Link> ila_sh_link = ILA_SafetyHazard_Links.Where(x => x.ILAId == this.Id).ToList();
            if (ila_sh_link != null)
            {
                RemoveEntitiesFromNavigationProperty<ILA_SafetyHazard_Link>(ila_sh_link);
            }
        }

        public void UnLinkStudentEvaluation()
        {
            List<ILA_StudentEvaluation_Link> ila_se_link = ILA_StudentEvaluation_Links.Where(x => x.ILAId == this.Id).ToList();
            if (ila_se_link != null)
            {
                RemoveEntitiesFromNavigationProperty<ILA_StudentEvaluation_Link>(ila_se_link);
            }
        }

        public ILA_Position_Link LinkPosition(Position pos)
        {
            ILA_Position_Link ila_pos_link = ILA_Position_Links.FirstOrDefault(x => x.ILAId == this.Id && x.PositionId == pos.Id);
            if (ila_pos_link != null)
            {
                return ila_pos_link;
            }

            ila_pos_link = new ILA_Position_Link(this, pos);
            AddEntityToNavigationProperty<ILA_Position_Link>(ila_pos_link);
            return ila_pos_link;
        }

        public void UnlinkPosition(Position pos)
        {
            ILA_Position_Link ila_pos_link = ILA_Position_Links.FirstOrDefault(x => x.ILAId == this.Id && x.PositionId == pos.Id);
            if (ila_pos_link != null)
            {
                RemoveEntityFromNavigationProperty<ILA_Position_Link>(ila_pos_link);
            }
        }

        public ILA_NercStandard_Link LinkNercStandard(NercStandard nps, NercStandardMember nsm, float creditHoursByStd)
        {
            ILA_NercStandard_Link ila_nps_link = ILA_NercStandard_Links.FirstOrDefault(x => x.ILAId == this.Id && x.StdId == nps.Id && x.NERCStdMemberId == nsm.Id);
            if (ila_nps_link != null)
            {
                return ila_nps_link;
            }

            ila_nps_link = new ILA_NercStandard_Link(this, nps, nsm, creditHoursByStd);
            AddEntityToNavigationProperty<ILA_NercStandard_Link>(ila_nps_link);
            return ila_nps_link;
        }

        public void UnlinkNercStandard(NercStandard nps, NercStandardMember nsm)
        {
            ILA_NercStandard_Link ila_nps_link = ILA_NercStandard_Links.FirstOrDefault(x => x.ILAId == this.Id && x.StdId == nps.Id && x.NERCStdMemberId == nsm.Id);
            if (ila_nps_link != null)
            {
                RemoveEntityFromNavigationProperty<ILA_NercStandard_Link>(ila_nps_link);
            }
        }

        public ILA_PreRequisite_Link LinkPreRequisite(ILA preReq_ila)
        {
            ILA_PreRequisite_Link ila_preReq_link = ILA_PreRequisite_Links.FirstOrDefault(x => x.ILAId == this.Id && x.PreRequisiteId == preReq_ila.Id);
            if (ila_preReq_link != null)
            {
                return ila_preReq_link;
            }

            ila_preReq_link = new ILA_PreRequisite_Link(this, preReq_ila);
            AddEntityToNavigationProperty<ILA_PreRequisite_Link>(ila_preReq_link);
            return ila_preReq_link;
        }

        public void UnlinkPreRequisite(ILA preReq_ila)
        {
            ILA_PreRequisite_Link ila_preReq_link = ILA_PreRequisite_Links.FirstOrDefault(x =>x.ILAId == this.Id && x.PreRequisiteId == preReq_ila.Id);
            if (ila_preReq_link != null)
            {
                RemoveEntityFromNavigationProperty<ILA_PreRequisite_Link>(ila_preReq_link);
            }
        }

        public void UnlinkPreRequisites()
        {
            List<ILA_PreRequisite_Link> ila_preReq_link = ILA_PreRequisite_Links.Where(x => x.ILAId == this.Id).ToList();
            if (ila_preReq_link.Count != 0)
            {
                RemoveEntitiesFromNavigationProperty<ILA_PreRequisite_Link>(ila_preReq_link);
            }
        }

        /* link and unlink for ILA_NERCAudience_Link starts */
        public ILA_NERCAudience_Link LinkNERCAudience(NERCTargetAudience nercTargetAudience)
        {
            ILA_NERCAudience_Link ila_NERCAudience_Link = ILA_NERCAudience_Links.FirstOrDefault(x => x.NERCAudienceID == nercTargetAudience.Id && x.ILAId == this.Id);
            if (ila_NERCAudience_Link != null)
            {
                return ila_NERCAudience_Link;
            }

            ila_NERCAudience_Link = new ILA_NERCAudience_Link(this, nercTargetAudience);
            AddEntityToNavigationProperty<ILA_NERCAudience_Link>(ila_NERCAudience_Link);
            return ila_NERCAudience_Link;
        }

        public void UnlinkNERCAudience(NERCTargetAudience nercTargetAudience)
        {
            ILA_NERCAudience_Link ila_NERCAudience_Link = ILA_NERCAudience_Links.FirstOrDefault(x => x.NERCAudienceID == nercTargetAudience.Id && x.ILAId == this.Id);
            if (ila_NERCAudience_Link != null)
            {
                RemoveEntityFromNavigationProperty<ILA_NERCAudience_Link>(ila_NERCAudience_Link);
            }
        }

        public ILA_StudentEvaluation_Link LinkstudentEvaluation(StudentEvaluation studentEvaluationForm, StudentEvaluationAvailability studentEvaluationAvailability, StudentEvaluationAudience studentEvaluationAudience)
        {
            ILA_StudentEvaluation_Link ila_SE_link = ILA_StudentEvaluation_Links.FirstOrDefault(x => x.studentEvalFormID == studentEvaluationForm.Id && x.ILAId == this.Id);
            if (ila_SE_link != null)
            {
                return ila_SE_link;
            }

            ila_SE_link = new ILA_StudentEvaluation_Link(this, studentEvaluationForm);
            AddEntityToNavigationProperty<ILA_StudentEvaluation_Link>(ila_SE_link);
            return ila_SE_link;
        }

        public void UnlinkStudentEvaluation(StudentEvaluation studentEvaluation, StudentEvaluationAvailability studentEvaluationAvailability, StudentEvaluationAudience studentEvaluationAudience)
        {
            ILA_StudentEvaluation_Link ila_se_link = ILA_StudentEvaluation_Links.FirstOrDefault(x => x.studentEvalFormID == studentEvaluation.Id && x.studentEvalAvailabilityID == studentEvaluationAvailability.Id && x.studentEvalAudienceID == studentEvaluationAudience.Id && x.ILAId == this.Id);
            if (ila_se_link != null)
            {
                RemoveEntityFromNavigationProperty<ILA_StudentEvaluation_Link>(ila_se_link);
            }
        }

        public ILACustomObjective_Link LinkCustomEnablingObjective(CustomEnablingObjective customEO)
        {
            ILACustomObjective_Link iLACustomObj_Link = ILACustomObjective_Links.FirstOrDefault(x => x.ILAId == this.Id && x.CustomObjId == customEO.Id);
            if (iLACustomObj_Link != null)
            {
                return iLACustomObj_Link;
            }

            iLACustomObj_Link = new ILACustomObjective_Link(this, customEO);
            AddEntityToNavigationProperty<ILACustomObjective_Link>(iLACustomObj_Link);
            return iLACustomObj_Link;
        }

        public void UnLinkCustomEnablingObjective(CustomEnablingObjective customEO)
        {
            ILACustomObjective_Link iLACustomObjective_Link = ILACustomObjective_Links.FirstOrDefault(x => x.CustomObjId == customEO.Id && x.ILAId == this.Id);
            if (iLACustomObjective_Link != null)
            {
                RemoveEntityFromNavigationProperty<ILACustomObjective_Link>(iLACustomObjective_Link);
            }
        }

        public ILA_Topic_Link LinkILATopic(ILA_Topic topic)
        {
            ILA_Topic_Link ila_topic_link = ILA_Topic_Links.FirstOrDefault(x => x.ILAId == this.Id && x.ILATopicId == topic.Id);
            if (ila_topic_link != null)
            {
                return ila_topic_link;
            }

            ila_topic_link = new ILA_Topic_Link(this, topic);
            AddEntityToNavigationProperty<ILA_Topic_Link>(ila_topic_link);
            return ila_topic_link;
        }
        public Version_ILA CreateSnapshot()
        {
            return new Version_ILA(this);
        }

        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<T>(createdBy) as ILA;

            copy.Name = "Copy - " + this.Name;
            copy.Number = "Copy - " + this.Number;
            copy.IsPublished = false;

            //ensure includes
            copy.ILACertificationLinks = new List<ILACertificationLink>();
            foreach (var ilaCertLink in this.ILACertificationLinks)
            {
                var ilaCertLinkcopy = ilaCertLink.Copy<ILACertificationLink>(createdBy);
                ilaCertLinkcopy.ILAId = 0;
                copy.ILACertificationLinks.Add(ilaCertLinkcopy);
            }

            //public virtual ICollection<ILA_NercStandard_Link> ILA_NercStandard_Links { get; set; } = new List<ILA_NercStandard_Link>();
            foreach (var nercStandarLink in this.ILA_NercStandard_Links)
            {
                var nercStandarLinkcopy = nercStandarLink.Copy<ILA_NercStandard_Link>(createdBy);
                nercStandarLinkcopy.ILAId = 0;
                copy.ILA_NercStandard_Links.Add(nercStandarLinkcopy);
            }

            //public virtual ICollection<ILA_SafetyHazard_Link> ILA_SafetyHazard_Links { get; set; } = new List<ILA_SafetyHazard_Link>();
            foreach (var saftyHazardLink in this.ILA_SafetyHazard_Links)
            {
                var ilaSaftyHazardLinkcopy = saftyHazardLink.Copy<ILA_SafetyHazard_Link>(createdBy);
                ilaSaftyHazardLinkcopy.ILAId = 0;
                copy.ILA_SafetyHazard_Links.Add(ilaSaftyHazardLinkcopy);
            }

            //public virtual ICollection<ILA_Segment_Link> ILA_Segment_Links { get; set; } = new List<ILA_Segment_Link>();
            foreach (var ilaSegmentLink in this.ILA_Segment_Links)
            {
                //TODO Impliment
                var ilaSegmentLinkcopy = ilaSegmentLink.Copy<ILA_Segment_Link>(createdBy);
                ilaSegmentLinkcopy.ILAId = 0;
                ilaSegmentLink.SegmentId = 0;
                copy.ILA_Segment_Links.Add(ilaSegmentLinkcopy);
            }

            //public virtual ICollection<ILA_Position_Link> ILA_Position_Links { get; set; } = new List<ILA_Position_Link>();
            foreach (var ilaPositionLink in this.ILA_Position_Links)
            {
                var ilaPositionLinkcopy = ilaPositionLink.Copy<ILA_Position_Link>(createdBy);
                ilaPositionLink.ILAId = 0;
                copy.ILA_Position_Links.Add(ilaPositionLinkcopy);
            }
            //public virtual ICollection<ILA_Topic_Link> ILA_Topic_Links { get; set; } = new List<ILA_Topic_Link>();
            foreach (var ilaTopicLink in this.ILA_Topic_Links)
            {
                var ilaTopicLinkcopy = ilaTopicLink.Copy<ILA_Topic_Link>(createdBy);
                ilaTopicLinkcopy.ILAId = 0;
                copy.ILA_Topic_Links.Add(ilaTopicLinkcopy);
            }

            //public virtual ICollection<ILACollaborator> ILACollaborators { get; set; } = new List<ILACollaborator>();
            foreach (var iLACollaborator in this.ILACollaborators)
            {
                var iLACollaboratorcopy = iLACollaborator.Copy<ILACollaborator>(createdBy);
                iLACollaboratorcopy.ILAId = 0;
                copy.ILACollaborators.Add(iLACollaboratorcopy);
            }

            //public virtual ICollection<ILA_TaskObjective_Link> ILA_TaskObjective_Links { get; set; } = new List<ILA_TaskObjective_Link>();
            foreach (var iLA_TaskObjective_Links in this.ILA_TaskObjective_Links)
            {
                var iLA_TaskObjective_Linkscopy = iLA_TaskObjective_Links.Copy<ILA_TaskObjective_Link>(createdBy);
                iLA_TaskObjective_Linkscopy.ILAId = 0;
                copy.ILA_TaskObjective_Links.Add(iLA_TaskObjective_Linkscopy);
            }

            //public virtual ICollection<ILA_EnablingObjective_Link> ILA_EnablingObjective_Links { get; set; } = new List<ILA_EnablingObjective_Link>();
            foreach (var iLA_EnablingObjective_Link in this.ILA_EnablingObjective_Links)
            {
                var iLA_EnablingObjective_Linkcopy = iLA_EnablingObjective_Link.Copy<ILA_EnablingObjective_Link>(createdBy);
                iLA_EnablingObjective_Linkcopy.ILAId = 0;
                copy.ILA_EnablingObjective_Links.Add(iLA_EnablingObjective_Linkcopy);
            }

            //public virtual ICollection<ILA_Procedure_Link> ILA_Procedure_Links { get; set; } = new List<ILA_Procedure_Link>();
            foreach (var iLA_Procedure_Link in this.ILA_Procedure_Links)
            {
                var iLA_Procedure_Linkcopy = iLA_Procedure_Link.Copy<ILA_Procedure_Link>(createdBy);
                iLA_Procedure_Linkcopy.ILAId = 0;
                copy.ILA_Procedure_Links.Add(iLA_Procedure_Linkcopy);
            }

            //public virtual ICollection<ILA_TrainingTopic_Link> ILA_TrainingTopic_Links { get; set; } = new List<ILA_TrainingTopic_Link>();
            foreach (var iLA_TrainingTopic_Link in this.ILA_TrainingTopic_Links)
            {
                var iLA_TrainingTopic_Linkcopy = iLA_TrainingTopic_Link.Copy<ILA_TrainingTopic_Link>(createdBy);
                iLA_TrainingTopic_Linkcopy.ILAId = 0;
                copy.ILA_TrainingTopic_Links.Add(iLA_TrainingTopic_Linkcopy);
            }

            //public virtual ICollection<ILA_RegRequirement_Link> ILA_RegRequirement_Links { get; set; } = new List<ILA_RegRequirement_Link>();
            foreach (var iLA_RegRequirement_Link in this.ILA_RegRequirement_Links)
            {
                var iLA_RegRequirement_Linkcopy = iLA_RegRequirement_Link.Copy<ILA_RegRequirement_Link>(createdBy);
                iLA_RegRequirement_Linkcopy.ILAId = 0;
                copy.ILA_RegRequirement_Links.Add(iLA_RegRequirement_Linkcopy);
            }

            //public virtual ICollection<ILA_AssessmentTool_Link> ILA_AssessmentTool_Links { get; set; } = new List<ILA_AssessmentTool_Link>();   //assesment tools
            foreach (var iLA_AssessmentTool_Link in this.ILA_AssessmentTool_Links)
            {
                var iLA_AssessmentTool_Linkcopy = iLA_AssessmentTool_Link.Copy<ILA_AssessmentTool_Link>(createdBy);
                iLA_AssessmentTool_Linkcopy.ILAId = 0;
                copy.ILA_AssessmentTool_Links.Add(iLA_AssessmentTool_Linkcopy);
            }

            //public virtual ICollection<SafetyHazard_ILA_Link> SafetyHazard_ILA_Links { get; set; } = new List<SafetyHazard_ILA_Link>();
            foreach (var safetyHazard_ILA_Link in this.SafetyHazard_ILA_Links)
            {
                var safetyHazard_ILA_Linkcopy = safetyHazard_ILA_Link.Copy<SafetyHazard_ILA_Link>(createdBy);
                safetyHazard_ILA_Linkcopy.ILAId = 0;
                copy.SafetyHazard_ILA_Links.Add(safetyHazard_ILA_Linkcopy);
            }

            //public virtual ICollection<ILA_StudentEvaluation_Link> ILA_StudentEvaluation_Links { get; set; } = new List<ILA_StudentEvaluation_Link>();
            foreach (var iLA_StudentEvaluation_Link in this.ILA_StudentEvaluation_Links)
            {
                var iLA_StudentEvaluation_Linkcopy = iLA_StudentEvaluation_Link.Copy<ILA_StudentEvaluation_Link>(createdBy);
                iLA_StudentEvaluation_Linkcopy.ILAId = 0;
                copy.ILA_StudentEvaluation_Links.Add(iLA_StudentEvaluation_Linkcopy);
            }

            //public virtual ICollection<ILA_PreRequisite_Link> ILA_PreRequisite_Links { get; set; } = new List<ILA_PreRequisite_Link>();
            foreach (var iLA_PreRequisite_Link in this.ILA_PreRequisite_Links)
            {
                var iLA_PreRequisite_Linkcopy = iLA_PreRequisite_Link.Copy<ILA_PreRequisite_Link>(createdBy);
                iLA_PreRequisite_Linkcopy.ILAId = 0;
                copy.ILA_PreRequisite_Links.Add(iLA_PreRequisite_Linkcopy);
            }

            //public virtual ICollection<ILA_NERCAudience_Link> ILA_NERCAudience_Links { get; set; } = new List<ILA_NERCAudience_Link>();
            foreach (var iLA_NERCAudience_Link in this.ILA_NERCAudience_Links)
            {
                var iLA_NERCAudience_Linkcopy = iLA_NERCAudience_Link.Copy<ILA_NERCAudience_Link>(createdBy);
                iLA_NERCAudience_Linkcopy.ILAId = 0;
                copy.ILA_NERCAudience_Links.Add(iLA_NERCAudience_Linkcopy);
            }

            //SKIP
            //public virtual ICollection<Meta_ILAMembers_Link> Meta_ILAMembers_Links { get; set; } = new List<Meta_ILAMembers_Link>();

            //public virtual ICollection<ILACustomObjective_Link> ILACustomObjective_Links { get; set; } = new List<ILACustomObjective_Link>();
            foreach (var iLACustomObjective_Link in this.ILACustomObjective_Links)
            {
                var iLACustomObjective_Linkcopy = iLACustomObjective_Link.Copy<ILACustomObjective_Link>(createdBy);
                iLACustomObjective_Linkcopy.ILAId = 0;
                copy.ILACustomObjective_Links.Add(iLACustomObjective_Linkcopy);
            }

            //public virtual ICollection<ILATraineeEvaluation> ILATraineeEvaluations { get; set; } = new List<ILATraineeEvaluation>();
            foreach (var iLATraineeEvaluation in this.ILATraineeEvaluations)
            {
                var iLATraineeEvaluationcopy = iLATraineeEvaluation.Copy<ILATraineeEvaluation>(createdBy);
                iLATraineeEvaluationcopy.ILAId = 0;
                copy.ILATraineeEvaluations.Add(iLATraineeEvaluationcopy);
            }

            //public virtual ICollection<ILA_Upload> ILA_Uploads { get; set; } = new List<ILA_Upload>();
            foreach (var iLA_Upload in this.ILA_Uploads)
            {
                var iLA_Uploadcopy = iLA_Upload.Copy<ILA_Upload>(createdBy);
                iLA_Uploadcopy.ILAId = 0;
                copy.ILA_Uploads.Add(iLA_Uploadcopy);
            }

            //public virtual ICollection<Task_ILA_Link> Task_ILA_Links { get; set; } = new List<Task_ILA_Link>();
            foreach (var task_ILA_Link in this.Task_ILA_Links)
            {
                var task_ILA_Linkcopy = task_ILA_Link.Copy<Task_ILA_Link>(createdBy);
                task_ILA_Linkcopy.ILAId = 0;
                copy.Task_ILA_Links.Add(task_ILA_Linkcopy);
            }

            //SKIP
            //public virtual ICollection<ILAHistory> ILAHistories { get; set; } = new List<ILAHistory>();

            //public virtual ICollection<SimulatorScenarioILA_Link> SimulatorScenarioILA_Links { get; set; } = new List<SimulatorScenarioILA_Link>();
            foreach (var simulatorScenarioILA_Link in this.SimulatorScenarioILA_Links)
            {
                var simulatorScenarioILA_Linkcopy = simulatorScenarioILA_Link.Copy<SimulatorScenarioILA_Link_Old>(createdBy);
                simulatorScenarioILA_Linkcopy.ILAID = 0;
                copy.SimulatorScenarioILA_Links.Add(simulatorScenarioILA_Linkcopy);
            }

            //SKIP
            //public virtual ICollection<Version_ILA> Version_ILAs { get; set; } = new List<Version_ILA>();

            //SKIP
            //public virtual ICollection<ClassSchedule> ClassSchedules { get; set; } = new List<ClassSchedule>();

            //SKIP
            //public virtual ICollection<IDP> IDPs { get; set; } = new List<IDP>();

            //SKIP
            //public virtual ICollection<CBT> CBTs { get; set; } = new List<CBT>();

            //public virtual EvaluationReleaseEMPSettings EvaluationReleaseEMPSetting { get; set; }
            if(this.EvaluationReleaseEMPSetting != null)
            {
                copy.EvaluationReleaseEMPSetting = this.EvaluationReleaseEMPSetting.Copy<EvaluationReleaseEMPSettings>(createdBy);
                copy.EvaluationReleaseEMPSetting.ILAId = 0;
            }


            //public virtual TestReleaseEMPSettings TestReleaseEMPSettings { get; set; }
            if (this.TestReleaseEMPSettings != null)
            {
                copy.TestReleaseEMPSettings = this.TestReleaseEMPSettings.Copy<TestReleaseEMPSettings>(createdBy);
                copy.TestReleaseEMPSettings.ILAId = 0;
                // copy.TestReleaseEMPSettings.FinalTestId = null;
                // copy.TestReleaseEMPSettings.FinalTest = null;
                // copy.TestReleaseEMPSettings.PreTestId = null;
                // copy.TestReleaseEMPSettings.PreTest = null;
            }


            //public virtual ILA_SelfRegistrationOptions ILA_SelfRegistrationOption { get; set; }
            if (this.ILA_SelfRegistrationOption != null)
            {
                copy.ILA_SelfRegistrationOption = this.ILA_SelfRegistrationOption.Copy<ILA_SelfRegistrationOptions>(createdBy);
                copy.ILA_SelfRegistrationOption.ILAId = 0;
            }
      

            //public virtual ICollection<TQILAEmpSetting> TQILAEmpSettings { get; set; } = new List<TQILAEmpSetting>();
            foreach (var tQILAEmpSetting in this.TQILAEmpSettings)
            {
                var tQILAEmpSettingcopy = tQILAEmpSetting.Copy<TQILAEmpSetting>(createdBy);
                tQILAEmpSettingcopy.ILAId = 0;
                copy.TQILAEmpSettings.Add(tQILAEmpSettingcopy);
            }

            //public virtual ICollection<ILA_Evaluator_Link> ILA_Evaluator_Links { get; set; } = new List<ILA_Evaluator_Link>();
            foreach (var iLA_Evaluator_Link in this.ILA_Evaluator_Links)
            {
                var iLA_Evaluator_Linkcopy = iLA_Evaluator_Link.Copy<ILA_Evaluator_Link>(createdBy);
                iLA_Evaluator_Linkcopy.ILAId = 0;
                copy.ILA_Evaluator_Links.Add(iLA_Evaluator_Linkcopy);
            }

            //public virtual ICollection<ILA_Resource> ILA_Resources { get; set; } = new List<ILA_Resource>();
            foreach (var iLA_Resource in this.ILA_Resources)
            {
                var iLA_Resourcecopy = iLA_Resource.Copy<ILA_Resource>(createdBy);
                iLA_Resourcecopy.ILAId = 0;
                copy.ILA_Resources.Add(iLA_Resourcecopy);
            }

            //public virtual ICollection<TrainingPrograms_ILA_Link> TrainingPrograms_ILA_Links { get; set; } = new List<TrainingPrograms_ILA_Link>();
            foreach (var trainingPrograms_ILA_Link in this.TrainingPrograms_ILA_Links)
            {
                var trainingPrograms_ILA_Linkcopy = trainingPrograms_ILA_Link.Copy<TrainingPrograms_ILA_Link>(createdBy);
                trainingPrograms_ILA_Linkcopy.ILAId = 0;
                copy.TrainingPrograms_ILA_Links.Add(trainingPrograms_ILA_Linkcopy);
            }

            return (T)(object)copy;
        }

        public override void Delete()
        {
            AddDomainEvent(new Domain.Events.Core.OnILA_Deleted(this));
            base.Delete();
        }

        public void OrderObjectives()
        {
            int currentOrder = 1;
            Dictionary<string, List<int>> usedValues = new Dictionary<string, List<int>>();

            usedValues.Add("Task", new List<int>());
            usedValues.Add("EO", new List<int>());
            usedValues.Add("CustomEO", new List<int>());

            foreach (var segmentLink in ILA_Segment_Links.Where(r=> r.Active && !r.Deleted).OrderBy(r => r.DisplayOrder))
            {
                foreach (var objectiveLink in segmentLink.Segment.SegmentObjective_Links.Where(r => r.Active && !r.Deleted).OrderBy(r => r.Order))
                {
                    string type = "";
                    int objectiveLinkId = -1;

                    if (objectiveLink.TaskId.HasValue)
                    {
                        type = "Task";
                        objectiveLinkId = objectiveLink.TaskId.Value;
                    }

                    else if (objectiveLink.EnablingObjectiveId.HasValue)
                    {
                        type = "EO";
                        objectiveLinkId = objectiveLink.EnablingObjectiveId.Value;
                    }

                    else if (objectiveLink.CustomEOId.HasValue)
                    {
                        type = "CustomEO";
                        objectiveLinkId = objectiveLink.CustomEOId.Value;
                    }

                    List<int> usedIds = usedValues[type];

                    if (usedIds == null)
                    {
                        usedValues.Add(type, new List<int>() { objectiveLinkId });
                    }

                    else
                    {
                        if (usedIds.Contains(objectiveLinkId)) continue;

                        if (objectiveLink.TaskId.HasValue)
                        {
                            var link = ILA_TaskObjective_Links.Where(r => r.TaskId == objectiveLinkId).FirstOrDefault();
                            if (link != null) link.UpdateOrder(currentOrder);
                        }

                        else if (objectiveLink.EnablingObjectiveId.HasValue)
                        {
                            var link = ILA_EnablingObjective_Links.Where(r => r.EnablingObjectiveId == objectiveLinkId).FirstOrDefault();
                            if (link != null) link.UpdateOrder(currentOrder);
                        }

                        else if (objectiveLink.CustomEOId.HasValue)
                        {
                            var link = ILACustomObjective_Links.Where(r => r.CustomObjId == objectiveLinkId).FirstOrDefault();
                            if (link != null) link.UpdateOrder(currentOrder);
                        }

                        currentOrder = currentOrder += 1;
                        usedIds.Add(objectiveLinkId);
                    }
                }
            }

            foreach (var taskObjectiveLink in ILA_TaskObjective_Links.OrderBy(itl => itl.Task.FullNumber, new AlphaNumericSortHelper()))
            {
                var usedIds = usedValues["Task"];
                if (!usedIds.Contains(taskObjectiveLink.TaskId))
                {
                    taskObjectiveLink.UpdateOrder(currentOrder);
                    currentOrder += 1;
                }
            }

            foreach (var enablingObjectiveLink in ILA_EnablingObjective_Links.OrderBy(itl => itl.EnablingObjective.FullNumber, new AlphaNumericSortHelper())) 
            {
                var usedIds = usedValues["EO"];
                if (!usedIds.Contains(enablingObjectiveLink.EnablingObjectiveId))
                {
                    enablingObjectiveLink.UpdateOrder(currentOrder);
                    currentOrder += 1;
                }
            }

            foreach (var customEoLink in ILACustomObjective_Links.OrderBy(itl => itl.CustomEnablingObjective.FullNumber, new AlphaNumericSortHelper())) 
            {
                var usedIds = usedValues["CustomEO"];
                if (!usedIds.Contains(customEoLink.CustomObjId))
                {
                    customEoLink.UpdateOrder(currentOrder);
                    currentOrder += 1;
                }
            }
        }

        public override void Deactivate()
        {
            base.Deactivate();
            AddDomainEvent(new Domain.Events.Core.OnILA_Deactivated(this));
        }
        public override void Activate()
        {
            base.Activate();
            AddDomainEvent(new Domain.Events.Core.OnILA_Activated(this));
        }
    }
}
