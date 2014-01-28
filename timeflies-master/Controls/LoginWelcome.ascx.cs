using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeFliesBy.WebUI;
using TimeFliesBy.Business.Dao;
using System.Linq;
using TimeFlies.Data;

public partial class UserControls_LoginWelcome : System.Web.UI.UserControl
{
  protected void Page_Load(object sender, EventArgs e)
  {
    Panel2.Visible = false;

    if (!String.IsNullOrEmpty(SessionHelper.UserId))
    {
      AppUser user = ContextHelper.DataContext.User.FirstOrDefault(o => o.UserId == SessionHelper.UserId);

      if (user != null)
      {
        Panel2.Visible = true;
        lnkLoginName.Text = HttpUtility.HtmlEncode(user.FullName);
      }
    }
  }

  protected void btnLogout_Click(object sender, EventArgs e)
  {
    SessionHelper.ClearSession();
    Panel2.Visible = false;
    Response.Redirect("~/");
  }
}
