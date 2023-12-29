CREATE PROCEDURE [dbo].[spProgramGetById]
	@id int
AS
BEGIN
	select program_id, name as program_name, description
	from dbo.[Program]
	where program_id = @id;

END
