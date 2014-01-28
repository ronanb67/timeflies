using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeFlies.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace TimeFliesBy.WebUI
{
  public class ImageHelper
  {
    public static int ZipUserImages(string userId, bool sendEmail, out string zipFileUrl)
    {
      IEnumerable<Images> imgs = ContextHelper.DataContext.Images.Where(o => o.UserId == userId && !o.IsDelete);
      
      int count = imgs.Count();
      if (count > 0)
      {
        string[] imgCollection = new string[count];
        int i = 0;
        foreach (Images img in imgs)
        {
          imgCollection[i] = GetImageFullPath(userId, img.ImagePath);
          i++;
        }
        string zipFile = Path.Combine(Settings.ZipLocalPath, userId + ".zip");
        ZipOperation.CreateZipFile(imgCollection, zipFile);
        zipFileUrl = Settings.ZipUrl + "/" + userId + ".zip";
        if (sendEmail)
        {
          AppUser usr = ContextHelper.DataContext.User.FirstOrDefault(o => o.UserId == userId);
          EmailService.YourImages(usr, zipFileUrl);
        }
      }
      else
      {
        zipFileUrl = "";
      }
      return count;
    }

    public static void SaveImage(string userId, string fileName, byte[] data)
    {
      string fn = GetImageFullPath(userId, fileName);
      string dir = Path.GetDirectoryName(fn);
      if (!Directory.Exists(dir))
        Directory.CreateDirectory(dir);

      File.WriteAllBytes(fn, data);

     string tfn = GetThumbImageFullPath(userId, fileName);
     dir = Path.GetDirectoryName(tfn);
      if (!Directory.Exists(dir))
        Directory.CreateDirectory(dir);
      
      ResizeAndSaveImage(data, tfn);
    }
    
    private static void ResizeAndSaveImage(byte[] data, string thumbName)
    {
      using (MemoryStream ms = new MemoryStream(data))
      {
        using (Bitmap imageOrigin = new Bitmap(ms))
        {
          using (Bitmap imageSmall = new Bitmap(DrawUtils.Crop(imageOrigin, 170, 139, "")))
          {
            imageSmall.Save(thumbName, ImageFormat.Jpeg);

            imageSmall.Dispose();
          }
        }
      }
    }


    public static string GetImageFullPath(string userId, string img)
    {
      return Path.Combine(Settings.UserImagesLocalPath, userId, img);
    }
    public static string GetThumbImageFullPath(string userId, string img)
    {
      return Path.Combine(Settings.UserImagesLocalPath, userId, "thumbs", img);
    }
    public static string GetImageUrl(Images image)
    {
      return Settings.UserImagesUrl + "/" + image.UserId + "/" + image.ImagePath;
    }
    public static string GetImageUrl(string userId, string imagePath)
    {
      return Settings.UserImagesUrl + "/" + userId + "/" + imagePath;
    }

    public static string GetThumbImageUrl(Images image)
    {
      return Settings.UserImagesUrl + "/" + image.UserId + "/thumbs/" + image.ImagePath;
    }

    public static void DeleteImage(Images image)
    {
      string fn = GetImageFullPath(image.UserId, image.ImagePath);
      if (File.Exists(fn))
        File.Delete(fn);
      
      fn = GetThumbImageFullPath(image.UserId, image.ImagePath);
      if (File.Exists(fn))
        File.Delete(fn);
    }

  }
}