using ChatLuongComputer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ChatLuongComputer.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        private ChatLuongComputerDbContext db = new ChatLuongComputerDbContext();
        public ActionResult Login()
        {
            if (Session["Admin_Name"] != null)
            {
                Response.Redirect("~/Admin");
            }
            return View();
        }
        [HttpPost]
        public JsonResult Login(string User, string Pass)
        {
            // 🔐 Kiểm tra Google reCAPTCHA
            string captchaResponse = Request["g-recaptcha-response"];
            string secretKey = "6Le0qh8rAAAAAPYllJPm0DPPqu-l64ie8NfBeJOU"; 

            using (var client = new WebClient())
            {
                var result = client.DownloadString(
                    $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={captchaResponse}"
                );

                var captchaResult = new JavaScriptSerializer().Deserialize<ReCaptchaResponse>(result);
                if (!captchaResult.success)
                {
                    return Json(new { s = 3 }); // s = 3 → CAPTCHA thất bại
                }
            }

            // 🔍 Kiểm tra username tồn tại
            int count_username = db.Users.Where(m => m.Status == 1 &&
                        ((m.Phone).ToString() == User || m.Email == User || m.Name == User)
                        && m.Access != 0).Count();

            if (count_username == 0)
                return Json(new { s = 1 }); // Tài khoản không tồn tại

            // 🔐 Kiểm tra mật khẩu
            string Password = MyString.ToMD5(Pass);
            var user_acount = db.Users.Where(m => m.Status == 1 &&
                        ((m.Phone).ToString() == User || m.Email == User || m.Name == User)
                        && m.Access != 0 && m.Password == Password);

            if (user_acount.Count() == 0)
                return Json(new { s = 2 }); // Sai mật khẩu

            // ✅ Đăng nhập thành công
            var user = user_acount.First();
            Session["Admin_Name"] = user.FullName;
            Session["Admin_ID"] = user.ID;
            Session["Admin_Images"] = user.Image;
            Session["Admin_Email"] = user.Email;
            Session["Admin_Created_at"] = user.Created_at;

            return Json(new { s = 0 }); // Thành công
        }


        public ActionResult Logout()
        {
            if (Session["Admin_Name"] != null)
            {
                Session["Admin_Name"] = null;
                Session["Admin_ID"] = null;
                Session["Admin_Images"] = null;
                Session["Admin_Address"] = null;
                Session["Admin_Email"] = null;
                Session["Admin_Created_at"] = null;
            }
            return Redirect("~/Admin/Login");
        }
        public ActionResult Profiles()
        {
            if (Session["Admin_Name"] == null)
            {
                return Redirect("~/Admin/Login");
            }
            if (Session["Admin_Name"] == null)
            {
                return HttpNotFound();
            }


            return View("Profiles");

        }
        public ActionResult Check()
        {
            if (Session["Admin_Name"] == null)
            {
                return Redirect("~/Admin/Login");
            }
            if (Session["Admin_Name"] == null)
            {
                return HttpNotFound();
            }
            var list = db.Users.Where(m => m.Status == 1).ToList();
            return View("_Test", list);

        }
    }
}
public class ReCaptchaResponse
{
    public bool success { get; set; }
    public List<string> error_codes { get; set; }
}
