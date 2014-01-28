using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Data;
using TimeFliesBy.Business.Dao;
using System.Linq;
using TimeFliesBy.WebUI;
using TimeFlies.Data;

public partial class _Default : System.Web.UI.Page
{
  string DisableTabs = "false";

  protected void Page_Load(object sender, EventArgs e)
  {
    try
    {
      if (Request.TotalBytes > 0)
      {
        string uid = Request.QueryString["uid"];
        string vid = Request.QueryString["vid"];
        string isUpdate = Request.QueryString["isUpdate"];
        string imgId = Request.QueryString["imgId"];
        byte[] fileData = Request.BinaryRead(Request.TotalBytes);
        string filePath = Guid.NewGuid().ToString().Substring(0, 8);

        string fileName;
        if (isUpdate == "1")
        {
          int id = Int32.Parse(imgId);
          Images img = ContextHelper.DataContext.Images.First(o => o.ImageId == id);
          fileName = img.ImageName + "E";
        }
        else
        {
          fileName = GetNextImageNumber(uid);
        }

        ImageHelper.SaveImage(uid, fileName + ".jpg", fileData);

        if (isUpdate == "1")
        {
          int id = Int32.Parse(imgId);
          Images img = ContextHelper.DataContext.Images.First(o => o.ImageId == id);

          //File.Delete(Server.MapPath("~/" + img.ThumbPath));

          //string setEditPath = filePath.Replace("~/", "");
          //string setThumbPath = thumbPath.Replace("~/", "");

          //img.ThumbPath = fileName;
          img.ImagePath = fileName + ".jpg";
          img.IsEdit = true;

          Videos video = ContextHelper.DataContext.Videos.First(o => o.VideoId == vid);
          video.IsCompile = false;
          video.IsError = false;

          ContextHelper.DataContext.SaveChanges();
          string url = ImageHelper.GetImageUrl(img);
          //workaround for flash component which incorrectly build url
          url = url.Replace("http://www.timeflies.by/", "");
          url = url.Replace("http://localhost/", "");

          Response.Write(url);
          return;
        }
        else
        {
          Images img = new Images();
          img.ImagePath = fileName + ".jpg";
          //img.ThumbPath = thumbPath.Replace("~/", "");
          img.ImageName = fileName;
          img.VideoId = vid;
          img.UserId = uid;
          img.DateAdded = DateTime.Now;
          img.IsRead = false;
          img.IsInvalid = false;
          img.IsEdit = false;
          img.IsDelete = false;
          ContextHelper.DataContext.AddToImages(img);

          Videos video = ContextHelper.DataContext.Videos.First(o => o.VideoId == vid);
          video.IsCompile = false;
          video.IsError = false;

          ContextHelper.DataContext.SaveChanges();

          int imageCount = ContextHelper.DataContext.Images.Count(o => o.VideoId == video.VideoId);
          if (imageCount == 1)
          {
            DisableTabs = "true";
          }
          Response.Write(ImageHelper.GetImageUrl(img) + "_" + DisableTabs);
        }
      }
    }
    catch (Exception ex)
    {
      EmailService.ErrorEmail(ex.ToString());
      Response.Write(ex.Message + "----------------" + ex.InnerException);
    }
  }



  public string GetNextImageNumber(string userId)
  {
    string imageName = "00001";

    Images image = ContextHelper.DataContext.Images.Where(o => o.UserId == userId).OrderByDescending(o => o.DateAdded).FirstOrDefault();

    if (image != null)
      imageName = (Convert.ToInt64(image.ImageName) + 1).ToString().PadLeft(5, '0');

    return imageName;
  }
}
