using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace TimeFliesBy.WebUI
{
  public class Settings
  {
    private static string contentPath;
    private static string contentUrl;
    
    static Settings()
    {
      contentPath = ConfigurationManager.AppSettings["ContentPath"];
      if (!String.IsNullOrEmpty(contentPath) && !contentPath.EndsWith("\\"))
        contentPath += "\\";
      
      contentUrl = ConfigurationManager.AppSettings["ContentUrl"];
      if (!String.IsNullOrEmpty(contentUrl) && !contentPath.EndsWith("/"))
        contentUrl += "/";
    }

    public static string EFConnectionString
    {
      get
      {
        return ConfigurationManager.ConnectionStrings["EFConnectionString"].ConnectionString;
      }
    }

    public static string FBAppId
    {
      get
      {
        return ConfigurationManager.AppSettings["FBAppId"];
      }
    }
    public static string FBPageId
    {
      get
      {
        return ConfigurationManager.AppSettings["FBPageId"];
      }
    }

    

    public static string MailFromAddress
    {
      get
      {
        return ConfigurationManager.AppSettings["MailFromAddress"];
      }
    }

    public static string ContactEmailAddress
    {
      get
      {
        return ConfigurationManager.AppSettings["ContactEmailAddress"];
      }
    }
    public static string Url
    {
      get
      {
        return ConfigurationManager.AppSettings["Url"];
      }
    }
    public static string IP
    {
      get
      {
        return ConfigurationManager.AppSettings["IP"];
      }
    }
    public static string FBSecret
    {
      get
      {
        return ConfigurationManager.AppSettings["FBSecret"];
      }
    }

    public static string UserImagesLocalPath
    {
      get
      {
        return contentPath + "UserImages";
      }
    }
    public static string ZipLocalPath
    {
      get
      {
        return contentPath + "Zip";
      }
    }

    public static string UserImagesUrl
    {
      get
      {
        return contentUrl + "UserImages";
      }
    }
    public static string ZipUrl
    {
      get
      {
        return contentUrl + "Zip";
      }
    }
    
    
  }
}
