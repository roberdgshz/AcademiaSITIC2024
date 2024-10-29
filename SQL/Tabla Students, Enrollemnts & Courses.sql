create database Exercises;
use Exercises;

create table [User]
(
	userID	 int identity(1,1) primary key,
	userName varchar(50) not null,
	Password varchar(50) not null
)

create table [dbo].[Students]
(
	studentId	int primary key identity(1,1),
	Name		varchar(150) not null,
	DateOfBirth date not null,
	Email		varchar(100),
)

create table Courses
(
	CourseId	int primary key identity(1,1),
	Name		varchar(100),
	Credits		int not null,
)

create table Enrollments
(
	EnrollmentId int primary key identity(1,1),
	StudentId	 int not null,
	CourseId	 int not null,
	foreign key (StudentID) references Students(StudentId),
	foreign key (CourseID)  references Courses (CourseId),
)

/*Consultas*/
select * from Courses
select * from Enrollments
select * from Students

