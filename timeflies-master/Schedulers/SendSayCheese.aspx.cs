using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeFliesBy.WebUI;

public partial class Schedulers_SendSayCheese : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    SayCheeseReminder.Send();
  }
}