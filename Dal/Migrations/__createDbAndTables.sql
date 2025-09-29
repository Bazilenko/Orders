
CREATE DATABASE OrderDB;
GO

USE OrderDB;
GO

CREATE TABLE Customers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE
);
GO

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
GO

CREATE TABLE OrderDishes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    DishId INT NOT NULL,
    Quantity INT NOT NULL DEFAULT 1,
    PriceAtTimeOfOrder DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE
);
GO


CREATE TABLE Payments (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',
    PaymentMethod NVARCHAR(50) NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(Id)
);
GO


CREATE INDEX IX_Orders_CustomerId ON Orders(CustomerId);
CREATE INDEX IX_Orders_RestaurantId ON Orders(RestaurantId);
CREATE INDEX IX_Orders_Status ON Orders(Status);
CREATE INDEX IX_OrderDishes_OrderId ON OrderDishes(OrderId);
CREATE INDEX IX_Payments_OrderId ON Payments(OrderId);
GO
