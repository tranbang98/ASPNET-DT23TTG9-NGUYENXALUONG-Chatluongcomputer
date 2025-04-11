
-- =============================================
-- DATABASE: ChatluongComputerDB
-- =============================================
USE ChatluongComputerDB;
GO

-- =============================================
-- STORED PROCEDURE: USERS
-- =============================================

-- Thêm người dùng mới
CREATE PROCEDURE sp_AddUser
    @Username NVARCHAR(50),
    @PasswordHash NVARCHAR(255),
    @FullName NVARCHAR(100),
    @Email NVARCHAR(100),
    @Role NVARCHAR(20)
AS
BEGIN
    INSERT INTO Users (Username, PasswordHash, FullName, Email, Role, CreatedAt)
    VALUES (@Username, @PasswordHash, @FullName, @Email, @Role, GETDATE())
END
GO

-- Cập nhật thông tin người dùng
CREATE PROCEDURE sp_UpdateUser
    @UserId INT,
    @FullName NVARCHAR(100),
    @Email NVARCHAR(100),
    @Role NVARCHAR(20)
AS
BEGIN
    UPDATE Users
    SET FullName = @FullName, Email = @Email, Role = @Role
    WHERE UserId = @UserId
END
GO

-- Xoá người dùng
CREATE PROCEDURE sp_DeleteUser
    @UserId INT
AS
BEGIN
    DELETE FROM Users WHERE UserId = @UserId
END
GO

-- Lấy tất cả người dùng
CREATE PROCEDURE sp_GetAllUsers
AS
BEGIN
    SELECT * FROM Users
END
GO

-- =============================================
-- STORED PROCEDURE: ORDERS
-- =============================================

-- Thêm đơn hàng
CREATE PROCEDURE sp_AddOrder
    @UserId INT,
    @OrderDate DATETIME,
    @TotalAmount DECIMAL(18,2),
    @Status NVARCHAR(20)
AS
BEGIN
    INSERT INTO Orders (UserId, OrderDate, TotalAmount, Status)
    VALUES (@UserId, @OrderDate, @TotalAmount, @Status)
END
GO

-- Cập nhật trạng thái đơn hàng
CREATE PROCEDURE sp_UpdateOrderStatus
    @OrderId INT,
    @Status NVARCHAR(20)
AS
BEGIN
    UPDATE Orders
    SET Status = @Status
    WHERE OrderId = @OrderId
END
GO

-- Xoá đơn hàng
CREATE PROCEDURE sp_DeleteOrder
    @OrderId INT
AS
BEGIN
    DELETE FROM Orders WHERE OrderId = @OrderId
END
GO

-- Lấy tất cả đơn hàng
CREATE PROCEDURE sp_GetAllOrders
AS
BEGIN
    SELECT * FROM Orders
END
GO
