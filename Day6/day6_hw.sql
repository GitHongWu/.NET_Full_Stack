--1
create table Projects
(
	ProjectID int primary key,
	ProjectName varchar(20) not null,
	ProjectTitle varchar(20) not null,
	StartDate datetime,
	EndDate datetime,
	Budget decimal(5,3),
	ManagerID int FOREIGN KEY REFERENCES Employees(EmployeeID),
)

create table Employees
(
	EmployeeID int primary key,
	Name varchar(20) not null,
	Country varchar(20) not null,
	Address varchar(20) not null,
)

create table Offices
(
	OfficeID int primary key,
	Name varchar(20) not null,
	City varchar(20) not null,
	Country varchar(20) not null,
	Address varchar(20) not null,
	Phone varchar(20) not null,
	DirectorID int FOREIGN KEY REFERENCES Employees(EmployeeID),
	ProjectID int FOREIGN KEY REFERENCES Projects(ProjectID),
)

--2
create table Transactions
(
	TransactionID int primary key,
	Date datetime,
	Budget decimal(5,3),
	Rate decimal(5,3),
	LenderID int FOREIGN KEY REFERENCES Lenders(LenderID),
	BorrowerID int FOREIGN KEY REFERENCES Borrowers(BorrowerID),
)
create table Lenders
(
	LenderID int primary key,
	Name varchar(20) not null,
	RiskLevel varchar(20) not null,
)

create table Borrowers
(
	BorrowerID int primary key,
	Name varchar(20) not null,
	RiskLevel varchar(20) not null,
)

--3
create table Courses
(
	CourseID int primary key,
	Name varchar(20) not null,
	CategoryID int FOREIGN KEY REFERENCES Categories(CategoryID),
	ShortDescription varchar(100) not null,
	Photo varchar(max),
	Price int not null,
)

create table Employees
(
	EmployeeID int primary key,
	Name varchar(20) not null,
	Country varchar(20) not null,
	Address varchar(20) not null,
)


create table Categories
(
	CategoryID int primary key,
	CategoryName varchar(20) not null,
	ShortDescription varchar(100) not null,
	ManagerID int FOREIGN KEY REFERENCES Employees(EmployeeID),
)

create table Recipes
(
	RecipeID int primary key,
	RecipeName varchar(20) not null,
	Ingredient varchar(100) not null,
	Quantity int,
	UnitMeasurement varchar(20),
)