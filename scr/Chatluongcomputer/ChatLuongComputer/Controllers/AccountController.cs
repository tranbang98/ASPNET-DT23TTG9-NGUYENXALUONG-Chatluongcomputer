﻿using ChatLuongComputer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ChatLuongComputer.Controllers
{
    public class AccountController : Controller
    {
        private ChatLuongComputerDbContext db = new ChatLuongComputerDbContext();
        //public AccountController()
        //{
        //    if (System.Web.HttpContext.Current.Session["User_Name"] == null)
        //    {
        //        System.Web.HttpContext.Current.Response.Redirect("~/");
        //    }
        //}

        [HttpPost]
        public JsonResult UserLogin(string User, string Password)
        {
            int count_username = db.Users.Where(m => m.Status == 1 && ((m.Phone).ToString() == User || m.Email == User || m.Name == User)).Count();
            if (count_username == 0)
            {
                return Json(new { s = 1 }); // Tài khoản không tồn tại
            }
            else
            {
                Password = MyString.ToMD5(Password); // Mã hóa mật khẩu
                var user_acount = db.Users.Where(m => m.Status == 1 && ((m.Phone).ToString() == User || m.Email == User || m.Name == User) && m.Password == Password);
                if (user_acount.Count() == 0)
                {
                    return Json(new { s = 2 }); // Sai mật khẩu
                }
                else
                {
                    var user = user_acount.First();
                    Session["User_Name"] = user.FullName;
                    Session["User_ID"] = user.ID;
                }
            }
            return Json(new { s = 0 }); // Đăng nhập thành công
        }


        public ActionResult UserLogout(String url)
        {
            Session["User_Name"] = null;
            Session["User_ID"] = null;
            return Redirect("~/" + url);
        }
        public ActionResult ProFile()
        {
            if (System.Web.HttpContext.Current.Session["User_Name"] == null)
            {
                System.Web.HttpContext.Current.Response.Redirect("~/");
            }
            return View();
        }
        public ActionResult Notification()
        {
            if (System.Web.HttpContext.Current.Session["User_Name"] == null)
            {
                System.Web.HttpContext.Current.Response.Redirect("~/");
            }
            return View();
        }
        public ActionResult Order()
        {
            if (System.Web.HttpContext.Current.Session["User_Name"] == null)
            {
                System.Web.HttpContext.Current.Response.Redirect("~/");
            }
            int userid = Convert.ToInt32(Session["User_ID"]);
            var list = db.Orders.Where(m => m.CustemerId == userid).OrderByDescending(m => m.CreateDate).ToList();
            ViewBag.itemOrder = db.Orderdetails.ToList();
            int user_id = Convert.ToInt32(Session["User_ID"]);
            ViewBag.Info = db.Users.Where(m => m.ID == user_id).First();
            ViewBag.productOrder = db.Products.ToList();
            return View(list);
        }
        public ActionResult ActionOrder()
        {
            if (System.Web.HttpContext.Current.Session["User_Name"] == null)
            {
                System.Web.HttpContext.Current.Response.Redirect("~/");
            }
            var list = db.Orders.ToList();
            ViewBag.Hoanthanh = db.Orders.Where(m => m.Status == 3).Count();
            ViewBag.ChoXuLy = db.Orders.Where(m => m.Status == 1).Count();
            ViewBag.DangXuLy = db.Orders.Where(m => m.Status == 2).Count();
            return View("_ActionOrder", list);
        }
        public ActionResult OrderDetails(int id)
        {
            if (System.Web.HttpContext.Current.Session["User_Name"] == null)
            {
                System.Web.HttpContext.Current.Response.Redirect("~/");
            }
            int userid = Convert.ToInt32(Session["User_ID"]);
            var checkO = db.Orders.Where(m => m.CustemerId == userid && m.Id == id);
            if (checkO.Count() == 0)
            {
                return this.NotFound();
            }

            var id_order = db.Orders.Where(m => m.CustemerId == userid && m.Id == id).FirstOrDefault();
            ViewBag.Order = id_order;
            var itemOrder = db.Orderdetails.Where(m => m.OrderId == id_order.Id).ToList();
            ViewBag.productOrder = db.Products.ToList();
            return View(itemOrder);
        }
        public ActionResult NotFound()
        {
            if (System.Web.HttpContext.Current.Session["User_Name"] == null)
            {
                System.Web.HttpContext.Current.Response.Redirect("~/");
            }
            return View();
        }
        [HttpPost]
        public JsonResult Register(MUser user)
        {
            try
            {
                var checkPM = db.Users.Any(m => m.Phone == user.Phone && m.Email.ToLower().Equals(user.Email.ToLower()));
                if (checkPM)
                {
                    return Json(new { Code = 1, Message = "Số điện thoại hoặc Email đã được sử dụng." });
                }
                // Kiểm tra định dạng email
                if (!IsValidEmail(user.Email))
                {
                    return Json(new { Code = 1, Message = "Email không hợp lệ." });
                }

                // Kiểm tra trùng tên truy cập
                if (db.Users.Any(m => m.Name.ToLower().Equals(user.Name.ToLower())))
                {
                    return Json(new { Code = 1, Message = "Tên truy cập đã được sử dụng." });
                }

                // Kiểm tra trùng tên
                if (db.Users.Any(m => m.Phone == user.Phone && m.Email.ToLower().Equals(user.Email.ToLower())))
                {
                    return Json(new { Code = 1, Message = "Số điện thoại hoặc Email đã được sử dụng." });
                }
                user.Gender = 1;
                user.Image = "";
                user.Access = 0;
                user.Status = 1;
                user.Password = MyString.ToMD5(user.Password);
                user.Created_at = DateTime.Now;
                user.Created_by = 1;
                user.Updated_at = DateTime.Now;
                user.Updated_by = 1;

                db.Users.Add(user);
                db.SaveChanges();

                return Json(new { Code = 0, Message = "Đăng ký thành công!" });
            }
            catch (Exception e)
            {
                return Json(new { Code = 1, Message = "Đăng ký thất bại!" });
                throw e;
            }
        }
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            return regex.IsMatch(email);
        }
    }
}