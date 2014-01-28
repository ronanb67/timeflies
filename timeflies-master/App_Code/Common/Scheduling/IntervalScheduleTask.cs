using System;
using TimeFlies.Common.Logging;

namespace TimeFlies.Common.Scheduling
{
  public abstract class IntervalScheduleTask : ScheduleTask
  {
    private readonly TimeSpan minInterval = new TimeSpan(0, 0, 10);
    private TimeSpan interval = TimeSpan.MaxValue;

    protected IntervalScheduleTask(ILogManager logManager)
      : base(logManager)
    {
    }

    public override bool IsReady()
    {
      if ((LastFiredDateTime == null) ||
          (LastFiredDateTime.Value.Add(Interval) < DateTime.Now))
      {
        return true;
      }

      return false;
    }

    public TimeSpan Interval
    {
      get { return interval; }
      protected set 
      {
        if (interval < minInterval)
          throw new ArgumentOutOfRangeException("Interval");
        interval = value; 
      }
    }
  }
}