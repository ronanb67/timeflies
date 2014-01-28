using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeFliesBy.WebUI;
using TimeFliesBy.Business.Dao;
using TimeFlies.Data;

public partial class SettingPage : System.Web.UI.Page
{
  protected string UserId
  {
    get
    {
      return SessionHelper.UserId;
    }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.IsPostBack)
    {
      if (SessionHelper.UserId != null && SessionHelper.UserType != null && SessionHelper.UserType == "admin")
      {
        Response.Redirect(Settings.Url + "/UserListing.aspx");
      }
      if (SessionHelper.UserId == null)
      {
        Response.Redirect(Settings.Url);
      }
      else
      {
        Videos vdo = ContextHelper.DataContext.Videos.First(o => o.UserId == SessionHelper.UserId);
        if (vdo.IsReminder)
        {
          imgOnOf.ImageUrl = "images/on.png";
          imgOnOf.CommandName = "off";
          imgOnOf.CommandArgument = vdo.VideoId;
        }
        else
        {
          imgOnOf.ImageUrl = "images/off.png";
          imgOnOf.CommandName = "on";
          imgOnOf.CommandArgument = vdo.VideoId;
        }
        if (vdo.Publish == "Private")
        {
          rdoPublish.SelectedValue = "Private";
        }
        else if (vdo.Publish == "PublicFriends")
        {
          rdoPublish.SelectedValue = "PublicFriends";
        }
        else if (vdo.Publish == "Public")
        {
          rdoPublish.SelectedValue = "Public";
        }
      }
    }
  }
  protected void imgOnOf_Click(object sender, ImageClickEventArgs e)
  {
    if (SessionHelper.UserId != null)
    {
      if (imgOnOf.CommandName == "on")
      {
        Videos vdo = ContextHelper.DataContext.Videos.First(o => o.VideoId == imgOnOf.CommandArgument);
        vdo.IsReminder = true;
        ContextHelper.DataContext.SaveChanges();

        imgOnOf.ImageUrl = "images/on.png";
        imgOnOf.CommandName = "off";
        imgOnOf.CommandArgument = vdo.VideoId;
      }
      else if (imgOnOf.CommandName == "off")
      {
        Videos vdo = ContextHelper.DataContext.Videos.First(o => o.VideoId == imgOnOf.CommandArgument);
        vdo.IsReminder = false;
        ContextHelper.DataContext.SaveChanges();
        imgOnOf.ImageUrl = "images/off.png";
        imgOnOf.CommandName = "on";
        imgOnOf.CommandArgument = vdo.VideoId;
      }
    }
    else
    {
      Response.Redirect(Settings.Url);
    }
  }

  protected void rdoPublish_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (SessionHelper.UserId == null)
    {
      Response.Redirect(Settings.Url);
    }
    else
    {
      Videos vdo = ContextHelper.DataContext.Videos.First(o => o.UserId == SessionHelper.UserId);
      vdo.Publish = rdoPublish.SelectedItem.Value;
      ContextHelper.DataContext.SaveChanges();
      CommonFunctions.ShowSuccess(lblmsg, "Setting saved successfully.");
    }
  }
}
