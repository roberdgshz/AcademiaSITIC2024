create or alter procedure [Students].[GET]
	@StudentId int
as
begin
	select StudentId, Name, DateOfBirth, Email from Students 
	where @StudentId is null or (@StudentId is not null and studentId = @StudentId)
end
go
exec sp_recompile N'[Students].[Get]'

exec [Students].[GET] 1
drop procedure[Students].[GET]

-- Courses
create or alter procedure [Courses].[GET]
	@CourseId int
as
begin
	select CourseId, Name, Credits from Courses 
	where @CourseId is null or (@CourseId is not null and CourseId = @CourseId)
end
go
exec sp_recompile N'[Courses].[Get]'

exec [Courses].[GET] 1

-- Enrollments
create or alter procedure [Enrollments].[GET]
	@EnrollmentId int
as
begin
	select EnrollmentId, StudentId, CourseId from Enrollments 
	where @EnrollmentId is null or (@EnrollmentId is not null and EnrollmentId = @EnrollmentId)
end
go
exec sp_recompile N'[Enrollments].[Get]'

exec [Enrollments].[GET] 4
drop procedure [Enrollments].[GET]

use Exercises;