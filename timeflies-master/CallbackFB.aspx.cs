using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using TimeFliesBy.WebUI;
using TimeFliesBy.Business.Dao;
using Jobs.Common.DB;
using System.Collections.Generic;
using TimeFlies.Data;
using Facebook;
using Facebook.Web;
using TimeFlies.Common;

public partial class CallbackFB : System.Web.UI.Page
{
  protected bool IsFacebookAuthenticated
  {
    get
    {
      return !String.IsNullOrEmpty(Request.Params["code"]);
    }
  }

  protected bool IsFacebookError
  {
    get
    {
      return !String.IsNullOrEmpty(Request.Params["error_description"]);
    }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    if (IsFacebookAuthenticated)
    {
      FacebookOAuthResult authResult;
      bool isOAuthResult = FacebookOAuthResult.TryParse(HttpContext.Current.Request.Url, out authResult);
      if (!isOAuthResult || !authResult.IsSuccess)
      {
        ShowErr("Getting data from Facebook failed. Please try again after 3-5 minutes.");
        return;
      }

      string returnUrl = Request.QueryString["ReturnUrl"];
      string callBackUrl = Settings.Url + "/CallbackFB.aspx" + (String.IsNullOrEmpty(returnUrl) ? "" : "?ReturnUrl=" + HttpUtility.UrlEncode(returnUrl));

      FacebookOAuthClient oauth = new FacebookOAuthClient(GlobalObjects.FBApp);
      oauth.RedirectUri = new Uri(callBackUrl);

      IDictionary<string, object> result = (IDictionary<string, object>)oauth.ExchangeCodeForAccessToken(authResult.Code, null);
      string accessToken = (string)result["access_token"];
      if (String.IsNullOrEmpty(accessToken))
      {
        ShowErr("Getting data from Facebook failed. Please try again after 3-5 minutes.");
        return;
      }
      AppUser appUser = LoginFacebook(accessToken);
      if (appUser != null)
      {
        pnlFacebookLogged.Visible = true;
        SessionHelper.UserId = appUser.UserId;
      }
    }
    else if (IsFacebookError)
      ShowErr("You have cancelled the login request.");
  }

  private AppUser LoginFacebook(string accessToken)
  {
    FacebookWebContext wc = new FacebookWebContext(GlobalObjects.FBApp);
    FacebookWebClient fb = new FacebookWebClient(wc);
    fb.AccessToken = accessToken;
    dynamic result = fb.Get("/me");
    string fbEmail = result.email ?? String.Empty;
    if (string.IsNullOrEmpty(fbEmail))
    {
      ShowErr("Getting data from Facebook failed. Please try again after 3-5 minutes.");
      return null;
    }
    string userId = result.id;
    AppUser usr = ContextHelper.DataContext.User.FirstOrDefault(o => o.UserId == userId);

    if (usr == null)
      usr = RegisterUser(result.id, result.email, result.first_name + " " + result.last_name, accessToken);
    else
    {
      usr.LastLogin = DateTime.Now;
      usr.AccessToken = accessToken;// result.Token;
      usr.FullName = result.first_name + " " + result.last_name;
      ContextHelper.DataContext.SaveChanges();

      ScriptManager.RegisterStartupScript(this, this.GetType(), "MyScript", "window.opener.FBCalback();window.close ();", true);
    }
    return usr;
  }

  protected string GetUserHomeUrl()
  {
    if (!String.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
      return StringUtils.FromHexString(Request.QueryString["ReturnUrl"]);
    else
      return Settings.Url + "/Default.aspx";
  }

  protected void ShowErr(string err)
  {
    pnlFBLoginError.Visible = true;
    pnlErrMsg.InnerText = err;
  }

  public AppUser RegisterUser(string uid, string email, string fullname, string accessToken)
  {
    TimeFliesByEntities dc = ContextHelper.DataContext;

    AppUser usr = new AppUser();
    usr.UserId = uid;
    usr.Email = email;
    usr.FullName = fullname;
    usr.AccessToken = accessToken;
    usr.LastLogin = DateTime.Now;
    usr.IsActive = true;
    usr.DateAdded = DateTime.Now;
    dc.User.AddObject(usr);

    SessionHelper.UserId = usr.UserId;
    
    Videos vdo = new Videos();
    vdo.VideoId = Guid.NewGuid().ToString().Substring(0, 8);
    vdo.UserId = usr.UserId;
    vdo.VideoName = "My Video";
    vdo.Publish = "PublicFriends";
    vdo.IsReminder = true;
    vdo.IsSentReminder = false;
    vdo.IsCompile = false;
    vdo.IsImage = false;
    vdo.IsError = false;
    vdo.IsSoundTrack = false;
    vdo.ServerReminderTime = DateTime.Now;
    vdo.DateAdded = DateTime.Now;
    dc.Videos.AddObject(vdo);

    dc.SaveChanges();

    EmailService.NewUserRegister(usr, vdo.VideoId);
    return usr;
  }
}