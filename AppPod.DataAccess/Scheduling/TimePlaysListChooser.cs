using Sensing.SDK.Contract;
using System;
using System.Linq;

namespace AppPod.DataAccess.Scheduling
{
    public class TimePlaysListChooser : PlayListChooserBase
    {
        public TimePlaysListChooser( AdAndAppTimelineScheduleViewModel adSchedulingContent, DateTime timelineStartedTimeOnPC) : base(adSchedulingContent, timelineStartedTimeOnPC)
        {
            adSchedulingContent.StartTime =  adSchedulingContent.StartTime ?? new TimeSpan();
        }

        public override AdOrAppPlayItem FindNowPlayItem()
        {
            double timeSinceStart = DateTime.Now.TimeOfDay.Subtract(_currentSchedule.StartTime.Value).TotalSeconds;

            _currentPlayerList = _currentSchedule.AdAndApps.FirstOrDefault(a => a.ScheduleStartTime <= timeSinceStart && timeSinceStart < a.ScheduleEndTime);
            if (_currentPlayerList == null) return null;

            var abSpanTotal = timeSinceStart - _currentPlayerList.ScheduleStartTime;
            var sumTime = _currentPlayerList.Children.Sum(c => c.Duration);
            if (sumTime == 0)
                return null;
            //可能在循环播放了,需要时间求余
            var abSpan = abSpanTotal % sumTime;
            var listSum = 0;
            for (int i = 0; i < _currentPlayerList.Children.Count; i++)
            {
                var item = _currentPlayerList.Children[i];
                var itemSpan = item.Duration;
                listSum += itemSpan;
                if (listSum > abSpan)
                {
                    var playItem = new AdOrAppPlayItem();
                    playItem.Type = _currentPlayerList.Type;
                    playItem.AdOrAppId = item.Id;
                    playItem.PlayDuration = itemSpan;
                    _currentPlayItem = playItem;
                    return playItem;
                }
                continue;
            }
            return null;
        }
    }
}
