CREATE PROCEDURE [dbo].[spSessionGetAll]
	@IncludeInactive bit = 0
AS
BEGIN
	
	DECLARE @SQL nvarchar(255)

	SET @SQL = 
		'SELECT session_id, session_name,description, start_date, end_date, inactivated_datetime, '
		+ 'program_id, program_name, ' 
		+ 'user_id, user_name '
		+ 'FROM dbo.[vSessions]'

	IF @includeInactive = 0
		SET @SQL = @SQL + ' '
			+ 'WHERE inactivated_datetime IS NULL'
	
	EXEC (@SQL)

END