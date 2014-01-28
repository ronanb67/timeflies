using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.IO;
using TimeFliesBy.WebUI;
using TimeFliesBy.Business.Dao;
using TimeFlies.Data;

public partial class DeleteAccountPage : System.Web.UI.Page
{
  string userId = "";
  protected void Page_Load(object sender, EventArgs e)
  {
    string data = "";
    if (!Page.IsPostBack)
    {
      if (Request.QueryString["id"] != null)
      {
        userId = Request.QueryString["id"].ToString();
      }
      if (Request.QueryString["data"] != null)
      {
        data = Request.QueryString["data"].ToString();
      }

      if (data == "false")
      {
        lbDownload.Visible = false;
        DeleteAccount(userId);
      }
      else if (data == "true")
      {
        lbDownload.Visible = true;
        string strDownloadPageURL = Settings.Url + "/Download.aspx?id=" + userId;
        lbDownload.Attributes.Add("onclick", "window.open('" + strDownloadPageURL + "', 'Download', 'menubar=0, toolbar=0, location=0, status=0, resizable=0, width=100, height=50');");
      }
    }
  }

  public void DeleteAccount(string userId)
  {
    try
    {
      TimeFliesByEntities dc = ContextHelper.DataContext;
      IEnumerable<Images> images = dc.Images.Where(o=>o.UserId == userId);
      foreach (Images image in images)
        dc.Images.DeleteObject(image);
      
      IEnumerable<Videos> videos = dc.Videos.Where(o => o.UserId == userId);
      foreach (Videos video in videos)
        dc.Videos.DeleteObject(video);

      AppUser usr = dc.User.FirstOrDefault(o => o.UserId == userId);

      if (usr != null)
      {
        dc.User.DeleteObject(usr);

        dc.SaveChanges();
        
        string userName = usr.FullName;
        string email = usr.Email;
        CommonFunctions.ShowSuccess(lblMsg, "Account Deleted Successfully.");

        EmailService.SendEmail(Settings.ContactEmailAddress, "Account deleted at TimeFlies.by", "Hi, <br><br>User Name : " + userName + " <br>Email : " + email + "<br><br> deletes his/her account from Timeflies.by");

        SessionHelper.ClearSession();

        if (!ClientScript.IsStartupScriptRegistered("logout"))
        {
          Page.ClientScript.RegisterStartupScript(this.GetType(), "logout", "fbLogout();", true);
        }
      }
      else
      {
        CommonFunctions.ShowWarning(lblMsg, "Your account already been deleted");
      }
    }
    catch (Exception ex)
    {
      EmailService.ErrorEmail(ex.ToString());
      CommonFunctions.ShowError(lblMsg, "Error in delete Account");
    }
  }


  protected void lbDownload_Click(object sender, EventArgs e)
  {
    if (Request.QueryString["id"] != null)
    {
      userId = Request.QueryString["id"].ToString();
    }
    DeleteAccount(userId);
  }
}
