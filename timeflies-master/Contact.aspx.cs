using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Configuration;
using TimeFliesBy.WebUI;
using TimeFliesBy.Business.Dao;
using TimeFlies.Common;
using Facebook;

public partial class Contact : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (SessionHelper.UserId != null)
    {
      trFbLogin.Visible = false;
      trFbLogin1.Visible = false;
      btnSend.Enabled = true;
    }
    else
    {
      btnSend.Enabled = false;
      trFbLogin.Visible = true;
      trFbLogin1.Visible = true;
      imgfbConnect.Attributes.Add("onclick", "window.open('" + GetFbLoginUrl() + "',null,'height=450, width=750,status= no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no ');return false;");
    }

  }
  protected void btnSend_Click(object sender, EventArgs e)
  {
    if (txtFeature.Text != "")
    {
      string email = ContextHelper.DataContext.User.FirstOrDefault(o => o.UserId == SessionHelper.UserId).Email;
      EmailService.SendEmail(Settings.ContactEmailAddress, "Contact us message", "Got a message from User (" + email + ") <br><br>" + txtFeature.Text);
      txtFeature.Text = "";
      CommonFunctions.ShowSuccess(lblmsg, "Your message has been sent, Thanks for contacting us.");
    }
    else
    {
      CommonFunctions.ShowWarning(lblmsg, "Please type some message to send.");
    }
  }

  private string GetFbLoginUrl()
  {
    string callBackUrl = Settings.Url + "/CallbackFB.aspx";
    callBackUrl += "?ReturnUrl=" + StringUtils.ToHexString(HttpUtility.UrlEncode("/Contact.aspx"));//facebook sdk does not like special characters

    FacebookOAuthClient oauth = new FacebookOAuthClient(GlobalObjects.FBApp);
    oauth.RedirectUri = new Uri(callBackUrl);

    Dictionary<string, object> dict = new Dictionary<string, object>();
    dict.Add("scope", "email");
    return oauth.GetLoginUrl(dict).ToString();
  }

}
