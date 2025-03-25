using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact

        [HttpPost]
        public ActionResult SendMail(string name, string email, string message)
        {
            try
            {
                var fromEmail = System.Configuration.ConfigurationManager.AppSettings["Email"];
                var password = System.Configuration.ConfigurationManager.AppSettings["PasswordEmail"];
                var toEmail = System.Configuration.ConfigurationManager.AppSettings["EmailAdmin"];

                var subject = "Thông tin liên hệ từ khách hàng";
                var body = $"Tên: {name}\nEmail: {email}\nNội dung:\n{message}";

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromEmail, password),
                    EnableSsl = true,
                };

                smtpClient.Send(fromEmail, toEmail, subject, body);

                TempData["SuccessMessage"] = "Gửi thành công! Chúng tôi sẽ phản hồi sớm.";
            }
            catch
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi gửi, vui lòng thử lại sau.";
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}