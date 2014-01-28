using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace TimeFliesBy.WebUI
{

  public class DrawUtils
  {

    /// <summary>
    /// Resizing image
    /// </summary>
    /// <param name="imgPhoto">Image object</param>
    /// <param name="Width">New width</param>
    /// <param name="Height">New Height</param>
    /// <returns></returns>
    public static Image FixedSize(System.Drawing.Image imgPhoto, int Width, int Height)
    {
      int sourceWidth = imgPhoto.Width;
      int sourceHeight = imgPhoto.Height;
      int sourceX = 0;
      int sourceY = 0;
      int destX = 0;
      int destY = 0;

      float nPercent = 0;
      float nPercentW = 0;
      float nPercentH = 0;

      nPercentW = ((float)Width / (float)sourceWidth);
      nPercentH = ((float)Height / (float)sourceHeight);
      if (nPercentH < nPercentW)
      {
        nPercent = nPercentH;
        destX = System.Convert.ToInt16((Width -
          (sourceWidth * nPercent)) / 2);
      }
      else
      {
        nPercent = nPercentW;
        destY = System.Convert.ToInt16((Height -
          (sourceHeight * nPercent)) / 2);
      }

      int destWidth = (int)(sourceWidth * nPercent);
      int destHeight = (int)(sourceHeight * nPercent);

      Bitmap bmPhoto = new Bitmap(Width, Height,
        PixelFormat.Format24bppRgb);

      bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
        imgPhoto.VerticalResolution);

      using (Graphics grPhoto = Graphics.FromImage(bmPhoto))
      {
        grPhoto.Clear(Color.White);
        grPhoto.InterpolationMode =
          InterpolationMode.HighQualityBicubic;

        grPhoto.DrawImage(imgPhoto,
          new Rectangle(destX, destY, destWidth, destHeight),
          new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
          GraphicsUnit.Pixel);
      }

      return bmPhoto;
    }

    public static Image Crop(System.Drawing.Image imgPhoto, int Width, int Height, string Anchor)
    {
      int sourceWidth = imgPhoto.Width;
      int sourceHeight = imgPhoto.Height;
      int sourceX = 0;
      int sourceY = 0;
      int destX = 0;
      int destY = 0;

      float nPercent = 0;
      float nPercentW = 0;
      float nPercentH = 0;

      nPercentW = ((float)Width / (float)sourceWidth);
      nPercentH = ((float)Height / (float)sourceHeight);

      if (nPercentH < nPercentW)
      {
        nPercent = nPercentW;
        switch (Anchor)
        {
          case "top":
            destY = 0;
            break;
          case "bottom":
            destY = (int)
              (Height - (sourceHeight * nPercent));
            break;
          default:
            destY = (int)
              ((Height - (sourceHeight * nPercent)) / 2);
            break;
        }
      }
      else
      {
        nPercent = nPercentH;
        switch (Anchor)
        {
          case "Left":
            destX = 0;
            break;
          case "Right":
            destX = (int)
              (Width - (sourceWidth * nPercent));
            break;
          default:
            destX = (int)
              ((Width - (sourceWidth * nPercent)) / 2);
            break;
        }
      }

      int destWidth = (int)(sourceWidth * nPercent);
      int destHeight = (int)(sourceHeight * nPercent);

      Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);

      bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
        imgPhoto.VerticalResolution);

      using (Graphics grPhoto = Graphics.FromImage(bmPhoto))
      {
        grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
        grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
        grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;

        grPhoto.DrawImage(imgPhoto,
          new Rectangle(destX, destY, destWidth, destHeight),
          new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
          GraphicsUnit.Pixel);
      }

      return bmPhoto;
    }

  }
}
