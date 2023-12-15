CREATE PROCEDURE [dbo].[spProgramGetAll]
AS
BEGIN

	SELECT program_id, name as program_name, description
	FROM dbo.[Program]


END
