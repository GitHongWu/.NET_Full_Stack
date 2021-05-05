use AdventureWorks2019;

-- 1.	Write a query that retrieves the columns ProductID, Name, Color and ListPrice from the Production.Product table, with no filter.
select ProductID, Name, Color, ListPrice from Production.Product;

-- 2.	Write a query that retrieves the columns ProductID, Name, Color and ListPrice from the Production.Product table, the rows that are 0 for the column ListPrice
select ProductID, Name, Color, ListPrice from Production.Product
where ListPrice = 0
;

-- 3.	Write a query that retrieves the columns ProductID, Name, Color and ListPrice from the Production.Product table, the rows that are rows that are NULL for the Color column.
select ProductID, Name, Color, ListPrice from Production.Product
where Color is NULL
;

-- 4.	Write a query that retrieves the columns ProductID, Name, Color and ListPrice from the Production.Product table, the rows that are not NULL for the Color column.
select ProductID, Name, Color, ListPrice from Production.Product
where Color is not NULL
;

-- 5.	Write a query that retrieves the columns ProductID, Name, Color and ListPrice from the Production.Product table, the rows that are not NULL for the column Color, and the column ListPrice has a value greater than zero.
select ProductID, Name, Color, ListPrice from Production.Product
where Color is not NULL and ListPrice > 0
;

-- 6.	Generate a report that concatenates the columns Name and Color from the Production.Product table by excluding the rows that are null for color.
SELECT CONCAT(Name,' ',Color) as Name_Color
FROM Production.Product
where Color is not NULL
;

-- 7.	Write a query that generates the following result set  from Production.Product:
SELECT CONCAT('NAME: ', Name,' -- COLOR: ',Color) as Name_Color
FROM Production.Product
where Color is not NULL
;

-- 8.	Write a query to retrieve the to the columns ProductID and Name from the Production.Product table filtered by ProductID from 400 to 500
select ProductID, Name
from Production.Product
where ProductID >= 400 and ProductID <= 500
;

-- 9.	Write a query to retrieve the to the columns  ProductID, Name and color from the Production.Product table restricted to the colors black and blue
select ProductID, Name, Color
from Production.Product
where Color = 'Black' or Color = 'Blue'
;

-- 10.	Write a query to generate a report on products that begins with the letter S. 
select Name
from Production.Product
where Name LIKE 'S%'
;

-- 11.	Write a query that retrieves the columns Name and ListPrice from the Production.Product table. Your result set should look something like the following. Order the result set by the Name column. 
select Name, ListPrice
from Production.Product
where Name LIKE 'S%'
order by Name
;

-- 12.	 Write a query that retrieves the columns Name and ListPrice from the Production.Product table. Your result set should look something like the following. Order the result set by the Name column. The products name should start with either 'A' or 'S'
select Name, ListPrice
from Production.Product
where Name LIKE '[A, S]%'
order by Name
;

--13.	Write a query so you retrieve rows that have a Name that begins with the letters SPO, but is then not followed by the letter K. After this zero or more letters can exists. Order the result set by the Name column.
select Name
from Production.Product
where Name LIKE 'SPO%' and Name not LIKE '___k%'
;

-- 14.	Write a query that retrieves unique colors from the table Production.Product. Order the results  in descending  manner
select Color
from Production.Product
where Color is not NULL
group by Color
order by Color DESC
;

-- 15.	Write a query that retrieves the unique combination of columns ProductSubcategoryID and Color from the Production.Product table. Format and sort so the result set accordingly to the following. We do not want any rows that are NULL.in any of the two columns in the result.
select ProductSubcategoryID, Color
from Production.Product
where ProductSubcategoryID is not NULL and Color is not NULL
group by ProductSubcategoryID, Color
order by ProductSubcategoryID, Color
;

-- 16.	Something is ¡°wrong¡± with the WHERE clause in the following query. 
-- We do not want any Red or Black products from any SubCategory than those with the value of 1 in column ProductSubCategoryID, unless they cost between 1000 and 2000.
SELECT ProductSubCategoryID
      , LEFT([Name],35) AS [Name]
      , Color, ListPrice 
FROM Production.Product
WHERE Color not IN ('Red','Black') 
      OR ListPrice BETWEEN 1000 AND 2000 
      AND ProductSubCategoryID != 1
ORDER BY ProductID
;

-- 17.	Write the query in the editor and execute it. Take a look at the result set and then adjust the query so it delivers the following result set.
SELECT ProductSubCategoryID
      , LEFT([Name],35) AS [Name]
      , Color, ListPrice 
FROM Production.Product
WHERE 
      ListPrice BETWEEN 1000 AND 2000 
      AND ProductSubCategoryID IS NOT NULL
ORDER BY ProductID
;
