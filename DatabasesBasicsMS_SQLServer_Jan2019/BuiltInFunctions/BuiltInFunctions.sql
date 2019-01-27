USE SoftUni
--Problem 1.	Find Names of All Employees by First Name
SELECT 
	FirstName
	,LastName
FROM 
	Employees
WHERE
	SUBSTRING(FirstName,1,2)='SA'

--Problem 2.	  Find Names of All employees by Last Name 
SELECT 
	FirstName
	,LastName
FROM 
	Employees
WHERE
	CHARINDEX('ei',LastName) != 0


--Problem 3.	Find First Names of All Employees
SELECT 
	FirstName
FROM 
	Employees
WHERE 
	DepartmentID in (3,10)	AND
	DATEPART(YEAR,HireDate) between 1995 and 2005
	
	
--Problem 4.	Find All Employees Except Engineers
SELECT 
	FirstName
	,LastName
FROM 
	Employees
WHERE 
	CHARINDEX('engineer', JobTitle) = 0

--Problem 5.	Find Towns with Name Length
SELECT 
	Name
FROM 
	Towns
WHERE 
	DATALENGTH(Name)=5 OR DATALENGTH(Name)=6
ORDER BY
	Name ASC

--Problem 6.	 Find Towns Starting With
SELECT 
	TownID
	,Name
FROM
	Towns
WHERE
	LEFT(Name,1) IN ('M', 'K', 'B',  'E')
ORDER BY 
	Name ASC

--Problem 7.	 Find Towns Not Starting With
SELECT 
	*
FROM
	Towns
WHERE
	LEFT(Name,1) !='R' AND LEFT(Name,1) !='B' AND LEFT(Name,1) != 'D'
ORDER BY 
	Name ASC


--Problem 8.	Create View Employees Hired After 2000 Year
-- That solution should be testet in new batch
CREATE VIEW	V_EmployeesHiredAfter2000 AS
SELECT 
	FirstName
	,LastName
FROM
	Employees
WHERE
	DATEPART(YEAR,HireDate) >2000


--Problem 9.	Length of Last Name
SELECT 
	FirstName
	,LastName
FROM
	Employees
WHERE
	DATALENGTH(LastName)=5


--Problem 10.	 Rank Employees by Salary
SELECT
	 EmployeeID
	 ,FirstName
	 ,LastName
	 ,Salary,
	DENSE_RANK () over (PARTITION BY Salary  order by EmployeeID ASC) as [Rank]
FROM 
	Employees
WHERE
	Salary BETWEEN 10000 AND 50000 
ORDER BY
	Salary DESC


--Problem 11.	Find All Employees with Rank 2 *
SELECT *
FROM 
	(SELECT 
		EmployeeID
		,FirstName
		,LastName
		,Salary
		,DENSE_RANK() OVER (PARTITION BY Salary  order by EmployeeID ASC) AS Rank
	FROM
		Employees) RESULT
WHERE 
	Salary BETWEEN 10000 AND 50000 AND
	RESULT.Rank =2	
ORDER BY
	Salary DESC

--Problem 12.	Countries Holding ‘A’ 3 or More Times
use Geography

SELECT 
	CountryName
	,IsoCode
FROM 
	Countries
WHERE
	LEN(CountryName) - len(replace(CountryName, 'A', ''))>=3
ORDER BY
	IsoCode ASC


--Problem 13.	 Mix of Peak and River Names
SELECT 
	PeakName
	,RiverName
	,LOWER(PeakName+SUBSTRING(RiverName, 2, LEN(RiverName))) AS Mix
FROM 
	Peaks
	,Rivers
WHERE
	RIGHT(PeakName,1) = LEFT(RiverName,1)
ORDER BY
	Mix ASC


--Problem 14.	Games from 2011 and 2012 year
USE Diablo

SELECT TOP 50
	Name
	,FORMAT([Start], 'yyyy-MM-dd') AS Start
FROM 
	Games
WHERE 
	YEAR(Start) BETWEEN 2011 AND 2012
ORDER BY 
	Start ASC
	,Name ASC


--Problem 15.	 User Email Providers
SELECT 
	Username
	,RIGHT(Email, len(Email) - charindex('@', Email)) AS [Email Provider]
FROM 
	Users
ORDER BY
	[Email Provider] ASC
	,Username ASC

--Problem 16.	 Get Users with IPAdress Like Pattern
SELECT 
	Username
	,IpAddress	
FROM 
	Users
WHERE
	IpAddress LIKE '___.1%.%.___'
ORDER BY
	Username ASC

--Problem 17.	 Show All Games with Duration and Part of the Day
SELECT 
	[Name] AS [Game]
	,CASE
		WHEN DATEPART(hh,[Start]) >= 0 AND DATEPART(hh,[Start]) < 12 THEN 'Morning'
		WHEN DATEPART(hh,[Start]) >= 12 AND DATEPART(hh,[Start]) < 18 THEN 'Afternoon'
		WHEN DATEPART(hh,[Start]) >= 18 AND DATEPART(hh,[Start]) < 24 THEN 'Evening'
	END as [Part of the Day]
	,CASE
		WHEN Duration <= 3 THEN 'Extra Short'
		WHEN Duration >= 4 AND Duration <= 6 THEN 'Short'
		WHEN Duration >6 THEN 'Long'
		ELSE 'Extra Long' 
	END AS [Duration]
FROM
	Games
ORDER BY 
	[Name] ASC
	,[Duration] ASC
	,[Part of the Day] ASC

--Problem 18.	 Orders Table
CREATE DATABASE TESTBASE
USE TESTBASE

CREATE TABLE Orders(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	ProductName VARCHAR(50),
	OrderDate DATETIME 
)

INSERT INTO Orders(ProductName, OrderDate)
VALUES('Butter','2016-09-19 00:00:00.000'),
('Milk','2016-09-30 00:00:00.000'),
('Cheese','2016-09-04 00:00:00.000'),
('Bread','2015-12-20 00:00:00.000'),
('Tomatoes','2015-12-30 00:00:00.000')

SELECT 
	ProductName
	,OrderDate 
	,DATEADD(DAY, 3, OrderDate) AS [Pay Due]  
	,DATEADD(MONTH, 1, OrderDate) AS [Deliver Due]
FROM 
	Orders


--Problem 19.	 People Table
CREATE TABLE People(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] VARCHAR(50),
	Birthdate DATETIME 
)

INSERT INTO People([Name], Birthdate)
VALUES('Victor','2000-12-07 00:00:00.000'),
('Steven','1992-09-10 00:00:00.000'),
('Stephen','1910-09-19 00:00:00.000'),
('John','2010-01-06 00:00:00.000')

SELECT 
	[Name]
	,Birthdate 
	,DATEDIFF(YEAR,Birthdate, GETDATE()) as [Age in Years]
	,DATEDIFF(MONTH,Birthdate, GETDATE()) as [Age in Months]
	,DATEDIFF(DAY,Birthdate, GETDATE()) as [Age in Days]
	,DATEDIFF(MINUTE,Birthdate, GETDATE()) as [Age in Minutes]
FROM 
	People
