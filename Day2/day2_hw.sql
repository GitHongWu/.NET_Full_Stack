use AdventureWorks2019;

-- 1.	How many products can you find in the Production.Product table?
SELECT count(*) FROM Production.Product;

-- 2.	Write a query that retrieves the number of products in the Production.Product 
--		table that are included in a subcategory. The rows that have NULL in column 
--		ProductSubcategoryID are considered to not be a part of any subcategory.
SELECT count(*)
FROM Production.Product
WHERE ProductSubcategoryID IS NOT NULL
;

-- 3.	How many Products reside in each SubCategory? Write a query to display the results with the following titles.
--		ProductSubcategoryID CountedProducts
select ProductSubcategoryID, count(*) as CountedProducts
from Production.Product
group by ProductSubcategoryID
;

-- 4.	How many products that do not have a product subcategory. 
select count(*)
from Production.Product
where ProductSubcategoryID is NULL
;

-- 5.	Write a query to list the summary of products quantity in the Production.ProductInventory table.
select count(*)
from Production.ProductInventory
;

-- 6.	Write a query to list the summary of products in the Production.ProductInventory table and LocationID 
--		set to 40 and limit the result to include just summarized quantities less than 100
select ProductID, count(*) as TheSum
from Production.ProductInventory
where LocationID = 40 and Quantity < 100
group by ProductID
;

-- 7.	Write a query to list the summary of products with the shelf information in the Production.ProductInventory 
--		table and LocationID set to 40 and limit the result to include just summarized quantities less than 100
select Shelf, ProductID, count(*) as TheSum
from Production.ProductInventory
where LocationID = 40 and Quantity < 100
group by Shelf, ProductID
;

--8.	Write the query to list the average quantity for products where column LocationID has the value of 10 from 
--		the table Production.ProductInventory table.
select ProductID, avg(Quantity) as Average
from Production.ProductInventory
where LocationID = 10
group by ProductID
;

--9.	Write query to see the average quantity of products by shelf from the table Production.ProductInventory
select ProductID, Shelf, avg(Quantity) as TheAvg
from Production.ProductInventory
group by ProductID, Shelf
;

--10.	Write query  to see the average quantity  of  products by shelf excluding rows that has the value of N/A 
--		in the column Shelf from the table Production.ProductInventory
select ProductID, Shelf, avg(Quantity) as TheAvg
from Production.ProductInventory
where Shelf != 'N/A'
group by ProductID, Shelf
;

--11.	List the members (rows) and average list price in the Production.Product table. This should be grouped 
--		independently over the Color and the Class column. Exclude the rows where Color or Class are null.
select Color, Class, count(*) as TheCount, avg(ListPrice) as AvgPrice
from Production.Product
group by Color, Class
;

--12.	Write a query that lists the country and province names from person. CountryRegion and person. StateProvince 
--		tables. Join them and produce a result set similar to the following.
--		Country		Province
select Person.CountryRegion.CountryRegionCode, Person.StateProvince.StateProvinceCode as Province
from Person.CountryRegion
join Person.StateProvince on Person.CountryRegion.CountryRegionCode = Person.StateProvince.CountryRegionCode
;

--13.	Write a query that lists the country and province names from person. CountryRegion and person. StateProvince 
--		tables and list the countries filter them by Germany and Canada. Join them and produce a result set similar 
--		to the following.
select Person.CountryRegion.CountryRegionCode, Person.StateProvince.StateProvinceCode as Province
from Person.CountryRegion
join Person.StateProvince on Person.CountryRegion.CountryRegionCode = Person.StateProvince.CountryRegionCode
where Person.CountryRegion.Name = 'Germany' or Person.CountryRegion.Name = 'Canada'
;

use Northwind;

--14.	List all Products that has been sold at least once in last 25 years.
select Products.ProductName
from Orders
left join [Order Details] on Orders.OrderID = [Order Details].OrderID
left join Products on [Order Details].ProductID = Products.ProductID
where year(Orders.OrderDate) > 2021-25
group by Products.ProductName
;

--15.	List top 5 locations (Zip Code) where the products sold most.
select top 5 ShipPostalCode, count(ShipPostalCode) as zip
from Orders
group by ShipPostalCode
order by zip desc
;

--16.	List top 5 locations (Zip Code) where the products sold most in last 20 years.
select top 5 Orders.ShipPostalCode, count(ShipPostalCode) as zip
from Orders
left join [Order Details] on Orders.OrderID = [Order Details].OrderID
left join Products on [Order Details].ProductID = Products.ProductID
where year(Orders.OrderDate) > 2021-20
group by Orders.ShipPostalCode
order by zip desc
;

--17.	List all city names and number of customers in that city.
select City, count(*) as TheSum
from Customers
group by City
;

--18.	List city names which have more than 10 customers, and number of customers in that city 
select City, count(*) as TheSum
from Customers
group by City
having count(*) > 10
;

--19.	List the names of customers who placed orders after 1/1/98 with order date.
select ContactName
from Customers
left join Orders on Customers.CustomerID = Orders.CustomerID
where Orders.OrderDate > '1/1/98'
group by ContactName
;

--20.	List the names of all customers with most recent order dates 
select Customers.ContactName, max(Orders.OrderDate)
from Customers
left join Orders on Customers.CustomerID = Orders.CustomerID
group by ContactName, Orders.OrderDate
;

--21.	Display the names of all customers  along with the  count of products they bought
select Customers.ContactName, count(*) as TheSum
from Customers
left join Orders on Customers.CustomerID = Orders.CustomerID
group by ContactName
;

--22.	Display the customer ids who bought more than 100 Products with count of products.
select Customers.CustomerID, count(*) as TheSum
from Customers
left join Orders on Customers.CustomerID = Orders.CustomerID
group by Customers.CustomerID
having count(*) > 100
;

--23.	List all of the possible ways that suppliers can ship their products. Display the results as below
--		Supplier Company Name   	Shipping Company Name
select Suppliers.CompanyName as [Supplier Company Name], Shippers.CompanyName as [Shipping Company Name]
from Suppliers
cross join Shippers
;

--24.	Display the products order each day. Show Order date and Product Name.
select Orders.OrderDate, Products.ProductName
from Orders
left join [Order Details] on Orders.OrderID = [Order Details].OrderID
left join Products on [Order Details].ProductID = Products.ProductID
;

--25.	Displays pairs of employees who have the same job title.
select Title, count(*)
from Employees
group by Title
;

--26.	Display all the Managers who have more than 2 employees reporting to them.
select LastName, FirstName, ReportsTo
from Employees
where ReportsTo > 2
;

--27.	Display the customers and suppliers by city. The results should have the following columns
select City, ContactName
from Customers
UNION
SELECT City, ContactName
from Suppliers
;

-- 28
select F1.T1, F2.T2
from F1
inner join F2 on F1.T1 = F2.T2
;
--result
--	| F1.T1 | F2.T2 |
--	|	2	|	2	|
--	|	3	|	3	|

--29
select F1.T1, F2.T2
from F1
left join F2 on F1.T1 = F2.T2
;
--result
--	| F1.T1 | F2.T2 |
--	|	1	|  NULL	|
--	|	2	|	2	|
--	|	3	|	3	|