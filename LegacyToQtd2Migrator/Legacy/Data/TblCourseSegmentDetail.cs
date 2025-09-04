using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblCourseSegmentDetail
    {
        public int Csid { get; set; }
        public int Corid { get; set; }
        public bool? CriticalDecisionMaking { get; set; }
        public bool? FieldVisit { get; set; }
        public bool? Ojt { get; set; }
        public bool? PerformanceDemo { get; set; }
        public bool? ProblemSolving { get; set; }
        public bool? Simulation { get; set; }
        public bool? SituationalAwareness { get; set; }
        public bool? TestQuizElectronic { get; set; }
        public bool? TestQuizWritten { get; set; }
        public bool DeliveryOjt { get; set; }
        public bool DeliveryCbt { get; set; }
        public bool DeliveryClassroom { get; set; }
        public bool? DeliveryGroupExercise { get; set; }
        public bool? DeliveryOther { get; set; }
        public string DeliveryOtherText { get; set; }
        public string InstructorId { get; set; }
    }
}
