----Problem 1. Employees with Salary Above 35000
CREATE PROCEDURE usp_GetEmployeesSalaryAbove35000
AS
SELECT 
	FirstName AS [First Name]
	,LastName AS [Last Name]
FROM
	Employees
WHERE
	Salary >= 35000

EXEC usp_GetEmployeesSalaryAbove35000


----Problem 2. Employees with Salary Above Number
CREATE PROCEDURE usp_GetEmployeesSalaryAboveNumber @Input DECIMAL(18,4)
AS
SELECT 
	FirstName
	,LastName
FROM
	Employees
WHERE
	Salary >= @Input

EXEC usp_GetEmployeesSalaryAboveNumber  48100

--Problem 3. Town Names Starting With

CREATE PROCEDURE usp_GetTownsStartingWith @StartString VARCHAR(MAX)
AS
SELECT 
	[Name]
FROM
	Towns
WHERE
	SUBSTRING(Name,1, DATALENGTH(@StartString)) =  @StartString

EXEC usp_GetTownsStartingWith B


----Problem 4. Employees from Town
CREATE PROCEDURE usp_GetEmployeesFromTown @TownName VARCHAR(MAX)
AS
SELECT 
	E.FirstName AS [First Name]
	,E.LastName AS [Last Name]
FROM 
	Employees AS E
	JOIN Addresses AS A ON A.AddressID = E.AddressID
	JOIN Towns AS T ON T.TownID = A.TownID
WHERE
	T.Name = @TownName

EXEC usp_GetEmployeesFromTown Sofia


----Problem 5. Salary Level Function
CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS VARCHAR(10)
AS
BEGIN
	IF(@salary<30000)
		RETURN 'Low'
	ELSE IF(@salary BETWEEN 30000 AND 50000) 
		BEGIN
			RETURN 'Average'
		END
	RETURN 'High'
END

select dbo.ufn_GetSalaryLevel(43300.00)


----Problem 6. Employees by Salary Level
CREATE PROCEDURE usp_EmployeesBySalaryLevel @LevelOfSalary VARCHAR(MAX)
AS
SELECT
	FirstName AS [First Name]
	,LastName AS [Last Name]
FROM 
	Employees
WHERE
	@LevelOfSalary = dbo.ufn_GetSalaryLevel(Salary)

EXEC usp_EmployeesBySalaryLevel High



----Problem 7. Define Function

CREATE FUNCTION ufn_IsWordComprised(@setOfLetters VARCHAR(MAX), @word VARCHAR(MAX)) 
RETURNS BIT
AS 
BEGIN
	DECLARE @Counter INT = 1
	WHILE(@Counter <=DATALENGTH(@word))
		BEGIN
			IF(CHARINDEX(SUBSTRING(@word, @Counter, 1), @setOfLetters) <= 0)
			BEGIN
				RETURN 0
			END

		SET @Counter+=1
		END

		RETURN 1
END

SELECT dbo.ufn_IsWordComprised('pppp', 'Guy')


--Problem 9. Find Full Name
CREATE PROCEDURE usp_GetHoldersFullName
AS
SELECT 
	FirstName + ' ' + LastName AS [Full Name]
FROM
	AccountHolders

EXEC usp_GetHoldersFullName


--Problem 10. People with Balance Higher Than
CREATE PROCEDURE usp_GetHoldersWithBalanceHigherThan @Number DECIMAL(18,4)
AS
SELECT
	H.FirstName AS [First Name]
	,H.LastName AS [Last Name]
FROM
	(SELECT
		ACC.Id
		,ACC.FirstName
		,ACC.LastName
	FROM
		AccountHolders AS ACC
		JOIN Accounts AS A ON A.AccountHolderId = ACC.Id
	GROUP BY ACC.Id, ACC.FirstName, ACC.LastName
		HAVING SUM(A.Balance)>@Number) AS H
	ORDER BY
		H.FirstName
		,H.LastName

EXEC usp_GetHoldersWithBalanceHigherThan 50000


--Problem 11. Future Value Function
CREATE FUNCTION ufn_CalculateFutureValue (@Sum DECIMAL (8,4), @YearlyInterestRate FLOAT, @NumberOfYears INT)
RETURNS DECIMAL(8, 4)
AS
BEGIN
	DECLARE @FV DECIMAL(8,4)
	SET @FV = @Sum * ( POWER(1+@YearlyInterestRate, @NumberOfYears))
	
	RETURN @FV
END


SELECT dbo.ufn_CalculateFutureValue(1000, 0.1, 5)


--Problem 12. Calculating Interest
CREATE PROC usp_CalculateFutureValueForAccount @accountId INT, @interestRate FLOAT
AS
SELECT
	A.Id
	,AH.FirstName
	,AH.LastName
	,A.Balance 
	,dbo.ufn_CalculateFutureValue(A.Balance, 0.1, 5) AS ASD
FROM 
	Accounts AS A
	JOIN AccountHolders AS AH ON A.AccountHolderId = AH.Id
WHERE
	A.Id = @accountId

EXEC usp_CalculateFutureValueForAccount 1,0.1
	

--Problem 13. *Scalar Function: Cash in User Games Odd Rows
CREATE FUNCTION ufn_CashInUsersGames (@gameName VARCHAR(MAX))
RETURNS TABLE
RETURN(SELECT SUM(K.CASH) AS TotalCash
FROM(
SELECT
	G.Name
	,UG.Cash
	,ROW_NUMBER() OVER(ORDER BY Cash DESC) AS RowNumber
FROM
	GAMES AS G
	JOIN UsersGames AS UG ON UG.GameId = G.Id
WHERE
	G.Name = @gameName)AS K
	WHERE 
	K.RowNumber%2 =1)


SELECT * FROM ufn_CashInUsersGames('Love in a mist')