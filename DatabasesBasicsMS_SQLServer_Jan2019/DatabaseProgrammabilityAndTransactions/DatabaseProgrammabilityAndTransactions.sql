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


--Problem 14. Create Table Logs
CREATE TABLE Logs(
	LogId INT PRIMARY KEY IDENTITY
	,AccountId INT 
	,OldSum DECIMAL (15,2)
	,NewSum DECIMAL (15,2)
)

CREATE TRIGGER tr_InsertEntryIntoLogs ON Accounts FOR UPDATE
AS

	DECLARE @NewSum DECIMAL(15,2) = (SELECT Balance FROM inserted)
	DECLARE @OldSum DECIMAL(15,2) = (SELECT Balance FROM deleted)
	DECLARE @AccountId INT = (SELECT Id FROM inserted)
	
	INSERT INTO Logs(AccountId, NewSum, OldSum) VALUES
	(@AccountId, @NewSum, @OldSum)


--Problem 15. Create Table Emails
CREATE TABLE NotificationEmails(
	Id INT PRIMARY KEY IDENTITY
	,Recipient INT
	,Subject VARCHAR(MAX)
	,Body VARCHAR(MAX)
) 


CREATE TRIGGER tr_CreateNewEmails ON Logs FOR INSERT
AS
	DECLARE @newSum DECIMAL(15,2) = (SELECT NewSum FROM inserted)
	DECLARE @oldSum DECIMAL(15,2) = (SELECT OldSum FROM inserted)
	DECLARE @accountId INT = (SELECT TOP(1) AccountId FROM inserted)

INSERT INTO NotificationEmails(Recipient, Subject, Body) VALUES
(
@accountId
,'Balance change for account: '+ CAST(@accountId  AS VARCHAR(20))
,'On '+CONVERT(VARCHAR(30), GETDATE(), 103)+' your balance was changed from '+CAST(@oldSum AS VARCHAR(20)) +' to '+CAST(@newSum AS varchar(20)) 
)


--Problem 16. Deposit Money
CREATE PROC usp_DepositMoney (@AccountId INT, @MoneyAmount DECIMAL(15,4)) 
AS
BEGIN TRANSACTION
	DECLARE @CurrentAccout INT = (SELECT TOP(1) Id FROM Accounts WHERE Id = @AccountId)
	DECLARE @Ballance DECIMAL (15,4) = (SELECT Balance FROM Accounts WHERE Id = @CurrentAccout)

	IF(@CurrentAccout IS NULL)
	BEGIN
		ROLLBACK
		RAISERROR('Invalid Account Id!', 16,1)
		RETURN
	END

	
	UPDATE Accounts 
	SET Balance +=@MoneyAmount
	WHERE ID=@CurrentAccout
COMMIT


--Problem 17. Withdraw Money
CREATE PROC usp_WithdrawMoney (@AccountId INT, @MoneyAmount DECIMAL(15,4)) 
AS
BEGIN TRANSACTION
	DECLARE @CurrentAccount INT = (SELECT TOP(1)Id FROM Accounts WHERE Id = @AccountId)
	DECLARE @Balance DECIMAL(15,4) = (SELECT Balance FROM Accounts WHERE Id = @CurrentAccount)

	IF(@CurrentAccount IS NULL)
	BEGIN
		ROLLBACK
		RAISERROR('Invalid Account Id', 16,1)
		RETURN
	END

	IF(@Balance-@MoneyAmount<0)
	BEGIN
		ROLLBACK
		RAISERROR('Insufficient funds!', 16,1)
		RETURN
	END

	UPDATE Accounts
	SET Balance -=@MoneyAmount
	WHERE
		Id=@CurrentAccount

COMMIT


--Problem 18. Money Transfer
CREATE PROC usp_TransferMoney(@SenderId INT, @ReceiverId INT, @Amount DECIMAL(15,4))
AS
BEGIN TRANSACTION
	EXEC usp_WithdrawMoney @SenderId, @Amount
	EXEC usp_DepositMoney @ReceiverId, @Amount
COMMIT 


--Problem 21. Employees with Three Projects
CREATE PROC usp_AssignProject(@emloyeeId INT, @projectID INT)
AS
BEGIN TRANSACTION
	DECLARE @EmpId INT = (SELECT  EmployeeID FROM Employees WHERE EmployeeID = @emloyeeId)
	DECLARE @ProjId INT = (SELECT  ProjectID FROM Projects WHERE ProjectID = @projectID)
	
	IF(@EmpId IS NULL OR @ProjId IS NULL)
	BEGIN
		ROLLBACK
		RAISERROR('Invalid Employee Id!',16,1)
		RETURN
	END
	DECLARE @EmployeeProjectsCount INT = (SELECT COUNT(@EmpId) FROM EmployeesProjects WHERE EmployeeID = @EmpId)

	IF(@EmployeeProjectsCount >= 3)
	BEGIN
		ROLLBACK
		RAISERROR('The employee has too many projects!', 16,1)
		RETURN
	END
	
	INSERT INTO EmployeesProjects(EmployeeID, ProjectID) VALUES
	(@emloyeeId, @projectID)
	
COMMIT 


--Problem 22. Delete Employees
CREATE TABLE Deleted_Employees
(
EmployeeId INT PRIMARY KEY
,FirstName VARCHAR(MAX)
,LastName VARCHAR(MAX)
,MiddleName VARCHAR(MAX)
,JobTitle VARCHAR(MAX)
,DepartmentId INT
,Salary DECIMAL (15,2)
)

CREATE TRIGGER tr_DeleteEmployees ON Employees FOR DELETE
AS
INSERT INTO Deleted_Employees ( FirstName, LastName, MiddleName, JobTitle, DepartmentId, Salary)
SELECT FirstName, LastName, MiddleName, JobTitle, DepartmentId, Salary FROM deleted