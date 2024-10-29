if not exists (select 1 from sys.schemas where name = 'Students') begin
	exec('create schema Students')
end

if not exists (select 1 from sys.schemas where name = 'Courses') begin
	exec('create schema Courses')
end

if not exists (select 1 from sys.schemas where name = 'Enrollments') begin
	exec('create schema Enrollments')
end