using System;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;

namespace TimeFliesBy.WebUI
{
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  public class KeepSessionAlive : IHttpHandler, IRequiresSessionState
  {
    public void ProcessRequest(HttpContext context)
    {
      if (SessionHelper.UserId != null || SessionHelper.NewUID != null)
      {
        SessionHelper.KeepSessionAlive = DateTime.Now;
      }
    }

    public bool IsReusable
    {
      get
      {
        return false;
      }
    }
  }
}
