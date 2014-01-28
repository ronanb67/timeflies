using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeFlies.Common.Logging;
using TimeFlies.Common.Scheduling;
using Facebook;

namespace TimeFliesBy.WebUI
{
  public class GlobalObjects
  {
    private static ILogManager logManager;
    private static Scheduler scheduler;
    private static DefaultFacebookApplication fbLoginApp;

    static GlobalObjects()
    {
      logManager = new LogManager();
      scheduler = new Scheduler(logManager);
      scheduler.AddTask(new SayCheeseTask());
      scheduler.Start();
      
      fbLoginApp = new DefaultFacebookApplication();
      fbLoginApp.AppId = Settings.FBAppId;
      fbLoginApp.AppSecret = Settings.FBSecret;
    }

    public static ILogManager LogManager
    {
      get
      {
        return logManager;
      }
    }

    public static Scheduler Scheduler
    {
      get
      {
        return scheduler;
      }
    }

    public static IFacebookApplication FBApp
    {
      get { return fbLoginApp; }
    }


  }
}