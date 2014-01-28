using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jobs.Common.DB;
using System.Data.SqlClient;
using System.Data;
using TimeFliesBy.WebUI;
using TimeFlies.Data;
using TimeFliesBy.Business.Entity;

namespace TimeFliesBy.Business.Dao
{
  public class AppUserDao
  {
    public static List<UserReminderInfo> GetUsersReminderInfo(TimeFliesByEntities dc)
    {
      return dc.ExecuteStoreQuery<UserReminderInfo>(@"select u.UserId, u.AccessToken, u.IsActive, v.IsReminder, v.ServerReminderTime as LastReminderDT, u.FullName, u.Email,
count(i.ImageId) as ImageCount, max(i.DateAdded) as MaxImageDT
from [User] u (nolock)
inner join Videos v (nolock) on v.UserId=u.UserId
left join [Images] i (nolock) on i.UserId=u.UserId
group by u.UserId, u.AccessToken, u.IsActive, v.IsReminder, v.ServerReminderTime, u.FullName, u.Email").ToList();
    }

    public static DataTable GetUsersInfo(IEnumerable<string> userIds)
    {
      DataTable dt = new DataTable();
      if (userIds == null || userIds.Count() == 0)
        return dt;

      string s = String.Join(",", userIds.Where(o => !String.IsNullOrEmpty(o)).Select(o => "'" + o.Replace("'", "''") + "'"));

      string sql = @"
with ImageShow as
(
  select U.userId,U.FullName
,(select top 1 ImagePath from Images where UserId =u.UserId order by DateAdded desc) as ImagePath
,(select top 1 VideoId from Videos where UserId =u.UserId order by DateAdded desc ) as VideoPath
,(select top 1 ImageId from Images where UserId =u.UserId order by DateAdded desc) as ImageId
,(select top 1 Publish from Videos where UserId =u.UserId order by DateAdded desc) as Publish
 from [User] u where u.userid in (" + s + @") 
)

select * from imageShow where Publish !='Private'";

      
      QueryExecutor.FillDataTable(sql, dt);
      return dt;
    }
  }
}