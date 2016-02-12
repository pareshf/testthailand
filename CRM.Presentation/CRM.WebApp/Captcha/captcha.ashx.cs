using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Web.SessionState;
namespace CRM.WebApp.Captcha
{
    /// <summary>
    /// Summary description for captcha
    /// </summary>
    public class captcha : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/jpeg";
            CRM.WebApp.Captcha.CatpchaImage captcha = new CRM.WebApp.Captcha.CatpchaImage();
            string str = captcha.DrawNumbers(5);

            if (context.Session[CRM.WebApp.Captcha.CatpchaImage.SESSION_CAPTCHA] == null) context.Session.Add(CRM.WebApp.Captcha.CatpchaImage.SESSION_CAPTCHA, str);

            else
            {
                context.Session[CatpchaImage.SESSION_CAPTCHA] = str;
            }
            Bitmap bmp = captcha.Result;
            bmp.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}