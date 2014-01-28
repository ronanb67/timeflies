using System;
using TimeFlies.Common.Logging;

namespace TimeFlies.Common.Scheduling
{
  public abstract class DailyScheduleTask : ScheduleTask
  {
    private DateTime plannedTime = DateTime.MinValue;

    protected DailyScheduleTask(ILogManager logManager, DateTime plannedTime) 
      : base(logManager)
    {
      PlannedTime = plannedTime;
    }

    public override bool IsReady()
    {
      if (plannedTime == DateTime.MinValue)
        throw new InvalidOperationException();

      DateTime now = DateTime.Now;

      if ((LastFiredDateTime == null) || (LastFiredDateTime.Value.Day != now.Day))
      {
        TimeSpan nowTime = new TimeSpan(now.Hour, now.Minute, 0);
        TimeSpan startTime = new TimeSpan(plannedTime.Hour, plannedTime.Minute, 0);

        if ((nowTime - startTime).Ticks >= 0)
          return true;
      }

      return false;
    }

    public DateTime PlannedTime
    {
      get { return plannedTime; }
      protected set { plannedTime = value; }
    }
  }
}