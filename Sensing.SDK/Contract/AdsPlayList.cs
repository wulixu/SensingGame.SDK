namespace Sensing.SDK.Contract
{
    public class ProgramItem
    {
        public int? ScheduleStartTime { get; set; }
        public int? ScheduleEndTime { get; set; }
        public bool IdleAble { get; set; }
        public AdOrAppItem[] Children { get; set; }
        public int Type { get; set; }
        public string PackageName { get; set; }
    }

    public class AdOrAppItem
    {
        public long Id { get; set; }
        public int Duration { get; set; }
        public string Transition { get; set; }
    }
}
