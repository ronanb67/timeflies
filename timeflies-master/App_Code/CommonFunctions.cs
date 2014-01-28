using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

namespace TimeFliesBy.WebUI
{
  public enum MyEnum
  {
    aaaa = 1,
  }
  public class CommonFunctions
  {
    public static void ShowSuccess(Label lblStaus, string message)
    {
      string FormatMessage = "";
      FormatMessage += "<div class='success'>";
      lblStaus.Visible = true;
      FormatMessage += message + "</div>";
      lblStaus.Text = FormatMessage;
      lblStaus.EnableViewState = false;
    }
    public static void ShowError(Label lblStaus, string message)
    {
      string FormatMessage = "";
      FormatMessage += "<div class='error'>";
      lblStaus.Visible = true;
      FormatMessage += message + "</div>";
      lblStaus.Text = FormatMessage;
      lblStaus.EnableViewState = false;
    }
    public static void ShowWarning(Label lblStaus, string message)
    {
      string FormatMessage = "";
      FormatMessage += "<div class='warning'>";
      lblStaus.Visible = true;
      FormatMessage += message + "</div>";
      lblStaus.Text = FormatMessage;
      lblStaus.EnableViewState = false;
    }

    public static void ShowInfo(Label lblStaus, string message)
    {
      string FormatMessage = "";
      FormatMessage += "<div class='info'>";
      lblStaus.Visible = true;
      FormatMessage += message + "</div>";
      lblStaus.Text = FormatMessage;
      lblStaus.EnableViewState = false;
    }
  }
}