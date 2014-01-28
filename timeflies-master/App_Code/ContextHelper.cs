using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeFlies.Data;

namespace TimeFliesBy.WebUI
{
  public class ContextHelper
  {
    public static TimeFliesByEntities DataContext
    {
      get
      {
        TimeFliesByEntities dc = HttpContext.Current.Items["dc"] as TimeFliesByEntities;
        if (dc == null)
        {
          dc = new TimeFliesByEntities(Settings.EFConnectionString);
          HttpContext.Current.Items["dc"] = dc;
        }
        return dc;
      }
    }

    public static void CloseDataContext()
    {
      TimeFliesByEntities dc = HttpContext.Current.Items["dc"] as TimeFliesByEntities;
      if (dc != null)
      {
        dc.SaveChanges();
        dc.Dispose();
        HttpContext.Current.Items["dc"] = null;
      }
    }
  }
}