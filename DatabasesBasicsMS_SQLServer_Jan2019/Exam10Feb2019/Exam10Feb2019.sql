CREATE DATABASE ColonialJourney

CREATE TABLE Planets(
Id INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(30) NOT NULL,
)
 
CREATE TABLE Spaceports(
Id INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(50) NOT NULL,
PlanetId INT NOT NULL FOREIGN KEY REFERENCES Planets(Id)
)
 
CREATE TABLE Spaceships(
Id INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(50) NOT NULL,
Manufacturer VARCHAR(30) NOT NULL,
LightSpeedRate INT DEFAULT(0)
)
 
CREATE TABLE Colonists(
Id INT PRIMARY KEY IDENTITY,
FirstName VARCHAR(20) NOT NULL,
LastName VARCHAR(20) NOT NULL,
Ucn VARCHAR(10) NOT NULL UNIQUE,
BirthDate DATE NOT NULL
)
 
CREATE TABLE Journeys(
Id INT PRIMARY KEY IDENTITY,
JourneyStart DATETIME NOT NULL,
JourneyEnd DATETIME NOT NULL,
Purpose VARCHAR(11) CHECK(Purpose IN('Medical','Technical','Educational','Military')),
DestinationSpaceportId INT NOT NULL FOREIGN KEY REFERENCES Spaceports(Id)   ,
SpaceshipId INT NOT NULL FOREIGN KEY REFERENCES Spaceships(Id)
)
 
CREATE TABLE TravelCards (
Id INT PRIMARY KEY IDENTITY,
CardNumber CHAR(10) NOT NULL UNIQUE,
JobDuringJourney VARCHAR(8) CHECK (JobDuringJourney IN('Pilot','Engineer','Trooper','Cleaner','Cook')),
ColonistId INT NOT NULL FOREIGN KEY REFERENCES Colonists(Id) ,
JourneyId INT NOT NULL FOREIGN KEY REFERENCES Journeys(Id)
)

--2.	Insert
INSERT INTO Planets(Name) VALUES
('Mars')
,('Earth')
,('Jupiter')
,('Saturn')

INSERT INTO Spaceships(Name,	Manufacturer,	LightSpeedRate) VALUES
('Golf',	'VW',	3)
,('WakaWaka'	,'Wakanda',	4)
,('Falcon9',	'SpaceX',	1)
,('Bed'	,'Vidolov',	6)


--3.	Update
UPDATE Spaceships
SET LightSpeedRate+=1
WHERE
	ID BETWEEN 8 AND 12

--4.	Delete
DELETE FROM TravelCards
WHERE JourneyId IN (1,2,3)

DELETE  FROM Journeys
WHERE Id IN (1,2,3)

--5.	Select all travel cards
SELECT 
	CardNumber
	,JobDuringJourney
FROM
	TravelCards
ORDER BY
	CardNumber ASC


--6.	Select all colonists
SELECT 
	Id
	,FirstName + ' ' + LastName AS [FullName]
	,Ucn
FROM Colonists
ORDER BY
	FirstName
	,LastName
	,Id

--7.	Select all military journeys
SELECT 
	ID
	,FORMAT(JourneyStart, 'dd/MM/yyyy')
	,FORMAT(JourneyEnd, 'dd/MM/yyyy')
FROM
	Journeys
WHERE
	Purpose = 'Military'
ORDER BY
	JourneyStart

--8.	Select all pilots
SELECT C.Id, C.FirstName+' '+C.LastName AS [full_name]
FROM Colonists AS C
 JOIN TravelCards AS T ON C.Id = T.ColonistId
 WHERE T.JobDuringJourney='Pilot'
 ORDER BY
 C.Id

 --9.	Count colonists
 SELECT COUNT(*) AS [count] FROM Colonists AS C
 JOIN TravelCards AS T ON T.ColonistId = C.Id
 JOIN Journeys AS J ON J.Id = T.JourneyId
 WHERE
	J.Purpose = 'technical'


--10.	Select the fastest spaceship
SELECT TOP(1) 
	S.Name AS [SpaceshipName]
	,SP.Name AS [SpaceportName]
FROM Spaceships AS S
JOIN Journeys AS J ON J.SpaceshipId = S.Id
JOIN Spaceports AS SP ON SP.Id = J.DestinationSpaceportId
ORDER BY
	S.LightSpeedRate DESC


--11.	Select spaceships with pilots younger than 30 years
SELECT S.Name, S.Manufacturer 
FROM
	Colonists AS C
	JOIN TravelCards AS T ON T.ColonistId = C.Id
	JOIN Journeys AS J ON J.Id = T.JourneyId
	JOIN Spaceships AS S ON S.Id = J.SpaceshipId
WHERE 
	DATEDIFF(YEAR, C.BirthDate, '01/01/2019') < 30 AND  T.JobDuringJourney='Pilot'
ORDER BY
	S.Name


--12.	Select all educational mission planets and spaceports

SELECT P.Name,S.Name
FROM Planets AS P
JOIN Spaceports AS S ON S.PlanetId = P.Id
JOIN Journeys AS J ON J.DestinationSpaceportId = S.Id
WHERE
J.Purpose = 'educational'
ORDER BY
	S.Name DESC


--13.	Select all planets and their journey count

--Extract from the database all planets’ names and their journeys count. Order the results by journeys count, descending and by planet name ascending

SELECT P.Name,COUNT(J.Id) AS [JourneysCount]
FROM Planets AS P
JOIN Spaceports AS S ON S.PlanetId = P.Id
JOIN Journeys AS J ON J.DestinationSpaceportId = S.Id
GROUP BY P.Name
ORDER BY 
	[JourneysCount] DESC
	,P.Name


--14.	Select the shortest journey
SELECT TOP 1 A.Id, A.PlanetName, A.SpaceportName,A.JourneyPurpose
FROM
(SELECT J.Id AS [Id] , P.Name AS [PlanetName],SP.Name AS [SpaceportName], J.Purpose AS [JourneyPurpose], DATEDIFF(DAY, J.JourneyStart,J.JourneyEnd) AS [DIFFERENCE]
FROM
	Journeys AS J
	JOIN Spaceports AS SP ON SP.Id = J.DestinationSpaceportId
	JOIN Planets AS P ON P.Id = SP.PlanetId
	GROUP BY J.Id , P.Name,SP.Name, J.Purpose, J.JourneyStart, J.JourneyEnd)  AS A
	ORDER BY A.DIFFERENCE ASC

--15.	Select the less popular job
--Extract from the database the less popular job in the longest journey. In other words, the job with less assign colonists.




--18.	Get Colonists Count
CREATE FUNCTION udf_GetColonistsCount(@PlanetName VARCHAR (30)) 
RETURNS INT
AS
BEGIN
DECLARE @ColCount INT=(SELECT COUNT(*)
						FROM Planets AS P
						JOIN Spaceports AS SP ON SP.PlanetId = P.Id
						JOIN Journeys AS J ON J.DestinationSpaceportId =SP.Id
						JOIN TravelCards AS TC ON TC.JourneyId = J.Id
						JOIN Colonists AS C ON C.Id = TC.ColonistId
						WHERE
						P.Name = @PlanetName) 
	RETURN @ColCount
END

SELECT dbo.udf_GetColonistsCount('Otroyphus')


--19.	Change Journey Purpose

CREATE PROC usp_ChangeJourneyPurpose(@JourneyId INT, @NewPurpose VARCHAR(30))
AS
BEGIN
	DECLARE @JId INT = (SELECT Id FROM Journeys WHERE Id = @JourneyId) 

	IF(@JId  IS NULL)
	BEGIN	
			;THROW 51000, 'The journey does not exist!', 1	
	END

	DECLARE @JourNPurP VARCHAR(30) = (SELECT Purpose FROM Journeys WHERE Id = @JourneyId)

	IF(@NewPurpose = @JourNPurP)
	BEGIN
		;THROW 51000, 'You cannot change the purpose!', 2
	END

	UPDATE Journeys
	SET Purpose=@NewPurpose
	WHERE Id=@JourneyId
END

--20.	Deleted Journeys
CREATE TABLE DeletedJourneys
(
	Id INT,
	JourneyStart DATETIME,
	JourneyEnd DATETIME,
	Purpose VARCHAR(11),
	DestinationSpaceportId INT,
	SpaceshipId INT
)

CREATE TRIGGER t_DeleteJourneyS
	ON Journeys
	AFTER DELETE
AS
	BEGIN
		INSERT INTO DeletedJourneys(Id,JourneyStart,JourneyEnd,Purpose,DestinationSpaceportId,
		SpaceshipId)
		SELECT Id,JourneyStart,JourneyEnd, Purpose, DestinationSpaceportId, SpaceshipId FROM deleted
	END


