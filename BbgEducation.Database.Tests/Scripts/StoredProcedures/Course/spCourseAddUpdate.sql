CREATE PROCEDURE [dbo].[spCourseAddUpdate]
	@id int = -1,	
	@course_name nvarchar(50),
	@description nvarchar(255),
	@is_public bit
AS
	
BEGIN
	IF (@id = -1) BEGIN
		INSERT INTO [dbo].[course] (name,description,is_public) 
			VALUES (@course_name, @description, @is_public)
			
		SELECT @@IDENTITY

	END ELSE BEGIN
		IF EXISTS (SELECT 1 FROM dbo.course WHERE course_id = @id)
			UPDATE [dbo].[course] 
			SET  [name] = @course_name, description = @description, is_public = @is_public
			WHERE course_id = @id

		SELECT @id
	END

END
