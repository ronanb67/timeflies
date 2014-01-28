using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
using System.Web.SessionState;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Data.SqlClient;
using TimeFliesBy.Business.Dao;
using System.Linq;
using TimeFlies.Data;

namespace TimeFliesBy.WebUI
{
  public class Global : System.Web.HttpApplication
  {
    void Application_Start(object sender, EventArgs e)
    {
      //GlobalObjects.Scheduler.AddTask(new SayCheeseTask());
      //GlobalObjects.Scheduler.Start();
    }

    void Application_End(object sender, EventArgs e)
    {
    }

    void Application_Error(object sender, EventArgs e)
    {
      return;
      // Code that runs when an unhandled error occurs
      Exception ex = Server.GetLastError();
      string loginUser = "( No User Login ) <br><br>";
      if (SessionHelper.UserId != null)
      {
        AppUser user = ContextHelper.DataContext.User.First(o => o.UserId == SessionHelper.UserId);
        loginUser = "User Info : <br><br> UserId :" + SessionHelper.UserId + "<br><br>FullName :" + HttpUtility.HtmlEncode(user.FullName) + "<br><br>Email :" + HttpUtility.HtmlEncode(user.Email) + "<br><br>";
      }
      EmailService.ErrorEmail(loginUser + HttpContext.Current.Request.Url.AbsoluteUri + " <br><br>" + HttpUtility.HtmlEncode(ex.Message) + " <br> " + HttpUtility.HtmlEncode(ex.InnerException));
      Server.ClearError();
    }

    void Session_Start(object sender, EventArgs e)
    {
    }

    void Session_End(object sender, EventArgs e)
    {
    }

    protected void Application_EndRequest(Object sender, EventArgs e)
    {
      ContextHelper.CloseDataContext();
    }
  }
}

