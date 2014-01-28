using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using TimeFliesBy.WebUI;
using TimeFlies.Common;
using Facebook;

public partial class _DefaultPage : System.Web.UI.Page
{
  protected string FBAppId
  {
    get
    {
      return Settings.FBAppId;
    }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    if (SessionHelper.UserId != null && SessionHelper.UserType != null && SessionHelper.UserType == "admin")
    {
      Response.Redirect(Settings.Url + "/UserListing.aspx"); //workaround for godaddy virtual dir
    }
    if (SessionHelper.UserId != null)
    {
      Response.Redirect(Settings.Url + "/MyVideos.aspx?Camera=Camera");
    }
    imgfbConnect.Attributes.Add("onclick", "window.open('" + GetFbLoginUrl() + "',null,'height=450, width=750,status= no, resizable= yes, scrollbars=no, toolbar=no,location=no,menubar=no ');return false;");
  }

  private string GetFbLoginUrl()
  {
    string callBackUrl = Settings.Url + "/CallbackFB.aspx";
    string returnUrl = Request.QueryString["ReturnUrl"];
    if (!String.IsNullOrEmpty(returnUrl))
      callBackUrl += "?ReturnUrl=" + StringUtils.ToHexString(returnUrl);//facebook sdk does not like special characters

    FacebookOAuthClient oauth = new FacebookOAuthClient(GlobalObjects.FBApp);
    oauth.RedirectUri = new Uri(callBackUrl);

    Dictionary<string, object> dict = new Dictionary<string, object>();
    dict.Add("scope", "email");
    return oauth.GetLoginUrl(dict).ToString();
  }
}
