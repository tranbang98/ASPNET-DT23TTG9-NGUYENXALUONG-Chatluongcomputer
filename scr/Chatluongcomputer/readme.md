## 📂 Cấu trúc thư mục dự án bán linh kiện

/Controllers
├── HomeController.cs         ← Front-end
├── AdminController.cs        ← Back-end (quản trị)
├── CustomerController.cs     ← Back-end (người dùng)
└── ProductController.cs      ← Dùng chung (sản phẩm)

/Views
├── /Shared                   ← Layout & thành phần dùng chung
│   ├── _Layout.cshtml            ← Layout cho front-end
│   ├── _AdminLayout.cshtml       ← Layout cho admin
│   ├── _Header.cshtml            ← Header chung
│   └── _Footer.cshtml            ← Footer chung
│
├── /Home                     ← Giao diện người dùng
│   ├── Index.cshtml              ← Trang chủ
│   └── About.cshtml              ← Giới thiệu
│
├── /Admin                    ← Giao diện quản trị
│   ├── Login.cshtml              ← Đăng nhập
│   ├── Index.cshtml              ← Dashboard
│   └── ManageUsers.cshtml        ← Quản lý người dùng
│
└── /Product                  ← Giao diện sản phẩm
    ├── List.cshtml               ← Danh sách sản phẩm
    └── Detail.cshtml             ← Chi tiết sản phẩm

/Models
├── User.cs                  ← Người dùng
├── Product.cs               ← Sản phẩm
├── Order.cs                 ← Đơn hàng
├── Review.cs                ← Đánh giá
├── ShippingInfo.cs          ← Giao hàng
├── Discount.cs              ← Mã giảm giá
├── SEOInfo.cs               ← Thông tin SEO
└── TrackingCode.cs          ← Mã vận chuyển

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