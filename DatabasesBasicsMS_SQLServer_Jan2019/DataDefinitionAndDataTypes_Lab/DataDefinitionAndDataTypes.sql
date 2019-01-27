
--Problem 13.	Movies Database
CREATE DATABASE Movies

CREATE TABLE Directors(
	Id INT  PRIMARY KEY NOT NULL,
	DirectorName VARCHAR(30) NOT NULL,
	Notes NVARCHAR(MAX)
)

INSERT INTO Directors(Id, DirectorName)
VALUES(1,'Todor'),
(2,'Gosho'),
(3,'Pesho'),
(4,'Misho'),
(5,'Nik')

CREATE TABLE Genres(
	Id INT NOT NULL PRIMARY KEY,
	GenreName VARCHAR(30) NOT NULL,
	Notes NVARCHAR(MAX)
)

INSERT INTO Genres(Id, GenreName)
VALUES(1,'horror'),
(2,'comedy'),
(3,'action'),
(4,'historical'),
(5,'documentary')

CREATE TABLE Categories(
	Id INT PRIMARY KEY NOT NULL,
	CategoryName VARCHAR(30) NOT NULL,
	Notes NVARCHAR(MAX)
)

INSERT INTO Categories(Id, CategoryName)
VALUES(1,'category one'),
(2,'category two'),
(3,'category three'),
(4,'category four'),
(5,'category five')


CREATE TABLE Movies(
	Id INT  PRIMARY KEY NOT NULL,
	Title VARCHAR(100) NOT NULL,
	DirectorId INT FOREIGN KEY REFERENCES Directors(Id),
	CopyrightYear DATETIME2 ,
	[Length] TIME ,
	GenreId INT FOREIGN KEY REFERENCES Genres(Id),
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id),
	Rating INT ,
	Notes NVARCHAR(MAX)
)

--ALTER TABLE Movies
--ALTER COLUMN Rating INT NULL

INSERT INTO Movies(Id, Title, DirectorId, GenreId, CategoryId)
VALUES(1, 'Title 1', 1, 1, 1),
(2, 'Title 2', 1, 2, 3),
(3, 'Title 3', 2, 5, 4),
(4, 'Title 4', 3, 1, 2),
(5, 'Title 5', 3, 4, 5)

--Problem 14.	Car Rental Database
CREATE DATABASE CarRental 

CREATE TABLE Categories(
	Id INT PRIMARY KEY  NOT NULL,
	CategoryName VARCHAR(30),
	DailyRate    DECIMAL(10, 2) NOT NULL,
    WeeklyRate   DECIMAL(10, 2) NOT NULL,
    MonthlyRate  DECIMAL(10, 2) NOT NULL,
    WeekendRate  DECIMAL(10, 2) NOT NULL
)

INSERT INTO Categories(Id, CategoryName, DailyRate, WeeklyRate, MonthlyRate, WeekendRate)
VALUES(1, 'FirstCategoryName',10,20,30,40),
(2, 'SecondCategoryName',20,23,42,10),
(3, 'ThirdCategoryName', 100,42,23,55)

CREATE TABLE Cars(
	Id INT PRIMARY KEY NOT NULL,
	PlateNumber VARCHAR(10) NOT NULL,
	Manufacturer VARCHAR(50) NOT NULL,
	Model VARCHAR(30) NOT NULL,
	CarYear INT NOT NULL,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
	Doors TINYINT NOT NULL,
	Picture VARBINARY(MAX),
	Condition VARCHAR(30),
	Available BIT DEFAULT 1
)

INSERT INTO Cars(Id, PlateNumber, Manufacturer, Model, CarYear, CategoryId, Doors, Available)
VALUES(1,'PLATENUM1', 'MANUFACTURER1', 'MODEL1', 1, 2, 4,1),
(2,'PLATENUM2', 'MANUFACTURER2', 'MODEL2', 2, 1, 4,0),
(3,'PLATENUM3', 'MANUFACTURER3', 'MODEL3', 3, 3, 1,1)

CREATE TABLE Employees(
	Id INT PRIMARY KEY NOT NULL,
	FirstName VARCHAR(20) NOT NULL,
	LastName VARCHAR(20) NOT NULL,
	Title VARCHAR(20),
	Notes NVARCHAR(MAX)
)

INSERT INTO Employees(Id, FirstName, LastName, Title)
VALUES(1, 'NAME1','FAMILY1','TECHNICIAN1'),
(2, 'NAME2','FAMILY2','TECHNICIAN2'),
(3, 'NAME3','FAMILY3','TECHNICIAN3')

CREATE TABLE Customers(
	Id INT PRIMARY KEY NOT NULL,
	DriverLicenceNumber VARCHAR(30) UNIQUE NOT NULL,
	FullName VARCHAR(50) NOT NULL,
	Address VARCHAR(100),
	City VARCHAR(30),
	ZIPCode VARCHAR(30),
	Notes NVARCHAR(MAX)
)

INSERT INTO Customers(Id, DriverLicenceNumber, FullName)
VALUES(1, 'DRIVERlICENSNUMBER_1','FULLNAME_1'),
(2, 'DRIVERlICENSNUMBER_2','FULLNAME_2'),
(3, 'DRIVERlICENSNUMBER_3','FULLNAME_3')

CREATE TABLE RentalOrders(
	Id INT PRIMARY KEY NOT NULL,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id) NOT NULL,
	CustomerId INT FOREIGN KEY REFERENCES Customers(Id) NOT NULL,
	CarId INT FOREIGN KEY REFERENCES Cars(Id) NOT NULL,
	TankLevel NUMERIC(4,2),
	KilometrageStart INT,
	KilometrageEnd INT,
	TotalKilometrage INT,
	StartDate DATE NOT NULL,
	EndDate DATE NOT NULL,
	TotalDays INT NOT NULL,
	RateApplied DECIMAL(10, 2),
    TaxRate DECIMAL(10, 2),
    OrderStatus NVARCHAR(50),
    NOTES NVARCHAR(MAX)
)

ALTER TABLE RentalOrders
ADD CONSTRAINT CK_TotalDays CHECK(DATEDIFF(DAY, StartDate, EndDate) = TotalDays);


INSERT INTO RentalOrders(Id, EmployeeId, CustomerId, CarId, StartDate,EndDate, TotalDays)
VALUES(1, 1, 1, 1,'01-01-2010', '01-10-2010', 9),
(2, 2, 2, 2,'01-01-2010', '01-9-2010', 8),
(3, 3, 3, 3,'01-01-2010', '01-8-2010', 7)

--Problem 15.	Hotel Database
CREATE DATABASE Hotel

CREATE TABLE Employees
(
             Id        INT
             PRIMARY KEY NOT NULL,
             FirstName NVARCHAR(50) NOT NULL,
             LastName  NVARCHAR(50) NOT NULL,
             Title     NVARCHAR(255) NOT NULL,
             Notes     NVARCHAR(MAX)
);

INSERT INTO Employees(Id,
                      FirstName,
                      LastName,
                      Title
                     )
VALUES
(
       1,
       'First',
       'Employee',
       'Manager'
),
(
       2,
       'Second',
       'Employee',
       'Manager'
),
(
       3,
       'Third',
       'Employee',
       'Manager'
);

CREATE TABLE Customers
(
             AccountNumber   INT
             PRIMARY KEY NOT NULL,
             FirstName       NVARCHAR(50) NOT NULL,
             LastName        NVARCHAR(50) NOT NULL,
             PhoneNumber     VARCHAR(50),
             EmergencyName   NVARCHAR(50) NOT NULL,
             EmergencyNumber INT NOT NULL,
             Notes           NVARCHAR(50)
);

INSERT INTO Customers(AccountNumber,
                      FirstName,
                      LastName,
                      EmergencyName,
                      EmergencyNumber
                     )
VALUES
(
       1,
       'First',
       'Customer',
       'Em1',
       11111
),
(
       2,
       'Second',
       'Customer',
       'Em2',
       22222
),
(
       3,
       'Third',
       'Customer',
       'Em3',
       33333
);

CREATE TABLE RoomStatus
(
             RoomStatus NVARCHAR(50)
             PRIMARY KEY NOT NULL,
             Notes      NVARCHAR(MAX)
);

INSERT INTO RoomStatus(RoomStatus)
VALUES
(
       'Free'
),
(
       'In use'
),
(
       'Reserved'
);

CREATE TABLE RoomTypes
(
             RoomType NVARCHAR(50)
             PRIMARY KEY NOT NULL,
             Notes    NVARCHAR(MAX)
);

INSERT INTO RoomTypes(RoomType)
VALUES
(
       'Luxory'
),
(
       'Casual'
),
(
       'Misery'
);

CREATE TABLE BedTypes
(
             BedType NVARCHAR(50)
             PRIMARY KEY NOT NULL,
             Notes   NVARCHAR(MAX)
);

INSERT INTO BedTypes(BedType)
VALUES
(
       'Single'
),
(
       'Double'
),
(
       'King'
);

CREATE TABLE Rooms
(
             RoomNumber INT
             PRIMARY KEY NOT NULL,
             RoomType   NVARCHAR(50) NOT NULL,
             BedType    NVARCHAR(50) NOT NULL,
             Rate       DECIMAL(10, 2) NOT NULL,
             RoomStatus NVARCHAR(50) NOT NULL,
             Notes      NVARCHAR(MAX)
);

INSERT INTO Rooms(RoomNumber,
                  RoomType,
                  BedType,
                  Rate,
                  RoomStatus
                 )
VALUES
(
       1,
       'Luxory',
       'King',
       100,
       'Reserved'
),
(
       2,
       'Casual',
       'Double',
       50,
       'In use'
),
(
       3,
       'Misery',
       'Single',
       19,
       'Free'
);

CREATE TABLE Payments
(
             Id                INT
             PRIMARY KEY NOT NULL,
             EmployeeId        INT NOT NULL,
             PaymentDate       DATE NOT NULL,
             AccountNumber     INT NOT NULL,
             FirstDateOccupied DATE NOT NULL,
             LastDateOccupied  DATE NOT NULL,
             TotalDays         INT NOT NULL,
             AmountCharged     DECIMAL(10, 2) NOT NULL,
             TaxRate           DECIMAL(10, 2) NOT NULL,
             TaxAmount         DECIMAL(10, 2) NOT NULL,
             PaymentTotal      DECIMAL(10, 2) NOT NULL,
             Notes             NVARCHAR(MAX)
);

ALTER TABLE Payments
ADD CONSTRAINT CK_TotalDays CHECK(DATEDIFF(DAY, FirstDateOccupied, LastDateOccupied) = TotalDays);

ALTER TABLE Payments
ADD CONSTRAINT CK_TaxAmount CHECK(TaxAmount = TotalDays * TaxRate);

INSERT INTO Payments(Id,
                     EmployeeId,
                     PaymentDate,
                     AccountNumber,
                     FirstDateOccupied,
                     LastDateOccupied,
                     TotalDays,
                     AmountCharged,
                     TaxRate,
                     TaxAmount,
                     PaymentTotal
                    )
VALUES
(
       1,
       1,
       '10-05-2015',
       1,
       '10-05-2015',
       '10-10-2015',
       5,
       75,
       50,
       250,
       75
),
(
       2,
       3,
       '10-11-2015',
       1,
       '12-15-2015',
       '12-25-2015',
       10,
       100,
       50,
       500,
       100
),
(
       3,
       2,
       '12-23-2015',
       1,
       '12-23-2015',
       '12-24-2015',
       1,
       75,
       75,
       75,
       75
);

CREATE TABLE Occupancies
(
             Id            INT
             PRIMARY KEY NOT NULL,
             EmployeeId    INT NOT NULL,
             DateOccupied  DATE NOT NULL,
             AccountNumber INT NOT NULL,
             RoomNumber    INT NOT NULL,
             RateApplied   DECIMAL(10, 2),
             PhoneCharge   VARCHAR(50) NOT NULL,
             Notes         NVARCHAR(MAX)
);

INSERT INTO Occupancies(Id,
                        EmployeeId,
                        DateOccupied,
                        AccountNumber,
                        RoomNumber,
                        PhoneCharge
                       )
VALUES
(
       1,
       2,
       '08-24-2012',
       3,
       1,
       '088 88 888 888'
),
(
       2,
       3,
       '06-15-2015',
       2,
       3,
       '088 88 555 555'
),
(
       3,
       1,
       '05-12-1016',
       1,
       2,
       '088 88 555 333'
);


--Problem 16.	Create SoftUni Database

CREATE DATABASE SoftUni;

CREATE TABLE Towns(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(50) NOT NULL,
)


CREATE TABLE Addresses(
	Id INT PRIMARY KEY IDENTITY,
	AddressText VARCHAR(200) NOT NULL,
	TownId INT FOREIGN KEY REFERENCES Towns(Id) NOT NULL
)

CREATE TABLE Departments(
	Id INT  PRIMARY KEY IDENTITY,
	Name VARCHAR(50) NOT NULL
)

CREATE TABLE Employees(
	Id INT PRIMARY KEY IDENTITY,
	FirstName VARCHAR(50),
	MiddleName VARCHAR(50),
	LastName VARCHAR(50),
	JobTitle VARCHAR(50),
	DepartmentId INT FOREIGN KEY REFERENCES Departments(Id),
	HireDate DATETIME NOT NULL,
	Salary DECIMAL(15,2) NOT NULL,
	AddressId INT FOREIGN KEY REFERENCES Addresses(Id)
)

--•	Departments: Engineering, Sales, Marketing, Software Development, Quality Assurance
--•	Employees:

INSERT INTO Towns(Name)
VALUES('Sofia'),
('Plovdiv'),
('Varna'),
('Burgas')


INSERT INTO Departments(Name)
VALUES('Engineering'),
('Sales'),
('Marketing'),
('Software Development'),
('Quality Assurance')

--Ivan Ivanov Ivanov	.NET Developer	Software Development	01/02/2013	3500.00
--Petar Petrov Petrov	Senior Engineer	Engineering	02/03/2004	4000.00
--Maria Petrova Ivanova	Intern	Quality Assurance	28/08/2016	525.25
--Georgi Teziev Ivanov	CEO	Sales	09/12/2007	3000.00
--Peter Pan Pan	Intern	Marketing	28/08/2016	599.88


INSERT INTO Employees(FirstName,JobTitle, DepartmentId, HireDate,Salary)
VALUES ('Ivan Ivanov Ivanov','.NET Developer', 4, '01/02/2013', 3500.00),
('Petar Petrov Petrov','Senior Engineer', 1, '02/03/2004', 4000.00),
('Maria Petrova Ivanova','Intern', 5, '10/08/2016', 525.25),
('Georgi Teziev Ivanov','CEO', 2, '09/12/2007', 3000.00),
('Peter Pan Pan	Intern','Intern', 3, '12/08/2016', 599.88)


--Problem 19.	Basic Select All Fields

SELECT * FROM Towns
SELECT * FROM Departments
SELECT * FROM Employees

--Problem 20.	Basic Select All Fields and Order Them
SELECT * FROM Towns ORDER BY Name
SELECT * FROM Departments ORDER BY Name
SELECT * FROM Employees ORDER BY Salary DESC

--Problem 21.	Basic Select Some Fields
SELECT Name FROM Towns ORDER BY Name
SELECT Name FROM Departments ORDER BY Name
SELECT FirstName, LastName, JobTitle, Salary FROM Employees ORDER BY Salary DESC

--Problem 22.	Increase Employees Salary
UPDATE Employees
SET Salary +=Salary*0.10
SELECT Salary FROM Employees


--Problem 23.	Decrease Tax Rate
--USE  Hotel
UPDATE Payments
SET TaxRate -=TaxRate*0.03
SELECT TaxRate FROM Payments


--Problem 24.	Delete All Records
DELETE FROM Occupancies