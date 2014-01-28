using System;
using System.Collections.Generic;
using System.Timers;
using TimeFlies.Common.Logging;

namespace TimeFlies.Common.Scheduling
{
  public class Scheduler
  {
    private readonly Timer timer = new Timer();
    private readonly List<ScheduleTask> tasks = new List<ScheduleTask>();
    private const int pollIntervalSeconds = 30;
    // injected props
    private readonly ILogManager logManager;

    private void LoadConfiguration()
    {
      double pollInterval = pollIntervalSeconds * 1000;

      if (timer.Interval != pollInterval)
        timer.Interval = pollInterval;

      foreach (ScheduleTask task in tasks)
      {
        task.LoadConfiguration();
      }
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        LoadConfiguration();
        FireTasks();
      }
      catch (Exception ex)
      {
        logManager.Error(ex, "Error in scheduler.");
      }
    }

    private void FireTasks()
    {
      foreach (ScheduleTask task in tasks)
      {
        if (task.IsReady())
        {
          if (!task.Running)          
            task.Fire();
          //else
          //  logManager.Warning("Task {0} is still running.", task.GetType().Name);
        }
      }      
    }

    public Scheduler(ILogManager logManager)
    {
      this.logManager = logManager;

      timer.Elapsed += Timer_Elapsed;
    }

    public void AddTask(ScheduleTask task)
    {
      tasks.Add(task);
    }

    public void Start()
    {
      LoadConfiguration();
      timer.Start();
    }
  }
}