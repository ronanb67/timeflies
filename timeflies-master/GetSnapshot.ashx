<%@ WebHandler Language="C#" Class="getImages" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Specialized;
using System.Net.Mail;
using TimeFliesBy.Business.Dao;
using System.Collections.Generic;
using System.Linq;
using TimeFliesBy.WebUI;
using Jobs.Common.DB;
using TimeFlies.Data;
using System.IO;

public class getImages : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
	public void ProcessRequest(HttpContext context)
	{
		string userId = context.Request.QueryString["userid"].ToString();
		string flag = context.Request.QueryString["flag"].ToString();

		try
		{
			if (flag == "LoginUser")
			{
				AppUser usr = ContextHelper.DataContext.User.FirstOrDefault(o => o.UserId == userId);

				if (usr != null)
				{
					SessionHelper.UserId = userId;
					context.Response.Write("Success");
				}
				else
				{
					context.Response.Write("UserDeleted");
				}
			}
			else if (flag == "zip")
			{
				string zipFileUrl;
				int count = ImageHelper.ZipUserImages(userId, true, out zipFileUrl);

				if (count > 0)
				{
					context.Response.Write("Success");
					//context.Response.Write("Error");
				}
				else
				{
					context.Response.Write("NoImage");
				}
			}
			else if (flag == "1")
			{
				IEnumerable<Images> images = ContextHelper.DataContext.Images.Where(o => o.UserId == userId && !o.IsDelete).OrderByDescending(o => o.DateAdded);
				string[] urls = images.Select(o => ImageHelper.GetImageUrl(o) + "#" + o.DateAdded.ToShortDateString()).ToArray();
				string imageUrl = String.Join(",", urls);
				if (String.IsNullOrEmpty(imageUrl))
					imageUrl = "noimage";
				context.Response.Write("Success|" + imageUrl + "|" + urls.Length);
			}
			else if (flag == "DeleteAccount")
			{
				AppUser usr = ContextHelper.DataContext.User.FirstOrDefault(o => o.UserId == userId);
				EmailService.ConfirmDeleteAccount(usr);
				context.Response.Write("Success");
			}
			
		}
		catch (Exception ex)
		{
			EmailService.ErrorEmail(ex.ToString());
			context.Response.Write("Error");
		}

		context.Response.End();
	}
	
	public bool IsReusable
	{
		get
		{
			return false;
		}
	}
}