CREATE DATABASE MotorbikeStoreDB;
USE MotorbikeStoreDB;

CREATE TABLE Motorbikes (
    MotorbikeId INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Brand NVARCHAR(100),
    Description NVARCHAR(255),
    Price DECIMAL(18, 2),
    Stock INT,
    ImageUrl NVARCHAR(255)
);

CREATE TABLE SalesRecords (
    SaleId INT PRIMARY KEY IDENTITY,
    MotorbikeId INT,
    QuantitySold INT,
    SaleDate DATE,
    FOREIGN KEY (MotorbikeId) REFERENCES Motorbikes(MotorbikeId)
);

-- Thêm dữ liệu mẫu vào motorbikes và SalesRecords
INSERT INTO Motorbikes (Name, Brand, Description, Price, Stock, ImageUrl) VALUES ('Exciter 150', 'Yamaha', 'Adrenaline of Speed', 45000000.00, 100, 'exciter150.png');
INSERT INTO Motorbikes (Name, Brand, Description, Price, Stock, ImageUrl) VALUES ('Winner150', 'Honda', 'Become a winner', 38000000.00, 50, 'winner150.png');
INSERT INTO Motorbikes (Name, Brand, Description, Price, Stock, ImageUrl) VALUES ('Satria150', 'Suzuki', 'Max speed, max ga', 48000000.00, 150, 'satria150.png');

INSERT INTO SalesRecords (MotorbikeId, QuantitySold, SaleDate) VALUES (1, 25, '2024-10-01');
INSERT INTO SalesRecords (MotorbikeId, QuantitySold, SaleDate) VALUES (2, 30, '2024-10-02');
INSERT INTO SalesRecords (MotorbikeId, QuantitySold, SaleDate) VALUES (3, 20, '2024-10-03');
INSERT INTO SalesRecords (MotorbikeId, QuantitySold, SaleDate) VALUES (2, 25, '2024-12-03');

DELETE FROM Motorbikes
WHERE Brand = 'Brand C';


