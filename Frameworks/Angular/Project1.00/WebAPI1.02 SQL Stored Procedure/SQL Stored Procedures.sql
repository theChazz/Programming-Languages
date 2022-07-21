CREATE DATABASE [Angular1.00_DB];

USE [Angular1.00_DB];

CREATE TABLE Employees (
    EmployeeID int identity(1,1) primary key,
    Email varchar(255),
    Salary varchar(255)
);

/** SELECT ALL EMPLOYEES */
CREATE PROC usp_SelectEmployees
as
begin
select * from Employees
end

EXEC usp_SelectEmployees;


/** SELECT AN EMPLOYEE BY AN ID */
CREATE PROC usp_SelectEmployee
@EmployeeID int
as
begin
select * from Employees where EmployeeID = @EmployeeID
end

exec usp_SelectEmployee 6;


/** ADD AN EMPLOYEE */
CREATE PROCEDURE usp_AddEmployee (@Email VARCHAR(100), @Salary VARCHAR(100))
AS
BEGIN
	INSERT INTO Employees(Email, Salary)
	VALUES(@Email, @Salary)
END;

EXECUTE dbo.usp_AddEmployee '123@gmail.com', '4444';


/** UPDATE AN EMPLOYEE */
CREATE PROCEDURE usp_UpdateEmployee
@EmployeeID INT, 
@Email VARCHAR(100), 
@Salary VARCHAR(100)
AS
BEGIN
    UPDATE employees
    SET    Email = @Email,
            Salary = @Salary
    WHERE  EmployeeID = @EmployeeID
END;

EXECUTE dbo.usp_UpdateEmployee 6, 'email', '100'

/** DELETE AN EMPLOYEE BY FOLLOWING PARAMETER*/
CREATE PROC usp_DeleteEmployee (@EmployeeID INT)
AS
BEGIN
	DELETE FROM Employees 
	WHERE EmployeeID = @EmployeeID;
END;

EXECUTE dbo.usp_DeleteEmployee 7