using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace CRM.WebApp
{
	public class Global : System.Web.HttpApplication
	{
		#region by sunil
		protected void Application_Start(object sender, EventArgs e)
		{
			Application["OnlineUsers"] = 0;
		}
		#endregion
		#region by sunil
		protected void Session_Start(object sender, EventArgs e)
		{
			Application.Lock();

			Application["OnlineUsers"] = (int)Application["OnlineUsers"] + 1;

			Application.UnLock();

            

         

		}
		#endregion
		protected void Application_BeginRequest(object sender, EventArgs e)
		{
            

		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{

		}

		protected void Application_Error(object sender, EventArgs e)
		{
			HttpContext ctx = HttpContext.Current;
			Exception exception = ctx.Server.GetLastError().InnerException;
			HttpContext.Current.Application.Add("LastError", exception);
		}
		#region by sunil
		protected void Session_End(object sender, EventArgs e)
		{
           
            Application.Lock();

            Application["OnlineUsers"] = (int)Application["OnlineUsers"] - 1;

            Application.UnLock();

           
     
		}
		#endregion
		protected void Application_End(object sender, EventArgs e)
		{

		}
	}
}