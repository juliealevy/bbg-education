CREATE PROCEDURE [dbo].[spSessionGetAll]
	@IncludeInactive bit = 0,
	@ProgramId int = -1
AS
BEGIN
	
	DECLARE @SQL nvarchar(255)
	DECLARE @WHERE nvarchar(255) = ''

	IF @includeInactive = 0 BEGIN
		SET @WHERE = 'inactivated_datetime IS NULL'
	END
	IF @ProgramId <> -1 BEGIN
		IF (@WHERE <> '') BEGIN
			SET @WHERE = @WHERE + ' AND '
		END 
		SET @WHERE = @WHERE + 
			'program_id = ' + CAST(@ProgramId AS VARCHAR(20))
	END


	SET @SQL = 
		'SELECT session_id, session_name,description, start_date, end_date, inactivated_datetime'
		+ 'program_id, program_name, ' 
		+ 'user_id, user_name '
		+ 'FROM dbo.[vSessions]'

	IF @WHERE <> ''
		SET @SQL = @SQL + ' '
			+ 'WHERE ' + @WHERE
	
	EXEC (@SQL)
	

END