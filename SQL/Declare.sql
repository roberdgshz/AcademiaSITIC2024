declare @StudentId int
exec [Students].[Insert] 'maria2','19950305','maria.escobar@gmail.com', @StudentId output
select @StudentId

declare @CourseId int
exec [Courses].[Insert] 'SITIC','305',@CourseId output
select @CourseId

declare @EnrollmentId int
exec [Enrollments].[Insert] @StudentId,@CourseId,@EnrollmentId output
select @EnrollmentId