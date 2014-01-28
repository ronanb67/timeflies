using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeFliesBy.WebUI;

public partial class Comment : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (Request.QueryString["Vid"] != null)
    {
      string str = "<div id=\"fb-root\"></div><script src=\"http://connect.facebook.net/en_US/all.js#appId=" + Settings.FBAppId + "&amp;xfbml=1\"></script><fb:comments href=\"http://www.timeflies.by/" + Request.QueryString["Vid"] + "&" + "\" num_posts=\"3\" width=\"500\"></fb:comments>";
      Literal myScript = new Literal();
      myScript.Text = str;
      phFBComment.Controls.Add(myScript);
    }
  }
}
