--if not exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[Students].[Insert]') and
--type in (N'P', N'PC')) begin
--	drop procedure [Students].[Insert]
--end
--go
create or alter procedure [Students].[Insert]
	@Name		 varchar(150),
	@DateOfBirth date,
	@Email		 varchar(100),
	@StudentId	 int output
--with encryption
as
begin
	insert into Students(Name, DateOfBirth, Email)
	values(@Name, @DateOfBirth, @Email)
	set @StudentId = SCOPE_IDENTITY()
end
go
exec sp_recompile N'[Students].[Insert]'

-- Courses
create or alter procedure [Courses].[Insert]
	@Name		 varchar(100),
	@Credits	 int,
	@CourseId	 int output
--with encryption
as
begin
	insert into Courses(Name, Credits)
	values(@Name, @Credits)
	set @CourseId = SCOPE_IDENTITY()
end
go
exec sp_recompile N'[Courses].[Insert]'

-- Enrollment
create or alter procedure [Enrollments].[Insert]
	@StudentId	  int,
	@CourseId	  int,
	@EnrollmentId int output
--with encryption
as
begin
	insert into Enrollments(StudentId, CourseId)
	values(@StudentId, @CourseId)
	set @EnrollmentId= SCOPE_IDENTITY()
end
go
exec sp_recompile N'[Enrollments].[Insert]'

---Update
-- Students
create or alter procedure [Students].[Update]
	@Name		 varchar(150),
	@DateOfBirth date,
	@Email		 varchar(100),
	@StudentId	 int
--with encryption
as
begin
	update Students set 
		Name = @Name,
		DateOfBirth = @DateOfBirth,
		Email = @Email
	where StudentId = @StudentId
end
go
exec sp_recompile N'[Students].[Insert]'

-- Courses
create or alter procedure [Courses].[Update]
	@Name		 varchar(100),
	@Credits	 int,
	@CourseId	 int output
--with encryption
as
begin
	update Courses set 
		Name = @Name,
		Credits = @Credits
	where CourseId = @CourseId
end
go
exec sp_recompile N'[Courses].[insert]'

-- Enrollments
create or alter procedure [Enrollments].[Update]
	@StudentId	  int,
	@CourseId	  int,
	@EnrollmentId int output
--with encryption
as
begin
	update Enrollments set 
		StudentId = @StudentId,
		CourseId = @CourseId
	where EnrollmentId = @EnrollmentId
end
go
exec sp_recompile N'[Enrollments].[Insert]'

--- Delete
-- Students
create or alter procedure [Students].[Delete]
	@StudentId int
as
begin
	delete Students where StudentId = @StudentId
end
go
exec sp_recompile N'[Students].[Delete]'

-- Courses
create or alter procedure [Courses].[Delete]
	@CourseId int
as
begin
	delete Courses where CourseId = @CourseId
end
go
exec sp_recompile N'[Courses].[Delete]'

-- Enrollments
create or alter procedure [Enrollments].[Delete]
	@EnrollmentId int
as
begin
	delete Enrollments where EnrollmentId = @EnrollmentId
end
go
exec sp_recompile N'[Enrollments].[Delete]'



use Exercises;
exec [Enrollments].[Delete] 4