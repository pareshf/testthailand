using System.Data;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Xml;
using System.Web.Security;
using System.Net;
using System.IO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

using CRM.DataAccess.SecurityDAL;
using CRM.Core.Constants;
using CRM.Model.Security;
using CRM.Model.General;
using System.Net.Mail;
using System.Web.Mail;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace CRM.WebApp.WebHelper
{
    public class WebManager
    {
        #region Member varibles


        #endregion

        public static void CheckSessionIsActive()
        {
            if (HttpContext.Current.Session == null)
            {
                HttpContext.Current.Session.RemoveAll();
                HttpContext.Current.Session.Abandon();
                FormsAuthentication.SignOut();
                HttpContext.Current.Response.Redirect("../SessionExpired.aspx");
            }
        }

        public static void CheckUserAuthorizationForProgram(string ProgramName)
        {
            DataTable dtAccess = new DataTable();
            DataSet dsAccess = new DataSet();
            AuthorizationBDto objAuthorizationBDto;
            AuthorizationDal objAuthorizationDal;
            if (HttpContext.Current.Session[PageConstants.ssnUserAuthorization] != null)
            {
                objAuthorizationBDto = (AuthorizationBDto)HttpContext.Current.Session[PageConstants.ssnUserAuthorization];
                int RoleId = objAuthorizationBDto.UserSelectedRoleId;
                if (RoleId != 0)
                {
                    objAuthorizationDal = new AuthorizationDal();
                    dsAccess = objAuthorizationDal.GetProgrameAccessByProgramName(RoleId, ProgramName);
                    dtAccess = dsAccess.Tables[0];
                    if (dtAccess.Rows.Count > 0)
                    {
                        if (dtAccess.Rows[0]["READ_ACCESS"] != null)
                        {
                            if (dtAccess.Rows[0]["READ_ACCESS"].ToString() != "True")
                            {
                                int ModuleId = int.Parse(dtAccess.Rows[0]["MODULE_ID"].ToString());
                                switch (ModuleId)
                                {
                                    case 1: // Administration
                                        HttpContext.Current.Response.Redirect("~/Views/Administration/AccessDenied.aspx");
                                        break;
                                    case 2: // Customers
                                        HttpContext.Current.Response.Redirect("~/Views/Customers/AccessDenied.aspx");
                                        break;
                                    case 3: // Inquiry
                                        HttpContext.Current.Response.Redirect("~/Views/Inquiry/AccessDenied.aspx");
                                        break;
                                    case 4: // Orders
                                        HttpContext.Current.Response.Redirect("~/Views/Orders/AccessDenied.aspx");
                                        break;
                                    case 5: // Fares
                                        HttpContext.Current.Response.Redirect("~/Views/Fares/AccessDenied.aspx");
                                        break;
                                    case 6: // HR
                                        HttpContext.Current.Response.Redirect("~/Views/HR/AccessDenied.aspx");
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        else
                        {
                            HttpContext.Current.Response.Redirect("~/Default.aspx");
                        }
                        objAuthorizationBDto = new AuthorizationBDto();
                        objAuthorizationBDto = (AuthorizationBDto)HttpContext.Current.Session[PageConstants.ssnUserAuthorization];
                        if (dtAccess.Rows[0]["READ_ACCESS"] != null)
                            objAuthorizationBDto.ProgramReadAccess = bool.Parse(dtAccess.Rows[0]["READ_ACCESS"].ToString());
                        if (dtAccess.Rows[0]["WRITE_ACCESS"] != null)
                            objAuthorizationBDto.ProgramWriteAccess = bool.Parse(dtAccess.Rows[0]["WRITE_ACCESS"].ToString());
                        if (dtAccess.Rows[0]["DELETE_ACCESS"] != null)
                            objAuthorizationBDto.ProgramDeleteAccess = bool.Parse(dtAccess.Rows[0]["DELETE_ACCESS"].ToString());
                        if (dtAccess.Rows[0]["PRINT_ACCESS"] != null)
                            objAuthorizationBDto.ProgramPrintAccess = bool.Parse(dtAccess.Rows[0]["PRINT_ACCESS"].ToString());
                        HttpContext.Current.Session[PageConstants.ssnUserAuthorization] = objAuthorizationBDto;
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("~/Default.aspx");
                    }
                }
            }
        }

        public static bool SendSMS(string mobileNo, string message)
        {
            bool returnFlag;
            try
            {                
                string userName = GetApplicationSetting("smsusername");
                string password = GetApplicationSetting("smspassword");
                string domian = GetApplicationSetting("smsdomain");
                string senderId = GetApplicationSetting("smssenderid");

                string result = SMSApiCall("http://" + domian + "?username=" + userName + "&pass=" + password + "&senderid=" + senderId + "&message=" + message + "&dest_mobileno=" + "91"+mobileNo + "&response=Y");
                if (result.StartsWith("Invalid Username and Password"))
                    returnFlag = false;
                else if (result.StartsWith("Message is blank"))
                    returnFlag = false;
                else if (result.StartsWith("Account is Expire"))
                    returnFlag = false;
                else if (result.StartsWith("You have Exceeded your SMS Limit"))
                    returnFlag = false;
                else if (result.StartsWith("Invalid SenderID"))
                    returnFlag = false;
                else
                    returnFlag = true;

                return returnFlag;
            }
            catch
            {
                returnFlag = false;
                return returnFlag;
            }
        }

        public static bool SendEmail(EmailBDto email)
        {

            bool mailSent = true;
            System.Net.Mail.MailMessage objMsg = null;
            SmtpClient smtpClient = new SmtpClient();



            objMsg = new System.Net.Mail.MailMessage();
            objMsg.From = new MailAddress(email.From, email.DisplayName, Encoding.UTF8);
            objMsg.To.Add(email.To);

            if (!string.IsNullOrEmpty(email.CC))
            { objMsg.CC.Add(email.CC); }

            if (!string.IsNullOrEmpty(email.Bcc))
            { objMsg.Bcc.Add(email.Bcc); }

            objMsg.Subject = email.Subject;
            objMsg.SubjectEncoding = Encoding.UTF8;
            objMsg.Body = email.Body;
            objMsg.BodyEncoding = Encoding.UTF8;
            
            objMsg.IsBodyHtml = true;
            objMsg.Priority = System.Net.Mail.MailPriority.High;

            if (email.Attachments !=null)
            {

                foreach (Attachment attachment in email.Attachments)
                {
                    objMsg.Attachments.Add(attachment);
                }
            }
            try
            {
               // smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings[PageConstants.SmtpUserName], ConfigurationManager.AppSettings[PageConstants.SmtpPassword]);
               // smtpClient.Host = ConfigurationManager.AppSettings[PageConstants.SmtpHost];
                //smtpClient.Host = "localhost";
                //smtpClient.Port = int.Parse(ConfigurationManager.AppSettings[PageConstants.SmtpPort]);
                //smtpClient.EnableSsl = true;
                //System.Web.Mail.SmtpMail.SmtpServer = "localhost";
               


                
                //smtpClient.Host = "localhost";
                            
                //smtpClient.Port = 25;
                //smtpClient.Send(objMsg);
                //mailSent = true;



               // smtpClient.Host = "smtp.gmail.com";

                //smtpClient.Port= 587;

                //smtpClient.EnableSsl = true;

                //smtpClient.Credentials = new NetworkCredential("tester.source@gmail.com", "ambasoft");
                 smtpClient.Send(objMsg);
                
                
            }
            catch (Exception ex)
            {
                mailSent = false;
            }
            return mailSent;
        }

        public static string SMSApiCall(string url)
        {
            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);

            try
            {

                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();

                StreamReader sr = new StreamReader(httpres.GetResponseStream());

                string results = sr.ReadToEnd();

                sr.Close();
                return results;
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public static bool SetApplicationSetting(string key, string value)
        {
            
            try
            {
                string path = HttpContext.Current.Server.MapPath("~/Config/ApplicationSettings.xml");
                File.SetAttributes(path, FileAttributes.Archive);
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(path);
                xDoc.GetElementsByTagName(key).Item(0).InnerText = value.Trim();
                xDoc.Save(path);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool SetApplicationSetting(Dictionary<string, string> settings)
        {
            
            try
            {
                string path = HttpContext.Current.Server.MapPath("~/Config/ApplicationSettings.xml");
                File.SetAttributes(path, FileAttributes.Archive);
                
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(path);

                foreach (KeyValuePair<string, string> pair in settings)
                {
                    xDoc.GetElementsByTagName(pair.Key).Item(0).InnerText = pair.Value.Trim();
                }

                xDoc.Save(path);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string GetApplicationSetting(string key)
        {
            string value = string.Empty;

            try
            {
                string path = HttpContext.Current.Server.MapPath("~/Config/ApplicationSettings.xml");
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(path);
                value = xDoc.GetElementsByTagName(key).Item(0).InnerText;
                return value.Trim();
            }
            catch (Exception ex)
            {
                return value;
            }
        }

        public static Dictionary<string, string> GetApplicationSetting(string key, bool issectiongroup)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();

            try
            {
                string path = HttpContext.Current.Server.MapPath("~/Config/ApplicationSettings.xml");
                
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(path);
                XmlNodeList nodelist = xDoc.GetElementsByTagName(key);
                XmlNodeList appsettingslist = nodelist[0].ChildNodes;

                foreach (XmlNode node in appsettingslist)
                {
                    values.Add(node.Name, node.InnerText);
                }
                return values;
            }
            catch (Exception ex)
            {
                return values;
            }
        }

        public static Boolean DownloadDocument(string fileName, string fileContentType, byte[] fileByte)
        {
            try
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = fileContentType;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                HttpContext.Current.Response.Charset = "";

                // Response.Cache.SetCacheability(HttpCacheability.NoCache);

                // If you write,
                // Response.Write(bytFile1);
                // then you will get only 13 byte in bytFile.

                HttpContext.Current.Response.BinaryWrite(fileByte);
                HttpContext.Current.Response.End();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    public class Pop3Client : IDisposable
    {
        public string Host { get; protected set; }
        public int Port { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public bool IsSecure { get; protected set; }

        public TcpClient Client { get; protected set; }
        public Stream ClientStream { get; protected set; }

        public StreamWriter Writer { get; protected set; }
        public StreamReader Reader { get; protected set; }

        private bool disposed = false;

        public Pop3Client(string host, int port, string email,
          string password)
            : this(host, port, email, password, false)
        {
        }

        public Pop3Client(string host, int port, string email,
          string password, bool secure)
        {
            Host = host;
            Port = port;
            Email = email;
            Password = password;
            IsSecure = secure;
        }

        public void Connect()
        {
            if (Client == null)
                Client = new TcpClient();

            if (!Client.Connected)
                Client.Connect(Host, Port);

            if (IsSecure)
            {
                SslStream secureStream =
                  new SslStream(Client.GetStream());
                secureStream.AuthenticateAsClient(Host);

                ClientStream = secureStream;
                secureStream = null;
            }
            else
                ClientStream = Client.GetStream();

            Writer = new StreamWriter(ClientStream);
            Reader = new StreamReader(ClientStream);

            ReadLine();
            Login();
        }

        public int GetEmailCount()
        {
            int count = 0;
            string response = SendCommand("STAT");

            if (IsResponseOk(response))
            {
                string[] arr = response.Substring(4).Split(' ');
                count = Convert.ToInt32(arr[0]);
            }
            else
                count = -1;

            return count;
        }

        public Email FetchEmail(int emailId)
        {
            if (IsResponseOk(SendCommand("TOP " + emailId + " 0")))
                return new Email(ReadLines());
            else
                return null;
        }

        public List<Email> FetchEmailList(int start, int count)
        {
            List<Email> emails = new List<Email>(count);

            for (int i = start; i < (start + count); i++)
            {
                Email email = FetchEmail(i);

                if (email != null)
                    emails.Add(email);
            }

            return emails;
        }

        public List<MessagePart> FetchMessageParts(int emailId)
        {
            if (IsResponseOk(SendCommand("RETR " + emailId)))
                return Util.ParseMessageParts(ReadLines());

            return null;
        }


        public void Close()
        {
            if (Client != null)
            {
                if (Client.Connected)
                    Logout();

                Client.Close();
                Client = null;
            }

            if (ClientStream != null)
            {
                ClientStream.Close();
                ClientStream = null;
            }

            if (Writer != null)
            {
                Writer.Close();
                Writer = null;
            }

            if (Reader != null)
            {
                Reader.Close();
                Reader = null;
            }

            disposed = true;
        }

        public void Dispose()
        {
            if (!disposed)
                Close();
        }

        protected void Login()
        {
            if (!IsResponseOk(SendCommand("USER " + Email)) ||
              !IsResponseOk(SendCommand("PASS " + Password)))
                throw new Exception("User/password not accepted");
        }

        protected void Logout()
        {
            SendCommand("RSET");
            //SendCommand("QUIT");
        }

        protected string SendCommand(string cmdtext)
        {
            Writer.WriteLine(cmdtext);
            Writer.Flush();

            return ReadLine();
        }

        protected string ReadLine()
        {
            return Reader.ReadLine() + "\r\n";
        }

        protected string ReadLines()
        {
            StringBuilder b = new StringBuilder();

            while (true)
            {
                string temp = ReadLine();

                if (temp == ".\r\n" || temp.IndexOf("-ERR") != -1)
                    break;

                b.Append(temp);
            }

            return b.ToString();
        }

        protected static bool IsResponseOk(string response)
        {
            if (response.StartsWith("+OK"))
                return true;
            if (response.StartsWith("-ERR"))
                return false;

            throw new Exception("Cannot understand server response: " +
              response);
        }
    }

    public class Email
    {
        public NameValueCollection Headers { get; protected set; }

        public string ContentType { get; protected set; }
        public DateTime UtcDateTime { get; protected set; }
        public string From { get; protected set; }
        public string To { get; protected set; }
        public string Subject { get; protected set; }

        public Email(string emailText)
        {
            Headers = Util.ParseHeaders(emailText);

            ContentType = Headers["Content-Type"];
            From = Headers["From"];
            To = Headers["To"];
            Subject = Headers["Subject"];

            if (Headers["Date"] != null)
                try
                {
                    UtcDateTime =
                      Util.ConvertStrToUtcDateTime(Headers["Date"]);
                }
                catch (FormatException)
                {
                    UtcDateTime = DateTime.MinValue;
                }
            else
                UtcDateTime = DateTime.MinValue;
        }
    }

    public class MessagePart
    {
        public NameValueCollection Headers { get; protected set; }

        public string ContentType { get; protected set; }
        public string MessageText { get; protected set; }

        public MessagePart(NameValueCollection headers, string messageText)
        {
            Headers = headers;
            ContentType = Headers["Content-Type"];
            MessageText = messageText;
        }
    }

    public class Util
    {
        protected static Regex BoundaryRegex =
          new Regex("Content-Type: multipart(?:/\\S+;)" +
          "\\s+[^\r\n]*boundary=\"?(?<boundary>" +
          "[^\"\r\n]+)\"?\r\n", RegexOptions.IgnoreCase |
          RegexOptions.Compiled);
        protected static Regex UtcDateTimeRegex = new Regex(
          @"^(?:\w+,\s+)?(?<day>\d+)\s+(?<month>\w+)\s+(?<year>\d+)\s+(?<hour>\d{1,2})" +
          @":(?<minute>\d{1,2}):(?<second>\d{1,2})\s+(?<offsetsign>\-|\+)(?<offsethours>" +
          @"\d{2,2})(?<offsetminutes>\d{2,2})(?:.*)$",
          RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static NameValueCollection ParseHeaders(string headerText)
        {
            NameValueCollection headers = new NameValueCollection();
            StringReader reader = new StringReader(headerText);

            string line;
            string headerName = null, headerValue;
            int colonIndx;

            while ((line = reader.ReadLine()) != null)
            {
                if (line == "")
                    break;

                if (Char.IsLetterOrDigit(line[0]) && (colonIndx = line.IndexOf(':')) != -1)
                {
                    headerName = line.Substring(0, colonIndx);
                    headerValue = line.Substring(colonIndx + 1).Trim();

                    headers.Add(headerName, headerValue);
                }
                else if (headerName != null)
                    headers[headerName] += " " + line.Trim();
                else
                    throw new FormatException("Could not parse headers");
            }

            return headers;
        }

        public static List<MessagePart> ParseMessageParts(string emailText)
        {
            List<MessagePart> messageParts = new List<MessagePart>();
            int newLinesIndx = emailText.IndexOf("\r\n\r\n");

            Match m = BoundaryRegex.Match(emailText);

            if (m.Index < emailText.IndexOf("\r\n\r\n") && m.Success)
            {
                string boundary = m.Groups["boundary"].Value;
                string startingBoundary = "\r\n--" + boundary;

                int startingBoundaryIndx = -1;

                while (true)
                {
                    if (startingBoundaryIndx == -1)
                        startingBoundaryIndx = emailText.IndexOf(startingBoundary);

                    if (startingBoundaryIndx != -1)
                    {
                        int nextBoundaryIndx = emailText.IndexOf(startingBoundary,
                          startingBoundaryIndx + startingBoundary.Length);

                        if (nextBoundaryIndx != -1 && nextBoundaryIndx != startingBoundaryIndx)
                        {
                            string multipartMsg = emailText.Substring(startingBoundaryIndx +
                              startingBoundary.Length,
                              (nextBoundaryIndx - startingBoundaryIndx - startingBoundary.Length));

                            int headersIndx = multipartMsg.IndexOf("\r\n\r\n");

                            if (headersIndx == -1)
                                throw new FormatException("Incompatible multipart message format");

                            string bodyText = multipartMsg.Substring(headersIndx).Trim();

                            NameValueCollection headers = Util.ParseHeaders(multipartMsg.Trim());
                            messageParts.Add(new MessagePart(headers, bodyText));
                        }
                        else
                            break;

                        startingBoundaryIndx = nextBoundaryIndx;
                    }
                    else
                        break;
                }

                if (newLinesIndx != -1)
                {
                    string emailBodyText = emailText.Substring(newLinesIndx + 1);
                }
            }
            else
            {
                int headersIndx = emailText.IndexOf("\r\n\r\n");

                if (headersIndx == -1)
                    throw new FormatException("Incompatible multipart message format");

                string bodyText = emailText.Substring(headersIndx).Trim();

                NameValueCollection headers = Util.ParseHeaders(emailText);
                messageParts.Add(new MessagePart(headers, bodyText));
            }

            return messageParts;
        }
        public static DateTime ConvertStrToUtcDateTime(string str)
        {
            Match m = UtcDateTimeRegex.Match(str);

            int day, month, year, hour, min, sec;

            if (m.Success)
            {
                day = Convert.ToInt32(m.Groups["day"].Value);
                year = Convert.ToInt32(m.Groups["year"].Value);
                hour = Convert.ToInt32(m.Groups["hour"].Value);
                min = Convert.ToInt32(m.Groups["minute"].Value);
                sec = Convert.ToInt32(m.Groups["second"].Value);

                switch (m.Groups["month"].Value)
                {
                    case "Jan":
                        month = 1;
                        break;
                    case "Feb":
                        month = 2;
                        break;
                    case "Mar":
                        month = 3;
                        break;
                    case "Apr":
                        month = 4;
                        break;
                    case "May":
                        month = 5;
                        break;
                    case "Jun":
                        month = 6;
                        break;
                    case "Jul":
                        month = 7;
                        break;
                    case "Aug":
                        month = 8;
                        break;
                    case "Sep":
                        month = 9;
                        break;
                    case "Oct":
                        month = 10;
                        break;
                    case "Nov":
                        month = 11;
                        break;
                    case "Dec":
                        month = 12;
                        break;
                    default:
                        throw new FormatException("Unknown month.");
                }

                string offsetSign = m.Groups["offsetsign"].Value;
                int offsetHours = Convert.ToInt32(m.Groups["offsethours"].Value);
                int offsetMinutes = Convert.ToInt32(m.Groups["offsetminutes"].Value);

                DateTime dt = new DateTime(year, month, day, hour, min, sec);

                if (offsetSign == "+")
                {
                    dt.AddHours(offsetHours);
                    dt.AddMinutes(offsetMinutes);
                }
                else if (offsetSign == "-")
                {
                    dt.AddHours(-offsetHours);
                    dt.AddMinutes(-offsetMinutes);
                }

                return dt;
            }

            throw new FormatException("Incompatible date/time string format");
        }
    }

    public class PrintHelper
    {
        public PrintHelper()
        {
        }

        public static void PrintWebControl(Control ctrl)
        {
            PrintWebControl(ctrl, string.Empty);
        }

        public static void PrintWebControl(Control ctrl, string Script)
        {
            StringWriter stringWrite = new StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
            if (ctrl is WebControl)
            {
                Unit w = new Unit(100, UnitType.Percentage); ((WebControl)ctrl).Width = w;
            }
            Page pg = new Page();
            pg.EnableEventValidation = false;
            if (Script != string.Empty)
            {
                pg.ClientScript.RegisterStartupScript(pg.GetType(), "PrintJavaScript", Script);
            }
            HtmlForm frm = new HtmlForm();
            pg.Controls.Add(frm);
            frm.Attributes.Add("runat", "server");
            frm.Controls.Add(ctrl);
            pg.DesignerInitialize();
            pg.RenderControl(htmlWrite);
            string strHTML = stringWrite.ToString();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(strHTML);
            HttpContext.Current.Response.Write("<script>window.print();</script>");
            HttpContext.Current.Response.End();

           
        }
    }
}
