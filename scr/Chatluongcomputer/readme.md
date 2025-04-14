## 📂 Cấu trúc thư mục dự án bán linh kiện

/Controllers
├── HomeController.cs         ← Front-end
├── AdminController.cs        ← Back-end (quản trị)
├── CustomerController.cs     ← Back-end (người dùng)
└── ProductController.cs      ← Dùng chung (sản phẩm)

/Views/
├── 📂 Shared                      ← Layout & thành phần dùng chung
│   ├── _Layout.cshtml            ← Giao diện layout chính (frontend)
│   ├── _AdminLayout.cshtml       ← Giao diện layout riêng cho Admin
│   ├── _Header.cshtml            ← Phần header dùng chung
│   └── _Footer.cshtml            ← Phần footer dùng chung

├── 📂 Home                        ← Giao diện người dùng
│   ├── Index.cshtml              ← Trang chủ
│   └── About.cshtml              ← Giới thiệu

├── 📂 Admin                       ← Giao diện quản trị
│   ├── Login.cshtml              ← Giao diện đăng nhập admin
│   ├── Index.cshtml              ← Trang dashboard tổng quan
│   └── ManageUsers.cshtml        ← Quản lý danh sách người dùng

├── 📂 Product                    ← Giao diện sản phẩm
│   ├── List.cshtml               ← Danh sách sản phẩm
│   └── Detail.cshtml             ← Chi tiết sản phẩm

├── 📂 Customer                   ← Giao diện khách hàng (nếu có)
│   ├── Profile.cshtml            ← Thông tin cá nhân
│   └── MyOrders.cshtml           ← Danh sách đơn hàng đã mua

├── 📂 Order                      ← Giao diện đặt hàng / thanh toán
│   └── Checkout.cshtml           ← Thanh toán đơn hàng

/Models/
├── 📂 Người dùng & Phân quyền
│   └── User.cs               ← Người dùng

├── 📂 Sản phẩm & Danh mục
│   ├── Product.cs            ← Sản phẩm
│   ├── Category.cs           ← Danh mục sản phẩm
│   └── ProductCategory.cs    ← Liên kết nhiều-nhiều giữa Product và Category

├── 📂 Đơn hàng & Chi tiết
│   ├── Order.cs              ← Đơn hàng
│   ├── OrderDetail.cs        ← Chi tiết đơn hàng
│   └── TrackingCode.cs       ← Mã vận chuyển (tracking đơn hàng)

├── 📂 Đánh giá & SEO
│   ├── Review.cs             ← Đánh giá sản phẩm
│   └── SEOInfo.cs            ← Thông tin SEO (slug, meta...)

├── 📂 Giao hàng & Thanh toán
│   ├── ShippingInfo.cs       ← Giao hàng (địa chỉ, trạng thái...)
│   └── Payment.cs            ← Thanh toán (lịch sử hoặc trạng thái)

├── 📂 Mã khuyến mãi
│   └── Discount.cs           ← Mã giảm giá

├── 📂 Khác
│   └── MyDbContext.cs        ← DbContext chính của toàn hệ thống (Entity Framework)



/App_Start
└── RouteConfig.cs           ← Định tuyến (routing)

/Content
├── /CSS                     ← Giao diện
├── /JS
└── /Images

/Scripts
├── jquery.js
└── bootstrap.js

Global.asax                 ← Tập tin khởi tạo
Web.config                 ← Cấu hình chính
Views/Web.config           ← Cấu hình riêng cho Razor



Chú ý:
SQL80001: Incorrect syntax: 'CREATE PROCEDURE' must be the only statement in the batch.
Nguyên nhân: SQL Server Management Studio (SSMS) hoặc Visual Studio yêu cầu mỗi 
CREATE PROCEDURE phải đứng riêng biệt trong một batch.


## Yêu cầu để chạy dự án

### Cài đặt phần mềm cần thiết:
- [.NET SDK] 
- [SQL Server Express hoặc LocalDB]
- [SQL Server Management Studio (SSMS)](https://aka.ms/ssms) để quản lý CSDL

### Cách khởi tạo cơ sở dữ liệu:
1. Mở SSMS và kết nối tới SQL Server (mặc định: `localhost\SQLEXPRESS`)
2. Chạy file `Database/ChatluongComputer.sql` để tạo database & bảng.
3. Chạy tiếp file `Database/ChatluongComputer_SP_User_Order.sql` để tạo Stored Procedures.
1. Tài khoản mặc định:
   - Tên đăng nhập: `admin`
   - Mật khẩu: `123456`
   - Quyền: `Admin` (quản trị viên)