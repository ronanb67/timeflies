using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeFliesBy.WebUI;
using TimeFliesBy.Business.Dao;
using TimeFlies.Data;

public partial class TimeFliesMasterPage : System.Web.UI.MasterPage
{
  protected string FBAppId
  {
    get
    {
      return Settings.FBAppId;
    }
  }
  protected string FBPageId
  {
    get
    {
      return Settings.FBPageId;
    }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    pnlLoginWelcome.Visible = false;
    tblMenu.Visible = false;

    if (!String.IsNullOrEmpty(SessionHelper.UserId))
    {
      AppUser user = ContextHelper.DataContext.User.FirstOrDefault(o => o.UserId == SessionHelper.UserId);
      if (user != null)
      {
        pnlLoginWelcome.Visible = true;
        hlLoginName.Text = HttpUtility.HtmlEncode(user.FullName);

        if (ContextHelper.DataContext.Videos.Count(o => o.UserId == user.UserId) > 0)
        {
          hdnEnableDisableTabs.Value = "showTabs";
        }
        tblMenu.Visible = true;
      }
    }
    fbAppId.Content = Settings.FBAppId;
  }

  public void ChangeMetaFBImage(string description)
  {
    FBImage.Attributes["content"] = description;
  }
  public void ChangeMetaFBurl(string url)
  {
    FBurl.Attributes["content"] = url;
  }
  public void ChangeMetaFBTitle(string Title)
  {
    FBTitle.Attributes["content"] = Title;
  }

  
  protected void btnLogout_Click(object sender, EventArgs e)
  {
    SessionHelper.ClearSession();

    Response.Redirect(Settings.Url);
  }
}
