CREATE PROCEDURE [dbo].[spSessionAddUpdate]
	@id int = -1,
	@program_id int,
	@name nvarchar(50),
	@description nvarchar(255),
	@start datetime,
	@end datetime
AS
	BEGIN
		IF (@id = -1) BEGIN
			INSERT INTO [dbo].[Session] (program_id, name,description, start_date, end_date) 
				VALUES (@program_id, @name, @description, @start, @end)
			SELECT @@IDENTITY
		END ELSE BEGIN
			IF EXISTS (SELECT 1 FROM dbo.Session WHERE session_id = @id)				
				UPDATE [dbo].[Session] 
				SET  
				--program_id = @program_id,  --not yet
				[name] = @name, 
				description = @description, 
				start_date = @start, 
				end_date = @end,
				updated_datetime = GETDATE()
				WHERE session_id = @id
			SELECT @id
		END
	--TODO:  figure out how to handle program changes.... can it happen here?
	END

