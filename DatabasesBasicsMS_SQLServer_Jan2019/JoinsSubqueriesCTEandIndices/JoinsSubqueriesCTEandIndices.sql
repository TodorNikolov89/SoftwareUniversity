--Problem 1.	Employee Address
SELECT TOP 5
	E.EmployeeID
	,E.JobTitle
	,A.AddressID
	,A.AddressText
FROM
	Employees AS E
	INNER JOIN Addresses AS A ON A.AddressID = E.AddressID
ORDER BY
	AddressID ASC


--Problem 2.	Addresses with Towns
SELECT TOP 50
	E.FirstName
	,E.LastName
	,T.Name
	,A.AddressText
FROM
	Employees AS E
	JOIN Addresses AS A ON A.AddressID = E.AddressID
	JOIN Towns AS T ON T.TownID = A.TownID
ORDER BY
	E.FirstName ASC
	,E.LastName ASC

--Problem 3.	Sales Employee
SELECT 
	E.EmployeeID
	,E.FirstName
	,E.LastName
	,D.Name
FROM 
	Employees AS E
INNER JOIN Departments AS D ON D.DepartmentID = E.DepartmentID
WHERE
	D.Name = 'Sales'
ORDER BY
	E.EmployeeID ASC


--Problem 4.	Employee Departments
SELECT TOP 5
	E.EmployeeID
	,E.FirstName
	,E.Salary
	,D.Name
FROM
	Employees AS E
	JOIN Departments AS D ON D.DepartmentID = E.DepartmentID
WHERE
	E.Salary > 15000
ORDER BY
	D.DepartmentID ASC


--Problem 5.	Employees Without ProjecT
SELECT TOP 3
	E.EmployeeID
	,E.FirstName
FROM
	Employees AS E
	LEFT OUTER JOIN EmployeesProjects AS EP ON EP.EmployeeID = E.EmployeeID 
WHERE
	EP.EmployeeID IS NULL
ORDER BY
	EP.EmployeeID ASC


--Problem 6.	Employees Hired After
SELECT 
	E.FirstName
	,E.LastName
	,E.HireDate
	,D.Name
FROM
	Employees AS E
	JOIN Departments AS D ON D.DepartmentID = E.DepartmentID
WHERE
	D.Name = 'Sales' OR D.Name = 'Finance'
ORDER BY 
	E.HireDate ASC



--Problem 7.	Employees with Project
SELECT TOP 5
	E.EmployeeID
	,E.FirstName
	,P.Name
FROM
	Employees AS E
	JOIN EmployeesProjects  AS EP ON EP.EmployeeID = E.EmployeeID
	JOIN Projects AS P ON P.ProjectID = EP.ProjectID
WHERE
	FORMAT(E.HireDate,'DD-MM-YYY') >   '13.08.2002' AND P.EndDate IS NULL
ORDER BY
	E.EmployeeID ASC



--Problem 8.	Employee 24
SELECT
	E.EmployeeID
	,E.FirstName
	,P.Name
FROM
	Employees AS E
	JOIN EmployeesProjects AS EP ON EP.EmployeeID = E.EmployeeID
	LEFT OUTER JOIN Projects AS P ON P.ProjectID = EP.ProjectID AND P.StartDate <'01.01.2005'
WHERE
	E.EmployeeID = 24



--Problem 9.	Employee Manager
SELECT
	EMP.EmployeeID
	,EMP.FirstName
	,MNG.EmployeeID
	,MNG.FirstName
FROM
	Employees AS EMP
	JOIN Employees AS MNG ON EMP.ManagerID = MNG.EmployeeID
WHERE
	MNG.EmployeeID = 3 OR MNG.EmployeeID = 7
ORDER BY
	EMP.EmployeeID ASC


--Problem 10.	Employee Summary
SELECT TOP 50
	EMP.EmployeeID
	,EMP.FirstName+' '+EMP.LastName AS EmployeeName
	,MNG.FirstName+' '+MNG.LastName AS ManagerName
	,EMPDEP.Name AS DepartmentName
FROM
	Employees AS EMP
	JOIN Employees AS MNG ON EMP.ManagerID = MNG.EmployeeID
	JOIN Departments AS EMPDEP ON EMPDEP.DepartmentID = EMP.DepartmentID
ORDER BY 
	EMP.EmployeeID ASC


--Problem 11.	Min Average Salary
SELECT MIN(A.MinAverageSalary)
FROM
	(SELECT 
		AVG(E.Salary) AS MinAverageSalary
	FROM
		Employees AS E GROUP BY E.DepartmentID) AS A

--SELECT TOP 1
--	AVG(E.Salary) AS MinAverageSalary
--FROM
--	Employees AS E
--GROUP BY 
--	E.DepartmentID
--ORDER BY 
--	MinAverageSalary ASC



--Problem 12.	Highest Peaks in Bulgaria
SELECT 
	MC.CountryCode
	,M.MountainRange
	,P.PeakName
	,P.Elevation
FROM
	MountainsCountries AS MC
	JOIN Mountains AS M ON M.Id = MC.MountainId
	JOIN Peaks AS P ON P.MountainId = M.Id

WHERE
	MC.CountryCode='BG' AND
	P.Elevation>2835
ORDER BY 
	P.Elevation DESC


--Problem 13.	Count Mountain Ranges
SELECT 
	MC.CountryCode
	,COUNT(M.MountainRange) AS MountainRanges
FROM
	Mountains AS M
	JOIN MountainsCountries AS MC ON MC.MountainId = M.Id
WHERE
	MC.CountryCode IN ('BG', 'US', 'RU')
GROUP BY
	MC.CountryCode 



--Problem 14.  	Countries with Rivers
