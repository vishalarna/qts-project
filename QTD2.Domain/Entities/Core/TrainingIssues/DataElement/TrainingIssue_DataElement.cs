using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TrainingIssue_DataElement : Common.Entity
    {
        public int TrainingIssueId { get; set; }
        public TrainingIssue TrainingIssue { get; set; }
        public string DataElementDisplay
        {
            get {
                return this.GetType().Name.Replace("DataElement_", "");
            }
            set { }
        }
        public TrainingIssue_DataElement(int trainingIssueId)
        {
            TrainingIssueId = trainingIssueId;
        }

        public TrainingIssue_DataElement()
        {

        }
        public void Update(int? dataElementId)
        {
            switch (this)
            {
                case DataElement_Procedure dataElementProcedure:
                    dataElementProcedure.ProcedureId = dataElementId;
                    break;
                case DataElement_RegulatoryRequirement dataElementRegRequirement:
                    dataElementRegRequirement.RegulatoryRequirementId = dataElementId;
                    break;
                case DataElement_SafetyHazard dataElementSafetyHazard:
                    dataElementSafetyHazard.SafetyHazardId = dataElementId;
                    break;
                case DataElement_Tool dataElementTool:
                    dataElementTool.ToolId = dataElementId;
                    break;
                case DataElement_ILAsCourses dataElementILAsCourses:
                    dataElementILAsCourses.ILAId = dataElementId;
                    break;
                case DataElement_MetaILAsCourses dataElementMetaILAsCourses:
                    dataElementMetaILAsCourses.MetaILAId = dataElementId;
                    break;
                case DataElement_Test dataElementTest:
                    dataElementTest.TestId = dataElementId;
                    break;
                case DataElement_Pretest dataElementPreTest:
                    dataElementPreTest.PreTestId = dataElementId;
                    break;
                case DataElement_ComputerBasedTraining dataElementComputerBasedTraining:
                    dataElementComputerBasedTraining.CBT_ScormUploadId = dataElementId;
                    break;
                case DataElement_TrainingProgram dataElementTrainingProgram:
                    AddDomainEvent(new Domain.Events.Core.OnTrainingIssue_DataElementUpdate(dataElementTrainingProgram));
                    dataElementTrainingProgram.TrainingProgramId = dataElementId;
                    break;
                case DataElement_Task dataElementTask:
                    dataElementTask.TaskId = dataElementId;
                    break;
                case DataElement_TestItem dataElementTestItem:
                    dataElementTestItem.TestItemId = dataElementId;
                    break;
                case DataElement_EnablingObjective dataElement_EnablingObjective:
                    dataElement_EnablingObjective.EnablingObjectiveId = dataElementId;
                    break;
                case DataElement_MetaTask dataElement_MetaTask:
                    dataElement_MetaTask.MetaTaskId = dataElementId;
                    break;
                case DataElement_MetaEnablingObjective dataElement_MetaEnablingObjective:
                    dataElement_MetaEnablingObjective.MetaEnablingObjectiveId = dataElementId;
                    break;
            }
        }

        public override void Delete()
        {
            base.Delete();
            AddDomainEvent(new Domain.Events.Core.OnTrainingIssue_DataElementUpdate(this));
        }

        public TrainingIssue_DataElement CopyDataElement<T>(string createdBy)
        {
            switch (this)
            {
                case DataElement_Procedure original:
                    return new DataElement_Procedure
                    {
                        ProcedureId = original.ProcedureId,
                        CreatedBy = createdBy,
                        CreatedDate = DateTime.UtcNow
                    };

                case DataElement_RegulatoryRequirement original:
                    return new DataElement_RegulatoryRequirement
                    {
                        RegulatoryRequirementId = original.RegulatoryRequirementId,
                        CreatedBy = createdBy,
                        CreatedDate = DateTime.UtcNow
                    };

                case DataElement_SafetyHazard original:
                    return new DataElement_SafetyHazard
                    {
                        SafetyHazardId = original.SafetyHazardId,
                        CreatedBy = createdBy,
                        CreatedDate = DateTime.UtcNow
                    };

                case DataElement_Tool original:
                    return new DataElement_Tool
                    {
                        ToolId = original.ToolId,
                        CreatedBy = createdBy,
                        CreatedDate = DateTime.UtcNow
                    };

                case DataElement_ILAsCourses original:
                    return new DataElement_ILAsCourses
                    {
                        ILAId = original.ILAId,
                        CreatedBy = createdBy,
                        CreatedDate = DateTime.UtcNow
                    };

                case DataElement_MetaILAsCourses original:
                    return new DataElement_MetaILAsCourses
                    {
                        MetaILAId = original.MetaILAId,
                        CreatedBy = createdBy,
                        CreatedDate = DateTime.UtcNow
                    };

                case DataElement_Test original:
                    return new DataElement_Test
                    {
                        TestId = original.TestId,
                        CreatedBy = createdBy,
                        CreatedDate = DateTime.UtcNow
                    };

                case DataElement_Pretest original:
                    return new DataElement_Pretest
                    {
                        PreTestId = original.PreTestId,
                        CreatedBy = createdBy,
                        CreatedDate = DateTime.UtcNow
                    };

                case DataElement_ComputerBasedTraining original:
                    return new DataElement_ComputerBasedTraining
                    {
                        CBT_ScormUploadId = original.CBT_ScormUploadId,
                        CreatedBy = createdBy,
                        CreatedDate = DateTime.UtcNow
                    };

                case DataElement_TrainingProgram original:
                    return new DataElement_TrainingProgram
                    {
                        TrainingProgramId = original.TrainingProgramId,
                        CreatedBy = createdBy,
                        CreatedDate = DateTime.UtcNow
                    };

                case DataElement_Task original:
                    return new DataElement_Task
                    {
                        TaskId = original.TaskId,
                        CreatedBy = createdBy,
                        CreatedDate = DateTime.UtcNow
                    };

                case DataElement_TestItem original:
                    return new DataElement_TestItem
                    {
                        TestItemId = original.TestItemId,
                        CreatedBy = createdBy,
                        CreatedDate = DateTime.UtcNow
                    };

                case DataElement_EnablingObjective original:
                    return new DataElement_EnablingObjective
                    {
                        EnablingObjectiveId = original.EnablingObjectiveId,
                        CreatedBy = createdBy,
                        CreatedDate = DateTime.UtcNow
                    };

                case DataElement_MetaTask original:
                    return new DataElement_MetaTask
                    {
                        MetaTaskId = original.MetaTaskId,
                        CreatedBy = createdBy,
                        CreatedDate = DateTime.UtcNow
                    };

                case DataElement_MetaEnablingObjective original:
                    return new DataElement_MetaEnablingObjective
                    {
                        MetaEnablingObjectiveId = original.MetaEnablingObjectiveId,
                        CreatedBy = createdBy,
                        CreatedDate = DateTime.UtcNow
                    };

                default:
                    throw new NotSupportedException($"Unsupported DataElement type: {this.GetType()}");
            }
        }

    }
}
