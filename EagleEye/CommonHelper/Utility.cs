using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace EagleEye.CommonHelper
{
    public static class Utility
    {
        public static int StartIndex()
        {
            return Convert.ToInt32(HttpContext.Current.Request.Form.GetValues("start").FirstOrDefault());
        }

        public static int PageSize()
        {
            var length = HttpContext.Current.Request.Form.GetValues("length").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            return pageSize;
        }

        public static string SortBy()
        {
            var context = HttpContext.Current.Request;
            var sortByColumn = context.Form.GetValues("columns[" + context.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            return sortByColumn;
        }

        public static string SortDesc()
        {
            var context = HttpContext.Current.Request;
            var sortColumnDir = context.Form.GetValues("order[0][dir]").FirstOrDefault();
            if (string.IsNullOrEmpty(sortColumnDir))
                sortColumnDir = "asc";

            return sortColumnDir;
        }

        public static string FilterText()
        {
            var context = HttpContext.Current.Request;
            return context.Form.GetValues("search[value]")[0];
        }

        public static void SetCookie(string key, string value)
        {
            HttpCookie StudentCookies = new HttpCookie(key);
            StudentCookies.Value = value;
            StudentCookies.Expires = DateTime.Now.AddHours(24);
            HttpContext.Current.Response.SetCookie(StudentCookies);
        }

        public static string GetCookie(string key)
        {
            var result = "";
            if (HttpContext.Current.Request.Cookies[key] != null)
            {
                result = HttpContext.Current.Request.Cookies[key].Value;
            }
            return result;
        }

        public static bool RemoveCookie(string key)
        {
            bool result = false;
            if (HttpContext.Current.Request.Cookies[key] != null)
            {
                var c = new HttpCookie(key);
                c.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(c);
                result = true;
            }
            return result;
        }

        public static string SaveImage()
        {
            var context = HttpContext.Current;
            string dbPath = string.Empty;
            if (context.Request.Files.Count > 0)
            {
                var file = context.Request.Files[0];
                if (file.ContentLength > 0)
                {
                    string segment = DateTime.Now.ToString("yyyyMMdd");
                    string folderPath = Path.Combine(context.Server.MapPath(ConfigValues.ImagePath), segment);
                    string fileExtension = Path.GetExtension(file.FileName).ToLower();
                    if (string.IsNullOrWhiteSpace(fileExtension))
                    {
                        char[] splitImg = { '/' };
                        string[] getExtention = file.ContentType.Split(splitImg);
                        fileExtension = "." + getExtention[1];
                    }
                    if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif" || fileExtension == ".bmp")
                    {
                        string fileName = Guid.NewGuid().ToString("N") + fileExtension;
                        string CheckImageExtension = Path.GetExtension(fileName);

                        string path = Path.Combine(folderPath, fileName);
                        dbPath = ConfigValues.ImagePath + "/" + segment + "/" + fileName;
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        file.SaveAs(path);
                    }
                    else
                    {
                        dbPath = "0";
                        return dbPath;
                    }
                }
            }
            return dbPath;
        }

        public static string SaveStoryImage(string StoryId)
        {

            var context = HttpContext.Current;
            string dbPath = string.Empty;
            if (context.Request.Files.Count > 0)
            {
                var file = context.Request.Files[0];
                if (file.ContentLength > 0)
                {
                    string segment = DateTime.Now.ToString("yyyyMMdd");
                    string folderPath = Path.Combine(context.Server.MapPath("~/Uploads/Story"));
                    string fileExtension = Path.GetExtension(file.FileName).ToLower();
                    if (string.IsNullOrWhiteSpace(fileExtension))
                    {
                        char[] splitImg = { '/' };
                        string[] getExtention = file.ContentType.Split(splitImg);
                        fileExtension = "." + getExtention[1];
                    }
                    if (fileExtension == ".jpg" || fileExtension == ".png" || fileExtension == ".gif" || fileExtension == ".jpeg")
                    {

                        string fileName = Guid.NewGuid().ToString("N") + fileExtension;
                        string CheckImageExtension = Path.GetExtension(fileName);

                        string path = Path.Combine(folderPath, "Story" + StoryId + fileExtension);
                        dbPath = "~/Uploads/Story" + "/" + "Story" + StoryId + fileExtension;
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        file.SaveAs(path);
                    }
                    else
                    {
                        dbPath = "0";
                        return dbPath;
                    }
                }
            }
            return dbPath;
        }

        public static string SaveTeamImage(string TeamId)
        {

            var context = HttpContext.Current;
            string dbPath = string.Empty;
            if (context.Request.Files.Count > 0)
            {
                var file = context.Request.Files[0];
                if (file.ContentLength > 0)
                {
                    string segment = DateTime.Now.ToString("yyyyMMdd");
                    string folderPath = Path.Combine(context.Server.MapPath("~/Uploads/Team"));
                    string fileExtension = Path.GetExtension(file.FileName).ToLower();
                    if (string.IsNullOrWhiteSpace(fileExtension))
                    {
                        char[] splitImg = { '/' };
                        string[] getExtention = file.ContentType.Split(splitImg);
                        fileExtension = "." + getExtention[1];
                    }
                    if (fileExtension == ".jpg" || fileExtension == ".png" || fileExtension == ".gif" || fileExtension == ".jpeg")
                    {

                        string fileName = Guid.NewGuid().ToString("N") + fileExtension;
                        string CheckImageExtension = Path.GetExtension(fileName);

                        string path = Path.Combine(folderPath, "Team" + TeamId + fileExtension);
                        dbPath = "~/Uploads/Team" + "/" + "Team" + TeamId + fileExtension;
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        file.SaveAs(path);
                    }
                    else
                    {
                        dbPath = "0";
                        return dbPath;
                    }
                }
            }
            return dbPath;
        }

        public static string SaveUserImage(string UserId)
        {

            var context = HttpContext.Current;
            string dbPath = string.Empty;
            if (context.Request.Files.Count > 0)
            {
                var file = context.Request.Files[0];
                if (file.ContentLength > 0)
                {
                    string segment = DateTime.Now.ToString("yyyyMMdd");
                    string folderPath = Path.Combine(context.Server.MapPath("~/Uploads/Users"));
                    string fileExtension = Path.GetExtension(file.FileName).ToLower();
                    if (string.IsNullOrWhiteSpace(fileExtension))
                    {
                        char[] splitImg = { '/' };
                        string[] getExtention = file.ContentType.Split(splitImg);
                        fileExtension = "." + getExtention[1];
                    }
                    if (fileExtension == ".jpg" || fileExtension == ".png" || fileExtension == ".gif" || fileExtension == ".jpeg")
                    {

                        string fileName = Guid.NewGuid().ToString("N") + fileExtension;
                        string CheckImageExtension = Path.GetExtension(fileName);

                        string path = Path.Combine(folderPath, "User_" + UserId + fileExtension);
                        dbPath = "~/Uploads/Users" + "/" + "User_" + UserId + fileExtension;
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        file.SaveAs(path);
                    }
                    else
                    {
                        dbPath = "0";
                        return dbPath;
                    }
                }
            }
            return dbPath;
        }

        public static string SaveAffiliateImage(string AffiliateId)
        {

            var context = HttpContext.Current;
            string dbPath = string.Empty;
            if (context.Request.Files.Count > 0)
            {
                var file = context.Request.Files[0];
                if (file.ContentLength > 0)
                {
                    string segment = DateTime.Now.ToString("yyyyMMdd");
                    string folderPath = Path.Combine(context.Server.MapPath("~/Uploads/Affiliate"));
                    string fileExtension = Path.GetExtension(file.FileName).ToLower();
                    if (string.IsNullOrWhiteSpace(fileExtension))
                    {
                        char[] splitImg = { '/' };
                        string[] getExtention = file.ContentType.Split(splitImg);
                        fileExtension = "." + getExtention[1];
                    }
                    if (fileExtension == ".jpg" || fileExtension == ".png" || fileExtension == ".gif" || fileExtension == ".jpeg")
                    {

                        string fileName = Guid.NewGuid().ToString("N") + fileExtension;
                        string CheckImageExtension = Path.GetExtension(fileName);

                        string path = Path.Combine(folderPath, "Affiliate" + AffiliateId + fileExtension);
                        dbPath = "~/Uploads/Affiliate" + "/" + "Affiliate" + AffiliateId + fileExtension;
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        file.SaveAs(path);
                    }
                    else
                    {
                        dbPath = "0";
                        return dbPath;
                    }
                }
            }
            return dbPath;
        }


        public static string SaveItemImage(string ItemId)
        {

            var context = HttpContext.Current;
            string dbPath = string.Empty;
            string path = string.Empty;

            if (context.Request.Files.Count > 0)
            {
                var file = context.Request.Files[0];
                if (file.ContentLength > 0)
                {
                    string segment = DateTime.Now.ToString("yyyyMMdd");
                    string folderPath;
                    folderPath = Path.Combine(context.Server.MapPath("~/Uploads/Items"), ItemId);

                    string fileExtension = Path.GetExtension(file.FileName).ToLower();
                    if (string.IsNullOrWhiteSpace(fileExtension))
                    {
                        char[] splitImg = { '/' };
                        string[] getExtention = file.ContentType.Split(splitImg);
                        fileExtension = "." + getExtention[1];
                    }

                    if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif")
                    {
                        string fileName = Guid.NewGuid().ToString("N") + fileExtension;
                        string CheckImageExtension = Path.GetExtension(fileName);

                        path = Path.Combine(folderPath, fileName);
                        dbPath = "~/Uploads/Items/" + ItemId + "/" + fileName;

                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        file.SaveAs(path);
                    }
                    else
                    {
                        dbPath = "0";
                        return dbPath;
                    }
                }
            }
            return dbPath;
        }

        public static string SaveTempImage()
        {

            var context = HttpContext.Current;
            string dbPath = string.Empty;
            string path = string.Empty;

            if (context.Request.Files.Count > 0)
            {
                var file = context.Request.Files[0];
                if (file.ContentLength > 0)
                {
                    string segment = DateTime.Now.ToString("yyyyMMdd");
                    string folderPath;
                    folderPath = Path.Combine(context.Server.MapPath("~/Uploads/Temp"));

                    string fileExtension = Path.GetExtension(file.FileName).ToLower();
                    if (string.IsNullOrWhiteSpace(fileExtension))
                    {
                        char[] splitImg = { '/' };
                        string[] getExtention = file.ContentType.Split(splitImg);
                        fileExtension = "." + getExtention[1];
                    }

                    if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif")
                    {
                        string fileName = Guid.NewGuid().ToString("N") + fileExtension;
                        string CheckImageExtension = Path.GetExtension(fileName);

                        path = Path.Combine(folderPath, fileName);
                        dbPath = "~/Uploads/Temp/" + fileName;

                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        file.SaveAs(path);
                    }
                    else
                    {
                        dbPath = "0";
                        return dbPath;
                    }
                }
            }
            return dbPath;
        }
        public static string SaveSponsorImage(string Id)
        {

            var context = HttpContext.Current;
            string dbPath = string.Empty;
            if (context.Request.Files.Count > 0)
            {
                var file = context.Request.Files[0];
                if (file.ContentLength > 0)
                {
                    string segment = DateTime.Now.ToString("yyyyMMdd");
                    string folderPath = Path.Combine(context.Server.MapPath("~/Uploads/Sponsor"));
                    string fileExtension = Path.GetExtension(file.FileName).ToLower();
                    if (string.IsNullOrWhiteSpace(fileExtension))
                    {
                        char[] splitImg = { '/' };
                        string[] getExtention = file.ContentType.Split(splitImg);
                        fileExtension = "." + getExtention[1];
                    }
                    if (fileExtension == ".jpg" || fileExtension == ".png" || fileExtension == ".gif" || fileExtension == ".jpeg")
                    {

                        string fileName = Guid.NewGuid().ToString("N") + fileExtension;
                        string CheckImageExtension = Path.GetExtension(fileName);

                        string path = Path.Combine(folderPath, "Sponsor" + Id + fileExtension);
                        dbPath = "~/Uploads/Sponsor" + "/" + "Sponsor" + Id + fileExtension;
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        file.SaveAs(path);
                    }
                    else
                    {
                        dbPath = "0";
                        return dbPath;
                    }
                }
            }
            return dbPath;
        }
        public static string ReferenceError(string errorMsg)
        {
            if (errorMsg.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                return "This records is using somewhere else,delete the reference before continue.";
            else
                return errorMsg;
        }

        public static bool SendEmail(String toEmail, String subject, string body, out string outMessage, string cc = "", string bcc = "", string pathToAttachment="")
        {
            bool result = false;
            string message = string.Empty;
            try
            {
                var host = ConfigurationManager.AppSettings["Host"].ToString();
                var fromEmail = ConfigurationManager.AppSettings["FromMail"].ToString();
                var fromEmailPassword = ConfigurationManager.AppSettings["FromMailPassword"].ToString();
                var port = ConfigurationManager.AppSettings["Port"].ToString();
                var useSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"].ToString());

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromEmail);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                if (!string.IsNullOrWhiteSpace(pathToAttachment))
                    mailMessage.Attachments.Add(new Attachment(pathToAttachment));

                string[] ToMultiEmailIds = toEmail.Split(',');
                foreach (string ToEMailId in ToMultiEmailIds)
                {
                    mailMessage.To.Add(new MailAddress(ToEMailId)); //adding multiple TO Email Id
                }

                string[] CCIds = cc.Split(',');
                foreach (string CCEmail in CCIds)
                {
                    if (!string.IsNullOrWhiteSpace(CCEmail))
                        mailMessage.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                }

                string[] bccid = bcc.Split(',');
                foreach (string bccEmailId in bccid)
                {
                    if (!string.IsNullOrWhiteSpace(bccEmailId))
                        mailMessage.Bcc.Add(new MailAddress(bccEmailId)); //Adding Multiple BCC email Id
                }

                SmtpClient smtp = new SmtpClient();
                smtp.Host = host; //smtp.gmail.com etc

                //network and security related credentials
                 
                NetworkCredential NetworkCred = new NetworkCredential();
                NetworkCred.UserName = mailMessage.From.Address;
                NetworkCred.Password = fromEmailPassword;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = Convert.ToInt32(port);
                smtp.EnableSsl = useSsl;
                smtp.Send(mailMessage);
                result = true;
                message = "Email sent successfully.";
            }
            catch (Exception ex)
            {
                result = false;
                message = ex.Message;
            }
            outMessage = message;
            return result;
        }
    }
}