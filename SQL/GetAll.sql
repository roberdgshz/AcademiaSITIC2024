create or alter procedure [Students].[GETAll]
as
begin
	select StudentId, Name, DateOfBirth, Email from Students 
end
go
exec sp_recompile N'[Students].[GetAll]'

exec [Students].[GETAll] 

-- Courses
create or alter procedure [Courses].[GETAll]
as
begin
	select CourseId, Name, Credits from Courses 
end
go
exec sp_recompile N'[Courses].[GetAll]'

exec [Courses].[GETAll] 

-- Enrollments
create or alter procedure [Enrollments].[GETAll]
as
begin
	select EnrollmentId, StudentId, CourseId from Enrollments 
end
go
exec sp_recompile N'[Enrollments].[GetAll]'

exec [Enrollments].[GETAll]

use Exercises;