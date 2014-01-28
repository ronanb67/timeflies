using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using TimeFliesBy.WebUI;
using TimeFliesBy.Business.Dao;
using TimeFlies.Data;
using Facebook.Web;

public partial class FriendsVideo : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    try
    {
      if (Request.QueryString["id"] != null)
      {
        string id = Request.QueryString["id"];
        
        Videos vdo;
        if (id.Length > 8) // here got UserID
          vdo = ContextHelper.DataContext.Videos.First(o => o.UserId == id);
        else // here got videoID
          vdo = ContextHelper.DataContext.Videos.First(o => o.VideoId == id);
        hdnUid.Value = vdo.UserId;
        hdnVid.Value = vdo.VideoId;
        hdnIp.Value = Settings.IP;
        
        string name = ContextHelper.DataContext.User.First(o => o.UserId == vdo.UserId).FullName;
        lblUserName.Text = name;
        FbLike.InnerHtml = "<iframe src=\"http://www.facebook.com/plugins/like.php?href=http%3A//www.timeflies.by/FriendsVideo.aspx%3Fid%3D" + vdo.VideoId + "&amp;layout=standard&amp;show_faces=false&amp;width=450&amp;action=like&amp;colorscheme=light&amp;height=30\" scrolling=\"no\" frameborder=\"0\" style=\"border:none; overflow:hidden; width:450px; height:30px;\" allowTransparency=\"true\"></iframe>";
        Page.Header.Title = "Time flies as " + name + " takes a photo every day";
        if (vdo.Publish == "Private")
        {
          if (SessionHelper.UserId != null && vdo.UserId == SessionHelper.UserId)
          {
            BindPlayer(vdo, name);
          }
          else
          {
            tblVideo.Visible = false;
            tblPrivate.Visible = true;
            CommonFunctions.ShowWarning(lblmsg, "Oops, This video is private.");
            return;
          }
        }
        else if (vdo.Publish == "PublicFriends")
        {
          if (SessionHelper.UserId != null && (vdo.UserId == SessionHelper.UserId || IsFbFriend(hdnUid.Value, SessionHelper.UserId)))
          {
            BindPlayer(vdo, name);
          }
          else
          {
            tblVideo.Visible = false;
            tblPrivate.Visible = true;
            CommonFunctions.ShowWarning(lblmsg, "Oops, This video is private.");
            return;
          }
        }
        else if (vdo.Publish == "Public")
        {
          BindPlayer(vdo, name);
        }
      }
      else
      {
        Response.Redirect(Settings.Url);
      }
    }
    catch (Exception ex)
    {
      EmailService.ErrorEmail(ex.ToString());
      //Response.Redirect(Settings.Url);
      throw;
    }
  }

  private void BindPlayer(Videos vdo, string name)
  {
    tblVideo.Visible = true;
    tblPrivate.Visible = false;
    BindFBComment(vdo.VideoId);
    IEnumerable<Images> images = ContextHelper.DataContext.Images.Where(o => o.VideoId == vdo.VideoId);
    if (images.Count() > 0)
    {
      Images img = images.OrderByDescending(o => o.DateAdded).First();
      string imgUrl = ImageHelper.GetImageUrl(img);
      (this.Master as TimeFliesMasterPage).ChangeMetaFBImage(imgUrl);
    }
    (this.Master as TimeFliesMasterPage).ChangeMetaFBurl("http://" + Settings.IP + "/FriendsVideo.aspx?id=" + vdo.VideoId);
    (this.Master as TimeFliesMasterPage).ChangeMetaFBTitle("" + name + "'s TimeFlies.by Video");
  }

  public void BindFBComment(string Vid)
  {
    string str = "<div id=\"fb-root\"></div><script src=\"http://connect.facebook.net/en_US/all.js#appId=" + Settings.FBAppId + "&amp;xfbml=1\"></script><fb:comments xid=\"" + Vid + "\" href=\"http://www.timeflies.by/FriendsVideo.aspx?Id=" + Vid + "\" num_posts=\"4\" width=\"500\"></fb:comments>";
    Literal myScript = new Literal();
    myScript.Text = str;
    phFBComment.Controls.Add(myScript);
  }

  public bool IsFbFriend(string videoUserId, string friendUserId)
  {
    AppUser usr = ContextHelper.DataContext.User.First(o => o.UserId == videoUserId);

    FacebookWebContext wc = new FacebookWebContext(GlobalObjects.FBApp);
    FacebookWebClient fb = new FacebookWebClient(wc);
    fb.AccessToken = usr.AccessToken;
    dynamic friends = fb.Get("/me/friends");

    List<string> userIds = new List<string>();
    foreach (dynamic friend in friends.data)
      userIds.Add(friend.id);

    return userIds.Contains(friendUserId);
  }
}
