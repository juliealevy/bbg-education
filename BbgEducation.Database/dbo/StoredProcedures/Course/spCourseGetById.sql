CREATE PROCEDURE [dbo].[spCourseGetById]
	@id int
AS
BEGIN
	select course_id, name as course_name, description, is_public
	from dbo.[Course]
	where course_id = @id;

END
