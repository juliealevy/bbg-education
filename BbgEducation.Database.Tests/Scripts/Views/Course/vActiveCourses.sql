CREATE VIEW [dbo].[vActiveCourses]
	AS 
	
	SELECT  course_id, name, description, is_public
	FROM dbo.[course]
	WHERE inactivated_datetime is null
