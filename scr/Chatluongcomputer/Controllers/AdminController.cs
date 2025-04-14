using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chatluongcomputer.Models;
namespace Chatluongcomputer.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        private ChatluongComputerDBEntities db = new ChatluongComputerDBEntities();


        public ActionResult Index()
        {
            var list = db.Users.ToList(); // đảm bảo bạn đang truy vấn toàn bộ User
            ViewBag.Roles = GetRoles();
            return View(list);             // phải trả danh sách về View
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            var allowedRoles = GetRoles();

            if (!allowedRoles.Any(r => r.Value == user.Role))
            {
                TempData["Message"] = "❌ Vai trò không hợp lệ.";
                ViewBag.Roles = allowedRoles;
                return View(user);
            }

            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                TempData["Message"] = "✅ Tạo người dùng thành công!";
                return RedirectToAction("Index");
            }

            TempData["Message"] = "❌ Tạo thất bại. Vui lòng kiểm tra lại.";
            ViewBag.Roles = allowedRoles;
            return View(user);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            ViewBag.Roles = GetRoles();
            return View();
        }





        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.Roles = GetRoles();
            return View(user); // Truyền model vào View
        }
        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.Users.Find(model.UserId);
                if (existingUser == null)
                {
                    return HttpNotFound();
                }

                // Cập nhật thủ công
                existingUser.FullName = model.FullName;
                existingUser.Email = model.Email;
                existingUser.Role = model.Role;

                db.SaveChanges();

                return Json(new { success = true });
            }

            ViewBag.Roles = GetRoles();
            return PartialView(model);
        }
        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            var user = db.Users.Find(id); // Lấy user theo ID

            if (user == null)
            {
                return HttpNotFound(); // Trả về lỗi nếu không tìm thấy
            }

            return View(user); // Trả dữ liệu về cho View
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                return PartialView("Delete", user);
            }
            catch (Exception ex)
            {
                return Content("⚠️ Lỗi khi tải thông tin: " + ex.Message);
            }
        }



        // POST: Admin/DeleteConfirmed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? UserId)
        {
            if (UserId == null)
            {
                return Json(new { success = false, message = "⚠️ Thiếu mã người dùng." });
            }

            var user = db.Users.Find(UserId);
            if (user == null)
            {
                return Json(new { success = false, message = "❌ Người dùng không tồn tại." });
            }

            try
            {
                db.Users.Remove(user);
                db.SaveChanges();
                return Json(new { success = true, message = "🗑️ Đã xóa người dùng." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "❌ Xóa thất bại: " + ex.Message });
            }
        }



        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            // TODO: Xử lý đăng nhập
            if (username == "admin" && password == "123") // Demo
            {
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Sai tài khoản hoặc mật khẩu";
            return View();
        }

        // GET: Admin/ManageUsers
        public ActionResult ManageUsers()
        {
            return View();
        }
        private List<SelectListItem> GetRoles()
        {
            return new List<SelectListItem>
    {
        new SelectListItem { Text = "Nhân viên bán hàng", Value = "nhanvien_banhang" },
        new SelectListItem { Text = "Nhân viên kho", Value = "nhanvien_kho" },
        new SelectListItem { Text = "Kế toán", Value = "nhanvien_ketoan" },
        new SelectListItem { Text = "Content", Value = "nhanvien_content" },
        new SelectListItem { Text = "Chăm sóc khách hàng", Value = "nhanvien_cs" },
        new SelectListItem { Text = "Giám đốc", Value = "giamdoc" }
    };
        }
    }

}
