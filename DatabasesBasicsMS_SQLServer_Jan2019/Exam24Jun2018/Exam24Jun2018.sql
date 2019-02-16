CREATE DATABASE TripService

USE TripService

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
INSERT INTO Accounts( FirstName,	MiddleName,	LastName, CityId, BirthDate, Email) VALUES
('John', 'Smith', 'Smith', 34,	'1975-07-21', 'j_smith@gmail.com')
,('Gosho', '', 'Petrov', 11,	'1978-05-16', 'g_petrov@gmail.com')
,('Ivan', 'Petrovich', 'Pavlov', 59,	'1849-09-26', 'i_pavlov@softuni.bg')
,('Friedrich', 'Wilhelm', 'Nietzsche', 2,	'1844-10-15', 'f_nietzsche@softuni.bg')

INSERT INTO Trips (RoomId,	BookDate	,ArrivalDate,	ReturnDate	,CancelDate) VALUES
(101,	'2015-04-12',	'2015-04-14',	'2015-04-20',	'2015-02-02')
,(102,	'2015-07-07',	'2015-07-15',	'2015-07-22',	'2015-04-29')
,(103,	'2013-07-17',	'2013-07-23',	'2013-07-24',	NULL)
,(104,	'2012-03-17',	'2012-03-31',	'2012-04-01',	'2012-01-10')
,(109,	'2017-08-07',	'2017-08-28',	'2017-08-29',	NULL)


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
SELECT C.Name 
	,COUNT(H.Id) AS TOTAL
  FROM	
	Cities AS C
	LEFT OUTER JOIN Hotels AS H ON C.Id = H.CityId
GROUP BY
	C.Name 
ORDER BY
	TOTAL DESC
	,C.Name 
	

--9. Expensive First-Class Rooms
SELECT 
	R.Id
	,R.Price
	,H.Name
	,C.Name
FROM 
	Rooms AS R
	JOIN Hotels AS H ON H.Id = R.HotelId
	JOIN Cities AS C ON C.Id = H.CityId
WHERE
	Type='First Class'
ORDER BY
	R.Price DESC
	,R.Id

--10. Longest and Shortest Trips
SELECT RES.Id,RES.FirstName + ' ' + RES.LastName AS [FullName], MAX(RES.DIFF) AS [LongestTrip] , MIN(RES.DIFF) AS [ShortestTrip]
FROM
(SELECT 
	A.Id
	,A.FirstName
	,A.MiddleName
	,A.LastName
	,T.CancelDate
	,DATEDIFF(DAY, T.ArrivalDate,T.ReturnDate) AS DIFF
FROM
	Accounts AS A
	JOIN AccountsTrips AS AT ON AT.AccountId = A.Id
	JOIN TRIPS AS T ON T.Id = AT.TripId ) AS RES
WHERE
	RES.MiddleName IS NULL AND RES.CancelDate IS NULL
GROUP BY  RES.FirstName, RES.LastName, RES.Id, RES.MiddleName
ORDER BY
	[LongestTrip] DESC
	,RES.Id
	

--11. Metropolis
SELECT TOP(5) C.Id,C.Name AS [City],C.CountryCode AS [Country], COUNT(A.CityId) AS [Accounts]
FROM
	Cities AS C
	JOIN Accounts AS A ON A.CityId = C.Id
GROUP BY
	C.Id,C.Name,C.CountryCode
ORDER BY
	[Accounts] DESC


--12. Romantic Getaways

--Find all accounts, which have had one or more trips to a hotel in their hometown.
--Order them by the trips count (descending), then by Account ID.

SELECT
	A.Id
	,A.Email
	,C.Name AS [City]
	,COUNT(T.Id) AS Trips
FROM 
	Accounts AS A
	JOIN AccountsTrips AS at ON at.AccountId = a.Id	
	JOIN TRIPS AS T ON T.Id = AT.TripId
	JOIN Rooms AS R ON R.Id = T.RoomId
	JOIN Hotels AS H ON H.Id = R.HotelId
	JOIN Cities AS C ON C.Id = H.CityId
WHERE
	A.CityId = H.CityId
GROUP BY
	A.Id, A.Email, C.Name 
ORDER BY
	Trips DESC
	,A.Id


--13. Lucrative Destinations
SELECT TOP(10)
	C.Id
	,C.Name AS [Name]
	,SUM(H.BaseRate+R.Price) AS [Total Revenue]
	,COUNT(T.Id) AS [Trips]
FROM
	Cities AS C
	JOIN Hotels AS H ON C.Id = H.CityId
	JOIN Rooms AS R ON R.HotelId = H.Id
	JOIN Trips AS T ON T.RoomId = R.Id
WHERE
	DATEPART(YEAR,T.BookDate) = 2016
GROUP BY
	C.Id, C.Name
ORDER BY
	[Total Revenue] DESC
	,Trips DESC


--14. Trip Revenues
SELECT 
	T.Id
	,H.Name AS [HotelName]
	,R.Type AS [RoomType]
	,CASE
		WHEN T.CancelDate IS NULL THEN SUM(H.BaseRate + R.Price)  
		ELSE  0.00
	 END AS [Revenue]
FROM
	Trips AS T
	JOIN AccountsTrips AS AT ON AT.TripId = T.Id
	JOIN Rooms AS R ON R.Id = T.RoomId
	JOIN Hotels AS H ON H.Id = R.HotelId
GROUP BY
	 T.Id
	,H.Name
	,R.Type
	,T.CancelDate
ORDER BY
	R.Type
	,T.Id
	
	
--15. Top Travelers
SELECT TEMP.Id,TEMP.Email,TEMP.CountryCode, TEMP.Trips FROM
(SELECT 
	A.Id
	,A.Email
	,C.CountryCode
	,COUNT(*) AS Trips
	,DENSE_RANK() OVER(PARTITION BY C.CountryCode ORDER BY COUNT(*) DESC,A.ID) AS TripsRank
FROM
	Accounts AS A
	JOIN AccountsTrips AS AT ON AT.AccountId = A.Id
	JOIN Trips AS T ON T.Id = AT.TripId
	JOIN Rooms AS R ON R.Id = T.RoomId
	JOIN Hotels AS H ON H.Id = R.HotelId
	JOIN Cities AS C ON C.Id = H.CityId
GROUP BY
	C.CountryCode
	,A.Email
	,A.Id
	) AS TEMP
WHERE
	TEMP.TripsRank = 1
ORDER BY
	TEMP.Trips DESC
	,TEMP.Id
	
	


