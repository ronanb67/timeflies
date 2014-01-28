<%@ WebHandler Language="C#" Class="imageChange" %>

using System;
using System.Web;
using System.Data;
using Jobs.Common.DB;
using System.Data.SqlClient;
using TimeFliesBy.Business.Dao;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeFliesBy.WebUI;
using TimeFlies.Data;

public class imageChange : IHttpHandler
{

	public void ProcessRequest(HttpContext context)
	{
		int imageId = Convert.ToInt16(context.Request.QueryString["imageId"].ToString());
		int newImage = Convert.ToInt16(context.Request.QueryString["newImage"].ToString());
		int pageIndex = Convert.ToInt16(context.Request.QueryString["pIndex"].ToString());
		int userId = Convert.ToInt16(context.Request.QueryString["UserId"].ToString());

		try
		{
			if (newImage == 1)
			{
				Images img = ContextHelper.DataContext.Images.First(o => o.ImageId == imageId);
				context.Response.Write(ImageHelper.GetThumbImageUrl(img) + "|" + imageId.ToString());
			}
			else if (newImage == 2)
			{
				DeleteImage(imageId);
				context.Response.Write("Success|" + imageId.ToString());
			}
			else if (newImage == 3)
			{
				context.Response.Write(GetPictureHtml(userId, pageIndex));
			}
		}
		catch (Exception ex)
		{
			EmailService.ErrorEmail(ex.ToString());
			context.Response.Write("Fail|" + imageId.ToString());
		}

		context.Response.End();
	}

	public string GetPictureHtml(int userNo, int pageIndex)
	{
		int pageSize = 12;

		StringBuilder sb = new StringBuilder(pageSize);

		IEnumerable<Images> images = ContextHelper.DataContext.Images.Where(o => o.UserId == userNo.ToString() && !o.IsDelete).OrderByDescending(o => o.DateAdded).Skip(pageIndex * pageSize).Take(pageSize);

		if (images.Count() > 0)
			foreach (Images img in images)//todo whats a crap, todo see how it works
				sb.AppendFormat("<div id=\"{0}\" class=\"pics\"><img class=\"pictures\" src=\"{1}\"  /><a href=\"#\" style=\" margin-left:22px;\" onclick=\"EditImage({2},{3},{0});\" title=\"edit\"><img class=\"editDelete\" src=\"App_Themes/Default/images/20/edit2.gif\" /></a><a id=\"a\" href=\"#\" style=\" margin-left:120px;\" onclick = \"deleteImage({0});\" title=\"delete\"><img class=\"editDelete\" src=\"App_Themes/Default/images/20/Error.png\" /></a></div>", img.ThumbPath, img.ImageId, img.ImagePath, img.UserId, img.VideoId);
		else
			sb.Append("noRecord");

		return sb.ToString();
	}

	public void DeleteImage(int imageId)
	{
		TimeFliesByEntities dc = ContextHelper.DataContext;

		Images image = dc.Images.First(o => o.ImageId == imageId);
		ImageHelper.DeleteImage(image);

		image.IsDelete = true;

		Videos vdo = dc.Videos.First(o => o.VideoId == image.VideoId);
		vdo.IsCompile = false;
		vdo.IsError = false;

		dc.SaveChanges();
	}

	public bool IsReusable
	{
		get
		{
			return false;
		}
	}
}