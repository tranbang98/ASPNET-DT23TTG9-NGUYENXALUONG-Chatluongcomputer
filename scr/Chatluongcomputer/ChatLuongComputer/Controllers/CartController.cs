using ChatLuongComputer.Library;
using ChatLuongComputer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using System.Text;
using System.Data.Entity;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using QRCoder;
namespace ChatLuongComputer.Controllers
{
    public class CartController : Controller
    {
        private ChatLuongComputerDbContext db = new ChatLuongComputerDbContext();



        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(int pid, int qty)
        {

            // 
            var p = db.Products.First(m => m.Status == 1 && m.ID == pid);

            var cart = Session["Cart"];

            if (cart == null)
            {
                var item = new ModelCart();
                item.ProductID = p.ID;
                item.Name = p.Name;
                item.Slug = p.Slug;
                item.Image = p.Image;
                item.Quantity = qty;
                item.Price = p.ProPrice;
                var list = new List<ModelCart>();
                list.Add(item);

                Session["Cart"] = list;
                return Json(new { result = 1 });
            }
            else
            {
                var list = (List<ModelCart>)cart;

                if (list.Exists(m => m.ProductID == pid))
                {
                    foreach (var item in list)
                    {
                        if (item.ProductID == pid)
                            item.Quantity += qty;
                        return Json(new { result = 2 });
                    }
                }
                else

                {
                    var item = new ModelCart();
                    item.ProductID = p.ID;
                    item.Name = p.Name;
                    item.Slug = p.Slug;
                    item.Image = p.Image;
                    item.Quantity = qty;
                    item.Price = p.ProPrice;
                    list.Add(item);
                    return Json(new { result = 1 });
                }
            }
            return Json(new { result = 0 });
        }

        public JsonResult Update(int pid, String option)
        {
            var sCart = (List<ModelCart>)Session["Cart"];
            ModelCart c = sCart.First(m => m.ProductID == pid);
            if (c != null)
            {
                switch (option)
                {
                    case "add":
                        c.Quantity++;
                        return Json(1);
                    case "minus":
                        c.Quantity--;
                        return Json(2);
                    case "remove":
                        sCart.Remove(c);
                        if (sCart.Count() == 0)
                            Session.Remove("Cart");
                        return Json(3);
                    default:
                        break;
                }
            }
            return Json(null);
        }
        public ActionResult RemoveAll()
        {
            Session.Remove("Cart");
            Notification.set_flash("Đã xóa toàn bộ sản phẩm trong giỏ hàng!", "success");
            return Redirect("~/gio-hang");
        }
        public ActionResult Checkout()
        {
            if (Session["User_Name"] != null && Session["Cart"] != null)
            {
                int user_id = Convert.ToInt32(Session["User_ID"]);
                ViewBag.Info = db.Users.Where(m => m.ID == user_id).First();
            }
            else
                return RedirectToAction("Index", "Cart");
            return View();
        }

        [HttpPost]
        public JsonResult Payment(String Email, String Address, String FullName, String Phone)
        {
            var order = new MOrder();
            int user_id = Convert.ToInt32(Session["User_ID"]);
            order.Code = DateTime.Now.ToString("yyyyMMddhhMMss"); // yyyy-MM-dd hh:MM:ss
            order.CustemerId = user_id;
            order.CreateDate = DateTime.Now;
            order.DeliveryAddress = Address;
            order.DeliveryEmail = Email;
            order.DeliveryPhone = Phone;
            order.DeliveryName = FullName;
            order.Status = 1;
            db.Orders.Add(order);
            db.SaveChanges();

            var OrderID = order.Id;

            foreach (var c in (List<ModelCart>)Session["Cart"])
            {
                var orderdetails = new MOrderdetail();
                orderdetails.OrderId = OrderID;
                orderdetails.ProductId = c.ProductID;
                orderdetails.Price = c.Price;
                orderdetails.Quantity = c.Quantity;
                orderdetails.Amount = c.Price * c.Quantity;
                db.Orderdetails.Add(orderdetails);
                db.SaveChanges();
            }
            if (Session["User_Name"] != null && Session["Cart"] != null)
            {
                int userid = Convert.ToInt32(Session["User_ID"]);
                var user = db.Users.FirstOrDefault(m => m.ID == userid);
                if (user != null)
                {
                    ViewBag.Info = user;
                    string userEmail = user.Email;
                    SendOrderConfirmationEmail(userEmail); //Gửi mail cho khách hàng
                }
            }
            Session.Remove("Cart");
            Notification.set_flash("Bạn đã đặt hàng thành công!", "success");
            return Json(true);

        }
        public void SendOrderConfirmationEmail(String Mail)
        {
            string subject = "Xác nhận đặt hàng";
            string body = "<html><body style='font-family: Arial, sans-serif; background-color: #fcffb0;'>";
            body += "<table style='margin: auto; border-collapse: collapse; width: 100%; background-color: #ffffff; border: 1px solid #ddd;'>";
            body += "<tr><td>";
            body += "<div style='text-align: center; margin: 10px;background-color: #c2ffc2'><img src='http://campro.somee.com/Public/user/img/campro.png'></div>";
            body += "<h2 style='text-align: center; color: #ffffff;'>Xác nhận đơn hàng</h2>";  // Màu xanh lá cây
            int userid = Convert.ToInt32(Session["User_ID"]);
            var user = db.Users.FirstOrDefault(m => m.ID == userid);
            body += "<p style='text-align: center'>Xin chào " + user.FullName + ", đơn hàng của bạn đã được giao cho đơn vị vận chuyển.</p>";
            body += "<h3 style='color: #4CAF50;text-align: center'>THÔNG TIN ĐƠN HÀNG - DÀNH CHO NGƯỜI MUA</h3>";  // Màu xanh lá cây
            body += "<table style='border-collapse: collapse; width: 100%;'>";
            body += "<tr style='background-color: #f5f5f5; border: 1px solid #ddd;'><th style='padding: 8px; text-align: left;'>Tên sản phẩm</th><th style='padding: 8px; text-align: left;'>Số lượng</th><th style='padding: 8px; text-align: left;'>Giá</th></tr>";
            // Lấy danh sách sản phẩm từ Session và thêm vào bảng
            var cart = (List<ModelCart>)Session["Cart"];
            foreach (var item in cart)
            {
                body += "<tr><td style='padding: 8px; border: 1px solid #ddd;'>" + item.Name + "</td><td style='padding: 8px; border: 1px solid #ddd;'>" + item.Quantity + "</td><td style='padding: 8px; border: 1px solid #ddd;'>" + item.Price + "</td></tr>";
            }
            body += "</table>";
            body += "<p style='margin: 10px; font-style: italic; color: #999;'>Đây là tin nhắn được gửi tự động từ hệ thống, vui lòng không trả lời tin nhắn này.</p>";
            body += "<div style='text-align: center; margin-top: 20px;'><a href='https://campro.somee.com' style='display: inline-block; padding: 10px 20px; background-color: #4CAF50; color: #fff; text-decoration: none; border-radius: 5px;'>Kiểm tra đơn hàng</a></div>";
            body += "</td></tr></table>";
            body += "<div style='padding: 20px; text-align: center; background-color: #f5f5f5;'>";
            body += "<p>Hãy mua sắm cùng Shopee</p>";
            body += "<a href='https://campro.somee.com'><img src='https://paragon.com.vn/wp-content/uploads/2021/06/Ban-sao-cua-Ban-sao-cua-Ban-sao-cua-Khong-co-tieu-de-1-2-1024x320.png' alt='Google Play' style='height: 40px; margin: 0 5px;'></a>";
            body += "<a href='https://campro.somee.com'><img src='https://w7.pngwing.com/pngs/34/523/png-transparent-app-store-apple-logo-apple-text-logo-video-game.png' alt='App Store' style='height: 40px; margin: 0 5px;'></a>";
            body += "</div>";
            body += "</body></html>";


            // Tạo đối tượng MailMessage
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("Your Mail");
            mailMessage.To.Add(Mail);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            // Gửi email sử dụng SmtpClient
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential("Your Mail", "Pass SMTP");
            smtpClient.EnableSsl = true;
            // Gửi email
            smtpClient.Send(mailMessage);
        }

        public JsonResult Tesst()
        {
            if (Session["User_Name"] != null)
                return Json(1);
            return Json(0);
        }
        public JsonResult CheckAuth()
        {
            if (Session["User_Name"] != null)
                return Json(1);
            return Json(0);
        }
    }
}