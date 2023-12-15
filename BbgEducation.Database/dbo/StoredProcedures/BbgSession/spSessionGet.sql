CREATE PROCEDURE [dbo].[spSessionGet]
	@id int
AS
begin
	select session_id, session_name, description, start_date, end_date, inactivated_datetime, 
		program_id, program_name,
		user_id, user_name
	from dbo.[vSessions]
	where session_id = @id;
end
