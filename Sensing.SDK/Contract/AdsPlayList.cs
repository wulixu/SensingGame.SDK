namespace Sensing.SDK.Contract
{
    public class AdsPlayList
    {
        public int? ScheduleStartTime { get; set; }
        public int? ScheduleEndTime { get; set; }
        public bool IdleAble { get; set; }
        public long[] Children { get; set; }
        public int[] ChildrenTimeSpanNumList { get; set; }
        public int Type { get; set; }
    }
}
