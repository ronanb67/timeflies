using System;
using System.Threading;
using TimeFlies.Common.Logging;

namespace TimeFlies.Common.Scheduling
{
  public abstract class ScheduleTask
  {
    private readonly ILogManager logManager;
    private readonly object syncRoot = new object();
    private volatile Thread workerThread;
    private volatile bool running;

    private void RunThreadProc()
    {
      running = true;
      try
      {
        //LogManager.Verbose("{0} task: Started.", TaskName);
        try
        {
          DoFire();

          //LogManager.Verbose("{0} task: Finished.", TaskName);
        }
        catch (Exception ex)
        {
          LogManager.Error(ex, "{0} task: Error.", TaskName);
        }
        finally
        {
          LastFiredDateTime = DateTime.Now;
          workerThread = null;
        }
      }
      finally
      {
        running = false;
      }
    }

    protected abstract void DoFire();

    protected DateTime? LastFiredDateTime
    {
      get; set;
    }

    protected ILogManager LogManager
    {
      get { return logManager; }
    }

    internal ScheduleTask(ILogManager logManager)
    {
      this.logManager = logManager;
    }

    public void Fire()
    {
      lock (syncRoot)
      {
        if (workerThread == null)
        {
          workerThread = new Thread(RunThreadProc);
          workerThread.Name = String.Format("{0} thread", TaskName);
          workerThread.IsBackground = true;
          workerThread.Priority = ThreadPriority.Lowest;
          workerThread.Start();
        }
      }
    }

    public abstract bool IsReady();

    public abstract void LoadConfiguration();

    public bool Running
    {
      get { return running; }
    }

    public abstract string TaskName
    {
      get;
    }
  }
}