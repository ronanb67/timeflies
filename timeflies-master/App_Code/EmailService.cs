using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using Jobs.Common;
using System.Linq;
using TimeFliesBy.Business.Dao;
using TimeFlies.Data;
using Facebook.Web;
using TimeFliesBy.Business.Entity;
using System.Text;

namespace TimeFliesBy.WebUI
{
  public class EmailService
  {
    public static void ErrorEmail(Exception ex)
    {
      ErrorEmail(ex.ToString());
    }
    public static void ErrorEmail(Exception ex, string message)
    {
      ErrorEmail(ex.ToString() + " " + message);
    }
    public static void ErrorEmail(string error)
    {
      SendEmail(Settings.ContactEmailAddress, "Error Occurred At TimeFlies.by", error);
      //SendEmail("umerzia@relax-solutions.com", "Error Occurred At TimeFlies.by", error);
    }

    public static void YourImages(AppUser user, string url)
    {
      string subject = "Your images at Timeflies.by";
      string html = String.Format(@"Hi {0},
<div>&nbsp;</div>
Here is a link for your images at Timeflies.by
<div>&nbsp;</div>
{1}
<div>&nbsp;</div>
Thanks <br>
Timeflies.by", user.FullName, url);
      
      SendEmail(user.Email, subject, html);
    }

    public static void ConfirmDeleteAccount(AppUser user)
    {
      string subject = "Confirm your account deletion at Timeflies.by";

      string html = String.Format(@"Hi {0},
<div>&nbsp;</div>
Please confirm your account deletion at Timeflies.by
<div>&nbsp;</div>
<a href=""{1}&data=true"">Save Data And Delete Account</a>
<div>&nbsp;</div>
<a href=""{1}&data=false"">Delete my account</a>  
<div>&nbsp;</div>
Thanks<br>
Timeflies.by", user.FullName, Settings.Url + "/DeleteAccount.aspx?id=" + user.UserId);

      SendEmail(user.Email, subject, html);
    }

    public static void NewUserRegister(AppUser user, string videoId)
    {
      string body = "New User Register At TimeFlies <br>Username :" + user.FullName + " <br>Email:" + user.Email + "<br><br><a href=" + Settings.Url + "/" + videoId + ">Click here to view user's profile.</a><br>";

      FacebookWebContext wc = new FacebookWebContext(GlobalObjects.FBApp);
      FacebookWebClient fb = new FacebookWebClient(wc);
      fb.AccessToken = user.AccessToken;
      dynamic friends = fb.Get("/me/friends");

      List<string> userIds = new List<string>();
      foreach (dynamic friend in friends.data)
        userIds.Add(friend.id);

      IEnumerable<string> existingUserIds = ContextHelper.DataContext.Images.Where(o => userIds.Contains(o.UserId)).Select(o => o.UserId).Distinct().Take(3);

      if (existingUserIds.Count() > 0)
      {
        body += "Thanks<br>New user friend's at TimeFlies.by are:<br>";
        foreach (string userId in existingUserIds)
        {
          AppUser usrData = ContextHelper.DataContext.User.FirstOrDefault(o => o.UserId == userId);
          body += usrData.FullName + " (" + usrData.Email + ") " + "<br>";
        }
      }

      SendEmail(Settings.ContactEmailAddress, "New User Register At TimeFlies.by", body);
      //SendEmail("umerzia@relax-solutions.com", "New User Register At TimeFlies.by", body);
    }

    public static void SayCheese(string userId, string to, string name, int photoCount, DateTime? lastPhotoDT, List<Images> friendsImages)
    {
      string subject = "Timeflies.by - Say cheese...";

      string lastPhoto = "";
      if (lastPhotoDT.HasValue)
        lastPhoto = ", your last photo was taken " + GetTimeSpan(lastPhotoDT.Value) + ".";

      string html = @"Hi " + HttpUtility.HtmlEncode(name) + @",
<br>
<h2><a href=""" + Settings.Url + @""">Click here to take today's photo.</a></h2>
<br>
So far you have " + photoCount + @" photo" + (photoCount != 1 ? "s" : "") + " taken" + lastPhoto + @"
<br>
" + GetFriendImages(friendsImages) + @"
<br>
If you would rather not receive these reminders, just <a href=""" + Settings.Url + @"/Setting.aspx?id=" + userId + @""">click here to change your settings</a>";

      SendEmail(to, subject, html);
    }

    private static string GetFriendImages(List<Images> friendsImages)
    {
      if (friendsImages == null || friendsImages.Count == 0)
        return "";

      StringBuilder sb = new StringBuilder();
      sb.Append(@"Here are some of your friend's recent photos: (Click to see their video and give them some encouragement).<br><br>");
      sb.Append(@"<table cellspacing=""10"" cellpadding=""0"">	<tr align=""center"">");


      foreach (Images image in friendsImages)
      {
        if (String.IsNullOrEmpty(image.ImagePath))
          continue;
        string imgUrl = ImageHelper.GetImageUrl(image);
        string videoUrl = Settings.Url + "/FriendsVideo.aspx?id=" + image.VideoId;

        sb.AppendFormat(@"<td align=""center""><a target=""_blank""  href=""{0}""><img width=""130"" height=""100"" src=""{1}"" alt=""Friends Photo"" border=""0"" /> </a> <br/> {2}</td>",
          videoUrl, imgUrl, GetTimeSpan(image.DateAdded));
      }
      sb.Append("</tr></table>");
      return sb.ToString();
    }

    public static string GetTimeSpan(DateTime dt)
    {
      TimeSpan ts = DateTime.Now.Subtract(dt);
      if (ts.Days > 365)
      {
        int n = ts.Days / 365;
        return n.ToString() + " year" + (n > 1 ? "s" : "") + " ago";
      }
      else if (ts.Days > 30)
      {
        int n = ts.Days / 30;
        return n.ToString() + " month" + (n > 1 ? "s" : "") + " ago";
      }
      else if (ts.Days == 1)
      {
        return "yesterday";
      }
      else if (ts.Days == 0)
      {
        return "today";
      }
      else
        return ts.Days.ToString() + " days ago";
    }


    public static void SendEmail(string to, string subject, string body)
    {
      MailAddress from = new MailAddress(Settings.MailFromAddress);
      MailAddress rec = new MailAddress(to);

      using (MailMessage mm = new MailMessage(from, rec))
      {
        mm.IsBodyHtml = true;
        //mm.Priority = MailPriority.High;
        mm.Subject = subject;
        mm.Body = "<html><body>" + body + "</body></html>";
        
        using (SmtpClient smtp = new SmtpClient())
        {
          smtp.Send(mm);
        }
      }
    }
  }
}