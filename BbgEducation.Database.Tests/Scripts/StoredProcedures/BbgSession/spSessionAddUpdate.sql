CREATE PROCEDURE [dbo].[spSessionAddUpdate]
	@id int = -1,
	@program_id int,
	@name nvarchar(50),
	@description nvarchar(255),
	@start datetime,
	@end datetime
AS
	BEGIN		
		IF NOT EXISTS (SELECT 1 FROM dbo.Program WHERE program_id = @program_id) 
					THROW 60000, 'Program Id is invalid', 1;	

		IF (@id = -1) BEGIN
			BEGIN TRY
				BEGIN TRANSACTION;									

				INSERT INTO [dbo].[Session] (program_id, name,description, start_date, end_date) 
					VALUES (@program_id, @name, @description, @start, @end)
				COMMIT TRANSACTION;
				SELECT @@IDENTITY
			END TRY
			BEGIN CATCH
				IF (XACT_STATE()) <> 0 
					ROLLBACK TRANSACTION;
				THROW;
			END CATCH			
		END ELSE BEGIN
			BEGIN TRY
				BEGIN TRANSACTION;				
					IF EXISTS (SELECT 1 FROM dbo.Session WHERE session_id = @id)				
						UPDATE [dbo].[Session] 
						SET  
						program_id = @program_id,
						[name] = @name, 
						description = @description, 
						start_date = @start, 
						end_date = @end,
						updated_datetime = GETDATE()
						WHERE session_id = @id					
				COMMIT TRANSACTION;
				SELECT @id
			END TRY
			BEGIN CATCH
				IF (XACT_STATE()) <> 0 
					ROLLBACK TRANSACTION;
				THROW;
			END CATCH
		END
	END