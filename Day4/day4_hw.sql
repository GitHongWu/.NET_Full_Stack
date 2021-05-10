/*
--1.	What is View? What are the benefits of using views?
view is a virtual table based on the result-set of an SQL statement. 
Views can represent a subset of the data contained in a table. Views 
can join and simplify multiple tables into a single virtual table.

--2.	Can data be modified through views?
You can't directly modify data in views based on union queries. You 
can't modify data in views that use GROUP BY or DISTINCT statements.

--3.	What is stored procedure and what are the benefits of using it?
A stored procedure is a precompiled set of one or more SQL statements 
that are stored on SQL Server. The benefit of Stored Procedures is that
they are executed on the server-side and perform a set of actions, 
before returning the results to the client-side.

--4.	What is the difference between view and stored procedure?
Views should be used to store commonly-used JOIN queries and specific 
columns to build virtual tables of an exact set of data we want to see.
Stored procedures hold the more complex logic, such as INSERT, DELETE,
and UPDATE statements to automate large SQL workflows.

--5.	What is the difference between stored procedure and functions?
The function must return a value but in Stored Procedure it is optional. Even a procedure can return zero or n values.
Functions can have only input parameters for it whereas Procedures can have input or output parameters.
Functions can be called from Procedure whereas Procedures cannot be called from a Function.

--6.	Can stored procedure return multiple result sets?
Yes

--7.	Can stored procedure be executed as part of SELECT Statement? Why?
Stored procedures are typically executed with an EXEC statement. 
However, you can execute a stored procedure implicitly from within a 
SELECT statement, provided that the stored procedure returns a result 
set.

--8.	What is Trigger? What types of Triggers are there?
A trigger is a special type of stored procedure in database that 
automatically invokes/runs/fires when an event occurs in the database 
server.
In SQL Server we can create four types of triggers Data Definition 
Language (DDL) triggers, Data Manipulation Language (DML) triggers, 
CLR triggers, and Logon triggers

--9.	What are the scenarios to use Triggers?
CREATE TRIGGER trigger_name ON { table | view } [ WITH ENCRYPTION ] 
   {FOR | AFTER | INSTEAD OF } 
	  { [ INSERT ][ , ][ UPDATE ] [ , ][DELETE ] }    AS 	{
        sql_statement [ ...n ]      }

--10.	What is the difference between Trigger and Stored Procedure?
Stored procedures are a pieces of the code in written in PL/SQL to do 
some specific task. On the other hand, trigger is a stored procedure 
that runs automatically when various events happen (eg update, insert,
delete).
*/

-------------------------------------------------------

use Northwind;

--1.	Lock tables Region, Territories, EmployeeTerritories and Employees. Insert following information into the database. In case of an error, no changes should be made to DB.
--		a.	A new region called “Middle Earth”;
--		b.	A new territory called “Gondor”, belongs to region “Middle Earth”;
--		c.	A new employee “Aragorn King” who's territory is “Gondor”.
INSERT INTO Region (RegionID, RegionDescription)
VALUES (5, 'Middle Earth')
;
INSERT INTO Territories
VALUES (00000, 'Gondor', 5)
;
INSERT INTO Territories
VALUES (00000, 'Gondor', 5)
;
INSERT INTO Employees(LastName, FirstName, City)
VALUES ('Aragorn', 'King', 'Gondor')
;
--

--2.	Change territory “Gondor” to “Arnor”.
update Employees
set City = 'Arnor'
where City = 'Gondor'
;

--3.	Delete Region “Middle Earth”. (tip: remove referenced data first) (Caution: do not forget WHERE or you will delete everything.) In case of an error, no changes should be made to DB. Unlock the tables mentioned in question 1.
ALTER TABLE Territories
ADD FOREIGN KEY (RegionID)
REFERENCES Region (RegionID) 
ON DELETE CASCADE
ON UPDATE CASCADE
;
DELETE FROM Region WHERE RegionDescription = 'Middle Earth'
;

--4.	Create a view named “view_product_order_[your_last_name]”, list all products and total ordered quantity for that product.
go;
CREATE VIEW view_product_order_Wu AS
select p.ProductID, p.ProductName, sum(od.Quantity) totalOrder
from Products p
left join [Order Details] od on p.ProductID = od.ProductID
group by p.ProductID, p.ProductName
go;

--5.	Create a stored procedure “sp_product_order_quantity_[your_last_name]” that accept product id as an input and total quantities of order as output parameter.
CREATE PROCEDURE sp_product_order_quantity_Wu @input int
AS
select p.ProductID, p.ProductName, sum(od.Quantity) totalOrder
from Products p
left join [Order Details] od on p.ProductID = od.ProductID
where p.ProductID = @input
group by p.ProductID, p.ProductName
order by sum(od.Quantity)
go;
EXEC sp_product_order_quantity_Wu @input = 10
go;

--6.	Create a stored procedure “sp_product_order_city_[your_last_name]” that accept product name as an input and top 5 cities that ordered most that product combined with the total quantity of that product ordered from that city as output.
CREATE PROCEDURE sp_product_order_city_Wu @input varchar(30)
AS
select top 5 ProductID, AVG(UnitPrice) as AvgPrice,
	(select top 1 City 
	from Customers c 
	join Orders o on o.CustomerID=c.CustomerID 
	join [Order Details] od2 on od2.OrderID=o.OrderID 
	where od2.ProductID=od1.ProductID 
	group by city 
	order by SUM(Quantity) desc) as City 
from [Order Details] od1
group by ProductID
order by sum(Quantity) desc
GO;--

--7.	Lock tables Region, Territories, EmployeeTerritories and Employees. Create a stored procedure “sp_move_employees_[your_last_name]” that automatically find all employees in territory “Tory”; if more than 0 found, insert a new territory “Stevens Point” of region “North” to the database, and then move those employees to “Stevens Point”.
CREATE PROCEDURE sp_move_employees_Wu
AS
IF EXISTS 
	(select *
	from Employees e
	join EmployeeTerritories et on e.EmployeeID = et.EmployeeID
	join Territories t on et.TerritoryID = t.TerritoryID
	where t.TerritoryDescription = 'Tory')
BEGIN
	INSERT INTO Territories --(TerritoryDescription, RegionID)
	VALUES (0, 'Stevens Point', 3)
	UPDATE Employees
	SET City = 'Stevens Point'
	where City = 'Tory'
END
go;
EXEC sp_move_employees_Wu
go;

--8.	Create a trigger that when there are more than 100 employees in territory “Stevens Point”, move them back to Troy. (After test your code,) remove the trigger. Move those employees back to “Troy”, if any. Unlock the tables.
create trigger trigger_wu
on Employees
for update
as
if (
	(select count(*) 
	from Employees 
	where City = 'Stevens Point') > 100
	)
begin
	UPDATE Employees
	SET City = 'Troy'
end
go;

--9.	Create 2 new tables “people_your_last_name” “city_your_last_name”. City table has two records: {Id:1, City: Seattle}, {Id:2, City: Green Bay}. People has three records: {id:1, Name: Aaron Rodgers, City: 2}, {id:2, Name: Russell Wilson, City:1}, {Id: 3, Name: Jody Nelson, City:2}. Remove city of Seattle. If there was anyone from Seattle, put them into a new city “Madison”. Create a view “Packers_your_name” lists all people from Green Bay. If any error occurred, no changes should be made to DB. (after test) Drop both tables and view.
create table city_Wu
	(
		id int primary key,
		City varchar(20)
	)
go;
INSERT INTO city_Wu VALUES (1, 'Seattle');
INSERT INTO city_Wu VALUES (2, 'Green Bay');
select * from city_Wu;

create table people_Wu
	(
		id int primary key,
		Name varchar(20),
		City int FOREIGN KEY REFERENCES city_Wu(id) ON DELETE CASCADE,
	)
go;
INSERT INTO people_Wu VALUES (1, 'Aaron Rodgers', 2);
INSERT INTO people_Wu VALUES (2, 'Russell Wilson', 1);
INSERT INTO people_Wu VALUES (3, ' Jody Nelson', 2);
select * from people_Wu;

if exists (
	select * 
	from people_Wu p
	join city_Wu c on p.City = c.id
	where c.City = 'Seattle')
begin
	INSERT INTO city_Wu VALUES (3, 'Madison')
	update people_Wu set City = 3 WHERE City = 1
	delete from city_Wu where City = 'Seattle'
end
go;

--10.	 Create a stored procedure “sp_birthday_employees_[you_last_name]” that creates a new table “birthday_employees_your_last_name” and fill it with all employees that have a birthday on Feb. (Make a screen shot) drop the table. Employee table should not be affected.
CREATE PROCEDURE sp_birthday_employees_Wu
AS
Select * Into birthday_employees_Wu From Employees Where 1 = 2
insert into birthday_employees_Wu select * from Employees where Month(BirthDate) = 2
go;

--11.	Create a stored procedure named “sp_your_last_name_1” that returns all cites that have at least 2 customers who have bought no or only one kind of product. Create a stored procedure named “sp_your_last_name_2” that returns the same but using a different approach. (sub-query and no-sub-query).
CREATE PROCEDURE sp_Wu_1
as
select c.City
from Customers c
join Orders o on o.CustomerID = c.CustomerID
Join [Order Details] od on o.OrderID = od.OrderID

--12.	How do you make sure two tables have the same data?
select * from tableA
minus
select * from tableB
If the query returns no rows then the data is exactly the same.

--14
SELECT CONCAT(FirstName,' ',LastName,' ',[MiddleName,'.']) as FullName
FROM table_name
;

--15
select top 1 *
from table_name
where Sex = 'F'
order by Marks
;

--16
select *
from table_name
order by CASE WHEN Sex = 'F' THEN '1'
              else Sex end, Marks
;







