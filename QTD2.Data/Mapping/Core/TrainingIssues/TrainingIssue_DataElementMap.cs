using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class TrainingIssue_DataElementMap : Common.CommonMap<TrainingIssue_DataElement>
    {
        public override void Configure(EntityTypeBuilder<TrainingIssue_DataElement> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.TrainingIssueId).IsRequired();
            builder.HasOne(o => o.TrainingIssue).WithOne(o=> o.DataElement).HasForeignKey<TrainingIssue_DataElement>(k => k.TrainingIssueId).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(e => e.TrainingIssueId).IsUnique().HasFilter("[Deleted] = 0");
            builder.Ignore("DataElementDisplay");

            builder.HasDiscriminator<string>("DataElementType")
              .HasValue<TrainingIssue_DataElement>("TrainingIssue_DataElement")
              .HasValue<DataElement_EnablingObjective>("DataElement_EnablingObjective")
              .HasValue<DataElement_MetaEnablingObjective>("DataElement_MetaEnablingObjective")
              .HasValue<DataElement_MetaTask>("DataElement_MetaTask")
              .HasValue<DataElement_Procedure>("DataElement_Procedure")
              .HasValue<DataElement_RegulatoryRequirement>("DataElement_RegulatoryRequirement")
              .HasValue<DataElement_SafetyHazard>("DataElement_SafetyHazard")
              .HasValue<DataElement_Task>("DataElement_Task")
              .HasValue<DataElement_Tool>("DataElement_Tool")
              .HasValue<DataElement_TrainingProgram>("DataElement_TrainingProgram")
              .HasValue<DataElement_ILAsCourses>("DataElement_ILAsCourses")
              .HasValue<DataElement_MetaILAsCourses>("DataElement_MetaILAsCourses")
              .HasValue<DataElement_ComputerBasedTraining>("DataElement_ComputerBasedTraining")
              .HasValue<DataElement_TestItem>("DataElement_TestItem")
              .HasValue<DataElement_Pretest>("DataElement_Pretest")
              .HasValue<DataElement_Test>("DataElement_Test");
        }
    }
}
