using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;
using System.Data;
using System.Collections.Specialized;
using System.Text;
using TimeFliesBy.WebUI;
using TimeFliesBy.Business.Dao;
using System.Data.SqlClient;
using Jobs.Common.DB;
using TimeFlies.Data;
using Facebook.Web;

public partial class Friends : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.IsPostBack)
    {
      if (SessionHelper.UserId != null)
      {
        BindData();
      }
      else
      {
        Response.Redirect(Settings.Url);
      }
    }
  }

  public void BindData()
  {
    AppUser usr = ContextHelper.DataContext.User.First(o => o.UserId == SessionHelper.UserId);

    FacebookWebContext wc = new FacebookWebContext(GlobalObjects.FBApp);
    FacebookWebClient fb = new FacebookWebClient(wc);
    fb.AccessToken = usr.AccessToken;
    dynamic friends = fb.Get("/me/friends");

    List<string> userIds = new List<string>();
    foreach (dynamic friend in friends.data)
      userIds.Add(friend.id);

    if (userIds.Count() > 0)
    {
      DataTable dt = AppUserDao.GetUsersInfo(userIds);
      if (dt.Rows.Count > 0)
      {
        dlFriends.DataSource = dt;
        dlFriends.DataBind();
      }
      else
      {
        CommonFunctions.ShowInfo(lblStatus, "No friends found at timeflies");
      }
    }
    else
    {
      CommonFunctions.ShowInfo(lblStatus, "No friends found at timeflies");
    }
  }

  protected void dlFriends_ItemDataBound(object sender, DataListItemEventArgs e)
  {
    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    {
      HyperLink hl = e.Item.FindControl("hl") as HyperLink;
      Label lblName = e.Item.FindControl("lblName") as Label;
      Image img = e.Item.FindControl("img") as Image;

      DataRowView drv = e.Item.DataItem as DataRowView;

      hl.NavigateUrl = Settings.Url + "/FriendsVideo.aspx?id=" + drv["videoPath"];
      img.ImageUrl = !String.IsNullOrEmpty(drv["imagePath"].ToString()) ? ImageHelper.GetImageUrl(drv["userId"].ToString(), drv["imagePath"].ToString()) : "Images/no_pic.jpg";
      lblName.ToolTip = drv["FullName"].ToString();
      lblName.Text = drv["FullName"].ToString().Length > 19 ? drv["FullName"].ToString().Substring(0, 19) + "..." : drv["FullName"].ToString();
    }
  }
}


