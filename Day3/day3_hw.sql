--1.	In SQL Server, assuming you can find the result by using both joins and subqueries, which one would you 
--		prefer to use and why?
Subqueries can be used to return either a scalar (single) value or a row set; whereas, joins are used to return rows. 

--2.	What is CTE and when to use it?
The Common Table Expressions is preferred to use as an alternative to a Subquery/View.

--3.	What are Table Variables? What is their scope and where are they created in SQL Server?
The table variable is a special type of the local variable that helps to store data temporarily, the table variable 
provides all the properties of the local variable.

--4.	What is the difference between DELETE and TRUNCATE? Which one will have better performance and why?
TRUNCATE removes all rows in a table by deallocating the pages that are used to store the table data, DELETE removes 
rows one at a time. Delete command is slower than the Truncate command.

--5.	What is Identity column? How does DELETE and TRUNCATE affect it?
Identity column is used with the CREATE TABLE and ALTER TABLE Transact-SQL statements. Truncate command reset the 
identity to its seed value. Delete retains the identity and does not reset it to the seed value.

-------------------------------------
use Northwind;

--1.	List all cities that have both Employees and Customers.
select e.City
from Employees e
join Customers c on e.City = c.City
group by e.city
;

--2.	List all cities that have Customers but no Employee.
--		a.	Use sub-query
--		b.	Do not use sub-query
select c.City
from Customers c
where not exists (select * from Employees e where e.City = c.City)
;
select c.City
from Customers c
left join Employees e on c.City = e.City
where e.City is null
group by c.City
;

--3.	List all products and their total order quantities throughout all orders.
select p.ProductName, count(*)
from Orders o
left join [Order Details] od on o.OrderID = od.OrderID
left join Products p on p.ProductID = od.ProductID
group by p.ProductName
;

--4.	List all Customer Cities and total products ordered by that city.
select c.City, count(*)
from Customers c
group by c.City
;

--5.	List all Customer Cities that have at least two customers.
--		a.	Use union
--		b.	Use sub-query and no union
SELECT c.City, count(*) FROM Customers c group by c.City
having count(*) >= 2
UNION ALL
SELECT c.City, count(*) FROM Customers c group by c.City
having count(*) >= 2
;
select * 
from(
	select c.City, count(*) NumCus
	from Customers c
	group by c.City
	having count(*) >= 2
) t
;

--6.	List all Customer Cities that have ordered at least two different kinds of products.
select City, count(City)
from(
	select c.City, p.ProductName
	from Orders o
	left join [Order Details] od on o.OrderID = od.OrderID
	left join Products p on p.ProductID = od.ProductID
	left join Customers c on c.CustomerID = o.CustomerID
	group by c.City, p.ProductName
) t
group by City
having count(City)>=2
;

--7.	List all Customers who have ordered products, but have the ¡®ship city¡¯ on the order different from their own 
--		customer cities.
select c.ContactName
from Orders o
left join Customers c on o.CustomerID = c.CustomerID
where c.City <> o.ShipCity
group by c.ContactName
;

--8.	List 5 most popular products, their average price, and the customer city that ordered most quantity of it.
select t.ProductName, max(c.City)
from(
	select top 5 p.ProductID, p.ProductName, sum(od.Quantity) as total
	from [Order Details] od
	left join Products p on p.ProductID = od.ProductID
	group by p.ProductID, p.ProductName
	order by total desc
) t
left join [Order Details] od on t.ProductID = od.ProductID
left join Orders o on od.OrderID = o.OrderID
left join Customers c on o.CustomerID = c.CustomerID
group by t.ProductName, c.City
;

--9.	List all cities that have never ordered something but we have employees there.
--		a.	Use sub-query
--		b.	Do not use sub-query
select t.City
from(
	select c.City
	from Orders o
	left join Customers c on o.CustomerID = c.CustomerID
	where c.City is null
) t
left join Employees e on t.City = e.City
;
select c.City
from Orders o
left join Customers c on o.CustomerID = c.CustomerID
left join Employees e on c.City = e.City
where c.City is null
;

--10.	List one city, if exists, that is the city from where the employee sold most orders (not the product quantity)
--		is, and also the city of most total quantity of products ordered from. (tip: join  sub-query)
select e.EmployeeID, max(o.ShipCity)
from Employees e
left join Orders o on o.EmployeeID = e.EmployeeID
group by e.EmployeeID

--11.	How do you remove the duplicates record of a table?
By using GROUP BY clause or ROW_NUMBER() function.

--12.	Sample table to be used for solutions below- Employee ( empid integer, mgrid integer, deptid integer, salary 
--		integer) Dept (deptid integer, deptname text) Find employees who do not manage anybody.
select empid
from Employee
where mgrid is null
;

--13.	Find departments that have maximum number of employees. (solution should consider scenario having more than 1 
--		departments that have maximum number of employees). Result should only have - deptname, count of employees 
--		sorted by deptname.
select d.deptname, count(*)
from Employee e
join Dept d on e.deptid = d.deptid
group by d.deptname
order by d.deptname

--14.	Find top 3 employees (salary based) in every department. Result should have deptname, empid, salary sorted by 
--		deptname and then employee with high to low salary.
select top 3 d.deptname, e.empid, max(e.salary) salary
from Employee e
join Dept d on e.deptid = d.deptid
group by d.deptid
order by d.deptname, e.salary
