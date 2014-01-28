using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Data;
using System.IO;
using TimeFlies.Data;
using Facebook.Web;

namespace TimeFliesBy.WebUI
{
  // NOTE: If you change the class name "Service" here, you must also update the reference to "Service" in Web.config.
  public class Service : IService
  {
    public string DoWork(string val)
    {
      return val;
    }

    public string GetUserImages(string userId)
    {
      try
      {
        //todo whats a bullshit xml generation
        string xml = "<?xml version='1.0' encoding='UTF-8'?>"
                               + "<List>";

        XmlDocument doc = new XmlDocument();
        List<Images> images;
        using (TimeFliesByEntities dc = new TimeFliesByEntities(Settings.EFConnectionString))
        {
          images = dc.Images.Where(o => o.UserId == userId).OrderBy(o => o.DateAdded).ToList();
        }
        if (images.Count > 0)
        {
          foreach (Images content in images)
          {
            string url = ImageHelper.GetImageUrl(content);
            //workaround for flash component which incorrectly build url
            url = url.Replace("http://www.timeflies.by/", "");
            url = url.Replace("http://localhost/", "");
            xml += "<content id =\"" + content.ImageId + "\" name =\"" + content.ImageName + "\" contentpath =\"" + url + "\" DateAdded =\"" + Convert.ToDateTime(content.DateAdded).ToString("MM/dd/yyyy H:mm") + "\" IsDeleted =\"" + content.IsDelete.ToString() + "\" IsEdit =\"" + content.IsEdit.ToString() + "\" />";
          }

          xml += "</List>";

          doc.LoadXml(xml);
          return doc.OuterXml;
        }
        else
        {
          return "No Record Found";
        }
      }
      catch (Exception ex)
      {
        EmailService.ErrorEmail(ex);
        return ex.ToString();
      }
    }

    public string DelUnDel(string imageId, string action)
    {
      try
      {

        using (TimeFliesByEntities dc = new TimeFliesByEntities(Settings.EFConnectionString))
        {
          int id = Int32.Parse(imageId);
          Images image = dc.Images.FirstOrDefault(o => o.ImageId == id);

          if (image == null)
            return "false";

          if (action == "Del")
          {
            image.IsDelete = true;
          }
          else
          {
            image.IsDelete = false;
          }
          dc.SaveChanges();
        }
        return "true";
      }
      catch (Exception ex)
      {
        EmailService.ErrorEmail(ex);
        return "error";
      }
    }

    public string ChangeDate(string imageId, string newDate)
    {
      try
      {
        using (TimeFliesByEntities dc = new TimeFliesByEntities(Settings.EFConnectionString))
        {
          int id = Int32.Parse(imageId);
          Images image = dc.Images.FirstOrDefault(o => o.ImageId == id);
          if (image == null)
            return "false";

          string[] datee = newDate.Split('-');
          image.DateAdded = Convert.ToDateTime(datee[0] + "/" + datee[1] + "/" + datee[2] + " " + datee[3] + ":" + datee[4] + ":" + datee[5] + " " + datee[6]);
          dc.SaveChanges();
          return "true";
        }
      }
      catch (Exception ex)
      {
        EmailService.ErrorEmail(ex);
        return "error";
      }
    }

    public string SaveEyes(string userId, string rEye, string lEye)
    {
      try
      {
        using (TimeFliesByEntities dc = new TimeFliesByEntities(Settings.EFConnectionString))
        {
          AppUser user = dc.User.FirstOrDefault(o => o.UserId == userId);
          if (user == null)
            return "User does not exist";
          user.RightEye = rEye;
          user.LeftEye = lEye;
          dc.SaveChanges();
          return "Record saved";
        }
      }
      catch (Exception ex)
      {
        EmailService.ErrorEmail(ex);
        return "DB error occur";
      }
    }

    public string GetEyes(string userId)
    {
      try
      {
        using (TimeFliesByEntities dc = new TimeFliesByEntities(Settings.EFConnectionString))
        {
          AppUser user = dc.User.FirstOrDefault(o => o.UserId == userId);
          if (user == null)
            return "User does not exist";

          if (!String.IsNullOrEmpty(user.LeftEye) && !String.IsNullOrEmpty(user.RightEye))
          {
            return user.RightEye + "-" + user.LeftEye;
          }
          else
          {
            return "No record found";
          }
        }
      }
      catch (Exception ex)
      {
        EmailService.ErrorEmail(ex);
        return "DB error occur";
      }
    }

    public string RevertImage(string imageId)
    {
      try
      {
        using (TimeFliesByEntities dc = new TimeFliesByEntities(Settings.EFConnectionString))
        {
          int id = Int32.Parse(imageId);
          Images image = dc.Images.FirstOrDefault(o => o.ImageId == id);
          if (image == null)
            return "false";

          image.IsEdit = false;
          ImageHelper.GetImageUrl(image);
          
          //image.ImagePath = "Contents/UserImages/" + image.UserId + "/" + image.ImageName + ".jpg";
          dc.SaveChanges();
          return "true_" + image.ImagePath;
        }
        /*if (CambaTv.Data.DataRepository.ImagesProvider.Update(img))
        {
          return "true_" + img.ImagePath;
        }
        else
        {
          return "false";
        }*/
      }
      catch (Exception ex)
      {
        EmailService.ErrorEmail(ex);
        return "DB error occur";
      }
    }

    public string AuthenticateUser(string userId, string token)
    {
      try
      {
        using (TimeFliesByEntities dc = new TimeFliesByEntities(Settings.EFConnectionString))
        {
          string videoId = "id_";
          if (dc.User.Count(o => o.UserId == userId) > 0)
          {
            videoId += dc.Videos.First(o => o.UserId == userId).VideoId;
          }
          else // Add New User
          {
            FacebookWebContext wc = new FacebookWebContext(GlobalObjects.FBApp);
            FacebookWebClient fb = new FacebookWebClient(wc);
            fb.AccessToken = token;
            dynamic result = fb.Get("/me");
            string fbEmail = result.email ?? String.Empty;
            userId = result.id;
            videoId += RegisterUser(result.id, result.email, result.first_name + " " + result.last_name, token);

            /*
            const string username = "me";
            Person userData = FacebookSvc.GetUserData(token, username);
            string FQLquery = "https://api.facebook.com/method/fql.query?query=select%20email%20from%20user%20where%20uid%3D" + userData.id + "&access_token=" + token + "";
            string resp = new oAuthFacebook().WebRequest(oAuthFacebook.Method.GET, FQLquery, string.Empty);
            DataSet ds = new DataSet();
            StringReader xmlReader = new StringReader(resp);
            ds.ReadXml(xmlReader);
            string email = ds.Tables[1].Rows[0][0].ToString();
            videoId += RegisterUser(userData.id, email, userData.first_name + " " + userData.last_name, token);
             */
          }
          return videoId;
        }
      }
      catch (Exception ex)
      {
        EmailService.ErrorEmail(ex);
        return ex.Message;
      }
    }

    public string RegisterUser(string uid, string email, string fullname, string accessToken)
    {
      string videoId = "";
      try
      {
        using (TimeFliesByEntities dc = new TimeFliesByEntities(Settings.EFConnectionString))
        {
          AppUser user = new AppUser();
          user.UserId = uid;
          user.Email = email;
          user.FullName = fullname;
          user.AccessToken = accessToken;
          user.LastLogin = DateTime.Now;
          user.IsActive = true;
          user.DateAdded = DateTime.Now;
          dc.AddToUser(user);

          Videos video = new Videos();

          video.VideoId = Guid.NewGuid().ToString().Substring(0, 8);
          video.UserId = user.UserId;
          video.VideoName = "My Video";
          video.Publish = "PublicFriends";
          video.IsReminder = true;
          video.IsSentReminder = false;
          video.IsCompile = false;
          video.IsImage = false;
          video.IsError = false;
          video.IsSoundTrack = false;
          video.ServerReminderTime = DateTime.Now;
          video.DateAdded = DateTime.Now;
          dc.AddToVideos(video);

          videoId = video.VideoId;
          EmailService.NewUserRegister(user, videoId);
        }
      }
      catch (Exception ex)
      {
        EmailService.ErrorEmail(ex);
        videoId = ex.Message;
      }
      return videoId;
    }
  }
}