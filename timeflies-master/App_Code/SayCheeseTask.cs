using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeFlies.Common.Scheduling;
using System.Net;

namespace TimeFliesBy.WebUI
{
  public class SayCheeseTask : IntervalScheduleTask
  {
    public SayCheeseTask()
      : base(GlobalObjects.LogManager)
    {
      Interval = new TimeSpan(0, 20, 0);
    }


    protected override void DoFire()
    {
      using (WebClient wc = new WebClient())
      {
        wc.DownloadData(Settings.Url + "/Schedulers/SendSayCheese.aspx"); //workaround against damn FB client which does not work in a thread without http context.
      }
    }

    public override void LoadConfiguration()
    {
      
    }
    
    public override string TaskName
    {
      get { return "Say Cheese Task"; }
    }
  }
}