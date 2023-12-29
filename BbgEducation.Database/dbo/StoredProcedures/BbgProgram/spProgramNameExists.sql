CREATE PROCEDURE [dbo].[spProgramNameExists]
	@program_name varchar(100)
AS
BEGIN

	DECLARE @Exists int

	IF EXISTS (SELECT 1 from dbo.[Program] where name = @program_name)
	BEGIN
		SET @Exists = 1
	END
	ELSE
	BEGIN
		SET @Exists = 0
	END

	RETURN @Exists	

END
