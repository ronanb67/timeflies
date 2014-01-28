using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeFliesBy.Business.Entity
{
  public class UserReminderInfo
  {
    public string UserId { get; set; }
    public string AccessToken { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public bool IsReminder { get; set; }
    public int ImageCount { get; set; }
    public DateTime? MaxImageDT { get; set; }
    public DateTime? LastReminderDT { get; set; }
  }
}