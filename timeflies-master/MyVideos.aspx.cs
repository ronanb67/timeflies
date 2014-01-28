using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net.Mail;
using System.Reflection;
using TimeFliesBy.WebUI;
using TimeFliesBy.Business.Dao;
using System.Linq;
using TimeFlies.Data;

public partial class MyVideos : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (SessionHelper.UserId != null && SessionHelper.UserType != null && SessionHelper.UserType == "admin")
    {
      Response.Redirect(Settings.Url + "/UserListing.aspx");
    }
    if (SessionHelper.UserId == null && Request.QueryString["uid"] != null)
    {
      SessionHelper.UserId = Request.QueryString["uid"];
    }

    if (SessionHelper.UserId != null && ContextHelper.DataContext.User.Any(o=>o.UserId == SessionHelper.UserId))
    {
      TimeFliesByEntities dc = ContextHelper.DataContext;
      hdnUid.Value = SessionHelper.UserId;
      Videos vdo = dc.Videos.First(o => o.UserId == SessionHelper.UserId);
      hdnVid.Value = vdo.VideoId;
      hdnIp.Value = Settings.IP;
      
      FbLike.InnerHtml = "<iframe src=\"http://www.facebook.com/plugins/like.php?href=http%3A%2F%2Fwww.timeflies.by%2F" + vdo.VideoId + "&amp;layout=standard&amp;show_faces=false&amp;width=450&amp;action=like&amp;colorscheme=light&amp;height=30\" scrolling=\"no\" frameborder=\"0\" style=\"border:none; overflow:hidden; width:450px; height:30px;\" allowTransparency=\"true\"></iframe>";
      BindFBComment(vdo.VideoId);

      string name = dc.User.First(o => o.UserId == SessionHelper.UserId).FullName;
      Page.Header.Title = "Time flies as " + name + " takes a photo every day";
      IEnumerable<Images> images = dc.Images.Where(o => o.VideoId == vdo.VideoId);
      if (images.Count() > 0)
      {
        Images img = images.OrderByDescending(o => o.DateAdded).First();
        (this.Master as TimeFliesMasterPage).ChangeMetaFBImage("http://" + Settings.IP + "/" + ImageHelper.GetImageUrl(img));
      }
      (this.Master as TimeFliesMasterPage).ChangeMetaFBurl("http://" + Settings.IP + "/" + SessionHelper.UserId);
      (this.Master as TimeFliesMasterPage).ChangeMetaFBTitle(name + "'s TimeFlies.by Video");
      link.InnerHtml = "Share";
    }
    else
    {
      Response.Redirect(Settings.Url);
    }
  }


  public void BindFBComment(string Vid)
  {
    string str = "<div id=\"fb-root\"></div><script src=\"http://connect.facebook.net/en_US/all.js#appId=" + Settings.FBAppId + "&amp;xfbml=1\"></script><fb:comments xid=\"" + Vid + "\" href=\"http://www.timeflies.by/" + Vid + "&" + "\" num_posts=\"4\"  width=\"500\"></fb:comments>";
    Literal myScript = new Literal();
    myScript.Text = str;
    FBCommentPlaceHolder.Controls.Add(myScript);
  }


  protected void CreatePlayer(string VideoPath, string VideoImagePath)
  {
    Literal literalPlayer = new Literal();
    string player = string.Empty;
    player = "<p class=\"Playlist\" id=\"preview\">";
    player += "<embed height=\"415\" width=\"70%\" flashvars=\"autostart=false&";
    player += "file=" + VideoPath + "&logo.hide=true&";
    player += "image=" + VideoImagePath;
    player += "&bgcolor=#000000&controlbar.position=over&skin=Common/JWplayer/glow.zip\" wmode=\"transparent\"";
    player += "allowscriptaccess=\"always\" allowfullscreen=\"true\"";
    player += "quality=\"high\" name=\"player\" id=\"player\" style=\"\"";
    player += "src=\"Common/JWplayer/player.swf\"";
    player += "type=\"application/x-shockwave-flash\">";
    player += "</p>";

    literalPlayer.Text = player;
  }
}
