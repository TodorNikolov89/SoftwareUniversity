CREATE DATABASE TEMP


--Problem 1.	One-To-One Relationship
CREATE TABLE Persons(
	PersonID INT NOT NULL
	,FirstName VARCHAR(30)
	,Salary DECIMAL (10,2)
	,PassportID INT
)

CREATE TABLE Passports(
	PassportID INT NOT NULL
	,PassportNumber VARCHAR(30)
)

INSERT INTO Persons(PersonID, FirstName, Salary, PassportID)
VALUES (1, 'Roberto',43300.00,	102),
(2,'Tom',56100.00,103),
(3, 'Yana',60200.00, 101)


INSERT INTO Passports(PassportID, PassportNumber)
VALUES (101,'N34FG21B'),
(102,'K65LO4R7'),
(103, 'ZE657QP2')


ALTER TABLE Persons
ADD PRIMARY KEY(PersonID)

ALTER TABLE Passports
ADD PRIMARY KEY(PassportID)


ALTER TABLE Persons
ADD FOREIGN KEY (PassportID) REFERENCES Passports(PassportID)


--Problem 2.	One-To-Many Relationship
CREATE TABLE Models(
	ModelID INT NOT NULL
	,Name VARCHAR (30)
	,ManufacturerID INT
)	

INSERT INTO Models( ModelID, Name, ManufacturerID)
VALUES (101, 'X1', 1),
(102, 'i6', 1),
(103, 'Model S', 2),
(104, 'Model X', 2),
(105, 'Model 3', 2),
(106, 'Nova', 3)

CREATE TABLE Manufacturers(
	ManufacturerID INT NOT NULL 
	,Name VARCHAR(30)
	,EstablishedOn DATETIME2
)

INSERT INTO Manufacturers( ManufacturerID, Name, EstablishedOn)
VALUES(1, 'BMW', '07/03/1916'),
(2,'Tesla','01/01/2003'),
(3,'Lada','01/05/1966')

ALTER TABLE Manufacturers
ADD PRIMARY KEY(ManufacturerID)

ALTER TABLE Models
ADD PRIMARY KEY (ModelID)

ALTER TABLE Models
ADD CONSTRAINT FK_Models_Manufacturers FOREIGN KEY(ManufacturerID) REFERENCES Manufacturers(ManufacturerID)


--Problem 3.	Many-To-Many Relationship

--CREATE TABLE Students(
--	StudentID INT NOT NULL
--	,[Name] VARCHAR(30)
--)

--INSERT INTO Students(StudentID, [Name])
--VALUES(1, 'Mila'),
--(2,'Toni'),
--(3,'Ron') 

--CREATE TABLE Exams(
--	ExamID INT NOT NULL
--	,[Name] VARCHAR(30)
--)

--INSERT INTO Exams(ExamID, [Name])
--VALUES(101, 'SpringMVC'),
--(102, 'Neo4j'),
--(103, 'Oracle 11g')

--CREATE TABLE StudentsExams(
--	StudentID INT NOT NULL
--	,ExamID INT NOT NULL
--)

--INSERT INTO StudentsExams(StudentID, ExamID)
--VALUES(1,101),
--(1,102),
--(2,101),
--(3,103),
--(2,102),
--(2,103)

--ALTER TABLE Students
--ADD PRIMARY KEY (StudentID)

--ALTER TABLE Exams
--ADD PRIMARY KEY (ExamID)

--ALTER TABLE StudentsExams
--ADD CONSTRAINT PK_StudentsExams PRIMARY KEY (StudentID, ExamID)

--ALTER TABLE StudentsExams
--ADD CONSTRAINT FK_Students FOREIGN KEY (StudentID) REFERENCES Students(StudentID)

--ALTER TABLE StudentsExams
--ADD CONSTRAINT FK_Exams FOREIGN KEY (ExamID) REFERENCES Exams(ExamID)


--Problem 4.	Self-Referencing 
CREATE TABLE Teachers(
	TeacherID INT NOT NULL
	,[Name] VARCHAR (30)
	,ManagerID INT
)

INSERT INTO Teachers(TeacherID, [Name], ManagerID)
VALUES(101,'John', NULL),
(102,'Maya', 106),
(103,'Silvia', 106),
(104,'Ted	105', 105),
(105,'Mark	105', 101),
(106,'Greta	105', 101)

ALTER TABLE Teachers
ADD PRIMARY KEY (TeacherID)

ALTER TABLE Teachers
ADD CONSTRAINT FK_ManagerTeacher FOREIGN KEY (ManagerId) REFERENCES Teachers(TeacherID)


--Problem 5.	Online Store Database
CREATE TABLE Cities(
	CityID INT NOT NULL
	,[Name] VARCHAR(50)
)

CREATE TABLE Customers(
	CustomerID INT NOT NULL
	,[Name] VARCHAR(50)
	,Birthday DATE
	,CityID INT
)

ALTER TABLE Cities
ADD PRIMARY KEY (CityID)

ALTER TABLE Customers
ADD PRIMARY KEY (CustomerID)

ALTER TABLE Customers
ADD CONSTRAINT FK_Customers_Cities FOREIGN KEY (CityID) REFERENCES Cities(CityID)

CREATE TABLE Orders(
	OrderID INT NOT NULL
	,CustomerID INT
)

ALTER TABLE Orders
ADD PRIMARY KEY (OrderID)

ALTER TABLE Orders
ADD CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)


CREATE TABLE Items(
	ItemID INT NOT NULL
	,[Name] VARCHAR(50)
	,ItemTypeID INT
)

ALTER TABLE Items
ADD PRIMARY KEY(ItemID)

CREATE TABLE ItemTypes(
	ItemTypeID INT NOT NULL
	,[Name] VARCHAR(50)
)

ALTER TABLE ItemTypes
ADD PRIMARY KEY (ItemTypeID)

ALTER TABLE Items
ADD CONSTRAINT FK_Items_ItemsTypes FOREIGN KEY (ItemTypeID) REFERENCES ItemTypes(ItemTypeID)



CREATE TABLE OrderItems(
	OrderID INT NOT NULL
	,ItemID INT NOT NULL
)


ALTER TABLE OrderItems
ADD PRIMARY KEY (OrderID, ItemID)

ALTER TABLE OrderItems
ADD CONSTRAINT FK_OrderID FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)

ALTER TABLE OrderItems
ADD CONSTRAINT FK_ItemID FOREIGN KEY (ItemID) REFERENCES Items(ItemID)


--Problem 6.	University Database
CREATE TABLE Majors(
	MajorID INT NOT NULL
	,[Name] VARCHAR(50)
)

ALTER TABLE Majors
ADD PRIMARY KEY (MajorID)

CREATE TABLE Payments(
	PaymentID INT NOT NULL
	,PaymentDate DATE
	,PaymentAmount DECIMAL (10,2)
	,StudentID INT
)

ALTER TABLE Payments
ADD PRIMARY KEY (PaymentID)

CREATE TABLE Students(
	StudentID INT NOT NULL
	,StudentNumber VARCHAR(50)
	,StudentName VARCHAR(50)
	,MajorID INT
)

ALTER TABLE Students
ADD PRIMARY KEY (StudentID)

ALTER TABLE Payments
ADD CONSTRAINT FK_StudentID_Payment FOREIGN KEY (StudentID) REFERENCES Students(StudentID)

ALTER TABLE Students
ADD CONSTRAINT FK_Students_Payments FOREIGN KEY (MajorID) REFERENCES Majors(MajorID)


CREATE TABLE Subjects(
	SubjectID INT NOT NULL
	,SubjectName VARCHAR(50)
)

ALTER TABLE Subjects
ADD PRIMARY KEY (SubjectID)


CREATE TABLE Agenda(
	StudentID INT NOT NULL
	,SubjectID INT NOT NULL
)

ALTER TABLE Agenda
ADD PRIMARY KEY(StudentID, SubjectID)

ALTER TABLE Agenda
ADD CONSTRAINT FK_Students FOREIGN KEY (StudentID) REFERENCES Students(StudentID)

ALTER TABLE Agenda
ADD CONSTRAINT FK_Subjects FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID)


--Problem 9.	*Peaks in Rila
SELECT
	 M.MountainRange
	 ,P.PeakName
	 ,P.Elevation
FROM	
	Mountains AS M
JOIN Peaks AS P ON M.Id=P.MountainId
WHERE M.MountainRange='Rila'
ORDER BY 
	P.Elevation DESC