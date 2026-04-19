-- 🔹 Створення БД, якщо не існує
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'ordersDb')
BEGIN
    CREATE DATABASE ordersDb;
END

USE ordersDb;

-- 🔹 Customers
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Customers')
BEGIN
    CREATE TABLE Customers (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Email NVARCHAR(255) NOT NULL UNIQUE
    );
END


-- 🔹 Orders
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Orders')
BEGIN
    CREATE TABLE Orders (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        CustomerId INT NOT NULL,
        RestaurantId INT NOT NULL,
        OrderDate DATETIME2 DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME2 DEFAULT GETUTCDATE(),
        Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',
        TotalAmount DECIMAL(10,2) NOT NULL DEFAULT 0,
        FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
    );
END


-- 🔹 OrderDishes
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'OrderDishes')
BEGIN
    CREATE TABLE OrderDishes (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        OrderId INT NOT NULL,
        DishId INT NOT NULL,
        Quantity INT NOT NULL DEFAULT 1,
        PriceAtTimeOfOrder DECIMAL(10,2) NOT NULL,
        FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE
    );
END


-- 🔹 Payments
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Payments')
BEGIN
    CREATE TABLE Payments (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        OrderId INT NOT NULL,
        Amount DECIMAL(10,2) NOT NULL,
        Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',
        PaymentMethod NVARCHAR(50) NOT NULL,
        FOREIGN KEY (OrderId) REFERENCES Orders(Id)
    );
END


-- 🔹 Індекси (створюємо тільки якщо нема)
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Orders_CustomerId')
    CREATE INDEX IX_Orders_CustomerId ON Orders(CustomerId);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Orders_RestaurantId')
    CREATE INDEX IX_Orders_RestaurantId ON Orders(RestaurantId);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Orders_Status')
    CREATE INDEX IX_Orders_Status ON Orders(Status);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_OrderDishes_OrderId')
    CREATE INDEX IX_OrderDishes_OrderId ON OrderDishes(OrderId);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Payments_OrderId')
    CREATE INDEX IX_Payments_OrderId ON Payments(OrderId);
