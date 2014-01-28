using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeFliesBy.WebUI
{
  public class SessionHelper
  {
    public static string UserId
    {
      get
      {
        return (string)HttpContext.Current.Session["userid"];
      }
      set
      {
        if (String.IsNullOrEmpty(value))
          HttpContext.Current.Session.Remove("userid");
        else
          HttpContext.Current.Session["userid"] = value;
      }
    }

    public static string UserType
    {
      get
      {
        return (string)HttpContext.Current.Session["UserType"];
      }
      set
      {
        HttpContext.Current.Session["UserType"] = value;
      }
    }
    public static string NewUID
    {
      get
      {
        return (string)HttpContext.Current.Session["NewUID"];
      }
      set
      {
        HttpContext.Current.Session["NewUID"] = value;
      }
    }
    public static DateTime KeepSessionAlive
    {
      get
      {
        return (DateTime)HttpContext.Current.Session["KeepSessionAlive"];
      }
      set
      {
        HttpContext.Current.Session["KeepSessionAlive"] = value;
      }
    }

    public static void ClearSession()
    {
      HttpContext.Current.Session.Clear();
    }
  }
}