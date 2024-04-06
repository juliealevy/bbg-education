CREATE PROCEDURE [dbo].[spSessionInactivate]	
	@id int
AS
BEGIN	
	UPDATE [dbo].[Session]
	SET inactivated_datetime = CURRENT_TIMESTAMP
	WHERE session_id = @id
END

