CREATE PROCEDURE [dbo].[spCourseGetAll]
AS
BEGIN

	SELECT course_id, name as course_name, description, is_public
	FROM dbo.[Course]


END
