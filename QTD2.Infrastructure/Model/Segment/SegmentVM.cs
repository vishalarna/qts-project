public class SegmentVM
{
    public int Duration { get; set; }
    public bool IsNercStandard { get; set; }
    public bool IsNercOperatingTopics { get; set; }
    public bool IsNercSimulation { get; set; }
    public bool IsPartialCredit { get; set; }
    public string Content { get; set; }
    public int SegmentObjectiveCounts { get; set; }

    public SegmentVM()
    {
    }

    public SegmentVM(int duration, bool isNercStandard, bool isNercOperatingTopics,
                     bool isNercSimulation, string content,
                     int segmentObjectiveCounts, bool isPartialCredit)
    {
        Duration = duration;
        IsNercStandard = isNercStandard;
        IsNercOperatingTopics = isNercOperatingTopics;
        IsNercSimulation = isNercSimulation;
        Content = content;
        SegmentObjectiveCounts = segmentObjectiveCounts;
        IsPartialCredit = isPartialCredit;
    }
}
