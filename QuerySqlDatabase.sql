-- Create database
CREATE DATABASE OrderFoodDB;
GO

-- Use the newly created database
USE OrderFoodDB;
GO

-- Create Users table
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(200) NOT NULL,
    FullName NVARCHAR(200),
    PhoneNumber NVARCHAR(15),
    Address NVARCHAR(300),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Create Categories table
CREATE TABLE Categories (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500)
);

-- Create Products table with ImageUrl column
CREATE TABLE Products (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    Price DECIMAL(10, 2) NOT NULL,
    ImageUrl NVARCHAR(500),
    CategoryId INT,
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Create Orders table
CREATE TABLE Orders (
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT,
    OrderDate DATETIME DEFAULT GETDATE(),
    TotalPrice DECIMAL(10, 2),
    Status NVARCHAR(50) DEFAULT 'Pending',
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

-- Create OrderDetails table
CREATE TABLE OrderDetails (
    OrderDetailId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT,
    ProductId INT,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10, 2) NOT NULL,
    Size NVARCHAR(50),
    Extras NVARCHAR(MAX),
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

-- Sample data insertion
-- Insert sample users
INSERT INTO Users (UserName, Email, PasswordHash, FullName, PhoneNumber, Address)
VALUES 
('john_doe', 'john@example.com', 'hashedpassword1', 'John Doe', '1234567890', '123 Main St'),
('jane_doe', 'jane@example.com', 'hashedpassword2', 'Jane Doe', '0987654321', '456 Elm St');

-- Insert sample categories
INSERT INTO Categories (CategoryName, Description)
VALUES 
('Beverages', 'Drinks and beverages'),
('Snacks', 'Light snacks and bites'),
('Margherita Pizza', 'Classic Margherita pizza'),
('Pepperoni Pizza', 'Pepperoni pizza with cheese'),
('BBQ Chicken Pizza', 'BBQ chicken pizza'),
('Veggie Pizza', 'Pizza with various vegetables'); -- Added various pizza categories

-- Insert sample products with ImageUrl
INSERT INTO Products (ProductName, Description, Price, ImageUrl, CategoryId)
VALUES 
('Coke', '500ml bottle of Coca-Cola', 1.50, 'https://upload.wikimedia.org/wikipedia/commons/b/bb/Pizza_Vi%E1%BB%87t_Nam_%C4%91%E1%BA%BF_d%C3%A0y%2C_x%C3%BAc_x%C3%ADch_%28SNaT_2018%29_%287%29.jpg', 1),
('Pepsi', '500ml bottle of Pepsi', 1.50, 'https://upload.wikimedia.org/wikipedia/commons/b/bb/Pizza_Vi%E1%BB%87t_Nam_%C4%91%E1%BA%BF_d%C3%A0y%2C_x%C3%BAc_x%C3%ADch_%28SNaT_2018%29_%287%29.jpg', 1),
('Chips', 'Pack of potato chips', 2.00, 'https://upload.wikimedia.org/wikipedia/commons/b/bb/Pizza_Vi%E1%BB%87t_Nam_%C4%91%E1%BA%BF_d%C3%A0y%2C_x%C3%BAc_x%C3%ADch_%28SNaT_2018%29_%287%29.jpg', 2),
('Cookies', 'Pack of chocolate cookies', 3.00, 'https://upload.wikimedia.org/wikipedia/commons/b/bb/Pizza_Vi%E1%BB%87t_Nam_%C4%91%E1%BA%BF_d%C3%A0y%2C_x%C3%BAc_x%C3%ADch_%28SNaT_2018%29_%287%29.jpg', 2),
('Margherita Pizza', 'Classic Margherita pizza', 15.00, 'https://upload.wikimedia.org/wikipedia/commons/a/a3/Eq_it-na_pizza-margherita_sep2005_sml.jpg', 3),
('Pepperoni Pizza', 'Pepperoni pizza with cheese', 18.00, 'https://upload.wikimedia.org/wikipedia/commons/6/6a/Pepperoni_pizza.jpg', 4),
('BBQ Chicken Pizza', 'BBQ chicken pizza', 20.00, 'https://upload.wikimedia.org/wikipedia/commons/4/45/BBQ_Chicken_Pizza.jpg', 5),
('Veggie Pizza', 'Pizza with various vegetables', 17.00, 'https://upload.wikimedia.org/wikipedia/commons/a/a5/Veggie_pizza.jpg', 6);

-- Insert sample orders
INSERT INTO Orders (UserId, TotalPrice)
VALUES 
(1, 5.00),
(2, 4.50);

-- Insert sample order details
INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice, Size, Extras)
VALUES 
(1, 1, 2, 1.50, NULL, NULL), -- Order 1: 2 Cokes
(1, 3, 1, 2.00, NULL, NULL), -- Order 1: 1 Pack of chips
(2, 2, 1, 1.50, NULL, NULL), -- Order 2: 1 Pepsi
(2, 4, 1, 3.00, NULL, NULL); -- Order 2: 1 Pack of cookies

