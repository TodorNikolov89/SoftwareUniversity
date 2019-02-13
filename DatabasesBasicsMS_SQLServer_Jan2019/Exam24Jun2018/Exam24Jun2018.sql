CREATE DATABASE TripService

CREATE TABLE Cities(
	Id INT PRIMARY KEY IDENTITY
	,[Name] VARCHAR(20) NOT NULL
	,CountryCode VARCHAR(20) NOT NULL
)

CREATE TABLE Hotels(
	Id INT PRIMARY KEY IDENTITY
	,[Name] VARCHAR(30) NOT NULL
	,CityId INT NOT NULL
	,EmployeeCount INT NOT NULL
	,BaseRate DECIMAL (15,2)
)

CREATE TABLE Rooms(
	Id INT PRIMARY KEY IDENTITY
	,Price DECIMAL(15,2) NOT NULL
	,[Type] VARCHAR(20) NOT NULL
	,Beds INT NOT NULL
	,HotelId INT NOT NULL
)

CREATE TABLE Trips(
	Id INT PRIMARY KEY IDENTITY
	,RoomId INT NOT NULL
	,BookDate DATE NOT NULL
	,ArrivalDate DATE NOT NULL
	,ReturnDate DATE NOT NULL
	,CancelDate DATE
)


CREATE TABLE Accounts(
	Id INT PRIMARY KEY IDENTITY 
	,FirstName VARCHAR(50) NOT NULL
	,MiddleName VARCHAR(20)
	,LastName VARCHAR(50) NOT NULL
	,CityId INT NOT NULL
	,BirthDate DATE NOT NULL
	,Email VARCHAR(100) NOT NULL UNIQUE
)


CREATE TABLE AccountsTrips(
	AccountId INT NOT NULL
	,TripId INT NOT NULL
	,Luggage INT NOT NULL
)


ALTER TABLE AccountsTrips
ADD CONSTRAINT PK_AccountTrips PRIMARY KEY(AccountId, TripId)

ALTER TABLE AccountsTrips
ADD CONSTRAINT FK_AccountTrips_AccountsFOREIGN KEY (AccountId) REFERENCES Accounts(Id)

ALTER TABLE AccountsTrips
ADD CONSTRAINT FK_AccountTrips_TripsFOREIGN KEY (TripId) REFERENCES Trips(Id)

ALTER TABLE Accounts
ADD CONSTRAINT FK_Accounts_Cities FOREIGN KEY (CityId) REFERENCES Cities(Id)

ALTER TABLE Hotels
ADD CONSTRAINT FK_Hotels_Cities FOREIGN KEY (CityId) REFERENCES Cities(Id)

ALTER TABLE Rooms
ADD CONSTRAINT FK_Rooms_Hotels FOREIGN KEY (HotelID) REFERENCES Hotels(Id)

ALTER TABLE Trips
ADD CONSTRAINT FK_Trips_Rooms FOREIGN KEY (RoomId) REFERENCES Rooms(Id)

ALTER TABLE Trips
ADD CONSTRAINT CHK_BookDate CHECK (BookDate < ArrivalDate)

ALTER TABLE Trips
ADD CONSTRAINT CHK_ArrivalDate CHECK (ArrivalDate < ReturnDate)

ALTER TABLE AccountsTrips
ADD CONSTRAINT CHK_Luggage CHECK (Luggage>= 0)





--2. Insert
INSERT INTO Accounts(FirstName,	MiddleName,	LastName, CityId, BirthDate, Email) VALUES
('John', 'Smith', 'Smith', 34,	'1975-07-21', 'j_smith@gmail.com')
,('Gosho', '', 'Petrov', 11,	'1978-05-16', 'g_petrov@gmail.com')
,('Ivan', 'Petrovich', 'Pavlov', 59,	'1849-09-26', 'i_pavlov@softuni.bg')
,('Friedrich', 'Wilhelm', 'Nietzsche', 2,	'1844-10-15', 'f_nietzsche@softuni.bg')

INSERT INTO Trips (RoomId,	BookDate	,ArrivalDate,	ReturnDate	,CancelDate) VALUES
(101,	'2015-04-12',	'2015-04-14',	'2015-04-20',	'2015-02-02'	)
,(102,	'2015-07-07',	'2015-07-15',	'2015-07-22',	'2015-04-29'	)
,(103,	'2013-07-17',	'2013-07-23',	'2013-07-24',	NULL		)
,(104,	'2012-03-17',	'2012-03-31',	'2012-04-01',	'2012-01-10'	)
,(109,	'2017-08-07',	'2017-08-28',	'2017-08-29',	NULL		)


--3. Update

UPDATE Rooms
SET Price +=  1.14
WHERE
	HotelId IN(5,7,9) 

--4. Delete
DELETE FROM AccountsTrips WHERE AccountId = 47
DELETE FROM Accounts WHERE Id = 47

--5. Bulgarian Cities
SELECT Id ,Name
FROM
	Cities
WHERE
	CountryCode = 'BG'
ORDER BY
	Name


--6. People Born After 1991
SELECT FirstName + ' '+ ISNULL(MiddleName+' ','')  + LastName AS [Full Name] 
	,YEAR(BirthDate) AS BirthYear
FROM
	Accounts
WHERE
	YEAR(BirthDate)>1991
ORDER BY
	YEAR(BirthDate) DESC
	,FirstName ASC


--7. EEE-Mails
SELECT 
	A.FirstName
	,A.LastName
	,FORMAT(A.BirthDate,'MM-dd-yyyy', 'en-us')
	,C.[Name] AS [Hometown]
	,A.Email
FROM
	Accounts AS A
	JOIN Cities AS C ON A.CityId=C.Id
WHERE
	LEFT(A.Email,1)='e'
ORDER BY
	C.[Name] DESC


--8. City Statistics
