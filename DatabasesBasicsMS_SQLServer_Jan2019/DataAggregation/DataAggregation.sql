--Problem 1.	Records’ Count
SELECT 
	COUNT(WizzardDeposits.Id) AS [Count]
FROM
	WizzardDeposits

--Problem 2.	Longest Magic Wand
SELECT  
	MAX(MagicWandSize) AS [LongestMagicWand]
FROM 
	WizzardDeposits

--Problem 3.	Longest Magic Wand per Deposit Groups
SELECT TOP 2
	DepositGroup
	FROM
	WizzardDeposits AS w
GROUP BY
	w.DepositGroup
ORDER BY
	AVG(w.MagicWandSize) 

--Problem 5.	Deposits Sum
SELECT
	W.DepositGroup
	,SUM(W.DepositAmount) AS [TotalSum]
FROM 
	WizzardDeposits AS W
GROUP BY
	W.DepositGroup


--Problem 6.	Deposits Sum for Ollivander Family
SELECT
	W.DepositGroup
	,SUM(W.DepositAmount) AS [TotalSum]
FROM 
	WizzardDeposits AS W
WHERE
	W.MagicWandCreator = 'Ollivander family'
GROUP BY 
	W.DepositGroup


--Problem 7.	Deposits Filter
SELECT *
FROM
	(SELECT 
		W.DepositGroup
		,SUM(W.DepositAmount) AS [TotalSum]
	FROM WizzardDeposits AS W
	WHERE W.MagicWandCreator= 'Ollivander family'
	GROUP BY 
		W.DepositGroup ) AS T
WHERE
	T.TotalSum<150000
ORDER BY
	T.TotalSum DESC

--Second Solution

--SELECT
--	W.DepositGroup
--	,SUM(W.DepositAmount) AS [TotalSum]
--FROM 
--	WizzardDeposits AS W
--WHERE
--	W.MagicWandCreator = 'Ollivander family' 
--GROUP BY 
--	W.DepositGroup
--HAVING 
--	SUM(W.DepositAmount) < 150000
--ORDER BY 
--	SUM(W.DepositAmount) DESC
	

--Problem 8.	 Deposit Charge
SELECT 
	W.DepositGroup
	,W.MagicWandCreator 
	,MIN(W.DepositCharge) AS [MinSum]
FROM 
	WizzardDeposits AS W
GROUP BY
	W.DepositGroup , W.MagicWandCreator 


--Problem 9.	Age Groups
SELECT 
	CASE
		WHEN Age BETWEEN 0 AND 10 THEN '[0-10]'
		WHEN Age BETWEEN 11 AND 20 THEN '[11-20]'
		WHEN Age BETWEEN 21 AND 30 THEN '[21-30]'
		WHEN Age BETWEEN 31 AND 40 THEN '[31-40]'
		WHEN Age BETWEEN 41 AND 50 THEN '[41-50]'
		WHEN Age BETWEEN 51 AND 60 THEN '[51-60]'
		ELSE '[61+]'
	END AS AgeGroup
	, COUNT(*)
FROM WizzardDeposits
GROUP BY (
CASE
		WHEN Age BETWEEN 0 AND 10 THEN '[0-10]'
		WHEN Age BETWEEN 11 AND 20 THEN '[11-20]'
		WHEN Age BETWEEN 21 AND 30 THEN '[21-30]'
		WHEN Age BETWEEN 31 AND 40 THEN '[31-40]'
		WHEN Age BETWEEN 41 AND 50 THEN '[41-50]'
		WHEN Age BETWEEN 51 AND 60 THEN '[51-60]'
		ELSE '[61+]'
	END 
)


--Problem 10.	First Letter

SELECT 
	LEFT(FirstName,1) AS FirstLetter
FROM
	WizzardDeposits
WHERE
	DepositGroup ='Troll Chest'
GROUP BY 
	LEFT(FirstName,1)  
ORDER BY 
	FirstLetter 


--Problem 11.	Average Interest 
SELECT 
	DepositGroup
	,IsDepositExpired
	, AVG(DepositInterest)
FROM 
	WizzardDeposits
WHERE 
	DepositStartDate >='01/01/1985'
GROUP BY 
	DepositGroup
	, IsDepositExpired
ORDER BY
	DepositGroup DESC
	,IsDepositExpired ASC
	

--Problem 12.	* Rich Wizard, Poor Wizard
SELECT SUM(K.Diff)
FROM(
SELECT WD.DepositAmount - ( SELECT W.DepositAmount	FROM WizzardDeposits AS W	WHERE W.Id = WD.Id+1) AS Diff						
FROM 
	WizzardDeposits AS WD)AS K


--Problem 13.	Departments Total Salaries
SELECT 
	DepartmentID
	,SUM(Salary)
FROM Employees
GROUP BY 
	DepartmentID
ORDER BY
	DepartmentID ASC

--Problem 14.	Employees Minimum Salaries
SELECT 
	DepartmentID
	,MIN(Salary)
FROM Employees
WHERE
	DepartmentID IN (2, 5, 7) AND HireDate>'01/01/2000'
GROUP BY 
	DepartmentID
ORDER BY
	DepartmentID ASC


--Problem 15.	Employees Average Salaries
SELECT * INTO NEWTABLE
FROM Employees
WHERE
	Salary>30000
DELETE FROM NEWTABLE
WHERE
	ManagerID = 42
