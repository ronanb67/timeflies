using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using TimeFliesBy.Business.Dao;
using TimeFliesBy.WebUI;
using TimeFlies.Data;

public partial class Download : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    string userId = Request.QueryString["id"];
    if (String.IsNullOrEmpty(userId))
      return;

    string zipFileUrl;
    int count = ImageHelper.ZipUserImages(userId, false, out zipFileUrl);

    if (count > 0)
    {
      Response.Redirect(zipFileUrl);
      //todo delete file later
    }
    else
    {
      CommonFunctions.ShowInfo(lblMsg, "There is no image.");
    }
  }
}
