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
