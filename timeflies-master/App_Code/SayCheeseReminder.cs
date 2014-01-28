using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeFlies.Data;
using Facebook.Web;
using System.Data;
using TimeFliesBy.Business.Dao;
using System.Text;
using TimeFliesBy.Business.Entity;

namespace TimeFliesBy.WebUI
{
  public class SayCheeseReminder
  {
    public static void Send()
    {
      using (TimeFliesByEntities dc = new TimeFliesByEntities(Settings.EFConnectionString))//do not use ContextHelper.DataContext as it runs in a background thread
      {
        DateTime fromDate = DateTime.Now.AddDays(-1);

        IEnumerable<UserReminderInfo> reminders = AppUserDao.GetUsersReminderInfo(dc);

        reminders = reminders.Where(o => o.IsActive
        && o.IsReminder && o.LastReminderDT.GetValueOrDefault(DateTime.MinValue) < fromDate
        && o.MaxImageDT.GetValueOrDefault(DateTime.MinValue) < fromDate);

        foreach (UserReminderInfo reminder in reminders)
        {
          List<Images> friendImages = null;
          try
          {
            friendImages = GetFriendImages(reminder.AccessToken, dc);
          }
          catch (Exception ex)
          {
            if (!(ex is Facebook.FacebookOAuthException))
              EmailService.ErrorEmail(ex);
          }

          EmailService.SayCheese(reminder.UserId, reminder.Email, reminder.FullName, reminder.ImageCount, reminder.MaxImageDT, friendImages);

          IEnumerable<Videos> videos = dc.Videos.Where(o => o.UserId == reminder.UserId).ToList();
          foreach (Videos video in videos)
            video.ServerReminderTime = DateTime.Now;
          dc.SaveChanges();
        }
      }
    }

    private static List<Images> GetFriendImages(string userToken, TimeFliesByEntities dc)
    {
      FacebookWebContext wc = new FacebookWebContext(GlobalObjects.FBApp);
      FacebookWebClient fb = new FacebookWebClient(wc);
      fb.AccessToken = userToken;
      dynamic friends = fb.Get("/me/friends");

      List<Images> images = new List<Images>();
      foreach (dynamic friend in friends.data)
      {
        string friendId = friend.id;
        Images image = dc.Images.Where(o => o.UserId == friendId).OrderByDescending(o => o.ImageId).FirstOrDefault();
        if (image != null)
          images.Add(image);
      }
      return images.OrderByDescending(o => o.DateAdded).Take(5).ToList();
    }


    /*
    public bool ProcessEmail(string accessToken)
    {
      bool isEmailed = false;
      try
      {
        isEmailed = true;
        int outcount;
        int count = 1;
        string FbFriendIDs = "";
        string FrndsImages = "";
        bool IsFriendImage = false;
        FacebookWebApp.GraphApiEntities.Friends frnd = FacebookWebApp.Services.FacebookSvc.GetFriends(accessToken, "me");
        if (frnd.data.Count > 0)
        {
          foreach (FacebookWebApp.GraphApiEntities.Friend f in frnd.data)
          {
            if (FbFriendIDs == "")
            {
              FbFriendIDs = "'" + f.id + "'";
            }
            else
            {
              FbFriendIDs += ",'" + f.id + "'";
            }
          }
          // DataSet ds = CambaTv.Data.DataRepository.Provider.ExecuteDataSet(CommandType.Text, "select distinct Userid from images where userid in ("+FbFriendIDs+")");
          DataSet ds = CambaTv.Data.DataRepository.Provider.ExecuteDataSet(CommandType.Text, "select Userid from [user] where userid in  (" + FbFriendIDs + ") order by newid()");

          if (ds.Tables[0].Rows.Count > 0)
          {
            FrndsImages = "Here are some of your friend's recent photos: (Click to see their video and give them some encouragement).<br><br>";
            FrndsImages += "<table cellspacing=\"10\" cellpadding=\"0\"><tr align=\"center\">";
            if (ds.Tables[0].Rows.Count <= 3)
            {
              foreach (DataRow drow in ds.Tables[0].Rows)
              {
                if (CambaTv.Data.DataRepository.ImagesProvider.GetByUserId(drow[0].ToString()).Count > 0)
                {
                  //   FrndsImages += "<img width=\"200\" height=\"150\" src=\"" + System.Configuration.ConfigurationManager.AppSettings["Url"] + "/" + CambaTv.Data.DataRepository.ImagesProvider.GetPaged("Userid='" + drow[0].ToString() + "'", "DateAdded DESC", 0, 1, out outcount)[0].ImagePath + "\" alt=\"Friends Photo\" /> ";
                  CambaTv.Entities.Images img = CambaTv.Data.DataRepository.ImagesProvider.GetPaged("Userid='" + drow[0].ToString() + "'", "DateAdded DESC", 0, 1, out outcount)[0];
                  if (CambaTv.Data.DataRepository.VideosProvider.GetByVideoId(img.VideoId).Publish != "Private")
                  {
                    FrndsImages += " <td align=\"center\"><a target=\"_blank\"  href=\"" + System.Configuration.ConfigurationManager.AppSettings["Url"] + "/" + img.VideoId + "\"><img width=\"130\" height=\"100\" src=\"" + System.Configuration.ConfigurationManager.AppSettings["Url"] + "/" + img.ImagePath + "\" alt=\"Friends Photo\" border=\"0\" /> </a> <br/>" + GetTimeSpan(Convert.ToDateTime(img.DateAdded)) + "</td>";
                    IsFriendImage = true;
                  }
                }
              }
              FrndsImages += "  </tr></table>";
            }
            else
            {

              foreach (DataRow drow in ds.Tables[0].Rows)
              {
                if (count <= 4)
                {
                  if (CambaTv.Data.DataRepository.ImagesProvider.GetByUserId(drow[0].ToString()).Count > 0)
                  {
                    //    FrndsImages += "<img width=\"200\" height=\"150\" src=\"" + System.Configuration.ConfigurationManager.AppSettings["Url"] + "/" + CambaTv.Data.DataRepository.ImagesProvider.GetPaged("Userid='" + drow[0].ToString() + "'", "DateAdded DESC", 0, 1, out outcount)[0].ImagePath + "\" alt=\"Friends Photo\" /> ";
                    CambaTv.Entities.Images img = CambaTv.Data.DataRepository.ImagesProvider.GetPaged("Userid='" + drow[0].ToString() + "'", "DateAdded DESC", 0, 1, out outcount)[0];
                    if (CambaTv.Data.DataRepository.VideosProvider.GetByVideoId(img.VideoId).Publish != "Private")
                    {
                      FrndsImages += " <td align=\"center\"><a target=\"_blank\"  href=\"" + System.Configuration.ConfigurationManager.AppSettings["Url"] + "/" + img.VideoId + "\"><img width=\"130\" height=\"100\" src=\"" + System.Configuration.ConfigurationManager.AppSettings["Url"] + "/" + img.ImagePath + "\" alt=\"Friends Photo\" border=\"0\" /> </a> <br/>" + GetTimeSpan(Convert.ToDateTime(img.DateAdded)) + "</td>";
                      IsFriendImage = true;
                      count++;
                    }
                  }
                }
              }
              FrndsImages += "  </tr></table>";

            }
          }
        }
        if (!IsFriendImage)
        {
          FrndsImages = "";
        }
        string Firstname = "";
        if (dr["FullName"].ToString().Contains(" "))
        {
          Firstname = dr["FullName"].ToString().Split(' ')[0].ToString();
        }
        else
        {
          Firstname = dr["FullName"].ToString();
        }
        new DataProvider().SendReminder(dr["Email"].ToString(), dr["VideoID"].ToString(), dr["UserID"].ToString(), Firstname, FrndsImages);

      }
      catch (Exception ex)
      {
        EventLog.WriteEntry("CambaReminderService ProcessEmail", dr["UserID"].ToString() + " ------------ " + ex.Message + " <br> " + ex.InnerException);
      }
      return isEmailed;
    }*/

  }
}