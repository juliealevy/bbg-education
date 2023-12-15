CREATE PROCEDURE [dbo].[spProgramAddUpdate]
	@id int = -1,	
	@program_name nvarchar(50),
	@description nvarchar(255)
AS
	
BEGIN
	IF (@id = -1)
		INSERT INTO [dbo].[Program] (name,description) 
			VALUES (@program_name, @description)
	ELSE
		IF EXISTS (SELECT 1 FROM dbo.Program WHERE program_id = @id)
			UPDATE [dbo].[Program] 
			SET  [name] = @program_name, description = @description
			WHERE program_id = @id

END
