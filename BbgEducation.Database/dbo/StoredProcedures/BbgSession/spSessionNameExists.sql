CREATE PROCEDURE [dbo].[spSessionNameExists]
	@session_name varchar(100)
AS
BEGIN

	DECLARE @Exists int

	IF EXISTS (SELECT 1 from dbo.[Session] where name = @session_name)
	BEGIN
		SET @Exists = 1
	END
	ELSE
	BEGIN
		SET @Exists = 0
	END

	SELECT @Exists	

END
