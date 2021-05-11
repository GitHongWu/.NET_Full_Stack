/*
--1.
An object is any SQL Server resource, such as a SQL Server lock or Windows process. 
Each object contains one or more counters that determine various aspects of the 
objects to monitor.

--2.
Indexes are used to retrieve data from the database more quickly than otherwise. The 
users cannot see the indexes, they are just used to speed up searches/queries.

--3.
Clustered Index
Non-Clustered Index
Unique Index
Filtered Index
Columnstore Index
Hash Index

--4.
Yes, When you create a PRIMARY KEY constraint, a unique clustered index on the column 
or columns is automatically created if a clustered index on the table does not already
exist and you do not specify a unique nonclustered index. The primary key column cannot
allow NULL values.

--5
No, There can be only one clustered index per table, because the data rows themselves can be stored in only one order. The only time the data rows in a table are stored in sorted order is when the table contains a clustered index. When a table has a clustered index, the table is called a clustered table.

--6
Yes. Indexes can be composites - composed of multiple columns - and the order is important because of the leftmost principle.

--7
Yes, The first index created on a view must be a unique clustered index.

--8
Normalization rules divide larger tables into smaller tables and link them using relationships. The purpose of Normalization in SQL is to eliminate redundant (repetitive) data and ensure data is stored logically.
1NF(Normal Form) Rules: Each table cell should contain a single value. Each record needs to be unique.
2NF: Rule 1- Be in 1NF. Rule 2- Single Column Primary Key
3NF: Rule 1- Be in 2NF. Rule 2- Has no transitive functional dependencies

--9
Denormalization is a database optimization technique in which we add redundant data to one or more tables. This can help us avoid costly joins in a relational database.

--10
We can apply Entity integrity to the Table by specifying a primary key, unique key, and not null. Referential integrity ensures the relationship between the Tables. We can apply this using a Foreign Key constraint.

--11
Not Null Constraint
Check Constraint
Default Constraint
Unique Constraint
Primary Constraint
Foreign Constraint

--12
Primary key will not accept NULL values whereas Unique key can accept one NULL value.
A table can have only primary key whereas there can be multiple unique key on a table.
A Clustered index automatically created when a primary key is defined whereas Unique key generates the non-clustered index.

--13
A foreign key is a key used to link two tables together. This is sometimes also called as a referencing key. A Foreign Key is a column or a combination of columns whose values match a Primary Key in a different table.

--14
yes

--15
Yes, it can be NULL or duplicate. a Foreign key simply requires that the value in that field must exist first in a different table (the parent table). That is all an FK is by definition.Null by definition is not a value. Null means that we do not yet know what the value is.

--16
table variables support only adding such indexes implicitly by defining Primary key constraint or Unique key constraint during table creation.
temp tables support adding clustered and non-clustered indexes after the SQL Server temp table creation and implicitly by defining Primary key constraint or Unique Key constraint during the tables creation.

--17
A transaction is a unit of work that is performed against a database. Transactions are units or sequences of work accomplished in a logical order, whether in a manual fashion by a user or automatically by some sort of a database program.
Read Uncommitted (Lowest level)
Read Committed
Repeatable Read
Serializable (Highest Level)
Snapshot Isolation


*/
---------------------------------
use Northwind;

--1.  Write an sql statement that will display the name of each customer and the sum 
--	  of order totals placed by that customer during the year 2002
select c.ContactName, count(*) as totalOrders 
from Orders o
join Customers c on o.CustomerID = c.CustomerID
where year(o.OrderDate) = 2002
group by c.ContactName

--2.  The following table is used to store information about company¡¯s personnel:
--	  Create table person (id int, firstname varchar(100), lastname varchar(100)) 
--	  write a query that returns all employees whose last names start with ¡°A¡±
select *
from Person
where lastname LIKE 'A%'

--3.  The information about company¡¯s personnel is stored in the following table:
Create table person(person_id int primary key, manager_id int null, name varchar(100)not null) 

select name, (select count(*) from person p2 where p1.manager_id = p2.person_id)
from person p1
where manager_id is null

--4.  List all events that can cause a trigger to be executed.
--DML statements (INSERT, UPDATE, DELETE) on a particular table or view, issued by any user
--DDL statements (CREATE or ALTER primarily) issued either by a particular schema/user or by any schema/user in the database
--Database events, such as logon/logoff, errors, or startup/shutdown, also issued either by a particular schema/user or by any schema/user in the database

--5.	Generate a destination schema in 3rd Normal Form.  Include all necessary fact,
--		join, and dictionary tables, and all Primary and Foreign Key relationships.  
--		The following assumptions can be made:
create table Locations
(
	LocationID int primary key,
	Address varchar(20) not null,
)

create table Divisions
(
	DivisionID int primary key,
	DivisionName varchar(20) not null,
	LocationID int FOREIGN KEY REFERENCES Locations(LocationID),
)

create table Companies
(
	ID int primary key,
	Name varchar(20) not null,
	DivisionID int FOREIGN KEY REFERENCES Divisions(DivisionID),
)

create table Contacts
(
	ContactID int primary key,
	ContactName varchar(20) not null,
	ContactCompany int FOREIGN KEY REFERENCES Companies(ID),
	ContactDivision int FOREIGN KEY REFERENCES Divisions(DivisionID),
	ContactLocation int FOREIGN KEY REFERENCES Locations(LocationID),
)