CREATE PROCEDURE [dbo].[spProgramAddUpdate]
	@id int = -1,	
	@program_name nvarchar(50),
	@description nvarchar(255)
AS
	
BEGIN
	IF (@id = -1) BEGIN
		INSERT INTO [dbo].[Program] (name,description) 
			VALUES (@program_name, @description)
			
		SELECT @@IDENTITY

	END ELSE BEGIN
		IF EXISTS (SELECT 1 FROM dbo.Program WHERE program_id = @id)
			UPDATE [dbo].[Program] 
			SET  [name] = @program_name, description = @description
			WHERE program_id = @id

		SELECT @id
	END

END
