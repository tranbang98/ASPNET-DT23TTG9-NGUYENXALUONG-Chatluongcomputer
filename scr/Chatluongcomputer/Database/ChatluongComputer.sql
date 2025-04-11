
-- Tạo CSDL
CREATE DATABASE ChatluongComputerDB;
GO

USE ChatluongComputerDB;
GO

-- Tạo bảng Users
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(50) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    FullName NVARCHAR(100),
    Email NVARCHAR(100),
    Role NVARCHAR(20), -- "Admin", "Customer"
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Tạo bảng Products
CREATE TABLE Products (
    ProductId INT PRIMARY KEY IDENTITY,
    ProductName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    Price DECIMAL(18, 2),
    Quantity INT,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Tạo bảng Orders
CREATE TABLE Orders (
    OrderId INT PRIMARY KEY IDENTITY,
    UserId INT FOREIGN KEY REFERENCES Users(UserId),
    OrderDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(18, 2),
    Status NVARCHAR(50) -- e.g. "Pending", "Shipped", "Delivered"
);

-- Tạo bảng Reviews
CREATE TABLE Reviews (
    ReviewId INT PRIMARY KEY IDENTITY,
    UserId INT FOREIGN KEY REFERENCES Users(UserId),
    ProductId INT FOREIGN KEY REFERENCES Products(ProductId),
    Rating INT CHECK(Rating BETWEEN 1 AND 5),
    Comment NVARCHAR(MAX),
    ReviewDate DATETIME DEFAULT GETDATE()
);

-- Tạo bảng ShippingInfo
CREATE TABLE ShippingInfo (
    ShippingId INT PRIMARY KEY IDENTITY,
    OrderId INT FOREIGN KEY REFERENCES Orders(OrderId),
    Address NVARCHAR(255),
    Phone NVARCHAR(20),
    ShippingMethod NVARCHAR(50)
);

-- Tạo bảng Discount
CREATE TABLE Discount (
    DiscountId INT PRIMARY KEY IDENTITY,
    Code NVARCHAR(50),
    Description NVARCHAR(MAX),
    Percentage DECIMAL(5, 2),
    ExpiryDate DATETIME
);

-- Tạo bảng SEOInfo
CREATE TABLE SEOInfo (
    SEOId INT PRIMARY KEY IDENTITY,
    ProductId INT FOREIGN KEY REFERENCES Products(ProductId),
    MetaTitle NVARCHAR(150),
    MetaDescription NVARCHAR(300),
    MetaKeywords NVARCHAR(300)
);

-- Tạo bảng TrackingCode
CREATE TABLE TrackingCode (
    TrackingId INT PRIMARY KEY IDENTITY,
    OrderId INT FOREIGN KEY REFERENCES Orders(OrderId),
    Carrier NVARCHAR(50),
    TrackingNumber NVARCHAR(100),
    Status NVARCHAR(50)
);

-- Tạo SP thêm sản phẩm
GO
CREATE PROCEDURE usp_AddProduct
    @ProductName NVARCHAR(100),
    @Description NVARCHAR(MAX),
    @Price DECIMAL(18, 2),
    @Quantity INT
AS
BEGIN
    INSERT INTO Products (ProductName, Description, Price, Quantity)
    VALUES (@ProductName, @Description, @Price, @Quantity);
END;

-- Tạo SP cập nhật sản phẩm
GO
CREATE PROCEDURE usp_UpdateProduct
    @ProductId INT,
    @ProductName NVARCHAR(100),
    @Description NVARCHAR(MAX),
    @Price DECIMAL(18, 2),
    @Quantity INT
AS
BEGIN
    UPDATE Products
    SET ProductName = @ProductName,
        Description = @Description,
        Price = @Price,
        Quantity = @Quantity
    WHERE ProductId = @ProductId;
END;

-- Tạo SP xóa sản phẩm
GO
CREATE PROCEDURE usp_DeleteProduct
    @ProductId INT
AS
BEGIN
    DELETE FROM Products WHERE ProductId = @ProductId;
END;

-- Tạo SP lấy danh sách sản phẩm
GO
CREATE PROCEDURE usp_GetAllProducts
AS
BEGIN
    SELECT * FROM Products;
END;
